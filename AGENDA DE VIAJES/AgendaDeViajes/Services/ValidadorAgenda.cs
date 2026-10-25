using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Services
{
    /// <summary>
    /// Resultado de una validación: contiene la lista de problemas encontrados.
    /// </summary>
    public sealed class ResultadoValidacion
    {
        public List<string> Errores { get; } = new List<string>();

        public bool EsValido
        {
            get { return Errores.Count == 0; }
        }

        public void Agregar(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                Errores.Add(error);
            }
        }

        public void AgregarTodo(IEnumerable<string> errores)
        {
            if (errores == null)
            {
                return;
            }

            foreach (string error in errores)
            {
                Agregar(error);
            }
        }

        public override string ToString()
        {
            return EsValido ? "Válido" : string.Join(Environment.NewLine, Errores);
        }
    }

    /// <summary>
    /// Reglas de negocio e integridad de la agenda.
    /// </summary>
    public static class ValidadorAgenda
    {
        private static readonly Regex PatronTelefono = new Regex(@"^[0-9+\-()\s]{7,20}$", RegexOptions.Compiled);

        /// <summary>
        /// Valida la agenda completa: unicidad de claves, referencias entre
        /// entidades, capacidad de los vuelos y coherencia de los importes.
        /// </summary>
        public static ResultadoValidacion Validar(AgendaData datos)
        {
            var resultado = new ResultadoValidacion();

            if (datos == null)
            {
                resultado.Agregar("La agenda no contiene datos.");
                return resultado;
            }

            List<Vuelo> vuelos = datos.Vuelos ?? new List<Vuelo>();
            List<Pasajero> pasajeros = datos.Pasajeros ?? new List<Pasajero>();
            List<Reservacion> reservaciones = datos.Reservaciones ?? new List<Reservacion>();

            if (vuelos.Count == 0 && pasajeros.Count == 0 && reservaciones.Count == 0)
            {
                resultado.Agregar("La agenda está vacía: no hay vuelos, pasajeros ni reservaciones.");
                return resultado;
            }

            ValidarCodigosUnicos(vuelos, resultado);
            ValidarDocumentosUnicos(pasajeros, resultado);
            ValidarCodigosReservacionUnicos(reservaciones, resultado);

            foreach (Vuelo vuelo in vuelos)
            {
                ResultadoValidacion parcial = ValidarVuelo(vuelo, false);
                resultado.AgregarTodo(parcial.Errores.Select(e => "Vuelo " + (vuelo.Codigo ?? "?") + ": " + e));
            }

            foreach (Pasajero pasajero in pasajeros)
            {
                ResultadoValidacion parcial = ValidarPasajero(pasajero);
                resultado.AgregarTodo(parcial.Errores.Select(e => "Pasajero " + (pasajero.Documento ?? "?") + ": " + e));
            }

            foreach (Reservacion reservacion in reservaciones)
            {
                ResultadoValidacion parcial = ValidarReservacion(reservacion, datos);
                resultado.AgregarTodo(parcial.Errores.Select(e => "Reservación " + (reservacion.Codigo ?? "?") + ": " + e));
            }

            ValidarCapacidades(vuelos, reservaciones, resultado);

            return resultado;
        }

        /// <summary>
        /// Valida los datos mínimos de un vuelo.
        /// </summary>
        /// <param name="vuelo">Vuelo a revisar.</param>
        /// <param name="exigirFechaFutura">
        /// true para altas y ediciones: la salida debe ser posterior al momento actual.
        /// false para datos ya almacenados que pueden haberse vuelto históricos.
        /// </param>
        public static ResultadoValidacion ValidarVuelo(Vuelo vuelo, bool exigirFechaFutura)
        {
            var resultado = new ResultadoValidacion();

            if (vuelo == null)
            {
                resultado.Agregar("El vuelo no existe.");
                return resultado;
            }

            if (string.IsNullOrWhiteSpace(vuelo.Codigo))
            {
                resultado.Agregar("El código del vuelo es obligatorio.");
            }
            else if (vuelo.Codigo.Trim().Length < 3 || vuelo.Codigo.Trim().Length > 10)
            {
                resultado.Agregar("El código del vuelo debe tener entre 3 y 10 caracteres.");
            }

            if (!Catalogos.ExisteAerolinea(vuelo.Aerolinea))
            {
                resultado.Agregar("La aerolínea no pertenece al catálogo de la agencia.");
            }

            if (!Catalogos.ExisteAeropuerto(vuelo.Origen))
            {
                resultado.Agregar("El aeropuerto de origen no pertenece al catálogo.");
            }

            if (!Catalogos.ExisteAeropuerto(vuelo.Destino))
            {
                resultado.Agregar("El aeropuerto de destino no pertenece al catálogo.");
            }

            if (!string.IsNullOrWhiteSpace(vuelo.Origen)
                && string.Equals(vuelo.Origen.Trim(), vuelo.Destino == null ? string.Empty : vuelo.Destino.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                resultado.Agregar("El origen y el destino del vuelo no pueden ser iguales.");
            }

            if (vuelo.FechaSalida == default(DateTime))
            {
                resultado.Agregar("La fecha y hora de salida son obligatorias.");
            }
            else if (exigirFechaFutura && vuelo.FechaSalida <= DateTime.Now)
            {
                resultado.Agregar("La fecha de salida debe ser posterior al momento actual.");
            }

            if (vuelo.Capacidad <= 0)
            {
                resultado.Agregar("La capacidad debe ser mayor que cero.");
            }

            if (vuelo.Precio <= 0)
            {
                resultado.Agregar("El precio por asiento debe ser mayor que cero.");
            }
            else if (vuelo.Precio > 100000m)
            {
                resultado.Agregar("El precio por asiento excede el máximo permitido (100,000 USD).");
            }

            if (vuelo.Escalas < 0 || vuelo.Escalas > 3)
            {
                resultado.Agregar("La cantidad de escalas debe estar entre 0 y 3.");
            }

            if (vuelo.AsientosReservados < 0)
            {
                resultado.Agregar("Los asientos reservados no pueden ser negativos.");
            }
            else if (vuelo.Capacidad > 0 && vuelo.AsientosReservados > vuelo.Capacidad)
            {
                resultado.Agregar("Los asientos reservados superan la capacidad del vuelo.");
            }

            return resultado;
        }

        /// <summary>
        /// Valida los datos mínimos de un pasajero.
        /// </summary>
        public static ResultadoValidacion ValidarPasajero(Pasajero pasajero)
        {
            var resultado = new ResultadoValidacion();

            if (pasajero == null)
            {
                resultado.Agregar("El pasajero no existe.");
                return resultado;
            }

            if (string.IsNullOrWhiteSpace(pasajero.Documento))
            {
                resultado.Agregar("El documento de identidad es obligatorio.");
            }
            else if (pasajero.Documento.Trim().Length < 5 || pasajero.Documento.Trim().Length > 20)
            {
                resultado.Agregar("El documento debe tener entre 5 y 20 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(pasajero.Nombres))
            {
                resultado.Agregar("Los nombres son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(pasajero.Apellidos))
            {
                resultado.Agregar("Los apellidos son obligatorios.");
            }

            if (!string.IsNullOrWhiteSpace(pasajero.Email) && !EsCorreoValido(pasajero.Email))
            {
                resultado.Agregar("El correo electrónico no tiene un formato válido.");
            }

            if (string.IsNullOrWhiteSpace(pasajero.Telefono))
            {
                resultado.Agregar("El teléfono es obligatorio.");
            }
            else if (!PatronTelefono.IsMatch(pasajero.Telefono.Trim()))
            {
                resultado.Agregar("El teléfono solo admite dígitos, espacios, paréntesis, guiones y el signo +.");
            }

            if (pasajero.FechaNacimiento == default(DateTime))
            {
                resultado.Agregar("La fecha de nacimiento es obligatoria.");
            }
            else if (pasajero.FechaNacimiento.Date >= DateTime.Today)
            {
                resultado.Agregar("La fecha de nacimiento debe ser anterior a la fecha actual.");
            }
            else if (pasajero.Edad > 120)
            {
                resultado.Agregar("La fecha de nacimiento no es coherente con la edad del pasajero.");
            }

            if (string.IsNullOrWhiteSpace(pasajero.Nacionalidad))
            {
                resultado.Agregar("La nacionalidad es obligatoria.");
            }

            if (!Catalogos.CategoriasPasajero.Any(c =>
                    string.Equals(c, pasajero.Categoria == null ? string.Empty : pasajero.Categoria.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                resultado.Agregar("La categoría del pasajero no es válida.");
            }

            return resultado;
        }

        /// <summary>
        /// Valida una reservación contra las reglas de negocio: referencias
        /// existentes, cupo disponible y vuelo que admita reservaciones.
        /// </summary>
        public static ResultadoValidacion ValidarReservacion(Reservacion reservacion, AgendaData datos)
        {
            var resultado = new ResultadoValidacion();

            if (reservacion == null)
            {
                resultado.Agregar("La reservación no existe.");
                return resultado;
            }

            if (string.IsNullOrWhiteSpace(reservacion.Codigo))
            {
                resultado.Agregar("El código de la reservación es obligatorio.");
            }

            if (datos == null)
            {
                resultado.Agregar("No hay datos de la agenda para verificar la reservación.");
                return resultado;
            }

            Vuelo vuelo = datos.BuscarVuelo(reservacion.CodigoVuelo);

            if (vuelo == null)
            {
                resultado.Agregar("El vuelo " + (reservacion.CodigoVuelo ?? "?") + " no existe en la agenda.");
            }

            if (datos.BuscarPasajero(reservacion.DocumentoPasajero) == null)
            {
                resultado.Agregar("El pasajero " + (reservacion.DocumentoPasajero ?? "?") + " no existe en la agenda.");
            }

            if (reservacion.CantidadAsientos <= 0)
            {
                resultado.Agregar("La cantidad de asientos debe ser mayor que cero.");
            }

            if (reservacion.FechaReserva == default(DateTime))
            {
                resultado.Agregar("La fecha de la reservación es obligatoria.");
            }

            if (reservacion.Total < 0)
            {
                resultado.Agregar("El total de la reservación no puede ser negativo.");
            }

            if (reservacion.OcupaAsientos && vuelo != null)
            {
                if (vuelo.Estado == EstadoVuelo.Cancelado)
                {
                    resultado.Agregar("No se permiten reservaciones vigentes en un vuelo cancelado.");
                }

                if (vuelo.Estado == EstadoVuelo.Finalizado)
                {
                    resultado.Agregar("No se permiten reservaciones vigentes en un vuelo finalizado.");
                }

                if (reservacion.FechaReserva.Date > vuelo.FechaSalida.Date)
                {
                    resultado.Agregar("La reservación no puede registrarse después de la fecha del vuelo.");
                }
            }

            return resultado;
        }

        /// <summary>
        /// Comprueba que, por cada vuelo, la suma de asientos de las reservaciones
        /// vigentes no supere su capacidad.
        /// </summary>
        public static ResultadoValidacion ValidarCupoDisponible(Vuelo vuelo, AgendaData datos, int asientosSolicitados, string codigoReservacionExcluida)
        {
            var resultado = new ResultadoValidacion();

            if (vuelo == null)
            {
                resultado.Agregar("El vuelo no existe.");
                return resultado;
            }

            if (asientosSolicitados <= 0)
            {
                resultado.Agregar("La cantidad de asientos solicitada debe ser mayor que cero.");
                return resultado;
            }

            if (datos == null)
            {
                resultado.Agregar("No hay datos de la agenda para verificar el cupo.");
                return resultado;
            }

            int ocupados = datos.Reservaciones
                .Where(r => r.OcupaAsientos &&
                            string.Equals(r.CodigoVuelo, vuelo.Codigo, StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(r.Codigo, codigoReservacionExcluida, StringComparison.OrdinalIgnoreCase))
                .Sum(r => r.CantidadAsientos);

            if (ocupados + asientosSolicitados > vuelo.Capacidad)
            {
                resultado.Agregar(
                    "El vuelo " + vuelo.Codigo + " solo tiene " + Math.Max(0, vuelo.Capacidad - ocupados)
                    + " asiento(s) disponible(s); se solicitaron " + asientosSolicitados + ".");
            }

            return resultado;
        }

        private static void ValidarCodigosUnicos(List<Vuelo> vuelos, ResultadoValidacion resultado)
        {
            foreach (var grupo in vuelos
                         .Where(v => !string.IsNullOrWhiteSpace(v.Codigo))
                         .GroupBy(v => v.Codigo.Trim(), StringComparer.OrdinalIgnoreCase)
                         .Where(g => g.Count() > 1))
            {
                resultado.Agregar("El código de vuelo '" + grupo.Key + "' está repetido.");
            }
        }

        private static void ValidarDocumentosUnicos(List<Pasajero> pasajeros, ResultadoValidacion resultado)
        {
            foreach (var grupo in pasajeros
                         .Where(p => !string.IsNullOrWhiteSpace(p.Documento))
                         .GroupBy(p => p.Documento.Trim(), StringComparer.OrdinalIgnoreCase)
                         .Where(g => g.Count() > 1))
            {
                resultado.Agregar("El documento de pasajero '" + grupo.Key + "' está repetido.");
            }
        }

        private static void ValidarCodigosReservacionUnicos(List<Reservacion> reservaciones, ResultadoValidacion resultado)
        {
            foreach (var grupo in reservaciones
                         .Where(r => !string.IsNullOrWhiteSpace(r.Codigo))
                         .GroupBy(r => r.Codigo.Trim(), StringComparer.OrdinalIgnoreCase)
                         .Where(g => g.Count() > 1))
            {
                resultado.Agregar("El código de reservación '" + grupo.Key + "' está repetido.");
            }
        }

        private static void ValidarCapacidades(List<Vuelo> vuelos, List<Reservacion> reservaciones, ResultadoValidacion resultado)
        {
            foreach (Vuelo vuelo in vuelos)
            {
                int ocupados = reservaciones
                    .Where(r => r.OcupaAsientos &&
                                string.Equals(r.CodigoVuelo, vuelo.Codigo, StringComparison.OrdinalIgnoreCase))
                    .Sum(r => r.CantidadAsientos);

                if (vuelo.Capacidad > 0 && ocupados > vuelo.Capacidad)
                {
                    resultado.Agregar(
                        "El vuelo " + vuelo.Codigo + " tiene " + ocupados
                        + " asiento(s) reservado(s) y una capacidad de " + vuelo.Capacidad + ".");
                }
            }
        }

        private static bool EsCorreoValido(string correo)
        {
            return MailAddress.TryCreate(correo.Trim(), out _);
        }
    }
}
