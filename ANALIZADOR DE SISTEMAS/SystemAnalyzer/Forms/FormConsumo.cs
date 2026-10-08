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
    private readonly Button _refresh = new();
    private readonly Label _lastUpdate = Muted("Última actualización: --:--:--");
    private readonly Label _activity = Muted(string.Empty);
    private readonly SummaryCard _processCount = new("Procesos detectados", "Calculando...");
    private readonly SummaryCard _cpuAvailable = new("Lecturas de CPU", "Calculando...");
    private readonly SummaryCard _memoryTotal = new("RAM física total", "Calculando...");
    private readonly List<Control> _summaryCards = new();
    private readonly FlowLayoutPanel _cpuRows = RankingRowsPanel();
    private readonly FlowLayoutPanel _memoryRows = RankingRowsPanel();
    private readonly Label _cpuNote = ExplanationLabel();
    private readonly Label _memoryNote = ExplanationLabel();
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
        BackColor = AppTheme.Background;
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
        _scrollHost.BackColor = AppTheme.Background;

        _page = new TableLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 1,
            RowCount = 4,
            Dock = DockStyle.Top,
            Padding = new Padding(22, 14, 22, 22),
            BackColor = AppTheme.Background
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
        _header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Height = 68, Margin = new Padding(0, 0, 0, 10) };
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        var titles = new Panel { Dock = DockStyle.Fill };
        titles.Controls.Add(new Label { Text = "Impacto de programas", AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), Location = new Point(0, 0) });
        titles.Controls.Add(new Label { Text = "Interpreta qué procesos destacan en la medición actual.", AutoSize = true, ForeColor = AppTheme.MutedText, Location = new Point(3, 40) });

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 0) };
        ConfigurePrimaryButton(_refresh, "Actualizar", 112);
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
        _summary = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, Height = 98, Margin = new Padding(0, 0, 0, 10) };
        _summaryCards.AddRange(new Control[] { _processCount, _cpuAvailable, _memoryTotal });
        for (var index = 0; index < _summaryCards.Count; index++) _summary.Controls.Add(_summaryCards[index], index, 0);
        return _summary;
    }

    private Control BuildRankings()
    {
        _rankings = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Height = 500, Margin = new Padding(0, 0, 0, 10) };
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
        card.Controls.Add(new Label { Text = title, AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), Location = new Point(18, 15) });
        card.Controls.Add(new Label { Text = subtitle, AutoEllipsis = true, ForeColor = AppTheme.MutedText, Location = new Point(20, 45), Size = new Size(430, 38), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right });
        rows.Location = new Point(16, 88);
        rows.Size = new Size(438, 318);
        rows.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        explanation.Location = new Point(20, 420);
        explanation.Size = new Size(430, 58);
        explanation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        card.Controls.Add(explanation);
        card.Controls.Add(rows);
        rows.SizeChanged += (_, _) => ResizeRankingRows(rows);
        card.Resize += (_, _) =>
        {
            rows.Width = Math.Max(220, card.ClientSize.Width - 32);
            explanation.Width = Math.Max(220, card.ClientSize.Width - 40);
        };
        return card;
    }

    private static void ResizeRankingRows(Control rows)
    {
        foreach (Control row in rows.Controls)
            row.Width = Math.Max(210, rows.ClientSize.Width - 8);
    }

    private static Control BuildDisclaimer()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Height = 78, MinimumSize = new Size(0, 78), Margin = Padding.Empty };
        card.Controls.Add(new Label
        {
            Text = "Alcance de la medición",
            AutoSize = true,
            ForeColor = AppTheme.Text,
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
            Location = new Point(18, 13)
        });
        card.Controls.Add(new Label
        {
            Text = "Una medición puntual no demuestra un problema permanente ni que un proceso sea innecesario. Las tendencias prolongadas requieren el historial planificado para una fase posterior.",
            AutoEllipsis = true,
            ForeColor = AppTheme.MutedText,
            Location = new Point(20, 39),
            Size = new Size(900, 28),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        });
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
        SetBusy(true);
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
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException or System.Runtime.InteropServices.ExternalException)
        {
            if (!IsDisposed) _activity.Text = "Actualización incompleta  ";
        }
        finally
        {
            if (!IsDisposed) SetBusy(false);
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

        _cpuRows.SuspendLayout();
        _cpuRows.Controls.Clear();
        if (ranking.Count == 0)
        {
            _cpuRows.Controls.Add(EmptyRanking("CPU todavía no disponible. Se necesitan dos muestras válidas del mismo proceso."));
            _cpuNote.Text = "No se asignó cero a las lecturas desconocidas; se muestran como pendientes o no disponibles.";
        }
        else
        {
            foreach (var item in ranking)
                _cpuRows.Controls.Add(new RankingRow(item.Name, item.Id, $"{item.CpuUsagePercent:0.0}%", item.CpuUsagePercent!.Value, AppTheme.Primary));
            var top = ranking[0];
            _cpuNote.Text = $"{DisplayName(top.Name)} figura entre los mayores consumidores de CPU de esta muestra. Esto no demuestra una carga sostenida ni una causa de lentitud.";
        }
        _cpuRows.ResumeLayout(true);
        ResizeRankingRows(_cpuRows);
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

        _memoryRows.SuspendLayout();
        _memoryRows.Controls.Clear();
        if (ranking.Count == 0)
        {
            _memoryRows.Controls.Add(EmptyRanking("No hay lecturas de memoria disponibles en esta muestra."));
            _memoryNote.Text = "No fue posible interpretar el impacto de memoria con los datos disponibles.";
        }
        else
        {
            foreach (var item in ranking)
            {
                var share = MemorySharePercent(item.WorkingSetBytes, totalMemoryBytes);
                var percentage = share.HasValue ? $"{share:0.00}% de RAM" : "Porcentaje no disponible";
                _memoryRows.Controls.Add(new RankingRow(item.Name, item.Id, $"{FormatBytes((ulong)item.WorkingSetBytes)} · {percentage}", share ?? 0, Color.FromArgb(167, 139, 250)));
            }
            var top = ranking[0];
            _memoryNote.Text = $"{DisplayName(top.Name)} figura entre los mayores consumidores de memoria en la medición actual. El working set no representa memoria exclusivamente reservada por el proceso.";
        }
        _memoryRows.ResumeLayout(true);
        ResizeRankingRows(_memoryRows);
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
        var usableWidth = Math.Max(1, _scrollHost.ClientSize.Width - _page.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth);
        ResponsiveLayout.Reflow(_summary, _summaryCards, usableWidth >= 850 ? 3 : 1, 96, 6);
        var headerControls = _header.Controls.Cast<Control>().ToArray();
        ResponsiveLayout.Reflow(_header, headerControls, usableWidth >= 820 ? 2 : 1, usableWidth >= 820 ? 68 : 62, 2);
        var rankingControls = new Control[] { _cpuCard, _memoryCard };
        ResponsiveLayout.Reflow(_rankings, rankingControls, usableWidth >= 900 ? 2 : 1, 500, 7);
        _page.PerformLayout();
        _scrollHost.AutoScrollMinSize = new Size(0, _page.PreferredSize.Height);
    }

    private void ResetScrollToTop()
    {
        if (_scrollHost.IsDisposed) return;
        _scrollHost.AutoScrollPosition = Point.Empty;
        if (_scrollHost.VerticalScroll.Visible) _scrollHost.VerticalScroll.Value = _scrollHost.VerticalScroll.Minimum;
    }

    private void SetBusy(bool busy)
    {
        _refresh.Enabled = !busy;
        _refresh.Text = busy ? "Analizando..." : "Actualizar";
        _activity.Text = busy ? "Recopilando datos  " : string.Empty;
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
    }

    private static FlowLayoutPanel RankingRowsPanel() => new()
    {
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoScroll = false,
        BackColor = AppTheme.Surface,
        Margin = Padding.Empty
    };

    private static Label EmptyRanking(string text) => new()
    {
        Text = text,
        ForeColor = AppTheme.MutedText,
        TextAlign = ContentAlignment.MiddleCenter,
        Size = new Size(390, 90),
        Margin = new Padding(4, 20, 4, 0)
    };

    private static Label ExplanationLabel() => new() { ForeColor = AppTheme.MutedText, AutoEllipsis = true };
    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.MutedText, Margin = new Padding(0, 11, 12, 0) };
    private static string DisplayName(string name) => string.IsNullOrWhiteSpace(name) ? "El proceso sin nombre disponible" : name;
    private static string FormatBytes(ulong bytes) => bytes >= 1024d * 1024 * 1024 ? $"{bytes / (1024d * 1024 * 1024):0.##} GB" : $"{bytes / (1024d * 1024):0.##} MB";

    private static void ConfigurePrimaryButton(Button button, string text, int width)
    {
        button.Text = text;
        button.Width = width;
        button.Height = 36;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = AppTheme.Primary;
        button.ForeColor = Color.FromArgb(8, 47, 73);
        button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        button.Cursor = Cursors.Hand;
        button.TabStop = false;
    }

    private sealed class RankingRow : Panel
    {
        private readonly UsageBar _bar;

        public RankingRow(string name, int pid, string value, double percentage, Color color)
        {
            Size = new Size(420, 58);
            Margin = new Padding(3, 1, 3, 3);
            BackColor = Color.FromArgb(38, 51, 70);
            Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(new Label { Text = name, AutoEllipsis = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold), Location = new Point(10, 7), Size = new Size(190, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right });
            Controls.Add(new Label { Text = $"PID {pid}", AutoSize = true, ForeColor = AppTheme.MutedText, Location = new Point(10, 29) });
            Controls.Add(new Label { Text = value, AutoSize = true, ForeColor = AppTheme.Text, TextAlign = ContentAlignment.TopRight, Location = new Point(245, 8), Anchor = AnchorStyles.Top | AnchorStyles.Right });
            _bar = new UsageBar { Location = new Point(120, 38), Width = 288, Value = percentage, FillColor = color, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Controls.Add(_bar);
        }
    }

    private sealed class SummaryCard : RoundedPanel
    {
        private readonly Label _value;
        private readonly Label _caption;

        public SummaryCard(string title, string value)
        {
            Controls.Add(new Label { Text = title, AutoSize = true, ForeColor = AppTheme.MutedText, Font = new Font("Segoe UI Semibold", 9F), Location = new Point(16, 12) });
            _value = new Label { Text = value, AutoEllipsis = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold), Location = new Point(16, 37), Size = new Size(210, 31), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            _caption = new Label { AutoEllipsis = true, ForeColor = AppTheme.MutedText, Location = new Point(17, 70), Size = new Size(210, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Controls.Add(_caption);
            Controls.Add(_value);
            Resize += (_, _) => { _value.Width = Math.Max(80, ClientSize.Width - 32); _caption.Width = Math.Max(80, ClientSize.Width - 34); };
        }

        public void SetValue(string value, string caption) { _value.Text = value; _caption.Text = caption; }
    }
}
