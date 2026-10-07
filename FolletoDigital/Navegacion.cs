namespace FolletoDigital;

internal static class Navegacion
{
    private static readonly Dictionary<Type, Form> Formularios = new();
    private static FormInicio? _inicio;

    internal static void RegistrarInicio(FormInicio inicio)
    {
        _inicio = inicio;
        Formularios[typeof(FormInicio)] = inicio;
        ConfigurarBarraLateral(inicio);
    }

    internal static void ConfigurarBarraLateral(Form formulario)
    {
        if (formulario.Controls.OfType<SidebarControl>().Any())
        {
            return;
        }

        var anchoContenido = formulario.ClientSize.Width;
        var altoContenido = formulario.ClientSize.Height;
        var controlesExistentes = formulario.Controls.Cast<Control>().ToArray();
        var contenido = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = formulario.BackColor,
            Size = new Size(anchoContenido, altoContenido),
            Margin = Padding.Empty
        };

        foreach (var control in controlesExistentes)
        {
            var indice = formulario.Controls.GetChildIndex(control);
            formulario.Controls.Remove(control);
            contenido.Controls.Add(control);
            contenido.Controls.SetChildIndex(control, indice);
        }

        var distribucion = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = formulario.BackColor,
            ColumnCount = 2,
            RowCount = 1,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        distribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, SidebarControl.Ancho));
        distribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        distribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var barraLateral = new SidebarControl(formulario, formulario.GetType());
        distribucion.Controls.Add(barraLateral, 0, 0);
        distribucion.Controls.Add(contenido, 1, 0);
        formulario.Controls.Add(distribucion);
        formulario.ClientSize = new Size(anchoContenido + SidebarControl.Ancho, altoContenido);
        formulario.FormClosing += (_, evento) =>
        {
            if (evento.CloseReason == CloseReason.UserClosing)
            {
                Salir();
            }
        };
    }

    internal static void Mostrar<TFormulario>(Form actual) where TFormulario : Form, new()
    {
        Mostrar(actual, typeof(TFormulario));
    }

    internal static void Mostrar(Form actual, Type tipo)
    {
        if (!Formularios.TryGetValue(tipo, out var destino) || destino.IsDisposed)
        {
            destino = CrearFormulario(tipo);
            Formularios[tipo] = destino;
            ConfigurarBarraLateral(destino);
        }

        ActualizarDatos(destino);

        if (actual != destino)
        {
            actual.Hide();
        }

        destino.Show();
        destino.BringToFront();
        destino.Activate();
    }

    internal static void Salir()
    {
        Application.Exit();
    }

    private static Form CrearFormulario(Type tipo)
    {
        if (tipo == typeof(FormInicio))
        {
            return _inicio is { IsDisposed: false } ? _inicio : new FormInicio();
        }

        if (tipo == typeof(FormInformacion))
        {
            return new FormInformacion();
        }

        if (tipo == typeof(FormCaracteristicas))
        {
            return new FormCaracteristicas();
        }

        if (tipo == typeof(FormGaleria))
        {
            return new FormGaleria();
        }

        if (tipo == typeof(FormFinal))
        {
            return new FormFinal();
        }

        throw new ArgumentOutOfRangeException(nameof(tipo), tipo, "La sección solicitada no está registrada.");
    }

    private static void ActualizarDatos(Form formulario)
    {
        switch (formulario)
        {
            case FormInicio inicio:
                inicio.ActualizarDashboard();
                break;
            case FormCaracteristicas historial:
                historial.CargarGastos();
                break;
            case FormGaleria resumen:
                resumen.ActualizarResumen();
                break;
        }
    }
}
