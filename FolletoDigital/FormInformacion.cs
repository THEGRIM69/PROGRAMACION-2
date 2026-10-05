namespace FolletoDigital;

public partial class FormInformacion : Form
{
    public FormInformacion()
    {
        InitializeComponent();
    }

    private void btnAnterior_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormInicio>(this);
    }

    private void btnSiguiente_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormCaracteristicas>(this);
    }
}
