namespace SystemAnalyzer.UI;

internal static class ResponsiveLayout
{
    public static void Reflow(TableLayoutPanel grid, IReadOnlyList<Control> controls, int columns, int rowHeight, int gap = 7)
    {
        columns = Math.Max(1, columns);
        var rows = (int)Math.Ceiling(controls.Count / (double)columns);
        grid.SuspendLayout();
        grid.ColumnCount = columns;
        grid.RowCount = rows;
        grid.ColumnStyles.Clear();
        grid.RowStyles.Clear();
        for (var column = 0; column < columns; column++)
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));
        for (var row = 0; row < rows; row++)
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));

        for (var index = 0; index < controls.Count; index++)
        {
            var control = controls[index];
            var column = index % columns;
            var row = index / columns;
            grid.SetColumn(control, column);
            grid.SetRow(control, row);
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(column == 0 ? 0 : gap, row == 0 ? 0 : gap, column == columns - 1 ? 0 : gap, 0);
        }

        grid.Height = rows * rowHeight + Math.Max(0, rows - 1) * gap;
        grid.MinimumSize = new Size(0, grid.Height);
        grid.ResumeLayout(true);
    }
}
