using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AgendaDeViajes.Services
{
    /// <summary>
    /// Servicio de exportación tabular a formato CSV (Comma-Separated Values).
    /// Implementa codificación UTF-8 con BOM para visualización correcta en Microsoft Excel,
    /// escape conforme al estándar RFC 4180 y protección activa contra inyección de fórmulas (CSV Injection).
    /// </summary>
    public static class ExportadorCsv
    {
        /// <summary>
        /// Escapa y sanea un valor de celda individual, previniendo inyección de fórmulas
        /// si el campo comienza con caracteres ejecutables (=, +, -, @, tab, retorno).
        /// </summary>
        public static string EscaparCampo(string campo)
        {
            if (string.IsNullOrEmpty(campo))
            {
                return string.Empty;
            }

            string valor = campo;

            // Mitigación de inyección de fórmulas en hojas de cálculo
            char primerCar = valor[0];
            if (primerCar == '=' || primerCar == '+' || primerCar == '-' ||
                primerCar == '@' || primerCar == '\t' || primerCar == '\r')
            {
                valor = "'" + valor;
            }

            bool requiereComillas = valor.Contains(",") ||
                                   valor.Contains(";") ||
                                   valor.Contains("\"") ||
                                   valor.Contains("\r") ||
                                   valor.Contains("\n") ||
                                   valor.StartsWith(" ") ||
                                   valor.EndsWith(" ");

            if (requiereComillas)
            {
                valor = "\"" + valor.Replace("\"", "\"\"") + "\"";
            }

            return valor;
        }

        /// <summary>
        /// Construye el texto completo del archivo CSV a partir de encabezados, registros y resumen métrico.
        /// </summary>
        public static string GenerarContenidoCsv(
            string tituloReporte,
            IEnumerable<string> encabezados,
            IEnumerable<IEnumerable<string>> filas,
            Dictionary<string, string> resumenMetricas = null)
        {
            var sb = new StringBuilder();

            // Encabezados tabulares
            if (encabezados != null)
            {
                sb.AppendLine(string.Join(",", encabezados.Select(EscaparCampo)));
            }

            // Filas de datos
            if (filas != null)
            {
                foreach (var fila in filas)
                {
                    sb.AppendLine(string.Join(",", fila.Select(EscaparCampo)));
                }
            }

            // Bloque final de indicadores y resumen métrico
            if (resumenMetricas != null && resumenMetricas.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine(EscaparCampo("--- RESUMEN DE INDICADORES ---"));
                foreach (var par in resumenMetricas)
                {
                    sb.AppendLine($"{EscaparCampo(par.Key)},{EscaparCampo(par.Value)}");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Escribe el archivo CSV en disco en la ruta especificada usando codificación UTF-8 con BOM.
        /// </summary>
        public static void GuardarCsv(
            string rutaArchivo,
            string tituloReporte,
            IEnumerable<string> encabezados,
            IEnumerable<IEnumerable<string>> filas,
            Dictionary<string, string> resumenMetricas = null)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                throw new ArgumentNullException(nameof(rutaArchivo));
            }

            string contenido = GenerarContenidoCsv(tituloReporte, encabezados, filas, resumenMetricas);

            // UTF-8 con BOM para compatibilidad plena con Microsoft Excel y LibreOffice Calc
            var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            File.WriteAllText(rutaArchivo, contenido, encoding);
        }
    }
}

