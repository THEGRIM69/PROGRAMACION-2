using System.Security;
using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;
using Timer = System.Windows.Forms.Timer;

namespace SystemAnalyzer.Forms;

public sealed class FormHistorial : Form
{
    private const int CollectionIntervalMilliseconds = 30_000;

    private readonly SystemInfoService _systemService = new();
    private readonly PerformanceHistoryService _historyService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Timer _timer = new() { Interval = CollectionIntervalMilliseconds };
    private readonly ThemedButton _start = new();
    private readonly ThemedButton _stop = new() { ButtonStyle = ThemedButtonStyle.Secondary };
    private readonly ThemedButton _refresh = new() { ButtonStyle = ThemedButtonStyle.Secondary };
    private readonly ThemedButton _clear = new() { ButtonStyle = ThemedButtonStyle.Secondary };
    private readonly Label _status = CreateLabel("Cargando historial...", AppTheme.TextSecondary);
    private readonly Label _sampleCount = CreateValue("0");
    private readonly Label _lastSample = CreateValue("Sin mediciones");
    private readonly Label _trend = CreateValue("Datos insuficientes");
    private readonly BufferedDataGridView _grid = new();
    private readonly List<PerformanceSample> _samples = new();
    private CancellationTokenSource? _collectionCancellation;
    private bool _collecting;
    private bool _operationInProgress;
    private bool _deleting;
    private Task? _activeCaptureTask;
    private bool _resourcesDisposed;

    public FormHistorial()
    {
        FormBorderStyle = FormBorderStyle.None;
        TopLevel = false;
        Dock = DockStyle.Fill;
        BackColor = AppTheme.BackgroundPrimary;
        Font = new Font("Segoe UI", 9F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BuildInterface();
        Shown += HandleShown;
        Disposed += HandleDisposed;
        _timer.Tick += HandleTimerTick;
    }

    private void BuildInterface()
    {
        var page = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
            Padding = new Padding(24, 16, 24, 24), BackColor = AppTheme.BackgroundPrimary
        };
        page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        page.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
        page.RowStyles.Add(new RowStyle(SizeType.Absolute, 112));
        page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        page.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        page.Controls.Add(BuildHeader(), 0, 0);
        page.Controls.Add(BuildSummary(), 0, 1);
        page.Controls.Add(BuildGridCard(), 0, 2);
        _status.Dock = DockStyle.Fill;
        _status.TextAlign = ContentAlignment.MiddleLeft;
        page.Controls.Add(_status, 0, 3);
        Controls.Add(page);
    }

    private Control BuildHeader()
    {
        var header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Margin = new Padding(0, 0, 0, 12) };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));
        var titles = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Margin = Padding.Empty };
        titles.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        titles.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        titles.Controls.Add(new Label
        {
            Text = "Historial de rendimiento", Dock = DockStyle.Fill, AutoEllipsis = true,
            ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        titles.Controls.Add(CreateLabel("Muestras locales de CPU y memoria RAM; la CPU representa una lectura momentánea de 0.5 s.", AppTheme.TextSecondary), 0, 1);

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft,
            WrapContents = true, Padding = new Padding(0, 4, 0, 0)
        };
        ConfigureButton(_start, "Iniciar", "Iniciar recopilación del historial", 0, HandleStartClick);
        ConfigureButton(_stop, "Detener", "Detener recopilación del historial", 1, HandleStopClick);
        ConfigureButton(_refresh, "Actualizar", "Registrar una medición ahora", 2, HandleRefreshClick);
        ConfigureButton(_clear, "Eliminar", "Eliminar el historial local de SystemAnalyzer", 3, HandleClearClick);
        _stop.Enabled = false;
        actions.Controls.AddRange(new Control[] { _clear, _refresh, _stop, _start });
        header.Controls.Add(titles, 0, 0);
        header.Controls.Add(actions, 1, 0);
        return header;
    }

    private Control BuildSummary()
    {
        var summary = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, Margin = new Padding(0, 0, 0, 12) };
        summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24));
        summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        summary.Controls.Add(CreateSummaryCard("Muestras conservadas", _sampleCount), 0, 0);
        summary.Controls.Add(CreateSummaryCard("Última medición válida", _lastSample), 1, 0);
        summary.Controls.Add(CreateSummaryCard("Tendencia reciente", _trend), 2, 0);
        return summary;
    }

    private Control BuildGridCard()
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 12) };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
            Padding = new Padding(16), BackColor = AppTheme.SurfacePrimary
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(new Label
        {
            Text = "Mediciones cronológicas", Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary,
            Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        ConfigureGrid();
        layout.Controls.Add(_grid, 0, 1);
        card.Controls.Add(layout);
        return card;
    }

    private void ConfigureGrid()
    {
        _grid.Dock = DockStyle.Fill;
        _grid.BackgroundColor = AppTheme.SurfacePrimary;
        _grid.BorderStyle = BorderStyle.None;
        _grid.ReadOnly = true;
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        _grid.RowHeadersVisible = false;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.MultiSelect = false;
        _grid.AutoGenerateColumns = false;
        _grid.EnableHeadersVisualStyles = false;
        _grid.ColumnHeadersHeight = 38;
        _grid.RowTemplate.Height = 34;
        _grid.GridColor = AppTheme.BorderSubtle;
        _grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = AppTheme.SurfaceSecondary, ForeColor = AppTheme.TextPrimary, SelectionBackColor = AppTheme.SurfaceSecondary, Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold) };
        _grid.DefaultCellStyle = new DataGridViewCellStyle { BackColor = AppTheme.SurfacePrimary, ForeColor = AppTheme.TextPrimary, SelectionBackColor = AppTheme.SelectionBackground, SelectionForeColor = AppTheme.TextPrimary, Padding = new Padding(8, 0, 8, 0) };
        _grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = AppTheme.BackgroundSecondary };
        _grid.AccessibleName = "Historial de rendimiento";
        _grid.AccessibleDescription = "Tabla de fecha y hora, uso de CPU y uso de memoria RAM almacenados localmente";
        _grid.TabIndex = 4;
        _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Timestamp", HeaderText = "Fecha y hora", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 55 });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cpu", HeaderText = "CPU", MinimumWidth = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 22 });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ram", HeaderText = "RAM", MinimumWidth = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 23 });
    }

    private async void HandleShown(object? sender, EventArgs e) => await LoadHistoryAsync();
    private async void HandleStartClick(object? sender, EventArgs e)
    {
        if (_collecting) return;
        _collectionCancellation?.Dispose();
        _collectionCancellation = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
        _collecting = true;
        UpdateButtons();
        await RunCaptureAsync(_collectionCancellation.Token);
        if (!IsDisposed && _collecting) _timer.Start();
    }
    private void HandleStopClick(object? sender, EventArgs e) => StopCollecting("Recopilación detenida.");
    private async void HandleRefreshClick(object? sender, EventArgs e) => await RunCaptureAsync(_lifetime.Token);
    private async void HandleClearClick(object? sender, EventArgs e)
    {
        if (_operationInProgress || IsDisposed) return;
        var confirmation = MessageBox.Show(this,
            "Se eliminará únicamente el historial local de rendimiento de SystemAnalyzer.",
            "Eliminar historial", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
        if (confirmation != DialogResult.OK) return;

        _deleting = true;
        StopCollecting("Deteniendo la recopilación para eliminar el historial...");
        UpdateButtons();
        var activeCapture = _activeCaptureTask;
        if (activeCapture is not null)
            await activeCapture;
        if (IsDisposed) return;

        _operationInProgress = true;
        UpdateButtons();
        try
        {
            await _historyService.ClearAsync(_lifetime.Token);
            if (IsDisposed) return;
            _samples.Clear();
            RenderHistory();
            SetStatus("El historial local fue eliminado.", AppTheme.StatusSuccess);
        }
        catch (OperationCanceledException) { }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or SecurityException)
        {
            if (!IsDisposed) SetStatus("No fue posible eliminar el historial local.", AppTheme.StatusError);
        }
        finally
        {
            _operationInProgress = false;
            _deleting = false;
            if (!IsDisposed) UpdateButtons();
        }
    }
    private async void HandleTimerTick(object? sender, EventArgs e)
    {
        if (_activeCaptureTask is { IsCompleted: false }) return;
        if (_collectionCancellation is not null && _collecting)
            await RunCaptureAsync(_collectionCancellation.Token);
    }
    private void HandleDisposed(object? sender, EventArgs e) => DisposeResources();

    private async Task LoadHistoryAsync()
    {
        if (_operationInProgress || IsDisposed) return;
        _operationInProgress = true;
        UpdateButtons();
        try
        {
            var result = await _historyService.LoadAsync(_lifetime.Token);
            if (IsDisposed) return;
            ReplaceSamples(result.Samples);
            SetStatus(result.Warning ?? EmptyOrReadyMessage(), result.Warning is null ? AppTheme.TextSecondary : AppTheme.StatusWarning);
        }
        catch (OperationCanceledException) { }
        finally
        {
            _operationInProgress = false;
            if (!IsDisposed) UpdateButtons();
        }
    }

    private async Task CaptureSampleAsync(CancellationToken token)
    {
        if (_operationInProgress || IsDisposed) return;
        _operationInProgress = true;
        UpdateButtons();
        SetStatus("Recopilando una muestra momentánea...", AppTheme.TextSecondary);
        try
        {
            var information = await _systemService.GetSummaryAsync(token);
            token.ThrowIfCancellationRequested();
            var sample = CreateSample(information, DateTimeOffset.UtcNow);
            if (!PerformanceHistoryService.IsValid(sample))
            {
                if (!IsDisposed) SetStatus("La medición no produjo valores válidos; se conserva el historial anterior.", AppTheme.StatusWarning);
                return;
            }

            var result = await _historyService.AppendAsync(sample, token);
            if (IsDisposed || token.IsCancellationRequested) return;
            ReplaceSamples(result.Samples);
            SetStatus(result.Warning is null ? "Medición guardada localmente." : $"Medición guardada. {result.Warning}",
                result.Warning is null ? AppTheme.StatusSuccess : AppTheme.StatusWarning);
        }
        catch (OperationCanceledException) { }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or SecurityException)
        {
            if (!IsDisposed) SetStatus("No fue posible guardar la medición; se conserva el historial anterior.", AppTheme.StatusError);
        }
        finally
        {
            _operationInProgress = false;
            if (!IsDisposed) UpdateButtons();
        }
    }

    private async Task RunCaptureAsync(CancellationToken token)
    {
        if (_activeCaptureTask is { IsCompleted: false }) return;
        var capture = CaptureSampleAsync(token);
        _activeCaptureTask = capture;
        try
        {
            await capture;
        }
        finally
        {
            if (ReferenceEquals(_activeCaptureTask, capture)) _activeCaptureTask = null;
        }
    }

    internal static PerformanceSample CreateSample(SystemInfo information, DateTimeOffset timestampUtc)
    {
        var cpu = PerformanceHistoryService.IsValidPercentage(information.CpuUsagePercent)
            ? information.CpuUsagePercent : null;
        double? ram = null;
        if (information.TotalMemoryBytes > 0 && information.AvailableMemoryBytes <= information.TotalMemoryBytes)
        {
            var calculated = (information.TotalMemoryBytes - information.AvailableMemoryBytes) * 100d / information.TotalMemoryBytes;
            if (PerformanceHistoryService.IsValidPercentage(calculated)) ram = calculated;
        }
        return new PerformanceSample { TimestampUtc = timestampUtc.ToUniversalTime(), CpuPercent = cpu, RamPercent = ram };
    }

    private void ReplaceSamples(IEnumerable<PerformanceSample> samples)
    {
        _samples.Clear();
        _samples.AddRange(samples.OrderBy(sample => sample.TimestampUtc));
        RenderHistory();
    }

    private void RenderHistory()
    {
        _grid.Rows.Clear();
        foreach (var sample in _samples)
            _grid.Rows.Add(sample.TimestampUtc.ToLocalTime().ToString("g"), FormatPercentage(sample.CpuPercent), FormatPercentage(sample.RamPercent));
        if (_grid.Rows.Count > 0) _grid.FirstDisplayedScrollingRowIndex = _grid.Rows.Count - 1;
        _sampleCount.Text = _samples.Count.ToString("N0");
        _lastSample.Text = _samples.Count == 0 ? "Sin mediciones" : _samples[^1].TimestampUtc.ToLocalTime().ToString("g");
        _trend.Text = BuildTrend(_samples);
    }

    internal static string BuildTrend(IReadOnlyList<PerformanceSample> samples)
    {
        var cpu = TrendFor(samples.Select(sample => sample.CpuPercent));
        var ram = TrendFor(samples.Select(sample => sample.RamPercent));
        return cpu is null && ram is null ? "Datos insuficientes"
            : $"CPU: {cpu ?? "sin datos"} · RAM: {ram ?? "sin datos"}";
    }

    private static string? TrendFor(IEnumerable<double?> values)
    {
        var valid = values.Where(value => value.HasValue).Select(value => value!.Value).TakeLast(3).ToArray();
        if (valid.Length < 3) return null;
        var difference = valid[^1] - valid[0];
        return Math.Abs(difference) < 3 ? "estable" : difference > 0 ? "en aumento" : "en descenso";
    }

    private void StopCollecting(string message)
    {
        _collecting = false;
        _timer.Stop();
        _collectionCancellation?.Cancel();
        if (!IsDisposed) { UpdateButtons(); SetStatus(message, AppTheme.TextSecondary); }
    }

    private void UpdateButtons()
    {
        _start.Enabled = !_deleting && !_collecting && !_operationInProgress;
        _stop.Enabled = !_deleting && _collecting;
        _refresh.Enabled = !_deleting && !_operationInProgress;
        _clear.Enabled = !_deleting && !_operationInProgress && _samples.Count > 0;
    }

    private void DisposeResources()
    {
        if (_resourcesDisposed) return;
        _resourcesDisposed = true;
        _timer.Stop();
        _timer.Tick -= HandleTimerTick;
        _timer.Dispose();
        _start.Click -= HandleStartClick;
        _stop.Click -= HandleStopClick;
        _refresh.Click -= HandleRefreshClick;
        _clear.Click -= HandleClearClick;
        Shown -= HandleShown;
        Disposed -= HandleDisposed;
        _collectionCancellation?.Cancel();
        _collectionCancellation?.Dispose();
        _lifetime.Cancel();
        _lifetime.Dispose();
    }

    private string EmptyOrReadyMessage() => _samples.Count == 0
        ? "Todavía no hay mediciones. Inicia la recopilación o registra una medición ahora."
        : "Historial local cargado.";
    private void SetStatus(string text, Color color) { _status.Text = text; _status.ForeColor = color; }
    private static string FormatPercentage(double? value) => value.HasValue ? $"{value.Value:0.#}%" : "No disponible";
    private static void ConfigureButton(ThemedButton button, string text, string accessibleName, int tabIndex, EventHandler handler)
    {
        button.Text = text; button.Size = new Size(104, 40); button.TabIndex = tabIndex;
        button.AccessibleName = accessibleName; button.Click += handler;
    }
    private static Control CreateSummaryCard(string title, Label value)
    {
        var card = new RoundedPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 8, 0) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(18, 12, 18, 12), BackColor = AppTheme.SurfacePrimary };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(CreateLabel(title, AppTheme.TextSecondary), 0, 0);
        value.Dock = DockStyle.Fill; value.AutoEllipsis = true;
        layout.Controls.Add(value, 0, 1); card.Controls.Add(layout); return card;
    }
    private static Label CreateLabel(string text, Color color) => new() { Text = text, Dock = DockStyle.Fill, ForeColor = color, TextAlign = ContentAlignment.MiddleLeft };
    private static Label CreateValue(string text) => new() { Text = text, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
}
