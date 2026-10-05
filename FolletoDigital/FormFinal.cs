namespace FolletoDigital;

public partial class FormFinal : Form
{
    public FormFinal()
    {
        InitializeComponent();
    }

    private void btnAnterior_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormGaleria>(this);
    }

    private void btnInicio_Click(object? sender, EventArgs e)
    {
        Navegacion.VolverAlInicio(this);
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Navegacion.Salir();
    }
}
