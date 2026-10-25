using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace AgendaDeViajes.Services
{
    /// <summary>
    /// Gestiona la impresión física y la vista previa de impresión para los reportes
    /// de AGENDA DE VIAJES utilizando GDI+ y el subsistema PrintDocument de Windows Forms.
    /// Incorpora asignación inteligente de anchos de columna para garantizar que encabezados
    /// y nombres completos (como ciudades, pasajeros y estados) nunca se partan a la mitad,
    /// saltos de línea estrictamente entre palabras completas, paginación dinámica,
    /// cuadrícula estructurada de tarjetas de métricas y numeración "Página X de Y".
    /// </summary>
    public class ImpresorReporte : PrintDocument
    {
        private readonly string tituloReporte;
        private readonly string subtituloFiltros;
        private readonly List<string> encabezados;
        private readonly List<float> anchosRelativos;
        private readonly List<List<string>> filas;
        private readonly Dictionary<string, string> resumenMetricas;

        private const float MinAltoFilaDatos = 24f;
        private const float AltoFilaEncabezado = 26f;
        private const float PaddingVerticalCelda = 8f;
        private const float PaddingHorizontalCelda = 6f;

        // Estructuras de paginación precalculadas
        private List<List<int>> paginasFilasIndices;
        private List<float> alturasFilas;
        private int paginaResumen;
        private int indicePaginaActual;
        private int totalPaginas;

        public ImpresorReporte(
            string tituloReporte,
            string subtituloFiltros,
            List<string> encabezados,
            List<float> anchosRelativos,
            List<List<string>> filas,
            Dictionary<string, string> resumenMetricas = null)
        {
            this.tituloReporte = tituloReporte ?? "Reporte Ejecutivo";
            this.subtituloFiltros = subtituloFiltros ?? string.Empty;
            this.encabezados = encabezados ?? new List<string>();
            this.anchosRelativos = anchosRelativos ?? new List<float>();
            this.filas = filas ?? new List<List<string>>();
            this.resumenMetricas = resumenMetricas ?? new Dictionary<string, string>();

            DefaultPageSettings.Landscape = true;
            DefaultPageSettings.Margins = new Margins(35, 35, 35, 35);
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);

            indicePaginaActual = 0;
            PrecalcularPaginacion();
        }

        private void PrecalcularPaginacion()
        {
            // Usar un contexto Graphics temporal para medir fuentes con exactitud
            using var bmp = new Bitmap(1, 1);
            using var g = Graphics.FromImage(bmp);
            using var fontHeader = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using var fontCeldas = new Font("Segoe UI", 8.5F, FontStyle.Regular);

            // Ancho imprimible en orientación horizontal A4 (11.69 x 8.27 pulgadas = 1169 x 827 a 100 DPI)
            float anchoTabla = DefaultPageSettings.Landscape
                ? (DefaultPageSettings.PaperSize.Height - DefaultPageSettings.Margins.Left - DefaultPageSettings.Margins.Right)
                : (DefaultPageSettings.PaperSize.Width - DefaultPageSettings.Margins.Left - DefaultPageSettings.Margins.Right);

            if (anchoTabla <= 0)
            {
                anchoTabla = 1169 - 70; // 1099 pt
            }

            // Asignación inteligente de anchos asegurando espacio suficiente para encabezados y contenido
            List<float> anchosCols = CalcularAnchosInteligente(g, fontHeader, encabezados, anchosRelativos, anchoTabla);

            // Medir la altura requerida para cada fila según su texto multilínea (respetando palabras completas)
            alturasFilas = new List<float>();
            foreach (var fila in filas)
            {
                float maxAltoCelda = 0f;
                for (int c = 0; c < encabezados.Count; c++)
                {
                    float w = c < anchosCols.Count ? anchosCols[c] : (anchoTabla / Math.Max(1, encabezados.Count));
                    float textoW = Math.Max(15f, w - (PaddingHorizontalCelda * 2));
                    string valor = c < fila.Count ? fila[c] : string.Empty;

                    // Medir altura con ajuste exclusivamente entre palabras completas
                    SizeF size = g.MeasureString(valor, fontCeldas, (int)textoW);
                    if (size.Height > maxAltoCelda)
                    {
                        maxAltoCelda = size.Height;
                    }
                }

                float altoFila = Math.Max(MinAltoFilaDatos, maxAltoCelda + PaddingVerticalCelda);
                alturasFilas.Add(altoFila);
            }

            // Altura disponible en página
            float altoPagina = DefaultPageSettings.Landscape
                ? (DefaultPageSettings.PaperSize.Width - DefaultPageSettings.Margins.Top - DefaultPageSettings.Margins.Bottom)
                : (DefaultPageSettings.PaperSize.Height - DefaultPageSettings.Margins.Top - DefaultPageSettings.Margins.Bottom);

            if (altoPagina <= 0)
            {
                altoPagina = 827 - 70; // 757 pt
            }

            float altoEncabezadoDoc = 72f;
            float altoPie = 30f;
            float alturaDisponiblePorPagina = altoPagina - altoEncabezadoDoc - altoPie - AltoFilaEncabezado;

            // Calcular altura requerida por el bloque de resumen métrico
            float altoBloqueResumen = CalcularAltoBloqueResumen(resumenMetricas);

            paginasFilasIndices = new List<List<int>>();
            var paginaActual = new List<int>();
            float alturaAcumulada = 0f;

            for (int i = 0; i < filas.Count; i++)
            {
                float altoFila = alturasFilas[i];

                if (alturaAcumulada + altoFila > alturaDisponiblePorPagina && paginaActual.Count > 0)
                {
                    paginasFilasIndices.Add(paginaActual);
                    paginaActual = new List<int>();
                    alturaAcumulada = 0f;
                }

                paginaActual.Add(i);
                alturaAcumulada += altoFila;
            }

            if (paginaActual.Count > 0 || filas.Count == 0)
            {
                paginasFilasIndices.Add(paginaActual);
            }

            paginaResumen = paginasFilasIndices.Count;
            if (resumenMetricas.Count > 0)
            {
                if (alturaAcumulada + altoBloqueResumen > alturaDisponiblePorPagina && filas.Count > 0)
                {
                    // Se requiere página adicional para el resumen de métricas
                    paginasFilasIndices.Add(new List<int>());
                    paginaResumen = paginasFilasIndices.Count;
                }
            }

            totalPaginas = paginasFilasIndices.Count;
        }

        /// <summary>
        /// Calcula anchos de columnas garantizando que encabezados críticos como
        /// Capacidad, Reservados, Disponibles y nombres de ciudades nunca se fragmenten.
        /// </summary>
        private static List<float> CalcularAnchosInteligente(
            Graphics g,
            Font fontHeader,
            List<string> encabezados,
            List<float> relativos,
            float total)
        {
            var res = new List<float>();
            int count = encabezados?.Count ?? 0;
            if (count <= 0) return res;

            var pesos = (relativos != null && relativos.Count == count)
                ? new List<float>(relativos)
                : Enumerable.Repeat(1f, count).ToList();

            var anchosMinimos = new float[count];
            for (int i = 0; i < count; i++)
            {
                string h = (encabezados[i] ?? string.Empty).Trim();
                float widthHeader = g.MeasureString(h, fontHeader).Width;

                // Palabra más larga del encabezado
                float maxWord = 0f;
                foreach (string w in h.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    float ww = g.MeasureString(w, fontHeader).Width;
                    if (ww > maxWord) maxWord = ww;
                }

                float minBase = h.Contains(' ')
                    ? Math.Max(maxWord + 24f, 65f)
                    : Math.Max(widthHeader + 24f, 75f);

                string hLower = h.ToLowerInvariant();
                if (hLower.Contains("capacidad") || hLower.Contains("reservado") || hLower.Contains("disponible"))
                {
                    minBase = Math.Max(minBase, 88f); // Garantiza que "Capacidad", "Reservados", "Disponibles" quepan completos
                    if (pesos[i] < 1.05f) pesos[i] = 1.05f;
                }
                else if (hLower.Contains("origen") || hLower.Contains("destino"))
                {
                    minBase = Math.Max(minBase, 122f); // Espacio generoso para nombres de ciudades completos
                    if (pesos[i] < 1.4f) pesos[i] = 1.4f;
                }
                else if (hLower.Contains("aerolínea") || hLower.Contains("aerolinea"))
                {
                    minBase = Math.Max(minBase, 115f);
                    if (pesos[i] < 1.35f) pesos[i] = 1.35f;
                }
                else if (hLower.Contains("código") || hLower.Contains("codigo"))
                {
                    minBase = Math.Max(minBase, 72f);
                }

                anchosMinimos[i] = minBase;
            }

            float sumaPesos = pesos.Sum();
            if (sumaPesos <= 0) sumaPesos = 1f;

            float sumaMinimos = anchosMinimos.Sum();
            if (sumaMinimos <= total)
            {
                float remanente = total - sumaMinimos;
                for (int i = 0; i < count; i++)
                {
                    float extra = (pesos[i] / sumaPesos) * remanente;
                    res.Add(anchosMinimos[i] + extra);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    res.Add((pesos[i] / sumaPesos) * total);
                }
            }

            return res;
        }

        private static float CalcularAltoBloqueResumen(Dictionary<string, string> resumenMetricas)
        {
            if (resumenMetricas == null || resumenMetricas.Count == 0)
            {
                return 0f;
            }

            int total = resumenMetricas.Count;
            int cols = total <= 4 ? Math.Max(1, total) : (total <= 6 ? 3 : 4);
            int filasGrid = (int)Math.Ceiling((double)total / cols);

            float altoTarjeta = 42f;
            float gapY = 8f;
            return 28f + (filasGrid * altoTarjeta) + ((filasGrid - 1) * gapY) + 14f;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            Graphics g = e.Graphics;
            Rectangle bounds = e.MarginBounds;

            // Fuentes corporativas de dibujo legibles y nítidas
            using var fontLogo = new Font("Segoe UI", 13F, FontStyle.Bold);
            using var fontTitulo = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var fontSubtitulo = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            using var fontHeader = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using var fontCeldas = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            using var fontCardLabel = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            using var fontCardValue = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            using var fontPie = new Font("Segoe UI", 8F, FontStyle.Regular);

            // Brochas y plumas corporativas
            using var brushAzulOscuro = new SolidBrush(Color.FromArgb(22, 34, 47));
            using var brushAzulAcento = new SolidBrush(Color.FromArgb(45, 156, 219));
            using var brushTexto = new SolidBrush(Color.FromArgb(30, 42, 56));
            using var brushSecundario = new SolidBrush(Color.FromArgb(107, 122, 140));
            using var brushBlanco = new SolidBrush(Color.White);
            using var brushZebra = new SolidBrush(Color.FromArgb(248, 250, 252));
            using var penBorde = new Pen(Color.FromArgb(226, 232, 240), 1f);
            using var penBordeCard = new Pen(Color.FromArgb(203, 213, 225), 1f);

            int numeroPagina = indicePaginaActual + 1;
            var indicesFilaDeEstaPagina = paginasFilasIndices[indicePaginaActual];
            bool dibujarResumenAqui = numeroPagina == paginaResumen && resumenMetricas.Count > 0;

            float y = bounds.Top;

            // 1. Franja decorativa superior azul acento
            g.FillRectangle(brushAzulAcento, bounds.Left, y, bounds.Width, 3.5f);
            y += 8;

            // 2. Encabezado institucional
            g.DrawString("✈ AGENDA DE VIAJES  —  SISTEMA DE GESTIÓN AÉREA", fontLogo, brushAzulOscuro, bounds.Left, y);
            y += 24;

            g.DrawString(tituloReporte, fontTitulo, brushAzulOscuro, bounds.Left, y);
            y += 20;

            string infoFiltro = $"Emisión: {DateTime.Now:dd/MM/yyyy HH:mm} | {subtituloFiltros}";
            g.DrawString(infoFiltro, fontSubtitulo, brushSecundario, bounds.Left, y);
            y += 18;

            g.DrawLine(penBorde, bounds.Left, y, bounds.Right, y);
            y += 6;

            // 3. Encabezados de tabla
            float anchoTabla = bounds.Width;
            List<float> anchosCols = CalcularAnchosInteligente(g, fontHeader, encabezados, anchosRelativos, anchoTabla);

            if (encabezados.Count > 0 && (indicesFilaDeEstaPagina.Count > 0 || filas.Count == 0))
            {
                g.FillRectangle(brushAzulOscuro, bounds.Left, y, anchoTabla, AltoFilaEncabezado);

                float xHeader = bounds.Left;
                // Formato estricto para encabezados: NoWrap garantiza que palabras como Capacidad nunca se partan
                var formatHeader = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.None,
                    FormatFlags = StringFormatFlags.NoWrap
                };

                for (int c = 0; c < encabezados.Count; c++)
                {
                    float w = anchosCols[c];
                    var rectHeader = new RectangleF(xHeader + PaddingHorizontalCelda, y, w - (PaddingHorizontalCelda * 2), AltoFilaEncabezado);
                    g.DrawString(encabezados[c], fontHeader, brushBlanco, rectHeader, formatHeader);
                    xHeader += w;
                }
                y += AltoFilaEncabezado;

                // 4. Filas de datos
                // Formato de celdas: salto de línea únicamente entre palabras completas (Word Trimming + LineLimit)
                var formatCell = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    Trimming = StringTrimming.Word,
                    FormatFlags = StringFormatFlags.LineLimit
                };

                for (int r = 0; r < indicesFilaDeEstaPagina.Count; r++)
                {
                    int filaOriginalIdx = indicesFilaDeEstaPagina[r];
                    float altoFila = alturasFilas[filaOriginalIdx];
                    var fila = filas[filaOriginalIdx];

                    // Fondo alternado (zebra)
                    if (r % 2 == 1)
                    {
                        g.FillRectangle(brushZebra, bounds.Left, y, anchoTabla, altoFila);
                    }

                    // Línea divisoria inferior
                    g.DrawLine(penBorde, bounds.Left, y + altoFila, bounds.Right, y + altoFila);

                    float x = bounds.Left;
                    for (int c = 0; c < encabezados.Count; c++)
                    {
                        float w = anchosCols[c];
                        string valor = c < fila.Count ? fila[c] : string.Empty;
                        var cellRect = new RectangleF(
                            x + PaddingHorizontalCelda,
                            y + (PaddingVerticalCelda / 2f),
                            w - (PaddingHorizontalCelda * 2),
                            altoFila - PaddingVerticalCelda);

                        g.DrawString(valor, fontCeldas, brushTexto, cellRect, formatCell);
                        x += w;
                    }

                    y += altoFila;
                }

                if (indicesFilaDeEstaPagina.Count == 0 && filas.Count == 0)
                {
                    g.DrawString("No se encontraron registros que coincidan con los criterios seleccionados.", fontCeldas, brushSecundario, bounds.Left + 10, y + 10);
                    y += 35;
                }
            }

            // 5. Bloque de resumen e indicadores (Cuadrícula organizada de tarjetas)
            if (dibujarResumenAqui)
            {
                y += 16f;

                // Título de sección de resumen
                using var fontBoldSeccion = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                g.DrawString("RESUMEN EJECUTIVO E INDICADORES CLAVE", fontBoldSeccion, brushAzulOscuro, bounds.Left, y);
                y += 20f;

                var metricasLista = resumenMetricas.ToList();
                int totalTarjetas = metricasLista.Count;
                int cols = totalTarjetas <= 4 ? Math.Max(1, totalTarjetas) : (totalTarjetas <= 6 ? 3 : 4);
                float gapX = 10f;
                float gapY = 8f;
                float altoTarjeta = 42f;
                float anchoTarjeta = (anchoTabla - ((cols - 1) * gapX)) / cols;

                var formatCardLabel = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                var formatCardValue = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.None
                };

                for (int m = 0; m < totalTarjetas; m++)
                {
                    int filaTarjeta = m / cols;
                    int colTarjeta = m % cols;

                    float cardX = bounds.Left + (colTarjeta * (anchoTarjeta + gapX));
                    float cardY = y + (filaTarjeta * (altoTarjeta + gapY));

                    // Fondo de tarjeta (#F8FAFC)
                    g.FillRectangle(brushZebra, cardX, cardY, anchoTarjeta, altoTarjeta);

                    // Borde de tarjeta
                    g.DrawRectangle(penBordeCard, cardX, cardY, anchoTarjeta, altoTarjeta);

                    // Franja de acento izquierda
                    g.FillRectangle(brushAzulAcento, cardX, cardY, 3.5f, altoTarjeta);

                    var kvp = metricasLista[m];
                    string etiqueta = kvp.Key.ToUpperInvariant();
                    string valor = kvp.Value;

                    // Etiqueta del indicador
                    var rectLabel = new RectangleF(cardX + 8, cardY + 4, anchoTarjeta - 12, 16);
                    g.DrawString(etiqueta, fontCardLabel, brushSecundario, rectLabel, formatCardLabel);

                    // Valor del indicador
                    var rectValue = new RectangleF(cardX + 8, cardY + 18, anchoTarjeta - 12, 20);
                    g.DrawString(valor, fontCardValue, brushAzulOscuro, rectValue, formatCardValue);
                }
            }

            // 6. Pie de página institucional
            DibujarPiePagina(g, bounds, numeroPagina, totalPaginas, fontPie, brushSecundario, brushAzulAcento, penBorde);

            indicePaginaActual++;
            e.HasMorePages = indicePaginaActual < paginasFilasIndices.Count;
        }

        private static void DibujarPiePagina(
            Graphics g,
            Rectangle bounds,
            int numPag,
            int totalPags,
            Font fontPie,
            Brush brushSecundario,
            Brush brushAzulAcento,
            Pen penBorde)
        {
            float yPie = bounds.Bottom - 18;
            g.DrawLine(penBorde, bounds.Left, yPie - 4, bounds.Right, yPie - 4);

            g.DrawString("AGENDA DE VIAJES  ·  By: Rodrigo Rivera - RF1631012025", fontPie, brushSecundario, bounds.Left, yPie);
            string pagTexto = $"Página {numPag} de {totalPags}";
            using var fontBold = new Font("Segoe UI", 8F, FontStyle.Bold);
            g.DrawString(pagTexto, fontBold, brushAzulAcento, bounds.Right - 90, yPie);
        }
    }
}
