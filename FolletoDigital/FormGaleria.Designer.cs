#nullable enable

namespace FolletoDigital;

partial class FormGaleria
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblTotalGastadoTitulo = null!;
    private Label lblTotalGastado = null!;
    private Label lblCantidadGastosTitulo = null!;
    private Label lblCantidadGastos = null!;
    private Label lblGastoMayorTitulo = null!;
    private Label lblGastoMayor = null!;
    private Label lblCategoriaPredominanteTitulo = null!;
    private Label lblCategoriaPredominante = null!;
    private ListBox lstResumen = null!;

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
        lblTotalGastadoTitulo = new Label();
        lblTotalGastado = new Label();
        lblCantidadGastosTitulo = new Label();
        lblCantidadGastos = new Label();
        lblGastoMayorTitulo = new Label();
        lblGastoMayor = new Label();
        lblCategoriaPredominanteTitulo = new Label();
        lblCategoriaPredominante = new Label();
        lstResumen = new ListBox();
        pnlHeader.SuspendLayout();
        SuspendLayout();

        pnlHeader.BackColor = Color.FromArgb(22, 33, 62);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(984, 112);
        pnlHeader.TabIndex = 0;

        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 27F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(55, 31);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(224, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Resumen de gastos";

        lblTotalGastadoTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTotalGastadoTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblTotalGastadoTitulo.Location = new Point(55, 160);
        lblTotalGastadoTitulo.Name = "lblTotalGastadoTitulo";
        lblTotalGastadoTitulo.Size = new Size(220, 25);
        lblTotalGastadoTitulo.TabIndex = 1;
        lblTotalGastadoTitulo.Text = "Total gastado";

        lblTotalGastado.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTotalGastado.ForeColor = Color.FromArgb(58, 134, 255);
        lblTotalGastado.Location = new Point(55, 190);
        lblTotalGastado.Name = "lblTotalGastado";
        lblTotalGastado.Size = new Size(220, 30);
        lblTotalGastado.TabIndex = 2;
        lblTotalGastado.Text = "$0.00";

        lblCantidadGastosTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCantidadGastosTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblCantidadGastosTitulo.Location = new Point(310, 160);
        lblCantidadGastosTitulo.Name = "lblCantidadGastosTitulo";
        lblCantidadGastosTitulo.Size = new Size(240, 25);
        lblCantidadGastosTitulo.TabIndex = 3;
        lblCantidadGastosTitulo.Text = "Gastos registrados";

        lblCantidadGastos.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCantidadGastos.ForeColor = Color.FromArgb(22, 33, 62);
        lblCantidadGastos.Location = new Point(310, 190);
        lblCantidadGastos.Name = "lblCantidadGastos";
        lblCantidadGastos.Size = new Size(240, 30);
        lblCantidadGastos.TabIndex = 4;
        lblCantidadGastos.Text = "0";

        lblGastoMayorTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblGastoMayorTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblGastoMayorTitulo.Location = new Point(565, 160);
        lblGastoMayorTitulo.Name = "lblGastoMayorTitulo";
        lblGastoMayorTitulo.Size = new Size(220, 25);
        lblGastoMayorTitulo.TabIndex = 5;
        lblGastoMayorTitulo.Text = "Gasto de mayor monto";

        lblGastoMayor.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblGastoMayor.ForeColor = Color.FromArgb(0, 128, 96);
        lblGastoMayor.Location = new Point(565, 190);
        lblGastoMayor.Name = "lblGastoMayor";
        lblGastoMayor.Size = new Size(220, 30);
        lblGastoMayor.TabIndex = 6;
        lblGastoMayor.Text = "$0.00";

        lblCategoriaPredominanteTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCategoriaPredominanteTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblCategoriaPredominanteTitulo.Location = new Point(55, 270);
        lblCategoriaPredominanteTitulo.Name = "lblCategoriaPredominanteTitulo";
        lblCategoriaPredominanteTitulo.Size = new Size(300, 25);
        lblCategoriaPredominanteTitulo.TabIndex = 7;
        lblCategoriaPredominanteTitulo.Text = "Categoría con mayor gasto";

        lblCategoriaPredominante.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblCategoriaPredominante.ForeColor = Color.FromArgb(15, 52, 96);
        lblCategoriaPredominante.Location = new Point(55, 300);
        lblCategoriaPredominante.Name = "lblCategoriaPredominante";
        lblCategoriaPredominante.Size = new Size(300, 30);
        lblCategoriaPredominante.TabIndex = 8;
        lblCategoriaPredominante.Text = "Ninguna";

        lstResumen.Font = new Font("Segoe UI", 11F);
        lstResumen.FormattingEnabled = true;
        lstResumen.ItemHeight = 24;
        lstResumen.Location = new Point(430, 270);
        lstResumen.Name = "lstResumen";
        lstResumen.Size = new Size(500, 220);
        lstResumen.TabIndex = 9;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(lstResumen);
        Controls.Add(lblCategoriaPredominante);
        Controls.Add(lblCategoriaPredominanteTitulo);
        Controls.Add(lblGastoMayor);
        Controls.Add(lblGastoMayorTitulo);
        Controls.Add(lblCantidadGastos);
        Controls.Add(lblCantidadGastosTitulo);
        Controls.Add(lblTotalGastado);
        Controls.Add(lblTotalGastadoTitulo);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormGaleria";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Resumen";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
