using System;
using System.Drawing;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Diálogo para el registro y edición de pasajeros.
    /// Presenta los campos correspondientes al modelo Pasajero: documento, nombres,
    /// apellidos, email, teléfono, fecha de nacimiento, nacionalidad, categoría,
    /// asistencia especial y servicios preferidos.
    /// </summary>
    public partial class FrmEditarPasajero : Form
    {
        private readonly AgendaData datos;
        private readonly Pasajero pasajeroOriginal;
        private readonly bool esEdicion;

        public FrmEditarPasajero(AgendaData datos, Pasajero pasajeroAEditar = null)
        {
            this.datos = datos ?? throw new ArgumentNullException(nameof(datos));
            this.pasajeroOriginal = pasajeroAEditar;
            this.esEdicion = pasajeroAEditar != null;

            InitializeComponent();
            ConfigurarCatalogos();

            if (esEdicion)
            {
                ConfigurarModoEdicion();
            }
            else
            {
                ConfigurarModoNuevo();
            }
        }

        private void ConfigurarCatalogos()
        {
            // Nacionalidades
            cboNacionalidad.Items.Clear();
            foreach (string nac in Catalogos.Nacionalidades)
            {
                cboNacionalidad.Items.Add(nac);
            }
            if (cboNacionalidad.Items.Count > 0)
            {
                cboNacionalidad.SelectedIndex = 0;
            }

            // Categorías de pasajero
            cboCategoria.Items.Clear();
            foreach (string cat in Catalogos.CategoriasPasajero)
            {
                cboCategoria.Items.Add(cat);
            }
            if (cboCategoria.Items.Count > 0)
            {
                cboCategoria.SelectedIndex = 0;
            }

            // Servicios adicionales preferidos
            clbServicios.Items.Clear();
            foreach (string serv in Catalogos.ServiciosAdicionales)
            {
                clbServicios.Items.Add(serv);
            }
        }

        private void ConfigurarModoNuevo()
        {
            Text = "Registrar nuevo pasajero";
            lblTitulo.Text = "REGISTRO DE PASAJERO";
            lblSubtitulo.Text = "Ingrese los datos personales, categoría y preferencias de viaje del pasajero.";
            txtDocumento.ReadOnly = false;
            txtDocumento.BackColor = Color.White;
            btnGuardar.Text = "Registrar pasajero";
            dtpFechaNacimiento.Value = DateTime.Today.AddYears(-25);
            chkAsistencia.Checked = false;
        }

        private void ConfigurarModoEdicion()
        {
            Text = "Editar pasajero — " + pasajeroOriginal.Documento;
            lblTitulo.Text = "EDICIÓN DE PASAJERO";
            lblSubtitulo.Text = "Modifique la información y preferencias del pasajero seleccionado.";
            txtDocumento.Text = pasajeroOriginal.Documento;
            txtDocumento.ReadOnly = true;
            txtDocumento.BackColor = Color.FromArgb(244, 246, 248);
            txtNombres.Text = pasajeroOriginal.Nombres;
            txtApellidos.Text = pasajeroOriginal.Apellidos;
            txtEmail.Text = pasajeroOriginal.Email;
            txtTelefono.Text = pasajeroOriginal.Telefono;

            if (pasajeroOriginal.FechaNacimiento > DateTime.MinValue && pasajeroOriginal.FechaNacimiento <= DateTime.Today)
            {
                dtpFechaNacimiento.Value = pasajeroOriginal.FechaNacimiento;
            }

            if (!string.IsNullOrEmpty(pasajeroOriginal.Nacionalidad))
            {
                int idxNac = cboNacionalidad.FindStringExact(pasajeroOriginal.Nacionalidad);
                if (idxNac >= 0)
                {
                    cboNacionalidad.SelectedIndex = idxNac;
                }
            }

            if (!string.IsNullOrEmpty(pasajeroOriginal.Categoria))
            {
                int idxCat = cboCategoria.FindStringExact(pasajeroOriginal.Categoria);
                if (idxCat >= 0)
                {
                    cboCategoria.SelectedIndex = idxCat;
                }
            }

            chkAsistencia.Checked = pasajeroOriginal.RequiereAsistencia;

            if (pasajeroOriginal.ServiciosPreferidos != null)
            {
                for (int i = 0; i < clbServicios.Items.Count; i++)
                {
                    string nombreServicio = clbServicios.Items[i].ToString();
                    if (pasajeroOriginal.ServiciosPreferidos.Contains(nombreServicio))
                    {
                        clbServicios.SetItemChecked(i, true);
                    }
                }
            }

            btnGuardar.Text = "Guardar cambios";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this,
                "Esta funcionalidad estará disponible en la siguiente etapa de integración",
                "Gestión de Pasajeros",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

