using System;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;

namespace AgendaDeViajes.Services
{
    /// <summary>
    /// Genera el conjunto de datos de demostración que se utiliza la primera vez
    /// que se ejecuta la aplicación: 10 vuelos, 8 pasajeros y 5 reservaciones.
    /// Los importes están expresados en dólares estadounidenses (USD).
    /// </summary>
    public static class DatosDemostracion
    {
        public static AgendaData Crear()
        {
            var datos = new AgendaData
            {
                Version = AgendaData.VersionActual,
                UltimaActualizacion = DateTime.Now
            };

            datos.Vuelos.AddRange(CrearVuelos());
            datos.Pasajeros.AddRange(CrearPasajeros());
            datos.Reservaciones.AddRange(CrearReservaciones(datos));

            datos.RecalcularOcupacion();
            return datos;
        }

        private static Vuelo[] CrearVuelos()
        {
            return new[]
            {
                new Vuelo
                {
                    Codigo = "AV-1024",
                    Aerolinea = "Avianca",
                    Origen = "SAL",
                    Destino = "MIA",
                    FechaSalida = Fecha(2, 7, 30),
                    Capacidad = 180,
                    Precio = 320.50m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 0,
                    Activo = true,
                    Observaciones = "Vuelo directo de la mañana. Incluye equipaje de mano."
                },
                new Vuelo
                {
                    Codigo = "CM-441",
                    Aerolinea = "Copa Airlines",
                    Origen = "SAL",
                    Destino = "PTY",
                    FechaSalida = Fecha(3, 9, 15),
                    Capacidad = 150,
                    Precio = 210.00m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 0,
                    Activo = true,
                    Observaciones = "Conexión con vuelos hacia Sudamérica."
                },
                new Vuelo
                {
                    Codigo = "AA-1450",
                    Aerolinea = "American Airlines",
                    Origen = "SAL",
                    Destino = "MIA",
                    FechaSalida = Fecha(5, 13, 40),
                    Capacidad = 220,
                    Precio = 365.75m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 0,
                    Activo = true,
                    Observaciones = "Salida de la tarde con servicio de comida a bordo."
                },
                new Vuelo
                {
                    Codigo = "UA-712",
                    Aerolinea = "United Airlines",
                    Origen = "SAL",
                    Destino = "MEX",
                    FechaSalida = Fecha(6, 10, 20),
                    Capacidad = 160,
                    Precio = 298.00m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 1,
                    Activo = true,
                    Observaciones = "Una escala en Ciudad de Guatemala."
                },
                new Vuelo
                {
                    Codigo = "DL-398",
                    Aerolinea = "Delta Air Lines",
                    Origen = "SAL",
                    Destino = "ATL",
                    FechaSalida = Fecha(8, 6, 0),
                    Capacidad = 200,
                    Precio = 410.25m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 1,
                    Activo = true,
                    Observaciones = "Escala técnica en Ciudad de Panamá."
                },
                new Vuelo
                {
                    Codigo = "AV-2050",
                    Aerolinea = "Avianca",
                    Origen = "SAL",
                    Destino = "BOG",
                    FechaSalida = Fecha(10, 15, 45),
                    Capacidad = 150,
                    Precio = 275.90m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 0,
                    Activo = true,
                    Observaciones = "Vuelo de la tarde hacia Bogotá."
                },
                new Vuelo
                {
                    Codigo = "CM-882",
                    Aerolinea = "Copa Airlines",
                    Origen = "SAL",
                    Destino = "SJO",
                    FechaSalida = Fecha(12, 11, 5),
                    Capacidad = 140,
                    Precio = 240.00m,
                    Estado = EstadoVuelo.Retrasado,
                    Escalas = 0,
                    Activo = true,
                    Observaciones = "Retraso estimado de 40 minutos por condiciones climáticas."
                },
                new Vuelo
                {
                    Codigo = "AA-2100",
                    Aerolinea = "American Airlines",
                    Origen = "SAL",
                    Destino = "JFK",
                    FechaSalida = Fecha(15, 8, 30),
                    Capacidad = 240,
                    Precio = 520.00m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 1,
                    Activo = true,
                    Observaciones = "Escala en Miami antes de continuar a Nueva York."
                },
                new Vuelo
                {
                    Codigo = "UA-945",
                    Aerolinea = "United Airlines",
                    Origen = "SAL",
                    Destino = "LAX",
                    FechaSalida = Fecha(18, 22, 10),
                    Capacidad = 230,
                    Precio = 495.60m,
                    Estado = EstadoVuelo.Programado,
                    Escalas = 1,
                    Activo = true,
                    Observaciones = "Vuelo nocturno con escala en Houston."
                },
                new Vuelo
                {
                    Codigo = "DL-1600",
                    Aerolinea = "Delta Air Lines",
                    Origen = "SAL",
                    Destino = "MAD",
                    FechaSalida = Fecha(20, 17, 0),
                    Capacidad = 210,
                    Precio = 780.00m,
                    Estado = EstadoVuelo.Cancelado,
                    Escalas = 1,
                    Activo = false,
                    Observaciones = "Vuelo cancelado por la aerolínea. Los pasajeros serán reubicados."
                }
            };
        }

        private static Pasajero[] CrearPasajeros()
        {
            return new[]
            {
                new Pasajero
                {
                    Documento = "04512345-6",
                    Nombres = "Ana María",
                    Apellidos = "López Rivera",
                    Email = "ana.lopez@example.com",
                    Telefono = "7788-1122",
                    FechaNacimiento = new DateTime(1990, 5, 12),
                    Nacionalidad = "Salvadoreña",
                    Categoria = "Adulto",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Asiento preferente" }
                },
                new Pasajero
                {
                    Documento = "01234567-8",
                    Nombres = "Carlos Alberto",
                    Apellidos = "Ramírez Soto",
                    Email = "carlos.ramirez@example.com",
                    Telefono = "7012-3456",
                    FechaNacimiento = new DateTime(1985, 11, 3),
                    Nacionalidad = "Salvadoreña",
                    Categoria = "Adulto",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Equipaje extra" }
                },
                new Pasajero
                {
                    Documento = "09876543-2",
                    Nombres = "María José",
                    Apellidos = "Hernández Cruz",
                    Email = "maria.hernandez@example.com",
                    Telefono = "7456-8890",
                    FechaNacimiento = new DateTime(1998, 2, 20),
                    Nacionalidad = "Guatemalteca",
                    Categoria = "Adulto",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Asiento preferente", "Comida a bordo" }
                },
                new Pasajero
                {
                    Documento = "05432198-1",
                    Nombres = "Luis Fernando",
                    Apellidos = "Martínez Peña",
                    Email = "luis.martinez@example.com",
                    Telefono = "7601-2233",
                    FechaNacimiento = new DateTime(1979, 7, 8),
                    Nacionalidad = "Hondureña",
                    Categoria = "Adulto",
                    RequiereAsistencia = true,
                    ServiciosPreferidos = { "Prioridad de abordaje" }
                },
                new Pasajero
                {
                    Documento = "06789012-3",
                    Nombres = "Sofía Beatriz",
                    Apellidos = "Aguilar Méndez",
                    Email = "sofia.aguilar@example.com",
                    Telefono = "7234-5566",
                    FechaNacimiento = new DateTime(2001, 9, 15),
                    Nacionalidad = "Salvadoreña",
                    Categoria = "Adulto",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Wi-Fi a bordo" }
                },
                new Pasajero
                {
                    Documento = "02345678-9",
                    Nombres = "Jorge Antonio",
                    Apellidos = "Mejía Campos",
                    Email = "jorge.mejia@example.com",
                    Telefono = "7890-1234",
                    FechaNacimiento = new DateTime(1968, 1, 30),
                    Nacionalidad = "Costarricense",
                    Categoria = "Adulto",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Equipaje extra", "Comida a bordo" }
                },
                new Pasajero
                {
                    Documento = "07654321-4",
                    Nombres = "Valeria Nicole",
                    Apellidos = "Rivas Portillo",
                    Email = "valeria.rivas@example.com",
                    Telefono = "7123-9988",
                    FechaNacimiento = new DateTime(2012, 4, 17),
                    Nacionalidad = "Salvadoreña",
                    Categoria = "Menor",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Asiento preferente" }
                },
                new Pasajero
                {
                    Documento = "P-88213457",
                    Nombres = "Diego Alejandro",
                    Apellidos = "Cruz Salazar",
                    Email = "diego.cruz@example.com",
                    Telefono = "7955-4411",
                    FechaNacimiento = new DateTime(1995, 12, 5),
                    Nacionalidad = "Estadounidense",
                    Categoria = "Adulto",
                    RequiereAsistencia = false,
                    ServiciosPreferidos = { "Mascota en cabina" }
                }
            };
        }

        private static Reservacion[] CrearReservaciones(AgendaData datos)
        {
            var reservaciones = new List<Reservacion>();

            reservaciones.Add(CrearReservacion(
                datos, "RES-0001", "AV-1024", "04512345-6", 2, TipoTarifa.Ejecutiva, true,
                new[] { "Equipaje extra" }, DateTime.Now.AddDays(-4), EstadoReservacion.Confirmada));

            reservaciones.Add(CrearReservacion(
                datos, "RES-0002", "CM-441", "01234567-8", 1, TipoTarifa.Economica, false,
                new string[0], DateTime.Now.AddDays(-3), EstadoReservacion.Confirmada));

            reservaciones.Add(CrearReservacion(
                datos, "RES-0003", "AA-1450", "09876543-2", 3, TipoTarifa.Economica, true,
                new[] { "Asiento preferente", "Comida a bordo" }, DateTime.Now.AddDays(-2), EstadoReservacion.Confirmada));

            reservaciones.Add(CrearReservacion(
                datos, "RES-0004", "AV-2050", "05432198-1", 1, TipoTarifa.Ejecutiva, false,
                new string[0], DateTime.Now.AddDays(-1), EstadoReservacion.Cancelada));

            reservaciones.Add(CrearReservacion(
                datos, "RES-0005", "CM-882", "06789012-3", 2, TipoTarifa.Primera, true,
                new[] { "Equipaje extra", "Wi-Fi a bordo" }, DateTime.Now, EstadoReservacion.Confirmada));

            return reservaciones.ToArray();
        }

        private static Reservacion CrearReservacion(
            AgendaData datos,
            string codigo,
            string codigoVuelo,
            string documento,
            int asientos,
            TipoTarifa tarifa,
            bool seguro,
            string[] servicios,
            DateTime fechaReserva,
            EstadoReservacion estado)
        {
            Vuelo vuelo = datos.BuscarVuelo(codigoVuelo);
            decimal precio = vuelo == null ? 0m : vuelo.Precio;

            return new Reservacion
            {
                Codigo = codigo,
                CodigoVuelo = codigoVuelo,
                DocumentoPasajero = documento,
                FechaReserva = fechaReserva,
                CantidadAsientos = asientos,
                Tarifa = tarifa,
                Seguro = seguro,
                Servicios = new List<string>(servicios),
                Total = Catalogos.CalcularTotalReservacion(precio, tarifa, asientos, servicios.Length, seguro),
                Estado = estado
            };
        }

        private static DateTime Fecha(int diasDesdeHoy, int hora, int minuto)
        {
            return DateTime.Today.AddDays(diasDesdeHoy).AddHours(hora).AddMinutes(minuto);
        }
    }
}
