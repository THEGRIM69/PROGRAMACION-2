#nullable enable

namespace FolletoDigital;

partial class FormInformacion
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private Label lblCategoria = null!;
    private Label lblMonto = null!;
    private Label lblFecha = null!;
    private TextBox txtDescripcion = null!;
    private ComboBox cmbCategoria = null!;
    private NumericUpDown nudMonto = null!;
    private DateTimePicker dtpFecha = null!;
    private Button btnGuardarGasto = null!;
    private Button btnLimpiar = null!;
    private Button btnVolver = null!;

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
        lblCategoria = new Label();
        lblMonto = new Label();
        lblFecha = new Label();
        txtDescripcion = new TextBox();
        cmbCategoria = new ComboBox();
        nudMonto = new NumericUpDown();
        dtpFecha = new DateTimePicker();
        btnGuardarGasto = new Button();
        btnLimpiar = new Button();
        btnVolver = new Button();
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
        lblTitulo.Size = new Size(263, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Registrar gasto";

        lblDescripcion.AutoSize = true;
        lblDescripcion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDescripcion.ForeColor = Color.FromArgb(15, 52, 96);
        lblDescripcion.Location = new Point(83, 160);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(95, 20);
        lblDescripcion.TabIndex = 1;
        lblDescripcion.Text = "Descripción";

        txtDescripcion.Font = new Font("Segoe UI", 11F);
        txtDescripcion.Location = new Point(83, 190);
        txtDescripcion.Name = "txtDescripcion";
        txtDescripcion.Size = new Size(820, 27);
        txtDescripcion.TabIndex = 0;

        lblCategoria.AutoSize = true;
        lblCategoria.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCategoria.ForeColor = Color.FromArgb(15, 52, 96);
        lblCategoria.Location = new Point(83, 245);
        lblCategoria.Name = "lblCategoria";
        lblCategoria.Size = new Size(80, 20);
        lblCategoria.TabIndex = 2;
        lblCategoria.Text = "Categoría";

        cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCategoria.Font = new Font("Segoe UI", 11F);
        cmbCategoria.Location = new Point(83, 275);
        cmbCategoria.Name = "cmbCategoria";
        cmbCategoria.Size = new Size(300, 28);
        cmbCategoria.TabIndex = 1;

        lblMonto.AutoSize = true;
        lblMonto.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblMonto.ForeColor = Color.FromArgb(15, 52, 96);
        lblMonto.Location = new Point(426, 245);
        lblMonto.Name = "lblMonto";
        lblMonto.Size = new Size(56, 20);
        lblMonto.TabIndex = 3;
        lblMonto.Text = "Monto";

        nudMonto.DecimalPlaces = 2;
        nudMonto.Font = new Font("Segoe UI", 11F);
        nudMonto.Location = new Point(426, 275);
        nudMonto.Maximum = 999999999;
        nudMonto.Name = "nudMonto";
        nudMonto.Size = new Size(200, 27);
        nudMonto.TabIndex = 2;
        nudMonto.ThousandsSeparator = true;

        lblFecha.AutoSize = true;
        lblFecha.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblFecha.ForeColor = Color.FromArgb(15, 52, 96);
        lblFecha.Location = new Point(666, 245);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(49, 20);
        lblFecha.TabIndex = 4;
        lblFecha.Text = "Fecha";

        dtpFecha.Font = new Font("Segoe UI", 11F);
        dtpFecha.Location = new Point(666, 275);
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Size = new Size(237, 27);
        dtpFecha.TabIndex = 3;

        btnGuardarGasto.BackColor = Color.FromArgb(58, 134, 255);
        btnGuardarGasto.FlatAppearance.BorderSize = 0;
        btnGuardarGasto.FlatStyle = FlatStyle.Flat;
        btnGuardarGasto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnGuardarGasto.ForeColor = Color.White;
        btnGuardarGasto.Location = new Point(83, 370);
        btnGuardarGasto.Name = "btnGuardarGasto";
        btnGuardarGasto.Size = new Size(180, 45);
        btnGuardarGasto.TabIndex = 4;
        btnGuardarGasto.Text = "Guardar gasto";
        btnGuardarGasto.UseVisualStyleBackColor = false;
        btnGuardarGasto.Click += btnGuardarGasto_Click;

        btnLimpiar.BackColor = Color.White;
        btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(58, 134, 255);
        btnLimpiar.FlatStyle = FlatStyle.Flat;
        btnLimpiar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLimpiar.ForeColor = Color.FromArgb(15, 52, 96);
        btnLimpiar.Location = new Point(283, 370);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Size = new Size(180, 45);
        btnLimpiar.TabIndex = 5;
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.UseVisualStyleBackColor = false;
        btnLimpiar.Click += btnLimpiar_Click;

        btnVolver.BackColor = Color.FromArgb(22, 33, 62);
        btnVolver.FlatAppearance.BorderSize = 0;
        btnVolver.FlatStyle = FlatStyle.Flat;
        btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnVolver.ForeColor = Color.White;
        btnVolver.Location = new Point(723, 370);
        btnVolver.Name = "btnVolver";
        btnVolver.Size = new Size(180, 45);
        btnVolver.TabIndex = 6;
        btnVolver.Text = "Volver al inicio";
        btnVolver.UseVisualStyleBackColor = false;
        btnVolver.Click += btnVolver_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnVolver);
        Controls.Add(btnLimpiar);
        Controls.Add(btnGuardarGasto);
        Controls.Add(dtpFecha);
        Controls.Add(nudMonto);
        Controls.Add(cmbCategoria);
        Controls.Add(txtDescripcion);
        Controls.Add(lblFecha);
        Controls.Add(lblMonto);
        Controls.Add(lblCategoria);
        Controls.Add(lblDescripcion);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormInformacion";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Registrar gasto";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
