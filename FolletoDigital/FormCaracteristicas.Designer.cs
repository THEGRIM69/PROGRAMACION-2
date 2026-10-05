#nullable enable

namespace FolletoDigital;

partial class FormCaracteristicas
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Panel pnlFacil = null!;
    private Panel pnlObjetos = null!;
    private Panel pnlNet = null!;
    private Panel pnlMultiplataforma = null!;
    private Label lblFacilTitulo = null!;
    private Label lblFacilTexto = null!;
    private Label lblObjetosTitulo = null!;
    private Label lblObjetosTexto = null!;
    private Label lblNetTitulo = null!;
    private Label lblNetTexto = null!;
    private Label lblMultiplataformaTitulo = null!;
    private Label lblMultiplataformaTexto = null!;
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
        pnlFacil = new Panel();
        pnlObjetos = new Panel();
        pnlNet = new Panel();
        pnlMultiplataforma = new Panel();
        lblFacilTitulo = new Label();
        lblFacilTexto = new Label();
        lblObjetosTitulo = new Label();
        lblObjetosTexto = new Label();
        lblNetTitulo = new Label();
        lblNetTexto = new Label();
        lblMultiplataformaTitulo = new Label();
        lblMultiplataformaTexto = new Label();
        lblPagina = new Label();
        btnAnterior = new Button();
        btnSiguiente = new Button();
        pnlHeader.SuspendLayout();
        pnlFacil.SuspendLayout();
        pnlObjetos.SuspendLayout();
        pnlNet.SuspendLayout();
        pnlMultiplataforma.SuspendLayout();
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
        lblTitulo.Size = new Size(488, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Características de C#";
        //
        // Tarjetas
        //
        ConfigurarTarjeta(pnlFacil, lblFacilTitulo, lblFacilTexto, 83, 150,
            "Fácil de aprender", "Posee una sintaxis organizada y clara.");
        ConfigurarTarjeta(pnlObjetos, lblObjetosTitulo, lblObjetosTexto, 509, 150,
            "Orientado a objetos", "Permite organizar programas mediante clases y objetos.");
        ConfigurarTarjeta(pnlNet, lblNetTitulo, lblNetTexto, 83, 325,
            "Integración con .NET", "Puede aprovechar las herramientas y bibliotecas de .NET.");
        ConfigurarTarjeta(pnlMultiplataforma, lblMultiplataformaTitulo, lblMultiplataformaTexto, 509, 325,
            "Multiplataforma", "Las aplicaciones .NET modernas pueden desarrollarse para diferentes plataformas.");
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
        lblPagina.Text = "Página 3 de 5";
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
        // FormCaracteristicas
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnSiguiente);
        Controls.Add(btnAnterior);
        Controls.Add(lblPagina);
        Controls.Add(pnlMultiplataforma);
        Controls.Add(pnlNet);
        Controls.Add(pnlObjetos);
        Controls.Add(pnlFacil);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormCaracteristicas";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folleto Digital | Características";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlFacil.ResumeLayout(false);
        pnlObjetos.ResumeLayout(false);
        pnlNet.ResumeLayout(false);
        pnlMultiplataforma.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private static void ConfigurarTarjeta(Panel panel, Label titulo, Label texto, int x, int y, string encabezado, string descripcion)
    {
        panel.BackColor = Color.White;
        panel.Controls.Add(texto);
        panel.Controls.Add(titulo);
        panel.Location = new Point(x, y);
        panel.Name = "pnl" + encabezado.Replace(" ", string.Empty).Replace(".", string.Empty);
        panel.Size = new Size(392, 145);
        panel.TabIndex = 1;

        titulo.AutoSize = true;
        titulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        titulo.ForeColor = Color.FromArgb(15, 52, 96);
        titulo.Location = new Point(22, 20);
        titulo.Name = "lbl" + encabezado.Replace(" ", string.Empty).Replace(".", string.Empty) + "Titulo";
        titulo.Size = new Size(100, 28);
        titulo.TabIndex = 0;
        titulo.Text = encabezado;

        texto.Font = new Font("Segoe UI", 11F);
        texto.ForeColor = Color.FromArgb(37, 37, 37);
        texto.Location = new Point(22, 61);
        texto.Name = "lbl" + encabezado.Replace(" ", string.Empty).Replace(".", string.Empty) + "Texto";
        texto.Size = new Size(345, 65);
        texto.TabIndex = 1;
        texto.Text = descripcion;
    }
}
