using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Módulo de Consultas y Reportes de AGENDA DE VIAJES.
    /// Presenta métricas consolidadas (KPIs), tablas analíticas de vuelos,
    /// reservaciones y pasajeros, y botones de exportación documental.
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

        public FrmReportes(FrmPrincipal principal)
        {
            this.principal = principal ?? throw new ArgumentNullException(nameof(principal));

            InitializeComponent();
            ConstruirKpis();
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

        private void ConfigurarSelectorReportes()
        {
            cboTipoReporte.Items.Clear();
            cboTipoReporte.Items.Add("1. Ocupación y Estado de Vuelos");
            cboTipoReporte.Items.Add("2. Desglose de Reservaciones e Ingresos");
            cboTipoReporte.Items.Add("3. Directorio de Pasajeros Registrados");
            cboTipoReporte.Items.Add("4. Consolidado Operativo por Aerolínea");
            cboTipoReporte.SelectedIndex = 0;
        }

        /// <summary>
        /// Recalcula los indicadores y regenera la tabla de reporte actual.
        /// </summary>
        public void GenerarReporte()
        {
            ActualizarKpis();

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

            // Ingresos
            decimal totalIngresos = datos.Reservaciones?
                .Where(r => r.Estado == EstadoReservacion.Confirmada)
                .Sum(r => r.Total) ?? 0m;
            lblKpiIngresos.Text = Catalogos.FormatearMoneda(totalIngresos);
            lblKpiIngresosSub.Text = "Reservaciones confirmadas en USD";
        }

        #region Generación de Vistas Tabulares

        private void ReporteOcupacionVuelos()
        {
            dgvReportes.SuspendLayout();
            dgvReportes.Columns.Clear();
            dgvReportes.Rows.Clear();

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

            dgvReportes.Columns["colCodigo"].Width = 90;
            dgvReportes.Columns["colAerolinea"].Width = 120;
            dgvReportes.Columns["colOrigen"].Width = 110;
            dgvReportes.Columns["colDestino"].Width = 110;
            dgvReportes.Columns["colSalida"].Width = 130;
            dgvReportes.Columns["colCapacidad"].Width = 75;
            dgvReportes.Columns["colOcupados"].Width = 75;
            dgvReportes.Columns["colLibres"].Width = 75;
            dgvReportes.Columns["colPorcentaje"].Width = 85;
            dgvReportes.Columns["colPrecio"].Width = 90;
            dgvReportes.Columns["colEstado"].Width = 95;

            var vuelos = principal.Datos?.Vuelos ?? new List<Vuelo>();
            foreach (Vuelo v in vuelos.OrderBy(v => v.FechaSalida))
            {
                dgvReportes.Rows.Add(
                    v.Codigo,
                    v.Aerolinea,
                    $"{v.Origen} · {Catalogos.ObtenerCiudad(v.Origen)}",
                    $"{v.Destino} · {Catalogos.ObtenerCiudad(v.Destino)}",
                    v.FechaSalida.ToString("dd/MM/yyyy HH:mm"),
                    v.Capacidad,
                    v.AsientosReservados,
                    v.AsientosDisponibles,
                    $"{v.OcupacionPorcentual:F1}%",
                    Catalogos.FormatearMoneda(v.Precio),
                    v.Estado.ToString()
                );
            }

            dgvReportes.ResumeLayout();
            lblResumenReporte.Text = $"Reporte de Ocupación: {vuelos.Count} vuelos analizados · Fecha de corte: {DateTime.Now:dd/MM/yyyy HH:mm}";
        }

        private void ReporteReservaciones()
        {
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
            dgvReportes.Columns["colVuelo"].Width = 140;
            dgvReportes.Columns["colPasajero"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReportes.Columns["colFecha"].Width = 120;
            dgvReportes.Columns["colAsientos"].Width = 70;
            dgvReportes.Columns["colTarifa"].Width = 85;
            dgvReportes.Columns["colSeguro"].Width = 65;
            dgvReportes.Columns["colServicios"].Width = 140;
            dgvReportes.Columns["colTotal"].Width = 95;
            dgvReportes.Columns["colEstado"].Width = 95;

            var reservaciones = principal.Datos?.Reservaciones ?? new List<Reservacion>();
            foreach (Reservacion r in reservaciones.OrderByDescending(r => r.FechaReserva))
            {
                Vuelo v = principal.Datos?.BuscarVuelo(r.CodigoVuelo);
                Pasajero p = principal.Datos?.BuscarPasajero(r.DocumentoPasajero);

                string vueloTexto = v != null ? $"{r.CodigoVuelo} ({v.Origen}→{v.Destino})" : r.CodigoVuelo;
                string pasajeroTexto = p != null ? $"{p.NombreCompleto} ({p.Documento})" : r.DocumentoPasajero;
                string serviciosTexto = r.Servicios != null && r.Servicios.Count > 0 ? string.Join(", ", r.Servicios) : "—";

                dgvReportes.Rows.Add(
                    r.Codigo,
                    vueloTexto,
                    pasajeroTexto,
                    r.FechaReserva.ToString("dd/MM/yyyy HH:mm"),
                    r.CantidadAsientos,
                    r.Tarifa.ToString(),
                    r.Seguro ? "Sí" : "No",
                    serviciosTexto,
                    Catalogos.FormatearMoneda(r.Total),
                    r.Estado.ToString()
                );
            }

            dgvReportes.ResumeLayout();
            lblResumenReporte.Text = $"Reporte de Reservaciones: {reservaciones.Count} registros consolidados · Total acumulado: {Catalogos.FormatearMoneda(reservaciones.Sum(r => r.Total))}";
        }

        private void ReportePasajeros()
        {
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

            dgvReportes.Columns["colDoc"].Width = 100;
            dgvReportes.Columns["colNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReportes.Columns["colCorreo"].Width = 160;
            dgvReportes.Columns["colTel"].Width = 100;
            dgvReportes.Columns["colNacimiento"].Width = 90;
            dgvReportes.Columns["colEdad"].Width = 60;
            dgvReportes.Columns["colNac"].Width = 110;
            dgvReportes.Columns["colCat"].Width = 80;
            dgvReportes.Columns["colAsistencia"].Width = 100;

            var pasajeros = principal.Datos?.Pasajeros ?? new List<Pasajero>();
            foreach (Pasajero p in pasajeros.OrderBy(p => p.Apellidos))
            {
                dgvReportes.Rows.Add(
                    p.Documento,
                    p.NombreCompleto,
                    p.Email,
                    p.Telefono,
                    p.FechaNacimiento > DateTime.MinValue ? p.FechaNacimiento.ToString("dd/MM/yyyy") : "—",
                    $"{p.Edad} años",
                    p.Nacionalidad,
                    p.Categoria,
                    p.RequiereAsistencia ? "Sí (Prioritaria)" : "No"
                );
            }

            dgvReportes.ResumeLayout();
            lblResumenReporte.Text = $"Directorio de Pasajeros: {pasajeros.Count} clientes registrados en el sistema.";
        }

        private void ReporteConsolidadoAerolineas()
        {
            dgvReportes.SuspendLayout();
            dgvReportes.Columns.Clear();
            dgvReportes.Rows.Clear();

            dgvReportes.Columns.Add("colAerolinea", "Aerolínea");
            dgvReportes.Columns.Add("colVuelos", "Vuelos programados");
            dgvReportes.Columns.Add("colCapacidad", "Capacidad total ofertada");
            dgvReportes.Columns.Add("colReservados", "Asientos reservados");
            dgvReportes.Columns.Add("colDisponibles", "Asientos libres");
            dgvReportes.Columns.Add("colOcupacion", "% Ocupación media");
            dgvReportes.Columns.Add("colIngresos", "Ingresos generados (USD)");

            dgvReportes.Columns["colAerolinea"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReportes.Columns["colVuelos"].Width = 130;
            dgvReportes.Columns["colCapacidad"].Width = 140;
            dgvReportes.Columns["colReservados"].Width = 125;
            dgvReportes.Columns["colDisponibles"].Width = 120;
            dgvReportes.Columns["colOcupacion"].Width = 120;
            dgvReportes.Columns["colIngresos"].Width = 150;

            var vuelos = principal.Datos?.Vuelos ?? new List<Vuelo>();
            var reservaciones = principal.Datos?.Reservaciones ?? new List<Reservacion>();

            var grupos = Catalogos.Aerolineas.Select(aerolinea =>
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
                dgvReportes.Rows.Add(
                    g.Aerolinea,
                    g.TotalVuelos,
                    g.Capacidad,
                    g.Reservados,
                    g.Libres,
                    $"{g.Ocupacion:F1}%",
                    Catalogos.FormatearMoneda(g.Ingresos)
                );
            }

            dgvReportes.ResumeLayout();
            lblResumenReporte.Text = $"Consolidado por Aerolíneas: {grupos.Count} compañías operando en la agenda.";
        }

        #endregion

        #region Eventos

        private void cboTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this,
                "Esta funcionalidad estará disponible en la siguiente etapa de integración",
                "Consultas y Reportes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion
    }
}

