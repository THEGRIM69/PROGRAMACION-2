using System;
using System.Collections.Generic;
using System.Globalization;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Data
{
    /// <summary>
    /// Aeropuerto manejado por la agencia.
    /// </summary>
    public sealed class Aeropuerto
    {
        public Aeropuerto(string codigo, string nombre, string ciudad, string pais)
        {
            Codigo = codigo;
            Nombre = nombre;
            Ciudad = ciudad;
            Pais = pais;
        }

        /// <summary>Código IATA del aeropuerto.</summary>
        public string Codigo { get; }

        public string Nombre { get; }

        public string Ciudad { get; }

        public string Pais { get; }

        public override string ToString()
        {
            return Codigo + " · " + Ciudad;
        }
    }

    /// <summary>
    /// Catálogos fijos de la aplicación: aerolíneas, aeropuertos, servicios,
    /// nacionalidades, categorías y factores de tarifa.
    /// </summary>
    public static class Catalogos
    {
        public const string MonedaCodigo = "USD";

        public const string MonedaSimbolo = "$";

        /// <summary>Recargo fijo en USD por cada servicio adicional contratado.</summary>
        public const decimal RecargoPorServicio = 25m;

        /// <summary>Recargo en USD por asiento cuando se contrata el seguro de viaje.</summary>
        public const decimal RecargoSeguroPorAsiento = 30m;

        public static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-SV");

        public static IReadOnlyList<string> Aerolineas { get; } = new List<string>
        {
            "Avianca",
            "Copa Airlines",
            "American Airlines",
            "United Airlines",
            "Delta Air Lines"
        };

        public static IReadOnlyList<Aeropuerto> Aeropuertos { get; } = new List<Aeropuerto>
        {
            new Aeropuerto("SAL", "Aeropuerto Internacional El Salvador", "San Salvador", "El Salvador"),
            new Aeropuerto("MIA", "Miami International Airport", "Miami", "Estados Unidos"),
            new Aeropuerto("JFK", "John F. Kennedy International Airport", "Nueva York", "Estados Unidos"),
            new Aeropuerto("LAX", "Los Angeles International Airport", "Los Ángeles", "Estados Unidos"),
            new Aeropuerto("ATL", "Hartsfield-Jackson Atlanta International Airport", "Atlanta", "Estados Unidos"),
            new Aeropuerto("PTY", "Aeropuerto Internacional Tocumen", "Ciudad de Panamá", "Panamá"),
            new Aeropuerto("BOG", "Aeropuerto Internacional El Dorado", "Bogotá", "Colombia"),
            new Aeropuerto("MEX", "Aeropuerto Internacional Benito Juárez", "Ciudad de México", "México"),
            new Aeropuerto("SJO", "Aeropuerto Internacional Juan Santamaría", "San José", "Costa Rica"),
            new Aeropuerto("GUA", "Aeropuerto Internacional La Aurora", "Ciudad de Guatemala", "Guatemala"),
            new Aeropuerto("LIM", "Aeropuerto Internacional Jorge Chávez", "Lima", "Perú"),
            new Aeropuerto("MAD", "Aeropuerto Adolfo Suárez Madrid-Barajas", "Madrid", "España"),
            new Aeropuerto("YYZ", "Toronto Pearson International Airport", "Toronto", "Canadá")
        };

        public static IReadOnlyList<string> ServiciosAdicionales { get; } = new List<string>
        {
            "Equipaje extra",
            "Asiento preferente",
            "Comida a bordo",
            "Wi-Fi a bordo",
            "Prioridad de abordaje",
            "Mascota en cabina"
        };

        public static IReadOnlyList<string> Nacionalidades { get; } = new List<string>
        {
            "Salvadoreña",
            "Guatemalteca",
            "Hondureña",
            "Nicaragüense",
            "Costarricense",
            "Panameña",
            "Estadounidense",
            "Mexicana",
            "Colombiana",
            "Peruana",
            "Española",
            "Canadiense"
        };

        public static IReadOnlyList<string> CategoriasPasajero { get; } = new List<string>
        {
            "Adulto",
            "Menor",
            "Infante"
        };

        public static IReadOnlyDictionary<TipoTarifa, decimal> FactoresTarifa { get; } =
            new Dictionary<TipoTarifa, decimal>
            {
                { TipoTarifa.Economica, 1.00m },
                { TipoTarifa.Ejecutiva, 1.65m },
                { TipoTarifa.Primera, 2.40m }
            };

        public static decimal ObtenerFactorTarifa(TipoTarifa tarifa)
        {
            return FactoresTarifa.TryGetValue(tarifa, out decimal factor) ? factor : 1.00m;
        }

        /// <summary>
        /// Calcula el importe total de una reservación en USD.
        /// </summary>
        public static decimal CalcularTotalReservacion(
            decimal precioVuelo,
            TipoTarifa tarifa,
            int cantidadAsientos,
            int cantidadServicios,
            bool seguro)
        {
            if (precioVuelo < 0 || cantidadAsientos <= 0)
            {
                return 0m;
            }

            decimal total = precioVuelo * ObtenerFactorTarifa(tarifa) * cantidadAsientos;
            total += RecargoPorServicio * Math.Max(0, cantidadServicios);

            if (seguro)
            {
                total += RecargoSeguroPorAsiento * cantidadAsientos;
            }

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Devuelve true si el código corresponde a un aeropuerto del catálogo.
        /// </summary>
        public static bool ExisteAeropuerto(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return false;
            }

            foreach (Aeropuerto aeropuerto in Aeropuertos)
            {
                if (string.Equals(aeropuerto.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ExisteAerolinea(string aerolinea)
        {
            if (string.IsNullOrWhiteSpace(aerolinea))
            {
                return false;
            }

            foreach (string nombre in Aerolineas)
            {
                if (string.Equals(nombre, aerolinea.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Formatea un importe con el formato monetario de dólares estadounidenses.
        /// </summary>
        public static string FormatearMoneda(decimal monto)
        {
            return monto.ToString("C2", Cultura);
        }

        /// <summary>
        /// Devuelve la ciudad asociada a un código IATA, o el propio código si no existe.
        /// </summary>
        public static string ObtenerCiudad(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return string.Empty;
            }

            foreach (Aeropuerto aeropuerto in Aeropuertos)
            {
                if (string.Equals(aeropuerto.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return aeropuerto.Ciudad;
                }
            }

            return codigo.Trim();
        }
    }
}
