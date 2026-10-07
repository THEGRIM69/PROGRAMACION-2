namespace FolletoDigital;

public partial class FormFinal : Form
{
    public FormFinal()
    {
        InitializeComponent();
    }

    private void btnVolver_Click(object? sender, EventArgs e)
    {
        Navegacion.VolverAlInicio(this);
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Navegacion.Salir();
    }
}
