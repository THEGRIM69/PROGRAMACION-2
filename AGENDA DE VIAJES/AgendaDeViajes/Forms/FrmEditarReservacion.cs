using System;
using System.Drawing;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Diálogo para el registro y edición de reservaciones.
    /// Presenta los campos correspondientes al modelo Reservacion: código,
    /// vuelo programado, pasajero titular, fecha de emisión, cantidad de asientos,
    /// clase de tarifa, seguro, servicios adicionales y estado operativo.
    /// Calcula dinámicamente el importe total estimado en USD.
    /// </summary>
    public partial class FrmEditarReservacion : Form
    {
        private readonly AgendaData datos;
        private readonly Reservacion reservacionOriginal;
        private readonly bool esEdicion;

        private sealed class VueloItem
        {
            public Vuelo Vuelo { get; }

            public VueloItem(Vuelo vuelo)
            {
                Vuelo = vuelo;
            }

            public override string ToString()
            {
                return $"{Vuelo.Codigo} · {Vuelo.Aerolinea} ({Vuelo.Origen} → {Vuelo.Destino}) · {Catalogos.FormatearMoneda(Vuelo.Precio)}";
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
            int siguiente = (datos.Reservaciones?.Count ?? 0) + 1;
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
        }

        private void clbServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Ejecutar después de que cambie el estado de la casilla
            BeginInvoke(new Action(CalcularTotalEstimado));
        }

        private void CalcularTotalEstimado()
        {
            decimal precioBaseVuelo = 0m;
            if (cboVuelo.SelectedItem is VueloItem itemVuelo)
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
            MessageBox.Show(
                this,
                "Esta funcionalidad estará disponible en la siguiente etapa de integración",
                "Gestión de Reservaciones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

