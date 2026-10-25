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
    /// Módulo de gestión y administración de reservaciones de la agencia.
    /// Permite consultar, registrar, editar y filtrar reservaciones de asientos,
    /// tarifas y servicios sobre los vuelos disponibles, manteniendo consistencia visual.
    /// </summary>
    public partial class FrmGestionReservaciones : Form
    {
        private readonly FrmPrincipal principal;
        private List<Reservacion> reservacionesFiltradas = new List<Reservacion>();

        public FrmGestionReservaciones(FrmPrincipal principal)
        {
            this.principal = principal ?? throw new ArgumentNullException(nameof(principal));

            InitializeComponent();
            ConfigurarFiltros();
            CargarReservaciones();
        }

        private void ConfigurarFiltros()
        {
            // Filtro por Estado
            cboFiltroEstado.Items.Clear();
            cboFiltroEstado.Items.Add("(Todos)");
            foreach (EstadoReservacion estado in Enum.GetValues(typeof(EstadoReservacion)))
            {
                cboFiltroEstado.Items.Add(estado);
            }
            cboFiltroEstado.SelectedIndex = 0;

            // Filtro por Tarifa
            cboFiltroTarifa.Items.Clear();
            cboFiltroTarifa.Items.Add("(Todas)");
            foreach (TipoTarifa tarifa in Enum.GetValues(typeof(TipoTarifa)))
            {
                cboFiltroTarifa.Items.Add(tarifa);
            }
            cboFiltroTarifa.SelectedIndex = 0;
        }

        /// <summary>
        /// Recarga las reservaciones desde la fuente de datos principal y aplica los filtros vigentes.
        /// </summary>
        public void CargarReservaciones()
        {
            if (principal.Datos == null || principal.Datos.Reservaciones == null)
            {
                reservacionesFiltradas.Clear();
                ActualizarTabla();
                return;
            }

            FiltrarReservaciones();
        }

        private void FiltrarReservaciones()
        {
            if (principal.Datos?.Reservaciones == null)
            {
                return;
            }

            IEnumerable<Reservacion> consulta = principal.Datos.Reservaciones;

            // Filtro por texto
            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                consulta = consulta.Where(r =>
                {
                    bool matchCodigo = r.Codigo != null && r.Codigo.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0;
                    bool matchVuelo = r.CodigoVuelo != null && r.CodigoVuelo.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0;
                    bool matchDoc = r.DocumentoPasajero != null && r.DocumentoPasajero.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0;

                    Pasajero p = principal.Datos.BuscarPasajero(r.DocumentoPasajero);
                    bool matchNombre = p != null && p.NombreCompleto.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0;

                    return matchCodigo || matchVuelo || matchDoc || matchNombre;
                });
            }

            // Filtro por estado
            if (cboFiltroEstado.SelectedIndex > 0 && cboFiltroEstado.SelectedItem is EstadoReservacion estadoSel)
            {
                consulta = consulta.Where(r => r.Estado == estadoSel);
            }

            // Filtro por tarifa
            if (cboFiltroTarifa.SelectedIndex > 0 && cboFiltroTarifa.SelectedItem is TipoTarifa tarifaSel)
            {
                consulta = consulta.Where(r => r.Tarifa == tarifaSel);
            }

            // Ordenamiento por fecha de reserva descendente
            reservacionesFiltradas = consulta.OrderByDescending(r => r.FechaReserva).ToList();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dgvReservaciones.SuspendLayout();
            dgvReservaciones.Rows.Clear();

            int totalRegistradas = principal.Datos?.Reservaciones?.Count ?? 0;

            foreach (Reservacion r in reservacionesFiltradas)
            {
                Vuelo v = principal.Datos?.BuscarVuelo(r.CodigoVuelo);
                Pasajero p = principal.Datos?.BuscarPasajero(r.DocumentoPasajero);

                string vueloTexto = v != null
                    ? $"{r.CodigoVuelo} ({v.Origen} → {v.Destino})"
                    : r.CodigoVuelo;

                string pasajeroTexto = p != null
                    ? $"{p.NombreCompleto} ({r.DocumentoPasajero})"
                    : r.DocumentoPasajero;

                string seguroTexto = r.Seguro ? "Sí" : "No";
                string serviciosTexto = r.Servicios != null && r.Servicios.Count > 0
                    ? string.Join(", ", r.Servicios)
                    : "(Ninguno)";

                int filaIdx = dgvReservaciones.Rows.Add(
                    r.Codigo,
                    vueloTexto,
                    pasajeroTexto,
                    r.FechaReserva > DateTime.MinValue ? r.FechaReserva.ToString("dd/MM/yyyy HH:mm") : "—",
                    r.CantidadAsientos,
                    r.Tarifa.ToString(),
                    seguroTexto,
                    serviciosTexto,
                    Catalogos.FormatearMoneda(r.Total),
                    r.Estado.ToString()
                );

                DataGridViewRow fila = dgvReservaciones.Rows[filaIdx];
                fila.Tag = r;

                if (r.Estado == EstadoReservacion.Cancelada)
                {
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                }
                else if (r.Estado == EstadoReservacion.Pendiente)
                {
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(180, 110, 20);
                }
            }

            dgvReservaciones.ResumeLayout();

            lblContador.Text = $"Mostrando {reservacionesFiltradas.Count} de {totalRegistradas} reservaciones registradas";

            ActualizarResumenSeleccion();
        }

        private Reservacion ObtenerReservacionSeleccionada()
        {
            if (dgvReservaciones.CurrentRow != null && dgvReservaciones.CurrentRow.Tag is Reservacion r)
            {
                return r;
            }

            return null;
        }

        private void ActualizarResumenSeleccion()
        {
            Reservacion r = ObtenerReservacionSeleccionada();
            if (r != null)
            {
                Pasajero p = principal.Datos?.BuscarPasajero(r.DocumentoPasajero);
                Vuelo v = principal.Datos?.BuscarVuelo(r.CodigoVuelo);

                string nombre = p != null ? p.NombreCompleto : r.DocumentoPasajero;
                string ruta = v != null ? $"{v.Origen} → {v.Destino}" : r.CodigoVuelo;

                lblResumenSeleccion.Text = $"Reservación {r.Codigo} · Vuelo: {r.CodigoVuelo} ({ruta}) · Pasajero: {nombre} · Asientos: {r.CantidadAsientos} · Tarifa: {r.Tarifa} · Total: {Catalogos.FormatearMoneda(r.Total)} · {r.Estado}";
                lblResumenSeleccion.ForeColor = Color.FromArgb(22, 34, 47);
            }
            else
            {
                lblResumenSeleccion.Text = "Seleccione una reservación de la lista para consultar su detalle o editar sus parámetros.";
                lblResumenSeleccion.ForeColor = Color.FromArgb(107, 122, 140);
            }
        }

        #region Eventos de Controles

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarReservaciones();
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarReservaciones();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cboFiltroEstado.SelectedIndex = 0;
            cboFiltroTarifa.SelectedIndex = 0;
            FiltrarReservaciones();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarReservaciones();
        }

        public void NuevaReservacion()
        {
            if (principal.Datos?.Vuelos == null || principal.Datos.Vuelos.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "No hay vuelos registrados para emitir reservaciones. Registre primero al menos un vuelo programado.",
                    "Gestión de Reservaciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (principal.Datos?.Pasajeros == null || principal.Datos.Pasajeros.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "No hay pasajeros registrados para emitir reservaciones. Registre primero al menos un pasajero en el sistema.",
                    "Gestión de Reservaciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new FrmEditarReservacion(principal.Datos))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.ReservacionResultado != null)
                {
                    principal.Datos.Reservaciones.Add(dlg.ReservacionResultado);
                    principal.Datos.RecalcularOcupacion();
                    principal.GuardarCambios();

                    CargarReservaciones();
                    SeleccionarFilaPorCodigo(dlg.ReservacionResultado.Codigo);

                    MessageBox.Show(
                        this,
                        $"Reservación '{dlg.ReservacionResultado.Codigo}' registrada exitosamente.",
                        "Gestión de Reservaciones",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevaReservacion();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Reservacion r = ObtenerReservacionSeleccionada();
            if (r == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione una reservación de la lista para editar.",
                    "Gestión de Reservaciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new FrmEditarReservacion(principal.Datos, r))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.ReservacionResultado != null)
                {
                    principal.Datos.RecalcularOcupacion();
                    principal.GuardarCambios();

                    CargarReservaciones();
                    SeleccionarFilaPorCodigo(dlg.ReservacionResultado.Codigo);

                    MessageBox.Show(
                        this,
                        $"Reservación '{dlg.ReservacionResultado.Codigo}' actualizada correctamente.",
                        "Gestión de Reservaciones",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Reservacion r = ObtenerReservacionSeleccionada();
            if (r == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione una reservación de la lista que desea cancelar.",
                    "Gestión de Reservaciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (r.Estado == EstadoReservacion.Cancelada)
            {
                MessageBox.Show(
                    this,
                    $"La reservación '{r.Codigo}' ya se encuentra cancelada y sus asientos están liberados.",
                    "Reservación cancelada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            Pasajero p = principal.Datos?.BuscarPasajero(r.DocumentoPasajero);
            string nombrePasajero = p != null ? p.NombreCompleto : r.DocumentoPasajero;
            Vuelo v = principal.Datos?.BuscarVuelo(r.CodigoVuelo);
            string rutaVuelo = v != null ? $"{v.Origen} → {v.Destino}" : r.CodigoVuelo;

            DialogResult confirmacion = MessageBox.Show(
                this,
                $"¿Está seguro de que desea cancelar la reservación '{r.Codigo}'?\r\n\r\n" +
                $"• Pasajero titular: {nombrePasajero}\r\n" +
                $"• Vuelo: {r.CodigoVuelo} ({rutaVuelo})\r\n" +
                $"• Asientos a liberar: {r.CantidadAsientos}\r\n" +
                $"• Importe registrado: {Catalogos.FormatearMoneda(r.Total)}\r\n\r\n" +
                "El registro no será eliminado del sistema; se conservará en el historial con estado 'Cancelada'.",
                "Confirmar cancelación de reservación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                r.Estado = EstadoReservacion.Cancelada;
                principal.Datos.RecalcularOcupacion();
                principal.GuardarCambios();

                CargarReservaciones();
                SeleccionarFilaPorCodigo(r.Codigo);

                MessageBox.Show(
                    this,
                    $"La reservación '{r.Codigo}' ha sido cancelada y sus {r.CantidadAsientos} asiento(s) fueron liberados.",
                    "Cancelación confirmada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void SeleccionarFilaPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return;
            }

            foreach (DataGridViewRow fila in dgvReservaciones.Rows)
            {
                if (fila.Tag is Reservacion r && string.Equals(r.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
                {
                    fila.Selected = true;
                    dgvReservaciones.CurrentCell = fila.Cells[0];
                    break;
                }
            }
            ActualizarResumenSeleccion();
        }

        private void dgvReservaciones_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarResumenSeleccion();
        }

        private void dgvReservaciones_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        #endregion
    }
}

