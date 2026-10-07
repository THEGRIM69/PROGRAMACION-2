namespace FolletoDigital;

public partial class FormCaracteristicas : Form
{
    public FormCaracteristicas()
    {
        InitializeComponent();
        Shown += FormCaracteristicas_Shown;
        CargarGastos();
    }

    private void FormCaracteristicas_Shown(object? sender, EventArgs e)
    {
        CargarGastos();
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        if (dgvGastos.CurrentRow is null)
        {
            MessageBox.Show("Seleccione un gasto para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var gasto = GastosRepositorio.ObtenerTodos()[dgvGastos.CurrentRow.Index];
        var resultado = MessageBox.Show(
            $"¿Desea eliminar el gasto de {gasto.Descripcion}?",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (resultado != DialogResult.Yes)
        {
            return;
        }

        GastosRepositorio.Eliminar(dgvGastos.CurrentRow.Index);
        CargarGastos();
    }

    private void btnVolver_Click(object? sender, EventArgs e)
    {
        Navegacion.VolverAlInicio(this);
    }

    private void CargarGastos()
    {
        dgvGastos.Rows.Clear();

        foreach (var gasto in GastosRepositorio.ObtenerTodos())
        {
            dgvGastos.Rows.Add(gasto.Fecha.ToShortDateString(), gasto.Descripcion, gasto.Categoria, gasto.Monto.ToString("C2"));
        }

        lblCantidad.Text = GastosRepositorio.ObtenerCantidad().ToString();
    }
}
