using System;
using System.IO;
using System.Linq;
using System.Text;
using AgendaDeViajes.Data;
using AgendaDeViajes.Models;
using AgendaDeViajes.Services;

namespace AgendaDeViajes.Pruebas
{
    /// <summary>
    /// Verificación automatizada de la Fase 2 sin paquetes externos:
    /// catálogos, datos de demostración, serialización JSON, respaldos e integridad.
    /// </summary>
    internal static class Program
    {
        private static int fallos;

        private static int Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("VERIFICACIÓN DE LA FASE 2 — AGENDA DE VUELOS");
            Console.WriteLine(new string('=', 68));

            ProbarCatalogos();
            ProbarDatosDemostracion();
            ProbarGuardarYCargar();
            ProbarRespaldos();
            ProbarDuplicados();
            ProbarCapacidadYReferencias();
            ProbarCancelacionLiberaAsientos();
            ProbarArchivoDanado();

            Console.WriteLine(new string('=', 68));
            Console.WriteLine(fallos == 0
                ? "RESULTADO: todas las verificaciones pasaron."
                : "RESULTADO: " + fallos + " verificación(es) fallaron.");

            return fallos == 0 ? 0 : 1;
        }

        private static void Verificar(string descripcion, bool condicion, string detalle = null)
        {
            if (condicion)
            {
                Console.WriteLine("[OK]    " + descripcion);
            }
            else
            {
                fallos++;
                Console.WriteLine("[FALLA] " + descripcion + (string.IsNullOrEmpty(detalle) ? string.Empty : " -> " + detalle));
            }
        }

        private static void ProbarCatalogos()
        {
            Console.WriteLine();
            Console.WriteLine("1) Catálogos");

            Verificar("Se registran las 5 aerolíneas esperadas", Catalogos.Aerolineas.Count == 5);
            Verificar("El catálogo de aeropuertos tiene 10 o más entradas", Catalogos.Aeropuertos.Count >= 10);
            Verificar("Existen servicios adicionales", Catalogos.ServiciosAdicionales.Count > 0);
            Verificar("La moneda de la aplicación es USD", Catalogos.MonedaCodigo == "USD");
            Verificar("El formato monetario usa el símbolo $",
                Catalogos.FormatearMoneda(1234.5m).StartsWith("$", StringComparison.Ordinal),
                Catalogos.FormatearMoneda(1234.5m));
            Verificar("El origen y el destino se validan contra el catálogo",
                Catalogos.ExisteAeropuerto("SAL") && !Catalogos.ExisteAeropuerto("XXX"));
        }

        private static void ProbarDatosDemostracion()
        {
            Console.WriteLine();
            Console.WriteLine("2) Datos de demostración");

            AgendaData datos = DatosDemostracion.Crear();

            Verificar("Se crean 10 vuelos", datos.Vuelos.Count == 10, datos.Vuelos.Count.ToString());
            Verificar("Se crean 8 pasajeros", datos.Pasajeros.Count == 8, datos.Pasajeros.Count.ToString());
            Verificar("Se crean 5 reservaciones", datos.Reservaciones.Count == 5, datos.Reservaciones.Count.ToString());
            Verificar("Solo 4 reservaciones ocupan asientos", datos.ReservacionesVigentes == 4, datos.ReservacionesVigentes.ToString());

            ResultadoValidacion validacion = ValidadorAgenda.Validar(datos);
            Verificar("La agenda de ejemplo cumple las reglas de integridad",
                validacion.EsValido,
                validacion.ToString());

            Vuelo directo = datos.BuscarVuelo("AV-1024");
            Verificar("AV-1024 registra 2 asientos ocupados", directo != null && directo.AsientosReservados == 2);
            Verificar("Las salidas de los vuelos son futuras",
                datos.Vuelos.All(v => v.FechaSalida > DateTime.Now));

            decimal totalEsperado = Math.Round(320.50m * 1.65m * 2 + 25m + 30m * 2, 2);
            Reservacion primera = datos.BuscarReservacion("RES-0001");
            Verificar("El total de RES-0001 se calcula con tarifa, servicios y seguro",
                primera != null && primera.Total == totalEsperado,
                primera == null ? "sin reservación" : primera.Total.ToString());
        }

        private static void ProbarGuardarYCargar()
        {
            Console.WriteLine();
            Console.WriteLine("3) Guardar y volver a cargar");

            string carpeta = PrepararCarpeta("roundtrip");
            var repositorio = new AgendaRepository(Path.Combine(carpeta, "agenda.json"));

            AgendaData original = DatosDemostracion.Crear();
            repositorio.Guardar(original);

            Verificar("El archivo JSON se crea en disco", File.Exists(repositorio.RutaArchivo));

            string json = File.ReadAllText(repositorio.RutaArchivo, Encoding.UTF8);
            Verificar("El JSON conserva los acentos (UTF-8)", json.Contains("María José"));
            Verificar("Los enums se guardan como texto", json.Contains("\"Programado\"") && json.Contains("\"Confirmada\""));

            ResultadoCarga carga = repositorio.Cargar();

            Verificar("La carga reporta éxito", carga.Exito, carga.Mensaje);
            Verificar("No se marca el archivo como dañado", !carga.ArchivoDanado);
            Verificar("Se recuperan los 10 vuelos", carga.Datos.Vuelos.Count == 10);
            Verificar("Se recuperan los 8 pasajeros", carga.Datos.Pasajeros.Count == 8);
            Verificar("Se recuperan las 5 reservaciones", carga.Datos.Reservaciones.Count == 5);
            Verificar("Se conserva el total de la reservación",
                carga.Datos.BuscarReservacion("RES-0001").Total == original.BuscarReservacion("RES-0001").Total);
            Verificar("La ocupación se recalcula al cargar",
                carga.Datos.BuscarVuelo("AA-1450").AsientosReservados == 3);
            Verificar("La versión del documento se conserva", carga.Datos.Version == AgendaData.VersionActual);
        }

        private static void ProbarRespaldos()
        {
            Console.WriteLine();
            Console.WriteLine("4) Respaldos antes de sobrescribir");

            string carpeta = PrepararCarpeta("respaldos");
            var repositorio = new AgendaRepository(Path.Combine(carpeta, "agenda.json"));

            AgendaData datos = DatosDemostracion.Crear();
            repositorio.Guardar(datos);
            Verificar("La primera escritura no genera respaldo", !File.Exists(repositorio.RutaRespaldo));

            string telefonoOriginal = datos.Pasajeros[0].Telefono;
            datos.Pasajeros[0].Telefono = "7000-0000";
            repositorio.Guardar(datos);

            Verificar("La segunda escritura genera el respaldo", File.Exists(repositorio.RutaRespaldo));

            var repositorioRespaldo = new AgendaRepository(repositorio.RutaRespaldo);
            ResultadoCarga cargaRespaldo = repositorioRespaldo.Cargar();

            Verificar("El respaldo contiene la versión anterior",
                cargaRespaldo.Exito && cargaRespaldo.Datos.Pasajeros[0].Telefono == telefonoOriginal,
                cargaRespaldo.Mensaje);

            ResultadoCarga cargaActual = repositorio.Cargar();
            Verificar("El archivo principal conserva el cambio más reciente",
                cargaActual.Exito && cargaActual.Datos.Pasajeros[0].Telefono == "7000-0000");
            Verificar("No queda archivo temporal después de guardar", !File.Exists(repositorio.RutaTemporal));
        }

        private static void ProbarDuplicados()
        {
            Console.WriteLine();
            Console.WriteLine("5) Prevención de duplicados");

            AgendaData datos = DatosDemostracion.Crear();
            datos.Vuelos.Add(datos.Vuelos[0]);
            ResultadoValidacion vueloDuplicado = ValidadorAgenda.Validar(datos);
            Verificar("Se detecta un código de vuelo repetido",
                vueloDuplicado.Errores.Any(e => e.Contains("AV-1024") && e.Contains("repetido")),
                vueloDuplicado.ToString());

            AgendaData datos2 = DatosDemostracion.Crear();
            datos2.Pasajeros.Add(datos2.Pasajeros[1]);
            ResultadoValidacion pasajeroDuplicado = ValidadorAgenda.Validar(datos2);
            Verificar("Se detecta un documento de pasajero repetido",
                pasajeroDuplicado.Errores.Any(e => e.Contains("01234567-8") && e.Contains("repetido")),
                pasajeroDuplicado.ToString());

            string carpeta = PrepararCarpeta("duplicados");
            var repositorio = new AgendaRepository(Path.Combine(carpeta, "agenda.json"));
            bool lanzo = false;

            try
            {
                repositorio.Guardar(datos);
            }
            catch (InvalidOperationException)
            {
                lanzo = true;
            }

            Verificar("El repositorio rechaza guardar datos duplicados", lanzo);
        }

        private static void ProbarCapacidadYReferencias()
        {
            Console.WriteLine();
            Console.WriteLine("6) Capacidad, referencias y vuelos cancelados");

            AgendaData datos = DatosDemostracion.Crear();
            datos.Reservaciones.Add(new Reservacion
            {
                Codigo = "RES-9001",
                CodigoVuelo = "CM-441",
                DocumentoPasajero = datos.Pasajeros[1].Documento,
                FechaReserva = DateTime.Now,
                CantidadAsientos = 500,
                Tarifa = TipoTarifa.Economica,
                Total = 100m,
                Estado = EstadoReservacion.Confirmada
            });

            ResultadoValidacion exceso = ValidadorAgenda.Validar(datos);
            Verificar("Se rechaza una reservación que supera la capacidad",
                exceso.Errores.Any(e => e.Contains("CM-441") && e.Contains("capacidad")),
                exceso.ToString());

            AgendaData datos2 = DatosDemostracion.Crear();
            datos2.Reservaciones.Add(new Reservacion
            {
                Codigo = "RES-9002",
                CodigoVuelo = "DL-1600",
                DocumentoPasajero = datos2.Pasajeros[2].Documento,
                FechaReserva = DateTime.Now,
                CantidadAsientos = 1,
                Tarifa = TipoTarifa.Economica,
                Total = 780m,
                Estado = EstadoReservacion.Confirmada
            });

            ResultadoValidacion cancelado = ValidadorAgenda.Validar(datos2);
            Verificar("Se rechaza reservar en un vuelo cancelado",
                cancelado.Errores.Any(e => e.Contains("cancelado")),
                cancelado.ToString());

            AgendaData datos3 = DatosDemostracion.Crear();
            datos3.Reservaciones.Add(new Reservacion
            {
                Codigo = "RES-9003",
                CodigoVuelo = "XX-0000",
                DocumentoPasajero = "NO-EXISTE",
                FechaReserva = DateTime.Now,
                CantidadAsientos = 1,
                Tarifa = TipoTarifa.Economica,
                Total = 50m,
                Estado = EstadoReservacion.Confirmada
            });

            ResultadoValidacion referencias = ValidadorAgenda.Validar(datos3);
            Verificar("Se rechazan reservaciones con vuelo o pasajero inexistente",
                referencias.Errores.Any(e => e.Contains("no existe")),
                referencias.ToString());

            ResultadoValidacion cupo = ValidadorAgenda.ValidarCupoDisponible(
                datos3.BuscarVuelo("AV-1024"), datos3, 1000, null);
            Verificar("La consulta de cupo detecta asientos insuficientes", !cupo.EsValido, cupo.ToString());
        }

        private static void ProbarCancelacionLiberaAsientos()
        {
            Console.WriteLine();
            Console.WriteLine("7) Las reservaciones canceladas liberan asientos");

            AgendaData datos = DatosDemostracion.Crear();
            Vuelo vuelo = datos.BuscarVuelo("AA-1450");
            Reservacion reservacion = datos.BuscarReservacion("RES-0003");

            int antes = vuelo.AsientosReservados;
            reservacion.Estado = EstadoReservacion.Cancelada;
            datos.RecalcularOcupacion();

            Verificar("La ocupación anterior era 3", antes == 3, antes.ToString());
            Verificar("Al cancelar se liberan los 3 asientos",
                vuelo.AsientosReservados == 0,
                vuelo.AsientosReservados.ToString());
            Verificar("Los asientos disponibles se actualizan",
                vuelo.AsientosDisponibles == vuelo.Capacidad);
        }

        private static void ProbarArchivoDanado()
        {
            Console.WriteLine();
            Console.WriteLine("8) Archivo dañado: sin pérdida silenciosa ni sobrescritura automática");

            string carpeta = PrepararCarpeta("danado");
            var repositorio = new AgendaRepository(Path.Combine(carpeta, "agenda.json"));

            File.WriteAllText(repositorio.RutaArchivo, "{ contenido danado sin cierre", new UTF8Encoding(false));

            ResultadoCarga carga = repositorio.Cargar();
            Verificar("La carga informa el fallo", !carga.Exito);
            Verificar("El archivo se marca como dañado", carga.ArchivoDanado);
            Verificar("El repositorio queda bloqueado", repositorio.DatosDanados);

            string contenidoAntes = File.ReadAllText(repositorio.RutaArchivo, Encoding.UTF8);
            bool lanzo = false;

            try
            {
                repositorio.Guardar(DatosDemostracion.Crear());
            }
            catch (InvalidOperationException)
            {
                lanzo = true;
            }

            Verificar("Guardar se bloquea sin autorización explícita", lanzo);
            Verificar("El archivo dañado permanece intacto",
                File.ReadAllText(repositorio.RutaArchivo, Encoding.UTF8) == contenidoAntes);

            repositorio.Guardar(DatosDemostracion.Crear(), true);
            Verificar("La sobrescritura explícita permite recuperar la aplicación",
                repositorio.Cargar().Exito);
            Verificar("Se conserva un respaldo del archivo dañado",
                File.Exists(repositorio.RutaRespaldo)
                && File.ReadAllText(repositorio.RutaRespaldo, Encoding.UTF8).Contains("danado"));

            var repositorioNuevo = new AgendaRepository(Path.Combine(PrepararCarpeta("vacio"), "agenda.json"));
            ResultadoCarga cargaVacia = repositorioNuevo.Cargar();
            Verificar("Sin archivo previo se informa primer arranque",
                !cargaVacia.Exito && cargaVacia.ArchivoNuevo);
        }

        /// <summary>
        /// Crea una carpeta de trabajo aislada dentro del directorio de la prueba.
        /// </summary>
        private static string PrepararCarpeta(string nombre)
        {
            string carpeta = Path.Combine(AppContext.BaseDirectory, "datos-prueba", nombre);

            if (Directory.Exists(carpeta))
            {
                Directory.Delete(carpeta, true);
            }

            Directory.CreateDirectory(carpeta);
            return carpeta;
        }
    }
}
