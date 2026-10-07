namespace FolletoDigital;

internal sealed class SidebarControl : UserControl
{
    internal const int Ancho = 220;

    private static readonly Color AzulOscuro = Color.FromArgb(22, 33, 62);
    private static readonly Color AzulSeleccionado = Color.FromArgb(58, 82, 132);
    private static readonly Color AzulAcento = Color.FromArgb(58, 134, 255);

    internal SidebarControl(Form formularioActual, Type seccionActual)
    {
        Dock = DockStyle.Fill;
        Margin = Padding.Empty;
        BackColor = AzulOscuro;
        Padding = new Padding(15, 22, 15, 16);

        var distribucion = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = AzulOscuro,
            ColumnCount = 1,
            RowCount = 3
        };
        distribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        distribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 94F));
        distribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        distribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));

        var encabezado = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = AzulOscuro,
            Margin = Padding.Empty
        };

        var titulo = new Label
        {
            Location = new Point(0, 4),
            Size = new Size(190, 52),
            Text = "CONTROL DE GASTOS",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var subtitulo = new Label
        {
            Location = new Point(0, 57),
            Size = new Size(190, 30),
            Text = "Menú principal",
            ForeColor = Color.FromArgb(190, 202, 226),
            Font = new Font("Segoe UI", 9F),
            TextAlign = ContentAlignment.MiddleLeft
        };
        encabezado.Controls.Add(subtitulo);
        encabezado.Controls.Add(titulo);

        var menu = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = false,
            BackColor = AzulOscuro,
            Padding = new Padding(0, 8, 0, 0),
            Margin = Padding.Empty
        };

        AgregarOpcion(menu, formularioActual, typeof(FormInicio), seccionActual, "Dashboard");
        AgregarOpcion(menu, formularioActual, typeof(FormInformacion), seccionActual, "Registrar gasto");
        AgregarOpcion(menu, formularioActual, typeof(FormCaracteristicas), seccionActual, "Historial");
        AgregarOpcion(menu, formularioActual, typeof(FormGaleria), seccionActual, "Resumen");
        AgregarOpcion(menu, formularioActual, typeof(FormFinal), seccionActual, "Acerca de");

        var salir = new Button
        {
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            Text = "Salir",
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(12, 0, 0, 0),
            FlatStyle = FlatStyle.Flat,
            BackColor = AzulOscuro,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        salir.FlatAppearance.BorderSize = 0;
        salir.Click += (_, _) => Navegacion.Salir();

        distribucion.Controls.Add(encabezado, 0, 0);
        distribucion.Controls.Add(menu, 0, 1);
        distribucion.Controls.Add(salir, 0, 2);
        Controls.Add(distribucion);
    }

    private static void AgregarOpcion(
        FlowLayoutPanel menu,
        Form formularioActual,
        Type destino,
        Type seccionActual,
        string texto)
    {
        var activa = destino == seccionActual;
        var boton = new Button
        {
            Width = Ancho - 30,
            Height = 48,
            Margin = new Padding(0, 0, 0, 7),
            Text = activa ? $"  {texto}" : texto,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(12, 0, 0, 0),
            FlatStyle = FlatStyle.Flat,
            BackColor = activa ? AzulSeleccionado : AzulOscuro,
            ForeColor = activa ? Color.White : Color.FromArgb(218, 226, 242),
            Font = new Font("Segoe UI", 10F, activa ? FontStyle.Bold : FontStyle.Regular),
            Cursor = Cursors.Hand,
            Tag = destino
        };
        boton.FlatAppearance.BorderSize = 0;
        boton.FlatAppearance.MouseOverBackColor = AzulSeleccionado;
        if (activa)
        {
            boton.FlatAppearance.BorderColor = AzulAcento;
            boton.FlatAppearance.BorderSize = 2;
        }

        boton.Click += (_, _) => Navegacion.Mostrar(formularioActual, (Type)boton.Tag!);
        menu.Controls.Add(boton);
    }
}
