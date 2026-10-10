using SystemAnalyzer.Models;
using SystemAnalyzer.Services;
using SystemAnalyzer.UI;
using Timer = System.Windows.Forms.Timer;

namespace SystemAnalyzer.Forms;

public sealed class FormProcesos : Form
{
    private readonly ProcessAnalyzerService _processService = new();
    private readonly SystemInfoService _systemService = new();
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Timer _timer = new() { Interval = 5_000 };
    private readonly BufferedDataGridView _grid = new();
    private readonly TextBox _search = new();
    private readonly ComboBox _filter = new();
    private readonly ThemedButton _refresh = new();
    private readonly ThemedButton _details = new() { ButtonStyle = ThemedButtonStyle.Secondary };
    private readonly Label _lastUpdate = Muted("Última actualización: --:--:--");
    private readonly Label _activity = Muted(string.Empty);
    private readonly Label _countLabel = Muted("0 procesos mostrados");
    private readonly SummaryCard _activeCard = new("Procesos activos", "0");
    private readonly SummaryCard _memoryCard = new("Memoria utilizada", "Calculando...");
    private readonly SummaryCard _cpuCard = new("CPU general", "Calculando...");
    private readonly SummaryCard _topCard = new("Mayor consumo", "Calculando...");
    private readonly List<Control> _summaryCards = new();
    private readonly List<SummaryCard> _summaryCardViews = new();
    private TableLayoutPanel _page = null!;
    private TableLayoutPanel _header = null!;
    private TableLayoutPanel _summary = null!;
    private TableLayoutPanel _toolbar = null!;
    private List<ProcessInfo> _allProcesses = new();
    private string _sortColumn = "Memory";
    private bool _sortDescending = true;
    private bool _refreshing;
    private bool _disposedResources;

    public FormProcesos()
    {
        FormBorderStyle = FormBorderStyle.None;
        TopLevel = false;
        Dock = DockStyle.Fill;
        BackColor = AppTheme.BackgroundPrimary;
        Font = new Font("Segoe UI", 9F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BuildInterface();
        ClientSizeChanged += (_, _) => ApplyResponsiveLayout();
        Shown += async (_, _) => { await RefreshProcessesAsync(); if (!IsDisposed) _timer.Start(); };
        _timer.Tick += async (_, _) => await RefreshProcessesAsync();
        Disposed += (_, _) => DisposeResources();
    }

    private void BuildInterface()
    {
        _page = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5,
            Padding = new Padding(24, 16, 24, 20), BackColor = AppTheme.BackgroundPrimary
        };
        _page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _page.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        _page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _page.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
        _page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        _page.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        _page.Controls.Add(BuildHeader(), 0, 0);
        _page.Controls.Add(BuildSummary(), 0, 1);
        _page.Controls.Add(BuildToolbar(), 0, 2);
        _page.Controls.Add(BuildGrid(), 0, 3);
        _page.Controls.Add(_countLabel, 0, 4);
        _header.TabIndex = 0;
        _summary.TabIndex = 1;
        _summary.TabStop = false;
        _toolbar.TabIndex = 2;
        _grid.TabIndex = 3;
        _grid.TabStop = true;
        _countLabel.TabIndex = 4;
        _grid.MinimumSize = new Size(0, 140);
        Controls.Add(_page);
        ApplyResponsiveLayout();
    }

    private Control BuildHeader()
    {
        _header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Margin = new Padding(0, 0, 0, 12) };
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
        _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
        var titles = new Panel { Dock = DockStyle.Fill };
        titles.Controls.Add(new Label { Text = "Procesos", AutoSize = true, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold), Location = new Point(0, 0) });
        titles.Controls.Add(new Label { Text = "Supervisa los programas y procesos que utilizan los recursos del equipo.", AutoSize = true, ForeColor = AppTheme.TextSecondary, Location = new Point(3, 40) });

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 0) };
        ConfigureButton(_refresh, "Actualizar", 120, "Actualizar lista de procesos");
        _refresh.TabIndex = 0;
        _refresh.Click += async (_, _) => await RefreshProcessesAsync();
        actions.Controls.Add(_refresh);
        actions.Controls.Add(_activity);
        actions.Controls.Add(_lastUpdate);
        _header.Controls.Add(titles, 0, 0);
        _header.Controls.Add(actions, 1, 0);
        return _header;
    }

    private Control BuildSummary()
    {
        _summary = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, Height = 108, Margin = new Padding(0, 0, 0, 12) };
        _summaryCards.AddRange(new Control[] { _activeCard, _memoryCard, _cpuCard, _topCard });
        _summaryCardViews.AddRange(new[] { _activeCard, _memoryCard, _cpuCard, _topCard });
        for (var index = 0; index < _summaryCards.Count; index++) _summary.Controls.Add(_summaryCards[index], index, 0);
        return _summary;
    }

    private Control BuildToolbar()
    {
        _toolbar = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, Margin = new Padding(0, 0, 0, 12) };
        _search.Dock = DockStyle.Fill;
        _search.Margin = new Padding(0, 6, 12, 6);
        _search.PlaceholderText = "Buscar proceso por nombre o PID";
        _search.BackColor = AppTheme.SurfaceSecondary;
        _search.ForeColor = AppTheme.TextPrimary;
        _search.BorderStyle = BorderStyle.FixedSingle;
        _search.Font = new Font("Segoe UI", 10F);
        _search.AccessibleName = "Buscar proceso por nombre o PID";
        _search.TabIndex = 0;
        _search.TextChanged += (_, _) => ApplyView();

        _filter.Dock = DockStyle.Fill;
        _filter.Margin = new Padding(0, 6, 12, 6);
        _filter.DropDownStyle = ComboBoxStyle.DropDownList;
        _filter.BackColor = AppTheme.SurfaceSecondary;
        _filter.ForeColor = AppTheme.TextPrimary;
        _filter.Font = new Font("Segoe UI", 10F);
        _filter.AccessibleName = "Filtrar procesos por nivel de consumo";
        _filter.TabIndex = 1;
        _filter.Items.AddRange(new object[] { "Todos", "Consumo alto", "Consumo moderado", "Consumo normal/bajo" });
        _filter.SelectedIndex = 0;
        _filter.SelectedIndexChanged += (_, _) => ApplyView();

        ConfigureButton(_details, "Ver detalles", 124, "Ver detalles del proceso seleccionado");
        _details.TabIndex = 2;
        _details.Margin = new Padding(0, 4, 0, 4);
        _details.Click += async (_, _) => await ShowSelectedDetailsAsync();
        _toolbar.Controls.Add(_search, 0, 0);
        _toolbar.Controls.Add(_filter, 1, 0);
        _toolbar.Controls.Add(_details, 2, 0);
        return _toolbar;
    }

    private Control BuildGrid()
    {
        _grid.Dock = DockStyle.Fill;
        _grid.Margin = Padding.Empty;
        _grid.BackgroundColor = AppTheme.SurfacePrimary;
        _grid.BorderStyle = BorderStyle.None;
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        _grid.ReadOnly = true;
        _grid.MultiSelect = false;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.AccessibleName = "Lista de procesos";
        _grid.AccessibleDescription = "Procesos en ejecución. Usa las flechas para recorrer filas, los encabezados para ordenar y el botón Ver detalles para consultar el proceso seleccionado.";
        _grid.RowHeadersVisible = false;
        _grid.AutoGenerateColumns = false;
        _grid.EnableHeadersVisualStyles = false;
        _grid.ColumnHeadersHeight = 40;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.RowTemplate.Height = 36;
        _grid.GridColor = AppTheme.BorderSubtle;
        _grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        _grid.ScrollBars = ScrollBars.Both;
        _grid.DefaultCellStyle = new DataGridViewCellStyle { BackColor = AppTheme.SurfacePrimary, ForeColor = AppTheme.TextPrimary, SelectionBackColor = AppTheme.SelectionBackground, SelectionForeColor = AppTheme.TextPrimary, Padding = new Padding(8, 0, 8, 0) };
        _grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = AppTheme.BackgroundSecondary, ForeColor = AppTheme.TextPrimary, SelectionBackColor = AppTheme.SelectionBackground, SelectionForeColor = AppTheme.TextPrimary };
        _grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = AppTheme.SurfaceSecondary, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold), SelectionBackColor = AppTheme.SurfaceSecondary, SelectionForeColor = AppTheme.TextPrimary, Padding = new Padding(8, 0, 8, 0) };

        AddColumn("Name", "Nombre", 210, DataGridViewAutoSizeColumnMode.Fill);
        AddColumn("Pid", "PID", 75);
        AddColumn("Memory", "Memoria RAM", 115);
        AddColumn("Cpu", "CPU", 90);
        AddColumn("Status", "Estado", 115);
        AddColumn("Consumption", "Consumo", 105);
        _grid.Columns["Pid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        _grid.Columns["Memory"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        _grid.Columns["Cpu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        _grid.Columns["Pid"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        _grid.Columns["Memory"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        _grid.Columns["Cpu"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        _grid.ColumnHeaderMouseClick += (_, eventArgs) => ChangeSort(_grid.Columns[eventArgs.ColumnIndex].Name);
        _grid.CellDoubleClick += async (_, eventArgs) => { if (eventArgs.RowIndex >= 0) await ShowSelectedDetailsAsync(); };
        return _grid;
    }

    private void AddColumn(string name, string header, int width, DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.None)
    {
        _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = name, HeaderText = header, Width = width, MinimumWidth = 65, AutoSizeMode = mode, SortMode = DataGridViewColumnSortMode.Programmatic });
    }

    private async Task RefreshProcessesAsync()
    {
        if (_refreshing || IsDisposed) return;
        _refreshing = true;
        SetBusy(true);
        var selectedPid = SelectedProcess()?.Id;
        try
        {
            var token = _lifetime.Token;
            var processesTask = _processService.GetProcessesAsync(token);
            var systemTask = _systemService.GetSummaryAsync(token);
            await Task.WhenAll(processesTask, systemTask);
            if (IsDisposed || token.IsCancellationRequested) return;

            var system = systemTask.Result;
            _allProcesses = processesTask.Result.ToList();
            foreach (var process in _allProcesses)
                process.Consumption = ProcessConsumptionClassifier.Classify(process.WorkingSetBytes, system.TotalMemoryBytes);

            UpdateSummary(system);
            ApplyView(selectedPid);
            _lastUpdate.Text = $"Última actualización: {DateTime.Now:HH:mm:ss}";
        }
        catch (OperationCanceledException) { }
        catch (Exception)
        {
            if (!IsDisposed)
            {
                _activity.Text = "No se pudo actualizar · intenta nuevamente  ";
                _activity.ForeColor = AppTheme.StatusError;
            }
        }
        finally { if (!IsDisposed) SetBusy(false); _refreshing = false; }
    }

    private void UpdateSummary(SystemInfo system)
    {
        _activeCard.SetValue(_allProcesses.Count.ToString("N0"), "Procesos detectados");
        if (system.TotalMemoryBytes > 0)
            _memoryCard.SetValue(FormatBytes((long)(system.TotalMemoryBytes - system.AvailableMemoryBytes)), $"{system.MemoryUsagePercent:0}% de la RAM total");
        else _memoryCard.SetValue("No disponible", "Memoria física del sistema");
        _cpuCard.SetValue(system.CpuUsagePercent.HasValue ? $"{system.CpuUsagePercent:0}%" : "No disponible", "Uso total del procesador");
        var top = _allProcesses.MaxBy(process => process.WorkingSetBytes);
        _topCard.SetValue(top?.Name ?? "No disponible", top is null ? "Sin datos" : FormatBytes(top.WorkingSetBytes));
    }

    private void ApplyView(int? selectedPid = null)
    {
        if (_grid.IsDisposed) return;
        selectedPid ??= SelectedProcess()?.Id;
        IEnumerable<ProcessInfo> query = _allProcesses;
        var term = _search.Text.Trim();
        if (term.Length > 0)
            query = query.Where(process => process.Name.Contains(term, StringComparison.OrdinalIgnoreCase) || process.Id.ToString().Contains(term, StringComparison.OrdinalIgnoreCase));

        query = _filter.SelectedIndex switch
        {
            1 => query.Where(process => process.Consumption == ConsumptionLevel.High),
            2 => query.Where(process => process.Consumption == ConsumptionLevel.Moderate),
            3 => query.Where(process => process.Consumption is ConsumptionLevel.Normal or ConsumptionLevel.Low),
            _ => query
        };

        var items = query.ToList();
        items.Sort(CompareProcesses);
        _grid.SuspendLayout();
        _grid.Rows.Clear();
        foreach (var process in items)
        {
            var rowIndex = _grid.Rows.Add(process.Name, process.Id, process.WorkingSetBytes, process.CpuUsagePercent, process.Status, ConsumptionText(process.Consumption));
            var row = _grid.Rows[rowIndex];
            row.Tag = process;
            row.Cells["Name"].ToolTipText = process.Name;
            row.Cells["Memory"].Value = process.WorkingSetBytes;
            row.Cells["Memory"].Style.Format = "N0";
            row.Cells["Memory"].ToolTipText = FormatBytes(process.WorkingSetBytes);
            row.Cells["Memory"].Value = process.WorkingSetBytes;
            row.Cells["Cpu"].Value = process.CpuUsagePercent;
            row.Cells["Cpu"].ToolTipText = process.CpuUsagePercent.HasValue ? $"{process.CpuUsagePercent:0.0}%" : process.CpuStatus;
            if (process.Consumption == ConsumptionLevel.High) row.Cells["Consumption"].Style.ForeColor = AppTheme.StatusWarning;
            else if (process.Consumption == ConsumptionLevel.Moderate) row.Cells["Consumption"].Style.ForeColor = AppTheme.StatusInfo;
        }
        _grid.ResumeLayout();
        FormatVisibleCells();
        RestoreSelection(selectedPid);
        UpdateSortGlyph();
        _countLabel.Text = items.Count == 0
            ? (_allProcesses.Count == 0 ? "Sin procesos disponibles" : "No hay procesos que coincidan con la búsqueda o el filtro")
            : $"{items.Count:N0} de {_allProcesses.Count:N0} procesos mostrados";
    }

    private void FormatVisibleCells()
    {
        foreach (DataGridViewRow row in _grid.Rows)
        {
            if (row.Tag is not ProcessInfo process) continue;
            row.Cells["Memory"].Value = $"{process.WorkingSetBytes / (1024d * 1024):N1} MB";
            row.Cells["Cpu"].Value = process.CpuUsagePercent.HasValue ? $"{process.CpuUsagePercent:0.0}%" : process.CpuStatus;
        }
    }

    private int CompareProcesses(ProcessInfo left, ProcessInfo right)
    {
        if (_sortColumn == "Cpu" && left.CpuUsagePercent.HasValue != right.CpuUsagePercent.HasValue)
            return left.CpuUsagePercent.HasValue ? -1 : 1;
        var result = _sortColumn switch
        {
            "Name" => StringComparer.OrdinalIgnoreCase.Compare(left.Name, right.Name),
            "Pid" => left.Id.CompareTo(right.Id),
            "Cpu" => Nullable.Compare(left.CpuUsagePercent, right.CpuUsagePercent),
            "Status" => StringComparer.OrdinalIgnoreCase.Compare(left.Status, right.Status),
            "Consumption" => left.Consumption.CompareTo(right.Consumption),
            _ => left.WorkingSetBytes.CompareTo(right.WorkingSetBytes)
        };
        return _sortDescending ? -result : result;
    }

    private void ChangeSort(string column)
    {
        if (_sortColumn == column) _sortDescending = !_sortDescending;
        else { _sortColumn = column; _sortDescending = column is "Memory" or "Cpu" or "Pid" or "Consumption"; }
        ApplyView();
    }

    private void UpdateSortGlyph()
    {
        foreach (DataGridViewColumn column in _grid.Columns) column.HeaderCell.SortGlyphDirection = SortOrder.None;
        if (_grid.Columns.Contains(_sortColumn)) _grid.Columns[_sortColumn].HeaderCell.SortGlyphDirection = _sortDescending ? SortOrder.Descending : SortOrder.Ascending;
    }

    private async Task ShowSelectedDetailsAsync()
    {
        var selected = SelectedProcess();
        if (selected is null) { MessageBox.Show(this, "Selecciona un proceso para consultar sus detalles.", "SystemAnalyzer", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
        _details.Enabled = false;
        try
        {
            var details = await _processService.GetDetailsAsync(selected.Id, selected.CpuUsagePercent, _lifetime.Token);
            if (IsDisposed) return;
            if (details is null) { MessageBox.Show(this, "El proceso terminó o ya no está disponible.", "SystemAnalyzer", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            using var dialog = new FormProcessDetails(details);
            dialog.ShowDialog(this);
        }
        catch (OperationCanceledException) { }
        finally { if (!IsDisposed) _details.Enabled = true; }
    }

    private ProcessInfo? SelectedProcess() => _grid.CurrentRow?.Tag as ProcessInfo;

    private void RestoreSelection(int? processId)
    {
        if (!processId.HasValue) return;
        foreach (DataGridViewRow row in _grid.Rows)
            if (row.Tag is ProcessInfo process && process.Id == processId.Value) { row.Selected = true; _grid.CurrentCell = row.Cells[0]; return; }
    }

    private void ApplyResponsiveLayout()
    {
        if (_summary is null || _header is null || _toolbar is null) return;
        var compactHeight = ClientSize.Height < 620;
        _page.Padding = compactHeight
            ? new Padding(16, 8, 16, 8)
            : ClientSize.Width < 700 ? new Padding(16, 16, 16, 20) : new Padding(24, 16, 24, 20);
        var usableWidth = Math.Max(1, ClientSize.Width - _page.Padding.Horizontal);
        var summaryHeight = compactHeight ? 80 : 108;
        foreach (var card in _summaryCardViews) card.Compact = compactHeight;
        ResponsiveLayout.Reflow(_summary, _summaryCards, usableWidth >= 1020 ? 4 : usableWidth >= 560 ? 2 : 1, summaryHeight, 6);
        var headerControls = _header.Controls.Cast<Control>().ToArray();
        var headerColumns = usableWidth >= (compactHeight ? 620 : 850) ? 2 : 1;
        var titlePanel = headerControls.FirstOrDefault();
        var subtitle = titlePanel?.Controls.OfType<Label>().LastOrDefault();
        if (subtitle is not null) subtitle.Visible = !compactHeight && usableWidth >= 600;
        _lastUpdate.Visible = !compactHeight && usableWidth >= 600;
        ResponsiveLayout.Reflow(_header, headerControls, headerColumns, compactHeight ? 52 : 68, 2);
        _page.RowStyles[0].Height = _header.Height + _header.Margin.Vertical;
        _page.RowStyles[4].Height = compactHeight ? 24 : 28;

        ApplyToolbarLayout(usableWidth, compactHeight);
    }

    private void SetBusy(bool busy)
    {
        _refresh.Enabled = !busy;
        _refresh.Text = busy ? "Analizando..." : "Actualizar";
        if (busy)
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
        if (_disposedResources) return;
        _disposedResources = true;
        _timer.Stop();
        _timer.Dispose();
        _lifetime.Cancel();
        _lifetime.Dispose();
    }

    private void ApplyToolbarLayout(int usableWidth, bool compactHeight)
    {
        _toolbar.SuspendLayout();
        _toolbar.ColumnStyles.Clear();
        _toolbar.RowStyles.Clear();
        if (usableWidth >= 560)
        {
            _toolbar.ColumnCount = 3;
            _toolbar.RowCount = 1;
            _toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            _toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            _toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136));
            var toolbarHeight = compactHeight ? 48 : 56;
            _toolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, toolbarHeight));
            _toolbar.SetCellPosition(_search, new TableLayoutPanelCellPosition(0, 0));
            _toolbar.SetCellPosition(_filter, new TableLayoutPanelCellPosition(1, 0));
            _toolbar.SetCellPosition(_details, new TableLayoutPanelCellPosition(2, 0));
            _toolbar.SetColumnSpan(_search, 1);
            _toolbar.Height = toolbarHeight;
        }
        else
        {
            _toolbar.ColumnCount = 2;
            _toolbar.RowCount = 2;
            _toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            _toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136));
            _toolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            _toolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            _toolbar.SetCellPosition(_search, new TableLayoutPanelCellPosition(0, 0));
            _toolbar.SetColumnSpan(_search, 2);
            _toolbar.SetCellPosition(_filter, new TableLayoutPanelCellPosition(0, 1));
            _toolbar.SetCellPosition(_details, new TableLayoutPanelCellPosition(1, 1));
            _toolbar.Height = 96;
        }
        _page.RowStyles[2].Height = _toolbar.Height + _toolbar.Margin.Vertical;
        _toolbar.ResumeLayout(true);
    }

    private static void ConfigureButton(ThemedButton button, string text, int width, string accessibleName)
    {
        button.Text = text;
        button.Width = width;
        button.Height = 40;
        button.AccessibleName = accessibleName;
    }

    private static string ConsumptionText(ConsumptionLevel level) => level switch
    {
        ConsumptionLevel.High => "Alto",
        ConsumptionLevel.Moderate => "Moderado",
        ConsumptionLevel.Normal => "Normal",
        _ => "Bajo"
    };

    private static string FormatBytes(long bytes) => bytes >= 1024d * 1024 * 1024
        ? $"{bytes / (1024d * 1024 * 1024):0.#} GB"
        : $"{bytes / (1024d * 1024):0.#} MB";

    private static Label Muted(string text) => new() { Text = text, AutoSize = true, ForeColor = AppTheme.TextSecondary, Margin = new Padding(0, 11, 12, 0) };

    private sealed class SummaryCard : RoundedPanel
    {
        private readonly TableLayoutPanel _layout;
        private readonly Label _value;
        private readonly Label _caption;
        private bool _compact;

        public bool Compact
        {
            get => _compact;
            set
            {
                if (_compact == value) return;
                _compact = value;
                _caption.Visible = !value;
                _layout.Padding = value ? new Padding(16, 8, 16, 8) : new Padding(20, 14, 20, 14);
                _layout.RowStyles[0].Height = value ? 22 : 25;
                _layout.RowStyles[1].Height = value ? 38 : 38;
            }
        }

        public SummaryCard(string title, string value)
        {
            _layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 14, 20, 14), ColumnCount = 1, RowCount = 3, BackColor = AppTheme.SurfacePrimary };
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            _layout.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, Font = new Font("Segoe UI Semibold", 10F), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty }, 0, 0);
            _value = new Label { Text = value, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextPrimary, Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
            _caption = new Label { Text = string.Empty, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.TextSecondary, TextAlign = ContentAlignment.TopLeft, Margin = Padding.Empty };
            _layout.Controls.Add(_value, 0, 1);
            _layout.Controls.Add(_caption, 0, 2);
            Controls.Add(_layout);
        }

        public void SetValue(string value, string caption) { _value.Text = value; _caption.Text = caption; }
    }
}
