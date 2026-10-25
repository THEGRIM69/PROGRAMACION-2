using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Panel principal de la aplicación: indicadores de la agencia, próximos
    /// vuelos, ocupación global y por aerolínea, filtro por fecha y umbral de
    /// ocupación. Toda la información se calcula a partir de los datos cargados
    /// por FrmPrincipal; el formulario no almacena datos propios.
    /// </summary>
    public partial class FrmDashboard : Form
    {
        private const int MaximoVuelosProximos = 12;

        private static readonly Color ColorAzulMarino = Color.FromArgb(22, 34, 47);
        private static readonly Color ColorCeleste = Color.FromArgb(45, 156, 219);
        private static readonly Color ColorTexto = Color.FromArgb(30, 42, 56);
        private static readonly Color ColorTextoSecundario = Color.FromArgb(107, 122, 140);
        private static readonly Color ColorFilaAlterna = Color.FromArgb(248, 250, 252);
        private static readonly Color ColorResaltado = Color.FromArgb(255, 243, 205);
        private static readonly Color ColorPeligro = Color.FromArgb(192, 57, 43);

        private AgendaData datos;

        private Label lblValorVuelos;
        private Label lblValorActivos;
        private Label lblValorPasajeros;
        private Label lblValorReservas;
        private Label lblValorIngresos;

        public FrmDashboard()
        {
            InitializeComponent();
            ConstruirTarjetas();
            monthCalendario.SetDate(DateTime.Today);
            ActualizarEtiquetaUmbral();
            Refrescar();
        }

        /// <summary>
        /// Asigna los datos de la agenda y actualiza todos los indicadores.
        /// </summary>
        public void ActualizarDatos(AgendaData datos)
        {
            this.datos = datos;
            Refrescar();
        }

        /// <summary>
        /// Vuelve a calcular los indicadores con los datos actuales.
        /// </summary>
        public void Refrescar()
        {
            if (datos == null)
            {
                MostrarEstadoVacio();
                return;
            }

            MostrarTarjetas();
            MostrarOcupaciones();
            FiltrarVuelos();
        }

        #region Tarjetas informativas

        private void ConstruirTarjetas()
        {
            flowTarjetas.Controls.Clear();

            lblValorVuelos = CrearTarjeta("Vuelos", "registrados");
            lblValorActivos = CrearTarjeta("Vuelos activos", "en operación");
            lblValorPasajeros = CrearTarjeta("Pasajeros", "registrados");
            lblValorReservas = CrearTarjeta("Reservaciones", "registradas");
            lblValorIngresos = CrearTarjeta("Ingresos (USD)", "reservaciones confirmadas");

            lblValorIngresos.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        }

        private Label CrearTarjeta(string titulo, string subtitulo)
        {
            var tarjeta = new Panel
            {
                BackColor = Color.White,
                Margin = new Padding(0, 0, 12, 0),
                Size = new Size(174, 92)
            };

            var franja = new Panel
            {
                BackColor = ColorCeleste,
                Location = new Point(0, 0),
                Size = new Size(4, 92)
            };

            var lblTitulo = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = ColorTextoSecundario,
                Location = new Point(16, 12),
                Size = new Size(146, 16),
                Text = titulo
            };

            var lblValor = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = ColorAzulMarino,
                Location = new Point(16, 30),
                Size = new Size(146, 32),
                Text = "0"
            };

            var lblSubtitulo = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(140, 152, 166),
                Location = new Point(16, 66),
                Size = new Size(146, 16),
                Text = subtitulo
            };

            tarjeta.Controls.Add(franja);
            tarjeta.Controls.Add(lblTitulo);
            tarjeta.Controls.Add(lblValor);
            tarjeta.Controls.Add(lblSubtitulo);
            flowTarjetas.Controls.Add(tarjeta);

            return lblValor;
        }

        private void MostrarTarjetas()
        {
            int vuelosActivos = datos.Vuelos.Count(v =>
                v.Activo && v.Estado != EstadoVuelo.Cancelado && v.Estado != EstadoVuelo.Finalizado);

            decimal ingresos = datos.Reservaciones
                .Where(r => r.Estado == EstadoReservacion.Confirmada)
                .Sum(r => r.Total);

            lblValorVuelos.Text = datos.Vuelos.Count.ToString(Catalogos.Cultura);
            lblValorActivos.Text = vuelosActivos.ToString(Catalogos.Cultura);
            lblValorPasajeros.Text = datos.Pasajeros.Count.ToString(Catalogos.Cultura);
            lblValorReservas.Text = datos.Reservaciones.Count.ToString(Catalogos.Cultura);
            lblValorIngresos.Text = Catalogos.FormatearMoneda(ingresos);
        }

        #endregion

        #region Ocupación

        private void MostrarOcupaciones()
        {
            List<Vuelo> vigentes = datos.Vuelos
                .Where(v => v.Estado != EstadoVuelo.Cancelado)
                .ToList();

            double ocupacionGlobal = CalcularOcupacion(vigentes);

            pbOcupacionGlobal.Value = ConvertirPorcentaje(ocupacionGlobal);
            lblPorcentajeGlobal.Text = ocupacionGlobal.ToString("0.0", Catalogos.Cultura) + " %";

            ConstruirOcupacionAerolineas(vigentes);
        }

        private void ConstruirOcupacionAerolineas(List<Vuelo> vuelos)
        {
            tablaAerolineas.SuspendLayout();
            tablaAerolineas.Controls.Clear();
            tablaAerolineas.RowStyles.Clear();
            tablaAerolineas.RowCount = 0;

            List<IGrouping<string, Vuelo>> grupos = vuelos
                .GroupBy(v => string.IsNullOrWhiteSpace(v.Aerolinea) ? "Sin aerolínea" : v.Aerolinea)
                .OrderBy(g => g.Key, StringComparer.CurrentCulture)
                .ToList();

            if (grupos.Count == 0)
            {
                tablaAerolineas.ResumeLayout();
                return;
            }

            float altoFila = 100F / grupos.Count;

            for (int i = 0; i < grupos.Count; i++)
            {
                IGrouping<string, Vuelo> grupo = grupos[i];
                double ocupacion = CalcularOcupacion(grupo);

                tablaAerolineas.RowCount = i + 1;
                tablaAerolineas.RowStyles.Add(new RowStyle(SizeType.Percent, altoFila));

                var lblNombre = new Label
                {
                    AutoEllipsis = true,
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = ColorTextoSecundario,
                    Margin = new Padding(0),
                    Text = grupo.Key,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                var barra = new ProgressBar
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 6, 8, 6),
                    Maximum = 100,
                    Minimum = 0,
                    Style = ProgressBarStyle.Continuous,
                    Value = ConvertirPorcentaje(ocupacion)
                };

                var lblPorcentaje = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = ColorTexto,
                    Margin = new Padding(0),
                    Text = ocupacion.ToString("0", Catalogos.Cultura) + "%",
                    TextAlign = ContentAlignment.MiddleRight
                };

                tablaAerolineas.Controls.Add(lblNombre, 0, i);
                tablaAerolineas.Controls.Add(barra, 1, i);
                tablaAerolineas.Controls.Add(lblPorcentaje, 2, i);
            }

            tablaAerolineas.ResumeLayout();
        }

        /// <summary>
        /// Calcula el porcentaje de ocupación evitando la división entre cero.
        /// </summary>
        private static double CalcularOcupacion(IEnumerable<Vuelo> vuelos)
        {
            int capacidad = 0;
            int ocupados = 0;

            foreach (Vuelo vuelo in vuelos)
            {
                capacidad += Math.Max(0, vuelo.Capacidad);
                ocupados += Math.Max(0, vuelo.AsientosReservados);
            }

            if (capacidad <= 0)
            {
                return 0d;
            }

            double porcentaje = ocupados * 100d / capacidad;
            return Math.Min(100d, Math.Round(porcentaje, 1, MidpointRounding.AwayFromZero));
        }

        private static int ConvertirPorcentaje(double porcentaje)
        {
            int valor = (int)Math.Round(porcentaje, MidpointRounding.AwayFromZero);
            return Math.Max(0, Math.Min(100, valor));
        }

        #endregion

        #region Próximos vuelos

        private void FiltrarVuelos()
        {
            if (datos == null)
            {
                dgvProximosVuelos.DataSource = null;
                return;
            }

            bool porFecha = chkFiltrarFecha.Checked;
            DateTime fechaSeleccionada = monthCalendario.SelectionStart.Date;

            IEnumerable<Vuelo> consulta = datos.Vuelos;

            if (porFecha)
            {
                consulta = consulta.Where(v => v.FechaSalida.Date == fechaSeleccionada);
            }
            else
            {
                consulta = consulta
                    .Where(v => v.FechaSalida >= DateTime.Now)
                    .Take(MaximoVuelosProximos);
            }

            List<FilaVuelo> filas = consulta
                .OrderBy(v => v.FechaSalida)
                .Select(v => new FilaVuelo
                {
                    Codigo = v.Codigo,
                    Aerolinea = v.Aerolinea,
                    Origen = v.Origen,
                    Destino = v.Destino,
                    FechaSalida = v.FechaSalida,
                    Estado = ObtenerTextoEstado(v.Estado),
                    EstadoEnum = v.Estado,
                    Disponibles = v.AsientosDisponibles,
                    Ocupacion = v.OcupacionPorcentual
                })
                .ToList();

            dgvProximosVuelos.DataSource = filas;
            dgvProximosVuelos.ClearSelection();

            if (filas.Count == 0)
            {
                lblFiltroActivo.Text = porFecha
                    ? "No hay vuelos programados para el " + fechaSeleccionada.ToString("dd/MM/yyyy", Catalogos.Cultura) + "."
                    : "No hay vuelos próximos registrados.";
            }
            else
            {
                lblFiltroActivo.Text = porFecha
                    ? "Vuelos del " + fechaSeleccionada.ToString("dddd, dd 'de' MMMM 'de' yyyy", Catalogos.Cultura)
                      + " · " + filas.Count + " resultado(s)."
                    : "Próximos " + filas.Count + " vuelo(s) ordenados por fecha de salida.";
            }

            AplicarEstilosFilas();
        }

        private void AplicarEstilosFilas()
        {
            if (dgvProximosVuelos.Rows.Count == 0)
            {
                return;
            }

            int umbral = trackBarUmbral.Value;

            foreach (DataGridViewRow fila in dgvProximosVuelos.Rows)
            {
                FilaVuelo datosFila = fila.DataBoundItem as FilaVuelo;

                if (datosFila == null)
                {
                    continue;
                }

                bool superaUmbral = datosFila.Ocupacion >= umbral;
                Color fondo = superaUmbral
                    ? ColorResaltado
                    : (fila.Index % 2 == 0 ? Color.White : ColorFilaAlterna);

                foreach (DataGridViewCell celda in fila.Cells)
                {
                    celda.Style.BackColor = fondo;
                }

                fila.Cells[colEstado.Index].Style.ForeColor = ObtenerColorEstado(datosFila.EstadoEnum);
                fila.Cells[colEstado.Index].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                fila.Cells[colDisponibles.Index].Style.ForeColor =
                    datosFila.Disponibles <= 10 ? ColorPeligro : ColorTexto;
            }
        }

        private static string ObtenerTextoEstado(EstadoVuelo estado)
        {
            switch (estado)
            {
                case EstadoVuelo.Programado:
                    return "Programado";
                case EstadoVuelo.Abordando:
                    return "Abordando";
                case EstadoVuelo.Retrasado:
                    return "Retrasado";
                case EstadoVuelo.EnVuelo:
                    return "En vuelo";
                case EstadoVuelo.Finalizado:
                    return "Finalizado";
                case EstadoVuelo.Cancelado:
                    return "Cancelado";
                default:
                    return estado.ToString();
            }
        }

        private static Color ObtenerColorEstado(EstadoVuelo estado)
        {
            switch (estado)
            {
                case EstadoVuelo.Programado:
                    return Color.FromArgb(30, 132, 73);
                case EstadoVuelo.Abordando:
                    return Color.FromArgb(41, 128, 185);
                case EstadoVuelo.Retrasado:
                    return Color.FromArgb(185, 119, 14);
                case EstadoVuelo.EnVuelo:
                    return Color.FromArgb(52, 73, 94);
                case EstadoVuelo.Finalizado:
                    return Color.FromArgb(127, 140, 141);
                case EstadoVuelo.Cancelado:
                    return ColorPeligro;
                default:
                    return ColorTexto;
            }
        }

        private sealed class FilaVuelo
        {
            public string Codigo { get; set; }

            public string Aerolinea { get; set; }

            public string Origen { get; set; }

            public string Destino { get; set; }

            public DateTime FechaSalida { get; set; }

            public string Estado { get; set; }

            public EstadoVuelo EstadoEnum { get; set; }

            public int Disponibles { get; set; }

            public double Ocupacion { get; set; }
        }

        #endregion

        #region Eventos

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void chkFiltrarFecha_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarVuelos();
        }

        private void monthCalendario_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (!chkFiltrarFecha.Checked)
            {
                chkFiltrarFecha.Checked = true;
                return;
            }

            FiltrarVuelos();
        }

        private void trackBarUmbral_ValueChanged(object sender, EventArgs e)
        {
            ActualizarEtiquetaUmbral();
            AplicarEstilosFilas();
        }

        private void dgvProximosVuelos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AplicarEstilosFilas();
        }

        private void ActualizarEtiquetaUmbral()
        {
            lblTituloUmbral.Text = "Umbral de ocupación: " + trackBarUmbral.Value + "%";
        }

        #endregion

        private void MostrarEstadoVacio()
        {
            lblValorVuelos.Text = "0";
            lblValorActivos.Text = "0";
            lblValorPasajeros.Text = "0";
            lblValorReservas.Text = "0";
            lblValorIngresos.Text = Catalogos.FormatearMoneda(0m);

            pbOcupacionGlobal.Value = 0;
            lblPorcentajeGlobal.Text = "0,0 %";

            tablaAerolineas.Controls.Clear();
            tablaAerolineas.RowStyles.Clear();
            tablaAerolineas.RowCount = 0;

            dgvProximosVuelos.DataSource = null;
            lblFiltroActivo.Text = "No hay datos cargados en la agenda.";
        }
    }
}
