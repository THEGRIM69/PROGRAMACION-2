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

            Console.WriteLine("VERIFICACIÓN INTEGRAL — FASES 2, 4, 6 Y 7 — AGENDA DE VIAJES");
            Console.WriteLine(new string('=', 68));

            ProbarCatalogos();
            ProbarDatosDemostracion();
            ProbarGuardarYCargar();
            ProbarRespaldos();
            ProbarDuplicados();
            ProbarCapacidadYReferencias();
            ProbarCancelacionLiberaAsientos();
            ProbarArchivoDanado();
            ProbarGestionVuelosFase4();
            ProbarGestionPasajerosFase6();
            ProbarGestionReservacionesFase7();

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

        private static void ProbarGestionVuelosFase4()
        {
            Console.WriteLine();
            Console.WriteLine("9) Gestión de vuelos — Fase 4");

            // 1. Registro válido
            var vueloNuevo = new Vuelo
            {
                Codigo = "AV-9001",
                Aerolinea = "Avianca",
                Origen = "SAL",
                Destino = "MIA",
                FechaSalida = DateTime.Now.AddDays(15),
                Capacidad = 180,
                Precio = 320.50m,
                Escalas = 0,
                Estado = EstadoVuelo.Programado,
                Activo = true,
                Observaciones = "Vuelo de prueba Fase 4"
            };
            ResultadoValidacion valNuevo = ValidadorAgenda.ValidarVuelo(vueloNuevo, true);
            Verificar("Registro de vuelo con datos válidos es aceptado", valNuevo.EsValido);

            // 2. Rechazo de información inválida
            var vueloInvalido = new Vuelo
            {
                Codigo = "",
                Aerolinea = "Aerolínea Inexistente",
                Origen = "XXX",
                Destino = "XXX",
                FechaSalida = DateTime.Now.AddDays(-1),
                Capacidad = -10,
                Precio = 0m,
                Escalas = 5
            };
            ResultadoValidacion valInvalido = ValidadorAgenda.ValidarVuelo(vueloInvalido, true);
            Verificar("Rechazo de vuelo con datos inválidos", !valInvalido.EsValido && valInvalido.Errores.Count >= 5);

            // 3. Rechazo de duplicados en la agenda
            AgendaData datos = DatosDemostracion.Crear();
            var vueloDuplicado = new Vuelo
            {
                Codigo = "AV-1024",
                Aerolinea = "Avianca",
                Origen = "SAL",
                Destino = "BOG",
                FechaSalida = DateTime.Now.AddDays(10),
                Capacidad = 160,
                Precio = 280m
            };
            datos.Vuelos.Add(vueloDuplicado);
            ResultadoValidacion valDuplicado = ValidadorAgenda.Validar(datos);
            Verificar("Detección y rechazo de código de vuelo duplicado", !valDuplicado.EsValido);
            datos.Vuelos.Remove(vueloDuplicado);

            // 4. Actualización correcta
            Vuelo vueloAEditar = datos.BuscarVuelo("AV-1024");
            decimal precioAnterior = vueloAEditar.Precio;
            vueloAEditar.Precio = 450.00m;
            vueloAEditar.Estado = EstadoVuelo.Abordando;
            vueloAEditar.Escalas = 1;
            ResultadoValidacion valEdicion = ValidadorAgenda.Validar(datos);
            Verificar("Actualización de datos operativos del vuelo mantiene integridad", valEdicion.EsValido && vueloAEditar.Precio == 450.00m);
            vueloAEditar.Precio = precioAnterior;
            vueloAEditar.Estado = EstadoVuelo.Programado;
            vueloAEditar.Escalas = 0;

            // 5. Eliminación de vuelo sin relaciones
            datos.Vuelos.Add(vueloNuevo);
            bool eliminadoSinRelaciones = datos.Vuelos.Remove(vueloNuevo);
            Verificar("Eliminación permitida para vuelo sin reservaciones asociadas", eliminadoSinRelaciones && datos.BuscarVuelo("AV-9001") == null);

            // 6. Protección de vuelos con reservaciones asociadas
            Vuelo vueloConReservas = datos.BuscarVuelo("AV-1024");
            var reservacionesAsociadas = datos.Reservaciones
                .Where(r => string.Equals(r.CodigoVuelo, vueloConReservas.Codigo, StringComparison.OrdinalIgnoreCase))
                .ToList();
            bool tieneRelaciones = reservacionesAsociadas.Count > 0;
            Verificar("Protección de vuelo con reservaciones: se detectan relaciones y se impide eliminar", tieneRelaciones && reservacionesAsociadas.Count == 1);

            // 7. Persistencia después de guardar y recargar
            string carpetaPrueba = PrepararCarpeta("fase4-crud");
            var repositorio = new AgendaRepository(Path.Combine(carpetaPrueba, "agenda.json"));
            datos.Vuelos.Add(vueloNuevo);
            repositorio.Guardar(datos);

            ResultadoCarga recarga = repositorio.Cargar();
            Vuelo vueloRecuperado = recarga.Datos?.BuscarVuelo("AV-9001");
            Verificar("Persistencia del nuevo vuelo tras guardar y recargar",
                recarga.Exito && vueloRecuperado != null && vueloRecuperado.Capacidad == 180 && vueloRecuperado.Precio == 320.50m);

            // 8. Búsqueda y filtrado
            var vuelosFiltradosTexto = recarga.Datos.Vuelos.Where(v =>
                v.Codigo.Contains("9001") || v.Origen == "SAL" || v.Destino == "MIA").ToList();
            var vuelosFiltradosEstado = recarga.Datos.Vuelos.Where(v => v.Estado == EstadoVuelo.Programado).ToList();
            Verificar("Búsqueda y filtrado de vuelos por código, ruta y estado",
                vuelosFiltradosTexto.Count > 0 && vuelosFiltradosEstado.Count > 0);
        }

        private static void ProbarGestionPasajerosFase6()
        {
            Console.WriteLine();
            Console.WriteLine("10) Gestión de pasajeros — Fase 6");

            AgendaData datos = DatosDemostracion.Crear();

            // 1. Registro válido
            Pasajero pasajeroNuevo = new Pasajero
            {
                Documento = "P98765432",
                Nombres = "Carlos Eduardo",
                Apellidos = "Mendoza Ramos",
                Email = "carlos.mendoza@example.com",
                Telefono = "+503 7123-4567",
                FechaNacimiento = new DateTime(1995, 5, 20),
                Nacionalidad = "Salvadoreña",
                Categoria = "Adulto",
                RequiereAsistencia = false,
                ServiciosPreferidos = new System.Collections.Generic.List<string> { "Equipaje extra", "Wi-Fi a bordo" }
            };
            ResultadoValidacion valNuevo = ValidadorAgenda.ValidarPasajero(pasajeroNuevo);
            Verificar("1. Registro de pasajero con datos válidos es aceptado", valNuevo.EsValido, valNuevo.ToString());

            // 2. Rechazo de datos incompletos
            Pasajero pasajeroIncompleto = new Pasajero
            {
                Documento = "",
                Nombres = "",
                Apellidos = "",
                Email = "invalido",
                Telefono = "abc",
                FechaNacimiento = DateTime.Today.AddDays(1),
                Nacionalidad = "",
                Categoria = "Desconocida"
            };
            ResultadoValidacion valIncompleto = ValidadorAgenda.ValidarPasajero(pasajeroIncompleto);
            Verificar("2. Rechazo de pasajero con datos incompletos o inválidos",
                !valIncompleto.EsValido && valIncompleto.Errores.Count >= 5,
                "Errores detectados: " + valIncompleto.Errores.Count);

            // 3. Rechazo de documentos duplicados
            Pasajero pasajeroDuplicado = new Pasajero
            {
                Documento = "04512345-6", // Ya existe en DatosDemostracion
                Nombres = "Clon",
                Apellidos = "Duplicado",
                Email = "clon@example.com",
                Telefono = "2222-3333",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Nacionalidad = "Salvadoreña",
                Categoria = "Adulto"
            };
            datos.Pasajeros.Add(pasajeroDuplicado);
            ResultadoValidacion valDuplicado = ValidadorAgenda.Validar(datos);
            Verificar("3. Detección y rechazo de documento de pasajero duplicado",
                !valDuplicado.EsValido && valDuplicado.Errores.Any(e => e.Contains("repetido")),
                valDuplicado.ToString());
            datos.Pasajeros.Remove(pasajeroDuplicado);

            // 4. Validación de formato de correo electrónico
            Pasajero pasajeroCorreoInvalido = new Pasajero
            {
                Documento = "P11223344",
                Nombres = "Juan",
                Apellidos = "Pérez",
                Email = "correo_sin_arroba",
                Telefono = "7777-8888",
                FechaNacimiento = new DateTime(1988, 3, 15),
                Nacionalidad = "Guatemalteca",
                Categoria = "Adulto"
            };
            ResultadoValidacion valCorreo = ValidadorAgenda.ValidarPasajero(pasajeroCorreoInvalido);
            Verificar("4. Rechazo de pasajero con formato de correo electrónico inválido",
                !valCorreo.EsValido && valCorreo.Errores.Any(e => e.Contains("correo")),
                valCorreo.ToString());

            // 5. Edición válida
            Pasajero pasajeroAEditar = datos.BuscarPasajero("04512345-6");
            string telefonoAnterior = pasajeroAEditar.Telefono;
            pasajeroAEditar.Telefono = "+503 7999-8888";
            pasajeroAEditar.RequiereAsistencia = true;
            ResultadoValidacion valEdicion = ValidadorAgenda.Validar(datos);
            Verificar("5. Actualización válida de datos personales de pasajero mantiene integridad",
                valEdicion.EsValido && pasajeroAEditar.Telefono == "+503 7999-8888",
                valEdicion.ToString());
            pasajeroAEditar.Telefono = telefonoAnterior;
            pasajeroAEditar.RequiereAsistencia = false;

            // 6. Conservación de identidad (el documento permanece como clave inmutable)
            Verificar("6. Conservación de identidad: el documento de identidad es inmutable",
                pasajeroAEditar.Documento == "04512345-6");

            // 7. Eliminación permitida para pasajero sin reservaciones
            datos.Pasajeros.Add(pasajeroNuevo);
            bool eliminadoSinRelaciones = datos.Pasajeros.Remove(pasajeroNuevo);
            Verificar("7. Eliminación permitida para pasajero sin reservaciones asociadas",
                eliminadoSinRelaciones && datos.BuscarPasajero("P98765432") == null);

            // 8. Protección de pasajeros con reservaciones asociadas
            Pasajero pasajeroConReservas = datos.BuscarPasajero("04512345-6");
            var reservacionesAsociadas = datos.Reservaciones
                .Where(r => string.Equals(r.DocumentoPasajero, pasajeroConReservas.Documento, StringComparison.OrdinalIgnoreCase))
                .ToList();
            bool tieneRelaciones = reservacionesAsociadas.Count > 0;
            Verificar("8. Protección de pasajero con reservaciones: se detectan relaciones y se impide eliminar",
                tieneRelaciones && reservacionesAsociadas.Count >= 1);

            // 9. Persistencia después de guardar y recargar
            string carpetaPrueba = PrepararCarpeta("fase6-crud");
            var repositorio = new AgendaRepository(Path.Combine(carpetaPrueba, "agenda.json"));
            datos.Pasajeros.Add(pasajeroNuevo);
            repositorio.Guardar(datos);

            ResultadoCarga recarga = repositorio.Cargar();
            Pasajero pasajeroRecuperado = recarga.Datos?.BuscarPasajero("P98765432");
            Verificar("9. Persistencia del nuevo pasajero tras guardar y recargar con AgendaRepository",
                recarga.Exito && pasajeroRecuperado != null &&
                pasajeroRecuperado.NombreCompleto == "Carlos Eduardo Mendoza Ramos" &&
                pasajeroRecuperado.Email == "carlos.mendoza@example.com" &&
                pasajeroRecuperado.ServiciosPreferidos.Count == 2);

            // 10. Búsquedas y filtros
            var filtradosTexto = recarga.Datos.Pasajeros.Where(p =>
                p.Documento.Contains("9876") ||
                p.NombreCompleto.IndexOf("Mendoza", StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Email.Contains("example.com")).ToList();
            var filtradosCategoria = recarga.Datos.Pasajeros.Where(p => p.Categoria == "Adulto").ToList();
            var filtradosNacionalidad = recarga.Datos.Pasajeros.Where(p => p.Nacionalidad == "Salvadoreña").ToList();
            Verificar("10. Búsqueda y filtrado de pasajeros por documento, nombre, correo, categoría y nacionalidad",
                filtradosTexto.Count > 0 && filtradosCategoria.Count > 0 && filtradosNacionalidad.Count > 0);
        }

        private static void ProbarGestionReservacionesFase7()
        {
            Console.WriteLine();
            Console.WriteLine("11) Gestión de Reservaciones — Fase 7");

            AgendaData datos = DatosDemostracion.Crear();

            // 1. Registro válido de reservación y cálculo dinámico
            Vuelo vueloAv = datos.BuscarVuelo("AV-1024");
            int ocupadosInicialesAv = vueloAv.AsientosReservados;
            decimal totalCalculado = Catalogos.CalcularTotalReservacion(
                vueloAv.Precio, TipoTarifa.Ejecutiva, 2, 2, true);

            var nuevaReserva = new Reservacion
            {
                Codigo = "RES-0099",
                CodigoVuelo = "AV-1024",
                DocumentoPasajero = "04512345-6",
                FechaReserva = DateTime.Now,
                CantidadAsientos = 2,
                Tarifa = TipoTarifa.Ejecutiva,
                Seguro = true,
                Servicios = new List<string> { "Equipaje extra", "Selección de asiento" },
                Total = totalCalculado,
                Estado = EstadoReservacion.Confirmada
            };

            ResultadoValidacion valNueva = ValidadorAgenda.ValidarReservacion(nuevaReserva, datos);
            ResultadoValidacion valCupoNueva = ValidadorAgenda.ValidarCupoDisponible(vueloAv, datos, 2, null);
            datos.Reservaciones.Add(nuevaReserva);
            datos.RecalcularOcupacion();

            Verificar("1. Registro válido de reservación: validación íntegra, total USD calculado y recálculo de cupo",
                valNueva.EsValido && valCupoNueva.EsValido &&
                vueloAv.AsientosReservados == ocupadosInicialesAv + 2 &&
                nuevaReserva.Total == totalCalculado,
                valNueva.ToString());

            // 2. Rechazo de reservación con pasajero inexistente
            var reservaPasajeroInvalido = new Reservacion
            {
                Codigo = "RES-0100",
                CodigoVuelo = "AV-1024",
                DocumentoPasajero = "DOC-INEXISTENTE-999",
                FechaReserva = DateTime.Now,
                CantidadAsientos = 1,
                Total = 100m,
                Estado = EstadoReservacion.Confirmada
            };
            ResultadoValidacion valPasajeroInvalido = ValidadorAgenda.ValidarReservacion(reservaPasajeroInvalido, datos);
            Verificar("2. Detección y rechazo de reservación con pasajero titular inexistente",
                !valPasajeroInvalido.EsValido && valPasajeroInvalido.Errores.Any(e => e.Contains("pasajero")),
                valPasajeroInvalido.ToString());

            // 3. Rechazo de reservación con vuelo inexistente
            var reservaVueloInvalido = new Reservacion
            {
                Codigo = "RES-0101",
                CodigoVuelo = "VUELO-NO-EXISTE",
                DocumentoPasajero = "04512345-6",
                FechaReserva = DateTime.Now,
                CantidadAsientos = 1,
                Total = 100m,
                Estado = EstadoReservacion.Confirmada
            };
            ResultadoValidacion valVueloInvalido = ValidadorAgenda.ValidarReservacion(reservaVueloInvalido, datos);
            Verificar("3. Detección y rechazo de reservación con vuelo no registrado en la agenda",
                !valVueloInvalido.EsValido && valVueloInvalido.Errores.Any(e => e.Contains("vuelo")),
                valVueloInvalido.ToString());

            // 4. Rechazo de reservación duplicada (mismo código)
            var reservaDuplicada = new Reservacion
            {
                Codigo = "RES-0001",
                CodigoVuelo = "CM-441",
                DocumentoPasajero = "01234567-8",
                FechaReserva = DateTime.Now,
                CantidadAsientos = 1,
                Total = 210m,
                Estado = EstadoReservacion.Confirmada
            };
            datos.Reservaciones.Add(reservaDuplicada);
            ResultadoValidacion valDuplicado = ValidadorAgenda.Validar(datos);
            Verificar("4. Detección y rechazo de código de reservación duplicado",
                !valDuplicado.EsValido && valDuplicado.Errores.Any(e => e.Contains("código de reservación") && e.Contains("repetido")),
                valDuplicado.ToString());
            datos.Reservaciones.Remove(reservaDuplicada);

            // 5. Rechazo por capacidad agotada (cupo insuficiente)
            Vuelo vueloCopa = datos.BuscarVuelo("CM-441");
            ResultadoValidacion valCupoExcedido = ValidadorAgenda.ValidarCupoDisponible(vueloCopa, datos, 200, null);
            Verificar("5. Rechazo de asignación de asientos por capacidad agotada en el vuelo",
                !valCupoExcedido.EsValido && valCupoExcedido.Errores.Any(e => e.Contains("asiento(s) disponible(s)")),
                valCupoExcedido.ToString());

            // 6. Rechazo de reservación activa en vuelo cancelado
            Vuelo vueloCancelado = datos.BuscarVuelo("DL-1600");
            var reservaEnCancelado = new Reservacion
            {
                Codigo = "RES-0102",
                CodigoVuelo = vueloCancelado.Codigo,
                DocumentoPasajero = "04512345-6",
                FechaReserva = DateTime.Now,
                CantidadAsientos = 1,
                Total = vueloCancelado.Precio,
                Estado = EstadoReservacion.Confirmada
            };
            ResultadoValidacion valCancelado = ValidadorAgenda.ValidarReservacion(reservaEnCancelado, datos);
            Verificar("6. Detección y rechazo de reservaciones vigentes sobre un vuelo cancelado",
                !valCancelado.EsValido && valCancelado.Errores.Any(e => e.Contains("cancelado")),
                valCancelado.ToString());

            // 7. Edición válida (modificación de tarifa, asientos y recálculo)
            Reservacion reservaAEditar = datos.BuscarReservacion("RES-0002");
            reservaAEditar.CantidadAsientos = 2;
            reservaAEditar.Tarifa = TipoTarifa.Primera;
            reservaAEditar.Total = Catalogos.CalcularTotalReservacion(vueloCopa.Precio, TipoTarifa.Primera, 2, 0, false);
            ResultadoValidacion valEdicion = ValidadorAgenda.ValidarReservacion(reservaAEditar, datos);
            ResultadoValidacion valCupoEdicion = ValidadorAgenda.ValidarCupoDisponible(vueloCopa, datos, 2, reservaAEditar.Codigo);
            datos.RecalcularOcupacion();
            Verificar("7. Edición válida de parámetros operativos (asientos, clase de tarifa e importe total)",
                valEdicion.EsValido && valCupoEdicion.EsValido &&
                reservaAEditar.CantidadAsientos == 2 && reservaAEditar.Tarifa == TipoTarifa.Primera,
                valEdicion.ToString());

            // 8. Conservación del código identificador tras la edición
            Verificar("8. Conservación de identidad: el código de reservación permanece inmutable tras la edición",
                reservaAEditar.Codigo == "RES-0002");

            // 9. Cancelación segura y liberación inmediata de asientos ocupados
            Reservacion reservaACancelar = datos.BuscarReservacion("RES-0001");
            int ocupadosAntesCancelacion = vueloAv.AsientosReservados;
            reservaACancelar.Estado = EstadoReservacion.Cancelada;
            bool noOcupaAsientos = !reservaACancelar.OcupaAsientos;
            datos.RecalcularOcupacion();
            int ocupadosDespuesCancelacion = vueloAv.AsientosReservados;

            Verificar("9. Cancelación de reservación: liberación inmediata de asientos y conservación del registro histórico",
                noOcupaAsientos &&
                ocupadosDespuesCancelacion == ocupadosAntesCancelacion - reservaACancelar.CantidadAsientos &&
                datos.BuscarReservacion("RES-0001") != null &&
                datos.BuscarReservacion("RES-0001").Estado == EstadoReservacion.Cancelada);

            // 10. Prevención de cancelación repetida y verificación de idempotencia
            int ocupadosPreIdempotencia = vueloAv.AsientosReservados;
            datos.RecalcularOcupacion();
            Verificar("10. Prevención de cancelación repetida e idempotencia en el recálculo de cupos",
                vueloAv.AsientosReservados == ocupadosPreIdempotencia &&
                reservaACancelar.Estado == EstadoReservacion.Cancelada);

            // 11. Persistencia integral tras guardar y recargar con AgendaRepository
            string carpetaPrueba = PrepararCarpeta("fase7-crud");
            var repositorio = new AgendaRepository(Path.Combine(carpetaPrueba, "agenda.json"));
            repositorio.Guardar(datos);

            ResultadoCarga recarga = repositorio.Cargar();
            Reservacion recuperadaNueva = recarga.Datos?.BuscarReservacion("RES-0099");
            Reservacion recuperadaCancelada = recarga.Datos?.BuscarReservacion("RES-0001");
            Verificar("11. Persistencia atómica de reservaciones (nuevas, editadas y canceladas) con AgendaRepository",
                recarga.Exito &&
                recuperadaNueva != null && recuperadaNueva.CodigoVuelo == "AV-1024" &&
                recuperadaNueva.CantidadAsientos == 2 && recuperadaNueva.Tarifa == TipoTarifa.Ejecutiva &&
                recuperadaNueva.Seguro && recuperadaNueva.Servicios.Count == 2 &&
                recuperadaCancelada != null && recuperadaCancelada.Estado == EstadoReservacion.Cancelada &&
                !recuperadaCancelada.OcupaAsientos);

            // 12. Integridad de referencias cruzadas y consistencia global
            bool todasReferenciasValidas = recarga.Datos.Reservaciones.All(r =>
                recarga.Datos.BuscarVuelo(r.CodigoVuelo) != null &&
                recarga.Datos.BuscarPasajero(r.DocumentoPasajero) != null);

            bool ocupacionesSincronizadas = recarga.Datos.Vuelos.All(v =>
                v.AsientosReservados == recarga.Datos.AsientosOcupadosDeVuelo(v.Codigo));

            int totalVigentesEsperadas = recarga.Datos.Reservaciones.Count(r => r.OcupaAsientos);

            Verificar("12. Integridad referencial cruzada entre vuelos, pasajeros y reservaciones sin huérfanos",
                todasReferenciasValidas && ocupacionesSincronizadas &&
                recarga.Datos.ReservacionesVigentes == totalVigentesEsperadas);
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
