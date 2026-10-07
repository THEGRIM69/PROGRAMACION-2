#nullable enable

namespace FolletoDigital;

partial class FormFinal
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Panel pnlTarjeta = null!;
    private Label lblDescripcion = null!;
    private Label lblCreadoPor = null!;
    private Label lblNombre = null!;
    private Label lblCarnetTitulo = null!;
    private Label lblCarnet = null!;
    private Label lblTecnologia = null!;
    private Label lblInterfaz = null!;
    private Label lblTipo = null!;

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
        pnlTarjeta = new Panel();
        lblDescripcion = new Label();
        lblCreadoPor = new Label();
        lblNombre = new Label();
        lblCarnetTitulo = new Label();
        lblCarnet = new Label();
        lblTecnologia = new Label();
        lblInterfaz = new Label();
        lblTipo = new Label();
        pnlHeader.SuspendLayout();
        pnlTarjeta.SuspendLayout();
        SuspendLayout();

        pnlHeader.BackColor = Color.FromArgb(22, 33, 62);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(984, 112);
        pnlHeader.TabIndex = 0;

        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(45, 31);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(335, 46);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "ACERCA DEL SISTEMA";

        pnlTarjeta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlTarjeta.BackColor = Color.White;
        pnlTarjeta.Controls.Add(lblDescripcion);
        pnlTarjeta.Controls.Add(lblCreadoPor);
        pnlTarjeta.Controls.Add(lblNombre);
        pnlTarjeta.Controls.Add(lblCarnetTitulo);
        pnlTarjeta.Controls.Add(lblCarnet);
        pnlTarjeta.Controls.Add(lblTecnologia);
        pnlTarjeta.Controls.Add(lblInterfaz);
        pnlTarjeta.Controls.Add(lblTipo);
        pnlTarjeta.Location = new Point(65, 150);
        pnlTarjeta.Name = "pnlTarjeta";
        pnlTarjeta.Padding = new Padding(30);
        pnlTarjeta.Size = new Size(850, 390);
        pnlTarjeta.TabIndex = 1;

        lblDescripcion.Font = new Font("Segoe UI", 12F);
        lblDescripcion.ForeColor = Color.FromArgb(55, 65, 81);
        lblDescripcion.Location = new Point(30, 25);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(790, 68);
        lblDescripcion.TabIndex = 0;
        lblDescripcion.Text = "Control de Gastos Personales es una aplicación desarrollada en C# Windows Forms para registrar, consultar y analizar gastos personales de manera sencilla.";

        lblCreadoPor.AutoSize = true;
        lblCreadoPor.Font = new Font("Segoe UI", 10F);
        lblCreadoPor.ForeColor = Color.FromArgb(90, 101, 120);
        lblCreadoPor.Location = new Point(30, 119);
        lblCreadoPor.Name = "lblCreadoPor";
        lblCreadoPor.Size = new Size(112, 19);
        lblCreadoPor.TabIndex = 1;
        lblCreadoPor.Text = "Proyecto creado por:";

        lblNombre.AutoSize = true;
        lblNombre.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblNombre.ForeColor = Color.FromArgb(15, 52, 96);
        lblNombre.Location = new Point(30, 145);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(220, 31);
        lblNombre.TabIndex = 2;
        lblNombre.Text = "Ing. Rodrigo Rivera";

        lblCarnetTitulo.AutoSize = true;
        lblCarnetTitulo.Font = new Font("Segoe UI", 10F);
        lblCarnetTitulo.ForeColor = Color.FromArgb(90, 101, 120);
        lblCarnetTitulo.Location = new Point(30, 195);
        lblCarnetTitulo.Name = "lblCarnetTitulo";
        lblCarnetTitulo.Size = new Size(49, 19);
        lblCarnetTitulo.TabIndex = 3;
        lblCarnetTitulo.Text = "Carnet:";

        lblCarnet.AutoSize = true;
        lblCarnet.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblCarnet.ForeColor = Color.FromArgb(15, 52, 96);
        lblCarnet.Location = new Point(30, 222);
        lblCarnet.Name = "lblCarnet";
        lblCarnet.Size = new Size(162, 25);
        lblCarnet.TabIndex = 4;
        lblCarnet.Text = "RF1631012025";

        lblTecnologia.AutoSize = true;
        lblTecnologia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTecnologia.ForeColor = Color.FromArgb(15, 52, 96);
        lblTecnologia.Location = new Point(30, 290);
        lblTecnologia.Name = "lblTecnologia";
        lblTecnologia.Size = new Size(111, 19);
        lblTecnologia.TabIndex = 5;
        lblTecnologia.Text = "Tecnología: C#";

        lblInterfaz.AutoSize = true;
        lblInterfaz.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblInterfaz.ForeColor = Color.FromArgb(15, 52, 96);
        lblInterfaz.Location = new Point(280, 290);
        lblInterfaz.Name = "lblInterfaz";
        lblInterfaz.Size = new Size(173, 19);
        lblInterfaz.TabIndex = 6;
        lblInterfaz.Text = "Interfaz: Windows Forms";

        lblTipo.AutoSize = true;
        lblTipo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTipo.ForeColor = Color.FromArgb(15, 52, 96);
        lblTipo.Location = new Point(570, 290);
        lblTipo.Name = "lblTipo";
        lblTipo.Size = new Size(160, 19);
        lblTipo.TabIndex = 7;
        lblTipo.Text = "Tipo: Proyecto académico";

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(pnlTarjeta);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormFinal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Acerca del sistema";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlTarjeta.ResumeLayout(false);
        pnlTarjeta.PerformLayout();
        ResumeLayout(false);
    }
}
