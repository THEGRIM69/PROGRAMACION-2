namespace AgendaDeViajes.Forms
{
    partial class FrmDashboard
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
            this.tablaRaiz = new System.Windows.Forms.TableLayoutPanel();
            this.flowTarjetas = new System.Windows.Forms.FlowLayoutPanel();
            this.tablaCuerpo = new System.Windows.Forms.TableLayoutPanel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.tablaGrid = new System.Windows.Forms.TableLayoutPanel();
            this.tablaTituloGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloProximos = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblFiltroActivo = new System.Windows.Forms.Label();
            this.dgvProximosVuelos = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAerolinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisponibles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablaLateral = new System.Windows.Forms.TableLayoutPanel();
            this.panelOcupacionGlobal = new System.Windows.Forms.Panel();
            this.lblTituloOcupacion = new System.Windows.Forms.Label();
            this.lblPorcentajeGlobal = new System.Windows.Forms.Label();
            this.pbOcupacionGlobal = new System.Windows.Forms.ProgressBar();
            this.panelAerolineas = new System.Windows.Forms.Panel();
            this.lblTituloAerolineas = new System.Windows.Forms.Label();
            this.tablaAerolineas = new System.Windows.Forms.TableLayoutPanel();
            this.panelCalendario = new System.Windows.Forms.Panel();
            this.chkFiltrarFecha = new System.Windows.Forms.CheckBox();
            this.monthCalendario = new System.Windows.Forms.MonthCalendar();
            this.panelUmbral = new System.Windows.Forms.Panel();
            this.lblTituloUmbral = new System.Windows.Forms.Label();
            this.trackBarUmbral = new System.Windows.Forms.TrackBar();
            this.tablaRaiz.SuspendLayout();
            this.tablaCuerpo.SuspendLayout();
            this.panelGrid.SuspendLayout();
            this.tablaGrid.SuspendLayout();
            this.tablaTituloGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximosVuelos)).BeginInit();
            this.tablaLateral.SuspendLayout();
            this.panelOcupacionGlobal.SuspendLayout();
            this.panelAerolineas.SuspendLayout();
            this.panelCalendario.SuspendLayout();
            this.panelUmbral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUmbral)).BeginInit();
            this.SuspendLayout();
            //
            // tablaRaiz
            //
            this.tablaRaiz.ColumnCount = 1;
            this.tablaRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaRaiz.Controls.Add(this.flowTarjetas, 0, 0);
            this.tablaRaiz.Controls.Add(this.tablaCuerpo, 0, 1);
            this.tablaRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaRaiz.Location = new System.Drawing.Point(0, 0);
            this.tablaRaiz.Margin = new System.Windows.Forms.Padding(0);
            this.tablaRaiz.Name = "tablaRaiz";
            this.tablaRaiz.RowCount = 2;
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tablaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaRaiz.Size = new System.Drawing.Size(941, 580);
            this.tablaRaiz.TabIndex = 0;
            //
            // flowTarjetas
            //
            this.flowTarjetas.AutoScroll = true;
            this.flowTarjetas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.flowTarjetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTarjetas.Location = new System.Drawing.Point(0, 0);
            this.flowTarjetas.Margin = new System.Windows.Forms.Padding(0);
            this.flowTarjetas.Name = "flowTarjetas";
            this.flowTarjetas.Size = new System.Drawing.Size(941, 110);
            this.flowTarjetas.TabIndex = 0;
            this.flowTarjetas.WrapContents = false;
            //
            // tablaCuerpo
            //
            this.tablaCuerpo.ColumnCount = 2;
            this.tablaCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 328F));
            this.tablaCuerpo.Controls.Add(this.panelGrid, 0, 0);
            this.tablaCuerpo.Controls.Add(this.tablaLateral, 1, 0);
            this.tablaCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaCuerpo.Location = new System.Drawing.Point(0, 110);
            this.tablaCuerpo.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.tablaCuerpo.Name = "tablaCuerpo";
            this.tablaCuerpo.RowCount = 1;
            this.tablaCuerpo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaCuerpo.Size = new System.Drawing.Size(941, 458);
            this.tablaCuerpo.TabIndex = 1;
            //
            // panelGrid
            //
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Controls.Add(this.tablaGrid);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelGrid.Size = new System.Drawing.Size(601, 458);
            this.panelGrid.TabIndex = 0;
            //
            // tablaGrid
            //
            this.tablaGrid.ColumnCount = 1;
            this.tablaGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaGrid.Controls.Add(this.tablaTituloGrid, 0, 0);
            this.tablaGrid.Controls.Add(this.lblFiltroActivo, 0, 1);
            this.tablaGrid.Controls.Add(this.dgvProximosVuelos, 0, 2);
            this.tablaGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaGrid.Location = new System.Drawing.Point(16, 12);
            this.tablaGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tablaGrid.Name = "tablaGrid";
            this.tablaGrid.RowCount = 3;
            this.tablaGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tablaGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tablaGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaGrid.Size = new System.Drawing.Size(569, 434);
            this.tablaGrid.TabIndex = 0;
            //
            // tablaTituloGrid
            //
            this.tablaTituloGrid.ColumnCount = 2;
            this.tablaTituloGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaTituloGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tablaTituloGrid.Controls.Add(this.lblTituloProximos, 0, 0);
            this.tablaTituloGrid.Controls.Add(this.btnActualizar, 1, 0);
            this.tablaTituloGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaTituloGrid.Location = new System.Drawing.Point(0, 0);
            this.tablaTituloGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tablaTituloGrid.Name = "tablaTituloGrid";
            this.tablaTituloGrid.RowCount = 1;
            this.tablaTituloGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaTituloGrid.Size = new System.Drawing.Size(569, 38);
            this.tablaTituloGrid.TabIndex = 0;
            //
            // lblTituloProximos
            //
            this.lblTituloProximos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloProximos.Font = new System.Drawing.Font("Segoe UI Semibold", 11.5F);
            this.lblTituloProximos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.lblTituloProximos.Location = new System.Drawing.Point(0, 0);
            this.lblTituloProximos.Margin = new System.Windows.Forms.Padding(0);
            this.lblTituloProximos.Name = "lblTituloProximos";
            this.lblTituloProximos.Size = new System.Drawing.Size(465, 38);
            this.lblTituloProximos.TabIndex = 0;
            this.lblTituloProximos.Text = "Próximos vuelos";
            this.lblTituloProximos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnActualizar
            //
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(469, 5);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 5, 0, 5);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(100, 28);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // lblFiltroActivo
            //
            this.lblFiltroActivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltroActivo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFiltroActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(122)))), ((int)(((byte)(140)))));
            this.lblFiltroActivo.Location = new System.Drawing.Point(0, 38);
            this.lblFiltroActivo.Margin = new System.Windows.Forms.Padding(0);
            this.lblFiltroActivo.Name = "lblFiltroActivo";
            this.lblFiltroActivo.Size = new System.Drawing.Size(569, 26);
            this.lblFiltroActivo.TabIndex = 1;
            this.lblFiltroActivo.Text = "Próximos vuelos ordenados por fecha de salida.";
            this.lblFiltroActivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvProximosVuelos
            //
            this.dgvProximosVuelos.AllowUserToAddRows = false;
            this.dgvProximosVuelos.AllowUserToDeleteRows = false;
            this.dgvProximosVuelos.AllowUserToResizeRows = false;
            this.dgvProximosVuelos.AutoGenerateColumns = false;
            this.dgvProximosVuelos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProximosVuelos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProximosVuelos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProximosVuelos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProximosVuelos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProximosVuelos.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47))))),
                Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F),
                ForeColor = System.Drawing.Color.White,
                Padding = new System.Windows.Forms.Padding(6, 0, 0, 0),
                SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47))))),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.False
            };
            this.dgvProximosVuelos.ColumnHeadersHeight = 34;
            this.dgvProximosVuelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProximosVuelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colAerolinea,
            this.colOrigen,
            this.colDestino,
            this.colSalida,
            this.colEstado,
            this.colDisponibles});
            this.dgvProximosVuelos.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9F),
                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56))))),
                Padding = new System.Windows.Forms.Padding(6, 0, 0, 0),
                SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(243)))), ((int)(((byte)(251))))),
                SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47))))),
                WrapMode = System.Windows.Forms.DataGridViewTriState.False
            };
            this.dgvProximosVuelos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProximosVuelos.EnableHeadersVisualStyles = false;
            this.dgvProximosVuelos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dgvProximosVuelos.Location = new System.Drawing.Point(0, 64);
            this.dgvProximosVuelos.Margin = new System.Windows.Forms.Padding(0);
            this.dgvProximosVuelos.MultiSelect = false;
            this.dgvProximosVuelos.Name = "dgvProximosVuelos";
            this.dgvProximosVuelos.ReadOnly = true;
            this.dgvProximosVuelos.RowHeadersVisible = false;
            this.dgvProximosVuelos.RowTemplate.Height = 30;
            this.dgvProximosVuelos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProximosVuelos.Size = new System.Drawing.Size(569, 370);
            this.dgvProximosVuelos.TabIndex = 2;
            this.dgvProximosVuelos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProximosVuelos_DataBindingComplete);
            //
            // colCodigo
            //
            this.colCodigo.DataPropertyName = "Codigo";
            this.colCodigo.FillWeight = 62F;
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            //
            // colAerolinea
            //
            this.colAerolinea.DataPropertyName = "Aerolinea";
            this.colAerolinea.FillWeight = 108F;
            this.colAerolinea.HeaderText = "Aerolínea";
            this.colAerolinea.Name = "colAerolinea";
            this.colAerolinea.ReadOnly = true;
            //
            // colOrigen
            //
            this.colOrigen.DataPropertyName = "Origen";
            this.colOrigen.FillWeight = 58F;
            this.colOrigen.HeaderText = "Origen";
            this.colOrigen.Name = "colOrigen";
            this.colOrigen.ReadOnly = true;
            //
            // colDestino
            //
            this.colDestino.DataPropertyName = "Destino";
            this.colDestino.FillWeight = 58F;
            this.colDestino.HeaderText = "Destino";
            this.colDestino.Name = "colDestino";
            this.colDestino.ReadOnly = true;
            //
            // colSalida
            //
            this.colSalida.DataPropertyName = "FechaSalida";
            this.colSalida.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                Format = "dd/MM/yyyy HH:mm",
                NullValue = "-"
            };
            this.colSalida.FillWeight = 96F;
            this.colSalida.HeaderText = "Salida";
            this.colSalida.Name = "colSalida";
            this.colSalida.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.FillWeight = 78F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // colDisponibles
            //
            this.colDisponibles.DataPropertyName = "Disponibles";
            this.colDisponibles.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            };
            this.colDisponibles.FillWeight = 68F;
            this.colDisponibles.HeaderText = "Disp.";
            this.colDisponibles.Name = "colDisponibles";
            this.colDisponibles.ReadOnly = true;
            //
            // tablaLateral
            //
            this.tablaLateral.AutoScroll = true;
            this.tablaLateral.ColumnCount = 1;
            this.tablaLateral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaLateral.Controls.Add(this.panelOcupacionGlobal, 0, 0);
            this.tablaLateral.Controls.Add(this.panelAerolineas, 0, 1);
            this.tablaLateral.Controls.Add(this.panelCalendario, 0, 2);
            this.tablaLateral.Controls.Add(this.panelUmbral, 0, 3);
            this.tablaLateral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaLateral.Location = new System.Drawing.Point(613, 0);
            this.tablaLateral.Margin = new System.Windows.Forms.Padding(0);
            this.tablaLateral.Name = "tablaLateral";
            this.tablaLateral.RowCount = 4;
            this.tablaLateral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tablaLateral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tablaLateral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tablaLateral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tablaLateral.Size = new System.Drawing.Size(328, 458);
            this.tablaLateral.TabIndex = 1;
            //
            // panelOcupacionGlobal
            //
            this.panelOcupacionGlobal.BackColor = System.Drawing.Color.White;
            this.panelOcupacionGlobal.Controls.Add(this.lblTituloOcupacion);
            this.panelOcupacionGlobal.Controls.Add(this.lblPorcentajeGlobal);
            this.panelOcupacionGlobal.Controls.Add(this.pbOcupacionGlobal);
            this.panelOcupacionGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOcupacionGlobal.Location = new System.Drawing.Point(0, 0);
            this.panelOcupacionGlobal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelOcupacionGlobal.Name = "panelOcupacionGlobal";
            this.panelOcupacionGlobal.Size = new System.Drawing.Size(328, 58);
            this.panelOcupacionGlobal.TabIndex = 0;
            //
            // lblTituloOcupacion
            //
            this.lblTituloOcupacion.AutoSize = false;
            this.lblTituloOcupacion.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTituloOcupacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(122)))), ((int)(((byte)(140)))));
            this.lblTituloOcupacion.Location = new System.Drawing.Point(12, 6);
            this.lblTituloOcupacion.Name = "lblTituloOcupacion";
            this.lblTituloOcupacion.Size = new System.Drawing.Size(300, 16);
            this.lblTituloOcupacion.TabIndex = 0;
            this.lblTituloOcupacion.Text = "Ocupación global de la agenda";
            //
            // lblPorcentajeGlobal
            //
            this.lblPorcentajeGlobal.AutoSize = false;
            this.lblPorcentajeGlobal.Font = new System.Drawing.Font("Segoe UI Semibold", 12.5F);
            this.lblPorcentajeGlobal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.lblPorcentajeGlobal.Location = new System.Drawing.Point(12, 22);
            this.lblPorcentajeGlobal.Name = "lblPorcentajeGlobal";
            this.lblPorcentajeGlobal.Size = new System.Drawing.Size(300, 22);
            this.lblPorcentajeGlobal.TabIndex = 1;
            this.lblPorcentajeGlobal.Text = "0,0 %";
            //
            // pbOcupacionGlobal
            //
            this.pbOcupacionGlobal.Location = new System.Drawing.Point(12, 44);
            this.pbOcupacionGlobal.Name = "pbOcupacionGlobal";
            this.pbOcupacionGlobal.Size = new System.Drawing.Size(300, 10);
            this.pbOcupacionGlobal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbOcupacionGlobal.TabIndex = 2;
            //
            // panelAerolineas
            //
            this.panelAerolineas.BackColor = System.Drawing.Color.White;
            this.panelAerolineas.Controls.Add(this.lblTituloAerolineas);
            this.panelAerolineas.Controls.Add(this.tablaAerolineas);
            this.panelAerolineas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAerolineas.Location = new System.Drawing.Point(0, 68);
            this.panelAerolineas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelAerolineas.Name = "panelAerolineas";
            this.panelAerolineas.Size = new System.Drawing.Size(328, 116);
            this.panelAerolineas.TabIndex = 1;
            //
            // lblTituloAerolineas
            //
            this.lblTituloAerolineas.AutoSize = false;
            this.lblTituloAerolineas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTituloAerolineas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(122)))), ((int)(((byte)(140)))));
            this.lblTituloAerolineas.Location = new System.Drawing.Point(12, 6);
            this.lblTituloAerolineas.Name = "lblTituloAerolineas";
            this.lblTituloAerolineas.Size = new System.Drawing.Size(300, 16);
            this.lblTituloAerolineas.TabIndex = 0;
            this.lblTituloAerolineas.Text = "Ocupación por aerolínea";
            //
            // tablaAerolineas
            //
            this.tablaAerolineas.ColumnCount = 3;
            this.tablaAerolineas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaAerolineas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tablaAerolineas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tablaAerolineas.Location = new System.Drawing.Point(12, 24);
            this.tablaAerolineas.Margin = new System.Windows.Forms.Padding(0);
            this.tablaAerolineas.Name = "tablaAerolineas";
            this.tablaAerolineas.RowCount = 0;
            this.tablaAerolineas.Size = new System.Drawing.Size(300, 88);
            this.tablaAerolineas.TabIndex = 1;
            //
            // panelCalendario
            //
            this.panelCalendario.BackColor = System.Drawing.Color.White;
            this.panelCalendario.Controls.Add(this.chkFiltrarFecha);
            this.panelCalendario.Controls.Add(this.monthCalendario);
            this.panelCalendario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalendario.Location = new System.Drawing.Point(0, 198);
            this.panelCalendario.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelCalendario.Name = "panelCalendario";
            this.panelCalendario.Size = new System.Drawing.Size(328, 188);
            this.panelCalendario.TabIndex = 2;
            //
            // chkFiltrarFecha
            //
            this.chkFiltrarFecha.AutoSize = true;
            this.chkFiltrarFecha.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkFiltrarFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.chkFiltrarFecha.Location = new System.Drawing.Point(12, 4);
            this.chkFiltrarFecha.Name = "chkFiltrarFecha";
            this.chkFiltrarFecha.Size = new System.Drawing.Size(183, 19);
            this.chkFiltrarFecha.TabIndex = 0;
            this.chkFiltrarFecha.Text = "Consultar vuelos por fecha";
            this.chkFiltrarFecha.UseVisualStyleBackColor = true;
            this.chkFiltrarFecha.CheckedChanged += new System.EventHandler(this.chkFiltrarFecha_CheckedChanged);
            //
            // monthCalendario
            //
            this.monthCalendario.Location = new System.Drawing.Point(10, 26);
            this.monthCalendario.MaxSelectionCount = 1;
            this.monthCalendario.Name = "monthCalendario";
            this.monthCalendario.ShowToday = false;
            this.monthCalendario.ShowWeekNumbers = false;
            this.monthCalendario.Size = new System.Drawing.Size(240, 162);
            this.monthCalendario.TabIndex = 1;
            this.monthCalendario.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(47)))));
            this.monthCalendario.TitleForeColor = System.Drawing.Color.White;
            this.monthCalendario.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(170)))), ((int)(((byte)(180)))));
            this.monthCalendario.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendario_DateSelected);
            //
            // panelUmbral
            //
            this.panelUmbral.BackColor = System.Drawing.Color.White;
            this.panelUmbral.Controls.Add(this.lblTituloUmbral);
            this.panelUmbral.Controls.Add(this.trackBarUmbral);
            this.panelUmbral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUmbral.Location = new System.Drawing.Point(0, 398);
            this.panelUmbral.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelUmbral.Name = "panelUmbral";
            this.panelUmbral.Size = new System.Drawing.Size(328, 46);
            this.panelUmbral.TabIndex = 3;
            //
            // lblTituloUmbral
            //
            this.lblTituloUmbral.AutoSize = false;
            this.lblTituloUmbral.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTituloUmbral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(122)))), ((int)(((byte)(140)))));
            this.lblTituloUmbral.Location = new System.Drawing.Point(12, 2);
            this.lblTituloUmbral.Name = "lblTituloUmbral";
            this.lblTituloUmbral.Size = new System.Drawing.Size(300, 16);
            this.lblTituloUmbral.TabIndex = 0;
            this.lblTituloUmbral.Text = "Umbral de ocupación: 80%";
            //
            // trackBarUmbral
            //
            this.trackBarUmbral.Location = new System.Drawing.Point(10, 18);
            this.trackBarUmbral.Maximum = 100;
            this.trackBarUmbral.Name = "trackBarUmbral";
            this.trackBarUmbral.Size = new System.Drawing.Size(300, 26);
            this.trackBarUmbral.SmallChange = 5;
            this.trackBarUmbral.TabIndex = 1;
            this.trackBarUmbral.TickFrequency = 10;
            this.trackBarUmbral.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarUmbral.Value = 80;
            this.trackBarUmbral.ValueChanged += new System.EventHandler(this.trackBarUmbral_ValueChanged);
            //
            // FrmDashboard
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(941, 580);
            this.Controls.Add(this.tablaRaiz);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashboard";
            this.Text = "Panel principal";
            this.tablaRaiz.ResumeLayout(false);
            this.tablaCuerpo.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            this.tablaGrid.ResumeLayout(false);
            this.tablaTituloGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximosVuelos)).EndInit();
            this.tablaLateral.ResumeLayout(false);
            this.panelOcupacionGlobal.ResumeLayout(false);
            this.panelAerolineas.ResumeLayout(false);
            this.panelCalendario.ResumeLayout(false);
            this.panelCalendario.PerformLayout();
            this.panelUmbral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUmbral)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablaRaiz;
        private System.Windows.Forms.FlowLayoutPanel flowTarjetas;
        private System.Windows.Forms.TableLayoutPanel tablaCuerpo;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.TableLayoutPanel tablaGrid;
        private System.Windows.Forms.TableLayoutPanel tablaTituloGrid;
        private System.Windows.Forms.Label lblTituloProximos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblFiltroActivo;
        private System.Windows.Forms.DataGridView dgvProximosVuelos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAerolinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisponibles;
        private System.Windows.Forms.TableLayoutPanel tablaLateral;
        private System.Windows.Forms.Panel panelOcupacionGlobal;
        private System.Windows.Forms.Label lblTituloOcupacion;
        private System.Windows.Forms.Label lblPorcentajeGlobal;
        private System.Windows.Forms.ProgressBar pbOcupacionGlobal;
        private System.Windows.Forms.Panel panelAerolineas;
        private System.Windows.Forms.Label lblTituloAerolineas;
        private System.Windows.Forms.TableLayoutPanel tablaAerolineas;
        private System.Windows.Forms.Panel panelCalendario;
        private System.Windows.Forms.CheckBox chkFiltrarFecha;
        private System.Windows.Forms.MonthCalendar monthCalendario;
        private System.Windows.Forms.Panel panelUmbral;
        private System.Windows.Forms.Label lblTituloUmbral;
        private System.Windows.Forms.TrackBar trackBarUmbral;
    }
}
