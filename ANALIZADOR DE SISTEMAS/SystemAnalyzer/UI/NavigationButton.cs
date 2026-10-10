using System.Drawing.Drawing2D;

namespace SystemAnalyzer.UI;

internal sealed class NavigationButton : Button
{
    private bool _compact;
    private bool _selected;
    private bool _hovered;
    private bool _pressed;

    public string IconGlyph { get; }
    public string NavigationText { get; }

    public bool Compact
    {
        get => _compact;
        set { _compact = value; Invalidate(); }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if (_selected == value) return;
            _selected = value;
            AccessibleDescription = value
                ? $"{NavigationText}, sección actual"
                : $"Abrir la sección {NavigationText}";
            Invalidate();
        }
    }

    public NavigationButton(string iconGlyph, string navigationText)
    {
        IconGlyph = iconGlyph;
        NavigationText = navigationText;
        Dock = DockStyle.Top;
        Height = 52;
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        BackColor = AppTheme.SidebarBackground;
        ForeColor = AppTheme.TextPrimary;
        Cursor = Cursors.Hand;
        TabStop = true;
        AccessibleName = navigationText;
        AccessibleDescription = $"Abrir la sección {navigationText}";
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
    }

    protected override void OnMouseEnter(EventArgs e) { _hovered = true; Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _hovered = false; _pressed = false; Invalidate(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) _pressed = true; Invalidate(); base.OnMouseDown(e); }
    protected override void OnMouseUp(MouseEventArgs e) { _pressed = false; Invalidate(); base.OnMouseUp(e); }
    protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
    protected override void OnLostFocus(EventArgs e) { _pressed = false; Invalidate(); base.OnLostFocus(e); }
    protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var background = !Enabled ? AppTheme.SurfaceSecondary
            : _pressed ? AppTheme.SurfaceElevated
            : _selected ? AppTheme.SelectionBackground
            : _hovered ? AppTheme.HoverBackground
            : AppTheme.SidebarBackground;
        e.Graphics.Clear(background);

        if (_selected)
            using (var marker = new SolidBrush(AppTheme.AccentPrimary))
                e.Graphics.FillRectangle(marker, 0, 0, 3, Height);

        var foreground = Enabled ? AppTheme.TextPrimary : AppTheme.TextDisabled;
        using var iconFont = new Font("Segoe MDL2 Assets", 14F, FontStyle.Regular, GraphicsUnit.Point);
        using var textFont = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        using var foregroundBrush = new SolidBrush(foreground);
        var iconBounds = _compact ? new Rectangle(3, 0, Width - 3, Height) : new Rectangle(18, 0, 28, Height);
        using var centered = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        e.Graphics.DrawString(IconGlyph, iconFont, foregroundBrush, iconBounds, centered);

        if (!_compact)
        {
            var textBounds = new Rectangle(56, 0, Math.Max(0, Width - 68), Height);
            using var left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
            e.Graphics.DrawString(NavigationText, textFont, foregroundBrush, textBounds, left);
        }

        if (Focused && ShowFocusCues)
        {
            using var focusPen = new Pen(AppTheme.FocusRing, 2F) { DashStyle = DashStyle.Dot };
            e.Graphics.DrawRectangle(focusPen, 4, 3, Math.Max(1, Width - 9), Math.Max(1, Height - 7));
        }
    }
}
