using SystemAnalyzer.Models;
using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public sealed class FormProcessDetails : Form
{
    private readonly ToolTip _toolTip = new();
    private bool _resourcesDisposed;

    public FormProcessDetails(ProcessDetails details)
    {
        Text = $"Detalles — {details.Name}";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(620, 410);
        BackColor = AppTheme.Background;
        ForeColor = AppTheme.Text;
        Font = new Font("Segoe UI", 9.5F);
        AutoScaleMode = AutoScaleMode.Dpi;

        var title = new Label
        {
            Text = details.Name,
            AutoEllipsis = true,
            ForeColor = AppTheme.Text,
            Font = new Font("Segoe UI Semibold", 19F, FontStyle.Bold),
            Location = new Point(28, 22),
            Size = new Size(550, 40)
        };
        var card = new RoundedPanel { Location = new Point(28, 76), Size = new Size(564, 274), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
        var grid = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 16), ColumnCount = 2, RowCount = 7 };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        AddRow(grid, 0, "Nombre", details.Name);
        AddRow(grid, 1, "PID", details.Id.ToString());
        AddRow(grid, 2, "Memoria RAM", FormatBytes(details.WorkingSetBytes));
        AddRow(grid, 3, "CPU", details.CpuUsagePercent.HasValue ? $"{details.CpuUsagePercent:0.0}%" : "No disponible");
        AddRow(grid, 4, "Hilos", details.ThreadCount?.ToString() ?? "No disponible");
        AddRow(grid, 5, "Hora de inicio", details.StartTime?.ToString("g") ?? "No disponible");
        AddRow(grid, 6, "Ruta", details.ExecutablePath);
        card.Controls.Add(grid);

        var close = new ThemedButton
        {
            Text = "Cerrar", DialogResult = DialogResult.OK, Size = new Size(100, 34),
            Location = new Point(492, 363), Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            AccessibleName = "Cerrar detalles del proceso",
            TabIndex = 0
        };
        AcceptButton = close;
        CancelButton = close;
        Controls.Add(close);
        Controls.Add(card);
        Controls.Add(title);
    }

    private void AddRow(TableLayoutPanel grid, int row, string label, string value)
    {
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 7));
        grid.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, ForeColor = AppTheme.MutedText, TextAlign = ContentAlignment.MiddleLeft }, 0, row);
        var valueLabel = new Label { Text = value, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = AppTheme.Text, TextAlign = ContentAlignment.MiddleLeft };
        _toolTip.SetToolTip(valueLabel, value);
        grid.Controls.Add(valueLabel, 1, row);
    }

    private static string FormatBytes(long bytes) => bytes >= 1024d * 1024 * 1024
        ? $"{bytes / (1024d * 1024 * 1024):0.##} GB"
        : $"{bytes / (1024d * 1024):0.##} MB";

    protected override void Dispose(bool disposing)
    {
        if (disposing && !_resourcesDisposed)
        {
            _resourcesDisposed = true;
            _toolTip.Dispose();
        }

        base.Dispose(disposing);
    }
}
