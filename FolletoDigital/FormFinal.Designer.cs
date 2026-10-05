#nullable enable

namespace FolletoDigital;

partial class FormFinal
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblContenido = null!;
    private Label lblGracias = null!;
    private Label lblPagina = null!;
    private Button btnAnterior = null!;
    private Button btnInicio = null!;
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
        lblContenido = new Label();
        lblGracias = new Label();
        lblPagina = new Label();
        btnAnterior = new Button();
        btnInicio = new Button();
        btnSalir = new Button();
        pnlHeader.SuspendLayout();
        SuspendLayout();
        //
        // pnlHeader
        //
        pnlHeader.BackColor = Color.FromArgb(22, 33, 62);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(984, 112);
        pnlHeader.TabIndex = 0;
        //
        // lblTitulo
        //
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 27F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(55, 31);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(222, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Conclusión";
        //
        // lblContenido
        //
        lblContenido.BackColor = Color.White;
        lblContenido.Font = new Font("Segoe UI", 14F);
        lblContenido.ForeColor = Color.FromArgb(37, 37, 37);
        lblContenido.Location = new Point(83, 162);
        lblContenido.Name = "lblContenido";
        lblContenido.Padding = new Padding(28);
        lblContenido.Size = new Size(818, 191);
        lblContenido.TabIndex = 1;
        lblContenido.Text = "C# es uno de los lenguajes más utilizados dentro\r\ndel ecosistema .NET y permite desarrollar diferentes\r\ntipos de aplicaciones utilizando una sintaxis clara\r\ny herramientas modernas.";
        lblContenido.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblGracias
        //
        lblGracias.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblGracias.ForeColor = Color.FromArgb(15, 52, 96);
        lblGracias.Location = new Point(83, 383);
        lblGracias.Name = "lblGracias";
        lblGracias.Size = new Size(818, 58);
        lblGracias.TabIndex = 2;
        lblGracias.Text = "¡Gracias por visitar el folleto!";
        lblGracias.TextAlign = ContentAlignment.MiddleCenter;
        //
        // lblPagina
        //
        lblPagina.AutoSize = true;
        lblPagina.Font = new Font("Segoe UI", 10F);
        lblPagina.ForeColor = Color.FromArgb(95, 105, 120);
        lblPagina.Location = new Point(55, 558);
        lblPagina.Name = "lblPagina";
        lblPagina.Size = new Size(77, 19);
        lblPagina.TabIndex = 3;
        lblPagina.Text = "Página 5 de 5";
        //
        // btnAnterior
        //
        btnAnterior.BackColor = Color.White;
        btnAnterior.FlatAppearance.BorderColor = Color.FromArgb(58, 134, 255);
        btnAnterior.FlatStyle = FlatStyle.Flat;
        btnAnterior.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAnterior.ForeColor = Color.FromArgb(15, 52, 96);
        btnAnterior.Location = new Point(465, 535);
        btnAnterior.Name = "btnAnterior";
        btnAnterior.Size = new Size(125, 45);
        btnAnterior.TabIndex = 4;
        btnAnterior.Text = "← Anterior";
        btnAnterior.UseVisualStyleBackColor = false;
        btnAnterior.Click += btnAnterior_Click;
        //
        // btnInicio
        //
        btnInicio.BackColor = Color.FromArgb(58, 134, 255);
        btnInicio.FlatAppearance.BorderSize = 0;
        btnInicio.FlatStyle = FlatStyle.Flat;
        btnInicio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnInicio.ForeColor = Color.White;
        btnInicio.Location = new Point(605, 535);
        btnInicio.Name = "btnInicio";
        btnInicio.Size = new Size(165, 45);
        btnInicio.TabIndex = 5;
        btnInicio.Text = "Volver al inicio";
        btnInicio.UseVisualStyleBackColor = false;
        btnInicio.Click += btnInicio_Click;
        //
        // btnSalir
        //
        btnSalir.BackColor = Color.FromArgb(22, 33, 62);
        btnSalir.FlatAppearance.BorderSize = 0;
        btnSalir.FlatStyle = FlatStyle.Flat;
        btnSalir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSalir.ForeColor = Color.White;
        btnSalir.Location = new Point(785, 535);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(116, 45);
        btnSalir.TabIndex = 6;
        btnSalir.Text = "Salir";
        btnSalir.UseVisualStyleBackColor = false;
        btnSalir.Click += btnSalir_Click;
        //
        // FormFinal
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnSalir);
        Controls.Add(btnInicio);
        Controls.Add(btnAnterior);
        Controls.Add(lblPagina);
        Controls.Add(lblGracias);
        Controls.Add(lblContenido);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormFinal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folleto Digital | Conclusión";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
