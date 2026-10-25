using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using AgendaDeViajes.Data;

namespace AgendaDeViajes.Services
{
    /// <summary>
    /// Resultado de una operación de carga de la agenda.
    /// </summary>
    public sealed class ResultadoCarga
    {
        private ResultadoCarga()
        {
        }

        public bool Exito { get; private set; }

        /// <summary>El archivo no existía todavía (primer arranque).</summary>
        public bool ArchivoNuevo { get; private set; }

        /// <summary>El archivo existía pero su contenido no pudo interpretarse.</summary>
        public bool ArchivoDanado { get; private set; }

        public string Mensaje { get; private set; } = string.Empty;

        public Exception Error { get; private set; }

        public AgendaData Datos { get; private set; }

        public static ResultadoCarga Correcto(AgendaData datos)
        {
            return new ResultadoCarga
            {
                Exito = true,
                Datos = datos,
                Mensaje = "Agenda cargada correctamente."
            };
        }

        public static ResultadoCarga SinArchivo()
        {
            return new ResultadoCarga
            {
                Exito = false,
                ArchivoNuevo = true,
                Mensaje = "Todavía no existe un archivo de agenda."
            };
        }

        public static ResultadoCarga Fallido(string mensaje, Exception error, bool archivoDanado)
        {
            return new ResultadoCarga
            {
                Exito = false,
                ArchivoDanado = archivoDanado,
                Mensaje = mensaje,
                Error = error
            };
        }
    }

    /// <summary>
    /// Persistencia local de la agenda en formato JSON.
    ///
    /// Reglas de seguridad aplicadas:
    /// - La escritura es atómica: primero se genera un archivo temporal y solo
    ///   después se reemplaza el archivo definitivo, de modo que un error de
    ///   escritura no destruye la información anterior.
    /// - Antes de reemplazar el archivo se conserva una copia en "agenda.bak.json".
    /// - Si el archivo existente está dañado, no se sobrescribe automáticamente:
    ///   es necesario indicarlo de forma explícita.
    /// </summary>
    public sealed class AgendaRepository
    {
        public const string NombreArchivo = "agenda.json";
        public const string NombreRespaldo = "agenda.bak.json";
        public const string NombreTemporal = "agenda.tmp";

        private static readonly JsonSerializerOptions Opciones = CrearOpcionesDeSerializacion();

        public AgendaRepository()
            : this(null)
        {
        }

        public AgendaRepository(string rutaArchivo)
        {
            RutaArchivo = string.IsNullOrWhiteSpace(rutaArchivo)
                ? ResolverRutaPredeterminada()
                : Path.GetFullPath(rutaArchivo);

            RutaCarpetaDatos = Path.GetDirectoryName(RutaArchivo);

            if (string.IsNullOrWhiteSpace(RutaCarpetaDatos))
            {
                RutaCarpetaDatos = Directory.GetCurrentDirectory();
            }

            Directory.CreateDirectory(RutaCarpetaDatos);

            string nombreArchivo = Path.GetFileName(RutaArchivo);
            string nombreBase = Path.GetFileNameWithoutExtension(nombreArchivo);

            RutaArchivo = Path.Combine(RutaCarpetaDatos, nombreArchivo);
            RutaRespaldo = Path.Combine(RutaCarpetaDatos, nombreBase + ".bak.json");
            RutaTemporal = Path.Combine(RutaCarpetaDatos, nombreBase + ".tmp");
        }

        public string RutaCarpetaDatos { get; }

        public string RutaArchivo { get; }

        public string RutaRespaldo { get; }

        public string RutaTemporal { get; }

        /// <summary>
        /// Se activa cuando el archivo existente no pudo interpretarse. Mientras
        /// esté activo, Guardar rechaza sobrescribir los datos del usuario.
        /// </summary>
        public bool DatosDanados { get; private set; }

        public bool ExisteArchivo
        {
            get { return File.Exists(RutaArchivo); }
        }

        /// <summary>
        /// Crea un repositorio cuya carpeta de datos está junto al ejecutable.
        /// Si esa ubicación no admite escritura, se utiliza la carpeta local del usuario.
        /// </summary>
        public static AgendaRepository CrearPredeterminado()
        {
            return new AgendaRepository(null);
        }

        private static string ResolverRutaPredeterminada()
        {
            string rutaJuntoAlEjecutable = Path.Combine(AppContext.BaseDirectory, "Data", NombreArchivo);

            try
            {
                string carpeta = Path.GetDirectoryName(rutaJuntoAlEjecutable);
                Directory.CreateDirectory(carpeta);
                return rutaJuntoAlEjecutable;
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException || ex is NotSupportedException)
            {
                string carpetaUsuario = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "AgendaDeViajes",
                    "Data");

                Directory.CreateDirectory(carpetaUsuario);
                return Path.Combine(carpetaUsuario, NombreArchivo);
            }
        }

        private static JsonSerializerOptions CrearOpcionesDeSerializacion()
        {
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            opciones.Converters.Add(new JsonStringEnumConverter());
            return opciones;
        }

        /// <summary>
        /// Lee la agenda desde disco. No lanza excepciones: informa el problema
        /// mediante el objeto <see cref="ResultadoCarga"/>.
        /// </summary>
        public ResultadoCarga Cargar()
        {
            if (!ExisteArchivo)
            {
                DatosDanados = false;
                return ResultadoCarga.SinArchivo();
            }

            string contenido;

            try
            {
                contenido = File.ReadAllText(RutaArchivo, Encoding.UTF8);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                return ResultadoCarga.Fallido(
                    "No fue posible leer el archivo de la agenda. Los datos existentes no se modificarán.",
                    ex,
                    false);
            }

            if (string.IsNullOrWhiteSpace(contenido))
            {
                DatosDanados = true;
                return ResultadoCarga.Fallido(
                    "El archivo de la agenda está vacío o no contiene información válida.",
                    null,
                    true);
            }

            try
            {
                AgendaData datos = JsonSerializer.Deserialize<AgendaData>(contenido, Opciones);

                if (datos == null)
                {
                    DatosDanados = true;
                    return ResultadoCarga.Fallido(
                        "El archivo de la agenda no pudo interpretarse como una agenda válida.",
                        null,
                        true);
                }

                datos.Normalizar();
                DatosDanados = false;
                return ResultadoCarga.Correcto(datos);
            }
            catch (JsonException ex)
            {
                DatosDanados = true;
                return ResultadoCarga.Fallido(
                    "El archivo de la agenda está dañado y no se leyó. Se conservará intacto hasta que se decida reemplazarlo.",
                    ex,
                    true);
            }
        }

        /// <summary>
        /// Guarda la agenda en disco de forma segura. Valida la integridad de los
        /// datos antes de escribir y protege los archivos existentes.
        /// </summary>
        /// <param name="datos">Información a guardar.</param>
        /// <param name="permitirSobrescribirArchivoDanado">
        /// Debe ser true para reemplazar un archivo que no pudo leerse. El contenido
        /// anterior queda en el archivo de respaldo.
        /// </param>
        public void Guardar(AgendaData datos, bool permitirSobrescribirArchivoDanado = false)
        {
            if (datos == null)
            {
                throw new ArgumentNullException(nameof(datos));
            }

            if (DatosDanados && !permitirSobrescribirArchivoDanado)
            {
                throw new InvalidOperationException(
                    "El archivo de la agenda está dañado. La operación se canceló para no perder información. "
                    + "Revisa el archivo o vuelve a intentarlo permitiendo la sobrescritura explícita.");
            }

            ResultadoValidacion validacion = ValidadorAgenda.Validar(datos);

            if (!validacion.EsValido)
            {
                throw new InvalidOperationException(
                    "No se guardaron los datos porque no cumplen las reglas de integridad: "
                    + string.Join(" | ", validacion.Errores));
            }

            Directory.CreateDirectory(RutaCarpetaDatos);

            datos.UltimaActualizacion = DateTime.Now;

            try
            {
                string json = JsonSerializer.Serialize(datos, Opciones);
                File.WriteAllText(RutaTemporal, json, new UTF8Encoding(false));

                if (File.Exists(RutaArchivo))
                {
                    File.Replace(RutaTemporal, RutaArchivo, RutaRespaldo, true);
                }
                else
                {
                    File.Move(RutaTemporal, RutaArchivo);
                }

                DatosDanados = false;
            }
            catch (Exception ex)
            {
                EliminarTemporal();

                if (ex is IOException || ex is UnauthorizedAccessException)
                {
                    throw new IOException(
                        "No fue posible guardar la agenda. Los datos anteriores permanecen intactos.",
                        ex);
                }

                throw;
            }
        }

        /// <summary>
        /// Elimina el archivo temporal que pudo quedar tras un error de escritura.
        /// </summary>
        private void EliminarTemporal()
        {
            try
            {
                if (File.Exists(RutaTemporal))
                {
                    File.Delete(RutaTemporal);
                }
            }
            catch (IOException)
            {
                // El archivo temporal se limpiará en la próxima escritura.
            }
            catch (UnauthorizedAccessException)
            {
                // Sin permisos para limpiar; no afecta la integridad del archivo principal.
            }
        }
    }
}
