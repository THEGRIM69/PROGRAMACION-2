#nullable enable

namespace FolletoDigital;

partial class FormGaleria
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Panel pnlEscritorio = null!;
    private Panel pnlWeb = null!;
    private Panel pnlVideojuegos = null!;
    private Panel pnlServicios = null!;
    private Label lblEscritorioIcono = null!;
    private Label lblEscritorioTitulo = null!;
    private Label lblEscritorioTexto = null!;
    private Label lblWebIcono = null!;
    private Label lblWebTitulo = null!;
    private Label lblWebTexto = null!;
    private Label lblVideojuegosIcono = null!;
    private Label lblVideojuegosTitulo = null!;
    private Label lblVideojuegosTexto = null!;
    private Label lblServiciosIcono = null!;
    private Label lblServiciosTitulo = null!;
    private Label lblServiciosTexto = null!;
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
        pnlEscritorio = new Panel();
        pnlWeb = new Panel();
        pnlVideojuegos = new Panel();
        pnlServicios = new Panel();
        lblEscritorioIcono = new Label();
        lblEscritorioTitulo = new Label();
        lblEscritorioTexto = new Label();
        lblWebIcono = new Label();
        lblWebTitulo = new Label();
        lblWebTexto = new Label();
        lblVideojuegosIcono = new Label();
        lblVideojuegosTitulo = new Label();
        lblVideojuegosTexto = new Label();
        lblServiciosIcono = new Label();
        lblServiciosTitulo = new Label();
        lblServiciosTexto = new Label();
        lblPagina = new Label();
        btnAnterior = new Button();
        btnSiguiente = new Button();
        pnlHeader.SuspendLayout();
        pnlEscritorio.SuspendLayout();
        pnlWeb.SuspendLayout();
        pnlVideojuegos.SuspendLayout();
        pnlServicios.SuspendLayout();
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
        lblTitulo.Size = new Size(638, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "¿Qué podemos crear con C#?";
        //
        // Bloques visuales
        //
        ConfigurarBloque(pnlEscritorio, lblEscritorioIcono, lblEscritorioTitulo, lblEscritorioTexto,
            83, 150, "APP", "Aplicaciones de escritorio", "Windows Forms y WPF");
        ConfigurarBloque(pnlWeb, lblWebIcono, lblWebTitulo, lblWebTexto,
            509, 150, "WEB", "Aplicaciones web", "ASP.NET");
        ConfigurarBloque(pnlVideojuegos, lblVideojuegosIcono, lblVideojuegosTitulo, lblVideojuegosTexto,
            83, 325, "PLAY", "Videojuegos", "Unity");
        ConfigurarBloque(pnlServicios, lblServiciosIcono, lblServiciosTitulo, lblServiciosTexto,
            509, 325, "API", "Servicios y APIs", ".NET");
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
        lblPagina.Text = "Página 4 de 5";
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
        // FormGaleria
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnSiguiente);
        Controls.Add(btnAnterior);
        Controls.Add(lblPagina);
        Controls.Add(pnlServicios);
        Controls.Add(pnlVideojuegos);
        Controls.Add(pnlWeb);
        Controls.Add(pnlEscritorio);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormGaleria";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folleto Digital | Aplicaciones";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlEscritorio.ResumeLayout(false);
        pnlWeb.ResumeLayout(false);
        pnlVideojuegos.ResumeLayout(false);
        pnlServicios.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private static void ConfigurarBloque(Panel panel, Label icono, Label titulo, Label texto,
        int x, int y, string etiqueta, string encabezado, string descripcion)
    {
        panel.BackColor = Color.White;
        panel.Controls.Add(texto);
        panel.Controls.Add(titulo);
        panel.Controls.Add(icono);
        panel.Location = new Point(x, y);
        panel.Name = "pnl" + encabezado.Replace(" ", string.Empty);
        panel.Size = new Size(392, 145);
        panel.TabIndex = 1;

        icono.BackColor = Color.FromArgb(232, 240, 255);
        icono.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        icono.ForeColor = Color.FromArgb(58, 134, 255);
        icono.Location = new Point(20, 20);
        icono.Name = "lbl" + etiqueta;
        icono.Size = new Size(72, 36);
        icono.TabIndex = 0;
        icono.Text = etiqueta;
        icono.TextAlign = ContentAlignment.MiddleCenter;

        titulo.AutoSize = true;
        titulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        titulo.ForeColor = Color.FromArgb(15, 52, 96);
        titulo.Location = new Point(111, 22);
        titulo.Name = "lbl" + encabezado.Replace(" ", string.Empty) + "Titulo";
        titulo.Size = new Size(100, 25);
        titulo.TabIndex = 1;
        titulo.Text = encabezado;

        texto.AutoSize = true;
        texto.Font = new Font("Segoe UI", 11F);
        texto.ForeColor = Color.FromArgb(95, 105, 120);
        texto.Location = new Point(111, 65);
        texto.Name = "lbl" + encabezado.Replace(" ", string.Empty) + "Texto";
        texto.Size = new Size(100, 20);
        texto.TabIndex = 2;
        texto.Text = descripcion;
    }
}
