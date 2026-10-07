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
        BackColor = AppTheme.Background;
        AutoScroll = true;

        var heading = new Label
        {
            AutoSize = true,
            Text = title,
            ForeColor = AppTheme.Text,
            Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
            Location = new Point(34, 28)
        };

        var subtitle = new Label
        {
            AutoSize = true,
            Text = description,
            ForeColor = AppTheme.MutedText,
            Font = new Font("Segoe UI", 10F),
            Location = new Point(38, 76)
        };

        ContentPanel = new Panel
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = AppTheme.Surface,
            Location = new Point(38, 118),
            Size = new Size(980, 500),
            Padding = new Padding(28)
        };

        Controls.Add(ContentPanel);
        Controls.Add(subtitle);
        Controls.Add(heading);
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
