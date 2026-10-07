using System.Drawing.Drawing2D;

namespace SystemAnalyzer.UI;

internal class RoundedPanel : Panel
{
    public int CornerRadius { get; set; } = 14;
    public Color BorderColor { get; set; } = Color.FromArgb(51, 65, 85);

    public RoundedPanel()
    {
        DoubleBuffered = true;
        BackColor = AppTheme.Surface;
        Resize += (_, _) => UpdateRegion();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = CreatePath(ClientRectangle, CornerRadius);
        using var pen = new Pen(BorderColor);
        e.Graphics.DrawPath(pen, path);
    }

    private void UpdateRegion()
    {
        if (Width <= 0 || Height <= 0) return;
        using var path = CreatePath(ClientRectangle, CornerRadius);
        Region = new Region(path);
    }

    private static GraphicsPath CreatePath(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        var diameter = Math.Max(2, radius * 2);
        var rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
        path.AddArc(rectangle.Left, rectangle.Top, diameter, diameter, 180, 90);
        path.AddArc(rectangle.Right - diameter, rectangle.Top, diameter, diameter, 270, 90);
        path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rectangle.Left, rectangle.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }
}
