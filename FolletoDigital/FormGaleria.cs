namespace FolletoDigital;

public partial class FormGaleria : Form
{
    public FormGaleria()
    {
        InitializeComponent();
        ActualizarResumen();
    }

    internal void ActualizarResumen()
    {
        var gastos = GastosRepositorio.ObtenerTodos();
        var totalGastado = GastosRepositorio.ObtenerTotal();

        lblTotalGastado.Text = totalGastado.ToString("C2");
        lblCantidadGastos.Text = GastosRepositorio.ObtenerCantidad().ToString();

        var gastoMayor = GastosRepositorio.ObtenerMayorMonto();
        lblGastoMayor.Text = gastoMayor is null ? "$0.00" : gastoMayor.Monto.ToString("C2");

        lblCategoriaPredominante.Text = GastosRepositorio.ObtenerCategoriaPredominante();

        lstResumen.Items.Clear();
        foreach (var categoria in GastosRepositorio.ObtenerTotalesPorCategoria().OrderBy(item => item.Key))
        {
            lstResumen.Items.Add($"{categoria.Key}: {categoria.Value:C2}");
        }

        if (gastos.Count == 0)
        {
            lstResumen.Items.Add("No hay gastos registrados.");
        }
    }
}
