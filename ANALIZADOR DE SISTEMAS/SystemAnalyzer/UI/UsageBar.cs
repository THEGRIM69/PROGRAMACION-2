namespace SystemAnalyzer.UI;

internal sealed class UsageBar : Control
{
    private double _value;
    public double Value { get => _value; set { _value = Math.Clamp(value, 0, 100); Invalidate(); } }
    public Color FillColor { get; set; } = AppTheme.AccentPrimary;

    public UsageBar()
    {
        DoubleBuffered = true;
        Height = 7;
        BackColor = AppTheme.Track;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var width = (int)Math.Round(ClientSize.Width * Value / 100d);
        if (width <= 0) return;
        using var brush = new SolidBrush(FillColor);
        e.Graphics.FillRectangle(brush, 0, 0, width, ClientSize.Height);
    }
}
