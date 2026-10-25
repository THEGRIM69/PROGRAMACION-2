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
    /// Módulo de gestión y administración de pasajeros de la agencia.
    /// Permite consultar, registrar, editar y filtrar pasajeros de manera interactiva,
    /// conservando el diseño moderno y la coherencia visual del sistema.
    /// </summary>
    public partial class FrmGestionPasajeros : Form
    {
        private readonly FrmPrincipal principal;
        private List<Pasajero> pasajerosFiltrados = new List<Pasajero>();

        public FrmGestionPasajeros(FrmPrincipal principal)
        {
            this.principal = principal ?? throw new ArgumentNullException(nameof(principal));

            InitializeComponent();
            ConfigurarFiltros();
            CargarPasajeros();
        }

        private void ConfigurarFiltros()
        {
            // Filtro por Categoría
            cboFiltroCategoria.Items.Clear();
            cboFiltroCategoria.Items.Add("(Todas)");
            foreach (string cat in Catalogos.CategoriasPasajero)
            {
                cboFiltroCategoria.Items.Add(cat);
            }
            cboFiltroCategoria.SelectedIndex = 0;

            // Filtro por Nacionalidad
            cboFiltroNacionalidad.Items.Clear();
            cboFiltroNacionalidad.Items.Add("(Todas)");
            foreach (string nac in Catalogos.Nacionalidades)
            {
                cboFiltroNacionalidad.Items.Add(nac);
            }
            cboFiltroNacionalidad.SelectedIndex = 0;
        }

        /// <summary>
        /// Recarga los pasajeros desde la fuente de datos principal y aplica los filtros vigentes.
        /// </summary>
        public void CargarPasajeros()
        {
            if (principal.Datos == null || principal.Datos.Pasajeros == null)
            {
                pasajerosFiltrados.Clear();
                ActualizarTabla();
                return;
            }

            FiltrarPasajeros();
        }

        private void FiltrarPasajeros()
        {
            if (principal.Datos?.Pasajeros == null)
            {
                return;
            }

            IEnumerable<Pasajero> consulta = principal.Datos.Pasajeros;

            // Filtro por texto
            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                consulta = consulta.Where(p =>
                    (p.Documento != null && p.Documento.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Nombres != null && p.Nombres.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Apellidos != null && p.Apellidos.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Email != null && p.Email.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Telefono != null && p.Telefono.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Nacionalidad != null && p.Nacionalidad.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // Filtro por categoría
            if (cboFiltroCategoria.SelectedIndex > 0 && cboFiltroCategoria.SelectedItem is string catSeleccionada)
            {
                consulta = consulta.Where(p => string.Equals(p.Categoria, catSeleccionada, StringComparison.OrdinalIgnoreCase));
            }

            // Filtro por nacionalidad
            if (cboFiltroNacionalidad.SelectedIndex > 0 && cboFiltroNacionalidad.SelectedItem is string nacSeleccionada)
            {
                consulta = consulta.Where(p => string.Equals(p.Nacionalidad, nacSeleccionada, StringComparison.OrdinalIgnoreCase));
            }

            // Ordenamiento por apellidos y nombres
            pasajerosFiltrados = consulta.OrderBy(p => p.Apellidos).ThenBy(p => p.Nombres).ToList();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dgvPasajeros.SuspendLayout();
            dgvPasajeros.Rows.Clear();

            int totalRegistrados = principal.Datos?.Pasajeros?.Count ?? 0;

            foreach (Pasajero p in pasajerosFiltrados)
            {
                string serviciosTexto = p.ServiciosPreferidos != null && p.ServiciosPreferidos.Count > 0
                    ? string.Join(", ", p.ServiciosPreferidos)
                    : "(Ninguno)";
                string asistenciaTexto = p.RequiereAsistencia ? "Sí (Prioritaria)" : "No";

                int filaIdx = dgvPasajeros.Rows.Add(
                    p.Documento,
                    p.NombreCompleto,
                    p.Email,
                    p.Telefono,
                    p.FechaNacimiento > DateTime.MinValue ? p.FechaNacimiento.ToString("dd/MM/yyyy") : "—",
                    p.Edad + " años",
                    p.Nacionalidad,
                    p.Categoria,
                    asistenciaTexto,
                    serviciosTexto
                );

                DataGridViewRow fila = dgvPasajeros.Rows[filaIdx];
                fila.Tag = p;

                if (p.RequiereAsistencia)
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 243);
                }
            }

            dgvPasajeros.ResumeLayout();

            lblContador.Text = $"Mostrando {pasajerosFiltrados.Count} de {totalRegistrados} pasajeros registrados";

            ActualizarResumenSeleccion();
        }

        private Pasajero ObtenerPasajeroSeleccionado()
        {
            if (dgvPasajeros.CurrentRow != null && dgvPasajeros.CurrentRow.Tag is Pasajero p)
            {
                return p;
            }

            return null;
        }

        private void ActualizarResumenSeleccion()
        {
            Pasajero p = ObtenerPasajeroSeleccionado();
            if (p != null)
            {
                string asistenciaInfo = p.RequiereAsistencia ? "Requiere asistencia en embarque" : "Sin requerimiento especial";
                lblResumenSeleccion.Text = $"Pasajero: {p.NombreCompleto} · Doc: {p.Documento} · Cat: {p.Categoria} · Edad: {p.Edad} años · {p.Nacionalidad} · {asistenciaInfo}";
                lblResumenSeleccion.ForeColor = Color.FromArgb(22, 34, 47);
            }
            else
            {
                lblResumenSeleccion.Text = "Seleccione un pasajero de la lista para ver sus detalles o editar su información.";
                lblResumenSeleccion.ForeColor = Color.FromArgb(107, 122, 140);
            }
        }

        #region Eventos de Controles

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarPasajeros();
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarPasajeros();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cboFiltroCategoria.SelectedIndex = 0;
            cboFiltroNacionalidad.SelectedIndex = 0;
            FiltrarPasajeros();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarPasajeros();
        }

        public void NuevoPasajero()
        {
            using (var dlg = new FrmEditarPasajero(principal.Datos))
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoPasajero();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Pasajero p = ObtenerPasajeroSeleccionado();
            if (p == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione un pasajero de la lista para editar.",
                    "Gestión de Pasajeros",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new FrmEditarPasajero(principal.Datos, p))
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Pasajero p = ObtenerPasajeroSeleccionado();
            if (p == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione un pasajero de la lista que desea eliminar.",
                    "Gestión de Pasajeros",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(
                this,
                "Esta funcionalidad estará disponible en la siguiente etapa de integración",
                "Gestión de Pasajeros",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void dgvPasajeros_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarResumenSeleccion();
        }

        private void dgvPasajeros_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        #endregion
    }
}

