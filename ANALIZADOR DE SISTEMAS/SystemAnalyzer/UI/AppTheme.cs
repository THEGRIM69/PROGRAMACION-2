namespace SystemAnalyzer.UI;

internal static class AppTheme
{
    public static readonly Color BackgroundPrimary = Color.FromArgb(11, 18, 32);
    public static readonly Color BackgroundSecondary = Color.FromArgb(15, 25, 41);
    public static readonly Color SidebarBackground = Color.FromArgb(13, 22, 38);
    public static readonly Color SurfacePrimary = Color.FromArgb(23, 36, 56);
    public static readonly Color SurfaceSecondary = Color.FromArgb(29, 45, 68);
    public static readonly Color SurfaceElevated = Color.FromArgb(34, 52, 77);
    public static readonly Color BorderSubtle = Color.FromArgb(42, 61, 85);
    public static readonly Color BorderStrong = Color.FromArgb(58, 83, 110);
    public static readonly Color TextPrimary = Color.FromArgb(242, 247, 252);
    public static readonly Color TextSecondary = Color.FromArgb(169, 184, 202);
    public static readonly Color TextDisabled = Color.FromArgb(113, 129, 152);
    public static readonly Color TextOnAccent = Color.FromArgb(6, 26, 34);
    public static readonly Color AccentPrimary = Color.FromArgb(39, 217, 232);
    public static readonly Color AccentPrimaryHover = Color.FromArgb(82, 229, 240);
    public static readonly Color AccentPrimaryPressed = Color.FromArgb(24, 184, 199);
    public static readonly Color AccentSecondary = Color.FromArgb(163, 139, 255);
    public static readonly Color AccentSecondaryHover = Color.FromArgb(182, 164, 255);
    public static readonly Color SelectionBackground = Color.FromArgb(22, 74, 99);
    public static readonly Color HoverBackground = Color.FromArgb(33, 55, 80);
    public static readonly Color FocusRing = Color.FromArgb(114, 233, 242);
    public static readonly Color StatusSuccess = Color.FromArgb(80, 217, 172);
    public static readonly Color StatusWarning = Color.FromArgb(242, 193, 78);
    public static readonly Color StatusError = Color.FromArgb(255, 112, 122);
    public static readonly Color StatusInfo = Color.FromArgb(98, 168, 255);
    public static readonly Color Track = Color.FromArgb(42, 61, 85);
    public static readonly Color Overlay = Color.FromArgb(204, 8, 16, 28);

    // Compatibility aliases used by the existing modules. Keep these until each
    // screen adopts the semantic token names in its corresponding UI phase.
    public static readonly Color Background = BackgroundPrimary;
    public static readonly Color Sidebar = SidebarBackground;
    public static readonly Color Surface = SurfacePrimary;
    public static readonly Color SurfaceHover = HoverBackground;
    public static readonly Color Primary = AccentPrimary;
    public static readonly Color Text = TextPrimary;
    public static readonly Color MutedText = TextSecondary;

    public static Button CreateNavigationButton(string text)
    {
        return new Button
        {
            Text = text,
            Dock = DockStyle.Top,
            Height = 52,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            BackColor = SidebarBackground,
            ForeColor = TextPrimary,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(22, 0, 0, 0),
            Cursor = Cursors.Hand,
            TabStop = true
        };
    }
}
