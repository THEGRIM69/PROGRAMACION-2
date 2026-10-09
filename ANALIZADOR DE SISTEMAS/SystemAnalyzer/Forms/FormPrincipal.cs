using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public sealed class FormPrincipal : Form
{
    private const int ExpandedWidth = 240;
    private const int CompactWidth = 72;
    private readonly Panel _sidebar = new();
    private readonly Panel _contentHost = new();
    private readonly TableLayoutPanel _workspace = new();
    private readonly Label _sectionLabel = new();
    private readonly Label _brandLabel = new();
    private readonly Label _appCaption = new();
    private readonly Button _toggleButton = new();
    private readonly ToolTip _toolTip = new();
    private readonly Dictionary<NavigationButton, NavigationItem> _navigation = new();
    private NavigationButton? _activeButton;
    private Form? _activeForm;
    private bool _compact;
    private bool? _lastNarrowState;

    public FormPrincipal()
    {
        Text = "SystemAnalyzer — Analizador de Sistema Operativo y Rendimiento";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 600);
        ClientSize = new Size(1340, 730);
        BackColor = AppTheme.Background;
        Font = new Font("Segoe UI", 9F);
        AutoScaleMode = AutoScaleMode.Dpi;
        KeyPreview = true;

        BuildSidebar();
        _contentHost.Dock = DockStyle.Fill;
        _contentHost.BackColor = AppTheme.Background;
        _contentHost.Margin = Padding.Empty;

        _workspace.Dock = DockStyle.Fill;
        _workspace.Margin = Padding.Empty;
        _workspace.Padding = Padding.Empty;
        _workspace.ColumnCount = 1;
        _workspace.RowCount = 2;
        _workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        _workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        _workspace.Controls.Add(BuildHeader(), 0, 0);
        _workspace.Controls.Add(_contentHost, 0, 1);

        Controls.Add(_workspace);
        Controls.Add(_sidebar);

        ClientSizeChanged += (_, _) => ApplyAutomaticSidebarMode();
        Shown += (_, _) => ApplyAutomaticSidebarMode();
        var first = _navigation.Keys.First();
        Navigate(first, _navigation[first].Factory);
    }

    private void BuildSidebar()
    {
        _sidebar.Dock = DockStyle.Left;
        _sidebar.Width = ExpandedWidth;
        _sidebar.BackColor = AppTheme.SidebarBackground;

        var brand = new Panel { Dock = DockStyle.Top, Height = 104, BackColor = AppTheme.SidebarBackground };
        _brandLabel.Text = "SYSTEM\nANALYZER";
        _brandLabel.AutoSize = true;
        _brandLabel.ForeColor = AppTheme.AccentPrimary;
        _brandLabel.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
        _brandLabel.Location = new Point(24, 25);
        brand.Controls.Add(_brandLabel);

        _toggleButton.Text = "‹";
        _toggleButton.Dock = DockStyle.Bottom;
        _toggleButton.Height = 48;
        _toggleButton.FlatStyle = FlatStyle.Flat;
        _toggleButton.FlatAppearance.BorderSize = 0;
        _toggleButton.BackColor = AppTheme.SidebarBackground;
        _toggleButton.ForeColor = AppTheme.TextSecondary;
        _toggleButton.Font = new Font("Segoe UI", 16F);
        _toggleButton.Cursor = Cursors.Hand;
        _toggleButton.TabStop = true;
        _toggleButton.TabIndex = 7;
        _toggleButton.AccessibleName = "Contraer o expandir menú";
        _toggleButton.FlatAppearance.MouseOverBackColor = AppTheme.HoverBackground;
        _toggleButton.FlatAppearance.MouseDownBackColor = AppTheme.SurfaceElevated;
        _toggleButton.Click += (_, _) => SetSidebarCompact(!_compact);
        _toolTip.SetToolTip(_toggleButton, "Contraer o expandir menú");

        AddNavigationButton("\uE80F", "Inicio", () => new FormInicio());
        AddNavigationButton("\uE950", "Sistema", () => new FormSistema());
        AddNavigationButton("\uE9D9", "Procesos", () => new FormProcesos());
        AddNavigationButton("\uE9D2", "Mayor consumo", () => new FormConsumo());
        AddNavigationButton("\uE958", "Almacenamiento", () => new FormAlmacenamiento());
        AddNavigationButton("\uE9D5", "Diagnóstico", () => new FormDiagnostico());
        AddNavigationButton("\uE946", "Créditos", () => new FormCreditos());

        _sidebar.Controls.Add(_toggleButton);
        _sidebar.Controls.Add(brand);
    }

    private Panel BuildHeader()
    {
        var header = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.SurfacePrimary, Padding = new Padding(24, 0, 24, 0), Margin = Padding.Empty };
        _sectionLabel.AutoSize = true;
        _sectionLabel.Text = "Inicio";
        _sectionLabel.ForeColor = AppTheme.TextPrimary;
        _sectionLabel.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
        _sectionLabel.Location = new Point(24, 23);

        _appCaption.AutoSize = true;
        _appCaption.Text = "Analizador de Sistema Operativo y Rendimiento";
        _appCaption.ForeColor = AppTheme.TextSecondary;
        _appCaption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        header.Controls.Add(_appCaption);
        header.Controls.Add(_sectionLabel);
        header.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = AppTheme.BorderSubtle });
        header.Resize += (_, _) =>
        {
            _appCaption.Visible = header.ClientSize.Width >= 650;
            _appCaption.Location = new Point(Math.Max(24, header.ClientSize.Width - _appCaption.PreferredWidth - 24), 27);
        };
        return header;
    }

    private void AddNavigationButton(string icon, string name, Func<Form> factory)
    {
        var button = new NavigationButton(icon, name) { TabIndex = _navigation.Count };
        button.Click += (_, _) => Navigate(button, factory);
        _navigation.Add(button, new NavigationItem(icon, name, factory));
        _toolTip.SetToolTip(button, name);
        _sidebar.Controls.Add(button);
        button.BringToFront();
    }

    private void ApplyAutomaticSidebarMode()
    {
        var narrow = ClientSize.Width < 1180;
        if (_lastNarrowState == narrow) return;
        _lastNarrowState = narrow;
        SetSidebarCompact(narrow);
    }

    private void SetSidebarCompact(bool compact)
    {
        _compact = compact;
        _sidebar.Width = compact ? CompactWidth : ExpandedWidth;
        _brandLabel.Text = compact ? "SA" : "SYSTEM\nANALYZER";
        _brandLabel.Location = compact ? new Point(21, 35) : new Point(24, 25);
        _toggleButton.Text = compact ? "›" : "‹";
        foreach (var pair in _navigation)
        {
            pair.Key.Compact = compact;
        }
    }

    private void Navigate(NavigationButton button, Func<Form> formFactory)
    {
        if (_activeButton == button) return;
        if (_activeButton is not null) _activeButton.Selected = false;
        _activeButton = button;
        button.Selected = true;
        _sectionLabel.Text = _navigation[button].Name;

        if (_activeForm is not null)
        {
            _contentHost.Controls.Remove(_activeForm);
            _activeForm.Dispose();
        }

        _activeForm = formFactory();
        _contentHost.Controls.Add(_activeForm);
        _activeForm.Show();
    }

    private sealed record NavigationItem(string Icon, string Name, Func<Form> Factory);
}
