using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;
using AgendaDeViajes.Services;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Diálogo modal para el registro y edición de pasajeros.
    /// Valida campos obligatorios, formato de correo, documento de identidad,
    /// prevención de duplicados y coherencia de fechas según las reglas de negocio.
    /// </summary>
    public partial class FrmEditarPasajero : Form
    {
        private static readonly Regex PatronTelefono = new Regex(@"^[0-9+\-()\s]{7,20}$", RegexOptions.Compiled);

        private readonly AgendaData datos;
        private readonly Pasajero pasajeroOriginal;
        private readonly bool esEdicion;

        /// <summary>
        /// Pasajero resultante de la operación de registro o edición.
        /// </summary>
        public Pasajero PasajeroResultado { get; private set; }

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
            lblAvisoIntegracion.Text = "Fase 6 · Registro de pasajeros";
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
            lblAvisoIntegracion.Text = "Fase 6 · Edición de pasajeros";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string documento = txtDocumento.Text.Trim();
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string email = txtEmail.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            DateTime fechaNacimiento = dtpFechaNacimiento.Value.Date;
            string nacionalidad = cboNacionalidad.SelectedItem?.ToString() ?? string.Empty;
            string categoria = cboCategoria.SelectedItem?.ToString() ?? "Adulto";
            bool requiereAsistencia = chkAsistencia.Checked;

            var serviciosPreferidos = new List<string>();
            foreach (var item in clbServicios.CheckedItems)
            {
                serviciosPreferidos.Add(item.ToString());
            }

            // ==========================================
            // VALIDACIÓN RIGUROSA DE DATOS
            // ==========================================
            var errores = new List<string>();

            // 1. Documento de identidad
            if (string.IsNullOrWhiteSpace(documento))
            {
                errores.Add("El documento de identidad es obligatorio.");
            }
            else if (documento.Length < 5 || documento.Length > 20)
            {
                errores.Add("El documento de identidad debe tener entre 5 y 20 caracteres.");
            }
            else if (!esEdicion)
            {
                // Validación contra duplicados en modo nuevo
                if (datos.BuscarPasajero(documento) != null)
                {
                    errores.Add($"Ya existe un pasajero registrado con el documento '{documento}'.");
                }
            }

            // 2. Nombres
            if (string.IsNullOrWhiteSpace(nombres))
            {
                errores.Add("Los nombres del pasajero son obligatorios.");
            }

            // 3. Apellidos
            if (string.IsNullOrWhiteSpace(apellidos))
            {
                errores.Add("Los apellidos del pasajero son obligatorios.");
            }

            // 4. Correo electrónico
            if (string.IsNullOrWhiteSpace(email))
            {
                errores.Add("El correo electrónico es obligatorio.");
            }
            else if (!MailAddress.TryCreate(email, out _))
            {
                errores.Add("El correo electrónico no tiene un formato válido (ejemplo: usuario@dominio.com).");
            }

            // 5. Teléfono
            if (string.IsNullOrWhiteSpace(telefono))
            {
                errores.Add("El número de teléfono es obligatorio.");
            }
            else if (!PatronTelefono.IsMatch(telefono))
            {
                errores.Add("El teléfono solo admite dígitos, espacios, paréntesis, guiones y el signo + (entre 7 y 20 caracteres).");
            }

            // 6. Fecha de nacimiento y edad
            if (fechaNacimiento >= DateTime.Today)
            {
                errores.Add("La fecha de nacimiento debe ser anterior a la fecha actual.");
            }
            else
            {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - fechaNacimiento.Year;
                if (fechaNacimiento > hoy.AddYears(-edad))
                {
                    edad--;
                }

                if (edad > 120)
                {
                    errores.Add("La fecha de nacimiento no es coherente con la edad del pasajero (máximo 120 años).");
                }
            }

            // 7. Nacionalidad
            if (string.IsNullOrWhiteSpace(nacionalidad))
            {
                errores.Add("La nacionalidad es obligatoria.");
            }

            // 8. Categoría
            if (!Catalogos.CategoriasPasajero.Contains(categoria))
            {
                errores.Add("La categoría seleccionada no es válida.");
            }

            // Si hay errores, mostrar aviso y detener
            if (errores.Count > 0)
            {
                MessageBox.Show(
                    this,
                    "Se encontraron los siguientes problemas en los datos ingresados:"
                    + Environment.NewLine + Environment.NewLine
                    + string.Join(Environment.NewLine + "• ", errores),
                    "Validación de pasajero",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ==========================================
            // APLICACIÓN O CREACIÓN DEL MODELO
            // ==========================================
            if (esEdicion)
            {
                pasajeroOriginal.Nombres = nombres;
                pasajeroOriginal.Apellidos = apellidos;
                pasajeroOriginal.Email = email;
                pasajeroOriginal.Telefono = telefono;
                pasajeroOriginal.FechaNacimiento = fechaNacimiento;
                pasajeroOriginal.Nacionalidad = nacionalidad;
                pasajeroOriginal.Categoria = categoria;
                pasajeroOriginal.RequiereAsistencia = requiereAsistencia;
                pasajeroOriginal.ServiciosPreferidos = serviciosPreferidos;

                PasajeroResultado = pasajeroOriginal;
            }
            else
            {
                PasajeroResultado = new Pasajero
                {
                    Documento = documento,
                    Nombres = nombres,
                    Apellidos = apellidos,
                    Email = email,
                    Telefono = telefono,
                    FechaNacimiento = fechaNacimiento,
                    Nacionalidad = nacionalidad,
                    Categoria = categoria,
                    RequiereAsistencia = requiereAsistencia,
                    ServiciosPreferidos = serviciosPreferidos
                };
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
