using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;
using AgendaDeViajes.Services;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Módulo de Consultas y Reportes de AGENDA DE VIAJES.
    /// Presenta métricas consolidadas (KPIs), tablas analíticas de vuelos,
    /// reservaciones, pasajeros y aerolíneas, filtros por fecha y estado,
    /// exportación a CSV (UTF-8 con BOM y sanitización de fórmulas),
    /// generación nativa de documentos PDF A4 e impresión con vista previa.
    /// </summary>
    public partial class FrmReportes : Form
    {
        private readonly FrmPrincipal principal;

        private Label lblKpiVuelos;
        private Label lblKpiVuelosSub;
        private Label lblKpiPasajeros;
        private Label lblKpiPasajerosSub;
        private Label lblKpiReservaciones;
        private Label lblKpiReservacionesSub;
        private Label lblKpiIngresos;
        private Label lblKpiIngresosSub;

        // Metadatos y registros del reporte activo para exportación e impresión
        private string tituloReporteActual = "Reporte Ejecutivo";
        private string subtituloFiltrosActual = "Sin filtros aplicados";
        private readonly List<string> encabezadosActuales = new List<string>();
        private readonly List<float> anchosRelativosActuales = new List<float>();
        private readonly List<List<string>> filasActuales = new List<List<string>>();
        private readonly Dictionary<string, string> resumenMetricasActual = new Dictionary<string, string>();

        private bool suprimiendoEventosFiltro = false;

        public FrmReportes(FrmPrincipal principal)
        {
            this.principal = principal ?? throw new ArgumentNullException(nameof(principal));

            InitializeComponent();
            ConstruirKpis();
            InicializarFiltros();
            ConfigurarSelectorReportes();
            GenerarReporte();
        }

        private void ConstruirKpis()
        {
            flowKpis.Controls.Clear();

            CrearTarjetaKpi("Vuelos programados", out lblKpiVuelos, out lblKpiVuelosSub);
            CrearTarjetaKpi("Pasajeros registrados", out lblKpiPasajeros, out lblKpiPasajerosSub);
            CrearTarjetaKpi("Reservaciones activas", out lblKpiReservaciones, out lblKpiReservacionesSub);
            CrearTarjetaKpi("Ingresos confirmados", out lblKpiIngresos, out lblKpiIngresosSub);

            lblKpiIngresos.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
        }

        private void CrearTarjetaKpi(string titulo, out Label lblValor, out Label lblDetalle)
        {
            var tarjeta = new Panel
            {
                BackColor = Color.White,
                Margin = new Padding(0, 0, 10, 0),
                Size = new Size(220, 80)
            };

            var franja = new Panel
            {
                BackColor = Color.FromArgb(45, 156, 219),
                Dock = DockStyle.Left,
                Width = 4
            };

            var lblTitulo = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI Semibold", 8F),
                ForeColor = Color.FromArgb(107, 122, 140),
                Location = new Point(14, 8),
                Size = new Size(198, 16),
                Text = titulo.ToUpperInvariant()
            };

            lblValor = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 34, 47),
                Location = new Point(14, 25),
                Size = new Size(198, 28),
                Text = "—"
            };

            lblDetalle = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(140, 152, 166),
                Location = new Point(14, 55),
                Size = new Size(198, 18),
                Text = "Cálculo en curso..."
            };

            tarjeta.Controls.Add(franja);
            tarjeta.Controls.Add(lblTitulo);
            tarjeta.Controls.Add(lblValor);
            tarjeta.Controls.Add(lblDetalle);

            flowKpis.Controls.Add(tarjeta);
        }

        private void InicializarFiltros()
        {
            suprimiendoEventosFiltro = true;

            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today.AddDays(90);
            chkFiltrarFechas.Checked = false;
            dtpFechaDesde.Enabled = false;
            dtpFechaHasta.Enabled = false;

            suprimiendoEventosFiltro = false;
        }

        private void ConfigurarSelectorReportes()
        {
            suprimiendoEventosFiltro = true;

            cboTipoReporte.Items.Clear();
            cboTipoReporte.Items.Add("1. Ocupación y Estado de Vuelos");
            cboTipoReporte.Items.Add("2. Desglose de Reservaciones e Ingresos");
            cboTipoReporte.Items.Add("3. Directorio de Pasajeros Registrados");
            cboTipoReporte.Items.Add("4. Consolidado Operativo por Aerolínea");
            cboTipoReporte.SelectedIndex = 0;

            ActualizarOpcionesFiltroSecundario();

            suprimiendoEventosFiltro = false;
        }

        private void ActualizarOpcionesFiltroSecundario()
        {
            suprimiendoEventosFiltro = true;

            cboFiltroSecundario.Items.Clear();

            switch (cboTipoReporte.SelectedIndex)
            {
                case 0: // Vuelos
                    lblFiltroFechas.Text = "Fecha salida:";
                    chkFiltrarFechas.Enabled = true;
                    lblFiltroSecundario.Text = "Estado vuelo:";
                    cboFiltroSecundario.Items.Add("(Todos los estados)");
                    foreach (EstadoVuelo estado in Enum.GetValues(typeof(EstadoVuelo)))
                    {
                        cboFiltroSecundario.Items.Add(estado);
                    }
                    cboFiltroSecundario.SelectedIndex = 0;
                    break;

                case 1: // Reservaciones
                    lblFiltroFechas.Text = "Fecha emisión:";
                    chkFiltrarFechas.Enabled = true;
                    lblFiltroSecundario.Text = "Estado reserva:";
                    cboFiltroSecundario.Items.Add("(Todos los estados)");
                    foreach (EstadoReservacion estado in Enum.GetValues(typeof(EstadoReservacion)))
                    {
                        cboFiltroSecundario.Items.Add(estado);
                    }
                    cboFiltroSecundario.SelectedIndex = 0;
                    break;

                case 2: // Pasajeros
                    lblFiltroFechas.Text = "Fechas:";
                    chkFiltrarFechas.Checked = false;
                    chkFiltrarFechas.Enabled = false;
                    dtpFechaDesde.Enabled = false;
                    dtpFechaHasta.Enabled = false;
                    lblFiltroSecundario.Text = "Categoría:";
                    cboFiltroSecundario.Items.Add("(Todas las categorías)");
                    foreach (string cat in Catalogos.CategoriasPasajero)
                    {
                        cboFiltroSecundario.Items.Add(cat);
                    }
                    cboFiltroSecundario.SelectedIndex = 0;
                    break;

                case 3: // Consolidado Aerolíneas
                    lblFiltroFechas.Text = "Fechas:";
                    chkFiltrarFechas.Checked = false;
                    chkFiltrarFechas.Enabled = false;
                    dtpFechaDesde.Enabled = false;
                    dtpFechaHasta.Enabled = false;
                    lblFiltroSecundario.Text = "Aerolínea:";
                    cboFiltroSecundario.Items.Add("(Todas las aerolíneas)");
                    foreach (string aerolinea in Catalogos.Aerolineas)
                    {
                        cboFiltroSecundario.Items.Add(aerolinea);
                    }
                    cboFiltroSecundario.SelectedIndex = 0;
                    break;
            }

            suprimiendoEventosFiltro = false;
        }

        /// <summary>
        /// Recalcula los indicadores generales y genera el reporte correspondiente al tipo seleccionado y filtros activos.
        /// </summary>
        public void GenerarReporte()
        {
            ActualizarKpis();

            encabezadosActuales.Clear();
            anchosRelativosActuales.Clear();
            filasActuales.Clear();
            resumenMetricasActual.Clear();

            switch (cboTipoReporte.SelectedIndex)
            {
                case 0:
                    ReporteOcupacionVuelos();
                    break;
                case 1:
                    ReporteReservaciones();
                    break;
                case 2:
                    ReportePasajeros();
                    break;
                case 3:
                    ReporteConsolidadoAerolineas();
                    break;
            }
        }

        private void ActualizarKpis()
        {
            AgendaData datos = principal.Datos;
            if (datos == null)
            {
                lblKpiVuelos.Text = "0";
                lblKpiPasajeros.Text = "0";
                lblKpiReservaciones.Text = "0";
                lblKpiIngresos.Text = "$0.00";
                return;
            }

            // Vuelos
            int totalVuelos = datos.Vuelos?.Count ?? 0;
            int vuelosActivos = datos.Vuelos?.Count(v => v.Activo && v.Estado != EstadoVuelo.Cancelado) ?? 0;
            lblKpiVuelos.Text = totalVuelos.ToString();
            lblKpiVuelosSub.Text = $"{vuelosActivos} activos · {totalVuelos - vuelosActivos} inactivos o cancelados";

            // Pasajeros
            int totalPasajeros = datos.Pasajeros?.Count ?? 0;
            int adultos = datos.Pasajeros?.Count(p => string.Equals(p.Categoria, "Adulto", StringComparison.OrdinalIgnoreCase)) ?? 0;
            lblKpiPasajeros.Text = totalPasajeros.ToString();
            lblKpiPasajerosSub.Text = $"{adultos} adultos · {totalPasajeros - adultos} menores / infantes";

            // Reservaciones
            int totalReservaciones = datos.Reservaciones?.Count ?? 0;
            int reservacionesActivas = datos.Reservaciones?.Count(r => r.OcupaAsientos) ?? 0;
            lblKpiReservaciones.Text = reservacionesActivas.ToString();
            lblKpiReservacionesSub.Text = $"{reservacionesActivas} activas · {totalReservaciones - reservacionesActivas} canceladas";

            // Ingresos confirmados (excluye canceladas y pendientes)
            decimal totalIngresos = datos.Reservaciones?
                .Where(r => r.Estado == EstadoReservacion.Confirmada)
                .Sum(r => r.Total) ?? 0m;
            lblKpiIngresos.Text = Catalogos.FormatearMoneda(totalIngresos);
            lblKpiIngresosSub.Text = "Reservaciones confirmadas en USD";
        }

        #region Generación de Vistas Tabulares

        private void ReporteOcupacionVuelos()
        {
            tituloReporteActual = "Reporte de Ocupación y Estado de Vuelos";

            dgvReportes.SuspendLayout();
            dgvReportes.Columns.Clear();
            dgvReportes.Rows.Clear();

            // Configurar columnas de visualización
            dgvReportes.Columns.Add("colCodigo", "Código");
            dgvReportes.Columns.Add("colAerolinea", "Aerolínea");
            dgvReportes.Columns.Add("colOrigen", "Origen");
            dgvReportes.Columns.Add("colDestino", "Destino");
            dgvReportes.Columns.Add("colSalida", "Fecha y hora salida");
            dgvReportes.Columns.Add("colCapacidad", "Capacidad");
            dgvReportes.Columns.Add("colOcupados", "Reservados");
            dgvReportes.Columns.Add("colLibres", "Disponibles");
            dgvReportes.Columns.Add("colPorcentaje", "% Ocupación");
            dgvReportes.Columns.Add("colPrecio", "Tarifa base");
            dgvReportes.Columns.Add("colEstado", "Estado");

            dgvReportes.Columns["colCodigo"].Width = 80;
            dgvReportes.Columns["colAerolinea"].Width = 115;
            dgvReportes.Columns["colOrigen"].Width = 125;
            dgvReportes.Columns["colDestino"].Width = 125;
            dgvReportes.Columns["colSalida"].Width = 125;
            dgvReportes.Columns["colCapacidad"].Width = 85;
            dgvReportes.Columns["colOcupados"].Width = 85;
            dgvReportes.Columns["colLibres"].Width = 85;
            dgvReportes.Columns["colPorcentaje"].Width = 85;
            dgvReportes.Columns["colPrecio"].Width = 90;
            dgvReportes.Columns["colEstado"].Width = 95;

            // Metadatos para exportación
            encabezadosActuales.AddRange(new[]
            {
                "Código", "Aerolínea", "Origen", "Destino", "Fecha Salida",
                "Capacidad", "Reservados", "Disponibles", "% Ocupación", "Tarifa base", "Estado"
            });
            anchosRelativosActuales.AddRange(new float[] { 0.85f, 1.35f, 1.45f, 1.45f, 1.35f, 1.05f, 1.05f, 1.05f, 1.0f, 1.0f, 1.1f });

            // Aplicar filtros sobre datos reales
            IEnumerable<Vuelo> consulta = principal.Datos?.Vuelos ?? new List<Vuelo>();

            string filtroEstadoTexto = "(Todos)";
            if (cboFiltroSecundario.SelectedIndex > 0 && cboFiltroSecundario.SelectedItem is EstadoVuelo estadoSel)
            {
                consulta = consulta.Where(v => v.Estado == estadoSel);
                filtroEstadoTexto = estadoSel.ToString();
            }

            string filtroFechasTexto = "Sin límite temporal";
            if (chkFiltrarFechas.Checked)
            {
                DateTime desde = dtpFechaDesde.Value.Date;
                DateTime hasta = dtpFechaHasta.Value.Date;
                consulta = consulta.Where(v => v.FechaSalida.Date >= desde && v.FechaSalida.Date <= hasta);
                filtroFechasTexto = $"{desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
            }

            subtituloFiltrosActual = $"Filtro Estado: {filtroEstadoTexto} | Rango Fechas: {filtroFechasTexto}";

            var vuelosFiltrados = consulta.OrderBy(v => v.FechaSalida).ToList();

            foreach (Vuelo v in vuelosFiltrados)
            {
                string origenTexto = $"{v.Origen} - {Catalogos.ObtenerCiudad(v.Origen)}";
                string destinoTexto = $"{v.Destino} - {Catalogos.ObtenerCiudad(v.Destino)}";
                string fechaTexto = v.FechaSalida.ToString("dd/MM/yyyy HH:mm");
                string ocupacionTexto = $"{v.OcupacionPorcentual:F1}%";
                string precioTexto = Catalogos.FormatearMoneda(v.Precio);

                dgvReportes.Rows.Add(
                    v.Codigo,
                    v.Aerolinea,
                    origenTexto,
                    destinoTexto,
                    fechaTexto,
                    v.Capacidad,
                    v.AsientosReservados,
                    v.AsientosDisponibles,
                    ocupacionTexto,
                    precioTexto,
                    v.Estado.ToString()
                );

                filasActuales.Add(new List<string>
                {
                    v.Codigo,
                    v.Aerolinea,
                    origenTexto,
                    destinoTexto,
                    fechaTexto,
                    v.Capacidad.ToString(),
                    v.AsientosReservados.ToString(),
                    v.AsientosDisponibles.ToString(),
                    ocupacionTexto,
                    precioTexto,
                    v.Estado.ToString()
                });
            }

            dgvReportes.ResumeLayout();

            // Cálculo de indicadores específicos del reporte
            int totalVuelos = vuelosFiltrados.Count;
            int programados = vuelosFiltrados.Count(v => v.Estado == EstadoVuelo.Programado);
            int cancelados = vuelosFiltrados.Count(v => v.Estado == EstadoVuelo.Cancelado);
            int finalizados = vuelosFiltrados.Count(v => v.Estado == EstadoVuelo.Finalizado);
            int capacidadTotal = vuelosFiltrados.Sum(v => v.Capacidad);
            int reservadosTotal = vuelosFiltrados.Sum(v => v.AsientosReservados);
            double ocupacionGeneral = capacidadTotal > 0 ? (double)reservadosTotal / capacidadTotal * 100.0 : 0.0;

            resumenMetricasActual["Total vuelos"] = totalVuelos.ToString();
            resumenMetricasActual["Programados"] = programados.ToString();
            resumenMetricasActual["Cancelados"] = cancelados.ToString();
            resumenMetricasActual["Finalizados"] = finalizados.ToString();
            resumenMetricasActual["Capacidad total ofertada"] = capacidadTotal.ToString();
            resumenMetricasActual["Asientos ocupados"] = reservadosTotal.ToString();
            resumenMetricasActual["Ocupación general"] = $"{ocupacionGeneral:F1}%";

            lblResumenReporte.Text = $"Reporte de Ocupación: {totalVuelos} vuelos analizados · Capacidad total: {capacidadTotal} · Asientos reservados: {reservadosTotal} ({ocupacionGeneral:F1}%) · Cancelados: {cancelados}";
        }

        private void ReporteReservaciones()
        {
            tituloReporteActual = "Reporte de Desglose de Reservaciones e Ingresos";

            dgvReportes.SuspendLayout();
            dgvReportes.Columns.Clear();
            dgvReportes.Rows.Clear();

            dgvReportes.Columns.Add("colCodigo", "Código");
            dgvReportes.Columns.Add("colVuelo", "Vuelo programado");
            dgvReportes.Columns.Add("colPasajero", "Pasajero titular");
            dgvReportes.Columns.Add("colFecha", "Fecha emisión");
            dgvReportes.Columns.Add("colAsientos", "Asientos");
            dgvReportes.Columns.Add("colTarifa", "Tarifa");
            dgvReportes.Columns.Add("colSeguro", "Seguro");
            dgvReportes.Columns.Add("colServicios", "Servicios adicionales");
            dgvReportes.Columns.Add("colTotal", "Total (USD)");
            dgvReportes.Columns.Add("colEstado", "Estado");

            dgvReportes.Columns["colCodigo"].Width = 90;
            dgvReportes.Columns["colVuelo"].Width = 135;
            dgvReportes.Columns["colPasajero"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReportes.Columns["colFecha"].Width = 120;
            dgvReportes.Columns["colAsientos"].Width = 70;
            dgvReportes.Columns["colTarifa"].Width = 85;
            dgvReportes.Columns["colSeguro"].Width = 65;
            dgvReportes.Columns["colServicios"].Width = 130;
            dgvReportes.Columns["colTotal"].Width = 95;
            dgvReportes.Columns["colEstado"].Width = 95;

            encabezadosActuales.AddRange(new[]
            {
                "Código", "Vuelo", "Pasajero titular", "Fecha emisión",
                "Asientos", "Tarifa", "Seguro", "Servicios adicionales", "Total (USD)", "Estado"
            });
            anchosRelativosActuales.AddRange(new float[] { 1.0f, 1.5f, 2.3f, 1.4f, 0.8f, 1.0f, 0.8f, 1.6f, 1.1f, 1.1f });

            IEnumerable<Reservacion> consulta = principal.Datos?.Reservaciones ?? new List<Reservacion>();

            string filtroEstadoTexto = "(Todos)";
            if (cboFiltroSecundario.SelectedIndex > 0 && cboFiltroSecundario.SelectedItem is EstadoReservacion estadoSel)
            {
                consulta = consulta.Where(r => r.Estado == estadoSel);
                filtroEstadoTexto = estadoSel.ToString();
            }

            string filtroFechasTexto = "Sin límite temporal";
            if (chkFiltrarFechas.Checked)
            {
                DateTime desde = dtpFechaDesde.Value.Date;
                DateTime hasta = dtpFechaHasta.Value.Date;
                consulta = consulta.Where(r => r.FechaReserva.Date >= desde && r.FechaReserva.Date <= hasta);
                filtroFechasTexto = $"{desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
            }

            subtituloFiltrosActual = $"Filtro Estado: {filtroEstadoTexto} | Rango Fechas: {filtroFechasTexto}";

            var reservacionesFiltradas = consulta.OrderByDescending(r => r.FechaReserva).ToList();

            foreach (Reservacion r in reservacionesFiltradas)
            {
                Vuelo v = principal.Datos?.BuscarVuelo(r.CodigoVuelo);
                Pasajero p = principal.Datos?.BuscarPasajero(r.DocumentoPasajero);

                string vueloTexto = v != null ? $"{r.CodigoVuelo} ({v.Origen}→{v.Destino})" : r.CodigoVuelo;
                string pasajeroTexto = p != null ? $"{p.NombreCompleto} ({p.Documento})" : r.DocumentoPasajero;
                string serviciosTexto = r.Servicios != null && r.Servicios.Count > 0 ? string.Join(", ", r.Servicios) : "—";
                string fechaTexto = r.FechaReserva.ToString("dd/MM/yyyy HH:mm");
                string totalTexto = Catalogos.FormatearMoneda(r.Total);

                int filaIdx = dgvReportes.Rows.Add(
                    r.Codigo,
                    vueloTexto,
                    pasajeroTexto,
                    fechaTexto,
                    r.CantidadAsientos,
                    r.Tarifa.ToString(),
                    r.Seguro ? "Sí" : "No",
                    serviciosTexto,
                    totalTexto,
                    r.Estado.ToString()
                );

                if (r.Estado == EstadoReservacion.Cancelada)
                {
                    dgvReportes.Rows[filaIdx].DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                }
                else if (r.Estado == EstadoReservacion.Pendiente)
                {
                    dgvReportes.Rows[filaIdx].DefaultCellStyle.ForeColor = Color.FromArgb(180, 110, 20);
                }

                filasActuales.Add(new List<string>
                {
                    r.Codigo,
                    vueloTexto,
                    pasajeroTexto,
                    fechaTexto,
                    r.CantidadAsientos.ToString(),
                    r.Tarifa.ToString(),
                    r.Seguro ? "Sí" : "No",
                    serviciosTexto,
                    totalTexto,
                    r.Estado.ToString()
                });
            }

            dgvReportes.ResumeLayout();

            // Cálculo de indicadores específicos
            int totalReservas = reservacionesFiltradas.Count;
            int confirmadas = reservacionesFiltradas.Count(r => r.Estado == EstadoReservacion.Confirmada);
            int pendientes = reservacionesFiltradas.Count(r => r.Estado == EstadoReservacion.Pendiente);
            int canceladas = reservacionesFiltradas.Count(r => r.Estado == EstadoReservacion.Cancelada);
            int asientosOcupados = reservacionesFiltradas.Where(r => r.OcupaAsientos).Sum(r => r.CantidadAsientos);
            decimal ingresosConfirmados = reservacionesFiltradas.Where(r => r.Estado == EstadoReservacion.Confirmada).Sum(r => r.Total);
            decimal montosCancelados = reservacionesFiltradas.Where(r => r.Estado == EstadoReservacion.Cancelada).Sum(r => r.Total);

            resumenMetricasActual["Total reservaciones"] = totalReservas.ToString();
            resumenMetricasActual["Confirmadas"] = confirmadas.ToString();
            resumenMetricasActual["Pendientes"] = pendientes.ToString();
            resumenMetricasActual["Canceladas"] = canceladas.ToString();
            resumenMetricasActual["Asientos comprometidos"] = asientosOcupados.ToString();
            resumenMetricasActual["Ingresos confirmados (USD)"] = Catalogos.FormatearMoneda(ingresosConfirmados);
            resumenMetricasActual["Importes cancelados (USD)"] = Catalogos.FormatearMoneda(montosCancelados);

            lblResumenReporte.Text = $"Reporte de Reservaciones: {totalReservas} registros · Confirmadas: {confirmadas} · Canceladas: {canceladas} · Ingresos confirmados: {Catalogos.FormatearMoneda(ingresosConfirmados)} (Cancelados: {Catalogos.FormatearMoneda(montosCancelados)})";
        }

        private void ReportePasajeros()
        {
            tituloReporteActual = "Directorio y Registro de Pasajeros";

            dgvReportes.SuspendLayout();
            dgvReportes.Columns.Clear();
            dgvReportes.Rows.Clear();

            dgvReportes.Columns.Add("colDoc", "Documento");
            dgvReportes.Columns.Add("colNombre", "Nombre completo");
            dgvReportes.Columns.Add("colCorreo", "Correo electrónico");
            dgvReportes.Columns.Add("colTel", "Teléfono");
            dgvReportes.Columns.Add("colNacimiento", "Fecha nac.");
            dgvReportes.Columns.Add("colEdad", "Edad");
            dgvReportes.Columns.Add("colNac", "Nacionalidad");
            dgvReportes.Columns.Add("colCat", "Categoría");
            dgvReportes.Columns.Add("colAsistencia", "Asistencia esp.");
            dgvReportes.Columns.Add("colReservas", "Reservas asociadas");

            dgvReportes.Columns["colDoc"].Width = 100;
            dgvReportes.Columns["colNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReportes.Columns["colCorreo"].Width = 150;
            dgvReportes.Columns["colTel"].Width = 95;
            dgvReportes.Columns["colNacimiento"].Width = 85;
            dgvReportes.Columns["colEdad"].Width = 55;
            dgvReportes.Columns["colNac"].Width = 105;
            dgvReportes.Columns["colCat"].Width = 80;
            dgvReportes.Columns["colAsistencia"].Width = 95;
            dgvReportes.Columns["colReservas"].Width = 100;

            encabezadosActuales.AddRange(new[]
            {
                "Documento", "Nombre completo", "Correo electrónico", "Teléfono",
                "Fecha nac.", "Edad", "Nacionalidad", "Categoría", "Asistencia", "Reservaciones"
            });
            anchosRelativosActuales.AddRange(new float[] { 1.2f, 2.4f, 1.8f, 1.2f, 1.0f, 0.7f, 1.3f, 1.0f, 1.1f, 1.1f });

            IEnumerable<Pasajero> consulta = principal.Datos?.Pasajeros ?? new List<Pasajero>();

            string filtroCatTexto = "(Todas)";
            if (cboFiltroSecundario.SelectedIndex > 0 && cboFiltroSecundario.SelectedItem is string catSel)
            {
                consulta = consulta.Where(p => string.Equals(p.Categoria, catSel, StringComparison.OrdinalIgnoreCase));
                filtroCatTexto = catSel;
            }

            subtituloFiltrosActual = $"Filtro Categoría: {filtroCatTexto}";

            var pasajerosFiltrados = consulta.OrderBy(p => p.Apellidos).ToList();
            var reservaciones = principal.Datos?.Reservaciones ?? new List<Reservacion>();

            int pasajerosConReservas = 0;

            foreach (Pasajero p in pasajerosFiltrados)
            {
                int cantReservas = reservaciones.Count(r =>
                    string.Equals(r.DocumentoPasajero, p.Documento, StringComparison.OrdinalIgnoreCase));

                if (cantReservas > 0)
                {
                    pasajerosConReservas++;
                }

                string fechaNacTexto = p.FechaNacimiento > DateTime.MinValue ? p.FechaNacimiento.ToString("dd/MM/yyyy") : "—";
                string edadTexto = $"{p.Edad} años";
                string asistenciaTexto = p.RequiereAsistencia ? "Sí" : "No";

                dgvReportes.Rows.Add(
                    p.Documento,
                    p.NombreCompleto,
                    p.Email,
                    p.Telefono,
                    fechaNacTexto,
                    edadTexto,
                    p.Nacionalidad,
                    p.Categoria,
                    asistenciaTexto,
                    $"{cantReservas} reserva(s)"
                );

                filasActuales.Add(new List<string>
                {
                    p.Documento,
                    p.NombreCompleto,
                    p.Email,
                    p.Telefono,
                    fechaNacTexto,
                    edadTexto,
                    p.Nacionalidad,
                    p.Categoria,
                    asistenciaTexto,
                    cantReservas.ToString()
                });
            }

            dgvReportes.ResumeLayout();

            int totalPasajeros = pasajerosFiltrados.Count;
            int adultos = pasajerosFiltrados.Count(p => string.Equals(p.Categoria, "Adulto", StringComparison.OrdinalIgnoreCase));
            int menores = totalPasajeros - adultos;
            int sinReservas = totalPasajeros - pasajerosConReservas;

            resumenMetricasActual["Total pasajeros"] = totalPasajeros.ToString();
            resumenMetricasActual["Adultos"] = adultos.ToString();
            resumenMetricasActual["Menores / Infantes"] = menores.ToString();
            resumenMetricasActual["Con reservaciones"] = pasajerosConReservas.ToString();
            resumenMetricasActual["Sin reservaciones"] = sinReservas.ToString();

            lblResumenReporte.Text = $"Directorio de Pasajeros: {totalPasajeros} clientes analizados · Con reservaciones: {pasajerosConReservas} · Sin reservaciones: {sinReservas} · Adultos: {adultos}";
        }

        private void ReporteConsolidadoAerolineas()
        {
            tituloReporteActual = "Consolidado Operativo y Comercial por Aerolínea";

            dgvReportes.SuspendLayout();
            dgvReportes.Columns.Clear();
            dgvReportes.Rows.Clear();

            dgvReportes.Columns.Add("colAerolinea", "Aerolínea");
            dgvReportes.Columns.Add("colVuelos", "Vuelos programados");
            dgvReportes.Columns.Add("colCapacidad", "Capacidad total ofertada");
            dgvReportes.Columns.Add("colReservados", "Asientos reservados");
            dgvReportes.Columns.Add("colDisponibles", "Asientos libres");
            dgvReportes.Columns.Add("colOcupacion", "% Ocupación media");
            dgvReportes.Columns.Add("colIngresos", "Ingresos confirmados (USD)");

            dgvReportes.Columns["colAerolinea"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReportes.Columns["colVuelos"].Width = 130;
            dgvReportes.Columns["colCapacidad"].Width = 140;
            dgvReportes.Columns["colReservados"].Width = 125;
            dgvReportes.Columns["colDisponibles"].Width = 120;
            dgvReportes.Columns["colOcupacion"].Width = 120;
            dgvReportes.Columns["colIngresos"].Width = 150;

            encabezadosActuales.AddRange(new[]
            {
                "Aerolínea", "Vuelos programados", "Capacidad total", "Asientos reservados",
                "Asientos disponibles", "% Ocupación media", "Ingresos confirmados (USD)"
            });
            anchosRelativosActuales.AddRange(new float[] { 2.2f, 1.4f, 1.4f, 1.3f, 1.3f, 1.3f, 1.7f });

            var vuelos = principal.Datos?.Vuelos ?? new List<Vuelo>();
            var reservaciones = principal.Datos?.Reservaciones ?? new List<Reservacion>();

            IEnumerable<string> listaAerolineas = Catalogos.Aerolineas;

            string filtroAeroTexto = "(Todas)";
            if (cboFiltroSecundario.SelectedIndex > 0 && cboFiltroSecundario.SelectedItem is string aeroSel)
            {
                listaAerolineas = listaAerolineas.Where(a => string.Equals(a, aeroSel, StringComparison.OrdinalIgnoreCase));
                filtroAeroTexto = aeroSel;
            }

            subtituloFiltrosActual = $"Filtro Aerolínea: {filtroAeroTexto}";

            var grupos = listaAerolineas.Select(aerolinea =>
            {
                var vuelosAerolinea = vuelos.Where(v => string.Equals(v.Aerolinea, aerolinea, StringComparison.OrdinalIgnoreCase)).ToList();
                int totalVuelosAero = vuelosAerolinea.Count;
                int capacidad = vuelosAerolinea.Sum(v => v.Capacidad);
                int reservados = vuelosAerolinea.Sum(v => v.AsientosReservados);
                int libres = Math.Max(0, capacidad - reservados);
                double ocupacion = capacidad > 0 ? (double)reservados / capacidad * 100.0 : 0.0;

                var codigosVuelos = new HashSet<string>(vuelosAerolinea.Select(v => v.Codigo), StringComparer.OrdinalIgnoreCase);
                decimal ingresos = reservaciones
                    .Where(r => r.Estado == EstadoReservacion.Confirmada && codigosVuelos.Contains(r.CodigoVuelo))
                    .Sum(r => r.Total);

                return new
                {
                    Aerolinea = aerolinea,
                    TotalVuelos = totalVuelosAero,
                    Capacidad = capacidad,
                    Reservados = reservados,
                    Libres = libres,
                    Ocupacion = ocupacion,
                    Ingresos = ingresos
                };
            }).ToList();

            foreach (var g in grupos)
            {
                string ocupacionTexto = $"{g.Ocupacion:F1}%";
                string ingresosTexto = Catalogos.FormatearMoneda(g.Ingresos);

                dgvReportes.Rows.Add(
                    g.Aerolinea,
                    g.TotalVuelos,
                    g.Capacidad,
                    g.Reservados,
                    g.Libres,
                    ocupacionTexto,
                    ingresosTexto
                );

                filasActuales.Add(new List<string>
                {
                    g.Aerolinea,
                    g.TotalVuelos.ToString(),
                    g.Capacidad.ToString(),
                    g.Reservados.ToString(),
                    g.Libres.ToString(),
                    ocupacionTexto,
                    ingresosTexto
                });
            }

            dgvReportes.ResumeLayout();

            int totalVuelosConsolidado = grupos.Sum(g => g.TotalVuelos);
            int capacidadTotalConsolidada = grupos.Sum(g => g.Capacidad);
            int reservadosTotalConsolidada = grupos.Sum(g => g.Reservados);
            decimal ingresosTotalesConsolidada = grupos.Sum(g => g.Ingresos);
            double ocupacionGlobal = capacidadTotalConsolidada > 0 ? (double)reservadosTotalConsolidada / capacidadTotalConsolidada * 100.0 : 0.0;

            resumenMetricasActual["Aerolíneas operativas"] = grupos.Count.ToString();
            resumenMetricasActual["Vuelos totales"] = totalVuelosConsolidado.ToString();
            resumenMetricasActual["Capacidad ofertada"] = capacidadTotalConsolidada.ToString();
            resumenMetricasActual["Asientos reservados"] = reservadosTotalConsolidada.ToString();
            resumenMetricasActual["Ocupación media"] = $"{ocupacionGlobal:F1}%";
            resumenMetricasActual["Ingresos confirmados (USD)"] = Catalogos.FormatearMoneda(ingresosTotalesConsolidada);

            lblResumenReporte.Text = $"Consolidado por Aerolíneas: {grupos.Count} compañías operando · Vuelos: {totalVuelosConsolidado} · Capacidad: {capacidadTotalConsolidada} · Ingresos totales: {Catalogos.FormatearMoneda(ingresosTotalesConsolidada)}";
        }

        #endregion

        #region Eventos y Manejadores

        private void cboTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suprimiendoEventosFiltro)
            {
                return;
            }

            ActualizarOpcionesFiltroSecundario();
            GenerarReporte();
        }

        private void FiltrosCambiados(object sender, EventArgs e)
        {
            if (suprimiendoEventosFiltro)
            {
                return;
            }

            dtpFechaDesde.Enabled = chkFiltrarFechas.Checked;
            dtpFechaHasta.Enabled = chkFiltrarFechas.Checked;

            if (chkFiltrarFechas.Checked && dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
            {
                MessageBox.Show(
                    this,
                    "La fecha inicial del intervalo no puede ser posterior a la fecha final.",
                    "Intervalo de fechas inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            GenerarReporte();
        }

        private void btnRestablecerFiltros_Click(object sender, EventArgs e)
        {
            suprimiendoEventosFiltro = true;

            chkFiltrarFechas.Checked = false;
            dtpFechaDesde.Enabled = false;
            dtpFechaHasta.Enabled = false;
            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today.AddDays(90);

            if (cboFiltroSecundario.Items.Count > 0)
            {
                cboFiltroSecundario.SelectedIndex = 0;
            }

            suprimiendoEventosFiltro = false;

            GenerarReporte();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            if (filasActuales.Count == 0)
            {
                DialogResult respuestaVacio = MessageBox.Show(
                    this,
                    "El reporte actual no contiene registros según los filtros seleccionados.\r\n\r\n¿Desea exportar el archivo CSV únicamente con los encabezados e indicadores?",
                    "Reporte sin registros",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuestaVacio != DialogResult.Yes)
                {
                    return;
                }
            }

            // Advertencia de privacidad para directorio de pasajeros
            if (cboTipoReporte.SelectedIndex == 2)
            {
                DialogResult respuestaPrivacidad = MessageBox.Show(
                    this,
                    "ADVERTENCIA DE SEGURIDAD Y PRIVACIDAD:\r\n\r\n" +
                    "El informe seleccionado contiene información personal sensible de los clientes (documentos de identidad, números telefónicos y correos electrónicos).\r\n\r\n" +
                    "¿Confirma que desea exportar estos datos personales a un archivo CSV externo?",
                    "Advertencia de datos sensibles",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuestaPrivacidad != DialogResult.Yes)
                {
                    return;
                }
            }

            string nombreSugerido = GenerarNombreArchivoSugerido("csv");

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Guardar reporte en formato CSV (Excel)";
                sfd.Filter = "Valores separados por comas (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
                sfd.FileName = nombreSugerido;
                sfd.DefaultExt = "csv";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        ExportadorCsv.GuardarCsv(
                            sfd.FileName,
                            tituloReporteActual,
                            encabezadosActuales,
                            filasActuales,
                            resumenMetricasActual);

                        MessageBox.Show(
                            this,
                            $"El archivo CSV fue exportado exitosamente:\r\n\r\n{sfd.FileName}\r\n\r\n" +
                            $"• Registros exportados: {filasActuales.Count}\r\n" +
                            $"• Codificación: UTF-8 con BOM (Compatible con Microsoft Excel)\r\n" +
                            "• Protección contra inyección de fórmulas activada.",
                            "Exportación exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            this,
                            $"Ocurrió un error al guardar el archivo CSV:\r\n\r\n{ex.Message}",
                            "Error de exportación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            if (filasActuales.Count == 0)
            {
                DialogResult respuestaVacio = MessageBox.Show(
                    this,
                    "El reporte actual no contiene registros según los filtros seleccionados.\r\n\r\n¿Desea generar el documento PDF con la estructura oficial e indicadores?",
                    "Reporte sin registros",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuestaVacio != DialogResult.Yes)
                {
                    return;
                }
            }

            // Advertencia de privacidad para directorio de pasajeros
            if (cboTipoReporte.SelectedIndex == 2)
            {
                DialogResult respuestaPrivacidad = MessageBox.Show(
                    this,
                    "ADVERTENCIA DE SEGURIDAD Y PRIVACIDAD:\r\n\r\n" +
                    "El informe seleccionado contiene información personal sensible de los clientes (documentos de identidad, números telefónicos y correos electrónicos).\r\n\r\n" +
                    "¿Confirma que desea exportar estos datos personales a un documento PDF?",
                    "Advertencia de datos sensibles",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuestaPrivacidad != DialogResult.Yes)
                {
                    return;
                }
            }

            string nombreSugerido = GenerarNombreArchivoSugerido("pdf");

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Guardar reporte en formato PDF";
                sfd.Filter = "Documentos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
                sfd.FileName = nombreSugerido;
                sfd.DefaultExt = "pdf";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        GeneradorPdfReporte.GenerarPdf(
                            sfd.FileName,
                            tituloReporteActual,
                            subtituloFiltrosActual,
                            encabezadosActuales,
                            anchosRelativosActuales,
                            filasActuales,
                            resumenMetricasActual);

                        MessageBox.Show(
                            this,
                            $"El documento PDF fue generado exitosamente:\r\n\r\n{sfd.FileName}\r\n\r\n" +
                            $"• Formato: A4 Apaisado (Landscape), ISO 32000-1 / PDF-1.4\r\n" +
                            $"• Registros procesados: {filasActuales.Count}\r\n" +
                            "• Paginación automática y encabezados corporativos integrados.",
                            "Exportación PDF exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            this,
                            $"Ocurrió un error al generar el archivo PDF:\r\n\r\n{ex.Message}",
                            "Error de exportación PDF",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (filasActuales.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "No hay registros disponibles en el reporte activo para enviar a la impresora.",
                    "Impresión de reportes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var impresor = new ImpresorReporte(
                    tituloReporteActual,
                    subtituloFiltrosActual,
                    encabezadosActuales,
                    anchosRelativosActuales,
                    filasActuales,
                    resumenMetricasActual))
                {
                    using (var preview = new PrintPreviewDialog())
                    {
                        preview.Document = impresor;
                        preview.Width = 1050;
                        preview.Height = 720;
                        preview.StartPosition = FormStartPosition.CenterParent;
                        preview.Text = $"Vista previa de impresión — {tituloReporteActual}";

                        // Mostrar la vista previa de impresión (si el usuario cierra sin imprimir, no se emite falso éxito)
                        preview.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Ocurrió un error al inicializar el subsistema de impresión de Windows:\r\n\r\n{ex.Message}",
                    "Error de impresión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string GenerarNombreArchivoSugerido(string extension)
        {
            string prefijo = cboTipoReporte.SelectedIndex switch
            {
                0 => "Reporte_Ocupacion_Vuelos",
                1 => "Reporte_Reservaciones_Ingresos",
                2 => "Reporte_Directorio_Pasajeros",
                3 => "Reporte_Consolidado_Aerolineas",
                _ => "Reporte_Agenda_Viajes"
            };

            string fechaSello = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return $"{prefijo}_{fechaSello}.{extension}";
        }

        #endregion
    }
}
