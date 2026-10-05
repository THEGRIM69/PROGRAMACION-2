#nullable enable

namespace FolletoDigital;

partial class FormInformacion
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblContenido = null!;
    private Panel pnlModerno = null!;
    private Panel pnlObjetos = null!;
    private Panel pnlVersatil = null!;
    private Label lblModerno = null!;
    private Label lblObjetos = null!;
    private Label lblVersatil = null!;
    private Label lblPagina = null!;
    private Button btnAnterior = null!;
    private Button btnSiguiente = null!;

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
        pnlModerno = new Panel();
        pnlObjetos = new Panel();
        pnlVersatil = new Panel();
        lblModerno = new Label();
        lblObjetos = new Label();
        lblVersatil = new Label();
        lblPagina = new Label();
        btnAnterior = new Button();
        btnSiguiente = new Button();
        pnlHeader.SuspendLayout();
        pnlModerno.SuspendLayout();
        pnlObjetos.SuspendLayout();
        pnlVersatil.SuspendLayout();
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
        lblTitulo.Size = new Size(270, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "¿Qué es C#?";
        //
        // lblContenido
        //
        lblContenido.BackColor = Color.White;
        lblContenido.Font = new Font("Segoe UI", 14F);
        lblContenido.ForeColor = Color.FromArgb(37, 37, 37);
        lblContenido.Location = new Point(83, 153);
        lblContenido.Name = "lblContenido";
        lblContenido.Padding = new Padding(28);
        lblContenido.Size = new Size(818, 225);
        lblContenido.TabIndex = 1;
        lblContenido.Text = "C# es un lenguaje de programación desarrollado\r\npara crear aplicaciones modernas, seguras y\r\norientadas a objetos.\r\n\r\nSe utiliza para desarrollar aplicaciones de\r\nescritorio, aplicaciones web, videojuegos,\r\nservicios y muchos otros tipos de software.";
        lblContenido.TextAlign = ContentAlignment.MiddleLeft;
        //
        // pnlModerno
        //
        pnlModerno.BackColor = Color.White;
        pnlModerno.Controls.Add(lblModerno);
        pnlModerno.Location = new Point(83, 410);
        pnlModerno.Name = "pnlModerno";
        pnlModerno.Size = new Size(250, 75);
        pnlModerno.TabIndex = 2;
        //
        // lblModerno
        //
        lblModerno.Dock = DockStyle.Fill;
        lblModerno.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblModerno.ForeColor = Color.FromArgb(15, 52, 96);
        lblModerno.Name = "lblModerno";
        lblModerno.Text = "Moderno";
        lblModerno.TextAlign = ContentAlignment.MiddleCenter;
        //
        // pnlObjetos
        //
        pnlObjetos.BackColor = Color.White;
        pnlObjetos.Controls.Add(lblObjetos);
        pnlObjetos.Location = new Point(367, 410);
        pnlObjetos.Name = "pnlObjetos";
        pnlObjetos.Size = new Size(250, 75);
        pnlObjetos.TabIndex = 3;
        //
        // lblObjetos
        //
        lblObjetos.Dock = DockStyle.Fill;
        lblObjetos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblObjetos.ForeColor = Color.FromArgb(15, 52, 96);
        lblObjetos.Name = "lblObjetos";
        lblObjetos.Text = "Orientado a objetos";
        lblObjetos.TextAlign = ContentAlignment.MiddleCenter;
        //
        // pnlVersatil
        //
        pnlVersatil.BackColor = Color.White;
        pnlVersatil.Controls.Add(lblVersatil);
        pnlVersatil.Location = new Point(651, 410);
        pnlVersatil.Name = "pnlVersatil";
        pnlVersatil.Size = new Size(250, 75);
        pnlVersatil.TabIndex = 4;
        //
        // lblVersatil
        //
        lblVersatil.Dock = DockStyle.Fill;
        lblVersatil.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblVersatil.ForeColor = Color.FromArgb(15, 52, 96);
        lblVersatil.Name = "lblVersatil";
        lblVersatil.Text = "Versátil";
        lblVersatil.TextAlign = ContentAlignment.MiddleCenter;
        //
        // lblPagina
        //
        lblPagina.AutoSize = true;
        lblPagina.Font = new Font("Segoe UI", 10F);
        lblPagina.ForeColor = Color.FromArgb(95, 105, 120);
        lblPagina.Location = new Point(55, 558);
        lblPagina.Name = "lblPagina";
        lblPagina.Size = new Size(77, 19);
        lblPagina.TabIndex = 5;
        lblPagina.Text = "Página 2 de 5";
        //
        // btnAnterior
        //
        btnAnterior.BackColor = Color.White;
        btnAnterior.FlatAppearance.BorderColor = Color.FromArgb(58, 134, 255);
        btnAnterior.FlatStyle = FlatStyle.Flat;
        btnAnterior.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAnterior.ForeColor = Color.FromArgb(15, 52, 96);
        btnAnterior.Location = new Point(665, 535);
        btnAnterior.Name = "btnAnterior";
        btnAnterior.Size = new Size(125, 45);
        btnAnterior.TabIndex = 6;
        btnAnterior.Text = "← Anterior";
        btnAnterior.UseVisualStyleBackColor = false;
        btnAnterior.Click += btnAnterior_Click;
        //
        // btnSiguiente
        //
        btnSiguiente.BackColor = Color.FromArgb(58, 134, 255);
        btnSiguiente.FlatAppearance.BorderSize = 0;
        btnSiguiente.FlatStyle = FlatStyle.Flat;
        btnSiguiente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSiguiente.ForeColor = Color.White;
        btnSiguiente.Location = new Point(805, 535);
        btnSiguiente.Name = "btnSiguiente";
        btnSiguiente.Size = new Size(123, 45);
        btnSiguiente.TabIndex = 7;
        btnSiguiente.Text = "Siguiente →";
        btnSiguiente.UseVisualStyleBackColor = false;
        btnSiguiente.Click += btnSiguiente_Click;
        //
        // FormInformacion
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnSiguiente);
        Controls.Add(btnAnterior);
        Controls.Add(lblPagina);
        Controls.Add(pnlVersatil);
        Controls.Add(pnlObjetos);
        Controls.Add(pnlModerno);
        Controls.Add(lblContenido);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormInformacion";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folleto Digital | ¿Qué es C#?";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlModerno.ResumeLayout(false);
        pnlObjetos.ResumeLayout(false);
        pnlVersatil.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
