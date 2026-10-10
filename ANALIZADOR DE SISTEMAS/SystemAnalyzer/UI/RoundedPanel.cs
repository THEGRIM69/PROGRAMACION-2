using System.Drawing.Drawing2D;

namespace SystemAnalyzer.UI;

internal class RoundedPanel : Panel
{
    private Size _regionSize;
    private int _regionRadius;
    private int _cornerRadius = 12;

    public int CornerRadius
    {
        get => _cornerRadius;
        set
        {
            var normalized = Math.Max(1, value);
            if (_cornerRadius == normalized) return;
            _cornerRadius = normalized;
            UpdateRegion();
            Invalidate();
        }
    }
    public Color BorderColor { get; set; } = AppTheme.BorderSubtle;

    public RoundedPanel()
    {
        DoubleBuffered = true;
        BackColor = AppTheme.SurfacePrimary;
        Resize += HandleResize;
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
        if (Region is not null && _regionSize == ClientSize && _regionRadius == CornerRadius) return;

        using var path = CreatePath(ClientRectangle, CornerRadius);
        _regionSize = ClientSize;
        _regionRadius = CornerRadius;
        Region = new Region(path);
    }

    private void HandleResize(object? sender, EventArgs e) => UpdateRegion();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Resize -= HandleResize;
            Region = null;
        }

        base.Dispose(disposing);
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
