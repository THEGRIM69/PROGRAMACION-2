using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AgendaDeViajes.Models
{
    /// <summary>
    /// Representa a un pasajero registrado en la agencia.
    /// El documento de identidad es la clave única del registro.
    /// </summary>
    public class Pasajero
    {
        public string Documento { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string Nacionalidad { get; set; } = string.Empty;

        /// <summary>Categoría del pasajero: Adulto, Menor o Infante.</summary>
        public string Categoria { get; set; } = "Adulto";

        public bool RequiereAsistencia { get; set; }

        /// <summary>Servicios adicionales preferidos por el pasajero.</summary>
        public List<string> ServiciosPreferidos { get; set; } = new List<string>();

        [JsonIgnore]
        public string NombreCompleto
        {
            get { return (Nombres + " " + Apellidos).Trim(); }
        }

        [JsonIgnore]
        public int Edad
        {
            get
            {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - FechaNacimiento.Year;

                if (FechaNacimiento.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

                return edad < 0 ? 0 : edad;
            }
        }

        public override string ToString()
        {
            return NombreCompleto + " (" + Documento + ")";
        }
    }
}
