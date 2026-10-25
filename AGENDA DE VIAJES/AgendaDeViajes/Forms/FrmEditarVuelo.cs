using System;
using System.Drawing;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;
using AgendaDeViajes.Services;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Diálogo para el registro y edición de vuelos.
    /// Valida campos obligatorios, coherencia de fechas, aeropuertos y capacidad.
    /// </summary>
    public partial class FrmEditarVuelo : Form
    {
        private readonly AgendaData datos;
        private readonly Vuelo vueloOriginal;
        private readonly bool esEdicion;

        /// <summary>
        /// Vuelo resultante tras la confirmación del diálogo.
        /// </summary>
        public Vuelo VueloResultado { get; private set; }

        public FrmEditarVuelo(AgendaData datos, Vuelo vueloAEditar = null)
        {
            this.datos = datos ?? throw new ArgumentNullException(nameof(datos));
            this.vueloOriginal = vueloAEditar;
            this.esEdicion = vueloAEditar != null;

            InitializeComponent();
            ConfigurarFormulario();
            CargarCatalogos();

            if (esEdicion)
            {
                CargarDatosVuelo(vueloAEditar);
            }
            else
            {
                CargarValoresPredeterminados();
            }
        }

        private void ConfigurarFormulario()
        {
            if (esEdicion)
            {
                Text = "Editar vuelo — " + vueloOriginal.Codigo;
                lblTitulo.Text = "EDICIÓN DE VUELO";
                lblSubtitulo.Text = "Modifique la información operativa del vuelo seleccionado.";
                txtCodigo.ReadOnly = true;
                txtCodigo.BackColor = Color.FromArgb(244, 246, 248);
                btnGuardar.Text = "Guardar cambios";

                if (vueloOriginal.AsientosReservados > 0)
                {
                    lblInfoOcupacion.Text = $"Asientos comprometidos por reservaciones: {vueloOriginal.AsientosReservados}. La capacidad no puede ser menor a este valor.";
                    lblInfoOcupacion.Visible = true;
                }
                else
                {
                    lblInfoOcupacion.Visible = false;
                }
            }
            else
            {
                Text = "Registrar nuevo vuelo";
                lblTitulo.Text = "REGISTRO DE NUEVO VUELO";
                lblSubtitulo.Text = "Complete los datos del itinerario, capacidad y tarifa de la aeronave.";
                txtCodigo.ReadOnly = false;
                txtCodigo.BackColor = Color.White;
                btnGuardar.Text = "Registrar vuelo";
                lblInfoOcupacion.Visible = false;
            }
        }

        private void CargarCatalogos()
        {
            // Aerolíneas
            cboAerolinea.Items.Clear();
            foreach (string aerolinea in Catalogos.Aerolineas)
            {
                cboAerolinea.Items.Add(aerolinea);
            }

            // Aeropuertos
            cboOrigen.Items.Clear();
            cboDestino.Items.Clear();
            foreach (Aeropuerto aeropuerto in Catalogos.Aeropuertos)
            {
                cboOrigen.Items.Add(aeropuerto);
                cboDestino.Items.Add(aeropuerto);
            }

            // Estados de vuelo
            cboEstado.Items.Clear();
            foreach (EstadoVuelo estado in Enum.GetValues(typeof(EstadoVuelo)))
            {
                cboEstado.Items.Add(estado);
            }
        }

        private void CargarValoresPredeterminados()
        {
            txtCodigo.Text = string.Empty;

            if (cboAerolinea.Items.Count > 0)
            {
                cboAerolinea.SelectedIndex = 0;
            }

            // Origen por defecto: SAL (San Salvador)
            SeleccionarAeropuerto(cboOrigen, "SAL");

            // Destino por defecto: MIA (Miami)
            SeleccionarAeropuerto(cboDestino, "MIA");

            dtpFechaSalida.Value = DateTime.Now.AddDays(7).Date.AddHours(8).AddMinutes(0);
            nudCapacidad.Value = 150;
            nudPrecio.Value = 250.00m;
            nudEscalas.Value = 0;
            cboEstado.SelectedItem = EstadoVuelo.Programado;
            chkActivo.Checked = true;
            txtObservaciones.Text = string.Empty;
        }

        private void CargarDatosVuelo(Vuelo v)
        {
            txtCodigo.Text = v.Codigo;
            cboAerolinea.SelectedItem = v.Aerolinea;

            SeleccionarAeropuerto(cboOrigen, v.Origen);
            SeleccionarAeropuerto(cboDestino, v.Destino);

            dtpFechaSalida.Value = v.FechaSalida > dtpFechaSalida.MinDate ? v.FechaSalida : DateTime.Now;
            nudCapacidad.Value = Math.Max(nudCapacidad.Minimum, Math.Min(nudCapacidad.Maximum, v.Capacidad));
            nudPrecio.Value = Math.Max(nudPrecio.Minimum, Math.Min(nudPrecio.Maximum, v.Precio));
            nudEscalas.Value = Math.Max(nudEscalas.Minimum, Math.Min(nudEscalas.Maximum, v.Escalas));
            cboEstado.SelectedItem = v.Estado;
            chkActivo.Checked = v.Activo;
            txtObservaciones.Text = v.Observaciones ?? string.Empty;
        }

        private static void SeleccionarAeropuerto(ComboBox cbo, string codigoIata)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (cbo.Items[i] is Aeropuerto a &&
                    string.Equals(a.Codigo, codigoIata, StringComparison.OrdinalIgnoreCase))
                {
                    cbo.SelectedIndex = i;
                    return;
                }
            }

            if (cbo.Items.Count > 0)
            {
                cbo.SelectedIndex = 0;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
            {
                return;
            }

            string codigo = txtCodigo.Text.Trim().ToUpperInvariant();
            string aerolinea = cboAerolinea.SelectedItem?.ToString() ?? string.Empty;
            string origen = (cboOrigen.SelectedItem as Aeropuerto)?.Codigo ?? string.Empty;
            string destino = (cboDestino.SelectedItem as Aeropuerto)?.Codigo ?? string.Empty;
            DateTime fechaSalida = dtpFechaSalida.Value;
            int capacidad = (int)nudCapacidad.Value;
            decimal precio = nudPrecio.Value;
            int escalas = (int)nudEscalas.Value;
            EstadoVuelo estado = (EstadoVuelo)(cboEstado.SelectedItem ?? EstadoVuelo.Programado);
            bool activo = chkActivo.Checked;
            string observaciones = txtObservaciones.Text.Trim();

            Vuelo vuelo = esEdicion ? vueloOriginal : new Vuelo();
            vuelo.Codigo = codigo;
            vuelo.Aerolinea = aerolinea;
            vuelo.Origen = origen;
            vuelo.Destino = destino;
            vuelo.FechaSalida = fechaSalida;
            vuelo.Capacidad = capacidad;
            vuelo.Precio = precio;
            vuelo.Escalas = escalas;
            vuelo.Estado = estado;
            vuelo.Activo = activo;
            vuelo.Observaciones = observaciones;

            if (!esEdicion)
            {
                vuelo.AsientosReservados = 0;
            }

            // Validación estricta con las reglas de negocio de ValidadorAgenda
            ResultadoValidacion validacion = ValidadorAgenda.ValidarVuelo(vuelo, !esEdicion);
            if (!validacion.EsValido)
            {
                MessageBox.Show(
                    this,
                    "No se puede guardar el vuelo debido a los siguientes errores:" + Environment.NewLine + Environment.NewLine + validacion,
                    "Validación de vuelo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            VueloResultado = vuelo;
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidarFormulario()
        {
            errorProvider.Clear();
            bool valido = true;

            string codigo = txtCodigo.Text.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                errorProvider.SetError(txtCodigo, "El código del vuelo es obligatorio.");
                valido = false;
            }
            else if (codigo.Length < 3 || codigo.Length > 10)
            {
                errorProvider.SetError(txtCodigo, "El código debe tener entre 3 y 10 caracteres.");
                valido = false;
            }
            else if (!esEdicion && datos.BuscarVuelo(codigo) != null)
            {
                errorProvider.SetError(txtCodigo, "Ya existe un vuelo registrado con este código.");
                valido = false;
            }

            if (cboAerolinea.SelectedItem == null)
            {
                errorProvider.SetError(cboAerolinea, "Seleccione una aerolínea válida.");
                valido = false;
            }

            Aeropuerto aeroOrigen = cboOrigen.SelectedItem as Aeropuerto;
            Aeropuerto aeroDestino = cboDestino.SelectedItem as Aeropuerto;

            if (aeroOrigen == null)
            {
                errorProvider.SetError(cboOrigen, "Seleccione un aeropuerto de origen.");
                valido = false;
            }

            if (aeroDestino == null)
            {
                errorProvider.SetError(cboDestino, "Seleccione un aeropuerto de destino.");
                valido = false;
            }

            if (aeroOrigen != null && aeroDestino != null &&
                string.Equals(aeroOrigen.Codigo, aeroDestino.Codigo, StringComparison.OrdinalIgnoreCase))
            {
                errorProvider.SetError(cboDestino, "El aeropuerto de destino no puede ser igual al de origen.");
                valido = false;
            }

            if (!esEdicion && dtpFechaSalida.Value <= DateTime.Now)
            {
                errorProvider.SetError(dtpFechaSalida, "La fecha de salida para un nuevo vuelo debe ser futura.");
                valido = false;
            }

            if (nudCapacidad.Value <= 0)
            {
                errorProvider.SetError(nudCapacidad, "La capacidad debe ser mayor que cero.");
                valido = false;
            }
            else if (esEdicion && nudCapacidad.Value < vueloOriginal.AsientosReservados)
            {
                errorProvider.SetError(nudCapacidad, $"La capacidad no puede ser menor a los asientos ya reservados ({vueloOriginal.AsientosReservados}).");
                valido = false;
            }

            if (nudPrecio.Value <= 0)
            {
                errorProvider.SetError(nudPrecio, "El precio por asiento debe ser mayor que cero.");
                valido = false;
            }

            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

