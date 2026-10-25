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
    /// Módulo de gestión y administración de vuelos de la agencia.
    /// Permite registrar, consultar, editar, eliminar y filtrar vuelos,
    /// garantizando la integridad referencial con las reservaciones y
    /// la persistencia local de los datos.
    /// </summary>
    public partial class FrmGestionVuelos : Form
    {
        private readonly FrmPrincipal principal;
        private List<Vuelo> vuelosFiltrados = new List<Vuelo>();

        public FrmGestionVuelos(FrmPrincipal principal)
        {
            this.principal = principal ?? throw new ArgumentNullException(nameof(principal));

            InitializeComponent();
            ConfigurarFiltros();
            CargarVuelos();
        }

        private void ConfigurarFiltros()
        {
            // Filtro por Estado
            cboFiltroEstado.Items.Clear();
            cboFiltroEstado.Items.Add("(Todos los estados)");
            foreach (EstadoVuelo estado in Enum.GetValues(typeof(EstadoVuelo)))
            {
                cboFiltroEstado.Items.Add(estado);
            }
            cboFiltroEstado.SelectedIndex = 0;

            // Filtro por Aerolínea
            cboFiltroAerolinea.Items.Clear();
            cboFiltroAerolinea.Items.Add("(Todas las aerolíneas)");
            foreach (string aerolinea in Catalogos.Aerolineas)
            {
                cboFiltroAerolinea.Items.Add(aerolinea);
            }
            cboFiltroAerolinea.SelectedIndex = 0;
        }

        /// <summary>
        /// Recarga los vuelos desde la fuente de datos principal y aplica los filtros vigentes.
        /// </summary>
        public void CargarVuelos()
        {
            if (principal.Datos == null || principal.Datos.Vuelos == null)
            {
                vuelosFiltrados.Clear();
                ActualizarTabla();
                return;
            }

            FiltrarVuelos();
        }

        private void FiltrarVuelos()
        {
            if (principal.Datos?.Vuelos == null)
            {
                return;
            }

            IEnumerable<Vuelo> consulta = principal.Datos.Vuelos;

            // Filtro por texto (código, origen, destino, aerolínea o ciudades)
            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                consulta = consulta.Where(v =>
                    (v.Codigo != null && v.Codigo.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (v.Aerolinea != null && v.Aerolinea.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (v.Origen != null && v.Origen.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (v.Destino != null && v.Destino.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (Catalogos.ObtenerCiudad(v.Origen).IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (Catalogos.ObtenerCiudad(v.Destino).IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // Filtro por estado
            if (cboFiltroEstado.SelectedIndex > 0 && cboFiltroEstado.SelectedItem is EstadoVuelo estadoSeleccionado)
            {
                consulta = consulta.Where(v => v.Estado == estadoSeleccionado);
            }

            // Filtro por aerolínea
            if (cboFiltroAerolinea.SelectedIndex > 0 && cboFiltroAerolinea.SelectedItem is string aerolineaSeleccionada)
            {
                consulta = consulta.Where(v => string.Equals(v.Aerolinea, aerolineaSeleccionada, StringComparison.OrdinalIgnoreCase));
            }

            // Ordenamiento por fecha de salida
            vuelosFiltrados = consulta.OrderBy(v => v.FechaSalida).ToList();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dgvVuelos.SuspendLayout();
            dgvVuelos.Rows.Clear();

            int totalRegistrados = principal.Datos?.Vuelos?.Count ?? 0;

            foreach (Vuelo v in vuelosFiltrados)
            {
                string origenFormateado = $"{v.Origen} · {Catalogos.ObtenerCiudad(v.Origen)}";
                string destinoFormateado = $"{v.Destino} · {Catalogos.ObtenerCiudad(v.Destino)}";
                string ocupacionTexto = $"{v.AsientosReservados} / {v.Capacidad} ({v.OcupacionPorcentual:F1}%)";
                string activosTexto = v.Activo ? "Sí" : "No";

                int filaIdx = dgvVuelos.Rows.Add(
                    v.Codigo,
                    v.Aerolinea,
                    origenFormateado,
                    destinoFormateado,
                    v.FechaSalida.ToString("dd/MM/yyyy HH:mm"),
                    v.Escalas,
                    Catalogos.FormatearMoneda(v.Precio),
                    v.Capacidad,
                    v.AsientosDisponibles,
                    ocupacionTexto,
                    v.Estado.ToString(),
                    activosTexto
                );

                DataGridViewRow fila = dgvVuelos.Rows[filaIdx];
                fila.Tag = v;

                // Estilos según estado operativo
                if (v.Estado == EstadoVuelo.Cancelado)
                {
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                }
                else if (!v.Activo)
                {
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(140, 152, 166);
                }
            }

            dgvVuelos.ResumeLayout();

            lblContador.Text = $"Mostrando {vuelosFiltrados.Count} de {totalRegistrados} vuelos registrados.";
            ActualizarResumenSeleccion();
        }

        private Vuelo ObtenerVueloSeleccionado()
        {
            if (dgvVuelos.CurrentRow != null && dgvVuelos.CurrentRow.Tag is Vuelo v)
            {
                return v;
            }

            return null;
        }

        private void ActualizarResumenSeleccion()
        {
            Vuelo v = ObtenerVueloSeleccionado();
            if (v == null)
            {
                lblResumenSeleccion.Text = "Seleccione un vuelo de la tabla para ver sus detalles operativos.";
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                return;
            }

            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;

            lblResumenSeleccion.Text = $"Vuelo seleccionado: {v.Codigo} ({v.Aerolinea}) · {Catalogos.ObtenerCiudad(v.Origen)} → {Catalogos.ObtenerCiudad(v.Destino)} · "
                + $"Salida: {v.FechaSalida:dd/MM/yyyy HH:mm} · Ocupación: {v.AsientosReservados}/{v.Capacidad} ({v.AsientosDisponibles} disp.) · Tarifa: {Catalogos.FormatearMoneda(v.Precio)} USD";
        }

        public void NuevoVuelo()
        {
            using (var dlg = new FrmEditarVuelo(principal.Datos))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.VueloResultado != null)
                {
                    principal.Datos.Vuelos.Add(dlg.VueloResultado);

                    if (principal.GuardarCambios())
                    {
                        CargarVuelos();
                        SeleccionarFilaPorCodigo(dlg.VueloResultado.Codigo);

                        MessageBox.Show(
                            this,
                            $"El vuelo {dlg.VueloResultado.Codigo} ha sido registrado exitosamente.",
                            "Vuelo registrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void EditarVueloSeleccionado()
        {
            Vuelo seleccionado = ObtenerVueloSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione el vuelo que desea editar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new FrmEditarVuelo(principal.Datos, seleccionado))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    principal.Datos.RecalcularOcupacion();

                    if (principal.GuardarCambios())
                    {
                        CargarVuelos();
                        SeleccionarFilaPorCodigo(seleccionado.Codigo);

                        MessageBox.Show(
                            this,
                            $"Los datos del vuelo {seleccionado.Codigo} fueron actualizados correctamente.",
                            "Vuelo actualizado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void EliminarVueloSeleccionado()
        {
            Vuelo seleccionado = ObtenerVueloSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione el vuelo que desea eliminar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // REVISIÓN DE INTEGRIDAD REFERENCIAL CON RESERVACIONES
            var reservacionesAsociadas = principal.Datos.Reservaciones
                .Where(r => string.Equals(r.CodigoVuelo, seleccionado.Codigo, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (reservacionesAsociadas.Count > 0)
            {
                string codigosEjemplo = string.Join(", ", reservacionesAsociadas.Take(3).Select(r => r.Codigo));
                if (reservacionesAsociadas.Count > 3)
                {
                    codigosEjemplo += "...";
                }

                MessageBox.Show(
                    this,
                    $"No es posible eliminar el vuelo '{seleccionado.Codigo}' porque tiene {reservacionesAsociadas.Count} reservación(es) asociada(s) ({codigosEjemplo})."
                    + Environment.NewLine + Environment.NewLine
                    + "Para proteger la integridad referencial del sistema, no se permite eliminar vuelos con reservaciones vigentes o históricas."
                    + Environment.NewLine + Environment.NewLine
                    + "Si el vuelo no operará, edítelo y cambie su estado a 'Cancelado' o desmarque la opción 'Vuelo activo'.",
                    "Eliminación protegida por integridad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Confirmación explícita antes de eliminar
            DialogResult confirmacion = MessageBox.Show(
                this,
                $"¿Está seguro de que desea eliminar el vuelo {seleccionado.Codigo} ({seleccionado.Ruta})?"
                + Environment.NewLine + Environment.NewLine
                + "Esta acción eliminará el registro de forma permanente.",
                "Confirmar eliminación de vuelo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmacion == DialogResult.Yes)
            {
                principal.Datos.Vuelos.Remove(seleccionado);

                if (principal.GuardarCambios())
                {
                    CargarVuelos();

                    MessageBox.Show(
                        this,
                        $"El vuelo {seleccionado.Codigo} fue eliminado correctamente.",
                        "Vuelo eliminado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void SeleccionarFilaPorCodigo(string codigo)
        {
            foreach (DataGridViewRow row in dgvVuelos.Rows)
            {
                if (row.Tag is Vuelo v && string.Equals(v.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dgvVuelos.CurrentCell = row.Cells[0];
                    break;
                }
            }
            ActualizarResumenSeleccion();
        }

        #region Eventos de controles

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoVuelo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarVueloSeleccionado();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarVueloSeleccionado();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVuelos();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarVuelos();
        }

        private void cboFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarVuelos();
        }

        private void cboFiltroAerolinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarVuelos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            cboFiltroEstado.SelectedIndex = 0;
            cboFiltroAerolinea.SelectedIndex = 0;
            FiltrarVuelos();
        }

        private void dgvVuelos_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarResumenSeleccion();
        }

        private void dgvVuelos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarVueloSeleccionado();
            }
        }

        private void dgvVuelos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditarVueloSeleccionado();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                EliminarVueloSeleccionado();
                e.Handled = true;
            }
        }

        #endregion
    }
}

