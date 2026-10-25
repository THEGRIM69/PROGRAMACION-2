namespace AgendaDeViajes.Forms
{
    partial class FrmPrincipal
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

            System.Windows.Forms.TreeNode treeNodePanelPrincipal = new System.Windows.Forms.TreeNode("Panel principal");
            System.Windows.Forms.TreeNode treeNodeVuelos = new System.Windows.Forms.TreeNode("Vuelos");
            System.Windows.Forms.TreeNode treeNodePasajeros = new System.Windows.Forms.TreeNode("Pasajeros");
            System.Windows.Forms.TreeNode treeNodeReservaciones = new System.Windows.Forms.TreeNode("Reservaciones");
            System.Windows.Forms.TreeNode treeNodeReportes = new System.Windows.Forms.TreeNode("Reportes");
            System.Windows.Forms.TreeNode treeNodeAcercaDe = new System.Windows.Forms.TreeNode("Acerca de");

            this.menuStripPrincipal = new System.Windows.Forms.MenuStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNuevoVuelo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNuevoPasajero = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNuevaReservacion = new System.Windows.Forms.ToolStripMenuItem();
            this.sepArchivo = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVerDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVerVuelos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVerPasajeros = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVerReservaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVerReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHerramientas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportar = new System.Windows.Forms.ToolStripMenuItem();
            this.sepHerramientas = new System.Windows.Forms.ToolStripSeparator();
            this.mnuColorAcento = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripPrincipal = new System.Windows.Forms.StatusStrip();
            this.lblEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEspacio = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFecha = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContenedor = new System.Windows.Forms.SplitContainer();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.lblLogoApp = new System.Windows.Forms.Label();
            this.lblNombreApp = new System.Windows.Forms.Label();
            this.lblSubtituloApp = new System.Windows.Forms.Label();
            this.panelLineaLateral = new System.Windows.Forms.Panel();
            this.lblSeccionNavegacion = new System.Windows.Forms.Label();
            this.treeViewNavegacion = new System.Windows.Forms.TreeView();
            this.lblPieLateral = new System.Windows.Forms.Label();
            this.tablaPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblModuloTitulo = new System.Windows.Forms.Label();
            this.lblModuloDescripcion = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelBienvenida = new System.Windows.Forms.Panel();
            this.tablaBienvenida = new System.Windows.Forms.TableLayoutPanel();
            this.lblBienvenidaTitulo = new System.Windows.Forms.Label();
            this.lblBienvenidaTexto = new System.Windows.Forms.Label();
            this.flowModulos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMarcador = new System.Windows.Forms.Panel();
            this.lblMarcadorTitulo = new System.Windows.Forms.Label();
            this.lblMarcadorTexto = new System.Windows.Forms.Label();
            this.lblMarcadorFase = new System.Windows.Forms.Label();
            this.timerReloj = new System.Windows.Forms.Timer(this.components);
            this.menuStripPrincipal.SuspendLayout();
            this.statusStripPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenedor)).BeginInit();
            this.splitContenedor.Panel1.SuspendLayout();
            this.splitContenedor.Panel2.SuspendLayout();
            this.splitContenedor.SuspendLayout();
            this.panelLateral.SuspendLayout();
            this.tablaPrincipal.SuspendLayout();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelBienvenida.SuspendLayout();
            this.tablaBienvenida.SuspendLayout();
            this.panelMarcador.SuspendLayout();
            this.SuspendLayout();
            //
            // menuStripPrincipal
            //
            this.menuStripPrincipal.BackColor = System.Drawing.Color.White;
            this.menuStripPrincipal.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.menuStripPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArchivo,
            this.menuVer,
            this.menuHerramientas,
            this.menuAyuda});
            this.menuStripPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuStripPrincipal.Name = "menuStripPrincipal";
            this.menuStripPrincipal.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.menuStripPrincipal.Renderer = new System.Windows.Forms.ToolStripProfessionalRenderer(new TemaMenu());
            this.menuStripPrincipal.Size = new System.Drawing.Size(1240, 32);
            this.menuStripPrincipal.TabIndex = 0;
            this.menuStripPrincipal.Text = "menuStripPrincipal";
            //
            // menuArchivo
            //
            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNuevoVuelo,
            this.mnuNuevoPasajero,
            this.mnuNuevaReservacion,
            this.sepArchivo,
            this.mnuSalir});
            this.menuArchivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.menuArchivo.Name = "menuArchivo";
            this.menuArchivo.Text = "&Archivo";
            //
            // mnuNuevoVuelo
            //
            this.mnuNuevoVuelo.Enabled = true;
            this.mnuNuevoVuelo.Name = "mnuNuevoVuelo";
            this.mnuNuevoVuelo.Text = "Nuevo vuelo...";
            this.mnuNuevoVuelo.Click += new System.EventHandler(this.mnuNuevoVuelo_Click);
            //
            // mnuNuevoPasajero
            //
            this.mnuNuevoPasajero.Enabled = true;
            this.mnuNuevoPasajero.Name = "mnuNuevoPasajero";
            this.mnuNuevoPasajero.Text = "Nuevo pasajero...";
            this.mnuNuevoPasajero.Click += new System.EventHandler(this.mnuNuevoPasajero_Click);
            //
            // mnuNuevaReservacion
            //
            this.mnuNuevaReservacion.Enabled = true;
            this.mnuNuevaReservacion.Name = "mnuNuevaReservacion";
            this.mnuNuevaReservacion.Text = "Nueva reservación...";
            this.mnuNuevaReservacion.Click += new System.EventHandler(this.mnuNuevaReservacion_Click);
            //
            // sepArchivo
            //
            this.sepArchivo.Name = "sepArchivo";
            //
            // mnuSalir
            //
            this.mnuSalir.Name = "mnuSalir";
            this.mnuSalir.Text = "&Salir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            //
            // menuVer
            //
            this.menuVer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVerDashboard,
            this.mnuVerVuelos,
            this.mnuVerPasajeros,
            this.mnuVerReservaciones,
            this.mnuVerReportes});
            this.menuVer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.menuVer.Name = "menuVer";
            this.menuVer.Text = "&Ver";
            //
            // mnuVerDashboard
            //
            this.mnuVerDashboard.Name = "mnuVerDashboard";
            this.mnuVerDashboard.Text = "Panel principal";
            this.mnuVerDashboard.Click += new System.EventHandler(this.mnuVerDashboard_Click);
            //
            // mnuVerVuelos
            //
            this.mnuVerVuelos.Name = "mnuVerVuelos";
            this.mnuVerVuelos.Text = "Vuelos";
            this.mnuVerVuelos.Click += new System.EventHandler(this.mnuVerVuelos_Click);
            //
            // mnuVerPasajeros
            //
            this.mnuVerPasajeros.Name = "mnuVerPasajeros";
            this.mnuVerPasajeros.Text = "Pasajeros";
            this.mnuVerPasajeros.Click += new System.EventHandler(this.mnuVerPasajeros_Click);
            //
            // mnuVerReservaciones
            //
            this.mnuVerReservaciones.Name = "mnuVerReservaciones";
            this.mnuVerReservaciones.Text = "Reservaciones";
            this.mnuVerReservaciones.Click += new System.EventHandler(this.mnuVerReservaciones_Click);
            //
            // mnuVerReportes
            //
            this.mnuVerReportes.Name = "mnuVerReportes";
            this.mnuVerReportes.Text = "Reportes y consultas";
            this.mnuVerReportes.Click += new System.EventHandler(this.mnuVerReportes_Click);
            //
            // menuHerramientas
            //
            this.menuHerramientas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportar,
            this.mnuExportar,
            this.sepHerramientas,
            this.mnuColorAcento});
            this.menuHerramientas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.menuHerramientas.Name = "menuHerramientas";
            this.menuHerramientas.Text = "&Herramientas";
            //
            // mnuImportar
            //
            this.mnuImportar.Enabled = false;
            this.mnuImportar.Name = "mnuImportar";
            this.mnuImportar.Text = "Importar agenda...";
            this.mnuImportar.Click += new System.EventHandler(this.mnuImportar_Click);
            //
            // mnuExportar
            //
            this.mnuExportar.Enabled = true;
            this.mnuExportar.Name = "mnuExportar";
            this.mnuExportar.Text = "Exportar reporte...";
            this.mnuExportar.Click += new System.EventHandler(this.mnuExportar_Click);
            //
            // sepHerramientas
            //
            this.sepHerramientas.Name = "sepHerramientas";
            //
            // mnuColorAcento
            //
            this.mnuColorAcento.Enabled = false;
            this.mnuColorAcento.Name = "mnuColorAcento";
            this.mnuColorAcento.Text = "Color de acento...";
            this.mnuColorAcento.Click += new System.EventHandler(this.mnuColorAcento_Click);
            //
            // menuAyuda
            //
            this.menuAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAcercaDe});
            this.menuAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.menuAyuda.Name = "menuAyuda";
            this.menuAyuda.Text = "A&yuda";
            //
            // mnuAcercaDe
            //
            this.mnuAcercaDe.Name = "mnuAcercaDe";
            this.mnuAcercaDe.Text = "Acerca de AGENDA DE VIAJES...";
            this.mnuAcercaDe.Click += new System.EventHandler(this.mnuAcercaDe_Click);
            //
            // statusStripPrincipal
            //
            this.statusStripPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47)))));
            this.statusStripPrincipal.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.statusStripPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstado,
            this.lblRegistros,
            this.lblEspacio,
            this.lblFecha,
            this.lblVersion});
            this.statusStripPrincipal.Location = new System.Drawing.Point(0, 740);
            this.statusStripPrincipal.Name = "statusStripPrincipal";
            this.statusStripPrincipal.Renderer = new System.Windows.Forms.ToolStripProfessionalRenderer(new TemaMenu());
            this.statusStripPrincipal.Size = new System.Drawing.Size(1240, 22);
            this.statusStripPrincipal.SizingGrip = false;
            this.statusStripPrincipal.TabIndex = 2;
            this.statusStripPrincipal.Text = "statusStripPrincipal";
            //
            // lblEstado
            //
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(227)))));
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Text = "Listo";
            //
            // lblRegistros
            //
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.lblRegistros.Margin = new System.Windows.Forms.Padding(16, 3, 0, 2);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Text = "Fase 1 · Estructura inicial";
            //
            // lblEspacio
            //
            this.lblEspacio.AutoSize = false;
            this.lblEspacio.Name = "lblEspacio";
            this.lblEspacio.Size = new System.Drawing.Size(420, 17);
            this.lblEspacio.Spring = true;
            //
            // lblFecha
            //
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(227)))));
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Text = "—";
            //
            // lblVersion
            //
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.lblVersion.Margin = new System.Windows.Forms.Padding(16, 3, 0, 2);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Text = "v0.6 · Fase 6";
            //
            // splitContenedor
            //
            this.splitContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47)))));
            this.splitContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenedor.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContenedor.IsSplitterFixed = true;
            this.splitContenedor.Location = new System.Drawing.Point(0, 32);
            this.splitContenedor.Name = "splitContenedor";
            //
            // splitContenedor.Panel1
            //
            this.splitContenedor.Panel1.Controls.Add(this.panelLateral);
            this.splitContenedor.Panel1MinSize = 250;
            //
            // splitContenedor.Panel2
            //
            this.splitContenedor.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.splitContenedor.Panel2.Controls.Add(this.tablaPrincipal);
            this.splitContenedor.Size = new System.Drawing.Size(1240, 708);
            this.splitContenedor.SplitterDistance = 250;
            this.splitContenedor.SplitterWidth = 1;
            this.splitContenedor.TabIndex = 1;
            //
            // panelLateral
            //
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47)))));
            this.panelLateral.Controls.Add(this.lblLogoApp);
            this.panelLateral.Controls.Add(this.lblNombreApp);
            this.panelLateral.Controls.Add(this.lblSubtituloApp);
            this.panelLateral.Controls.Add(this.panelLineaLateral);
            this.panelLateral.Controls.Add(this.lblSeccionNavegacion);
            this.panelLateral.Controls.Add(this.treeViewNavegacion);
            this.panelLateral.Controls.Add(this.lblPieLateral);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(250, 708);
            this.panelLateral.TabIndex = 0;
            //
            // lblLogoApp
            //
            this.lblLogoApp.AutoSize = true;
            this.lblLogoApp.BackColor = System.Drawing.Color.Transparent;
            this.lblLogoApp.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblLogoApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.lblLogoApp.Location = new System.Drawing.Point(18, 16);
            this.lblLogoApp.Name = "lblLogoApp";
            this.lblLogoApp.Size = new System.Drawing.Size(36, 37);
            this.lblLogoApp.TabIndex = 0;
            this.lblLogoApp.Text = "✈";
            //
            // lblNombreApp
            //
            this.lblNombreApp.AutoSize = true;
            this.lblNombreApp.Font = new System.Drawing.Font("Segoe UI Semibold", 11.5F);
            this.lblNombreApp.ForeColor = System.Drawing.Color.White;
            this.lblNombreApp.Location = new System.Drawing.Point(70, 22);
            this.lblNombreApp.Name = "lblNombreApp";
            this.lblNombreApp.Size = new System.Drawing.Size(148, 21);
            this.lblNombreApp.TabIndex = 1;
            this.lblNombreApp.Text = "AGENDA DE VUELOS";
            //
            // lblSubtituloApp
            //
            this.lblSubtituloApp.AutoSize = true;
            this.lblSubtituloApp.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtituloApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtituloApp.Location = new System.Drawing.Point(71, 45);
            this.lblSubtituloApp.Name = "lblSubtituloApp";
            this.lblSubtituloApp.Size = new System.Drawing.Size(119, 15);
            this.lblSubtituloApp.TabIndex = 2;
            this.lblSubtituloApp.Text = "Agencia de viajes aérea";
            //
            // panelLineaLateral
            //
            this.panelLineaLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(61)))), ((int)(((byte)(80)))));
            this.panelLineaLateral.Location = new System.Drawing.Point(20, 84);
            this.panelLineaLateral.Name = "panelLineaLateral";
            this.panelLineaLateral.Size = new System.Drawing.Size(210, 1);
            this.panelLineaLateral.TabIndex = 3;
            //
            // lblSeccionNavegacion
            //
            this.lblSeccionNavegacion.AutoSize = true;
            this.lblSeccionNavegacion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblSeccionNavegacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(134)))), ((int)(((byte)(156)))));
            this.lblSeccionNavegacion.Location = new System.Drawing.Point(22, 104);
            this.lblSeccionNavegacion.Name = "lblSeccionNavegacion";
            this.lblSeccionNavegacion.Size = new System.Drawing.Size(87, 13);
            this.lblSeccionNavegacion.TabIndex = 4;
            this.lblSeccionNavegacion.Text = "NAVEGACIÓN";
            //
            // treeViewNavegacion
            //
            this.treeViewNavegacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewNavegacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47)))));
            this.treeViewNavegacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewNavegacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.treeViewNavegacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(227)))));
            this.treeViewNavegacion.FullRowSelect = true;
            this.treeViewNavegacion.HideSelection = false;
            this.treeViewNavegacion.Indent = 14;
            this.treeViewNavegacion.ItemHeight = 34;
            this.treeViewNavegacion.Location = new System.Drawing.Point(8, 132);
            this.treeViewNavegacion.Name = "treeViewNavegacion";
            treeNodePanelPrincipal.Name = "treeNodePanelPrincipal";
            treeNodePanelPrincipal.Tag = "dashboard";
            treeNodePanelPrincipal.Text = "Panel principal";
            treeNodeVuelos.Name = "treeNodeVuelos";
            treeNodeVuelos.Tag = "vuelos";
            treeNodeVuelos.Text = "Vuelos";
            treeNodePasajeros.Name = "treeNodePasajeros";
            treeNodePasajeros.Tag = "pasajeros";
            treeNodePasajeros.Text = "Pasajeros";
            treeNodeReservaciones.Name = "treeNodeReservaciones";
            treeNodeReservaciones.Tag = "reservaciones";
            treeNodeReservaciones.Text = "Reservaciones";
            treeNodeReportes.Name = "treeNodeReportes";
            treeNodeReportes.Tag = "reportes";
            treeNodeReportes.Text = "Reportes";
            treeNodeAcercaDe.Name = "treeNodeAcercaDe";
            treeNodeAcercaDe.Tag = "acerca";
            treeNodeAcercaDe.Text = "Acerca de";
            this.treeViewNavegacion.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNodePanelPrincipal,
            treeNodeVuelos,
            treeNodePasajeros,
            treeNodeReservaciones,
            treeNodeReportes,
            treeNodeAcercaDe});
            this.treeViewNavegacion.ShowLines = false;
            this.treeViewNavegacion.ShowPlusMinus = false;
            this.treeViewNavegacion.ShowRootLines = false;
            this.treeViewNavegacion.Size = new System.Drawing.Size(234, 496);
            this.treeViewNavegacion.TabIndex = 5;
            this.treeViewNavegacion.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNavegacion_AfterSelect);
            //
            // lblPieLateral
            //
            this.lblPieLateral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPieLateral.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPieLateral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(113)))), ((int)(((byte)(134)))));
            this.lblPieLateral.Location = new System.Drawing.Point(22, 646);
            this.lblPieLateral.Name = "lblPieLateral";
            this.lblPieLateral.Size = new System.Drawing.Size(206, 44);
            this.lblPieLateral.TabIndex = 6;
            this.lblPieLateral.Text = "Proyecto Programación II\r\nFase 6 · Pasajeros";
            //
            // tablaPrincipal
            //
            this.tablaPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.tablaPrincipal.ColumnCount = 1;
            this.tablaPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaPrincipal.Controls.Add(this.panelEncabezado, 0, 0);
            this.tablaPrincipal.Controls.Add(this.panelContenido, 0, 1);
            this.tablaPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tablaPrincipal.Name = "tablaPrincipal";
            this.tablaPrincipal.RowCount = 2;
            this.tablaPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tablaPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaPrincipal.Size = new System.Drawing.Size(989, 708);
            this.tablaPrincipal.TabIndex = 0;
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.White;
            this.panelEncabezado.Controls.Add(this.lblModuloTitulo);
            this.panelEncabezado.Controls.Add(this.lblModuloDescripcion);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Margin = new System.Windows.Forms.Padding(0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(989, 86);
            this.panelEncabezado.TabIndex = 0;
            //
            // lblModuloTitulo
            //
            this.lblModuloTitulo.AutoSize = true;
            this.lblModuloTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 15F);
            this.lblModuloTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.lblModuloTitulo.Location = new System.Drawing.Point(28, 18);
            this.lblModuloTitulo.Name = "lblModuloTitulo";
            this.lblModuloTitulo.Size = new System.Drawing.Size(167, 28);
            this.lblModuloTitulo.TabIndex = 0;
            this.lblModuloTitulo.Text = "Panel principal";
            //
            // lblModuloDescripcion
            //
            this.lblModuloDescripcion.AutoSize = true;
            this.lblModuloDescripcion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblModuloDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(122)))), ((int)(((byte)(140)))));
            this.lblModuloDescripcion.Location = new System.Drawing.Point(30, 50);
            this.lblModuloDescripcion.Name = "lblModuloDescripcion";
            this.lblModuloDescripcion.Size = new System.Drawing.Size(287, 17);
            this.lblModuloDescripcion.TabIndex = 1;
            this.lblModuloDescripcion.Text = "Resumen general de la operación de la agencia";
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.panelContenido.Controls.Add(this.panelBienvenida);
            this.panelContenido.Controls.Add(this.panelMarcador);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 86);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(24, 18, 24, 24);
            this.panelContenido.Size = new System.Drawing.Size(989, 622);
            this.panelContenido.TabIndex = 1;
            //
            // panelBienvenida
            //
            this.panelBienvenida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.panelBienvenida.Controls.Add(this.tablaBienvenida);
            this.panelBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBienvenida.Location = new System.Drawing.Point(24, 18);
            this.panelBienvenida.Name = "panelBienvenida";
            this.panelBienvenida.Size = new System.Drawing.Size(941, 580);
            this.panelBienvenida.TabIndex = 0;
            //
            // tablaBienvenida
            //
            this.tablaBienvenida.ColumnCount = 1;
            this.tablaBienvenida.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaBienvenida.Controls.Add(this.lblBienvenidaTitulo, 0, 0);
            this.tablaBienvenida.Controls.Add(this.lblBienvenidaTexto, 0, 1);
            this.tablaBienvenida.Controls.Add(this.flowModulos, 0, 2);
            this.tablaBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaBienvenida.Location = new System.Drawing.Point(0, 0);
            this.tablaBienvenida.Name = "tablaBienvenida";
            this.tablaBienvenida.RowCount = 3;
            this.tablaBienvenida.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tablaBienvenida.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tablaBienvenida.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaBienvenida.Size = new System.Drawing.Size(941, 580);
            this.tablaBienvenida.TabIndex = 0;
            //
            // lblBienvenidaTitulo
            //
            this.lblBienvenidaTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienvenidaTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 15F);
            this.lblBienvenidaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.lblBienvenidaTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblBienvenidaTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.lblBienvenidaTitulo.Name = "lblBienvenidaTitulo";
            this.lblBienvenidaTitulo.Size = new System.Drawing.Size(941, 48);
            this.lblBienvenidaTitulo.TabIndex = 0;
            this.lblBienvenidaTitulo.Text = "Bienvenido a Agenda de Vuelos";
            this.lblBienvenidaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBienvenidaTexto
            //
            this.lblBienvenidaTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienvenidaTexto.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblBienvenidaTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(101)))), ((int)(((byte)(122)))));
            this.lblBienvenidaTexto.Location = new System.Drawing.Point(0, 48);
            this.lblBienvenidaTexto.Margin = new System.Windows.Forms.Padding(0);
            this.lblBienvenidaTexto.Name = "lblBienvenidaTexto";
            this.lblBienvenidaTexto.Size = new System.Drawing.Size(941, 64);
            this.lblBienvenidaTexto.TabIndex = 1;
            this.lblBienvenidaTexto.Text = "Sistema de gestión para una agencia de viajes aérea: administre vuelos, pasajeros y\r\nreservaciones desde un solo lugar. Use el árbol de navegación de la izquierda o el\r\nmenú superior para recorrer los módulos.";
            //
            // flowModulos
            //
            this.flowModulos.AutoScroll = true;
            this.flowModulos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.flowModulos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowModulos.Location = new System.Drawing.Point(0, 112);
            this.flowModulos.Margin = new System.Windows.Forms.Padding(0);
            this.flowModulos.Name = "flowModulos";
            this.flowModulos.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flowModulos.Size = new System.Drawing.Size(941, 468);
            this.flowModulos.TabIndex = 2;
            //
            // panelMarcador
            //
            this.panelMarcador.BackColor = System.Drawing.Color.White;
            this.panelMarcador.Controls.Add(this.lblMarcadorTitulo);
            this.panelMarcador.Controls.Add(this.lblMarcadorTexto);
            this.panelMarcador.Controls.Add(this.lblMarcadorFase);
            this.panelMarcador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMarcador.Location = new System.Drawing.Point(24, 18);
            this.panelMarcador.Name = "panelMarcador";
            this.panelMarcador.Size = new System.Drawing.Size(941, 580);
            this.panelMarcador.TabIndex = 1;
            this.panelMarcador.Visible = false;
            //
            // lblMarcadorTitulo
            //
            this.lblMarcadorTitulo.AutoSize = true;
            this.lblMarcadorTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 15F);
            this.lblMarcadorTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.lblMarcadorTitulo.Location = new System.Drawing.Point(32, 30);
            this.lblMarcadorTitulo.Name = "lblMarcadorTitulo";
            this.lblMarcadorTitulo.Size = new System.Drawing.Size(172, 28);
            this.lblMarcadorTitulo.TabIndex = 0;
            this.lblMarcadorTitulo.Text = "Módulo";
            //
            // lblMarcadorTexto
            //
            this.lblMarcadorTexto.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblMarcadorTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(101)))), ((int)(((byte)(122)))));
            this.lblMarcadorTexto.Location = new System.Drawing.Point(34, 72);
            this.lblMarcadorTexto.Name = "lblMarcadorTexto";
            this.lblMarcadorTexto.Size = new System.Drawing.Size(560, 56);
            this.lblMarcadorTexto.TabIndex = 1;
            this.lblMarcadorTexto.Text = "Descripción del módulo.";
            //
            // lblMarcadorFase
            //
            this.lblMarcadorFase.AutoSize = true;
            this.lblMarcadorFase.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblMarcadorFase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.lblMarcadorFase.Location = new System.Drawing.Point(34, 136);
            this.lblMarcadorFase.Name = "lblMarcadorFase";
            this.lblMarcadorFase.Size = new System.Drawing.Size(140, 15);
            this.lblMarcadorFase.TabIndex = 2;
            this.lblMarcadorFase.Text = "Se habilita en la Fase 4";
            //
            // timerReloj
            //
            this.timerReloj.Enabled = true;
            this.timerReloj.Interval = 1000;
            this.timerReloj.Tick += new System.EventHandler(this.timerReloj_Tick);
            //
            // FrmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1240, 762);
            this.Controls.Add(this.splitContenedor);
            this.Controls.Add(this.statusStripPrincipal);
            this.Controls.Add(this.menuStripPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainMenuStrip = this.menuStripPrincipal;
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agenda de Vuelos — Agencia de Viajes Aérea";
            this.menuStripPrincipal.ResumeLayout(false);
            this.menuStripPrincipal.PerformLayout();
            this.statusStripPrincipal.ResumeLayout(false);
            this.statusStripPrincipal.PerformLayout();
            this.splitContenedor.Panel1.ResumeLayout(false);
            this.splitContenedor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContenedor)).EndInit();
            this.splitContenedor.ResumeLayout(false);
            this.panelLateral.ResumeLayout(false);
            this.panelLateral.PerformLayout();
            this.tablaPrincipal.ResumeLayout(false);
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.panelBienvenida.ResumeLayout(false);
            this.tablaBienvenida.ResumeLayout(false);
            this.panelMarcador.ResumeLayout(false);
            this.panelMarcador.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripPrincipal;
        private System.Windows.Forms.ToolStripMenuItem menuArchivo;
        private System.Windows.Forms.ToolStripMenuItem mnuNuevoVuelo;
        private System.Windows.Forms.ToolStripMenuItem mnuNuevoPasajero;
        private System.Windows.Forms.ToolStripMenuItem mnuNuevaReservacion;
        private System.Windows.Forms.ToolStripSeparator sepArchivo;
        private System.Windows.Forms.ToolStripMenuItem mnuSalir;
        private System.Windows.Forms.ToolStripMenuItem menuVer;
        private System.Windows.Forms.ToolStripMenuItem mnuVerDashboard;
        private System.Windows.Forms.ToolStripMenuItem mnuVerVuelos;
        private System.Windows.Forms.ToolStripMenuItem mnuVerPasajeros;
        private System.Windows.Forms.ToolStripMenuItem mnuVerReservaciones;
        private System.Windows.Forms.ToolStripMenuItem mnuVerReportes;
        private System.Windows.Forms.ToolStripMenuItem menuHerramientas;
        private System.Windows.Forms.ToolStripMenuItem mnuImportar;
        private System.Windows.Forms.ToolStripMenuItem mnuExportar;
        private System.Windows.Forms.ToolStripSeparator sepHerramientas;
        private System.Windows.Forms.ToolStripMenuItem mnuColorAcento;
        private System.Windows.Forms.ToolStripMenuItem menuAyuda;
        private System.Windows.Forms.ToolStripMenuItem mnuAcercaDe;
        private System.Windows.Forms.StatusStrip statusStripPrincipal;
        private System.Windows.Forms.ToolStripStatusLabel lblEstado;
        private System.Windows.Forms.ToolStripStatusLabel lblRegistros;
        private System.Windows.Forms.ToolStripStatusLabel lblEspacio;
        private System.Windows.Forms.ToolStripStatusLabel lblFecha;
        private System.Windows.Forms.ToolStripStatusLabel lblVersion;
        private System.Windows.Forms.SplitContainer splitContenedor;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.Label lblNombreApp;
        private System.Windows.Forms.Label lblSubtituloApp;
        private System.Windows.Forms.Panel panelLineaLateral;
        private System.Windows.Forms.Label lblSeccionNavegacion;
        private System.Windows.Forms.TreeView treeViewNavegacion;
        private System.Windows.Forms.Label lblPieLateral;
        private System.Windows.Forms.TableLayoutPanel tablaPrincipal;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Label lblModuloTitulo;
        private System.Windows.Forms.Label lblModuloDescripcion;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Panel panelBienvenida;
        private System.Windows.Forms.TableLayoutPanel tablaBienvenida;
        private System.Windows.Forms.Label lblBienvenidaTitulo;
        private System.Windows.Forms.Label lblBienvenidaTexto;
        private System.Windows.Forms.FlowLayoutPanel flowModulos;
        private System.Windows.Forms.Panel panelMarcador;
        private System.Windows.Forms.Label lblMarcadorTitulo;
        private System.Windows.Forms.Label lblMarcadorTexto;
        private System.Windows.Forms.Label lblMarcadorFase;
        private System.Windows.Forms.Timer timerReloj;
    }
}
