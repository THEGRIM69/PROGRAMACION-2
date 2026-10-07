namespace FolletoDigital;

public partial class FormInicio : Form
{
    public FormInicio()
    {
        InitializeComponent();
        Navegacion.RegistrarInicio(this);
        ActualizarDashboard();
    }

    private void btnRegistrarGasto_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormInformacion>(this);
    }

    private void btnVerGastos_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormCaracteristicas>(this);
    }

    private void btnResumen_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormGaleria>(this);
    }

    private void btnAcercaDe_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormFinal>(this);
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Navegacion.Salir();
    }

    internal void ActualizarDashboard()
    {
        lblTotalGastado.Text = GastosRepositorio.ObtenerTotal().ToString("C2");
        lblCantidadGastos.Text = GastosRepositorio.ObtenerCantidad().ToString();
    }
}
