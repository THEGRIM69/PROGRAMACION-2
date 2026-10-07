namespace FolletoDigital;

public partial class FormInicio : Form
{
    public FormInicio()
    {
        InitializeComponent();
        Navegacion.RegistrarInicio(this);
        ActualizarDashboard();
    }

    internal void ActualizarDashboard()
    {
        lblTotalGastado.Text = GastosRepositorio.ObtenerTotal().ToString("C2");
        lblCantidadGastos.Text = GastosRepositorio.ObtenerCantidad().ToString();

        var gastoMayor = GastosRepositorio.ObtenerMayorMonto();
        lblGastoMayor.Text = gastoMayor is null ? "$0.00" : gastoMayor.Monto.ToString("C2");
        lblCategoriaTop.Text = GastosRepositorio.ObtenerCategoriaPredominante();

        dgvUltimosGastos.Rows.Clear();
        foreach (var gasto in GastosRepositorio.ObtenerTodos().Reverse().Take(5))
        {
            dgvUltimosGastos.Rows.Add(
                gasto.Fecha.ToShortDateString(),
                gasto.Descripcion,
                gasto.Categoria,
                gasto.Monto.ToString("C2"));
        }
    }
}
