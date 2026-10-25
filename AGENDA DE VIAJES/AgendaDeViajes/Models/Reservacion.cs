using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AgendaDeViajes.Models
{
    /// <summary>
    /// Representa la reservación de asientos de un pasajero sobre un vuelo.
    /// </summary>
    public class Reservacion
    {
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Código del vuelo al que pertenece la reservación.</summary>
        public string CodigoVuelo { get; set; } = string.Empty;

        /// <summary>Documento del pasajero que realiza la reservación.</summary>
        public string DocumentoPasajero { get; set; } = string.Empty;

        public DateTime FechaReserva { get; set; }

        public int CantidadAsientos { get; set; }

        public TipoTarifa Tarifa { get; set; } = TipoTarifa.Economica;

        public bool Seguro { get; set; }

        /// <summary>Servicios adicionales contratados en la reservación.</summary>
        public List<string> Servicios { get; set; } = new List<string>();

        /// <summary>Importe total de la reservación, expresado en dólares estadounidenses (USD).</summary>
        public decimal Total { get; set; }

        public EstadoReservacion Estado { get; set; } = EstadoReservacion.Confirmada;

        /// <summary>
        /// Indica si la reservación ocupa asientos del vuelo. Una reservación cancelada
        /// libera los asientos que tenía comprometidos.
        /// </summary>
        [JsonIgnore]
        public bool OcupaAsientos
        {
            get { return Estado != EstadoReservacion.Cancelada; }
        }

        public override string ToString()
        {
            return Codigo + " · " + CodigoVuelo;
        }
    }
}
