using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;
using System.Runtime.InteropServices;

namespace SystemAnalyzer.Forms;

public sealed class FormSistema : Form
{
    private readonly SystemInfoService _systemService = new();
    private readonly StorageAnalyzerService _storageService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Dictionary<string, Label> _values = new();
    private readonly FlowLayoutPanel _drives = new();
    private readonly UsageBar _memoryBar = new();
    private readonly Label _lastUpdate = TextLabel("Última actualización: --:--:--", true);
    private readonly Label _activity = TextLabel(string.Empty, true);
    private readonly Button _refresh = new();
    private TableLayoutPanel _headerGrid = null!;
    private TableLayoutPanel _topCardsGrid = null!;
    private TableLayoutPanel _memoryGrid = null!;
    private RoundedPanel _memoryCard = null!;
    private readonly List<Control> _topCards = new();
    private readonly List<Control> _memoryMetrics = new();
    private bool _refreshing;
    private bool _resourcesDisposed;

    public FormSistema()
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
        Shown += async (_, _) => await RefreshAsync();
        Disposed += (_, _) => DisposeResources();
    }

    private void BuildInterface()
    {
        var page = new TableLayoutPanel
        {
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, RowCount = 5,
            Dock = DockStyle.Top, Padding = new Padding(30, 24, 30, 30), BackColor = AppTheme.Background
        };
        page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 5; row++) page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        page.Controls.Add(BuildHeader(), 0, 0);
        page.Controls.Add(BuildTopCards(), 0, 1);
        page.Controls.Add(BuildMemoryCard(), 0, 2);
        page.Controls.Add(BuildEquipmentCard(), 0, 3);
        page.Controls.Add(BuildDrivesCard(), 0, 4);
        Controls.Add(page);
        ApplyResponsiveLayout();
    }

    private Control BuildHeader()
    {
        _headerGrid = CreateGrid(2, 82, 62, 38);
        _headerGrid.Margin = new Padding(0, 0, 0, 16);
        var title = new Panel { Dock = DockStyle.Fill };
        title.Controls.Add(new Label { Text = "Información del sistema", AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold), Location = new Point(0, 0) });
        title.Controls.Add(new Label { Text = "Detalles técnicos del equipo y sistema operativo", AutoSize = true, ForeColor = AppTheme.MutedText, Font = new Font("Segoe UI", 10F), Location = new Point(3, 45) });

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 5, 0, 0) };
        _refresh.Text = "Actualizar";
        _refresh.Size = new Size(112, 36);
        _refresh.FlatStyle = FlatStyle.Flat;
        _refresh.FlatAppearance.BorderSize = 0;
        _refresh.BackColor = AppTheme.Primary;
        _refresh.ForeColor = Color.FromArgb(8, 47, 73);
        _refresh.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _refresh.Cursor = Cursors.Hand;
        _refresh.Click += async (_, _) => await RefreshAsync();
        actions.Controls.Add(_refresh);
        actions.Controls.Add(_activity);
        actions.Controls.Add(_lastUpdate);
        _headerGrid.Controls.Add(title, 0, 0);
        _headerGrid.Controls.Add(actions, 1, 0);
        return _headerGrid;
    }

    private Control BuildTopCards()
    {
        _topCardsGrid = CreateGrid(2, 310, 55, 45);
        _topCardsGrid.Margin = new Padding(0, 0, 0, 16);
        var osCard = CreateInfoCard("Sistema operativo", new[]
        {
            ("Nombre", "osName"), ("Descripción", "osDescription"), ("Versión", "osVersion"),
            ("Versión visible", "osDisplayVersion"), ("Build", "osBuild"),
            ("Arquitectura", "osArchitecture"), ("Plataforma", "platform")
        }, Padding.Empty);
        var processorCard = CreateInfoCard("Procesador", new[]
        {
            ("Nombre", "processor"), ("Fabricante", "manufacturer"), ("Arquitectura", "processorArchitecture"),
            ("Núcleos físicos", "cores"), ("Procesadores lógicos", "logicalProcessors"), ("Frecuencia base", "frequency")
        }, Padding.Empty);
        _topCards.AddRange(new Control[] { osCard, processorCard });
        _topCardsGrid.Controls.Add(osCard, 0, 0);
        _topCardsGrid.Controls.Add(processorCard, 1, 0);
        return _topCardsGrid;
    }

    private Control BuildMemoryCard()
    {
        _memoryCard = new RoundedPanel { Dock = DockStyle.Fill, Height = 184, Margin = new Padding(0, 0, 0, 16) };
        _memoryCard.Controls.Add(SectionTitle("Memoria RAM"));
        _memoryGrid = new TableLayoutPanel { ColumnCount = 4, RowCount = 1, Location = new Point(20, 58), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Size = new Size(950, 75) };
        AddMetric(_memoryGrid, 0, "Total", "memoryTotal");
        AddMetric(_memoryGrid, 1, "Utilizada", "memoryUsed");
        AddMetric(_memoryGrid, 2, "Disponible", "memoryAvailable");
        AddMetric(_memoryGrid, 3, "Uso", "memoryPercent");
        _memoryBar.Location = new Point(20, 148);
        _memoryBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _memoryBar.Width = 950;
        _memoryCard.Controls.Add(_memoryBar);
        _memoryCard.Controls.Add(_memoryGrid);
        _memoryCard.Resize += (_, _) =>
        {
            _memoryGrid.Width = Math.Max(120, _memoryCard.ClientSize.Width - 40);
            _memoryBar.Width = Math.Max(80, _memoryCard.ClientSize.Width - 40);
        };
        return _memoryCard;
    }

    private Control BuildEquipmentCard()
    {
        var card = CreateInfoCard("Información del equipo", new[]
        {
            ("Nombre del equipo", "computer"), ("Usuario actual", "user"), ("Arquitectura del SO", "equipmentArchitecture"),
            ("Directorio de Windows", "windowsDirectory"), ("Directorio del sistema", "systemDirectory"),
            ("Procesadores lógicos", "equipmentLogicalProcessors"), ("Tiempo encendido", "uptime")
        }, new Padding(0, 0, 0, 16));
        card.Height = 310;
        return card;
    }

    private Control BuildDrivesCard()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, AutoSize = true, MinimumSize = new Size(0, 185), Margin = new Padding(0) };
        card.Controls.Add(SectionTitle("Unidades del sistema"));
        _drives.FlowDirection = FlowDirection.LeftToRight;
        _drives.WrapContents = true;
        _drives.AutoSize = true;
        _drives.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _drives.Location = new Point(15, 55);
        _drives.Size = new Size(970, 110);
        _drives.Controls.Add(TextLabel("Calculando...", true));
        card.Controls.Add(_drives);
        return card;
    }

    private RoundedPanel CreateInfoCard(string title, (string Label, string Key)[] fields, Padding margin)
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Margin = margin };
        card.Controls.Add(SectionTitle(title));
        var grid = new TableLayoutPanel { ColumnCount = 2, RowCount = fields.Length, Location = new Point(20, 55), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Size = new Size(520, Math.Max(100, fields.Length * 33)) };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
        for (var row = 0; row < fields.Length; row++)
        {
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 33));
            grid.Controls.Add(new Label { Text = fields[row].Label, Dock = DockStyle.Fill, ForeColor = AppTheme.MutedText, TextAlign = ContentAlignment.MiddleLeft }, 0, row);
            var value = new Label { Text = "Calculando...", AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.Text, TextAlign = ContentAlignment.MiddleLeft };
            _values[fields[row].Key] = value;
            grid.Controls.Add(value, 1, row);
        }
        card.Controls.Add(grid);
        card.Resize += (_, _) => grid.Width = Math.Max(120, card.ClientSize.Width - 40);
        return card;
    }

    private void AddMetric(TableLayoutPanel grid, int column, string title, string key)
    {
        var metric = new Panel { Dock = DockStyle.Fill };
        metric.Controls.Add(new Label { Text = title, AutoSize = true, ForeColor = AppTheme.MutedText, Location = new Point(0, 2) });
        var value = new Label { Text = "Calculando...", AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), Location = new Point(0, 26) };
        _values[key] = value;
        metric.Controls.Add(value);
        _memoryMetrics.Add(metric);
        grid.Controls.Add(metric, column, 0);
    }

    private void ApplyResponsiveLayout()
    {
        if (_topCardsGrid is null || _memoryGrid is null || _headerGrid is null) return;
        var usableWidth = Math.Max(1, ClientSize.Width - 60);

        ResponsiveLayout.Reflow(_topCardsGrid, _topCards, usableWidth >= 790 ? 2 : 1, 310);
        _topCardsGrid.Margin = new Padding(0, 0, 0, 16);

        var memoryColumns = usableWidth >= 720 ? 4 : usableWidth >= 400 ? 2 : 1;
        ResponsiveLayout.Reflow(_memoryGrid, _memoryMetrics, memoryColumns, 58, 4);
        _memoryBar.Top = _memoryGrid.Bottom + 10;
        _memoryCard.Height = _memoryBar.Bottom + 25;
        _memoryCard.MinimumSize = new Size(0, _memoryCard.Height);

        var headerControls = _headerGrid.Controls.Cast<Control>().ToArray();
        var headerColumns = usableWidth >= 700 ? 2 : 1;
        ResponsiveLayout.Reflow(_headerGrid, headerControls, headerColumns, headerColumns == 2 ? 82 : 66);
        _headerGrid.Margin = new Padding(0, 0, 0, 16);

        _drives.Width = Math.Max(200, usableWidth - 30);
    }

    private async Task RefreshAsync()
    {
        if (_refreshing || IsDisposed) return;
        _refreshing = true;
        SetBusy(true);
        try
        {
            var token = _lifetime.Token;
            var infoTask = _systemService.GetSummaryAsync(token);
            var drivesTask = _storageService.GetReadyDrivesAsync(token);
            await Task.WhenAll(infoTask, drivesTask);
            if (IsDisposed || token.IsCancellationRequested) return;
            ShowSystemInfo(infoTask.Result);
            ShowDrives(drivesTask.Result);
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException or ExternalException)
        {
            if (!IsDisposed) _activity.Text = "Actualización incompleta  ";
        }
        finally { if (!IsDisposed) SetBusy(false); _refreshing = false; }
    }

    private void ShowSystemInfo(SystemInfo info)
    {
        Set("osName", info.OsProductName);
        Set("osDescription", info.OsDescription);
        Set("osVersion", info.OsVersion);
        Set("osDisplayVersion", info.OsDisplayVersion);
        Set("osBuild", info.OsBuild);
        Set("osArchitecture", info.Architecture);
        Set("platform", info.Platform);
        Set("processor", info.Processor);
        Set("manufacturer", info.ProcessorManufacturer);
        Set("processorArchitecture", info.ProcessorArchitecture);
        Set("cores", info.PhysicalCoreCount?.ToString() ?? "No disponible");
        Set("logicalProcessors", info.LogicalProcessorCount.ToString());
        Set("frequency", info.ProcessorFrequency);
        Set("computer", info.ComputerName);
        Set("user", info.UserName);
        Set("equipmentArchitecture", info.Architecture);
        Set("windowsDirectory", info.WindowsDirectory);
        Set("systemDirectory", info.SystemDirectory);
        Set("equipmentLogicalProcessors", info.LogicalProcessorCount.ToString());
        Set("uptime", FormatUptime(info.Uptime));

        if (info.TotalMemoryBytes == 0)
        {
            Set("memoryTotal", "No disponible"); Set("memoryUsed", "No disponible");
            Set("memoryAvailable", "No disponible"); Set("memoryPercent", "No disponible");
            _memoryBar.Value = 0;
        }
        else
        {
            Set("memoryTotal", FormatBytes(info.TotalMemoryBytes));
            Set("memoryUsed", FormatBytes(info.TotalMemoryBytes - info.AvailableMemoryBytes));
            Set("memoryAvailable", FormatBytes(info.AvailableMemoryBytes));
            Set("memoryPercent", $"{info.MemoryUsagePercent:0}%");
            _memoryBar.Value = info.MemoryUsagePercent;
            _memoryBar.FillColor = UsageColor(info.MemoryUsagePercent);
        }
    }

    private void ShowDrives(IReadOnlyList<DriveInfoModel> drives)
    {
        _drives.SuspendLayout();
        _drives.Controls.Clear();
        if (drives.Count == 0) _drives.Controls.Add(TextLabel("No hay unidades listas disponibles.", true));
        foreach (var drive in drives)
        {
            var item = new Panel { BackColor = Color.FromArgb(38, 51, 70), Size = new Size(285, 96), Margin = new Padding(5) };
            item.Controls.Add(new Label { Text = drive.Name, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold), AutoSize = true, Location = new Point(13, 10) });
            item.Controls.Add(new Label { Text = $"{FormatBytes((ulong)drive.TotalBytes)} total · {FormatBytes((ulong)drive.UsedBytes)} utilizados\n{FormatBytes((ulong)drive.AvailableBytes)} disponibles · {drive.UsagePercent:0}% en uso", ForeColor = AppTheme.MutedText, AutoSize = true, Location = new Point(15, 39) });
            var bar = new UsageBar { Location = new Point(15, 79), Width = 255, Value = drive.UsagePercent, FillColor = UsageColor(drive.UsagePercent) };
            item.Controls.Add(bar);
            _drives.Controls.Add(item);
        }
        _drives.ResumeLayout();
    }

    private void Set(string key, string value) => _values[key].Text = string.IsNullOrWhiteSpace(value) ? "No disponible" : value;
    private void SetBusy(bool busy) { _refresh.Enabled = !busy; _refresh.Text = busy ? "Actualizando..." : "Actualizar"; _activity.Text = busy ? "Recopilando datos  " : string.Empty; }
    private void DisposeResources() { if (_resourcesDisposed) return; _resourcesDisposed = true; _lifetime.Cancel(); _lifetime.Dispose(); }
    private static string FormatBytes(ulong bytes) => bytes >= 1024d * 1024 * 1024 ? $"{bytes / (1024d * 1024 * 1024):0.#} GB" : $"{bytes / (1024d * 1024):0} MB";
    private static string FormatUptime(TimeSpan value) => $"{value.Days} {(value.Days == 1 ? "día" : "días")}, {value.Hours} horas, {value.Minutes} minutos";
    private static Color UsageColor(double value) => value >= 90 ? Color.FromArgb(248, 113, 113) : value >= 75 ? Color.FromArgb(251, 191, 36) : AppTheme.Primary;
    private static Label SectionTitle(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.Text, Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold), Location = new Point(20, 17) };
    private static Label TextLabel(string text, bool muted) => new() { Text = text, AutoSize = true, ForeColor = muted ? AppTheme.MutedText : AppTheme.Text, Margin = new Padding(0, 11, 14, 0) };
    private static TableLayoutPanel CreateGrid(int columns, int height, params float[] widths)
    {
        var grid = new TableLayoutPanel { Dock = DockStyle.Fill, Height = height, ColumnCount = columns };
        foreach (var width in widths) grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
        return grid;
    }
}
