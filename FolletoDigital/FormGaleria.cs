namespace FolletoDigital;

public partial class FormGaleria : Form
{
    public FormGaleria()
    {
        InitializeComponent();
    }

    private void btnAnterior_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormCaracteristicas>(this);
    }

    private void btnSiguiente_Click(object? sender, EventArgs e)
    {
        Navegacion.Mostrar<FormFinal>(this);
    }
}
