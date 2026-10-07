namespace SystemAnalyzer.UI;

internal static class AppTheme
{
    public static readonly Color Background = Color.FromArgb(15, 23, 42);
    public static readonly Color Sidebar = Color.FromArgb(17, 24, 39);
    public static readonly Color Surface = Color.FromArgb(30, 41, 59);
    public static readonly Color SurfaceHover = Color.FromArgb(51, 65, 85);
    public static readonly Color Primary = Color.FromArgb(56, 189, 248);
    public static readonly Color Text = Color.FromArgb(241, 245, 249);
    public static readonly Color MutedText = Color.FromArgb(148, 163, 184);

    public static Button CreateNavigationButton(string text)
    {
        return new Button
        {
            Text = text,
            Dock = DockStyle.Top,
            Height = 54,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            BackColor = Sidebar,
            ForeColor = Text,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(22, 0, 0, 0),
            Cursor = Cursors.Hand,
            TabStop = false
        };
    }
}
