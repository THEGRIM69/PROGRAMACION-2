using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public sealed class FormDiagnostico : Form
{
    private readonly SystemInfoService _systemService = new();
    private readonly StorageAnalyzerService _storageService = new();
    private readonly ProcessAnalyzerService _processService = new();
    private readonly DiagnosticService _diagnosticService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly ThemedButton _refresh = new();
    private readonly Panel _scrollHost = new();
    private readonly Label _activity = Muted("Preparando diagnóstico...");
    private readonly Label _lastUpdate = Muted("Última actualización: --:--:--");
    private readonly RoundedPanel _overallCard = new();
    private readonly Label _overallState = ValueLabel("Calculando...");
    private readonly Label _overallSummary = WrappingLabel("Recopilando mediciones disponibles del equipo.");
    private readonly TableLayoutPanel _indicatorGrid = new();
    private readonly RoundedPanel _indicatorsSection = new();
    private readonly List<Control> _indicatorCards = new();
    private TableLayoutPanel _page = null!;
    private TableLayoutPanel _header = null!;
    private bool _refreshing;
    private bool _resourcesDisposed;
    private bool _applyingLayout;

    public FormDiagnostico()
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
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Top,
            ColumnCount = 1, RowCount = 3, Padding = new Padding(24, 16, 24, 24), BackColor = AppTheme.BackgroundPrimary
        };
        _page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 3; row++) _page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _page.Controls.Add(BuildHeader(), 0, 0);
        _page.Controls.Add(BuildOverallCard(), 0, 1);
        _page.Controls.Add(BuildIndicatorsSection(), 0, 2);
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
        titles.Controls.Add(new Label { Text = "Diagnóstico preventivo", Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true }, 0, 0);
        titles.Controls.Add(WrappingLabel("Evaluación explicable basada únicamente en mediciones disponibles del equipo."), 0, 1);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 0) };
        _refresh.Text = "Actualizar";
        _refresh.Size = new Size(120, 40);
        _refresh.TabIndex = 0;
        _refresh.AccessibleName = "Actualizar diagnóstico preventivo";
        _refresh.AccessibleDescription = "Recopila nuevamente las mediciones disponibles del equipo";
        _refresh.Click += HandleRefreshClick;
        actions.Controls.Add(_refresh);
        actions.Controls.Add(_activity);
        actions.Controls.Add(_lastUpdate);
        _header.Controls.Add(titles, 0, 0);
        _header.Controls.Add(actions, 1, 0);
        return _header;
    }

    private Control BuildOverallCard()
    {
        _overallCard.Dock = DockStyle.Fill;
        _overallCard.MinimumSize = new Size(0, 140);
        _overallCard.Margin = new Padding(0, 0, 0, 12);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(20, 16, 20, 16), BackColor = AppTheme.SurfacePrimary };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(SectionTitle("Estado general"), 0, 0);
        _overallState.AccessibleName = "Estado general del diagnóstico";
        layout.Controls.Add(_overallState, 0, 1);
        layout.Controls.Add(_overallSummary, 0, 2);
        _overallCard.Controls.Add(layout);
        return _overallCard;
    }

    private Control BuildIndicatorsSection()
    {
        _indicatorsSection.Dock = DockStyle.Fill;
        _indicatorsSection.Margin = Padding.Empty;
        var layout = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Top, ColumnCount = 1, RowCount = 3, Padding = new Padding(20, 16, 20, 20), BackColor = AppTheme.SurfacePrimary };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.Controls.Add(SectionTitle("Indicadores y recomendaciones"), 0, 0);
        layout.Controls.Add(WrappingLabel("Los estados describen estas mediciones; no constituyen una prueba de avería física ni de amenaza."), 0, 1);
        _indicatorGrid.AutoSize = true;
        _indicatorGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _indicatorGrid.Dock = DockStyle.Top;
        _indicatorGrid.ColumnCount = 1;
        _indicatorGrid.RowCount = 1;
        _indicatorGrid.Margin = new Padding(0, 8, 0, 0);
        _indicatorGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _indicatorGrid.Controls.Add(StateLabel("Esperando mediciones...", AppTheme.TextSecondary), 0, 0);
        layout.Controls.Add(_indicatorGrid, 0, 2);
        _indicatorsSection.Controls.Add(layout);
        return _indicatorsSection;
    }

    private async void HandleShown(object? sender, EventArgs e) => await RefreshDiagnosticAsync();
    private async void HandleRefreshClick(object? sender, EventArgs e) => await RefreshDiagnosticAsync();
    private void HandleClientSizeChanged(object? sender, EventArgs e) => ApplyResponsiveLayout();
    private void HandleDisposed(object? sender, EventArgs e) => DisposeResources();

    private async Task RefreshDiagnosticAsync()
    {
        if (_refreshing || IsDisposed) return;
        _refreshing = true;
        SetBusy(true);
        try
        {
            var token = _lifetime.Token;
            var systemTask = CollectAsync(() => _systemService.GetSummaryAsync(token));
            var drivesTask = CollectAsync(() => _storageService.GetReadyDrivesAsync(token));
            var processesTask = CollectAsync(() => _processService.GetProcessesAsync(token));
            await Task.WhenAll(systemTask, drivesTask, processesTask);
            token.ThrowIfCancellationRequested();
            if (IsDisposed) return;

            var system = await systemTask;
            var drives = await drivesTask;
            var processes = await processesTask;
            var report = _diagnosticService.Evaluate(system.Value, drives.Value, processes.Value);
            ShowReport(report);
            var failedSources = new[] { system.Error, drives.Error, processes.Error }.Count(error => error);
            ShowUpdateStatus(report, failedSources);
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
        }
        catch (OperationCanceledException)
        {
            if (!IsDisposed) { _activity.Text = "Actualización cancelada  "; _activity.ForeColor = AppTheme.StatusWarning; }
        }
        finally
        {
            if (!IsDisposed) SetBusy(false);
            _refreshing = false;
        }
    }

    private void ShowUpdateStatus(DiagnosticReport report, int failedSources)
    {
        var partialUpdate = report.IsPartial || failedSources > 0;
        _activity.Text = failedSources > 0
            ? $"Actualización parcial · {failedSources} fuente(s) no disponible(s)  "
            : report.IsPartial
                ? "Actualización parcial · algunos datos no están disponibles  "
                : "Datos actualizados  ";
        _activity.ForeColor = partialUpdate ? AppTheme.StatusWarning : AppTheme.StatusSuccess;
    }

    private static async Task<CollectionResult<T>> CollectAsync<T>(Func<Task<T>> collector) where T : class
    {
        try { return new CollectionResult<T>(await collector(), false); }
        catch (OperationCanceledException) { throw; }
        catch (Exception exception) when (exception is not OutOfMemoryException and not AccessViolationException)
        {
            return new CollectionResult<T>(null, true);
        }
    }

    private void ShowReport(DiagnosticReport report)
    {
        _overallState.Text = report.OverallStatus == DiagnosticStatus.Correct && report.IsPartial
            ? "Evaluación parcial"
            : StatusName(report.OverallStatus);
        _overallState.ForeColor = StatusColor(report.OverallStatus);
        _overallSummary.Text = report.Summary;
        foreach (Control control in _indicatorGrid.Controls.Cast<Control>().ToArray()) control.Dispose();
        _indicatorGrid.Controls.Clear();
        _indicatorCards.Clear();
        foreach (var result in report.Results)
        {
            var card = CreateIndicatorCard(result);
            _indicatorCards.Add(card);
            _indicatorGrid.Controls.Add(card);
        }
        ApplyResponsiveLayout();
    }

    private static Control CreateIndicatorCard(DiagnosticResult result)
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, MinimumSize = new Size(0, 230), BackColor = AppTheme.SurfaceSecondary };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 6, Padding = new Padding(18, 14, 18, 14), BackColor = AppTheme.SurfaceSecondary };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        layout.Controls.Add(SectionTitle(result.Indicator), 0, 0);
        var state = ValueLabel(StatusName(result.Status));
        state.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        state.ForeColor = StatusColor(result.Status);
        layout.Controls.Add(state, 0, 1);
        var value = WrappingLabel(result.ObservedValue);
        value.ForeColor = result.Status == DiagnosticStatus.NoData ? AppTheme.TextDisabled : AppTheme.TextPrimary;
        layout.Controls.Add(value, 0, 2);
        layout.Controls.Add(WrappingLabel(result.Explanation), 0, 3);
        var recommendation = WrappingLabel($"Recomendación: {result.Recommendation}");
        recommendation.ForeColor = AppTheme.TextPrimary;
        layout.Controls.Add(recommendation, 0, 4);
        var source = WrappingLabel($"Origen: {result.Source}");
        source.Font = new Font("Segoe UI", 9F);
        layout.Controls.Add(source, 0, 5);
        card.AccessibleName = $"{result.Indicator}: {StatusName(result.Status)}";
        card.AccessibleDescription = $"{result.ObservedValue}. {result.Explanation}";
        card.Controls.Add(layout);
        return card;
    }

    private void ApplyResponsiveLayout()
    {
        if (_applyingLayout || _page is null || _header is null) return;
        _applyingLayout = true;
        try
        {
            _page.Padding = _scrollHost.ClientSize.Width < 700 ? new Padding(16) : new Padding(24, 16, 24, 24);
            var usableWidth = Math.Max(1, _scrollHost.ClientSize.Width - _page.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth);
            _lastUpdate.Visible = usableWidth >= 600;
            ResponsiveLayout.Reflow(_header, _header.Controls.Cast<Control>().ToArray(), usableWidth >= 820 ? 2 : 1, 80, 4);
            if (_indicatorCards.Count > 0) ResponsiveLayout.Reflow(_indicatorGrid, _indicatorCards, usableWidth >= 780 ? 2 : 1, 230, 6);
            _indicatorsSection.MinimumSize = Size.Empty;
            _indicatorsSection.Height = 16 + 34 + 38 + 8 + _indicatorGrid.Height + 20;
            _indicatorsSection.MinimumSize = new Size(0, _indicatorsSection.Height);
            _page.PerformLayout();
            _scrollHost.AutoScrollMinSize = new Size(0, _page.PreferredSize.Height);
        }
        finally { _applyingLayout = false; }
    }

    private void SetBusy(bool busy)
    {
        _refresh.Enabled = !busy;
        _refresh.Text = busy ? "Actualizando..." : "Actualizar";
        if (busy) { _activity.Text = "Recopilando mediciones...  "; _activity.ForeColor = AppTheme.TextSecondary; }
    }

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
    }

    private static string StatusName(DiagnosticStatus status) => status switch
    {
        DiagnosticStatus.Correct => "Correcto", DiagnosticStatus.Attention => "Atención",
        DiagnosticStatus.Critical => "Crítico", _ => "Sin datos"
    };
    private static Color StatusColor(DiagnosticStatus status) => status switch
    {
        DiagnosticStatus.Correct => AppTheme.StatusSuccess, DiagnosticStatus.Attention => AppTheme.StatusWarning,
        DiagnosticStatus.Critical => AppTheme.StatusError, _ => AppTheme.TextDisabled
    };
    private static Label SectionTitle(string text) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
    private static Label ValueLabel(string text) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty, AutoEllipsis = true };
    private static Label WrappingLabel(string text) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.TextSecondary, Margin = new Padding(0, 11, 12, 0) };
    private static Label StateLabel(string text, Color color) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = color, TextAlign = ContentAlignment.MiddleCenter };
    private sealed record CollectionResult<T>(T? Value, bool Error) where T : class;
}
