namespace FolletoDigital;

internal static class Navegacion
{
    private static readonly Dictionary<Type, Form> Formularios = new();
    private static FormInicio? _inicio;

    internal static void RegistrarInicio(FormInicio inicio)
    {
        _inicio = inicio;
        Formularios[typeof(FormInicio)] = inicio;
    }

    internal static void Mostrar<TFormulario>(Form actual) where TFormulario : Form, new()
    {
        var tipo = typeof(TFormulario);

        if (!Formularios.TryGetValue(tipo, out var destino) || destino.IsDisposed)
        {
            destino = new TFormulario();
            Formularios[tipo] = destino;
            destino.Owner = actual;
        }

        actual.Hide();
        destino.Show();
        destino.BringToFront();
        destino.Activate();
    }

    internal static void VolverAlInicio(Form actual)
    {
        if (actual == _inicio)
        {
            return;
        }

        actual.Hide();

        if (_inicio is { IsDisposed: false })
        {
            _inicio.ActualizarDashboard();
            _inicio.Show();
            _inicio.BringToFront();
            _inicio.Activate();
        }
    }

    internal static void Salir()
    {
        Application.Exit();
    }
}
