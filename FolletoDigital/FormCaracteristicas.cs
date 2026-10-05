namespace FolletoDigital;

public partial class FormCaracteristicas : Form
{
    public FormCaracteristicas()
    {
        InitializeComponent();
    }

    private void btnAnterior_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormInformacion>(this);
    }

    private void btnSiguiente_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormGaleria>(this);
    }
}
