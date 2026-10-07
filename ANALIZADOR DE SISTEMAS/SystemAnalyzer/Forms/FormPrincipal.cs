using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public sealed class FormPrincipal : Form
{
    private readonly Panel _contentHost;
    private readonly Label _sectionLabel;
    private readonly Dictionary<Button, Func<Form>> _navigation = new();
    private Button? _activeButton;
    private Form? _activeForm;

    public FormPrincipal()
    {
        Text = "SystemAnalyzer — Analizador de Sistema Operativo y Rendimiento";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1100, 650);
        ClientSize = new Size(1340, 730);
        BackColor = AppTheme.Background;
        Font = new Font("Segoe UI", 9F);

        var sidebar = new Panel { Dock = DockStyle.Left, Width = 240, BackColor = AppTheme.Sidebar };
        var brand = new Panel { Dock = DockStyle.Top, Height = 104, BackColor = AppTheme.Sidebar };
        brand.Controls.Add(new Label
        {
            Text = "SYSTEM\nANALYZER",
            AutoSize = true,
            ForeColor = AppTheme.Primary,
            Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold),
            Location = new Point(24, 25)
        });

        var header = new Panel { Dock = DockStyle.Top, Height = 72, BackColor = AppTheme.Surface };
        _sectionLabel = new Label
        {
            AutoSize = true,
            Text = "Inicio",
            ForeColor = AppTheme.Text,
            Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
            Location = new Point(28, 24)
        };
        var appCaption = new Label
        {
            AutoSize = true,
            Text = "Analizador de Sistema Operativo y Rendimiento",
            ForeColor = AppTheme.MutedText,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Location = new Point(760, 28)
        };
        header.Controls.Add(appCaption);
        header.Controls.Add(_sectionLabel);

        _contentHost = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Background };

        AddNavigationButton(sidebar, "  Inicio", () => new FormInicio());
        AddNavigationButton(sidebar, "  Sistema", () => new FormSistema());
        AddNavigationButton(sidebar, "  Procesos", () => new FormProcesos());
        AddNavigationButton(sidebar, "  Mayor consumo", () => new FormConsumo());
        AddNavigationButton(sidebar, "  Almacenamiento", () => new FormAlmacenamiento());
        AddNavigationButton(sidebar, "  Diagnóstico", () => new FormDiagnostico());
        AddNavigationButton(sidebar, "  Créditos", () => new FormCreditos());

        sidebar.Controls.Add(brand);
        Controls.Add(_contentHost);
        Controls.Add(header);
        Controls.Add(sidebar);

        var firstButton = _navigation.Keys.First();
        Navigate(firstButton, _navigation[firstButton]);
    }

    private void AddNavigationButton(Control sidebar, string text, Func<Form> formFactory)
    {
        var button = AppTheme.CreateNavigationButton(text);
        button.Click += (_, _) => Navigate(button, formFactory);
        button.MouseEnter += (_, _) => { if (button != _activeButton) button.BackColor = AppTheme.SurfaceHover; };
        button.MouseLeave += (_, _) => { if (button != _activeButton) button.BackColor = AppTheme.Sidebar; };
        _navigation.Add(button, formFactory);
        sidebar.Controls.Add(button);
        button.BringToFront();
    }

    private void Navigate(Button button, Func<Form> formFactory)
    {
        if (_activeButton == button) return;

        if (_activeButton is not null)
            _activeButton.BackColor = AppTheme.Sidebar;

        _activeButton = button;
        button.BackColor = AppTheme.SurfaceHover;
        _sectionLabel.Text = button.Text.Trim();

        if (_activeForm is not null)
        {
            _contentHost.Controls.Remove(_activeForm);
            _activeForm.Dispose();
        }

        _activeForm = formFactory();
        _contentHost.Controls.Add(_activeForm);
        _activeForm.Show();
    }
}
