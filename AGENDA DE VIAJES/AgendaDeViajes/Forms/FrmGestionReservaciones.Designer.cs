namespace AgendaDeViajes.Forms
{
    partial class FrmGestionReservaciones
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelFondo = new System.Windows.Forms.Panel();
            this.tablaRaiz = new System.Windows.Forms.TableLayoutPanel();

            // Panel Superior (Acciones y Filtros)
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelFranja = new System.Windows.Forms.Panel();
            this.tablaAccionesFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.flowAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblContador = new System.Windows.Forms.Label();
            this.flowFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblFiltroEstado = new System.Windows.Forms.Label();
            this.cboFiltroEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltroTarifa = new System.Windows.Forms.Label();
            this.cboFiltroTarifa = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();

            // Panel Grid
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvReservaciones = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPasajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAsientos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTarifa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeguro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServicios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblResumenSeleccion = new System.Windows.Forms.Label();

            this.panelFondo.SuspendLayout();
            this.tablaRaiz.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.tablaAccionesFiltros.SuspendLayout();
            this.flowAcciones.SuspendLayout();
            this.flowFiltros.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservaciones)).BeginInit();
            this.panelResumen.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelFondo
            // 
            this.panelFondo.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.panelFondo.Controls.Add(this.tablaRaiz);
            this.panelFondo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFondo.Location = new System.Drawing.Point(0, 0);
            this.panelFondo.Margin = new System.Windows.Forms.Padding(0);
            this.panelFondo.Name = "panelFondo";
            this.panelFondo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelFondo.Size = new System.Drawing.Size(941, 580);
            this.panelFondo.TabIndex = 0;

            // 
            // tablaRaiz
            // 
            this.tablaRaiz.ColumnCount = 1;
            this.tablaRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaRaiz.Controls.Add(this.panelSuperior, 0, 0);
            this.tablaRaiz.Controls.Add(this.panelGrid, 0, 1);
            this.tablaRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaRaiz.Location = new System.Drawing.Point(0, 0);
            this.tablaRaiz.Name = "tablaRaiz";
            this.tablaRaiz.RowCount = 2;
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaRaiz.Size = new System.Drawing.Size(941, 570);
            this.tablaRaiz.TabIndex = 0;

            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.White;
            this.panelSuperior.Controls.Add(this.tablaAccionesFiltros);
            this.panelSuperior.Controls.Add(this.panelFranja);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(941, 83);
            this.panelSuperior.TabIndex = 0;

            // 
            // panelFranja
            // 
            this.panelFranja.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranja.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFranja.Location = new System.Drawing.Point(0, 0);
            this.panelFranja.Name = "panelFranja";
            this.panelFranja.Size = new System.Drawing.Size(4, 83);
            this.panelFranja.TabIndex = 0;

            // 
            // tablaAccionesFiltros
            // 
            this.tablaAccionesFiltros.ColumnCount = 1;
            this.tablaAccionesFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaAccionesFiltros.Controls.Add(this.flowAcciones, 0, 0);
            this.tablaAccionesFiltros.Controls.Add(this.flowFiltros, 0, 1);
            this.tablaAccionesFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaAccionesFiltros.Location = new System.Drawing.Point(4, 0);
            this.tablaAccionesFiltros.Name = "tablaAccionesFiltros";
            this.tablaAccionesFiltros.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.tablaAccionesFiltros.RowCount = 2;
            this.tablaAccionesFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tablaAccionesFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaAccionesFiltros.Size = new System.Drawing.Size(937, 83);
            this.tablaAccionesFiltros.TabIndex = 1;

            // 
            // flowAcciones
            // 
            this.flowAcciones.Controls.Add(this.btnNuevo);
            this.flowAcciones.Controls.Add(this.btnEditar);
            this.flowAcciones.Controls.Add(this.btnEliminar);
            this.flowAcciones.Controls.Add(this.btnActualizar);
            this.flowAcciones.Controls.Add(this.lblContador);
            this.flowAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowAcciones.Location = new System.Drawing.Point(14, 8);
            this.flowAcciones.Margin = new System.Windows.Forms.Padding(0);
            this.flowAcciones.Name = "flowAcciones";
            this.flowAcciones.Size = new System.Drawing.Size(909, 34);
            this.flowAcciones.TabIndex = 0;
            this.flowAcciones.WrapContents = false;

            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(0, 0);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(145, 28);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "+ Nueva reservación";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(234, 243, 251);
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 220, 240);
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.btnEditar.Location = new System.Drawing.Point(153, 0);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(110, 28);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "✎ Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(250, 200, 200);
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnEliminar.Location = new System.Drawing.Point(271, 0);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(135, 28);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "🗑 Cancelar reserva";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnActualizar.Location = new System.Drawing.Point(389, 0);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(100, 28);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "⟳ Refrescar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // 
            // lblContador
            // 
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblContador.Location = new System.Drawing.Point(507, 0);
            this.lblContador.Margin = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(380, 24);
            this.lblContador.TabIndex = 4;
            this.lblContador.Text = "Cargando reservaciones...";
            this.lblContador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // flowFiltros
            // 
            this.flowFiltros.Controls.Add(this.lblBuscar);
            this.flowFiltros.Controls.Add(this.txtBuscar);
            this.flowFiltros.Controls.Add(this.lblFiltroEstado);
            this.flowFiltros.Controls.Add(this.cboFiltroEstado);
            this.flowFiltros.Controls.Add(this.lblFiltroTarifa);
            this.flowFiltros.Controls.Add(this.cboFiltroTarifa);
            this.flowFiltros.Controls.Add(this.btnLimpiar);
            this.flowFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowFiltros.Location = new System.Drawing.Point(14, 42);
            this.flowFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.flowFiltros.Name = "flowFiltros";
            this.flowFiltros.Size = new System.Drawing.Size(909, 33);
            this.flowFiltros.TabIndex = 1;
            this.flowFiltros.WrapContents = false;

            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblBuscar.Location = new System.Drawing.Point(0, 6);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(46, 15);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";

            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtBuscar.Location = new System.Drawing.Point(53, 3);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "Código, vuelo, pasajero o documento...";
            this.txtBuscar.Size = new System.Drawing.Size(240, 23);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // 
            // lblFiltroEstado
            // 
            this.lblFiltroEstado.AutoSize = true;
            this.lblFiltroEstado.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFiltroEstado.Location = new System.Drawing.Point(310, 6);
            this.lblFiltroEstado.Margin = new System.Windows.Forms.Padding(3, 6, 4, 0);
            this.lblFiltroEstado.Name = "lblFiltroEstado";
            this.lblFiltroEstado.Size = new System.Drawing.Size(46, 15);
            this.lblFiltroEstado.TabIndex = 2;
            this.lblFiltroEstado.Text = "Estado:";

            // 
            // cboFiltroEstado
            // 
            this.cboFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboFiltroEstado.FormattingEnabled = true;
            this.cboFiltroEstado.Location = new System.Drawing.Point(363, 3);
            this.cboFiltroEstado.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboFiltroEstado.Name = "cboFiltroEstado";
            this.cboFiltroEstado.Size = new System.Drawing.Size(140, 21);
            this.cboFiltroEstado.TabIndex = 3;
            this.cboFiltroEstado.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);

            // 
            // lblFiltroTarifa
            // 
            this.lblFiltroTarifa.AutoSize = true;
            this.lblFiltroTarifa.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFiltroTarifa.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFiltroTarifa.Location = new System.Drawing.Point(520, 6);
            this.lblFiltroTarifa.Margin = new System.Windows.Forms.Padding(3, 6, 4, 0);
            this.lblFiltroTarifa.Name = "lblFiltroTarifa";
            this.lblFiltroTarifa.Size = new System.Drawing.Size(40, 15);
            this.lblFiltroTarifa.TabIndex = 4;
            this.lblFiltroTarifa.Text = "Tarifa:";

            // 
            // cboFiltroTarifa
            // 
            this.cboFiltroTarifa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroTarifa.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboFiltroTarifa.FormattingEnabled = true;
            this.cboFiltroTarifa.Location = new System.Drawing.Point(567, 3);
            this.cboFiltroTarifa.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboFiltroTarifa.Name = "cboFiltroTarifa";
            this.cboFiltroTarifa.Size = new System.Drawing.Size(130, 21);
            this.cboFiltroTarifa.TabIndex = 5;
            this.cboFiltroTarifa.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);

            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.btnLimpiar.Location = new System.Drawing.Point(714, 3);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 23);
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Controls.Add(this.dgvReservaciones);
            this.panelGrid.Controls.Add(this.panelResumen);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 95);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(941, 475);
            this.panelGrid.TabIndex = 1;

            // 
            // dgvReservaciones
            // 
            this.dgvReservaciones.AllowUserToAddRows = false;
            this.dgvReservaciones.AllowUserToDeleteRows = false;
            this.dgvReservaciones.AllowUserToResizeRows = false;
            this.dgvReservaciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvReservaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReservaciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReservaciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvReservaciones.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.dgvReservaciones.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.dgvReservaciones.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvReservaciones.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.dgvReservaciones.ColumnHeadersHeight = 36;
            this.dgvReservaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReservaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colVuelo,
            this.colPasajero,
            this.colFecha,
            this.colAsientos,
            this.colTarifa,
            this.colSeguro,
            this.colServicios,
            this.colTotal,
            this.colEstado});
            this.dgvReservaciones.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvReservaciones.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvReservaciones.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.dgvReservaciones.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dgvReservaciones.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(234, 243, 251);
            this.dgvReservaciones.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.dgvReservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReservaciones.EnableHeadersVisualStyles = false;
            this.dgvReservaciones.GridColor = System.Drawing.Color.FromArgb(232, 237, 242);
            this.dgvReservaciones.Location = new System.Drawing.Point(0, 0);
            this.dgvReservaciones.MultiSelect = false;
            this.dgvReservaciones.Name = "dgvReservaciones";
            this.dgvReservaciones.ReadOnly = true;
            this.dgvReservaciones.RowHeadersVisible = false;
            this.dgvReservaciones.RowTemplate.Height = 32;
            this.dgvReservaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReservaciones.Size = new System.Drawing.Size(941, 443);
            this.dgvReservaciones.TabIndex = 0;
            this.dgvReservaciones.SelectionChanged += new System.EventHandler(this.dgvReservaciones_SelectionChanged);
            this.dgvReservaciones.DoubleClick += new System.EventHandler(this.dgvReservaciones_DoubleClick);

            // 
            // colCodigo
            // 
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.MinimumWidth = 85;
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            this.colCodigo.Width = 90;

            // 
            // colVuelo
            // 
            this.colVuelo.HeaderText = "Vuelo / Ruta";
            this.colVuelo.MinimumWidth = 120;
            this.colVuelo.Name = "colVuelo";
            this.colVuelo.ReadOnly = true;
            this.colVuelo.Width = 140;

            // 
            // colPasajero
            // 
            this.colPasajero.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPasajero.FillWeight = 120F;
            this.colPasajero.HeaderText = "Pasajero titular";
            this.colPasajero.MinimumWidth = 130;
            this.colPasajero.Name = "colPasajero";
            this.colPasajero.ReadOnly = true;

            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha reserva";
            this.colFecha.MinimumWidth = 100;
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            this.colFecha.Width = 110;

            // 
            // colAsientos
            // 
            this.colAsientos.HeaderText = "Asientos";
            this.colAsientos.MinimumWidth = 60;
            this.colAsientos.Name = "colAsientos";
            this.colAsientos.ReadOnly = true;
            this.colAsientos.Width = 65;

            // 
            // colTarifa
            // 
            this.colTarifa.HeaderText = "Tarifa";
            this.colTarifa.MinimumWidth = 80;
            this.colTarifa.Name = "colTarifa";
            this.colTarifa.ReadOnly = true;
            this.colTarifa.Width = 85;

            // 
            // colSeguro
            // 
            this.colSeguro.HeaderText = "Seguro";
            this.colSeguro.MinimumWidth = 60;
            this.colSeguro.Name = "colSeguro";
            this.colSeguro.ReadOnly = true;
            this.colSeguro.Width = 65;

            // 
            // colServicios
            // 
            this.colServicios.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colServicios.FillWeight = 100F;
            this.colServicios.HeaderText = "Servicios";
            this.colServicios.MinimumWidth = 100;
            this.colServicios.Name = "colServicios";
            this.colServicios.ReadOnly = true;

            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Total (USD)";
            this.colTotal.MinimumWidth = 90;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 95;

            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 85;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 90;

            // 
            // panelResumen
            // 
            this.panelResumen.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResumen.Controls.Add(this.lblResumenSeleccion);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelResumen.Location = new System.Drawing.Point(0, 443);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.panelResumen.Size = new System.Drawing.Size(941, 32);
            this.panelResumen.TabIndex = 1;

            // 
            // lblResumenSeleccion
            // 
            this.lblResumenSeleccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResumenSeleccion.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblResumenSeleccion.ForeColor = System.Drawing.Color.FromArgb(85, 101, 122);
            this.lblResumenSeleccion.Location = new System.Drawing.Point(12, 6);
            this.lblResumenSeleccion.Name = "lblResumenSeleccion";
            this.lblResumenSeleccion.Size = new System.Drawing.Size(915, 18);
            this.lblResumenSeleccion.TabIndex = 0;
            this.lblResumenSeleccion.Text = "Seleccione una reservación de la lista para consultar su detalle.";
            this.lblResumenSeleccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // FrmGestionReservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(941, 580);
            this.Controls.Add(this.panelFondo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmGestionReservaciones";
            this.Text = "Gestión de Reservaciones";
            this.panelFondo.ResumeLayout(false);
            this.tablaRaiz.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.tablaAccionesFiltros.ResumeLayout(false);
            this.flowAcciones.ResumeLayout(false);
            this.flowFiltros.ResumeLayout(false);
            this.flowFiltros.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservaciones)).EndInit();
            this.panelResumen.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFondo;
        private System.Windows.Forms.TableLayoutPanel tablaRaiz;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelFranja;
        private System.Windows.Forms.TableLayoutPanel tablaAccionesFiltros;
        private System.Windows.Forms.FlowLayoutPanel flowAcciones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.FlowLayoutPanel flowFiltros;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblFiltroEstado;
        private System.Windows.Forms.ComboBox cboFiltroEstado;
        private System.Windows.Forms.Label lblFiltroTarifa;
        private System.Windows.Forms.ComboBox cboFiltroTarifa;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvReservaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPasajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAsientos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarifa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeguro;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServicios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.Label lblResumenSeleccion;
    }
}

