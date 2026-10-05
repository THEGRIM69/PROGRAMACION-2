namespace FolletoDigital;

public partial class FormInicio : Form
{
    public FormInicio()
    {
        InitializeComponent();
        Navegacion.RegistrarInicio(this);
    }

    private void btnSiguiente_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormInformacion>(this);
    }
}
