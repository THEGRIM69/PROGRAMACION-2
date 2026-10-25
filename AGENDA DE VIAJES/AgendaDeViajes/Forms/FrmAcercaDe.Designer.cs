namespace AgendaDeViajes.Forms
{
    partial class FrmAcercaDe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelScroll = new System.Windows.Forms.Panel();
            this.tablaPrincipal = new System.Windows.Forms.TableLayoutPanel();

            // Sección 1: Hero / Cabecera
            this.panelHero = new System.Windows.Forms.Panel();
            this.panelFranjaHero = new System.Windows.Forms.Panel();
            this.tablaHero = new System.Windows.Forms.TableLayoutPanel();
            this.lblIconoApp = new System.Windows.Forms.Label();
            this.panelTextosHero = new System.Windows.Forms.FlowLayoutPanel();
            this.flowTituloHero = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNombreApp = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblDetalleApp = new System.Windows.Forms.Label();

            // Sección 2: Crédito principal
            this.panelCredito = new System.Windows.Forms.Panel();
            this.panelFranjaCredito = new System.Windows.Forms.Panel();
            this.tablaCredito = new System.Windows.Forms.TableLayoutPanel();
            this.lblIconoAutor = new System.Windows.Forms.Label();
            this.panelTextosCredito = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEtiquetaCredito = new System.Windows.Forms.Label();
            this.lblCredito = new System.Windows.Forms.Label();
            this.lblDetalleAutor = new System.Windows.Forms.Label();

            // Sección 3: Tecnologías utilizadas
            this.panelTecnologias = new System.Windows.Forms.Panel();
            this.panelFranjaTec = new System.Windows.Forms.Panel();
            this.panelContenidoTec = new System.Windows.Forms.Panel();
            this.lblTituloTec = new System.Windows.Forms.Label();
            this.lblDescTec = new System.Windows.Forms.Label();
            this.tablaGridTec = new System.Windows.Forms.TableLayoutPanel();

            // Sección 4: Arquitectura y estado
            this.panelArquitectura = new System.Windows.Forms.Panel();
            this.panelFranjaArq = new System.Windows.Forms.Panel();
            this.panelContenidoArq = new System.Windows.Forms.Panel();
            this.lblTituloArq = new System.Windows.Forms.Label();
            this.lblDescArq = new System.Windows.Forms.Label();
            this.flowPildoras = new System.Windows.Forms.FlowLayoutPanel();

            this.panelScroll.SuspendLayout();
            this.tablaPrincipal.SuspendLayout();
            this.panelHero.SuspendLayout();
            this.tablaHero.SuspendLayout();
            this.panelTextosHero.SuspendLayout();
            this.flowTituloHero.SuspendLayout();
            this.panelCredito.SuspendLayout();
            this.tablaCredito.SuspendLayout();
            this.panelTextosCredito.SuspendLayout();
            this.panelTecnologias.SuspendLayout();
            this.panelContenidoTec.SuspendLayout();
            this.panelArquitectura.SuspendLayout();
            this.panelContenidoArq.SuspendLayout();
            this.SuspendLayout();

            //
            // panelScroll
            //
            this.panelScroll.AutoScroll = true;
            this.panelScroll.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.panelScroll.Controls.Add(this.tablaPrincipal);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 0);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Padding = new System.Windows.Forms.Padding(0, 0, 8, 20);
            this.panelScroll.Size = new System.Drawing.Size(941, 580);
            this.panelScroll.TabIndex = 0;

            //
            // tablaPrincipal
            //
            this.tablaPrincipal.AutoSize = true;
            this.tablaPrincipal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablaPrincipal.ColumnCount = 1;
            this.tablaPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaPrincipal.Controls.Add(this.panelHero, 0, 0);
            this.tablaPrincipal.Controls.Add(this.panelCredito, 0, 1);
            this.tablaPrincipal.Controls.Add(this.panelTecnologias, 0, 2);
            this.tablaPrincipal.Controls.Add(this.panelArquitectura, 0, 3);
            this.tablaPrincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablaPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tablaPrincipal.Name = "tablaPrincipal";
            this.tablaPrincipal.RowCount = 4;
            this.tablaPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablaPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablaPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablaPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablaPrincipal.Size = new System.Drawing.Size(933, 560);
            this.tablaPrincipal.TabIndex = 0;

            // ========================================================
            // SECCIÓN 1: HERO / CABECERA DE LA APLICACIÓN
            // ========================================================
            this.panelHero.AutoSize = true;
            this.panelHero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHero.BackColor = System.Drawing.Color.White;
            this.panelHero.Controls.Add(this.tablaHero);
            this.panelHero.Controls.Add(this.panelFranjaHero);
            this.panelHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHero.Location = new System.Drawing.Point(0, 0);
            this.panelHero.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelHero.Name = "panelHero";
            this.panelHero.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelHero.Size = new System.Drawing.Size(933, 130);
            this.panelHero.TabIndex = 0;

            this.panelFranjaHero.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranjaHero.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFranjaHero.Location = new System.Drawing.Point(0, 0);
            this.panelFranjaHero.Name = "panelFranjaHero";
            this.panelFranjaHero.Size = new System.Drawing.Size(5, 130);
            this.panelFranjaHero.TabIndex = 0;

            this.tablaHero.AutoSize = true;
            this.tablaHero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablaHero.ColumnCount = 2;
            this.tablaHero.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tablaHero.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaHero.Controls.Add(this.lblIconoApp, 0, 0);
            this.tablaHero.Controls.Add(this.panelTextosHero, 1, 0);
            this.tablaHero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaHero.Location = new System.Drawing.Point(5, 0);
            this.tablaHero.Name = "tablaHero";
            this.tablaHero.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.tablaHero.RowCount = 1;
            this.tablaHero.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablaHero.Size = new System.Drawing.Size(908, 130);
            this.tablaHero.TabIndex = 1;

            this.lblIconoApp.BackColor = System.Drawing.Color.Transparent;
            this.lblIconoApp.Font = new System.Drawing.Font("Segoe UI Emoji", 28F);
            this.lblIconoApp.ForeColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.lblIconoApp.Location = new System.Drawing.Point(16, 16);
            this.lblIconoApp.Name = "lblIconoApp";
            this.lblIconoApp.Size = new System.Drawing.Size(60, 60);
            this.lblIconoApp.TabIndex = 0;
            this.lblIconoApp.Text = "✈";
            this.lblIconoApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTextosHero.AutoSize = true;
            this.panelTextosHero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTextosHero.Controls.Add(this.flowTituloHero);
            this.panelTextosHero.Controls.Add(this.lblDescripcion);
            this.panelTextosHero.Controls.Add(this.lblDetalleApp);
            this.panelTextosHero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTextosHero.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelTextosHero.Location = new System.Drawing.Point(86, 16);
            this.panelTextosHero.Margin = new System.Windows.Forms.Padding(0);
            this.panelTextosHero.Name = "panelTextosHero";
            this.panelTextosHero.Size = new System.Drawing.Size(806, 98);
            this.panelTextosHero.TabIndex = 1;
            this.panelTextosHero.WrapContents = false;

            this.flowTituloHero.AutoSize = true;
            this.flowTituloHero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowTituloHero.Controls.Add(this.lblNombreApp);
            this.flowTituloHero.Controls.Add(this.lblVersion);
            this.flowTituloHero.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowTituloHero.Location = new System.Drawing.Point(0, 0);
            this.flowTituloHero.Margin = new System.Windows.Forms.Padding(0);
            this.flowTituloHero.Name = "flowTituloHero";
            this.flowTituloHero.Size = new System.Drawing.Size(360, 36);
            this.flowTituloHero.TabIndex = 0;
            this.flowTituloHero.WrapContents = false;

            this.lblNombreApp.AutoSize = true;
            this.lblNombreApp.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblNombreApp.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.lblNombreApp.Location = new System.Drawing.Point(0, 0);
            this.lblNombreApp.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblNombreApp.Name = "lblNombreApp";
            this.lblNombreApp.Size = new System.Drawing.Size(252, 32);
            this.lblNombreApp.TabIndex = 0;
            this.lblNombreApp.Text = "AGENDA DE VIAJES";

            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.FromArgb(234, 243, 251);
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.lblVersion.Location = new System.Drawing.Point(264, 6);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.lblVersion.Size = new System.Drawing.Size(96, 21);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "VERSIÓN 1.0";

            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblDescripcion.Location = new System.Drawing.Point(0, 38);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(395, 19);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Sistema de gestión de vuelos, pasajeros y reservaciones.";

            this.lblDetalleApp.AutoSize = true;
            this.lblDetalleApp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDetalleApp.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblDetalleApp.Location = new System.Drawing.Point(0, 61);
            this.lblDetalleApp.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetalleApp.Name = "lblDetalleApp";
            this.lblDetalleApp.Size = new System.Drawing.Size(650, 15);
            this.lblDetalleApp.TabIndex = 2;
            this.lblDetalleApp.Text = "Plataforma para la administración integral de operaciones aéreas, control de itinerarios y emisión de reservaciones.";

            // ========================================================
            // SECCIÓN 2: CRÉDITO DEL DESARROLLADOR (DESTACADO)
            // ========================================================
            this.panelCredito.AutoSize = true;
            this.panelCredito.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCredito.BackColor = System.Drawing.Color.White;
            this.panelCredito.Controls.Add(this.tablaCredito);
            this.panelCredito.Controls.Add(this.panelFranjaCredito);
            this.panelCredito.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCredito.Location = new System.Drawing.Point(0, 144);
            this.panelCredito.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelCredito.Name = "panelCredito";
            this.panelCredito.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelCredito.Size = new System.Drawing.Size(933, 115);
            this.panelCredito.TabIndex = 1;

            this.panelFranjaCredito.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranjaCredito.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFranjaCredito.Location = new System.Drawing.Point(0, 0);
            this.panelFranjaCredito.Name = "panelFranjaCredito";
            this.panelFranjaCredito.Size = new System.Drawing.Size(5, 115);
            this.panelFranjaCredito.TabIndex = 0;

            this.tablaCredito.AutoSize = true;
            this.tablaCredito.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablaCredito.ColumnCount = 2;
            this.tablaCredito.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tablaCredito.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaCredito.Controls.Add(this.lblIconoAutor, 0, 0);
            this.tablaCredito.Controls.Add(this.panelTextosCredito, 1, 0);
            this.tablaCredito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaCredito.Location = new System.Drawing.Point(5, 0);
            this.tablaCredito.Name = "tablaCredito";
            this.tablaCredito.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.tablaCredito.RowCount = 1;
            this.tablaCredito.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablaCredito.Size = new System.Drawing.Size(908, 115);
            this.tablaCredito.TabIndex = 1;

            this.lblIconoAutor.BackColor = System.Drawing.Color.Transparent;
            this.lblIconoAutor.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblIconoAutor.ForeColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.lblIconoAutor.Location = new System.Drawing.Point(16, 16);
            this.lblIconoAutor.Name = "lblIconoAutor";
            this.lblIconoAutor.Size = new System.Drawing.Size(60, 50);
            this.lblIconoAutor.TabIndex = 0;
            this.lblIconoAutor.Text = "👤";
            this.lblIconoAutor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTextosCredito.AutoSize = true;
            this.panelTextosCredito.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTextosCredito.Controls.Add(this.lblEtiquetaCredito);
            this.panelTextosCredito.Controls.Add(this.lblCredito);
            this.panelTextosCredito.Controls.Add(this.lblDetalleAutor);
            this.panelTextosCredito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTextosCredito.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelTextosCredito.Location = new System.Drawing.Point(86, 16);
            this.panelTextosCredito.Margin = new System.Windows.Forms.Padding(0);
            this.panelTextosCredito.Name = "panelTextosCredito";
            this.panelTextosCredito.Size = new System.Drawing.Size(806, 83);
            this.panelTextosCredito.TabIndex = 1;
            this.panelTextosCredito.WrapContents = false;

            this.lblEtiquetaCredito.AutoSize = true;
            this.lblEtiquetaCredito.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblEtiquetaCredito.ForeColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.lblEtiquetaCredito.Location = new System.Drawing.Point(0, 0);
            this.lblEtiquetaCredito.Margin = new System.Windows.Forms.Padding(0);
            this.lblEtiquetaCredito.Name = "lblEtiquetaCredito";
            this.lblEtiquetaCredito.Size = new System.Drawing.Size(215, 15);
            this.lblEtiquetaCredito.TabIndex = 0;
            this.lblEtiquetaCredito.Text = "CRÉDITO PRINCIPAL DEL DESARROLLADOR";

            this.lblCredito.AutoSize = true;
            this.lblCredito.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblCredito.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.lblCredito.Location = new System.Drawing.Point(0, 18);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(0, 3, 0, 4);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(371, 28);
            this.lblCredito.TabIndex = 1;
            this.lblCredito.Text = "By: Rodrigo Rivera - RF1631012025";

            this.lblDetalleAutor.AutoSize = true;
            this.lblDetalleAutor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDetalleAutor.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblDetalleAutor.Location = new System.Drawing.Point(0, 50);
            this.lblDetalleAutor.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetalleAutor.Name = "lblDetalleAutor";
            this.lblDetalleAutor.Size = new System.Drawing.Size(382, 15);
            this.lblDetalleAutor.TabIndex = 2;
            this.lblDetalleAutor.Text = "Proyecto Académico Universitario · Programación II · Ciclo I-2026";

            // ========================================================
            // SECCIÓN 3: TECNOLOGÍAS UTILIZADAS
            // ========================================================
            this.panelTecnologias.AutoSize = true;
            this.panelTecnologias.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTecnologias.BackColor = System.Drawing.Color.White;
            this.panelTecnologias.Controls.Add(this.panelContenidoTec);
            this.panelTecnologias.Controls.Add(this.panelFranjaTec);
            this.panelTecnologias.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTecnologias.Location = new System.Drawing.Point(0, 273);
            this.panelTecnologias.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelTecnologias.Name = "panelTecnologias";
            this.panelTecnologias.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelTecnologias.Size = new System.Drawing.Size(933, 220);
            this.panelTecnologias.TabIndex = 2;

            this.panelFranjaTec.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranjaTec.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFranjaTec.Location = new System.Drawing.Point(0, 0);
            this.panelFranjaTec.Name = "panelFranjaTec";
            this.panelFranjaTec.Size = new System.Drawing.Size(5, 220);
            this.panelFranjaTec.TabIndex = 0;

            this.panelContenidoTec.AutoSize = true;
            this.panelContenidoTec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelContenidoTec.Controls.Add(this.lblTituloTec);
            this.panelContenidoTec.Controls.Add(this.lblDescTec);
            this.panelContenidoTec.Controls.Add(this.tablaGridTec);
            this.panelContenidoTec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenidoTec.Location = new System.Drawing.Point(5, 0);
            this.panelContenidoTec.Name = "panelContenidoTec";
            this.panelContenidoTec.Padding = new System.Windows.Forms.Padding(20, 16, 16, 16);
            this.panelContenidoTec.Size = new System.Drawing.Size(908, 220);
            this.panelContenidoTec.TabIndex = 1;

            this.lblTituloTec.AutoSize = true;
            this.lblTituloTec.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblTituloTec.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.lblTituloTec.Location = new System.Drawing.Point(20, 14);
            this.lblTituloTec.Name = "lblTituloTec";
            this.lblTituloTec.Size = new System.Drawing.Size(166, 21);
            this.lblTituloTec.TabIndex = 0;
            this.lblTituloTec.Text = "Tecnologías Utilizadas";

            this.lblDescTec.AutoSize = true;
            this.lblDescTec.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescTec.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblDescTec.Location = new System.Drawing.Point(20, 38);
            this.lblDescTec.Name = "lblDescTec";
            this.lblDescTec.Size = new System.Drawing.Size(490, 15);
            this.lblDescTec.TabIndex = 1;
            this.lblDescTec.Text = "Componentes técnicos y entorno configurado para el desarrollo y ejecución del proyecto:";

            this.tablaGridTec.AutoSize = true;
            this.tablaGridTec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablaGridTec.ColumnCount = 3;
            this.tablaGridTec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tablaGridTec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tablaGridTec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tablaGridTec.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablaGridTec.Location = new System.Drawing.Point(20, 62);
            this.tablaGridTec.Margin = new System.Windows.Forms.Padding(0);
            this.tablaGridTec.Name = "tablaGridTec";
            this.tablaGridTec.RowCount = 2;
            this.tablaGridTec.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tablaGridTec.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tablaGridTec.Size = new System.Drawing.Size(872, 140);
            this.tablaGridTec.TabIndex = 2;

            // Creación de las 6 tarjetas de tecnologías obligatorias
            this.tablaGridTec.Controls.Add(CrearTarjetaTecnologia("Lenguaje", "C#", "Tipado estático y moderno"), 0, 0);
            this.tablaGridTec.Controls.Add(CrearTarjetaTecnologia("Framework", ".NET 10", "SDK 10.0.401 · Runtime v10.0"), 1, 0);
            this.tablaGridTec.Controls.Add(CrearTarjetaTecnologia("Interfaz gráfica", "Windows Forms", "High DPI PerMonitorV2"), 2, 0);
            this.tablaGridTec.Controls.Add(CrearTarjetaTecnologia("Entorno de desarrollo", "Visual Studio Community 2026", "IDE y herramientas C#"), 0, 1);
            this.tablaGridTec.Controls.Add(CrearTarjetaTecnologia("Persistencia", "archivos JSON", "Serialización con System.Text.Json"), 1, 1);
            this.tablaGridTec.Controls.Add(CrearTarjetaTecnologia("Plataforma", "Windows 11", "Windows 11 Pro 64-bit"), 2, 1);

            // ========================================================
            // SECCIÓN 4: ARQUITECTURA Y ESTADO DEL SISTEMA
            // ========================================================
            this.panelArquitectura.AutoSize = true;
            this.panelArquitectura.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelArquitectura.BackColor = System.Drawing.Color.White;
            this.panelArquitectura.Controls.Add(this.panelContenidoArq);
            this.panelArquitectura.Controls.Add(this.panelFranjaArq);
            this.panelArquitectura.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelArquitectura.Location = new System.Drawing.Point(0, 507);
            this.panelArquitectura.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelArquitectura.Name = "panelArquitectura";
            this.panelArquitectura.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelArquitectura.Size = new System.Drawing.Size(933, 140);
            this.panelArquitectura.TabIndex = 3;

            this.panelFranjaArq.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranjaArq.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFranjaArq.Location = new System.Drawing.Point(0, 0);
            this.panelFranjaArq.Name = "panelFranjaArq";
            this.panelFranjaArq.Size = new System.Drawing.Size(5, 140);
            this.panelFranjaArq.TabIndex = 0;

            this.panelContenidoArq.AutoSize = true;
            this.panelContenidoArq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelContenidoArq.Controls.Add(this.lblTituloArq);
            this.panelContenidoArq.Controls.Add(this.lblDescArq);
            this.panelContenidoArq.Controls.Add(this.flowPildoras);
            this.panelContenidoArq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenidoArq.Location = new System.Drawing.Point(5, 0);
            this.panelContenidoArq.Name = "panelContenidoArq";
            this.panelContenidoArq.Padding = new System.Windows.Forms.Padding(20, 16, 16, 16);
            this.panelContenidoArq.Size = new System.Drawing.Size(908, 140);
            this.panelContenidoArq.TabIndex = 1;

            this.lblTituloArq.AutoSize = true;
            this.lblTituloArq.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblTituloArq.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.lblTituloArq.Location = new System.Drawing.Point(20, 14);
            this.lblTituloArq.Name = "lblTituloArq";
            this.lblTituloArq.Size = new System.Drawing.Size(188, 21);
            this.lblTituloArq.TabIndex = 0;
            this.lblTituloArq.Text = "Arquitectura del Sistema";

            this.lblDescArq.AutoSize = true;
            this.lblDescArq.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescArq.ForeColor = System.Drawing.Color.FromArgb(85, 101, 122);
            this.lblDescArq.Location = new System.Drawing.Point(20, 38);
            this.lblDescArq.MaximumSize = new System.Drawing.Size(860, 0);
            this.lblDescArq.Name = "lblDescArq";
            this.lblDescArq.Size = new System.Drawing.Size(850, 30);
            this.lblDescArq.TabIndex = 1;
            this.lblDescArq.Text = "Implementación modular en tres capas: presentación desacoplada, lógica y validación de reglas de negocio, y persistencia local atómica mediante serialización JSON con respaldo (.bak) y tolerancia a fallos.";

            this.flowPildoras.AutoSize = true;
            this.flowPildoras.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowPildoras.Controls.Add(CrearPildora("Planificación: 9 fases modulares"));
            this.flowPildoras.Controls.Add(CrearPildora("Pruebas: 48 verificaciones automatizadas superadas"));
            this.flowPildoras.Controls.Add(CrearPildora("Persistencia: agenda.json con respaldos"));
            this.flowPildoras.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPildoras.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowPildoras.Location = new System.Drawing.Point(20, 76);
            this.flowPildoras.Margin = new System.Windows.Forms.Padding(0);
            this.flowPildoras.Name = "flowPildoras";
            this.flowPildoras.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flowPildoras.Size = new System.Drawing.Size(872, 36);
            this.flowPildoras.TabIndex = 2;

            //
            // FrmAcercaDe
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(941, 580);
            this.Controls.Add(this.panelScroll);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAcercaDe";
            this.Text = "Acerca de";
            this.panelScroll.ResumeLayout(false);
            this.panelScroll.PerformLayout();
            this.tablaPrincipal.ResumeLayout(false);
            this.tablaPrincipal.PerformLayout();
            this.panelHero.ResumeLayout(false);
            this.panelHero.PerformLayout();
            this.tablaHero.ResumeLayout(false);
            this.tablaHero.PerformLayout();
            this.panelTextosHero.ResumeLayout(false);
            this.panelTextosHero.PerformLayout();
            this.flowTituloHero.ResumeLayout(false);
            this.flowTituloHero.PerformLayout();
            this.panelCredito.ResumeLayout(false);
            this.panelCredito.PerformLayout();
            this.tablaCredito.ResumeLayout(false);
            this.tablaCredito.PerformLayout();
            this.panelTextosCredito.ResumeLayout(false);
            this.panelTextosCredito.PerformLayout();
            this.panelTecnologias.ResumeLayout(false);
            this.panelTecnologias.PerformLayout();
            this.panelContenidoTec.ResumeLayout(false);
            this.panelContenidoTec.PerformLayout();
            this.panelArquitectura.ResumeLayout(false);
            this.panelArquitectura.PerformLayout();
            this.panelContenidoArq.ResumeLayout(false);
            this.panelContenidoArq.PerformLayout();
            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Panel CrearTarjetaTecnologia(string categoria, string valor, string detalle)
        {
            var pnl = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(248, 250, 252),
                Dock = System.Windows.Forms.DockStyle.Fill,
                Margin = new System.Windows.Forms.Padding(0, 4, 10, 6),
                Padding = new System.Windows.Forms.Padding(12, 8, 12, 8)
            };

            var franja = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(45, 156, 219),
                Dock = System.Windows.Forms.DockStyle.Left,
                Width = 3
            };

            var lblCat = new System.Windows.Forms.Label
            {
                AutoSize = false,
                Font = new System.Drawing.Font("Segoe UI", 8F),
                ForeColor = System.Drawing.Color.FromArgb(107, 122, 140),
                Location = new System.Drawing.Point(12, 6),
                Size = new System.Drawing.Size(240, 15),
                Text = categoria
            };

            var lblVal = new System.Windows.Forms.Label
            {
                AutoSize = false,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(22, 34, 47),
                Location = new System.Drawing.Point(12, 22),
                Size = new System.Drawing.Size(240, 20),
                Text = valor
            };

            var lblDet = new System.Windows.Forms.Label
            {
                AutoSize = false,
                Font = new System.Drawing.Font("Segoe UI", 7.5F),
                ForeColor = System.Drawing.Color.FromArgb(140, 152, 166),
                Location = new System.Drawing.Point(12, 42),
                Size = new System.Drawing.Size(240, 15),
                Text = detalle
            };

            pnl.Controls.Add(franja);
            pnl.Controls.Add(lblCat);
            pnl.Controls.Add(lblVal);
            pnl.Controls.Add(lblDet);

            return pnl;
        }

        private static System.Windows.Forms.Label CrearPildora(string texto)
        {
            return new System.Windows.Forms.Label
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.FromArgb(234, 243, 251),
                Font = new System.Drawing.Font("Segoe UI Semibold", 8F),
                ForeColor = System.Drawing.Color.FromArgb(45, 156, 219),
                Margin = new System.Windows.Forms.Padding(0, 0, 10, 6),
                Padding = new System.Windows.Forms.Padding(8, 4, 8, 4),
                Text = texto
            };
        }

        #endregion

        private System.Windows.Forms.Panel panelScroll;
        private System.Windows.Forms.TableLayoutPanel tablaPrincipal;
        private System.Windows.Forms.Panel panelHero;
        private System.Windows.Forms.Panel panelFranjaHero;
        private System.Windows.Forms.TableLayoutPanel tablaHero;
        private System.Windows.Forms.Label lblIconoApp;
        private System.Windows.Forms.FlowLayoutPanel panelTextosHero;
        private System.Windows.Forms.FlowLayoutPanel flowTituloHero;
        private System.Windows.Forms.Label lblNombreApp;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblDetalleApp;
        private System.Windows.Forms.Panel panelCredito;
        private System.Windows.Forms.Panel panelFranjaCredito;
        private System.Windows.Forms.TableLayoutPanel tablaCredito;
        private System.Windows.Forms.Label lblIconoAutor;
        private System.Windows.Forms.FlowLayoutPanel panelTextosCredito;
        private System.Windows.Forms.Label lblEtiquetaCredito;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Label lblDetalleAutor;
        private System.Windows.Forms.Panel panelTecnologias;
        private System.Windows.Forms.Panel panelFranjaTec;
        private System.Windows.Forms.Panel panelContenidoTec;
        private System.Windows.Forms.Label lblTituloTec;
        private System.Windows.Forms.Label lblDescTec;
        private System.Windows.Forms.TableLayoutPanel tablaGridTec;
        private System.Windows.Forms.Panel panelArquitectura;
        private System.Windows.Forms.Panel panelFranjaArq;
        private System.Windows.Forms.Panel panelContenidoArq;
        private System.Windows.Forms.Label lblTituloArq;
        private System.Windows.Forms.Label lblDescArq;
        private System.Windows.Forms.FlowLayoutPanel flowPildoras;
    }
}

