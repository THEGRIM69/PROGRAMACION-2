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
    private readonly ThemedButton _refresh = new();
    private readonly MetricCard _cpu = new("CPU", "Midiéndose mediante una muestra real", true, AppTheme.AccentPrimary);
    private readonly MetricCard _memory = new("Memoria RAM", "Memoria física utilizada", true, AppTheme.AccentSecondary);
    private readonly MetricCard _storage = new("Almacenamiento", "Unidad principal del sistema", true, AppTheme.StatusInfo);
    private readonly MetricCard _processes = new("Procesos activos", "Procesos actualmente en ejecución", false, AppTheme.AccentPrimary);
    private readonly Label _status = ValueLabel("CALCULANDO...", 18);
    private readonly Label _statusDetail = Muted("Recopilando indicadores del equipo.");
    private readonly Dictionary<string, Label> _systemValues = new();
    private readonly Label[] _topProcessNames = new Label[3];
    private readonly Label[] _topProcessValues = new Label[3];
    private TableLayoutPanel _page = null!;
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
        BackColor = AppTheme.BackgroundPrimary;
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
        _page = new TableLayoutPanel
        {
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, RowCount = 4,
            Dock = DockStyle.Top, Padding = new Padding(24, 16, 24, 24), BackColor = AppTheme.BackgroundPrimary
        };
        _page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 4; row++) _page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _page.Controls.Add(BuildHeader(), 0, 0);
        _page.Controls.Add(BuildCards(), 0, 1);
        _page.Controls.Add(BuildDetails(), 0, 2);
        _page.Controls.Add(BuildRanking(), 0, 3);
        Controls.Add(_page);
        ApplyResponsiveLayout();
    }

    private Control BuildHeader()
    {
        _headerGrid = Grid(2, 68, new[] { 60F, 40F });
        _headerGrid.Margin = new Padding(0, 0, 0, 12);
        var titles = new Panel { Dock = DockStyle.Fill };
        titles.Controls.Add(new Label { Text = "Panel de rendimiento", AutoSize = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), Location = new Point(0, 0) });
        titles.Controls.Add(new Label { Text = "Resumen del estado actual de tu equipo", AutoSize = true, ForeColor = AppTheme.TextSecondary, Font = new Font("Segoe UI", 9F), Location = new Point(3, 40) });

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 0) };
        _refresh.Text = "Actualizar";
        _refresh.Size = new Size(120, 40);
        _refresh.TabIndex = 0;
        _refresh.AccessibleName = "Actualizar panel de rendimiento";
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
        _cardsGrid = Grid(4, 152, new[] { 25F, 25F, 25F, 25F });
        _cardsGrid.Margin = new Padding(0, 0, 0, 12);
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
        _detailsGrid = Grid(2, 240, new[] { 38F, 62F });
        _detailsGrid.Margin = new Padding(0, 0, 0, 12);
        var health = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 6, 0), MinimumSize = new Size(0, 196) };
        var healthLayout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 18), ColumnCount = 1, RowCount = 3, BackColor = AppTheme.SurfacePrimary };
        healthLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        healthLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        healthLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        healthLayout.Controls.Add(SectionTitle("Estado general"), 0, 0);
        _status.Dock = DockStyle.Fill;
        _status.AutoSize = false;
        _status.TextAlign = ContentAlignment.MiddleLeft;
        _status.ForeColor = AppTheme.AccentPrimary;
        _statusDetail.Dock = DockStyle.Fill;
        _statusDetail.AutoSize = false;
        _statusDetail.Margin = new Padding(2, 4, 0, 0);
        _statusDetail.TextAlign = ContentAlignment.TopLeft;
        healthLayout.Controls.Add(_status, 0, 1);
        healthLayout.Controls.Add(_statusDetail, 0, 2);
        health.Controls.Add(healthLayout);

        var information = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(6, 0, 0, 0), MinimumSize = new Size(0, 240) };
        var informationLayout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 16), ColumnCount = 1, RowCount = 2, BackColor = AppTheme.SurfacePrimary };
        informationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        informationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        informationLayout.Controls.Add(SectionTitle("Información rápida"), 0, 0);
        var rows = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 5, Margin = Padding.Empty };
        rows.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
        rows.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
        rows.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        rows.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        rows.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        rows.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        rows.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        AddInfo(rows, 0, "Sistema operativo", "os");
        AddInfo(rows, 1, "Arquitectura", "architecture");
        AddInfo(rows, 2, "Procesador", "processor");
        AddInfo(rows, 3, "RAM instalada", "ram");
        AddInfo(rows, 4, "Equipo", "computer");
        informationLayout.Controls.Add(rows, 0, 1);
        information.Controls.Add(informationLayout);
        _detailCards.AddRange(new Control[] { health, information });
        _detailsGrid.Controls.Add(health, 0, 0);
        _detailsGrid.Controls.Add(information, 1, 0);
        return _detailsGrid;
    }

    private Control BuildRanking()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Height = 196, MinimumSize = new Size(0, 196), Margin = Padding.Empty };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 16), ColumnCount = 1, RowCount = 5, BackColor = AppTheme.SurfacePrimary };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        for (var row = 0; row < 3; row++) layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(SectionTitle("Mayor consumo de memoria"), 0, 0);
        layout.Controls.Add(new Label { Text = "Tres procesos con mayor memoria física en la medición actual", Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
        for (var i = 0; i < 3; i++)
        {
            var row = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = Padding.Empty };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            _topProcessNames[i] = new Label { Text = $"{i + 1}.  Calculando...", Dock = DockStyle.Fill, AutoEllipsis = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 9.5F), TextAlign = ContentAlignment.MiddleLeft };
            _topProcessValues[i] = new Label { Text = "—", Dock = DockStyle.Fill, ForeColor = AppTheme.AccentSecondary, Font = new Font("Segoe UI Semibold", 9.5F), TextAlign = ContentAlignment.MiddleRight };
            row.Controls.Add(_topProcessNames[i], 0, 0);
            row.Controls.Add(_topProcessValues[i], 1, 0);
            layout.Controls.Add(row, 0, i + 2);
        }
        card.Controls.Add(layout);
        return card;
    }

    private void ApplyResponsiveLayout()
    {
        if (_cardsGrid is null || _detailsGrid is null || _headerGrid is null) return;
        var compactPadding = ClientSize.Width < 700;
        _page.Padding = compactPadding ? new Padding(16) : new Padding(24, 16, 24, 24);
        var usableWidth = Math.Max(1, ClientSize.Width - _page.Padding.Horizontal);
        var cardColumns = usableWidth >= 1020 ? 4 : usableWidth >= 560 ? 2 : 1;
        ResponsiveLayout.Reflow(_cardsGrid, _metricCards, cardColumns, 152, 6);

        var detailColumns = usableWidth >= 760 ? 2 : 1;
        ResponsiveLayout.ReflowByContent(_detailsGrid, _detailCards, detailColumns, 240, 6);

        var headerControls = _headerGrid.Controls.Cast<Control>().ToArray();
        var headerColumns = usableWidth >= 850 ? 2 : 1;
        var subtitle = headerControls.FirstOrDefault()?.Controls.OfType<Label>().LastOrDefault();
        if (subtitle is not null) subtitle.Visible = usableWidth >= 600;
        ResponsiveLayout.Reflow(_headerGrid, headerControls, headerColumns, 68, 4);
        _headerGrid.Margin = new Padding(0, 0, 0, 12);
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
            if (!IsDisposed)
            {
                _activity.Text = "Actualización incompleta  ";
                _activity.ForeColor = AppTheme.StatusError;
            }
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
        {
            _topProcessNames[i].Text = i < top.Length ? $"{i + 1}.  {top[i].Name}.exe" : $"{i + 1}.  No disponible";
            _topProcessValues[i].Text = i < top.Length ? FormatBytes((ulong)top[i].WorkingSetBytes) : "—";
        }
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
            _status.ForeColor = AppTheme.TextSecondary;
            return;
        }

        var worst = new[] { info.TotalMemoryBytes > 0 ? info.MemoryUsagePercent : 0, drive?.UsagePercent ?? 0, info.CpuUsagePercent ?? 0 }.Max();
        var result = worst switch
        {
            >= 90 => ("ALTO CONSUMO", "Uno o más recursos presentan un uso elevado en esta muestra. Revisa las aplicaciones abiertas y el espacio disponible.", AppTheme.StatusError),
            >= 75 => ("MODERADO", "El equipo presenta una carga moderada en esta muestra. Conviene observar los recursos con mayor utilización.", AppTheme.StatusWarning),
            >= 50 => ("BUENO", "El equipo funciona dentro de parámetros normales.", AppTheme.StatusSuccess),
            _ => ("EXCELENTE", "No se observan niveles elevados de consumo en este momento.", AppTheme.AccentPrimary)
        };
        _status.Text = result.Item1;
        _statusDetail.Text = result.Item2;
        _status.ForeColor = result.Item3;
    }

    private void SetBusy(bool value)
    {
        _refresh.Enabled = !value;
        _refresh.Text = value ? "Actualizando..." : "Actualizar";
        if (value)
        {
            _activity.Text = "Recopilando datos  ";
            _activity.ForeColor = AppTheme.TextSecondary;
        }
        else if (_activity.Text == "Recopilando datos  ")
        {
            _activity.Text = string.Empty;
        }
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
        grid.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(0, 0, 12, 0) }, 0, row);
        var value = new Label { Text = "Calculando...", AutoEllipsis = row != 2, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
        _systemValues[key] = value;
        grid.Controls.Add(value, 1, row);
    }

    private static TableLayoutPanel Grid(int columns, int height, float[] widths)
    {
        var grid = new TableLayoutPanel { Dock = DockStyle.Fill, Height = height, ColumnCount = columns };
        foreach (var width in widths) grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
        return grid;
    }

    private static Label SectionTitle(string text) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.TextSecondary, Margin = new Padding(0, 11, 14, 0) };
    private static Label ValueLabel(string text, float size) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", size, FontStyle.Bold) };
    private static string UsageText(double value) => value >= 90 ? "Uso muy alto" : value >= 75 ? "Uso alto" : value >= 50 ? "Uso moderado" : "Uso normal";
    private static string FormatBytes(ulong bytes) => bytes >= 1024d * 1024 * 1024 ? $"{bytes / (1024d * 1024 * 1024):0.#} GB" : $"{bytes / (1024d * 1024):0} MB";

    private sealed class MetricCard : RoundedPanel
    {
        private readonly Label _title;
        private readonly Label _value;
        private readonly Label _caption;
        private readonly UsageBar? _bar;
        private readonly Color _accentColor;

        public MetricCard(string title, string caption, bool showBar, Color accentColor)
        {
            _accentColor = accentColor;
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 14, 20, 14), ColumnCount = 1, RowCount = 4, BackColor = AppTheme.SurfacePrimary };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, showBar ? 16 : 0));
            _title = new Label { Text = title, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, Font = new Font("Segoe UI Semibold", 10F), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            _value = new Label { Text = "Calculando...", AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            _caption = new Label { Text = caption, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.TopLeft, Margin = Padding.Empty };
            layout.Controls.Add(_title, 0, 0);
            layout.Controls.Add(_value, 0, 1);
            layout.Controls.Add(_caption, 0, 2);
            if (showBar)
            {
                _bar = new UsageBar { Dock = DockStyle.Top, Margin = new Padding(0, 5, 0, 0), FillColor = accentColor };
                layout.Controls.Add(_bar, 0, 3);
            }
            Controls.Add(layout);
        }

        public void SetTitle(string title) => _title.Text = title;
        public void SetValue(string value, string caption, double? percentage)
        {
            _value.Text = value; _caption.Text = caption;
            if (_bar is not null && percentage.HasValue)
            {
                _bar.Value = percentage.Value;
                _bar.FillColor = percentage >= 90 ? AppTheme.StatusError : percentage >= 75 ? AppTheme.StatusWarning : _accentColor;
            }
        }
        public void SetUnavailable() { _value.Text = "No disponible"; _caption.Text = "No fue posible obtener este dato"; if (_bar is not null) _bar.Value = 0; }
    }
}
