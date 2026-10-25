using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AgendaDeViajes.Services
{
    /// <summary>
    /// Generador nativo de documentos PDF (especificación ISO 32000-1 / PDF-1.4) en C# sin dependencias externas.
    /// Diseñado para emitir reportes ejecutivos en formato A4 apaisado (Landscape),
    /// con asignación inteligente del ancho de columnas para evitar partición de palabras o encabezados,
    /// saltos de línea estrictamente entre palabras completas, cálculo dinámico de altura de filas,
    /// cuadrícula estructurada de tarjetas de métricas y pies de página con numeración continua ("Página X de Y").
    /// </summary>
    public static class GeneradorPdfReporte
    {
        public const float AnchoA4Landscape = 841.89f; // 297 mm en puntos tipográficos
        public const float AltoA4Landscape = 595.28f;  // 210 mm en puntos tipográficos

        private const float MargenIzquierdo = 36f;
        private const float MargenDerecho = 36f;
        private const float MargenSuperior = 36f;
        private const float MargenInferior = 36f;

        private const float AltoFilaEncabezado = 24f;
        private const float MinAltoFilaDatos = 22f;
        private const float AltoLineaTexto = 11f;
        private const float PaddingVerticalCelda = 6f;
        private const float TamanoFuenteCeldas = 8f;
        private const float TamanoFuenteEncabezado = 8.5f;

        /// <summary>
        /// Genera el archivo PDF en disco en la ruta especificada.
        /// </summary>
        public static void GenerarPdf(
            string rutaArchivo,
            string tituloReporte,
            string subtituloFiltros,
            List<string> encabezados,
            List<float> anchosRelativos,
            List<List<string>> filas,
            Dictionary<string, string> resumenMetricas = null)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                throw new ArgumentNullException(nameof(rutaArchivo));
            }

            byte[] bytesPdf = GenerarPdfBytes(
                tituloReporte,
                subtituloFiltros,
                encabezados,
                anchosRelativos,
                filas,
                resumenMetricas);

            File.WriteAllBytes(rutaArchivo, bytesPdf);
        }

        /// <summary>
        /// Genera el contenido binario del documento PDF listo para guardar o transmitir.
        /// </summary>
        public static byte[] GenerarPdfBytes(
            string tituloReporte,
            string subtituloFiltros,
            List<string> encabezados,
            List<float> anchosRelativos,
            List<List<string>> filas,
            Dictionary<string, string> resumenMetricas = null)
        {
            encabezados = encabezados ?? new List<string>();
            filas = filas ?? new List<List<string>>();
            resumenMetricas = resumenMetricas ?? new Dictionary<string, string>();

            // 1. Calcular anchos inteligentes de columnas garantizando amplitud para encabezados y contenido clave
            float anchoTablaDisponible = AnchoA4Landscape - MargenIzquierdo - MargenDerecho;
            List<float> anchosColumnas = CalcularAnchosColumnas(encabezados, anchosRelativos, anchoTablaDisponible);

            // 2. Medir y precalcular alturas de todas las filas con soporte multilínea (palabras completas)
            var alturasFilas = new List<float>();
            var lineasPorFilaYColumna = new List<List<List<string>>>();

            foreach (var fila in filas)
            {
                var celdasLineas = new List<List<string>>();
                int maxLineas = 1;

                for (int c = 0; c < encabezados.Count; c++)
                {
                    float anchoCol = c < anchosColumnas.Count ? anchosColumnas[c] : (anchoTablaDisponible / Math.Max(1, encabezados.Count));
                    string valor = c < fila.Count ? fila[c] : string.Empty;
                    var lineas = DividirTextoEnLineas(valor, Math.Max(15f, anchoCol - 8f), TamanoFuenteCeldas);
                    celdasLineas.Add(lineas);
                    if (lineas.Count > maxLineas)
                    {
                        maxLineas = lineas.Count;
                    }
                }

                float altoCalculado = Math.Max(MinAltoFilaDatos, (maxLineas * AltoLineaTexto) + PaddingVerticalCelda);
                alturasFilas.Add(altoCalculado);
                lineasPorFilaYColumna.Add(celdasLineas);
            }

            // 3. Altura vertical utilizable para datos por página
            float yInicio = AltoA4Landscape - MargenSuperior - 65f; // Espacio ocupado por encabezado institucional
            float yFin = MargenInferior + 25f; // Espacio reservado para pie de página
            float alturaDisponiblePorPagina = yInicio - yFin - AltoFilaEncabezado;

            // 4. Calcular altura requerida para el bloque de resumen de métricas
            float altoBloqueResumen = CalcularAltoBloqueResumen(resumenMetricas, anchoTablaDisponible);

            // 5. Paginación dinámica según alturas reales
            var paginasFilasIndices = new List<List<int>>();
            var paginaActualIndices = new List<int>();
            float alturaAcumulada = 0f;

            for (int i = 0; i < filas.Count; i++)
            {
                float altoFila = alturasFilas[i];

                if (alturaAcumulada + altoFila > alturaDisponiblePorPagina && paginaActualIndices.Count > 0)
                {
                    paginasFilasIndices.Add(paginaActualIndices);
                    paginaActualIndices = new List<int>();
                    alturaAcumulada = 0f;
                }

                paginaActualIndices.Add(i);
                alturaAcumulada += altoFila;
            }

            if (paginaActualIndices.Count > 0 || filas.Count == 0)
            {
                paginasFilasIndices.Add(paginaActualIndices);
            }

            // Determinar en qué página se dibuja el resumen métrico
            int paginaResumen = paginasFilasIndices.Count;

            if (resumenMetricas.Count > 0)
            {
                if (alturaAcumulada + altoBloqueResumen > alturaDisponiblePorPagina && filas.Count > 0)
                {
                    // No cabe en la última página con datos: se agrega página dedicada para el resumen
                    paginasFilasIndices.Add(new List<int>());
                    paginaResumen = paginasFilasIndices.Count;
                }
            }

            int totalPaginas = paginasFilasIndices.Count;

            // 6. Construir flujos de contenido PDF para cada página
            var flujosPaginas = new List<string>();
            for (int numPag = 1; numPag <= totalPaginas; numPag++)
            {
                var indicesFilaDeEstaPagina = paginasFilasIndices[numPag - 1];
                bool dibujarResumenAqui = numPag == paginaResumen && resumenMetricas.Count > 0;

                string contenidoPagina = ConstruirPaginaPdf(
                    numPag,
                    totalPaginas,
                    tituloReporte,
                    subtituloFiltros,
                    encabezados,
                    anchosColumnas,
                    indicesFilaDeEstaPagina,
                    lineasPorFilaYColumna,
                    alturasFilas,
                    dibujarResumenAqui ? resumenMetricas : null,
                    anchoTablaDisponible,
                    altoBloqueResumen);

                flujosPaginas.Add(contenidoPagina);
            }

            return EnsamblarEstructuraPdf(flujosPaginas);
        }

        /// <summary>
        /// Calcula anchos de columnas distribuidos de forma inteligente para que
        /// encabezados y contenido esencial nunca queden fragmentados.
        /// </summary>
        private static List<float> CalcularAnchosColumnas(
            List<string> encabezados,
            List<float> anchosRelativos,
            float anchoTotal)
        {
            var resultado = new List<float>();
            int cantidad = encabezados?.Count ?? 0;
            if (cantidad <= 0)
            {
                return resultado;
            }

            // Pesos iniciales
            var pesos = (anchosRelativos != null && anchosRelativos.Count == cantidad)
                ? new List<float>(anchosRelativos)
                : Enumerable.Repeat(1f, cantidad).ToList();

            // Calcular anchos mínimos para cada columna
            var anchosMinimos = new float[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                string h = (encabezados[i] ?? string.Empty).Trim();
                float anchoTexto = MedirAnchoTexto(h, TamanoFuenteEncabezado);

                // Palabra más larga del encabezado
                float maxPalabra = 0f;
                foreach (string w in h.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    float pw = MedirAnchoTexto(w, TamanoFuenteEncabezado);
                    if (pw > maxPalabra) maxPalabra = pw;
                }

                float minBase = h.Contains(' ')
                    ? Math.Max(maxPalabra + 16f, 48f)
                    : Math.Max(anchoTexto + 16f, 52f);

                string hLower = h.ToLowerInvariant();
                if (hLower.Contains("capacidad") || hLower.Contains("reservado") || hLower.Contains("disponible"))
                {
                    minBase = Math.Max(minBase, 64f);
                    if (pesos[i] < 1.05f) pesos[i] = 1.05f;
                }
                else if (hLower.Contains("origen") || hLower.Contains("destino"))
                {
                    minBase = Math.Max(minBase, 84f);
                    if (pesos[i] < 1.4f) pesos[i] = 1.4f;
                }
                else if (hLower.Contains("aerolínea") || hLower.Contains("aerolinea"))
                {
                    minBase = Math.Max(minBase, 80f);
                    if (pesos[i] < 1.35f) pesos[i] = 1.35f;
                }
                else if (hLower.Contains("código") || hLower.Contains("codigo"))
                {
                    minBase = Math.Max(minBase, 50f);
                }

                anchosMinimos[i] = minBase;
            }

            float sumaPesos = pesos.Sum();
            if (sumaPesos <= 0) sumaPesos = 1f;

            float sumaMinimos = anchosMinimos.Sum();
            if (sumaMinimos <= anchoTotal)
            {
                float remanente = anchoTotal - sumaMinimos;
                for (int i = 0; i < cantidad; i++)
                {
                    float extra = (pesos[i] / sumaPesos) * remanente;
                    resultado.Add(anchosMinimos[i] + extra);
                }
            }
            else
            {
                for (int i = 0; i < cantidad; i++)
                {
                    resultado.Add((pesos[i] / sumaPesos) * anchoTotal);
                }
            }

            return resultado;
        }

        /// <summary>
        /// Divide un texto en múltiples líneas según el ancho máximo disponible en puntos para Helvetica.
        /// Garantiza que los saltos de línea ocurran únicamente entre palabras completas (sin partir palabras a la mitad).
        /// </summary>
        public static List<string> DividirTextoEnLineas(string texto, float anchoMaximo, float tamanoFuente)
        {
            var lineas = new List<string>();
            if (string.IsNullOrEmpty(texto))
            {
                lineas.Add(string.Empty);
                return lineas;
            }

            string[] parrafos = texto.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
            foreach (string parrafo in parrafos)
            {
                if (string.IsNullOrWhiteSpace(parrafo))
                {
                    lineas.Add(string.Empty);
                    continue;
                }

                string[] palabras = parrafo.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var lineaActual = new StringBuilder();

                foreach (string palabra in palabras)
                {
                    string candidata = lineaActual.Length == 0 ? palabra : $"{lineaActual} {palabra}";
                    if (MedirAnchoTexto(candidata, tamanoFuente) <= anchoMaximo)
                    {
                        if (lineaActual.Length > 0)
                        {
                            lineaActual.Append(' ');
                        }
                        lineaActual.Append(palabra);
                    }
                    else
                    {
                        // La palabra actual no cabe en la línea en curso.
                        // Se guarda la línea actual y la palabra pasa a la siguiente línea completa.
                        if (lineaActual.Length > 0)
                        {
                            lineas.Add(lineaActual.ToString());
                            lineaActual.Clear();
                        }

                        // Si la palabra sola excede el ancho y posee guion o barra natural, dividir por el separador
                        if (MedirAnchoTexto(palabra, tamanoFuente) > anchoMaximo && (palabra.Contains('-') || palabra.Contains('/')))
                        {
                            char separador = palabra.Contains('-') ? '-' : '/';
                            string[] subpartes = palabra.Split(separador);
                            for (int sp = 0; sp < subpartes.Length; sp++)
                            {
                                string parteConSep = sp < subpartes.Length - 1 ? subpartes[sp] + separador : subpartes[sp];
                                if (lineaActual.Length > 0 && MedirAnchoTexto($"{lineaActual} {parteConSep}", tamanoFuente) > anchoMaximo)
                                {
                                    lineas.Add(lineaActual.ToString());
                                    lineaActual.Clear();
                                }
                                if (lineaActual.Length > 0) lineaActual.Append(' ');
                                lineaActual.Append(parteConSep);
                            }
                        }
                        else
                        {
                            // Conservar la palabra completa en su propia línea (sin mutilar caracteres)
                            lineaActual.Append(palabra);
                        }
                    }
                }

                if (lineaActual.Length > 0)
                {
                    lineas.Add(lineaActual.ToString());
                }
            }

            if (lineas.Count == 0)
            {
                lineas.Add(string.Empty);
            }

            return lineas;
        }

        /// <summary>
        /// Estima con alta precisión métrica el ancho tipográfico de un texto en fuente Helvetica.
        /// </summary>
        public static float MedirAnchoTexto(string texto, float tamanoFuente)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return 0f;
            }

            float unidades = 0f;
            foreach (char c in texto)
            {
                if (c >= '0' && c <= '9') unidades += 556f;
                else if (c == ' ' || c == '.' || c == ',' || c == ':' || c == ';' || c == '-' || c == '/' || c == '(' || c == ')') unidades += 278f;
                else if (c == 'i' || c == 'j' || c == 'l' || c == 'I') unidades += 222f;
                else if (c == 'f' || c == 't' || c == 'r') unidades += 333f;
                else if (c == 'm' || c == 'w' || c == 'M' || c == 'W' || c == '@' || c == '%') unidades += 850f;
                else if (char.IsUpper(c)) unidades += 667f;
                else unidades += 500f;
            }

            return (unidades / 1000f) * tamanoFuente;
        }

        private static float CalcularAltoBloqueResumen(Dictionary<string, string> resumenMetricas, float anchoDisponible)
        {
            if (resumenMetricas == null || resumenMetricas.Count == 0)
            {
                return 0f;
            }

            int total = resumenMetricas.Count;
            int cols = total <= 4 ? Math.Max(1, total) : (total <= 6 ? 3 : 4);
            int filas = (int)Math.Ceiling((double)total / cols);

            float altoTarjeta = 40f;
            float gapY = 8f;
            // 28pt cabecera resumen + filas * tarjeta + gaps + margen inferior
            return 28f + (filas * altoTarjeta) + ((filas - 1) * gapY) + 12f;
        }

        private static string ConstruirPaginaPdf(
            int numeroPagina,
            int totalPaginas,
            string tituloReporte,
            string subtituloFiltros,
            List<string> encabezados,
            List<float> anchosColumnas,
            List<int> indicesFilas,
            List<List<List<string>>> lineasPorFilaYColumna,
            List<float> alturasFilas,
            Dictionary<string, string> resumenMetricas,
            float anchoTabla,
            float altoBloqueResumen)
        {
            var sb = new StringBuilder();

            // 1. Franja decorativa superior azul acento corporativo (#2D9CDB)
            sb.AppendLine("0.176 0.612 0.859 rg"); // #2D9CDB
            sb.AppendLine($"{MargenIzquierdo} {AltoA4Landscape - MargenSuperior} {anchoTabla} 3.5 re f");

            // 2. Encabezado institucional: Logotipo textual y nombre
            sb.AppendLine("BT");
            sb.AppendLine("/F2 13 Tf");
            sb.AppendLine("0.086 0.133 0.184 rg"); // #16222F
            sb.AppendLine($"1 0 0 1 {MargenIzquierdo} {AltoA4Landscape - MargenSuperior - 18} Tm");
            sb.AppendLine($"(AGENDA DE VIAJES  -  SISTEMA DE GESTION AEREA) Tj");
            sb.AppendLine("ET");

            // Título del informe
            sb.AppendLine("BT");
            sb.AppendLine("/F2 11 Tf");
            sb.AppendLine("0.118 0.165 0.220 rg"); // #1E2A38
            sb.AppendLine($"1 0 0 1 {MargenIzquierdo} {AltoA4Landscape - MargenSuperior - 34} Tm");
            sb.AppendLine($"({EscaparTextoPdf(tituloReporte)}) Tj");
            sb.AppendLine("ET");

            // Metadatos: fecha de corte y filtros activos
            string fechaTexto = $"Emision: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | {subtituloFiltros}";
            sb.AppendLine("BT");
            sb.AppendLine("/F1 8 Tf");
            sb.AppendLine("0.419 0.478 0.549 rg"); // #6B7A8C
            sb.AppendLine($"1 0 0 1 {MargenIzquierdo} {AltoA4Landscape - MargenSuperior - 48} Tm");
            sb.AppendLine($"({EscaparTextoPdf(fechaTexto)}) Tj");
            sb.AppendLine("ET");

            // Línea separadora tenue
            sb.AppendLine("0.886 0.910 0.941 RG"); // #E2E8F0
            sb.AppendLine("0.75 w");
            sb.AppendLine($"{MargenIzquierdo} {AltoA4Landscape - MargenSuperior - 54} m {AnchoA4Landscape - MargenDerecho} {AltoA4Landscape - MargenSuperior - 54} l S");

            float yActual = AltoA4Landscape - MargenSuperior - 60f;
            float xInicio = MargenIzquierdo;

            // 3. Encabezados de tabla (se imprimen en todas las páginas que contengan datos o si es reporte vacío)
            if (encabezados.Count > 0 && (indicesFilas.Count > 0 || lineasPorFilaYColumna.Count == 0))
            {
                yActual -= AltoFilaEncabezado;

                // Fondo del encabezado de tabla (#16222F)
                sb.AppendLine("0.086 0.133 0.184 rg");
                sb.AppendLine($"{xInicio} {yActual} {anchoTabla} {AltoFilaEncabezado} re f");

                // Textos del encabezado
                float xCol = xInicio;
                for (int c = 0; c < encabezados.Count; c++)
                {
                    float anchoCol = anchosColumnas[c];
                    string headerText = encabezados[c];

                    sb.AppendLine("BT");
                    sb.AppendLine("/F2 8.5 Tf");
                    sb.AppendLine("1 1 1 rg"); // Blanco
                    sb.AppendLine($"1 0 0 1 {xCol + 5} {yActual + 8} Tm");
                    sb.AppendLine($"({EscaparTextoPdf(headerText)}) Tj");
                    sb.AppendLine("ET");

                    xCol += anchoCol;
                }

                // 4. Filas de datos para esta página
                for (int r = 0; r < indicesFilas.Count; r++)
                {
                    int filaOriginalIdx = indicesFilas[r];
                    float altoFila = alturasFilas[filaOriginalIdx];
                    yActual -= altoFila;

                    // Fondo alternado (zebra)
                    if (r % 2 == 1)
                    {
                        sb.AppendLine("0.973 0.980 0.988 rg"); // #F8FAFC
                        sb.AppendLine($"{xInicio} {yActual} {anchoTabla} {altoFila} re f");
                    }

                    // Borde inferior sutil
                    sb.AppendLine("0.925 0.941 0.957 RG");
                    sb.AppendLine("0.5 w");
                    sb.AppendLine($"{xInicio} {yActual} m {xInicio + anchoTabla} {yActual} l S");

                    var celdasLineas = lineasPorFilaYColumna[filaOriginalIdx];
                    xCol = xInicio;

                    for (int c = 0; c < encabezados.Count; c++)
                    {
                        float anchoCol = anchosColumnas[c];
                        var lineas = c < celdasLineas.Count ? celdasLineas[c] : new List<string>();

                        // Dibujar cada línea de texto dentro de la celda
                        for (int l = 0; l < lineas.Count; l++)
                        {
                            string linea = lineas[l];
                            float yTexto = yActual + altoFila - 13f - (l * AltoLineaTexto);

                            sb.AppendLine("BT");
                            sb.AppendLine("/F1 8 Tf");
                            sb.AppendLine("0.118 0.165 0.220 rg"); // #1E2A38
                            sb.AppendLine($"1 0 0 1 {xCol + 5} {yTexto} Tm");
                            sb.AppendLine($"({EscaparTextoPdf(linea)}) Tj");
                            sb.AppendLine("ET");
                        }

                        xCol += anchoCol;
                    }
                }

                if (indicesFilas.Count == 0 && lineasPorFilaYColumna.Count == 0)
                {
                    yActual -= MinAltoFilaDatos + 10;
                    sb.AppendLine("BT");
                    sb.AppendLine("/F1 8.5 Tf");
                    sb.AppendLine("0.419 0.478 0.549 rg");
                    sb.AppendLine($"1 0 0 1 {xInicio + 10} {yActual + 8} Tm");
                    sb.AppendLine($"(No se encontraron registros que coincidan con los criterios y filtros especificados.) Tj");
                    sb.AppendLine("ET");
                }
            }
            else if (indicesFilas.Count == 0 && resumenMetricas != null)
            {
                // Página dedicada para el resumen métrico
                yActual -= 10f;
            }

            // 5. Bloque de resumen e indicadores (Cuadrícula organizada de tarjetas)
            if (resumenMetricas != null && resumenMetricas.Count > 0)
            {
                yActual -= 16f;

                // Título del bloque de resumen
                sb.AppendLine("BT");
                sb.AppendLine("/F2 9.5 Tf");
                sb.AppendLine("0.086 0.133 0.184 rg"); // #16222F
                sb.AppendLine($"1 0 0 1 {xInicio} {yActual} Tm");
                sb.AppendLine($"(RESUMEN EJECUTIVO E INDICADORES CLAVE) Tj");
                sb.AppendLine("ET");

                yActual -= 8f;

                var metricasLista = resumenMetricas.ToList();
                int totalTarjetas = metricasLista.Count;
                int cols = totalTarjetas <= 4 ? Math.Max(1, totalTarjetas) : (totalTarjetas <= 6 ? 3 : 4);
                float gapX = 8f;
                float gapY = 8f;
                float altoTarjeta = 40f;
                float anchoTarjeta = (anchoTabla - ((cols - 1) * gapX)) / cols;

                for (int m = 0; m < totalTarjetas; m++)
                {
                    int filaTarjeta = m / cols;
                    int colTarjeta = m % cols;

                    float cardX = xInicio + (colTarjeta * (anchoTarjeta + gapX));
                    float cardY = yActual - ((filaTarjeta + 1) * altoTarjeta) - (filaTarjeta * gapY);

                    // Fondo de la tarjeta (#F8FAFC)
                    sb.AppendLine("0.973 0.980 0.988 rg");
                    sb.AppendLine($"{cardX} {cardY} {anchoTarjeta} {altoTarjeta} re f");

                    // Borde de la tarjeta (#E2E8F0)
                    sb.AppendLine("0.886 0.910 0.941 RG");
                    sb.AppendLine("0.75 w");
                    sb.AppendLine($"{cardX} {cardY} {anchoTarjeta} {altoTarjeta} re S");

                    // Franja de acento izquierda (#2D9CDB)
                    sb.AppendLine("0.176 0.612 0.859 rg");
                    sb.AppendLine($"{cardX} {cardY} 3.5 {altoTarjeta} re f");

                    var kvp = metricasLista[m];
                    string etiqueta = kvp.Key.ToUpperInvariant();
                    string valor = kvp.Value;

                    // Etiqueta del indicador (pequeña, gris corporativo, hasta 2 líneas si es necesario)
                    var lineasEtiqueta = DividirTextoEnLineas(etiqueta, anchoTarjeta - 14f, 6.5f);
                    for (int l = 0; l < lineasEtiqueta.Count && l < 2; l++)
                    {
                        float yLabel = cardY + altoTarjeta - 11f - (l * 8.5f);
                        sb.AppendLine("BT");
                        sb.AppendLine("/F2 6.5 Tf");
                        sb.AppendLine("0.419 0.478 0.549 rg"); // #6B7A8C
                        sb.AppendLine($"1 0 0 1 {cardX + 8} {yLabel} Tm");
                        sb.AppendLine($"({EscaparTextoPdf(lineasEtiqueta[l])}) Tj");
                        sb.AppendLine("ET");
                    }

                    // Valor del indicador (destacado, azul oscuro)
                    sb.AppendLine("BT");
                    sb.AppendLine("/F2 10 Tf");
                    sb.AppendLine("0.086 0.133 0.184 rg"); // #16222F
                    sb.AppendLine($"1 0 0 1 {cardX + 8} {cardY + 7} Tm");
                    sb.AppendLine($"({EscaparTextoPdf(valor)}) Tj");
                    sb.AppendLine("ET");
                }
            }

            // 6. Pie de página institucional
            float yPie = MargenInferior;
            sb.AppendLine("0.886 0.910 0.941 RG");
            sb.AppendLine("0.5 w");
            sb.AppendLine($"{MargenIzquierdo} {yPie + 14} m {AnchoA4Landscape - MargenDerecho} {yPie + 14} l S");

            // Crédito institucional a la izquierda
            sb.AppendLine("BT");
            sb.AppendLine("/F1 8 Tf");
            sb.AppendLine("0.419 0.478 0.549 rg");
            sb.AppendLine($"1 0 0 1 {MargenIzquierdo} {yPie + 4} Tm");
            sb.AppendLine($"(AGENDA DE VIAJES  -  By: Rodrigo Rivera - RF1631012025) Tj");
            sb.AppendLine("ET");

            // Numeración de páginas a la derecha ("Página X de Y")
            string paginacionTexto = $"Pagina {numeroPagina} de {totalPaginas}";
            sb.AppendLine("BT");
            sb.AppendLine("/F2 8 Tf");
            sb.AppendLine("0.176 0.612 0.859 rg"); // Azul acento
            sb.AppendLine($"1 0 0 1 {AnchoA4Landscape - MargenDerecho - 80} {yPie + 4} Tm");
            sb.AppendLine($"({EscaparTextoPdf(paginacionTexto)}) Tj");
            sb.AppendLine("ET");

            return sb.ToString();
        }

        private static byte[] EnsamblarEstructuraPdf(List<string> flujosPaginas)
        {
            using (var ms = new MemoryStream())
            {
                var offsets = new List<long>();

                void Escribir(string texto)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(texto);
                    ms.Write(bytes, 0, bytes.Length);
                }

                void EscribirBytes(byte[] bytes)
                {
                    ms.Write(bytes, 0, bytes.Length);
                }

                // Encabezado PDF 1.4 con comentario binario
                Escribir("%PDF-1.4\r\n%\xE2\xE3\xCF\xD3\r\n");

                int totalPaginas = flujosPaginas.Count;

                // Objeto 1: Catálogo principal
                offsets.Add(ms.Position);
                Escribir("1 0 obj\r\n<< /Type /Catalog /Pages 2 0 R >>\r\nendobj\r\n");

                // Objeto 2: Raíz del árbol de páginas
                offsets.Add(ms.Position);
                var listaIdsPaginas = new StringBuilder();
                for (int i = 0; i < totalPaginas; i++)
                {
                    int idPagina = 5 + (i * 2);
                    listaIdsPaginas.Append($"{idPagina} 0 R ");
                }
                Escribir($"2 0 obj\r\n<< /Type /Pages /Kids [ {listaIdsPaginas} ] /Count {totalPaginas} >>\r\nendobj\r\n");

                // Objeto 3: Fuente Helvetica
                offsets.Add(ms.Position);
                Escribir("3 0 obj\r\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>\r\nendobj\r\n");

                // Objeto 4: Fuente Helvetica-Bold
                offsets.Add(ms.Position);
                Escribir("4 0 obj\r\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>\r\nendobj\r\n");

                // Objetos de páginas y sus contenidos
                for (int i = 0; i < totalPaginas; i++)
                {
                    int idPagina = 5 + (i * 2);
                    int idContenido = idPagina + 1;

                    byte[] bytesContenido = Encoding.Latin1.GetBytes(flujosPaginas[i]);

                    // Objeto Página
                    offsets.Add(ms.Position);
                    Escribir($"{idPagina} 0 obj\r\n" +
                            $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {AnchoA4Landscape:F2} {AltoA4Landscape:F2}] " +
                            $"/Resources << /Font << /F1 3 0 R /F2 4 0 R >> >> " +
                            $"/Contents {idContenido} 0 R >>\r\nendobj\r\n");

                    // Objeto Flujo de contenido
                    offsets.Add(ms.Position);
                    Escribir($"{idContenido} 0 obj\r\n<< /Length {bytesContenido.Length} >>\r\nstream\r\n");
                    EscribirBytes(bytesContenido);
                    Escribir("\r\nendstream\r\nendobj\r\n");
                }

                // Tabla de referencias cruzadas (Xref)
                long posicionXref = ms.Position;
                int totalObjetos = 4 + (totalPaginas * 2);

                Escribir($"xref\r\n0 {totalObjetos + 1}\r\n");
                Escribir("0000000000 65535 f\r\n");
                foreach (long offset in offsets)
                {
                    Escribir($"{offset:D10} 00000 n\r\n");
                }

                // Trailer y fin del archivo
                Escribir($"trailer\r\n<< /Size {totalObjetos + 1} /Root 1 0 R >>\r\n");
                Escribir($"startxref\r\n{posicionXref}\r\n%%EOF\r\n");

                return ms.ToArray();
            }
        }

        private static string EscaparTextoPdf(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (char c in texto)
            {
                if (c == '\\')
                {
                    sb.Append("\\\\");
                }
                else if (c == '(')
                {
                    sb.Append("\\(");
                }
                else if (c == ')')
                {
                    sb.Append("\\)");
                }
                else if (c == '\r' || c == '\n')
                {
                    sb.Append(' ');
                }
                else if (c == '→')
                {
                    sb.Append("->");
                }
                else if (c == '✈')
                {
                    sb.Append("++");
                }
                else if (c == '·')
                {
                    sb.Append('-');
                }
                else if (c >= 32 && c <= 126)
                {
                    sb.Append(c);
                }
                else
                {
                    // Convertir acentos y caracteres especiales a octales de Latin-1 (compatible WinAnsi)
                    try
                    {
                        byte[] b = Encoding.Latin1.GetBytes(new[] { c });
                        if (b.Length > 0 && b[0] >= 32)
                        {
                            sb.Append($"\\{Convert.ToString(b[0], 8).PadLeft(3, '0')}");
                        }
                        else
                        {
                            sb.Append('?');
                        }
                    }
                    catch
                    {
                        sb.Append('?');
                    }
                }
            }

            return sb.ToString();
        }
    }
}
