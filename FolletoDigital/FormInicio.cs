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
    }
}
