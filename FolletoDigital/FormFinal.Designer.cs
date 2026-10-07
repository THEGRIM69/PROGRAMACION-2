#nullable enable

namespace FolletoDigital;

partial class FormFinal
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private Label lblTecnologia = null!;
    private Label lblInterfaz = null!;
    private Label lblProyecto = null!;
    private Button btnVolver = null!;
    private Button btnSalir = null!;

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
        lblDescripcion = new Label();
        lblTecnologia = new Label();
        lblInterfaz = new Label();
        lblProyecto = new Label();
        btnVolver = new Button();
        btnSalir = new Button();
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
        lblTitulo.Size = new Size(240, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Acerca del sistema";

        lblDescripcion.BackColor = Color.White;
        lblDescripcion.Font = new Font("Segoe UI", 14F);
        lblDescripcion.ForeColor = Color.FromArgb(37, 37, 37);
        lblDescripcion.Location = new Point(83, 170);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Padding = new Padding(28);
        lblDescripcion.Size = new Size(818, 100);
        lblDescripcion.TabIndex = 1;
        lblDescripcion.Text = "Control de Gastos Personales es una aplicación desarrollada en C# Windows Forms que permite registrar y consultar gastos, además de obtener un resumen básico de los gastos realizados.";
        lblDescripcion.TextAlign = ContentAlignment.MiddleLeft;

        lblTecnologia.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTecnologia.ForeColor = Color.FromArgb(15, 52, 96);
        lblTecnologia.Location = new Point(83, 305);
        lblTecnologia.Name = "lblTecnologia";
        lblTecnologia.Size = new Size(250, 35);
        lblTecnologia.TabIndex = 2;
        lblTecnologia.Text = "Tecnología: C#";

        lblInterfaz.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblInterfaz.ForeColor = Color.FromArgb(15, 52, 96);
        lblInterfaz.Location = new Point(83, 350);
        lblInterfaz.Name = "lblInterfaz";
        lblInterfaz.Size = new Size(300, 35);
        lblInterfaz.TabIndex = 3;
        lblInterfaz.Text = "Interfaz: Windows Forms";

        lblProyecto.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblProyecto.ForeColor = Color.FromArgb(15, 52, 96);
        lblProyecto.Location = new Point(83, 395);
        lblProyecto.Name = "lblProyecto";
        lblProyecto.Size = new Size(300, 35);
        lblProyecto.TabIndex = 4;
        lblProyecto.Text = "Proyecto académico";

        btnVolver.BackColor = Color.FromArgb(58, 134, 255);
        btnVolver.FlatAppearance.BorderSize = 0;
        btnVolver.FlatStyle = FlatStyle.Flat;
        btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnVolver.ForeColor = Color.White;
        btnVolver.Location = new Point(515, 520);
        btnVolver.Name = "btnVolver";
        btnVolver.Size = new Size(180, 45);
        btnVolver.TabIndex = 5;
        btnVolver.Text = "Volver al inicio";
        btnVolver.UseVisualStyleBackColor = false;
        btnVolver.Click += btnVolver_Click;

        btnSalir.BackColor = Color.FromArgb(22, 33, 62);
        btnSalir.FlatAppearance.BorderSize = 0;
        btnSalir.FlatStyle = FlatStyle.Flat;
        btnSalir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSalir.ForeColor = Color.White;
        btnSalir.Location = new Point(721, 520);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(180, 45);
        btnSalir.TabIndex = 6;
        btnSalir.Text = "Salir";
        btnSalir.UseVisualStyleBackColor = false;
        btnSalir.Click += btnSalir_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnSalir);
        Controls.Add(btnVolver);
        Controls.Add(lblProyecto);
        Controls.Add(lblInterfaz);
        Controls.Add(lblTecnologia);
        Controls.Add(lblDescripcion);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormFinal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Acerca del sistema";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
