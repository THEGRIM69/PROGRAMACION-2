using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;
using AgendaDeViajes.Services;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Diálogo para el registro y edición de reservaciones.
    /// Presenta los campos correspondientes al modelo Reservacion: código,
    /// vuelo programado, pasajero titular, fecha de emisión, cantidad de asientos,
    /// clase de tarifa, seguro, servicios adicionales y estado operativo.
    /// Calcula dinámicamente el importe total estimado en USD y valida cupos en tiempo real.
    /// </summary>
    public partial class FrmEditarReservacion : Form
    {
        private readonly AgendaData datos;
        private readonly Reservacion reservacionOriginal;
        private readonly bool esEdicion;

        /// <summary>
        /// Resultado de la operación una vez completada y confirmada la validación.
        /// </summary>
        public Reservacion ReservacionResultado { get; private set; }

        private sealed class VueloItem
        {
            public Vuelo Vuelo { get; }

            public VueloItem(Vuelo vuelo)
            {
                Vuelo = vuelo;
            }

            public override string ToString()
            {
                int disponibles = Math.Max(0, Vuelo.Capacidad - Vuelo.AsientosReservados);
                string estadoInfo = Vuelo.Estado != EstadoVuelo.Programado ? $" [{Vuelo.Estado}]" : string.Empty;
                return $"{Vuelo.Codigo} · {Vuelo.Aerolinea} ({Vuelo.Origen} → {Vuelo.Destino}) · Disp: {disponibles}/{Vuelo.Capacidad} · {Catalogos.FormatearMoneda(Vuelo.Precio)}{estadoInfo}";
            }
        }

        private sealed class PasajeroItem
        {
            public Pasajero Pasajero { get; }

            public PasajeroItem(Pasajero pasajero)
            {
                Pasajero = pasajero;
            }

            public override string ToString()
            {
                return $"{Pasajero.NombreCompleto} ({Pasajero.Documento}) · {Pasajero.Nacionalidad}";
            }
        }

        public FrmEditarReservacion(AgendaData datos, Reservacion reservacionAEditar = null)
        {
            this.datos = datos ?? throw new ArgumentNullException(nameof(datos));
            this.reservacionOriginal = reservacionAEditar;
            this.esEdicion = reservacionAEditar != null;

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

            CalcularTotalEstimado();
            ActualizarDisponibilidadVuelo();
        }

        private void ConfigurarCatalogos()
        {
            // Vuelos programados
            cboVuelo.Items.Clear();
            if (datos.Vuelos != null)
            {
                foreach (Vuelo v in datos.Vuelos)
                {
                    cboVuelo.Items.Add(new VueloItem(v));
                }
            }
            if (cboVuelo.Items.Count > 0)
            {
                cboVuelo.SelectedIndex = 0;
            }

            // Pasajeros registrados
            cboPasajero.Items.Clear();
            if (datos.Pasajeros != null)
            {
                foreach (Pasajero p in datos.Pasajeros)
                {
                    cboPasajero.Items.Add(new PasajeroItem(p));
                }
            }
            if (cboPasajero.Items.Count > 0)
            {
                cboPasajero.SelectedIndex = 0;
            }

            // Clases de Tarifa
            cboTarifa.Items.Clear();
            foreach (TipoTarifa tarifa in Enum.GetValues(typeof(TipoTarifa)))
            {
                cboTarifa.Items.Add(tarifa);
            }
            cboTarifa.SelectedIndex = 0;

            // Estados de Reservación
            cboEstado.Items.Clear();
            foreach (EstadoReservacion estado in Enum.GetValues(typeof(EstadoReservacion)))
            {
                cboEstado.Items.Add(estado);
            }
            cboEstado.SelectedIndex = 0;

            // Servicios adicionales
            clbServicios.Items.Clear();
            foreach (string servicio in Catalogos.ServiciosAdicionales)
            {
                clbServicios.Items.Add(servicio);
            }
        }

        private void ConfigurarModoNuevo()
        {
            Text = "Registrar nueva reservación";
            lblTitulo.Text = "REGISTRO DE RESERVACIÓN";
            lblSubtitulo.Text = "Asigne un vuelo programado, pasajero titular, cupo de asientos, tarifa y servicios.";
            txtCodigo.ReadOnly = false;
            txtCodigo.BackColor = Color.White;
            txtCodigo.Text = GenerarSugerenciaCodigo();
            dtpFechaReserva.Value = DateTime.Now;
            numAsientos.Value = 1;
            chkSeguro.Checked = false;
            btnGuardar.Text = "Registrar reservación";
        }

        private string GenerarSugerenciaCodigo()
        {
            int maxId = 0;
            if (datos.Reservaciones != null)
            {
                foreach (Reservacion r in datos.Reservaciones)
                {
                    if (!string.IsNullOrWhiteSpace(r.Codigo) && r.Codigo.StartsWith("RES-", StringComparison.OrdinalIgnoreCase))
                    {
                        string numParte = r.Codigo.Substring(4);
                        if (int.TryParse(numParte, out int id) && id > maxId)
                        {
                            maxId = id;
                        }
                    }
                }
            }
            int siguiente = maxId > 0 ? maxId + 1 : (datos.Reservaciones?.Count ?? 0) + 1;
            return $"RES-{siguiente:D4}";
        }

        private void ConfigurarModoEdicion()
        {
            Text = "Editar reservación — " + reservacionOriginal.Codigo;
            lblTitulo.Text = "EDICIÓN DE RESERVACIÓN";
            lblSubtitulo.Text = "Modifique los parámetros operativos y servicios de la reservación.";
            txtCodigo.Text = reservacionOriginal.Codigo;
            txtCodigo.ReadOnly = true;
            txtCodigo.BackColor = Color.FromArgb(244, 246, 248);

            // Seleccionar vuelo
            for (int i = 0; i < cboVuelo.Items.Count; i++)
            {
                if (cboVuelo.Items[i] is VueloItem item &&
                    string.Equals(item.Vuelo.Codigo, reservacionOriginal.CodigoVuelo, StringComparison.OrdinalIgnoreCase))
                {
                    cboVuelo.SelectedIndex = i;
                    break;
                }
            }

            // Seleccionar pasajero
            for (int i = 0; i < cboPasajero.Items.Count; i++)
            {
                if (cboPasajero.Items[i] is PasajeroItem item &&
                    string.Equals(item.Pasajero.Documento, reservacionOriginal.DocumentoPasajero, StringComparison.OrdinalIgnoreCase))
                {
                    cboPasajero.SelectedIndex = i;
                    break;
                }
            }

            if (reservacionOriginal.FechaReserva > DateTime.MinValue)
            {
                dtpFechaReserva.Value = reservacionOriginal.FechaReserva;
            }

            if (reservacionOriginal.CantidadAsientos >= numAsientos.Minimum &&
                reservacionOriginal.CantidadAsientos <= numAsientos.Maximum)
            {
                numAsientos.Value = reservacionOriginal.CantidadAsientos;
            }

            cboTarifa.SelectedItem = reservacionOriginal.Tarifa;
            cboEstado.SelectedItem = reservacionOriginal.Estado;
            chkSeguro.Checked = reservacionOriginal.Seguro;

            if (reservacionOriginal.Servicios != null)
            {
                for (int i = 0; i < clbServicios.Items.Count; i++)
                {
                    string servicio = clbServicios.Items[i].ToString();
                    if (reservacionOriginal.Servicios.Contains(servicio))
                    {
                        clbServicios.SetItemChecked(i, true);
                    }
                }
            }

            btnGuardar.Text = "Guardar cambios";
        }

        private void ActualizarCalculoTotal(object sender, EventArgs e)
        {
            CalcularTotalEstimado();
            ActualizarDisponibilidadVuelo();
        }

        private void ActualizarDisponibilidadVuelo()
        {
            if (cboVuelo.SelectedItem is VueloItem itemVuelo && itemVuelo.Vuelo != null)
            {
                Vuelo v = itemVuelo.Vuelo;
                string excluirCodigo = esEdicion ? reservacionOriginal?.Codigo : null;
                int ocupadosOtros = datos.Reservaciones != null
                    ? datos.Reservaciones
                        .Where(r => r.OcupaAsientos &&
                                    string.Equals(r.CodigoVuelo, v.Codigo, StringComparison.OrdinalIgnoreCase) &&
                                    !string.Equals(r.Codigo, excluirCodigo, StringComparison.OrdinalIgnoreCase))
                        .Sum(r => r.CantidadAsientos)
                    : 0;
                int disponibles = Math.Max(0, v.Capacidad - ocupadosOtros);

                if (v.Estado == EstadoVuelo.Cancelado)
                {
                    lblAvisoIntegracion.Text = $"Vuelo {v.Codigo}: CANCELADO · No admite nuevas reservaciones.";
                    lblAvisoIntegracion.ForeColor = Color.FromArgb(192, 57, 43);
                }
                else if (!v.Activo)
                {
                    lblAvisoIntegracion.Text = $"Vuelo {v.Codigo}: INACTIVO para la operación comercial.";
                    lblAvisoIntegracion.ForeColor = Color.FromArgb(192, 57, 43);
                }
                else
                {
                    lblAvisoIntegracion.Text = $"Vuelo {v.Codigo}: {disponibles} de {v.Capacidad} asientos disponibles · Tarifa base {Catalogos.FormatearMoneda(v.Precio)}";
                    lblAvisoIntegracion.ForeColor = disponibles > 0 ? Color.FromArgb(45, 156, 219) : Color.FromArgb(192, 57, 43);
                }
            }
            else
            {
                lblAvisoIntegracion.Text = "Seleccione un vuelo programado para verificar el cupo disponible.";
                lblAvisoIntegracion.ForeColor = Color.FromArgb(107, 122, 140);
            }
        }

        private void clbServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Ejecutar después de que cambie el estado de la casilla
            BeginInvoke(new Action(CalcularTotalEstimado));
        }

        private void CalcularTotalEstimado()
        {
            decimal precioBaseVuelo = 0m;
            if (cboVuelo.SelectedItem is VueloItem itemVuelo && itemVuelo.Vuelo != null)
            {
                precioBaseVuelo = itemVuelo.Vuelo.Precio;
            }

            TipoTarifa tarifa = TipoTarifa.Economica;
            if (cboTarifa.SelectedItem is TipoTarifa t)
            {
                tarifa = t;
            }

            int asientos = (int)numAsientos.Value;
            int servicios = clbServicios.CheckedItems.Count;
            bool seguro = chkSeguro.Checked;

            decimal total = Catalogos.CalcularTotalReservacion(precioBaseVuelo, tarifa, asientos, servicios, seguro);
            txtTotalEstimado.Text = Catalogos.FormatearMoneda(total);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim().ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show(
                    this,
                    "El código de reservación es obligatorio.",
                    "Validación de Reservación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            if (!esEdicion)
            {
                if (datos.Reservaciones != null && datos.Reservaciones.Any(r =>
                    string.Equals(r.Codigo, codigo, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show(
                        this,
                        $"Ya existe una reservación registrada con el código '{codigo}'. Ingrese un código único.",
                        "Código duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCodigo.Focus();
                    return;
                }
            }

            if (!(cboVuelo.SelectedItem is VueloItem itemVuelo) || itemVuelo.Vuelo == null)
            {
                MessageBox.Show(
                    this,
                    "Debe seleccionar un vuelo programado para la reservación.",
                    "Validación de Reservación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboVuelo.Focus();
                return;
            }
            Vuelo vuelo = itemVuelo.Vuelo;

            if (!(cboPasajero.SelectedItem is PasajeroItem itemPasajero) || itemPasajero.Pasajero == null)
            {
                MessageBox.Show(
                    this,
                    "Debe seleccionar el pasajero titular de la reservación.",
                    "Validación de Reservación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboPasajero.Focus();
                return;
            }
            Pasajero pasajero = itemPasajero.Pasajero;

            int asientos = (int)numAsientos.Value;
            if (asientos <= 0)
            {
                MessageBox.Show(
                    this,
                    "La cantidad de asientos debe ser de al menos 1.",
                    "Validación de Reservación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                numAsientos.Focus();
                return;
            }

            TipoTarifa tarifa = cboTarifa.SelectedItem is TipoTarifa t ? t : TipoTarifa.Economica;
            EstadoReservacion estado = cboEstado.SelectedItem is EstadoReservacion est ? est : EstadoReservacion.Confirmada;
            bool seguro = chkSeguro.Checked;

            var servicios = new List<string>();
            foreach (var item in clbServicios.CheckedItems)
            {
                servicios.Add(item.ToString());
            }

            decimal total = Catalogos.CalcularTotalReservacion(vuelo.Precio, tarifa, asientos, servicios.Count, seguro);

            // Validar estado del vuelo si la reservación ocupará asientos
            bool ocupaAsientos = estado != EstadoReservacion.Cancelada;
            if (ocupaAsientos)
            {
                if (vuelo.Estado == EstadoVuelo.Cancelado)
                {
                    MessageBox.Show(
                        this,
                        $"El vuelo '{vuelo.Codigo}' se encuentra cancelado. No es posible crear ni activar reservaciones sobre vuelos cancelados.",
                        "Vuelo no disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (!vuelo.Activo)
                {
                    MessageBox.Show(
                        this,
                        $"El vuelo '{vuelo.Codigo}' no está activo para la operación comercial.",
                        "Vuelo inactivo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (vuelo.Estado == EstadoVuelo.Finalizado)
                {
                    MessageBox.Show(
                        this,
                        $"El vuelo '{vuelo.Codigo}' ya ha finalizado su itinerario.",
                        "Vuelo finalizado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validar cupo disponible
                string excluirCodigo = esEdicion ? reservacionOriginal?.Codigo : null;
                ResultadoValidacion cupoValidacion = ValidadorAgenda.ValidarCupoDisponible(vuelo, datos, asientos, excluirCodigo);
                if (!cupoValidacion.EsValido)
                {
                    MessageBox.Show(
                        this,
                        cupoValidacion.ToString(),
                        "Cupo insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    numAsientos.Focus();
                    return;
                }
            }

            // Advertencia informativa si el pasajero ya tiene reservación activa en el mismo vuelo
            bool tieneOtraReserva = datos.Reservaciones != null && datos.Reservaciones.Any(r =>
                r.OcupaAsientos &&
                string.Equals(r.DocumentoPasajero, pasajero.Documento, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(r.CodigoVuelo, vuelo.Codigo, StringComparison.OrdinalIgnoreCase) &&
                (!esEdicion || !string.Equals(r.Codigo, reservacionOriginal.Codigo, StringComparison.OrdinalIgnoreCase)));

            if (tieneOtraReserva && ocupaAsientos)
            {
                DialogResult respuesta = MessageBox.Show(
                    this,
                    $"El pasajero {pasajero.NombreCompleto} ya cuenta con una reservación activa en el vuelo {vuelo.Codigo}.\r\n\r\n¿Desea registrar una reservación adicional para este pasajero en el mismo vuelo?",
                    "Reservación adicional",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }
            }

            Reservacion reservacion;
            if (esEdicion)
            {
                reservacionOriginal.CodigoVuelo = vuelo.Codigo;
                reservacionOriginal.DocumentoPasajero = pasajero.Documento;
                reservacionOriginal.FechaReserva = dtpFechaReserva.Value;
                reservacionOriginal.CantidadAsientos = asientos;
                reservacionOriginal.Tarifa = tarifa;
                reservacionOriginal.Seguro = seguro;
                reservacionOriginal.Servicios = servicios;
                reservacionOriginal.Total = total;
                reservacionOriginal.Estado = estado;
                reservacion = reservacionOriginal;
            }
            else
            {
                reservacion = new Reservacion
                {
                    Codigo = codigo,
                    CodigoVuelo = vuelo.Codigo,
                    DocumentoPasajero = pasajero.Documento,
                    FechaReserva = dtpFechaReserva.Value,
                    CantidadAsientos = asientos,
                    Tarifa = tarifa,
                    Seguro = seguro,
                    Servicios = servicios,
                    Total = total,
                    Estado = estado
                };
            }

            ResultadoValidacion valReservacion = ValidadorAgenda.ValidarReservacion(reservacion, datos);
            if (!valReservacion.EsValido)
            {
                MessageBox.Show(
                    this,
                    "No se puede guardar la reservación debido a inconsistencias:\r\n\r\n" + valReservacion,
                    "Validación fallida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ReservacionResultado = reservacion;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

