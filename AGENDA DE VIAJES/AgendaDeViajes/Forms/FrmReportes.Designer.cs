namespace AgendaDeViajes.Forms
{
    partial class FrmReportes
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

            // Panel Superior: Tarjetas de resumen métrico (KPIs)
            this.panelKpis = new System.Windows.Forms.Panel();
            this.flowKpis = new System.Windows.Forms.FlowLayoutPanel();

            // Panel de Control: Selector de reporte y botones de exportación
            this.panelControl = new System.Windows.Forms.Panel();
            this.panelFranja = new System.Windows.Forms.Panel();
            this.tablaControl = new System.Windows.Forms.TableLayoutPanel();
            this.flowSelector = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTipoReporte = new System.Windows.Forms.Label();
            this.cboTipoReporte = new System.Windows.Forms.ComboBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.flowExportacion = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExportarCsv = new System.Windows.Forms.Button();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.flowFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFiltroFechas = new System.Windows.Forms.Label();
            this.chkFiltrarFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblSeparadorA = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.lblFiltroSecundario = new System.Windows.Forms.Label();
            this.cboFiltroSecundario = new System.Windows.Forms.ComboBox();
            this.btnRestablecerFiltros = new System.Windows.Forms.Button();

            // Panel Grid: Resultados tabulares
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvReportes = new System.Windows.Forms.DataGridView();
            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblResumenReporte = new System.Windows.Forms.Label();

            this.panelFondo.SuspendLayout();
            this.tablaRaiz.SuspendLayout();
            this.panelKpis.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.tablaControl.SuspendLayout();
            this.flowSelector.SuspendLayout();
            this.flowExportacion.SuspendLayout();
            this.flowFiltros.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).BeginInit();
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
            this.tablaRaiz.Controls.Add(this.panelKpis, 0, 0);
            this.tablaRaiz.Controls.Add(this.panelControl, 0, 1);
            this.tablaRaiz.Controls.Add(this.panelGrid, 0, 2);
            this.tablaRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaRaiz.Location = new System.Drawing.Point(0, 0);
            this.tablaRaiz.Name = "tablaRaiz";
            this.tablaRaiz.RowCount = 3;
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaRaiz.Size = new System.Drawing.Size(941, 570);
            this.tablaRaiz.TabIndex = 0;

            // 
            // panelKpis
            // 
            this.panelKpis.Controls.Add(this.flowKpis);
            this.panelKpis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKpis.Location = new System.Drawing.Point(0, 0);
            this.panelKpis.Margin = new System.Windows.Forms.Padding(0);
            this.panelKpis.Name = "panelKpis";
            this.panelKpis.Size = new System.Drawing.Size(941, 90);
            this.panelKpis.TabIndex = 0;

            // 
            // flowKpis
            // 
            this.flowKpis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowKpis.Location = new System.Drawing.Point(0, 0);
            this.flowKpis.Margin = new System.Windows.Forms.Padding(0);
            this.flowKpis.Name = "flowKpis";
            this.flowKpis.Size = new System.Drawing.Size(941, 90);
            this.flowKpis.TabIndex = 0;
            this.flowKpis.WrapContents = false;

            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.White;
            this.panelControl.Controls.Add(this.tablaControl);
            this.panelControl.Controls.Add(this.panelFranja);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 96);
            this.panelControl.Margin = new System.Windows.Forms.Padding(0, 6, 0, 8);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(941, 66);
            this.panelControl.TabIndex = 1;

            // 
            // panelFranja
            // 
            this.panelFranja.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranja.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFranja.Location = new System.Drawing.Point(0, 0);
            this.panelFranja.Name = "panelFranja";
            this.panelFranja.Size = new System.Drawing.Size(4, 66);
            this.panelFranja.TabIndex = 0;

            // 
            // tablaControl
            // 
            this.tablaControl.ColumnCount = 2;
            this.tablaControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tablaControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tablaControl.Controls.Add(this.flowSelector, 0, 0);
            this.tablaControl.Controls.Add(this.flowExportacion, 1, 0);
            this.tablaControl.Controls.Add(this.flowFiltros, 0, 1);
            this.tablaControl.SetColumnSpan(this.flowFiltros, 2);
            this.tablaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaControl.Location = new System.Drawing.Point(4, 0);
            this.tablaControl.Margin = new System.Windows.Forms.Padding(0);
            this.tablaControl.Name = "tablaControl";
            this.tablaControl.RowCount = 2;
            this.tablaControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tablaControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tablaControl.Size = new System.Drawing.Size(937, 66);
            this.tablaControl.TabIndex = 1;

            // 
            // flowSelector
            // 
            this.flowSelector.Controls.Add(this.lblTipoReporte);
            this.flowSelector.Controls.Add(this.cboTipoReporte);
            this.flowSelector.Controls.Add(this.btnActualizar);
            this.flowSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSelector.Location = new System.Drawing.Point(12, 3);
            this.flowSelector.Margin = new System.Windows.Forms.Padding(12, 3, 0, 0);
            this.flowSelector.Name = "flowSelector";
            this.flowSelector.Size = new System.Drawing.Size(503, 33);
            this.flowSelector.TabIndex = 0;
            this.flowSelector.WrapContents = false;

            // 
            // lblTipoReporte
            // 
            this.lblTipoReporte.AutoSize = true;
            this.lblTipoReporte.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblTipoReporte.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblTipoReporte.Location = new System.Drawing.Point(0, 6);
            this.lblTipoReporte.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblTipoReporte.Name = "lblTipoReporte";
            this.lblTipoReporte.Size = new System.Drawing.Size(92, 15);
            this.lblTipoReporte.TabIndex = 0;
            this.lblTipoReporte.Text = "Tipo de informe:";

            // 
            // cboTipoReporte
            // 
            this.cboTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoReporte.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboTipoReporte.FormattingEnabled = true;
            this.cboTipoReporte.Location = new System.Drawing.Point(101, 3);
            this.cboTipoReporte.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.cboTipoReporte.Name = "cboTipoReporte";
            this.cboTipoReporte.Size = new System.Drawing.Size(260, 21);
            this.cboTipoReporte.TabIndex = 1;
            this.cboTipoReporte.SelectedIndexChanged += new System.EventHandler(this.cboTipoReporte_SelectedIndexChanged);

            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnActualizar.Location = new System.Drawing.Point(372, 3);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(80, 23);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "⟳ Generar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // 
            // flowExportacion
            // 
            this.flowExportacion.Controls.Add(this.btnExportarCsv);
            this.flowExportacion.Controls.Add(this.btnExportarPdf);
            this.flowExportacion.Controls.Add(this.btnImprimir);
            this.flowExportacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowExportacion.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowExportacion.Location = new System.Drawing.Point(515, 3);
            this.flowExportacion.Margin = new System.Windows.Forms.Padding(0, 3, 14, 0);
            this.flowExportacion.Name = "flowExportacion";
            this.flowExportacion.Size = new System.Drawing.Size(408, 33);
            this.flowExportacion.TabIndex = 1;
            this.flowExportacion.WrapContents = false;

            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.White;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnImprimir.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnImprimir.Location = new System.Drawing.Point(100, 2);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(85, 25);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "🖨 Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);

            // 
            // btnExportarPdf
            // 
            this.btnExportarPdf.BackColor = System.Drawing.Color.White;
            this.btnExportarPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarPdf.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnExportarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPdf.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnExportarPdf.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnExportarPdf.Location = new System.Drawing.Point(193, 2);
            this.btnExportarPdf.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(100, 25);
            this.btnExportarPdf.TabIndex = 1;
            this.btnExportarPdf.Text = "📄 Exportar PDF";
            this.btnExportarPdf.UseVisualStyleBackColor = false;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);

            // 
            // btnExportarCsv
            // 
            this.btnExportarCsv.BackColor = System.Drawing.Color.FromArgb(234, 243, 251);
            this.btnExportarCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarCsv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 220, 240);
            this.btnExportarCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarCsv.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.btnExportarCsv.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.btnExportarCsv.Location = new System.Drawing.Point(301, 2);
            this.btnExportarCsv.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnExportarCsv.Name = "btnExportarCsv";
            this.btnExportarCsv.Size = new System.Drawing.Size(105, 25);
            this.btnExportarCsv.TabIndex = 0;
            this.btnExportarCsv.Text = "📥 Exportar CSV";
            this.btnExportarCsv.UseVisualStyleBackColor = false;
            this.btnExportarCsv.Click += new System.EventHandler(this.btnExportarCsv_Click);

            // 
            // flowFiltros
            // 
            this.flowFiltros.Controls.Add(this.lblFiltroFechas);
            this.flowFiltros.Controls.Add(this.chkFiltrarFechas);
            this.flowFiltros.Controls.Add(this.dtpFechaDesde);
            this.flowFiltros.Controls.Add(this.lblSeparadorA);
            this.flowFiltros.Controls.Add(this.dtpFechaHasta);
            this.flowFiltros.Controls.Add(this.lblFiltroSecundario);
            this.flowFiltros.Controls.Add(this.cboFiltroSecundario);
            this.flowFiltros.Controls.Add(this.btnRestablecerFiltros);
            this.flowFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowFiltros.Location = new System.Drawing.Point(12, 34);
            this.flowFiltros.Margin = new System.Windows.Forms.Padding(12, 1, 0, 0);
            this.flowFiltros.Name = "flowFiltros";
            this.flowFiltros.Size = new System.Drawing.Size(925, 32);
            this.flowFiltros.TabIndex = 2;
            this.flowFiltros.WrapContents = false;

            // 
            // lblFiltroFechas
            // 
            this.lblFiltroFechas.AutoSize = true;
            this.lblFiltroFechas.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFiltroFechas.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFiltroFechas.Location = new System.Drawing.Point(0, 6);
            this.lblFiltroFechas.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblFiltroFechas.Name = "lblFiltroFechas";
            this.lblFiltroFechas.Size = new System.Drawing.Size(46, 15);
            this.lblFiltroFechas.TabIndex = 0;
            this.lblFiltroFechas.Text = "Fechas:";

            // 
            // chkFiltrarFechas
            // 
            this.chkFiltrarFechas.AutoSize = true;
            this.chkFiltrarFechas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkFiltrarFechas.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.chkFiltrarFechas.Location = new System.Drawing.Point(50, 5);
            this.chkFiltrarFechas.Margin = new System.Windows.Forms.Padding(0, 5, 4, 0);
            this.chkFiltrarFechas.Name = "chkFiltrarFechas";
            this.chkFiltrarFechas.Size = new System.Drawing.Size(61, 19);
            this.chkFiltrarFechas.TabIndex = 1;
            this.chkFiltrarFechas.Text = "Activar";
            this.chkFiltrarFechas.UseVisualStyleBackColor = true;
            this.chkFiltrarFechas.CheckedChanged += new System.EventHandler(this.FiltrosCambiados);

            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaDesde.Enabled = false;
            this.dtpFechaDesde.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDesde.Location = new System.Drawing.Point(115, 3);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(95, 23);
            this.dtpFechaDesde.TabIndex = 2;
            this.dtpFechaDesde.ValueChanged += new System.EventHandler(this.FiltrosCambiados);

            // 
            // lblSeparadorA
            // 
            this.lblSeparadorA.AutoSize = true;
            this.lblSeparadorA.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSeparadorA.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblSeparadorA.Location = new System.Drawing.Point(213, 6);
            this.lblSeparadorA.Margin = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.lblSeparadorA.Name = "lblSeparadorA";
            this.lblSeparadorA.Size = new System.Drawing.Size(13, 15);
            this.lblSeparadorA.TabIndex = 3;
            this.lblSeparadorA.Text = "a";

            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaHasta.Enabled = false;
            this.dtpFechaHasta.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHasta.Location = new System.Drawing.Point(228, 3);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(95, 23);
            this.dtpFechaHasta.TabIndex = 4;
            this.dtpFechaHasta.ValueChanged += new System.EventHandler(this.FiltrosCambiados);

            // 
            // lblFiltroSecundario
            // 
            this.lblFiltroSecundario.AutoSize = true;
            this.lblFiltroSecundario.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFiltroSecundario.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFiltroSecundario.Location = new System.Drawing.Point(334, 6);
            this.lblFiltroSecundario.Margin = new System.Windows.Forms.Padding(8, 6, 4, 0);
            this.lblFiltroSecundario.Name = "lblFiltroSecundario";
            this.lblFiltroSecundario.Size = new System.Drawing.Size(46, 15);
            this.lblFiltroSecundario.TabIndex = 5;
            this.lblFiltroSecundario.Text = "Estado:";

            // 
            // cboFiltroSecundario
            // 
            this.cboFiltroSecundario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroSecundario.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboFiltroSecundario.FormattingEnabled = true;
            this.cboFiltroSecundario.Location = new System.Drawing.Point(384, 3);
            this.cboFiltroSecundario.Name = "cboFiltroSecundario";
            this.cboFiltroSecundario.Size = new System.Drawing.Size(180, 21);
            this.cboFiltroSecundario.TabIndex = 6;
            this.cboFiltroSecundario.SelectedIndexChanged += new System.EventHandler(this.FiltrosCambiados);

            // 
            // btnRestablecerFiltros
            // 
            this.btnRestablecerFiltros.BackColor = System.Drawing.Color.White;
            this.btnRestablecerFiltros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestablecerFiltros.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnRestablecerFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestablecerFiltros.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRestablecerFiltros.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnRestablecerFiltros.Location = new System.Drawing.Point(572, 3);
            this.btnRestablecerFiltros.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.btnRestablecerFiltros.Name = "btnRestablecerFiltros";
            this.btnRestablecerFiltros.Size = new System.Drawing.Size(125, 23);
            this.btnRestablecerFiltros.TabIndex = 7;
            this.btnRestablecerFiltros.Text = "⟲ Restablecer filtros";
            this.btnRestablecerFiltros.UseVisualStyleBackColor = false;
            this.btnRestablecerFiltros.Click += new System.EventHandler(this.btnRestablecerFiltros_Click);

            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Controls.Add(this.dgvReportes);
            this.panelGrid.Controls.Add(this.panelResumen);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 140);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(941, 430);
            this.panelGrid.TabIndex = 2;

            // 
            // dgvReportes
            // 
            this.dgvReportes.AllowUserToAddRows = false;
            this.dgvReportes.AllowUserToDeleteRows = false;
            this.dgvReportes.AllowUserToResizeRows = false;
            this.dgvReportes.BackgroundColor = System.Drawing.Color.White;
            this.dgvReportes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReportes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReportes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvReportes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.dgvReportes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.dgvReportes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvReportes.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.dgvReportes.ColumnHeadersHeight = 36;
            this.dgvReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReportes.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvReportes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvReportes.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.dgvReportes.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dgvReportes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(234, 243, 251);
            this.dgvReportes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.dgvReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportes.EnableHeadersVisualStyles = false;
            this.dgvReportes.GridColor = System.Drawing.Color.FromArgb(232, 237, 242);
            this.dgvReportes.Location = new System.Drawing.Point(0, 0);
            this.dgvReportes.MultiSelect = false;
            this.dgvReportes.Name = "dgvReportes";
            this.dgvReportes.ReadOnly = true;
            this.dgvReportes.RowHeadersVisible = false;
            this.dgvReportes.RowTemplate.Height = 32;
            this.dgvReportes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportes.Size = new System.Drawing.Size(941, 398);
            this.dgvReportes.TabIndex = 0;

            // 
            // panelResumen
            // 
            this.panelResumen.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResumen.Controls.Add(this.lblResumenReporte);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelResumen.Location = new System.Drawing.Point(0, 398);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.panelResumen.Size = new System.Drawing.Size(941, 32);
            this.panelResumen.TabIndex = 1;

            // 
            // lblResumenReporte
            // 
            this.lblResumenReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResumenReporte.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblResumenReporte.ForeColor = System.Drawing.Color.FromArgb(85, 101, 122);
            this.lblResumenReporte.Location = new System.Drawing.Point(12, 6);
            this.lblResumenReporte.Name = "lblResumenReporte";
            this.lblResumenReporte.Size = new System.Drawing.Size(915, 18);
            this.lblResumenReporte.TabIndex = 0;
            this.lblResumenReporte.Text = "Reporte generado satisfactoriamente.";
            this.lblResumenReporte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(941, 580);
            this.Controls.Add(this.panelFondo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReportes";
            this.Text = "Consultas y Reportes";
            this.panelFondo.ResumeLayout(false);
            this.tablaRaiz.ResumeLayout(false);
            this.panelKpis.ResumeLayout(false);
            this.panelControl.ResumeLayout(false);
            this.tablaControl.ResumeLayout(false);
            this.flowSelector.ResumeLayout(false);
            this.flowSelector.PerformLayout();
            this.flowExportacion.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).EndInit();
            this.panelResumen.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFondo;
        private System.Windows.Forms.TableLayoutPanel tablaRaiz;
        private System.Windows.Forms.Panel panelKpis;
        private System.Windows.Forms.FlowLayoutPanel flowKpis;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Panel panelFranja;
        private System.Windows.Forms.TableLayoutPanel tablaControl;
        private System.Windows.Forms.FlowLayoutPanel flowSelector;
        private System.Windows.Forms.Label lblTipoReporte;
        private System.Windows.Forms.ComboBox cboTipoReporte;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.FlowLayoutPanel flowExportacion;
        private System.Windows.Forms.Button btnExportarCsv;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvReportes;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.Label lblResumenReporte;
        private System.Windows.Forms.FlowLayoutPanel flowFiltros;
        private System.Windows.Forms.Label lblFiltroFechas;
        private System.Windows.Forms.CheckBox chkFiltrarFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblSeparadorA;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Label lblFiltroSecundario;
        private System.Windows.Forms.ComboBox cboFiltroSecundario;
        private System.Windows.Forms.Button btnRestablecerFiltros;
    }
}

