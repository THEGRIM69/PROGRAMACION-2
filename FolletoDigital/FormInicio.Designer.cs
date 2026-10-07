#nullable enable

namespace FolletoDigital;

partial class FormInicio
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblBienvenida = null!;
    private Panel pnlContenido = null!;
    private Label lblTotalGastadoTitulo = null!;
    private Label lblTotalGastado = null!;
    private Label lblCantidadGastosTitulo = null!;
    private Label lblCantidadGastos = null!;
    private Panel pnlAcciones = null!;
    private Button btnRegistrarGasto = null!;
    private Button btnVerGastos = null!;
    private Button btnResumen = null!;
    private Button btnAcercaDe = null!;
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
        lblBienvenida = new Label();
        pnlContenido = new Panel();
        lblTotalGastadoTitulo = new Label();
        lblTotalGastado = new Label();
        lblCantidadGastosTitulo = new Label();
        lblCantidadGastos = new Label();
        pnlAcciones = new Panel();
        btnRegistrarGasto = new Button();
        btnVerGastos = new Button();
        btnResumen = new Button();
        btnAcercaDe = new Button();
        btnSalir = new Button();
        pnlHeader.SuspendLayout();
        pnlContenido.SuspendLayout();
        pnlAcciones.SuspendLayout();
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
        lblTitulo.Size = new Size(421, 46);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Control de Gastos Personales";

        lblBienvenida.AutoSize = true;
        lblBienvenida.Font = new Font("Segoe UI", 13F);
        lblBienvenida.ForeColor = Color.FromArgb(208, 222, 255);
        lblBienvenida.Location = new Point(54, 78);
        lblBienvenida.Name = "lblBienvenida";
        lblBienvenida.Size = new Size(375, 25);
        lblBienvenida.TabIndex = 1;
        lblBienvenida.Text = "¡Bienvenido! Gestiona tus gastos de forma sencilla.";

        pnlContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pnlContenido.BackColor = Color.White;
        pnlContenido.Controls.Add(lblTotalGastadoTitulo);
        pnlContenido.Controls.Add(lblTotalGastado);
        pnlContenido.Controls.Add(lblCantidadGastosTitulo);
        pnlContenido.Controls.Add(lblCantidadGastos);
        pnlContenido.Location = new Point(80, 170);
        pnlContenido.Name = "pnlContenido";
        pnlContenido.Size = new Size(824, 120);
        pnlContenido.TabIndex = 2;

        lblTotalGastadoTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalGastadoTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblTotalGastadoTitulo.Location = new Point(25, 25);
        lblTotalGastadoTitulo.Name = "lblTotalGastadoTitulo";
        lblTotalGastadoTitulo.Size = new Size(170, 25);
        lblTotalGastadoTitulo.TabIndex = 0;
        lblTotalGastadoTitulo.Text = "Total gastado";

        lblTotalGastado.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTotalGastado.ForeColor = Color.FromArgb(58, 134, 255);
        lblTotalGastado.Location = new Point(25, 60);
        lblTotalGastado.Name = "lblTotalGastado";
        lblTotalGastado.Size = new Size(250, 35);
        lblTotalGastado.TabIndex = 1;
        lblTotalGastado.Text = "$0.00";

        lblCantidadGastosTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCantidadGastosTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblCantidadGastosTitulo.Location = new Point(350, 25);
        lblCantidadGastosTitulo.Name = "lblCantidadGastosTitulo";
        lblCantidadGastosTitulo.Size = new Size(220, 25);
        lblCantidadGastosTitulo.TabIndex = 2;
        lblCantidadGastosTitulo.Text = "Gastos registrados";

        lblCantidadGastos.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblCantidadGastos.ForeColor = Color.FromArgb(22, 33, 62);
        lblCantidadGastos.Location = new Point(350, 60);
        lblCantidadGastos.Name = "lblCantidadGastos";
        lblCantidadGastos.Size = new Size(200, 35);
        lblCantidadGastos.TabIndex = 3;
        lblCantidadGastos.Text = "0";

        pnlAcciones.BackColor = Color.FromArgb(245, 247, 250);
        pnlAcciones.Controls.Add(btnRegistrarGasto);
        pnlAcciones.Controls.Add(btnVerGastos);
        pnlAcciones.Controls.Add(btnResumen);
        pnlAcciones.Controls.Add(btnAcercaDe);
        pnlAcciones.Controls.Add(btnSalir);
        pnlAcciones.Location = new Point(80, 330);
        pnlAcciones.Name = "pnlAcciones";
        pnlAcciones.Size = new Size(824, 190);
        pnlAcciones.TabIndex = 3;

        btnRegistrarGasto.BackColor = Color.FromArgb(58, 134, 255);
        btnRegistrarGasto.FlatAppearance.BorderSize = 0;
        btnRegistrarGasto.FlatStyle = FlatStyle.Flat;
        btnRegistrarGasto.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnRegistrarGasto.ForeColor = Color.White;
        btnRegistrarGasto.Location = new Point(15, 15);
        btnRegistrarGasto.Name = "btnRegistrarGasto";
        btnRegistrarGasto.Size = new Size(180, 55);
        btnRegistrarGasto.TabIndex = 0;
        btnRegistrarGasto.Text = "Registrar gasto";
        btnRegistrarGasto.UseVisualStyleBackColor = false;
        btnRegistrarGasto.Click += btnRegistrarGasto_Click;

        btnVerGastos.BackColor = Color.White;
        btnVerGastos.FlatAppearance.BorderColor = Color.FromArgb(58, 134, 255);
        btnVerGastos.FlatStyle = FlatStyle.Flat;
        btnVerGastos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnVerGastos.ForeColor = Color.FromArgb(15, 52, 96);
        btnVerGastos.Location = new Point(215, 15);
        btnVerGastos.Name = "btnVerGastos";
        btnVerGastos.Size = new Size(180, 55);
        btnVerGastos.TabIndex = 1;
        btnVerGastos.Text = "Ver gastos";
        btnVerGastos.UseVisualStyleBackColor = false;
        btnVerGastos.Click += btnVerGastos_Click;

        btnResumen.BackColor = Color.White;
        btnResumen.FlatAppearance.BorderColor = Color.FromArgb(58, 134, 255);
        btnResumen.FlatStyle = FlatStyle.Flat;
        btnResumen.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnResumen.ForeColor = Color.FromArgb(15, 52, 96);
        btnResumen.Location = new Point(415, 15);
        btnResumen.Name = "btnResumen";
        btnResumen.Size = new Size(180, 55);
        btnResumen.TabIndex = 2;
        btnResumen.Text = "Resumen";
        btnResumen.UseVisualStyleBackColor = false;
        btnResumen.Click += btnResumen_Click;

        btnAcercaDe.BackColor = Color.White;
        btnAcercaDe.FlatAppearance.BorderColor = Color.FromArgb(58, 134, 255);
        btnAcercaDe.FlatStyle = FlatStyle.Flat;
        btnAcercaDe.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnAcercaDe.ForeColor = Color.FromArgb(15, 52, 96);
        btnAcercaDe.Location = new Point(615, 15);
        btnAcercaDe.Name = "btnAcercaDe";
        btnAcercaDe.Size = new Size(180, 55);
        btnAcercaDe.TabIndex = 3;
        btnAcercaDe.Text = "Acerca de";
        btnAcercaDe.UseVisualStyleBackColor = false;
        btnAcercaDe.Click += btnAcercaDe_Click;

        btnSalir.BackColor = Color.FromArgb(22, 33, 62);
        btnSalir.FlatAppearance.BorderSize = 0;
        btnSalir.FlatStyle = FlatStyle.Flat;
        btnSalir.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnSalir.ForeColor = Color.White;
        btnSalir.Location = new Point(315, 100);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(180, 55);
        btnSalir.TabIndex = 4;
        btnSalir.Text = "Salir";
        btnSalir.UseVisualStyleBackColor = false;
        btnSalir.Click += btnSalir_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(pnlAcciones);
        Controls.Add(pnlContenido);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormInicio";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Inicio";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlContenido.ResumeLayout(false);
        pnlAcciones.ResumeLayout(false);
        ResumeLayout(false);
    }
}
