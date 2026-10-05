namespace FolletoDigital;

internal static class Navegacion
{
    private static readonly Dictionary<Type, Form> Formularios = new();
    private static FormInicio? _inicio;
    private static bool _cerrandoAplicacion;

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
            destino.FormClosed += (_, _) =>
            {
                Formularios.Remove(tipo);
                if (!_cerrandoAplicacion && _inicio is { IsDisposed: false })
                {
                    _inicio.Show();
                    _inicio.BringToFront();
                }
            };
        }

        actual.Hide();
        destino.Show();
        destino.BringToFront();
    }

    internal static void VolverAlInicio(Form actual)
    {
        Mostrar<FormInicio>(actual);
    }

    internal static void Salir()
    {
        _cerrandoAplicacion = true;
        Application.Exit();
    }
}
