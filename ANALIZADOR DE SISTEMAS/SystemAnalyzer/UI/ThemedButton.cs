using System.Drawing.Drawing2D;

namespace SystemAnalyzer.UI;

internal enum ThemedButtonStyle
{
    Primary,
    Secondary
}

internal sealed class ThemedButton : Button
{
    private bool _hovered;
    private bool _pressed;

    public ThemedButtonStyle ButtonStyle { get; set; } = ThemedButtonStyle.Primary;

    public ThemedButton()
    {
        Height = 40;
        MinimumSize = new Size(0, 40);
        Padding = new Padding(16, 0, 16, 0);
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        Cursor = Cursors.Hand;
        TabStop = true;
        Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
    }

    protected override void OnMouseEnter(EventArgs e) { _hovered = true; Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _hovered = false; _pressed = false; Invalidate(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) _pressed = true; Invalidate(); base.OnMouseDown(e); }
    protected override void OnMouseUp(MouseEventArgs e) { _pressed = false; Invalidate(); base.OnMouseUp(e); }
    protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
    protected override void OnLostFocus(EventArgs e) { _pressed = false; Invalidate(); base.OnLostFocus(e); }
    protected override void OnEnabledChanged(EventArgs e) { Cursor = Enabled ? Cursors.Hand : Cursors.Default; Invalidate(); base.OnEnabledChanged(e); }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var primary = ButtonStyle == ThemedButtonStyle.Primary;
        var background = !Enabled ? AppTheme.SurfaceSecondary
            : _pressed ? (primary ? AppTheme.AccentPrimaryPressed : AppTheme.SurfaceElevated)
            : _hovered ? (primary ? AppTheme.AccentPrimaryHover : AppTheme.HoverBackground)
            : primary ? AppTheme.AccentPrimary : AppTheme.SurfaceSecondary;
        var foreground = !Enabled ? AppTheme.TextDisabled
            : primary ? AppTheme.TextOnAccent : AppTheme.TextPrimary;

        using var path = CreatePath(ClientRectangle, 8);
        using var backgroundBrush = new SolidBrush(background);
        e.Graphics.FillPath(backgroundBrush, path);

        if (!primary && Enabled)
        {
            using var borderPen = new Pen(AppTheme.BorderStrong);
            e.Graphics.DrawPath(borderPen, path);
        }

        TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, foreground,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);

        if (Focused && ShowFocusCues)
        {
            var focusBounds = Rectangle.Inflate(ClientRectangle, -3, -3);
            using var focusPath = CreatePath(focusBounds, 6);
            using var focusPen = new Pen(AppTheme.FocusRing, 2F) { DashStyle = DashStyle.Dot };
            e.Graphics.DrawPath(focusPen, focusPath);
        }
    }

    private static GraphicsPath CreatePath(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        var rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
        var diameter = Math.Min(radius * 2, Math.Min(rectangle.Width, rectangle.Height));
        path.AddArc(rectangle.Left, rectangle.Top, diameter, diameter, 180, 90);
        path.AddArc(rectangle.Right - diameter, rectangle.Top, diameter, diameter, 270, 90);
        path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rectangle.Left, rectangle.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }
}
