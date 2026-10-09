using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public abstract class BaseContentForm : Form
{
    protected readonly Panel ContentPanel;

    protected BaseContentForm(string title, string description)
    {
        FormBorderStyle = FormBorderStyle.None;
        TopLevel = false;
        Dock = DockStyle.Fill;
        BackColor = AppTheme.BackgroundPrimary;
        AutoScroll = true;
        AutoScaleMode = AutoScaleMode.Dpi;

        var heading = new Label
        {
            AutoSize = true,
            Text = title,
            ForeColor = AppTheme.TextPrimary,
            Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
            Location = new Point(24, 24)
        };

        var subtitle = new Label
        {
            AutoSize = true,
            Text = description,
            ForeColor = AppTheme.TextSecondary,
            Font = new Font("Segoe UI", 9F),
            Location = new Point(24, 66)
        };

        ContentPanel = new Panel
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = AppTheme.SurfacePrimary,
            Location = new Point(24, 104),
            Size = new Size(980, 500),
            Padding = new Padding(20)
        };

        Controls.Add(ContentPanel);
        Controls.Add(subtitle);
        Controls.Add(heading);
        Resize += (_, _) => ApplyResponsiveLayout();
        Shown += (_, _) => ApplyResponsiveLayout();
    }

    private void ApplyResponsiveLayout()
    {
        var horizontalPadding = ClientSize.Width < 700 ? 16 : 24;
        ContentPanel.Location = new Point(horizontalPadding, 104);
        ContentPanel.Size = new Size(
            Math.Max(280, ClientSize.Width - horizontalPadding * 2),
            Math.Max(260, ClientSize.Height - 128));
    }

    protected void ShowPhaseMessage(string message)
    {
        ContentPanel.Controls.Add(new Label
        {
            Dock = DockStyle.Fill,
            Text = message,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = AppTheme.MutedText,
            Font = new Font("Segoe UI", 12F)
        });
    }
}
