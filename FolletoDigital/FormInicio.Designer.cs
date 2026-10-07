#nullable enable

namespace FolletoDigital;

partial class FormInicio
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblBienvenida = null!;
    private Panel pnlTotalGastado = null!;
    private Label lblTotalGastadoTitulo = null!;
    private Label lblTotalGastado = null!;
    private Panel pnlCantidadGastos = null!;
    private Label lblCantidadGastosTitulo = null!;
    private Label lblCantidadGastos = null!;
    private Panel pnlGastoMayor = null!;
    private Label lblGastoMayorTitulo = null!;
    private Label lblGastoMayor = null!;
    private Panel pnlCategoriaTop = null!;
    private Label lblCategoriaTopTitulo = null!;
    private Label lblCategoriaTop = null!;
    private Label lblUltimosGastos = null!;
    private DataGridView dgvUltimosGastos = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlHeader = new Panel();
        lblTitulo = new Label();
        lblBienvenida = new Label();
        pnlTotalGastado = new Panel();
        lblTotalGastadoTitulo = new Label();
        lblTotalGastado = new Label();
        pnlCantidadGastos = new Panel();
        lblCantidadGastosTitulo = new Label();
        lblCantidadGastos = new Label();
        pnlGastoMayor = new Panel();
        lblGastoMayorTitulo = new Label();
        lblGastoMayor = new Label();
        pnlCategoriaTop = new Panel();
        lblCategoriaTopTitulo = new Label();
        lblCategoriaTop = new Label();
        lblUltimosGastos = new Label();
        dgvUltimosGastos = new DataGridView();
        pnlHeader.SuspendLayout();
        pnlTotalGastado.SuspendLayout();
        pnlCantidadGastos.SuspendLayout();
        pnlGastoMayor.SuspendLayout();
        pnlCategoriaTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUltimosGastos).BeginInit();
        SuspendLayout();

        pnlHeader.BackColor = Color.FromArgb(22, 33, 62);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Controls.Add(lblBienvenida);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(984, 130);
        pnlHeader.TabIndex = 0;

        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(50, 24);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(180, 46);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Dashboard";

        lblBienvenida.Font = new Font("Segoe UI", 12F);
        lblBienvenida.ForeColor = Color.FromArgb(208, 222, 255);
        lblBienvenida.Location = new Point(54, 78);
        lblBienvenida.Name = "lblBienvenida";
        lblBienvenida.Size = new Size(850, 30);
        lblBienvenida.TabIndex = 1;
        lblBienvenida.Text = "Bienvenido al Control de Gastos Personales. Administra y consulta tus gastos desde un solo lugar.";

        ConfigurarTarjeta(pnlTotalGastado, new Point(55, 158), "TOTAL GASTADO", lblTotalGastadoTitulo, lblTotalGastado);
        lblTotalGastado.Text = "$0.00";
        lblTotalGastado.ForeColor = Color.FromArgb(58, 134, 255);

        ConfigurarTarjeta(pnlCantidadGastos, new Point(519, 158), "NÚMERO DE GASTOS", lblCantidadGastosTitulo, lblCantidadGastos);
        lblCantidadGastos.Text = "0";

        ConfigurarTarjeta(pnlGastoMayor, new Point(55, 278), "GASTO MÁS ALTO", lblGastoMayorTitulo, lblGastoMayor);
        lblGastoMayor.Text = "$0.00";
        lblGastoMayor.ForeColor = Color.FromArgb(0, 128, 96);

        ConfigurarTarjeta(pnlCategoriaTop, new Point(519, 278), "CATEGORÍA TOP", lblCategoriaTopTitulo, lblCategoriaTop);
        lblCategoriaTop.Text = "Ninguna";

        lblUltimosGastos.AutoSize = true;
        lblUltimosGastos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblUltimosGastos.ForeColor = Color.FromArgb(15, 52, 96);
        lblUltimosGastos.Location = new Point(55, 412);
        lblUltimosGastos.Name = "lblUltimosGastos";
        lblUltimosGastos.Size = new Size(150, 25);
        lblUltimosGastos.TabIndex = 5;
        lblUltimosGastos.Text = "Últimos gastos";

        dgvUltimosGastos.AllowUserToAddRows = false;
        dgvUltimosGastos.AllowUserToDeleteRows = false;
        dgvUltimosGastos.AllowUserToResizeRows = false;
        dgvUltimosGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUltimosGastos.BackgroundColor = Color.White;
        dgvUltimosGastos.BorderStyle = BorderStyle.None;
        dgvUltimosGastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvUltimosGastos.Location = new Point(55, 446);
        dgvUltimosGastos.MultiSelect = false;
        dgvUltimosGastos.Name = "dgvUltimosGastos";
        dgvUltimosGastos.ReadOnly = true;
        dgvUltimosGastos.RowHeadersVisible = false;
        dgvUltimosGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUltimosGastos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvUltimosGastos.Size = new Size(874, 139);
        dgvUltimosGastos.TabIndex = 6;
        dgvUltimosGastos.Columns.Add("Fecha", "Fecha");
        dgvUltimosGastos.Columns.Add("Descripcion", "Descripción");
        dgvUltimosGastos.Columns.Add("Categoria", "Categoría");
        dgvUltimosGastos.Columns.Add("Monto", "Monto");
        ConfigurarColumnas(dgvUltimosGastos);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(dgvUltimosGastos);
        Controls.Add(lblUltimosGastos);
        Controls.Add(pnlCategoriaTop);
        Controls.Add(pnlGastoMayor);
        Controls.Add(pnlCantidadGastos);
        Controls.Add(pnlTotalGastado);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormInicio";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Dashboard";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlTotalGastado.ResumeLayout(false);
        pnlTotalGastado.PerformLayout();
        pnlCantidadGastos.ResumeLayout(false);
        pnlCantidadGastos.PerformLayout();
        pnlGastoMayor.ResumeLayout(false);
        pnlGastoMayor.PerformLayout();
        pnlCategoriaTop.ResumeLayout(false);
        pnlCategoriaTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUltimosGastos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private static void ConfigurarTarjeta(
        Panel panel,
        Point ubicacion,
        string titulo,
        Label etiquetaTitulo,
        Label valor)
    {
        panel.BackColor = Color.White;
        panel.Controls.Add(etiquetaTitulo);
        panel.Controls.Add(valor);
        panel.Location = ubicacion;
        panel.Name = $"pnl{titulo.Replace(" ", string.Empty)}";
        panel.Size = new Size(410, 104);
        panel.TabIndex = 1;
        panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        etiquetaTitulo.AutoSize = true;
        etiquetaTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        etiquetaTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        etiquetaTitulo.Location = new Point(20, 15);
        etiquetaTitulo.Name = $"lbl{titulo.Replace(" ", string.Empty)}Titulo";
        etiquetaTitulo.Size = new Size(160, 19);
        etiquetaTitulo.TabIndex = 0;
        etiquetaTitulo.Text = titulo;

        valor.AutoEllipsis = true;
        valor.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
        valor.ForeColor = Color.FromArgb(22, 33, 62);
        valor.Location = new Point(20, 48);
        valor.Name = $"lbl{titulo.Replace(" ", string.Empty)}";
        valor.Size = new Size(370, 40);
        valor.TabIndex = 1;
    }

    private static void ConfigurarColumnas(DataGridView tabla)
    {
        var pesos = new[] { 15F, 40F, 25F, 20F };
        for (var indice = 0; indice < tabla.Columns.Count; indice++)
        {
            tabla.Columns[indice].FillWeight = pesos[indice];
            tabla.Columns[indice].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
