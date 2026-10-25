using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Data
{
    /// <summary>
    /// Contenedor principal de la información de la agenda. Es la raíz del
    /// documento JSON que se guarda en disco.
    /// </summary>
    public class AgendaData
    {
        public const int VersionActual = 1;

        public int Version { get; set; } = VersionActual;

        public DateTime UltimaActualizacion { get; set; } = DateTime.Now;

        public List<Vuelo> Vuelos { get; set; } = new List<Vuelo>();

        public List<Pasajero> Pasajeros { get; set; } = new List<Pasajero>();

        public List<Reservacion> Reservaciones { get; set; } = new List<Reservacion>();

        [JsonIgnore]
        public bool EstaVacia
        {
            get { return Vuelos.Count == 0 && Pasajeros.Count == 0 && Reservaciones.Count == 0; }
        }

        [JsonIgnore]
        public int ReservacionesVigentes
        {
            get { return Reservaciones.Count(r => r.OcupaAsientos); }
        }

        /// <summary>
        /// Busca un vuelo por su código sin distinguir mayúsculas.
        /// </summary>
        public Vuelo BuscarVuelo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            return Vuelos.FirstOrDefault(v =>
                string.Equals(v.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Busca un pasajero por su documento sin distinguir mayúsculas.
        /// </summary>
        public Pasajero BuscarPasajero(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
            {
                return null;
            }

            return Pasajeros.FirstOrDefault(p =>
                string.Equals(p.Documento, documento.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public Reservacion BuscarReservacion(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            return Reservaciones.FirstOrDefault(r =>
                string.Equals(r.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Cantidad de asientos ocupados actualmente en un vuelo.
        /// Solo cuentan las reservaciones no canceladas.
        /// </summary>
        public int AsientosOcupadosDeVuelo(string codigoVuelo)
        {
            if (string.IsNullOrWhiteSpace(codigoVuelo))
            {
                return 0;
            }

            return Reservaciones
                .Where(r => r.OcupaAsientos &&
                            string.Equals(r.CodigoVuelo, codigoVuelo.Trim(), StringComparison.OrdinalIgnoreCase))
                .Sum(r => r.CantidadAsientos);
        }

        /// <summary>
        /// Sincroniza el contador de asientos reservados de cada vuelo con las
        /// reservaciones vigentes. Se ejecuta al cargar datos para garantizar la
        /// integridad entre vuelos y reservaciones.
        /// </summary>
        public void RecalcularOcupacion()
        {
            foreach (Vuelo vuelo in Vuelos)
            {
                vuelo.AsientosReservados = 0;
            }

            foreach (Reservacion reservacion in Reservaciones)
            {
                if (!reservacion.OcupaAsientos)
                {
                    continue;
                }

                Vuelo vuelo = BuscarVuelo(reservacion.CodigoVuelo);

                if (vuelo != null)
                {
                    vuelo.AsientosReservados += reservacion.CantidadAsientos;
                }
            }
        }

        /// <summary>
        /// Deja el contenedor en un estado utilizable: listas no nulas y sin
        /// elementos nulos, y contadores de ocupación consistentes.
        /// </summary>
        public void Normalizar()
        {
            Vuelos = Vuelos ?? new List<Vuelo>();
            Pasajeros = Pasajeros ?? new List<Pasajero>();
            Reservaciones = Reservaciones ?? new List<Reservacion>();

            Vuelos.RemoveAll(v => v == null);
            Pasajeros.RemoveAll(p => p == null);
            Reservaciones.RemoveAll(r => r == null);

            foreach (Pasajero pasajero in Pasajeros)
            {
                pasajero.ServiciosPreferidos = pasajero.ServiciosPreferidos ?? new List<string>();
            }

            foreach (Reservacion reservacion in Reservaciones)
            {
                reservacion.Servicios = reservacion.Servicios ?? new List<string>();
            }

            if (Version <= 0)
            {
                Version = VersionActual;
            }

            RecalcularOcupacion();
        }
    }
}
