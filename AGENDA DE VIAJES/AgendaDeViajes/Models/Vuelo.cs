using System;
using System.Text.Json.Serialization;

namespace AgendaDeViajes.Models
{
    /// <summary>
    /// Representa un vuelo programado por la agencia.
    /// El código del vuelo es la clave única del registro.
    /// </summary>
    public class Vuelo
    {
        public string Codigo { get; set; } = string.Empty;

        public string Aerolinea { get; set; } = string.Empty;

        /// <summary>Código IATA del aeropuerto de salida (por ejemplo, SAL).</summary>
        public string Origen { get; set; } = string.Empty;

        /// <summary>Código IATA del aeropuerto de llegada.</summary>
        public string Destino { get; set; } = string.Empty;

        public DateTime FechaSalida { get; set; }

        public int Capacidad { get; set; }

        /// <summary>Precio base por asiento, expresado en dólares estadounidenses (USD).</summary>
        public decimal Precio { get; set; }

        public EstadoVuelo Estado { get; set; } = EstadoVuelo.Programado;

        public int Escalas { get; set; }

        public bool Activo { get; set; } = true;

        public string Observaciones { get; set; } = string.Empty;

        /// <summary>Asientos comprometidos por reservaciones vigentes.</summary>
        public int AsientosReservados { get; set; }

        [JsonIgnore]
        public int AsientosDisponibles
        {
            get { return Math.Max(0, Capacidad - AsientosReservados); }
        }

        [JsonIgnore]
        public double OcupacionPorcentual
        {
            get { return Capacidad <= 0 ? 0d : (double)AsientosReservados / Capacidad * 100d; }
        }

        /// <summary>
        /// Indica si el vuelo admite nuevas reservaciones según su situación operativa.
        /// </summary>
        [JsonIgnore]
        public bool AceptaReservaciones
        {
            get
            {
                return Activo
                    && Estado != EstadoVuelo.Cancelado
                    && Estado != EstadoVuelo.EnVuelo
                    && Estado != EstadoVuelo.Finalizado;
            }
        }

        [JsonIgnore]
        public string Ruta
        {
            get { return Origen + " → " + Destino; }
        }

        public override string ToString()
        {
            return Codigo + " · " + Ruta;
        }
    }
}
