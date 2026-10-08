using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;
using Timer = System.Windows.Forms.Timer;

namespace SystemAnalyzer.Forms;

public sealed class FormInicio : Form
{
    private readonly SystemInfoService _systemService = new();
    private readonly ProcessAnalyzerService _processService = new();
    private readonly StorageAnalyzerService _storageService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Timer _timer = new() { Interval = 15_000 };
    private readonly Label _lastUpdate = Muted("Última actualización: --:--:--");
    private readonly Label _activity = Muted(string.Empty);
    private readonly Button _refresh = new();
    private readonly MetricCard _cpu = new("CPU", "Midiéndose mediante una muestra real");
    private readonly MetricCard _memory = new("Memoria RAM", "Memoria física utilizada");
    private readonly MetricCard _storage = new("Almacenamiento", "Unidad principal del sistema");
    private readonly MetricCard _processes = new("Procesos activos", "Procesos actualmente en ejecución", false);
    private readonly Label _status = ValueLabel("CALCULANDO...", 20);
    private readonly Label _statusDetail = Muted("Recopilando indicadores del equipo.");
    private readonly Dictionary<string, Label> _systemValues = new();
    private readonly Label[] _topProcesses = new Label[3];
    private TableLayoutPanel _headerGrid = null!;
    private TableLayoutPanel _cardsGrid = null!;
    private TableLayoutPanel _detailsGrid = null!;
    private readonly List<Control> _metricCards = new();
    private readonly List<Control> _detailCards = new();
    private bool _refreshing;
    private bool _resourcesDisposed;

    public FormInicio()
    {
        FormBorderStyle = FormBorderStyle.None;
        TopLevel = false;
        Dock = DockStyle.Fill;
        BackColor = AppTheme.Background;
        AutoScroll = true;
        Font = new Font("Segoe UI", 9F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BuildInterface();
        ClientSizeChanged += (_, _) => ApplyResponsiveLayout();

        Shown += async (_, _) =>
        {
            ResetScrollToTop();
            await RefreshDashboardAsync();
            if (!IsDisposed)
            {
                _timer.Start();
                BeginInvoke(ResetScrollToTop);
            }
        };
        _timer.Tick += async (_, _) => await RefreshDashboardAsync();
        Disposed += (_, _) => DisposeResources();
    }

    private void BuildInterface()
    {
        var page = new TableLayoutPanel
        {
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, RowCount = 4,
            Dock = DockStyle.Top, Padding = new Padding(22, 14, 22, 22), BackColor = AppTheme.Background
        };
        page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 4; row++) page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        page.Controls.Add(BuildHeader(), 0, 0);
        page.Controls.Add(BuildCards(), 0, 1);
        page.Controls.Add(BuildDetails(), 0, 2);
        page.Controls.Add(BuildRanking(), 0, 3);
        Controls.Add(page);
        ApplyResponsiveLayout();
    }

    private Control BuildHeader()
    {
        _headerGrid = Grid(2, 68, new[] { 60F, 40F });
        _headerGrid.Margin = new Padding(0, 0, 0, 10);
        var titles = new Panel { Dock = DockStyle.Fill };
        titles.Controls.Add(new Label { Text = "Panel de rendimiento", AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), Location = new Point(0, 0) });
        titles.Controls.Add(new Label { Text = "Resumen del estado actual de tu equipo", AutoSize = true, ForeColor = AppTheme.MutedText, Font = new Font("Segoe UI", 9.5F), Location = new Point(3, 39) });

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 5, 0, 0) };
        _refresh.Text = "Actualizar";
        _refresh.Size = new Size(112, 36);
        _refresh.FlatStyle = FlatStyle.Flat;
        _refresh.FlatAppearance.BorderSize = 0;
        _refresh.BackColor = AppTheme.Primary;
        _refresh.ForeColor = Color.FromArgb(8, 47, 73);
        _refresh.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _refresh.Cursor = Cursors.Hand;
        _refresh.Click += async (_, _) => await RefreshDashboardAsync();
        actions.Controls.Add(_refresh);
        actions.Controls.Add(_activity);
        actions.Controls.Add(_lastUpdate);
        _headerGrid.Controls.Add(titles, 0, 0);
        _headerGrid.Controls.Add(actions, 1, 0);
        return _headerGrid;
    }

    private Control BuildCards()
    {
        _cardsGrid = Grid(4, 144, new[] { 25F, 25F, 25F, 25F });
        _cardsGrid.Margin = new Padding(0, 0, 0, 10);
        _metricCards.AddRange(new Control[] { _cpu, _memory, _storage, _processes });
        for (var i = 0; i < _metricCards.Count; i++)
        {
            _metricCards[i].Dock = DockStyle.Fill;
            _cardsGrid.Controls.Add(_metricCards[i], i, 0);
        }
        return _cardsGrid;
    }

    private Control BuildDetails()
    {
        _detailsGrid = Grid(2, 210, new[] { 37F, 63F });
        _detailsGrid.Margin = new Padding(0, 0, 0, 10);
        var health = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 7, 0) };
        health.Controls.Add(SectionTitle("Estado general"));
        _status.Location = new Point(20, 58);
        _status.ForeColor = AppTheme.Primary;
        _statusDetail.Location = new Point(22, 103);
        _statusDetail.Size = new Size(315, 62);
        _statusDetail.AutoSize = false;
        _statusDetail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        health.Controls.Add(_statusDetail);
        health.Controls.Add(_status);

        var information = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(7, 0, 0, 0) };
        information.Controls.Add(SectionTitle("Información rápida"));
        var rows = new TableLayoutPanel { ColumnCount = 2, RowCount = 5, Location = new Point(20, 47), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Size = new Size(570, 145) };
        rows.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31));
        rows.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69));
        AddInfo(rows, 0, "Sistema operativo", "os");
        AddInfo(rows, 1, "Arquitectura", "architecture");
        AddInfo(rows, 2, "Procesador", "processor");
        AddInfo(rows, 3, "RAM instalada", "ram");
        AddInfo(rows, 4, "Equipo", "computer");
        information.Controls.Add(rows);
        information.Resize += (_, _) => rows.Width = Math.Max(120, information.ClientSize.Width - 40);
        _detailCards.AddRange(new Control[] { health, information });
        _detailsGrid.Controls.Add(health, 0, 0);
        _detailsGrid.Controls.Add(information, 1, 0);
        return _detailsGrid;
    }

    private Control BuildRanking()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Height = 154, MinimumSize = new Size(0, 154), Margin = new Padding(0) };
        card.Controls.Add(SectionTitle("Mayor consumo de memoria"));
        card.Controls.Add(new Label { Text = "Resumen de los tres procesos con mayor memoria física", AutoSize = true, ForeColor = AppTheme.MutedText, Location = new Point(22, 43) });
        for (var i = 0; i < 3; i++)
        {
            _topProcesses[i] = new Label { Text = $"{i + 1}.  Calculando...", ForeColor = AppTheme.Text, Font = new Font("Segoe UI", 10F), Location = new Point(22, 67 + i * 25), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Size = new Size(940, 22) };
            card.Controls.Add(_topProcesses[i]);
        }
        return card;
    }

    private void ApplyResponsiveLayout()
    {
        if (_cardsGrid is null || _detailsGrid is null || _headerGrid is null) return;
        var usableWidth = Math.Max(1, ClientSize.Width - 44);
        var cardColumns = usableWidth >= 1020 ? 4 : usableWidth >= 560 ? 2 : 1;
        ResponsiveLayout.Reflow(_cardsGrid, _metricCards, cardColumns, 144);

        var detailColumns = usableWidth >= 760 ? 2 : 1;
        ResponsiveLayout.Reflow(_detailsGrid, _detailCards, detailColumns, 210);

        var headerControls = _headerGrid.Controls.Cast<Control>().ToArray();
        var headerColumns = usableWidth >= 850 ? 2 : 1;
        var subtitle = headerControls.FirstOrDefault()?.Controls.OfType<Label>().LastOrDefault();
        if (subtitle is not null) subtitle.Visible = usableWidth >= 600;
        ResponsiveLayout.Reflow(_headerGrid, headerControls, headerColumns, headerColumns == 2 ? 68 : 54, 2);
        _headerGrid.Margin = new Padding(0, 0, 0, 10);
    }

    private async Task RefreshDashboardAsync()
    {
        if (_refreshing || IsDisposed) return;
        _refreshing = true;
        SetBusy(true);
        try
        {
            var token = _lifetime.Token;
            var systemTask = _systemService.GetSummaryAsync(token);
            var processTask = _processService.GetProcessesAsync(token);
            var storageTask = _storageService.GetSystemDriveAsync(token);
            await Task.WhenAll(systemTask, processTask, storageTask);
            if (IsDisposed || token.IsCancellationRequested) return;
            UpdateSystem(systemTask.Result);
            UpdateProcesses(processTask.Result);
            UpdateStorage(storageTask.Result);
            UpdateStatus(systemTask.Result, storageTask.Result);
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException or System.Runtime.InteropServices.ExternalException)
        {
            if (!IsDisposed) _activity.Text = "Actualización incompleta  ";
        }
        finally { if (!IsDisposed) SetBusy(false); _refreshing = false; }
    }

    private void UpdateSystem(SystemInfo info)
    {
        if (info.CpuUsagePercent is double cpu) _cpu.SetValue($"{cpu:0}%", $"{UsageText(cpu)} · muestra de 0.5 s", cpu);
        else _cpu.SetUnavailable();

        if (info.TotalMemoryBytes > 0)
        {
            var used = info.TotalMemoryBytes - info.AvailableMemoryBytes;
            _memory.SetValue($"{FormatBytes(used)} / {FormatBytes(info.TotalMemoryBytes)}", $"{info.MemoryUsagePercent:0}% utilizado", info.MemoryUsagePercent);
        }
        else _memory.SetUnavailable();

        _systemValues["os"].Text = info.OperatingSystem;
        _systemValues["architecture"].Text = info.Architecture;
        _systemValues["processor"].Text = info.Processor;
        _systemValues["ram"].Text = info.TotalMemoryBytes > 0 ? FormatBytes(info.TotalMemoryBytes) : "No disponible";
        _systemValues["computer"].Text = info.ComputerName;
    }

    private void UpdateProcesses(IReadOnlyList<ProcessInfo> items)
    {
        _processes.SetValue(items.Count.ToString("N0"), "Procesos actualmente en ejecución", null);
        var top = items.OrderByDescending(item => item.WorkingSetBytes).Take(3).ToArray();
        for (var i = 0; i < 3; i++)
            _topProcesses[i].Text = i < top.Length ? $"{i + 1}.  {top[i].Name}.exe     —     {FormatBytes((ulong)top[i].WorkingSetBytes)}" : $"{i + 1}.  No disponible";
    }

    private void UpdateStorage(DriveInfoModel? drive)
    {
        if (drive is null) { _storage.SetUnavailable(); return; }
        _storage.SetTitle($"Disco {drive.Name}");
        _storage.SetValue($"{FormatBytes((ulong)drive.UsedBytes)} / {FormatBytes((ulong)drive.TotalBytes)}", $"{drive.UsagePercent:0}% utilizado", drive.UsagePercent);
    }

    private void UpdateStatus(SystemInfo info, DriveInfoModel? drive)
    {
        if (info.TotalMemoryBytes == 0 && drive is null && info.CpuUsagePercent is null)
        {
            _status.Text = "NO DISPONIBLE";
            _statusDetail.Text = "No fue posible obtener indicadores suficientes para calcular el estado general.";
            _status.ForeColor = AppTheme.MutedText;
            return;
        }

        var worst = new[] { info.TotalMemoryBytes > 0 ? info.MemoryUsagePercent : 0, drive?.UsagePercent ?? 0, info.CpuUsagePercent ?? 0 }.Max();
        var result = worst switch
        {
            >= 90 => ("ALTO CONSUMO", "Uno o más recursos presentan un uso elevado. Revisa las aplicaciones abiertas y el espacio disponible.", Color.FromArgb(248, 113, 113)),
            >= 75 => ("MODERADO", "El equipo presenta una carga moderada. Conviene vigilar los recursos con mayor utilización.", Color.FromArgb(251, 191, 36)),
            >= 50 => ("BUENO", "El equipo funciona dentro de parámetros normales.", Color.FromArgb(52, 211, 153)),
            _ => ("EXCELENTE", "No se observan niveles elevados de consumo en este momento.", AppTheme.Primary)
        };
        _status.Text = result.Item1;
        _statusDetail.Text = result.Item2;
        _status.ForeColor = result.Item3;
    }

    private void SetBusy(bool value)
    {
        _refresh.Enabled = !value;
        _refresh.Text = value ? "Actualizando..." : "Actualizar";
        _activity.Text = value ? "Recopilando datos  " : string.Empty;
    }

    private void DisposeResources()
    {
        if (_resourcesDisposed) return;
        _resourcesDisposed = true;
        _timer.Stop();
        _timer.Dispose();
        _lifetime.Cancel();
        _lifetime.Dispose();
    }

    private void ResetScrollToTop()
    {
        AutoScrollPosition = Point.Empty;
        if (VerticalScroll.Visible) VerticalScroll.Value = VerticalScroll.Minimum;
        PerformLayout();
    }

    private void AddInfo(TableLayoutPanel grid, int row, string title, string key)
    {
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
        grid.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = AppTheme.MutedText, TextAlign = ContentAlignment.MiddleLeft }, 0, row);
        var value = new Label { Text = "Calculando...", AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.Text, TextAlign = ContentAlignment.MiddleLeft };
        _systemValues[key] = value;
        grid.Controls.Add(value, 1, row);
    }

    private static TableLayoutPanel Grid(int columns, int height, float[] widths)
    {
        var grid = new TableLayoutPanel { Dock = DockStyle.Fill, Height = height, ColumnCount = columns };
        foreach (var width in widths) grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
        return grid;
    }

    private static Label SectionTitle(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold), Location = new Point(20, 17) };
    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.MutedText, Margin = new Padding(0, 11, 14, 0) };
    private static Label ValueLabel(string text, float size) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", size, FontStyle.Bold) };
    private static string UsageText(double value) => value >= 90 ? "Uso muy alto" : value >= 75 ? "Uso alto" : value >= 50 ? "Uso moderado" : "Uso normal";
    private static string FormatBytes(ulong bytes) => bytes >= 1024d * 1024 * 1024 ? $"{bytes / (1024d * 1024 * 1024):0.#} GB" : $"{bytes / (1024d * 1024):0} MB";

    private sealed class MetricCard : RoundedPanel
    {
        private readonly Label _title;
        private readonly Label _value;
        private readonly Label _caption;
        private readonly UsageBar? _bar;

        public MetricCard(string title, string caption, bool showBar = true)
        {
            _title = new Label { Text = title, AutoSize = true, ForeColor = AppTheme.MutedText, Font = new Font("Segoe UI Semibold", 9F), Location = new Point(18, 12) };
            _value = new Label { Text = "Calculando...", AutoEllipsis = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold), Location = new Point(18, 40), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Size = new Size(220, 33) };
            _caption = new Label { Text = caption, AutoEllipsis = true, ForeColor = AppTheme.MutedText, Location = new Point(20, 79), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Size = new Size(220, 22) };
            Controls.Add(_caption); Controls.Add(_value); Controls.Add(_title);
            if (showBar) { _bar = new UsageBar { Location = new Point(20, 115), Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, Width = 218 }; Controls.Add(_bar); }
            Resize += (_, _) =>
            {
                var width = Math.Max(80, ClientSize.Width - 38);
                _value.Width = width;
                _caption.Width = width;
                if (_bar is not null) _bar.Width = Math.Max(40, ClientSize.Width - 40);
            };
        }

        public void SetTitle(string title) => _title.Text = title;
        public void SetValue(string value, string caption, double? percentage)
        {
            _value.Text = value; _caption.Text = caption;
            if (_bar is not null && percentage.HasValue)
            {
                _bar.Value = percentage.Value;
                _bar.FillColor = percentage >= 90 ? Color.FromArgb(248, 113, 113) : percentage >= 75 ? Color.FromArgb(251, 191, 36) : AppTheme.Primary;
            }
        }
        public void SetUnavailable() { _value.Text = "No disponible"; _caption.Text = "No fue posible obtener este dato"; if (_bar is not null) _bar.Value = 0; }
    }
}
