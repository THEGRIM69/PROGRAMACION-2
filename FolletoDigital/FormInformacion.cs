namespace FolletoDigital;

public partial class FormInformacion : Form
{
    public FormInformacion()
    {
        InitializeComponent();
        dtpFecha.Value = DateTime.Today;
        CargarCategorias();
        cmbCategoria.SelectedIndex = -1;
    }

    private void btnGuardarGasto_Click(object? sender, EventArgs e)
    {
        var descripcion = txtDescripcion.Text.Trim();
        var categoria = cmbCategoria.SelectedItem as string;
        var monto = nudMonto.Value;

        if (string.IsNullOrWhiteSpace(descripcion))
        {
            MessageBox.Show("Ingrese una descripción para el gasto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtDescripcion.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(categoria))
        {
            MessageBox.Show("Seleccione una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cmbCategoria.Focus();
            return;
        }

        if (monto <= 0)
        {
            MessageBox.Show("El monto debe ser mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudMonto.Focus();
            return;
        }

        var gasto = new Gasto
        {
            Fecha = dtpFecha.Value.Date,
            Descripcion = descripcion,
            Categoria = categoria,
            Monto = monto
        };

        GastosRepositorio.Agregar(gasto);
        MessageBox.Show("El gasto fue registrado correctamente.", "Gasto guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LimpiarFormulario();

        if (Owner is FormInicio inicio)
        {
            inicio.ActualizarDashboard();
        }
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void btnVolver_Click(object? sender, EventArgs e)
    {
        Navegacion.VolverAlInicio(this);
    }

    private void LimpiarFormulario()
    {
        txtDescripcion.Clear();
        cmbCategoria.SelectedIndex = -1;
        nudMonto.Value = 0;
        dtpFecha.Value = DateTime.Today;
        txtDescripcion.Focus();
    }

    private void CargarCategorias()
    {
        cmbCategoria.Items.AddRange(new[]
        {
            "Alimentación",
            "Transporte",
            "Estudios",
            "Entretenimiento",
            "Servicios",
            "Otros"
        });
    }
}
