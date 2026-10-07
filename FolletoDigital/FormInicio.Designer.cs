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
        pnlHeader.SuspendLayout();
        pnlTotalGastado.SuspendLayout();
        pnlCantidadGastos.SuspendLayout();
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

        pnlTotalGastado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlTotalGastado.BackColor = Color.White;
        pnlTotalGastado.Controls.Add(lblTotalGastadoTitulo);
        pnlTotalGastado.Controls.Add(lblTotalGastado);
        pnlTotalGastado.Location = new Point(55, 190);
        pnlTotalGastado.Name = "pnlTotalGastado";
        pnlTotalGastado.Size = new Size(410, 155);
        pnlTotalGastado.TabIndex = 1;

        lblTotalGastadoTitulo.AutoSize = true;
        lblTotalGastadoTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalGastadoTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblTotalGastadoTitulo.Location = new Point(25, 25);
        lblTotalGastadoTitulo.Name = "lblTotalGastadoTitulo";
        lblTotalGastadoTitulo.Size = new Size(131, 21);
        lblTotalGastadoTitulo.TabIndex = 0;
        lblTotalGastadoTitulo.Text = "TOTAL GASTADO";

        lblTotalGastado.AutoSize = true;
        lblTotalGastado.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
        lblTotalGastado.ForeColor = Color.FromArgb(58, 134, 255);
        lblTotalGastado.Location = new Point(25, 66);
        lblTotalGastado.Name = "lblTotalGastado";
        lblTotalGastado.Size = new Size(110, 46);
        lblTotalGastado.TabIndex = 1;
        lblTotalGastado.Text = "$0.00";

        pnlCantidadGastos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlCantidadGastos.BackColor = Color.White;
        pnlCantidadGastos.Controls.Add(lblCantidadGastosTitulo);
        pnlCantidadGastos.Controls.Add(lblCantidadGastos);
        pnlCantidadGastos.Location = new Point(519, 190);
        pnlCantidadGastos.Name = "pnlCantidadGastos";
        pnlCantidadGastos.Size = new Size(410, 155);
        pnlCantidadGastos.TabIndex = 2;

        lblCantidadGastosTitulo.AutoSize = true;
        lblCantidadGastosTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCantidadGastosTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblCantidadGastosTitulo.Location = new Point(25, 25);
        lblCantidadGastosTitulo.Name = "lblCantidadGastosTitulo";
        lblCantidadGastosTitulo.Size = new Size(192, 21);
        lblCantidadGastosTitulo.TabIndex = 0;
        lblCantidadGastosTitulo.Text = "GASTOS REGISTRADOS";

        lblCantidadGastos.AutoSize = true;
        lblCantidadGastos.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
        lblCantidadGastos.ForeColor = Color.FromArgb(22, 33, 62);
        lblCantidadGastos.Location = new Point(25, 66);
        lblCantidadGastos.Name = "lblCantidadGastos";
        lblCantidadGastos.Size = new Size(40, 46);
        lblCantidadGastos.TabIndex = 1;
        lblCantidadGastos.Text = "0";

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
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
        ResumeLayout(false);
    }
}
