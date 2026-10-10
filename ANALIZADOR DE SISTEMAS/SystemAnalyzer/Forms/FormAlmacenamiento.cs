using System.Security;
using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public sealed class FormAlmacenamiento : Form
{
    private readonly StorageAnalyzerService _storageService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly ThemedButton _refresh = new();
    private readonly ToolTip _toolTip = new();
    private readonly Label _lastUpdate = Muted("Última actualización: --:--:--");
    private readonly Label _activity = Muted(string.Empty);
    private readonly SummaryCard _driveCount = new("Unidades disponibles", "Calculando...");
    private readonly SummaryCard _totalCapacity = new("Capacidad combinada", "Calculando...");
    private readonly SummaryCard _availableSpace = new("Espacio disponible", "Calculando...");
    private readonly List<Control> _summaryCards = new();
    private readonly List<Control> _driveCards = new();
    private readonly Panel _scrollHost = new();
    private readonly Label _recommendations = WrappingLabel("Las recomendaciones aparecerán después de analizar las unidades disponibles.");
    private TableLayoutPanel _page = null!;
    private TableLayoutPanel _header = null!;
    private TableLayoutPanel _summary = null!;
    private TableLayoutPanel _driveList = null!;
    private RoundedPanel _drivesSection = null!;
    private bool _refreshing;
    private bool _resourcesDisposed;
    private bool _applyingLayout;

    public FormAlmacenamiento()
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
        Disposed += HandleDisposed;
    }

    private void BuildInterface()
    {
        _scrollHost.Dock = DockStyle.Fill;
        _scrollHost.AutoScroll = true;
        _scrollHost.BackColor = AppTheme.BackgroundPrimary;
        _page = new TableLayoutPanel
        {
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, RowCount = 4,
            Dock = DockStyle.Top, Padding = new Padding(24, 16, 24, 24), BackColor = AppTheme.BackgroundPrimary
        };
        _page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 4; row++) _page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _page.Controls.Add(BuildHeader(), 0, 0);
        _page.Controls.Add(BuildSummary(), 0, 1);
        _page.Controls.Add(BuildDrivesSection(), 0, 2);
        _page.Controls.Add(BuildRecommendations(), 0, 3);
        _scrollHost.Controls.Add(_page);
        Controls.Add(_scrollHost);
        ApplyResponsiveLayout();
    }

    private Control BuildHeader()
    {
        _header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Height = 80, Margin = new Padding(0, 0, 0, 12) };
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
        var titles = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        titles.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        titles.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        titles.Controls.Add(new Label { Text = "Análisis de almacenamiento", Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true }, 0, 0);
        titles.Controls.Add(WrappingLabel("Capacidad, ocupación y espacio disponible de las unidades accesibles."), 0, 1);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 0) };
        _refresh.Text = "Actualizar";
        _refresh.Size = new Size(120, 40);
        _refresh.TabIndex = 0;
        _refresh.AccessibleName = "Actualizar análisis de almacenamiento";
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
        _summaryCards.AddRange(new Control[] { _driveCount, _totalCapacity, _availableSpace });
        for (var index = 0; index < _summaryCards.Count; index++) _summary.Controls.Add(_summaryCards[index], index, 0);
        return _summary;
    }

    private Control BuildDrivesSection()
    {
        _drivesSection = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 12) };
        var layout = new TableLayoutPanel
        {
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Top, ColumnCount = 1, RowCount = 3,
            Padding = new Padding(20, 16, 20, 18), BackColor = AppTheme.SurfacePrimary
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.Controls.Add(SectionTitle("Unidades disponibles"), 0, 0);
        layout.Controls.Add(WrappingLabel("Solo se incluyen unidades listas y accesibles. Una unidad desconectada o sin acceso no se clasifica como saludable."), 0, 1);
        _driveList = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Top, ColumnCount = 1, RowCount = 1, Margin = new Padding(0, 8, 0, 0) };
        _driveList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _driveList.Controls.Add(CreateStateLabel("Recopilando información de las unidades...", AppTheme.TextSecondary), 0, 0);
        layout.Controls.Add(_driveList, 0, 2);
        _drivesSection.Controls.Add(layout);
        return _drivesSection;
    }

    private Control BuildRecommendations()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, MinimumSize = new Size(0, 184), Margin = Padding.Empty };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(20, 16, 20, 18), BackColor = AppTheme.SurfacePrimary };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        layout.Controls.Add(SectionTitle("Recomendaciones preventivas"), 0, 0);
        _recommendations.Margin = Padding.Empty;
        layout.Controls.Add(_recommendations, 0, 1);
        layout.Controls.Add(WrappingLabel("Los umbrales de ocupación son orientativos del proyecto; no diagnostican desgaste ni fallos físicos del dispositivo."), 0, 2);
        card.Controls.Add(layout);
        return card;
    }

    private async void HandleShown(object? sender, EventArgs e)
    {
        ResetScrollToTop();
        await RefreshStorageAsync();
        if (!IsDisposed && IsHandleCreated) BeginInvoke(ResetScrollToTop);
    }

    private async void HandleRefreshClick(object? sender, EventArgs e) => await RefreshStorageAsync();
    private void HandleClientSizeChanged(object? sender, EventArgs e) => ApplyResponsiveLayout();
    private void HandleDisposed(object? sender, EventArgs e) => DisposeResources();

    private async Task RefreshStorageAsync()
    {
        if (_refreshing || IsDisposed) return;
        _refreshing = true;
        SetBusy(true);
        try
        {
            var token = _lifetime.Token;
            var drives = await _storageService.GetReadyDrivesAsync(token);
            if (IsDisposed || token.IsCancellationRequested) return;
            ShowDrives(drives);
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
            _activity.Text = drives.Count == 0 ? "Sin unidades accesibles  " : "Datos actualizados  ";
            _activity.ForeColor = drives.Count == 0 ? AppTheme.StatusWarning : AppTheme.StatusSuccess;
        }
        catch (OperationCanceledException)
        {
            if (!IsDisposed) { _activity.Text = "Actualización cancelada  "; _activity.ForeColor = AppTheme.StatusWarning; }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or SecurityException)
        {
            if (!IsDisposed) { _activity.Text = "No fue posible actualizar; se conservan los datos anteriores  "; _activity.ForeColor = AppTheme.StatusError; }
        }
        finally
        {
            if (!IsDisposed) RestoreRefreshButton();
            _refreshing = false;
        }
    }

    private void ShowDrives(IReadOnlyList<DriveInfoModel> drives)
    {
        ClearDriveCards();
        var aggregate = SummarizeValidDrives(drives);
        var worstState = StorageState.Normal;
        foreach (var drive in drives)
        {
            var usage = CalculateUsagePercent(drive.TotalBytes, drive.AvailableBytes);
            var state = ClassifyUsage(usage);
            if (Severity(state) > Severity(worstState)) worstState = state;
            _driveCards.Add(CreateDriveCard(drive, usage, state));
        }
        if (_driveCards.Count == 0)
        {
            _driveList.Controls.Add(CreateStateLabel("No se encontraron unidades listas y accesibles. Las unidades desconectadas o sin acceso no se evaluaron.", AppTheme.StatusWarning), 0, 0);
            _driveList.RowCount = 1;
            _driveList.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        }
        else
        {
            foreach (var card in _driveCards) _driveList.Controls.Add(card);
        }
        _driveCount.SetValue($"{aggregate.ValidCount:N0} de {drives.Count:N0}", "Unidades evaluadas con datos válidos");
        _totalCapacity.SetValue(aggregate.ValidCount == 0 ? "No disponible" : FormatBytes(aggregate.TotalBytes), "Suma exclusiva de unidades evaluadas");
        _availableSpace.SetValue(aggregate.ValidCount == 0 ? "No disponible" : FormatBytes(aggregate.AvailableBytes), "Disponible en unidades evaluadas");
        _recommendations.Text = RecommendationsFor(drives.Count == 0 ? StorageState.Unknown : worstState);
        ApplyResponsiveLayout();
    }

    private RoundedPanel CreateDriveCard(DriveInfoModel drive, double? usage, StorageState state)
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, MinimumSize = new Size(0, 184) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 6, Padding = new Padding(18, 14, 18, 14), BackColor = AppTheme.SurfaceSecondary };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 14));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var heading = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Margin = Padding.Empty };
        heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
        heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        var name = ValueLabel(string.IsNullOrWhiteSpace(drive.Name) ? "Unidad no identificada" : drive.Name, true);
        var type = ValueLabel(DriveTypeName(drive.DriveType), false);
        type.TextAlign = ContentAlignment.MiddleRight;
        heading.Controls.Add(name, 0, 0);
        heading.Controls.Add(type, 1, 0);
        layout.Controls.Add(heading, 0, 0);
        if (usage.HasValue)
        {
            var available = drive.AvailableBytes;
            var used = drive.TotalBytes - available;
            layout.Controls.Add(InfoLine("Capacidad total", FormatBytes(drive.TotalBytes)), 0, 1);
            layout.Controls.Add(InfoLine("Utilizado", $"{FormatBytes(used)} · {usage.Value:0.#}%"), 0, 2);
            layout.Controls.Add(InfoLine("Disponible", FormatBytes(available)), 0, 3);
        }
        else
        {
            layout.Controls.Add(InfoLine("Capacidad total", "No disponible"), 0, 1);
            layout.Controls.Add(InfoLine("Utilizado", "No disponible"), 0, 2);
            layout.Controls.Add(InfoLine("Disponible", "No disponible"), 0, 3);
        }
        var bar = new UsageBar { Dock = DockStyle.Top, Margin = new Padding(0, 4, 0, 0), Value = usage ?? 0, FillColor = StateColor(state), AccessibleName = $"Ocupación de {drive.Name}", AccessibleDescription = usage.HasValue ? $"{usage.Value:0.#} por ciento utilizado" : "Ocupación no disponible" };
        layout.Controls.Add(bar, 0, 4);
        var status = WrappingLabel(StateDescription(state));
        status.ForeColor = StateColor(state);
        status.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        status.Margin = new Padding(0, 4, 0, 0);
        layout.Controls.Add(status, 0, 5);
        _toolTip.SetToolTip(name, name.Text);
        _toolTip.SetToolTip(type, type.Text);
        card.Controls.Add(layout);
        return card;
    }

    private void ApplyResponsiveLayout()
    {
        if (_applyingLayout || _page is null || _header is null || _summary is null || _driveList is null) return;
        _applyingLayout = true;
        try
        {
            _page.Padding = _scrollHost.ClientSize.Width < 700 ? new Padding(16) : new Padding(24, 16, 24, 24);
            var usableWidth = Math.Max(1, _scrollHost.ClientSize.Width - _page.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth);
            ResponsiveLayout.Reflow(_summary, _summaryCards, usableWidth >= 850 ? 3 : 1, 108, 6);
            var headerControls = _header.Controls.Cast<Control>().ToArray();
            _lastUpdate.Visible = usableWidth >= 600;
            ResponsiveLayout.Reflow(_header, headerControls, usableWidth >= 820 ? 2 : 1, 80, 4);
            _header.Margin = new Padding(0, 0, 0, 12);
            if (_driveCards.Count > 0) ResponsiveLayout.Reflow(_driveList, _driveCards, usableWidth >= 900 ? 2 : 1, 184, 6);
            _drivesSection.MinimumSize = Size.Empty;
            _drivesSection.Height = 16 + 34 + 38 + 8 + _driveList.Height + 18;
            _drivesSection.MinimumSize = new Size(0, _drivesSection.Height);
            _page.PerformLayout();
            _scrollHost.AutoScrollMinSize = new Size(0, _page.PreferredSize.Height);
        }
        finally { _applyingLayout = false; }
    }

    private void ClearDriveCards()
    {
        foreach (Control control in _driveList.Controls.Cast<Control>().ToArray()) control.Dispose();
        _driveList.Controls.Clear();
        _driveList.RowStyles.Clear();
        _driveList.ColumnStyles.Clear();
        _driveList.RowCount = 0;
        _driveList.ColumnCount = 1;
        _driveList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _driveCards.Clear();
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
        _refresh.Text = busy ? "Actualizando..." : "Actualizar";
        if (busy) { _activity.Text = "Recopilando unidades...  "; _activity.ForeColor = AppTheme.TextSecondary; }
    }

    private void RestoreRefreshButton() { _refresh.Enabled = true; _refresh.Text = "Actualizar"; }

    private void DisposeResources()
    {
        if (_resourcesDisposed) return;
        _resourcesDisposed = true;
        _refresh.Click -= HandleRefreshClick;
        ClientSizeChanged -= HandleClientSizeChanged;
        Shown -= HandleShown;
        Disposed -= HandleDisposed;
        _lifetime.Cancel();
        _lifetime.Dispose();
        _toolTip.Dispose();
    }

    internal static double? CalculateUsagePercent(long totalBytes, long availableBytes)
    {
        if (!HasValidStorageData(totalBytes, availableBytes)) return null;
        var usage = (totalBytes - availableBytes) * 100d / totalBytes;
        return double.IsFinite(usage) ? Math.Clamp(usage, 0, 100) : null;
    }

    internal static bool HasValidStorageData(long totalBytes, long availableBytes) =>
        totalBytes > 0 && availableBytes >= 0 && availableBytes <= totalBytes;

    internal static (int ValidCount, decimal TotalBytes, decimal AvailableBytes) SummarizeValidDrives(IEnumerable<DriveInfoModel> drives)
    {
        var validCount = 0;
        decimal totalBytes = 0;
        decimal availableBytes = 0;
        foreach (var drive in drives)
        {
            if (!HasValidStorageData(drive.TotalBytes, drive.AvailableBytes)) continue;
            validCount++;
            totalBytes += drive.TotalBytes;
            availableBytes += drive.AvailableBytes;
        }
        return (validCount, totalBytes, availableBytes);
    }

    internal static string UsageStateName(double? usage) => ClassifyUsage(usage) switch
    {
        StorageState.Normal => "Normal", StorageState.Attention => "Atención",
        StorageState.Critical => "Espacio crítico", _ => "No disponible"
    };

    private static StorageState ClassifyUsage(double? usage)
    {
        if (!usage.HasValue || !double.IsFinite(usage.Value) || usage.Value < 0) return StorageState.Unknown;
        return usage.Value >= 90 ? StorageState.Critical : usage.Value >= 80 ? StorageState.Attention : StorageState.Normal;
    }

    private static int Severity(StorageState state) => state switch { StorageState.Critical => 3, StorageState.Attention => 2, StorageState.Unknown => 1, _ => 0 };
    private static string StateDescription(StorageState state) => state switch
    {
        StorageState.Normal => "Normal · menos del 80% utilizado.",
        StorageState.Attention => "Atención · entre 80% y menos de 90% utilizado.",
        StorageState.Critical => "Espacio crítico · 90% o más utilizado.",
        _ => "Estado no disponible · los datos de capacidad o espacio disponible son inconsistentes."
    };
    private static Color StateColor(StorageState state) => state switch
    {
        StorageState.Normal => AppTheme.StatusSuccess, StorageState.Attention => AppTheme.StatusWarning,
        StorageState.Critical => AppTheme.StatusError, _ => AppTheme.TextDisabled
    };
    private static string RecommendationsFor(StorageState state) => state switch
    {
        StorageState.Critical => "• Revisa archivos grandes y conserva espacio para actualizaciones.\n• Considera liberar espacio con las herramientas oficiales de Windows.\n• Revisa manualmente Descargas y la Papelera de reciclaje antes de eliminar contenido.\n• Mantén copias de seguridad de la información importante.",
        StorageState.Attention => "• Revisa archivos grandes y el crecimiento del espacio utilizado.\n• Mantén espacio disponible para actualizaciones de Windows.\n• Revisa manualmente Descargas y la Papelera de reciclaje cuando sea necesario.\n• Conserva copias de seguridad de la información importante.",
        StorageState.Normal => "• La ocupación actual está bajo los umbrales preventivos.\n• Mantén espacio disponible para futuras actualizaciones.\n• Conserva copias de seguridad periódicas de la información importante.",
        _ => "• Verifica manualmente que las unidades estén conectadas y accesibles.\n• No se emitió una clasificación sin capacidad válida.\n• Conserva copias de seguridad de la información importante."
    };
    private static string DriveTypeName(string value) => value switch
    {
        "Fixed" => "Unidad fija", "Removable" => "Unidad removible", "Network" => "Unidad de red",
        "CDRom" => "Unidad óptica", "Ram" => "Unidad RAM", "NoRootDirectory" => "Sin directorio raíz",
        "Unknown" or "No disponible" or "" => "Tipo no disponible", _ => value
    };

    private static Control InfoLine(string title, string value)
    {
        var row = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Margin = Padding.Empty };
        row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
        row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
        row.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty }, 0, 0);
        row.Controls.Add(new Label { Text = value, Dock = DockStyle.Fill, ForeColor = value == "No disponible" ? AppTheme.TextDisabled : AppTheme.TextPrimary, TextAlign = ContentAlignment.MiddleRight, Margin = Padding.Empty, AutoEllipsis = true }, 1, 0);
        return row;
    }
    private static Label ValueLabel(string text, bool emphasized) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = emphasized ? new Font("Segoe UI Semibold", 12F, FontStyle.Bold) : new Font("Segoe UI", 9F), TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true, Margin = Padding.Empty };
    private static Label CreateStateLabel(string text, Color color) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = color, TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(4) };
    private static Label SectionTitle(string text) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
    private static Label WrappingLabel(string text) => new() { Text = text, Dock = DockStyle.Fill, AutoSize = false, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft };
    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.TextSecondary, Margin = new Padding(0, 11, 12, 0) };
    private static string FormatBytes(long bytes) => FormatBytes((decimal)Math.Max(0, bytes));
    private static string FormatBytes(decimal bytes)
    {
        const decimal gigabyte = 1024m * 1024 * 1024;
        const decimal megabyte = 1024m * 1024;
        return bytes >= gigabyte ? $"{bytes / gigabyte:0.##} GB" : $"{bytes / megabyte:0.##} MB";
    }

    private enum StorageState { Unknown, Normal, Attention, Critical }

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
            _value = new Label { Text = value, Dock = DockStyle.Fill, AutoEllipsis = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            _caption = new Label { Dock = DockStyle.Fill, AutoEllipsis = true, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.TopLeft, Margin = Padding.Empty };
            layout.Controls.Add(_value, 0, 1);
            layout.Controls.Add(_caption, 0, 2);
            Controls.Add(layout);
        }
        public void SetValue(string value, string caption) { _value.Text = value; _value.ForeColor = value == "No disponible" ? AppTheme.TextDisabled : AppTheme.TextPrimary; _caption.Text = caption; }
    }
}
