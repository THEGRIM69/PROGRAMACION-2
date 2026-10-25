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
    /// Módulo de gestión y administración de pasajeros de la agencia (Fase 6).
    /// Permite consultar, registrar, editar, eliminar y filtrar pasajeros de manera interactiva,
    /// garantizando la integridad referencial con las reservaciones y la persistencia local en JSON.
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

            // Filtro por texto (documento, nombres, apellidos, correo o nacionalidad)
            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                consulta = consulta.Where(p =>
                    (p.Documento != null && p.Documento.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Nombres != null && p.Nombres.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Apellidos != null && p.Apellidos.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.NombreCompleto != null && p.NombreCompleto.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
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

            // Ordenamiento alfabético por apellidos y nombres
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

        private void SeleccionarFilaPorDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
            {
                return;
            }

            foreach (DataGridViewRow fila in dgvPasajeros.Rows)
            {
                if (fila.Tag is Pasajero p && string.Equals(p.Documento, documento.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    fila.Selected = true;
                    dgvPasajeros.CurrentCell = fila.Cells[0];
                    break;
                }
            }
        }

        private void ActualizarResumenSeleccion()
        {
            Pasajero p = ObtenerPasajeroSeleccionado();
            if (p != null)
            {
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;

                string asistenciaInfo = p.RequiereAsistencia ? "Requiere asistencia prioritaria" : "Sin requerimiento especial";
                lblResumenSeleccion.Text = $"Pasajero seleccionado: {p.NombreCompleto} · Doc: {p.Documento} · Cat: {p.Categoria} · Edad: {p.Edad} años · {p.Nacionalidad} · {asistenciaInfo}";
                lblResumenSeleccion.ForeColor = Color.FromArgb(22, 34, 47);
            }
            else
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;

                lblResumenSeleccion.Text = "Seleccione un pasajero de la lista para ver sus detalles o editar su información.";
                lblResumenSeleccion.ForeColor = Color.FromArgb(107, 122, 140);
            }
        }

        #region Operaciones CRUD

        /// <summary>
        /// Abre el diálogo para registrar un nuevo pasajero y persiste el cambio atómicamente.
        /// </summary>
        public void NuevoPasajero()
        {
            using (var dlg = new FrmEditarPasajero(principal.Datos))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.PasajeroResultado != null)
                {
                    principal.Datos.Pasajeros.Add(dlg.PasajeroResultado);

                    if (principal.GuardarCambios())
                    {
                        CargarPasajeros();
                        SeleccionarFilaPorDocumento(dlg.PasajeroResultado.Documento);

                        MessageBox.Show(
                            this,
                            $"El pasajero '{dlg.PasajeroResultado.NombreCompleto}' ({dlg.PasajeroResultado.Documento}) ha sido registrado exitosamente.",
                            "Pasajero registrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        // En caso de fallo en persistencia, se remueve de memoria para mantener sincronía
                        principal.Datos.Pasajeros.Remove(dlg.PasajeroResultado);
                        CargarPasajeros();
                    }
                }
            }
        }

        /// <summary>
        /// Abre el diálogo para editar el pasajero seleccionado y guarda los cambios.
        /// </summary>
        public void EditarPasajeroSeleccionado()
        {
            Pasajero seleccionado = ObtenerPasajeroSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione un pasajero de la lista para editar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new FrmEditarPasajero(principal.Datos, seleccionado))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (principal.GuardarCambios())
                    {
                        CargarPasajeros();
                        SeleccionarFilaPorDocumento(seleccionado.Documento);

                        MessageBox.Show(
                            this,
                            $"Los datos del pasajero '{seleccionado.NombreCompleto}' fueron actualizados correctamente.",
                            "Pasajero actualizado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Elimina al pasajero seleccionado siempre que no tenga reservaciones asociadas.
        /// </summary>
        public void EliminarPasajeroSeleccionado()
        {
            Pasajero seleccionado = ObtenerPasajeroSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show(
                    this,
                    "Seleccione un pasajero de la lista que desea eliminar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // 1. REVISIÓN DE INTEGRIDAD REFERENCIAL CON RESERVACIONES
            var reservacionesAsociadas = principal.Datos.Reservaciones
                .Where(r => string.Equals(r.DocumentoPasajero, seleccionado.Documento, StringComparison.OrdinalIgnoreCase))
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
                    $"No es posible eliminar al pasajero '{seleccionado.NombreCompleto}' ({seleccionado.Documento}) porque tiene {reservacionesAsociadas.Count} reservación(es) asociada(s) ({codigosEjemplo})."
                    + Environment.NewLine + Environment.NewLine
                    + "Para proteger la integridad referencial del sistema, no se permite eliminar pasajeros con reservaciones asociadas vigentes o históricas.",
                    "Eliminación protegida por integridad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // 2. CONFIRMACIÓN EXPLÍCITA ANTES DE ELIMINAR
            DialogResult confirmacion = MessageBox.Show(
                this,
                $"¿Está seguro de que desea eliminar al pasajero '{seleccionado.NombreCompleto}' ({seleccionado.Documento})?"
                + Environment.NewLine + Environment.NewLine
                + "Esta acción eliminará el registro de forma permanente de la agenda.",
                "Confirmar eliminación de pasajero",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmacion == DialogResult.Yes)
            {
                principal.Datos.Pasajeros.Remove(seleccionado);

                if (principal.GuardarCambios())
                {
                    CargarPasajeros();

                    MessageBox.Show(
                        this,
                        $"El pasajero '{seleccionado.NombreCompleto}' ha sido eliminado exitosamente.",
                        "Pasajero eliminado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Si falla la persistencia, se restaura en memoria
                    principal.Datos.Pasajeros.Add(seleccionado);
                    CargarPasajeros();
                }
            }
        }

        #endregion

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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoPasajero();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarPasajeroSeleccionado();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarPasajeroSeleccionado();
        }

        private void dgvPasajeros_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarResumenSeleccion();
        }

        private void dgvPasajeros_DoubleClick(object sender, EventArgs e)
        {
            EditarPasajeroSeleccionado();
        }

        #endregion
    }
}
