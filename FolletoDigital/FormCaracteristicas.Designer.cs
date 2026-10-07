#nullable enable

namespace FolletoDigital;

partial class FormCaracteristicas
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private Label lblCantidadTitulo = null!;
    private Label lblCantidad = null!;
    private DataGridView dgvGastos = null!;
    private Button btnEliminar = null!;

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
        lblCantidadTitulo = new Label();
        lblCantidad = new Label();
        dgvGastos = new DataGridView();
        btnEliminar = new Button();
        pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGastos).BeginInit();
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
        lblTitulo.Size = new Size(260, 48);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Historial de gastos";

        lblCantidadTitulo.AutoSize = true;
        lblCantidadTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCantidadTitulo.ForeColor = Color.FromArgb(15, 52, 96);
        lblCantidadTitulo.Location = new Point(55, 140);
        lblCantidadTitulo.Name = "lblCantidadTitulo";
        lblCantidadTitulo.Size = new Size(174, 20);
        lblCantidadTitulo.TabIndex = 1;
        lblCantidadTitulo.Text = "Gastos registrados:";

        lblCantidad.AutoSize = true;
        lblCantidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCantidad.ForeColor = Color.FromArgb(58, 134, 255);
        lblCantidad.Location = new Point(235, 140);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(17, 20);
        lblCantidad.TabIndex = 2;
        lblCantidad.Text = "0";

        dgvGastos.AllowUserToAddRows = false;
        dgvGastos.AllowUserToDeleteRows = false;
        dgvGastos.AllowUserToResizeRows = false;
        dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGastos.BackgroundColor = Color.White;
        dgvGastos.BorderStyle = BorderStyle.Fixed3D;
        dgvGastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvGastos.Location = new Point(55, 180);
        dgvGastos.MultiSelect = false;
        dgvGastos.Name = "dgvGastos";
        dgvGastos.ReadOnly = true;
        dgvGastos.RowHeadersVisible = false;
        dgvGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGastos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvGastos.Size = new Size(874, 310);
        dgvGastos.TabIndex = 3;
        dgvGastos.Columns.Add("Fecha", "Fecha");
        dgvGastos.Columns.Add("Descripcion", "Descripción");
        dgvGastos.Columns.Add("Categoria", "Categoría");
        dgvGastos.Columns.Add("Monto", "Monto");
        foreach (DataGridViewColumn columna in dgvGastos.Columns)
        {
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        btnEliminar.BackColor = Color.FromArgb(218, 83, 73);
        btnEliminar.FlatAppearance.BorderSize = 0;
        btnEliminar.FlatStyle = FlatStyle.Flat;
        btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnEliminar.ForeColor = Color.White;
        btnEliminar.Location = new Point(55, 520);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(180, 45);
        btnEliminar.TabIndex = 4;
        btnEliminar.Text = "Eliminar seleccionado";
        btnEliminar.UseVisualStyleBackColor = false;
        btnEliminar.Click += btnEliminar_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(984, 611);
        Controls.Add(btnEliminar);
        Controls.Add(dgvGastos);
        Controls.Add(lblCantidad);
        Controls.Add(lblCantidadTitulo);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormCaracteristicas";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Gastos Personales | Historial";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGastos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
