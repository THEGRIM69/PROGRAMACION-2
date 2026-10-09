using System.Diagnostics;
using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;
using Timer = System.Windows.Forms.Timer;

namespace SystemAnalyzer.Forms;

public sealed class FormConsumo : Form
{
    private const int RankingSize = 5;
    private readonly ProcessAnalyzerService _processService = new();
    private readonly SystemInfoService _systemService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Timer _timer = new() { Interval = 5_000 };
    private readonly ThemedButton _refresh = new();
    private readonly ToolTip _toolTip = new();
    private readonly Label _lastUpdate = Muted("Última actualización: --:--:--");
    private readonly Label _activity = Muted(string.Empty);
    private readonly SummaryCard _processCount = new("Procesos detectados", "Calculando...");
    private readonly SummaryCard _cpuAvailable = new("Lecturas de CPU", "Calculando...");
    private readonly SummaryCard _memoryTotal = new("RAM física total", "Calculando...");
    private readonly List<Control> _summaryCards = new();
    private readonly TableLayoutPanel _cpuRows = RankingRowsPanel();
    private readonly TableLayoutPanel _memoryRows = RankingRowsPanel();
    private readonly Label _cpuNote = ExplanationLabel();
    private readonly Label _memoryNote = ExplanationLabel();
    private readonly Label _headerDescription = WrappingLabel("Compara los procesos que más CPU y memoria utilizan en la medición actual.");
    private readonly Panel _scrollHost = new();
    private TableLayoutPanel _page = null!;
    private TableLayoutPanel _header = null!;
    private TableLayoutPanel _summary = null!;
    private TableLayoutPanel _rankings = null!;
    private RoundedPanel _cpuCard = null!;
    private RoundedPanel _memoryCard = null!;
    private bool _refreshing;
    private bool _resourcesDisposed;

    public FormConsumo()
    {
        FormBorderStyle = FormBorderStyle.None;
        TopLevel = false;
        Dock = DockStyle.Fill;
        BackColor = AppTheme.BackgroundPrimary;
        Font = new Font("Segoe UI", 9F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BuildInterface();
        ClientSizeChanged += HandleClientSizeChanged;
        Shown += HandleShown;
        _timer.Tick += HandleTimerTick;
        Disposed += HandleDisposed;
    }

    private void BuildInterface()
    {
        _scrollHost.Dock = DockStyle.Fill;
        _scrollHost.AutoScroll = true;
        _scrollHost.BackColor = AppTheme.BackgroundPrimary;

        _page = new TableLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 1,
            RowCount = 4,
            Dock = DockStyle.Top,
            Padding = new Padding(24, 16, 24, 24),
            BackColor = AppTheme.BackgroundPrimary
        };
        _page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 4; row++) _page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _page.Controls.Add(BuildHeader(), 0, 0);
        _page.Controls.Add(BuildSummary(), 0, 1);
        _page.Controls.Add(BuildRankings(), 0, 2);
        _page.Controls.Add(BuildDisclaimer(), 0, 3);
        _scrollHost.Controls.Add(_page);
        Controls.Add(_scrollHost);
        ApplyResponsiveLayout();
    }

    private Control BuildHeader()
    {
        _header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Height = 80, Margin = new Padding(0, 0, 0, 12) };
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        var titles = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        titles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        titles.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        titles.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        titles.Controls.Add(new Label { Text = "Mayor consumo", Dock = DockStyle.Fill, AutoEllipsis = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        _headerDescription.Margin = Padding.Empty;
        titles.Controls.Add(_headerDescription, 0, 1);
        _toolTip.SetToolTip(_headerDescription, _headerDescription.Text);

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 0) };
        ConfigureButton(_refresh, "Actualizar", 120);
        _refresh.Click += HandleRefreshClick;
        actions.Controls.Add(_refresh);
        actions.Controls.Add(_activity);
        actions.Controls.Add(_lastUpdate);
        _header.Controls.Add(titles, 0, 0);
        _header.Controls.Add(actions, 1, 0);
        return _header;
    }

    private Control BuildSummary()
    {
        _summary = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, Height = 108, Margin = new Padding(0, 0, 0, 12) };
        _summaryCards.AddRange(new Control[] { _processCount, _cpuAvailable, _memoryTotal });
        for (var index = 0; index < _summaryCards.Count; index++) _summary.Controls.Add(_summaryCards[index], index, 0);
        return _summary;
    }

    private Control BuildRankings()
    {
        _rankings = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Height = 500, Margin = new Padding(0, 0, 0, 12) };
        _cpuCard = BuildRankingCard(
            "Mayor uso de CPU",
            "Porcentaje de la capacidad total de los procesadores lógicos durante el intervalo entre muestras.",
            _cpuRows,
            _cpuNote);
        _memoryCard = BuildRankingCard(
            "Mayor uso de RAM",
            "Conjunto de trabajo (working set) respecto a la memoria física total; puede incluir memoria compartida.",
            _memoryRows,
            _memoryNote);
        _rankings.Controls.Add(_cpuCard, 0, 0);
        _rankings.Controls.Add(_memoryCard, 1, 0);
        return _rankings;
    }

    private static RoundedPanel BuildRankingCard(string title, string subtitle, Control rows, Label explanation)
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, MinimumSize = new Size(0, 500) };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(20, 16, 20, 16),
            Margin = Padding.Empty,
            BackColor = AppTheme.SurfacePrimary
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
        layout.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, AutoEllipsis = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty }, 0, 0);
        layout.Controls.Add(WrappingLabel(subtitle), 0, 1);
        rows.Dock = DockStyle.Fill;
        rows.Margin = new Padding(0, 3, 0, 4);
        explanation.Dock = DockStyle.Fill;
        explanation.Margin = new Padding(3, 5, 3, 0);
        layout.Controls.Add(rows, 0, 2);
        layout.Controls.Add(explanation, 0, 3);
        card.Controls.Add(layout);
        return card;
    }

    private static Control BuildDisclaimer()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Height = 96, MinimumSize = new Size(0, 96), Margin = Padding.Empty };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(20, 12, 20, 12),
            Margin = Padding.Empty,
            BackColor = AppTheme.SurfacePrimary
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(new Label { Text = "Alcance de la medición", Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        layout.Controls.Add(WrappingLabel("Una medición puntual no demuestra un problema permanente ni que un proceso sea innecesario. Las tendencias prolongadas requieren el historial planificado para una fase posterior."), 0, 1);
        card.Controls.Add(layout);
        return card;
    }

    private async void HandleShown(object? sender, EventArgs e)
    {
        ResetScrollToTop();
        await RefreshRankingsAsync();
        if (!IsDisposed)
        {
            _timer.Start();
            BeginInvoke(ResetScrollToTop);
        }
    }

    private async void HandleTimerTick(object? sender, EventArgs e) => await RefreshRankingsAsync();
    private async void HandleRefreshClick(object? sender, EventArgs e) => await RefreshRankingsAsync();
    private void HandleClientSizeChanged(object? sender, EventArgs e) => ApplyResponsiveLayout();
    private void HandleDisposed(object? sender, EventArgs e) => DisposeResources();

    private async Task RefreshRankingsAsync()
    {
        if (_refreshing || IsDisposed) return;
        _refreshing = true;
        var stopwatch = Stopwatch.StartNew();
        SetUpdatingState();
        try
        {
            var token = _lifetime.Token;
            var processesTask = _processService.GetProcessesAsync(token);
            var systemTask = _systemService.GetSummaryAsync(token);
            await Task.WhenAll(processesTask, systemTask);
            if (IsDisposed || token.IsCancellationRequested) return;

            var processes = processesTask.Result;
            var system = systemTask.Result;
            UpdateSummary(processes, system);
            UpdateCpuRanking(processes);
            UpdateMemoryRanking(processes, system.TotalMemoryBytes);
            stopwatch.Stop();
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
            var cpuAvailable = processes.Count(process => process.CpuUsagePercent.HasValue);
            _activity.Text = cpuAvailable == 0
                ? $"Esperando segunda muestra · {stopwatch.ElapsedMilliseconds:N0} ms  "
                : $"Datos actualizados · {stopwatch.ElapsedMilliseconds:N0} ms  ";
            _activity.ForeColor = cpuAvailable == 0 ? AppTheme.StatusInfo : AppTheme.StatusSuccess;
        }
        catch (OperationCanceledException)
        {
            if (!IsDisposed)
            {
                _activity.Text = "Actualización cancelada  ";
                _activity.ForeColor = AppTheme.StatusWarning;
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException or System.Runtime.InteropServices.ExternalException)
        {
            if (!IsDisposed)
            {
                _activity.Text = "Error recuperable · intenta actualizar nuevamente  ";
                _activity.ForeColor = AppTheme.StatusError;
            }
        }
        finally
        {
            stopwatch.Stop();
            if (!IsDisposed) RestoreRefreshButton();
            _refreshing = false;
        }
    }

    private void UpdateSummary(IReadOnlyList<ProcessInfo> processes, SystemInfo system)
    {
        var availableCpu = processes.Count(process => process.CpuUsagePercent.HasValue);
        _processCount.SetValue(processes.Count.ToString("N0"), "Procesos incluidos en esta muestra");
        _cpuAvailable.SetValue($"{availableCpu:N0} de {processes.Count:N0}", availableCpu == 0 ? "Esperando una segunda muestra válida" : "Procesos con identidad y CPU verificables");
        _memoryTotal.SetValue(system.TotalMemoryBytes > 0 ? FormatBytes(system.TotalMemoryBytes) : "No disponible", "Denominador para porcentajes de RAM");
    }

    private void UpdateCpuRanking(IReadOnlyList<ProcessInfo> processes)
    {
        var ranking = processes
            .Where(process => process.CpuUsagePercent is >= 0 and <= 100)
            .OrderByDescending(process => process.CpuUsagePercent!.Value)
            .ThenBy(process => process.Name, StringComparer.OrdinalIgnoreCase)
            .ThenBy(process => process.Id)
            .Take(RankingSize)
            .ToList();

        PrepareRankingRows(_cpuRows, ranking.Count == 0 ? 1 : ranking.Count);
        if (ranking.Count == 0)
        {
            _cpuRows.Controls.Add(EmptyRanking("CPU todavía no disponible. Se necesitan dos muestras válidas del mismo proceso."), 0, 0);
            _cpuNote.Text = "No se asignó cero a las lecturas desconocidas; se muestran como pendientes o no disponibles.";
        }
        else
        {
            for (var index = 0; index < ranking.Count; index++)
            {
                var item = ranking[index];
                _cpuRows.Controls.Add(new RankingRow(index + 1, item.Name, item.Id, $"{item.CpuUsagePercent:0.0}%", item.CpuUsagePercent!.Value, AppTheme.AccentPrimary, _toolTip), 0, index);
            }
            var top = ranking[0];
            _cpuNote.Text = $"{DisplayName(top.Name)} figura entre los mayores consumidores de CPU de esta muestra. Esto no demuestra una carga sostenida ni una causa de lentitud.";
        }
        _cpuRows.ResumeLayout(true);
    }

    private void UpdateMemoryRanking(IReadOnlyList<ProcessInfo> processes, ulong totalMemoryBytes)
    {
        var ranking = processes
            .Where(process => process.WorkingSetBytes >= 0)
            .OrderByDescending(process => process.WorkingSetBytes)
            .ThenBy(process => process.Name, StringComparer.OrdinalIgnoreCase)
            .ThenBy(process => process.Id)
            .Take(RankingSize)
            .ToList();

        PrepareRankingRows(_memoryRows, ranking.Count == 0 ? 1 : ranking.Count);
        if (ranking.Count == 0)
        {
            _memoryRows.Controls.Add(EmptyRanking("No hay lecturas de memoria disponibles en esta muestra."), 0, 0);
            _memoryNote.Text = "No fue posible interpretar el impacto de memoria con los datos disponibles.";
        }
        else
        {
            for (var index = 0; index < ranking.Count; index++)
            {
                var item = ranking[index];
                var share = MemorySharePercent(item.WorkingSetBytes, totalMemoryBytes);
                var percentage = share.HasValue ? $"{share:0.00}% de RAM" : "Porcentaje no disponible";
                _memoryRows.Controls.Add(new RankingRow(index + 1, item.Name, item.Id, $"{FormatBytes((ulong)item.WorkingSetBytes)} · {percentage}", share ?? 0, AppTheme.AccentSecondary, _toolTip), 0, index);
            }
            var top = ranking[0];
            _memoryNote.Text = $"{DisplayName(top.Name)} figura entre los mayores consumidores de memoria en la medición actual. El working set no representa memoria exclusivamente reservada por el proceso.";
        }
        _memoryRows.ResumeLayout(true);
    }

    private static void PrepareRankingRows(TableLayoutPanel panel, int rowCount)
    {
        panel.SuspendLayout();
        panel.Controls.Clear();
        panel.RowStyles.Clear();
        panel.RowCount = rowCount;
        for (var row = 0; row < rowCount; row++)
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));
    }

    internal static double? MemorySharePercent(long workingSetBytes, ulong totalMemoryBytes)
    {
        if (workingSetBytes < 0 || totalMemoryBytes == 0) return null;
        var percentage = workingSetBytes * 100d / totalMemoryBytes;
        return double.IsFinite(percentage) && percentage >= 0 ? Math.Clamp(percentage, 0, 100) : null;
    }

    private void ApplyResponsiveLayout()
    {
        if (_page is null || _rankings is null || _summary is null || _header is null) return;
        _page.Padding = _scrollHost.ClientSize.Width < 700 ? new Padding(16) : new Padding(24, 16, 24, 24);
        var usableWidth = Math.Max(1, _scrollHost.ClientSize.Width - _page.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth);
        ResponsiveLayout.Reflow(_summary, _summaryCards, usableWidth >= 850 ? 3 : 1, 108, 6);
        var headerControls = _header.Controls.Cast<Control>().ToArray();
        _lastUpdate.Visible = usableWidth >= 600;
        ResponsiveLayout.Reflow(_header, headerControls, usableWidth >= 850 ? 2 : 1, 80, 4);
        var rankingControls = new Control[] { _cpuCard, _memoryCard };
        ResponsiveLayout.Reflow(_rankings, rankingControls, usableWidth >= 900 ? 2 : 1, 500, 6);
        _page.PerformLayout();
        _scrollHost.AutoScrollMinSize = new Size(0, _page.PreferredSize.Height);
    }

    private void ResetScrollToTop()
    {
        if (_scrollHost.IsDisposed) return;
        _scrollHost.AutoScrollPosition = Point.Empty;
        if (_scrollHost.VerticalScroll.Visible) _scrollHost.VerticalScroll.Value = _scrollHost.VerticalScroll.Minimum;
    }

    private void SetUpdatingState()
    {
        _refresh.Enabled = false;
        _refresh.Text = "Analizando...";
        _activity.Text = "Recopilando información...  ";
        _activity.ForeColor = AppTheme.TextSecondary;
    }

    private void RestoreRefreshButton()
    {
        _refresh.Enabled = true;
        _refresh.Text = "Actualizar";
    }

    private void DisposeResources()
    {
        if (_resourcesDisposed) return;
        _resourcesDisposed = true;
        _timer.Stop();
        _timer.Tick -= HandleTimerTick;
        _timer.Dispose();
        _refresh.Click -= HandleRefreshClick;
        ClientSizeChanged -= HandleClientSizeChanged;
        Shown -= HandleShown;
        Disposed -= HandleDisposed;
        _lifetime.Cancel();
        _lifetime.Dispose();
        _toolTip.Dispose();
    }

    private static TableLayoutPanel RankingRowsPanel()
    {
        var panel = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 1,
            BackColor = AppTheme.SurfacePrimary,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return panel;
    }

    private static Label EmptyRanking(string text) => new()
    {
        Text = text,
        ForeColor = AppTheme.TextSecondary,
        TextAlign = ContentAlignment.MiddleCenter,
        Dock = DockStyle.Fill,
        Margin = new Padding(4)
    };

    private static Label ExplanationLabel() => WrappingLabel(string.Empty);
    private static Label WrappingLabel(string text) => new() { Text = text, Dock = DockStyle.Fill, AutoEllipsis = false, AutoSize = false, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft };
    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.TextSecondary, Margin = new Padding(0, 11, 12, 0) };
    private static string DisplayName(string name) => string.IsNullOrWhiteSpace(name) ? "El proceso sin nombre disponible" : name;
    private static string FormatBytes(ulong bytes) => bytes >= 1024d * 1024 * 1024 ? $"{bytes / (1024d * 1024 * 1024):0.##} GB" : $"{bytes / (1024d * 1024):0.##} MB";

    private static void ConfigureButton(ThemedButton button, string text, int width)
    {
        button.Text = text;
        button.Width = width;
        button.Height = 40;
        button.TabIndex = 0;
        button.AccessibleName = "Actualizar rankings de consumo";
    }

    private sealed class RankingRow : Panel
    {
        public RankingRow(int position, string name, int pid, string value, double percentage, Color color, ToolTip toolTip)
        {
            Dock = DockStyle.Fill;
            Margin = new Padding(0, 0, 0, 8);
            BackColor = AppTheme.SurfaceSecondary;
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                Padding = new Padding(12, 4, 12, 4),
                Margin = Padding.Empty,
                BackColor = AppTheme.SurfaceSecondary
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 36));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 16));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var rank = new Label { Text = $"{position}.", Dock = DockStyle.Fill, ForeColor = color, Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            var processName = new Label { Text = name, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            var processValue = new Label { Text = value, AutoEllipsis = false, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight, Margin = Padding.Empty };
            var processId = new Label { Text = $"PID {pid}", Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            var bar = new UsageBar { Dock = DockStyle.Top, Margin = new Padding(0, 3, 0, 0), Value = percentage, FillColor = color };

            layout.Controls.Add(rank, 0, 0);
            layout.SetRowSpan(rank, 2);
            layout.Controls.Add(processName, 1, 0);
            layout.Controls.Add(processValue, 2, 0);
            layout.Controls.Add(processId, 1, 1);
            layout.SetColumnSpan(processId, 2);
            layout.Controls.Add(bar, 1, 2);
            layout.SetColumnSpan(bar, 2);
            toolTip.SetToolTip(processName, name);
            toolTip.SetToolTip(processValue, value);
            Controls.Add(layout);
            layout.Resize += (_, _) => ArrangeValueColumn(layout, processValue);
            ArrangeValueColumn(layout, processValue);
        }

        private static void ArrangeValueColumn(TableLayoutPanel layout, Label value)
        {
            var available = Math.Max(230, layout.ClientSize.Width - layout.Padding.Horizontal - 36);
            var desired = Math.Max(150, value.PreferredWidth + 16);
            layout.ColumnStyles[2].Width = Math.Min(desired, Math.Max(150, available - 80));
        }
    }

    private sealed class SummaryCard : RoundedPanel
    {
        private readonly Label _value;
        private readonly Label _caption;

        public SummaryCard(string title, string value)
        {
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 14, 20, 14), ColumnCount = 1, RowCount = 3, BackColor = AppTheme.SurfacePrimary };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, Font = new Font("Segoe UI Semibold", 10F), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty }, 0, 0);
            _value = new Label { Text = value, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            _caption = new Label { AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.TopLeft, Margin = Padding.Empty };
            layout.Controls.Add(_value, 0, 1);
            layout.Controls.Add(_caption, 0, 2);
            Controls.Add(layout);
        }

        public void SetValue(string value, string caption) { _value.Text = value; _caption.Text = caption; }
    }
}
