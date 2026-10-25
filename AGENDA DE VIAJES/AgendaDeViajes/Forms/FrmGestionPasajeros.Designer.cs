namespace AgendaDeViajes.Forms
{
    partial class FrmGestionPasajeros
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
            this.lblFiltroCategoria = new System.Windows.Forms.Label();
            this.cboFiltroCategoria = new System.Windows.Forms.ComboBox();
            this.lblFiltroNacionalidad = new System.Windows.Forms.Label();
            this.cboFiltroNacionalidad = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();

            // Panel Grid
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvPasajeros = new System.Windows.Forms.DataGridView();
            this.colDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNacionalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAsistencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServicios = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblResumenSeleccion = new System.Windows.Forms.Label();

            this.panelFondo.SuspendLayout();
            this.tablaRaiz.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.tablaAccionesFiltros.SuspendLayout();
            this.flowAcciones.SuspendLayout();
            this.flowFiltros.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasajeros)).BeginInit();
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
            this.btnNuevo.Size = new System.Drawing.Size(130, 28);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "+ Nuevo pasajero";
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
            this.btnEditar.Location = new System.Drawing.Point(138, 0);
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
            this.btnEliminar.Location = new System.Drawing.Point(256, 0);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 28);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "🗑 Eliminar";
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
            this.btnActualizar.Location = new System.Drawing.Point(374, 0);
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
            this.lblContador.Location = new System.Drawing.Point(492, 0);
            this.lblContador.Margin = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(380, 24);
            this.lblContador.TabIndex = 4;
            this.lblContador.Text = "Cargando pasajeros...";
            this.lblContador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // flowFiltros
            // 
            this.flowFiltros.Controls.Add(this.lblBuscar);
            this.flowFiltros.Controls.Add(this.txtBuscar);
            this.flowFiltros.Controls.Add(this.lblFiltroCategoria);
            this.flowFiltros.Controls.Add(this.cboFiltroCategoria);
            this.flowFiltros.Controls.Add(this.lblFiltroNacionalidad);
            this.flowFiltros.Controls.Add(this.cboFiltroNacionalidad);
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
            this.txtBuscar.PlaceholderText = "Documento, nombres, apellidos, correo o nacionalidad...";
            this.txtBuscar.Size = new System.Drawing.Size(240, 23);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // 
            // lblFiltroCategoria
            // 
            this.lblFiltroCategoria.AutoSize = true;
            this.lblFiltroCategoria.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFiltroCategoria.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFiltroCategoria.Location = new System.Drawing.Point(310, 6);
            this.lblFiltroCategoria.Margin = new System.Windows.Forms.Padding(3, 6, 4, 0);
            this.lblFiltroCategoria.Name = "lblFiltroCategoria";
            this.lblFiltroCategoria.Size = new System.Drawing.Size(60, 15);
            this.lblFiltroCategoria.TabIndex = 2;
            this.lblFiltroCategoria.Text = "Categoría:";

            // 
            // cboFiltroCategoria
            // 
            this.cboFiltroCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroCategoria.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboFiltroCategoria.FormattingEnabled = true;
            this.cboFiltroCategoria.Location = new System.Drawing.Point(377, 3);
            this.cboFiltroCategoria.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboFiltroCategoria.Name = "cboFiltroCategoria";
            this.cboFiltroCategoria.Size = new System.Drawing.Size(140, 21);
            this.cboFiltroCategoria.TabIndex = 3;
            this.cboFiltroCategoria.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);

            // 
            // lblFiltroNacionalidad
            // 
            this.lblFiltroNacionalidad.AutoSize = true;
            this.lblFiltroNacionalidad.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFiltroNacionalidad.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFiltroNacionalidad.Location = new System.Drawing.Point(534, 6);
            this.lblFiltroNacionalidad.Margin = new System.Windows.Forms.Padding(3, 6, 4, 0);
            this.lblFiltroNacionalidad.Name = "lblFiltroNacionalidad";
            this.lblFiltroNacionalidad.Size = new System.Drawing.Size(79, 15);
            this.lblFiltroNacionalidad.TabIndex = 4;
            this.lblFiltroNacionalidad.Text = "Nacionalidad:";

            // 
            // cboFiltroNacionalidad
            // 
            this.cboFiltroNacionalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroNacionalidad.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboFiltroNacionalidad.FormattingEnabled = true;
            this.cboFiltroNacionalidad.Location = new System.Drawing.Point(620, 3);
            this.cboFiltroNacionalidad.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboFiltroNacionalidad.Name = "cboFiltroNacionalidad";
            this.cboFiltroNacionalidad.Size = new System.Drawing.Size(150, 21);
            this.cboFiltroNacionalidad.TabIndex = 5;
            this.cboFiltroNacionalidad.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);

            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.btnLimpiar.Location = new System.Drawing.Point(787, 3);
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
            this.panelGrid.Controls.Add(this.dgvPasajeros);
            this.panelGrid.Controls.Add(this.panelResumen);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 95);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(941, 475);
            this.panelGrid.TabIndex = 1;

            // 
            // dgvPasajeros
            // 
            this.dgvPasajeros.AllowUserToAddRows = false;
            this.dgvPasajeros.AllowUserToDeleteRows = false;
            this.dgvPasajeros.AllowUserToResizeRows = false;
            this.dgvPasajeros.BackgroundColor = System.Drawing.Color.White;
            this.dgvPasajeros.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPasajeros.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPasajeros.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPasajeros.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.dgvPasajeros.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.dgvPasajeros.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPasajeros.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.dgvPasajeros.ColumnHeadersHeight = 36;
            this.dgvPasajeros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPasajeros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDocumento,
            this.colNombreCompleto,
            this.colEmail,
            this.colTelefono,
            this.colFechaNacimiento,
            this.colEdad,
            this.colNacionalidad,
            this.colCategoria,
            this.colAsistencia,
            this.colServicios});
            this.dgvPasajeros.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvPasajeros.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvPasajeros.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.dgvPasajeros.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dgvPasajeros.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(234, 243, 251);
            this.dgvPasajeros.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.dgvPasajeros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPasajeros.EnableHeadersVisualStyles = false;
            this.dgvPasajeros.GridColor = System.Drawing.Color.FromArgb(232, 237, 242);
            this.dgvPasajeros.Location = new System.Drawing.Point(0, 0);
            this.dgvPasajeros.MultiSelect = false;
            this.dgvPasajeros.Name = "dgvPasajeros";
            this.dgvPasajeros.ReadOnly = true;
            this.dgvPasajeros.RowHeadersVisible = false;
            this.dgvPasajeros.RowTemplate.Height = 32;
            this.dgvPasajeros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPasajeros.Size = new System.Drawing.Size(941, 443);
            this.dgvPasajeros.TabIndex = 0;
            this.dgvPasajeros.SelectionChanged += new System.EventHandler(this.dgvPasajeros_SelectionChanged);
            this.dgvPasajeros.DoubleClick += new System.EventHandler(this.dgvPasajeros_DoubleClick);

            // 
            // colDocumento
            // 
            this.colDocumento.HeaderText = "Documento";
            this.colDocumento.MinimumWidth = 90;
            this.colDocumento.Name = "colDocumento";
            this.colDocumento.ReadOnly = true;
            this.colDocumento.Width = 100;

            // 
            // colNombreCompleto
            // 
            this.colNombreCompleto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNombreCompleto.FillWeight = 130F;
            this.colNombreCompleto.HeaderText = "Nombre completo";
            this.colNombreCompleto.MinimumWidth = 130;
            this.colNombreCompleto.Name = "colNombreCompleto";
            this.colNombreCompleto.ReadOnly = true;

            // 
            // colEmail
            // 
            this.colEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmail.FillWeight = 110F;
            this.colEmail.HeaderText = "Correo electrónico";
            this.colEmail.MinimumWidth = 120;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;

            // 
            // colTelefono
            // 
            this.colTelefono.HeaderText = "Teléfono";
            this.colTelefono.MinimumWidth = 85;
            this.colTelefono.Name = "colTelefono";
            this.colTelefono.ReadOnly = true;
            this.colTelefono.Width = 100;

            // 
            // colFechaNacimiento
            // 
            this.colFechaNacimiento.HeaderText = "Fecha nac.";
            this.colFechaNacimiento.MinimumWidth = 80;
            this.colFechaNacimiento.Name = "colFechaNacimiento";
            this.colFechaNacimiento.ReadOnly = true;
            this.colFechaNacimiento.Width = 85;

            // 
            // colEdad
            // 
            this.colEdad.HeaderText = "Edad";
            this.colEdad.MinimumWidth = 50;
            this.colEdad.Name = "colEdad";
            this.colEdad.ReadOnly = true;
            this.colEdad.Width = 55;

            // 
            // colNacionalidad
            // 
            this.colNacionalidad.HeaderText = "Nacionalidad";
            this.colNacionalidad.MinimumWidth = 90;
            this.colNacionalidad.Name = "colNacionalidad";
            this.colNacionalidad.ReadOnly = true;
            this.colNacionalidad.Width = 105;

            // 
            // colCategoria
            // 
            this.colCategoria.HeaderText = "Categoría";
            this.colCategoria.MinimumWidth = 70;
            this.colCategoria.Name = "colCategoria";
            this.colCategoria.ReadOnly = true;
            this.colCategoria.Width = 80;

            // 
            // colAsistencia
            // 
            this.colAsistencia.HeaderText = "Asistencia esp.";
            this.colAsistencia.MinimumWidth = 90;
            this.colAsistencia.Name = "colAsistencia";
            this.colAsistencia.ReadOnly = true;
            this.colAsistencia.Width = 95;

            // 
            // colServicios
            // 
            this.colServicios.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colServicios.FillWeight = 100F;
            this.colServicios.HeaderText = "Servicios preferidos";
            this.colServicios.MinimumWidth = 100;
            this.colServicios.Name = "colServicios";
            this.colServicios.ReadOnly = true;

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
            this.lblResumenSeleccion.Text = "Seleccione un pasajero de la lista para ver sus detalles.";
            this.lblResumenSeleccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // FrmGestionPasajeros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(941, 580);
            this.Controls.Add(this.panelFondo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmGestionPasajeros";
            this.Text = "Gestión de Pasajeros";
            this.panelFondo.ResumeLayout(false);
            this.tablaRaiz.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.tablaAccionesFiltros.ResumeLayout(false);
            this.flowAcciones.ResumeLayout(false);
            this.flowFiltros.ResumeLayout(false);
            this.flowFiltros.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasajeros)).EndInit();
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
        private System.Windows.Forms.Label lblFiltroCategoria;
        private System.Windows.Forms.ComboBox cboFiltroCategoria;
        private System.Windows.Forms.Label lblFiltroNacionalidad;
        private System.Windows.Forms.ComboBox cboFiltroNacionalidad;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvPasajeros;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaNacimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNacionalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAsistencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServicios;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.Label lblResumenSeleccion;
    }
}

