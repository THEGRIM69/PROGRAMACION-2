#nullable enable

namespace FolletoDigital;

partial class FormInicio
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblSubtitulo = null!;
    private Label lblDescripcion = null!;
    private Panel pnlVisual = null!;
    private Label lblVisualTitulo = null!;
    private Label lblVisualSubtitulo = null!;
    private Label lblPagina = null!;
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
        lblSubtitulo = new Label();
        lblDescripcion = new Label();
        pnlVisual = new Panel();
        lblVisualTitulo = new Label();
        lblVisualSubtitulo = new Label();
        lblPagina = new Label();
        btnSiguiente = new Button();
        pnlHeader.SuspendLayout();
        pnlVisual.SuspendLayout();
        SuspendLayout();
        //
        // pnlHeader
        //
        pnlHeader.BackColor = Color.FromArgb(22, 33, 62);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Controls.Add(lblSubtitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(984, 130);
        pnlHeader.TabIndex = 0;
        //
        // lblTitulo
        //
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(54, 24);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(373, 46);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "FOLLETO DIGITAL";
        //
        // lblSubtitulo
        //
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 13F);
        lblSubtitulo.ForeColor = Color.FromArgb(208, 222, 255);
        lblSubtitulo.Location = new Point(58, 78);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(306, 25);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Introducción a C# y Windows Forms";
        //
        // lblDescripcion
        //
        lblDescripcion.Font = new Font("Segoe UI", 15F);
        lblDescripcion.ForeColor = Color.FromArgb(37, 37, 37);
        lblDescripcion.Location = new Point(95, 207);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(390, 105);
        lblDescripcion.TabIndex = 1;
        lblDescripcion.Text = "Un recorrido sencillo por los conceptos,\r\ncaracterísticas y aplicaciones de C#.";
        lblDescripcion.TextAlign = ContentAlignment.MiddleLeft;
        //
        // pnlVisual
        //
        pnlVisual.BackColor = Color.White;
        pnlVisual.Controls.Add(lblVisualTitulo);
        pnlVisual.Controls.Add(lblVisualSubtitulo);
        pnlVisual.Location = new Point(545, 178);
        pnlVisual.Name = "pnlVisual";
        pnlVisual.Size = new Size(345, 270);
        pnlVisual.TabIndex = 2;
        //
        // lblVisualTitulo
        //
        lblVisualTitulo.Dock = DockStyle.Top;
        lblVisualTitulo.Font = new Font("Segoe UI", 54F, FontStyle.Bold);
        lblVisualTitulo.ForeColor = Color.FromArgb(58, 134, 255);
        lblVisualTitulo.Location = new Point(0, 0);
        lblVisualTitulo.Name = "lblVisualTitulo";
        lblVisualTitulo.Size = new Size(345, 155);
        lblVisualTitulo.TabIndex = 0;
        lblVisualTitulo.Text = "C#";
        lblVisualTitulo.TextAlign = ContentAlignment.BottomCenter;
        //
        // lblVisualSubtitulo
        //
        lblVisualSubtitulo.Dock = DockStyle.Bottom;
        lblVisualSubtitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblVisualSubtitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblVisualSubtitulo.Location = new Point(0, 180);
        lblVisualSubtitulo.Name = "lblVisualSubtitulo";
        lblVisualSubtitulo.Size = new Size(345, 90);
        lblVisualSubtitulo.TabIndex = 1;
        lblVisualSubtitulo.Text = "Ideas que se convierten\r\nen aplicaciones";
        lblVisualSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
        //
        // lblPagina
        //
        lblPagina.AutoSize = true;
        lblPagina.Font = new Font("Segoe UI", 10F);
        lblPagina.ForeColor = Color.FromArgb(95, 105, 120);
        lblPagina.Location = new Point(58, 558);
        lblPagina.Name = "lblPagina";
        lblPagina.Size = new Size(77, 19);
        lblPagina.TabIndex = 3;
        lblPagina.Text = "Página 1 de 5";
        //
        // btnSiguiente
        //
        btnSiguiente.BackColor = Color.FromArgb(58, 134, 255);
        btnSiguiente.FlatAppearance.BorderSize = 0;
        btnSiguiente.FlatStyle = FlatStyle.Flat;
        btnSiguiente.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnSiguiente.ForeColor = Color.White;
        btnSiguiente.Location = new Point(758, 535);
        btnSiguiente.Name = "btnSiguiente";
        btnSiguiente.Size = new Size(170, 48);
        btnSiguiente.TabIndex = 4;
        btnSiguiente.Text = "Comenzar  →";
        btnSiguiente.UseVisualStyleBackColor = false;
        btnSiguiente.Click += btnSiguiente_Click;
        //
        // FormInicio
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnSiguiente);
        Controls.Add(lblPagina);
        Controls.Add(pnlVisual);
        Controls.Add(lblDescripcion);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormInicio";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folleto Digital | Inicio";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlVisual.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
