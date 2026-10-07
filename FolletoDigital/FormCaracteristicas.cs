namespace FolletoDigital;

public partial class FormCaracteristicas : Form
{
    public FormCaracteristicas()
    {
        InitializeComponent();
        CargarGastos();
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        if (dgvGastos.CurrentRow is null)
        {
            MessageBox.Show("Seleccione un gasto para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var indice = dgvGastos.CurrentRow.Index;
        var gasto = GastosRepositorio.ObtenerTodos()[indice];
        var resultado = MessageBox.Show(
            $"¿Desea eliminar el gasto de {gasto.Descripcion}?",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (resultado != DialogResult.Yes)
        {
            return;
        }

        GastosRepositorio.Eliminar(indice);
        CargarGastos();
    }

    internal void CargarGastos()
    {
        dgvGastos.Rows.Clear();

        foreach (var gasto in GastosRepositorio.ObtenerTodos())
        {
            dgvGastos.Rows.Add(gasto.Fecha.ToShortDateString(), gasto.Descripcion, gasto.Categoria, gasto.Monto.ToString("C2"));
        }

        lblCantidad.Text = GastosRepositorio.ObtenerCantidad().ToString();
    }
}
