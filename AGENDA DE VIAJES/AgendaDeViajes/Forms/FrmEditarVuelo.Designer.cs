namespace AgendaDeViajes.Forms
{
    partial class FrmEditarVuelo
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
            this.components = new System.ComponentModel.Container();

            this.panelCabecera = new System.Windows.Forms.Panel();
            this.lblIcono = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();

            this.panelCuerpo = new System.Windows.Forms.Panel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblAerolinea = new System.Windows.Forms.Label();
            this.cboAerolinea = new System.Windows.Forms.ComboBox();

            this.lblOrigen = new System.Windows.Forms.Label();
            this.cboOrigen = new System.Windows.Forms.ComboBox();
            this.lblDestino = new System.Windows.Forms.Label();
            this.cboDestino = new System.Windows.Forms.ComboBox();

            this.lblFechaSalida = new System.Windows.Forms.Label();
            this.dtpFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.lblEscalas = new System.Windows.Forms.Label();
            this.nudEscalas = new System.Windows.Forms.NumericUpDown();

            this.lblCapacidad = new System.Windows.Forms.Label();
            this.nudCapacidad = new System.Windows.Forms.NumericUpDown();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();

            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();

            this.lblInfoOcupacion = new System.Windows.Forms.Label();

            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();

            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);

            this.panelCabecera.SuspendLayout();
            this.panelCuerpo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEscalas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            this.panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            //
            // panelCabecera
            //
            this.panelCabecera.BackColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.panelCabecera.Controls.Add(this.lblIcono);
            this.panelCabecera.Controls.Add(this.lblTitulo);
            this.panelCabecera.Controls.Add(this.lblSubtitulo);
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecera.Location = new System.Drawing.Point(0, 0);
            this.panelCabecera.Name = "panelCabecera";
            this.panelCabecera.Size = new System.Drawing.Size(560, 68);
            this.panelCabecera.TabIndex = 0;

            //
            // lblIcono
            //
            this.lblIcono.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIcono.ForeColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.lblIcono.Location = new System.Drawing.Point(18, 14);
            this.lblIcono.Name = "lblIcono";
            this.lblIcono.Size = new System.Drawing.Size(40, 40);
            this.lblIcono.TabIndex = 0;
            this.lblIcono.Text = "✈";
            this.lblIcono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(66, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(182, 21);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "REGISTRO DE VUELO";

            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(143, 163, 184);
            this.lblSubtitulo.Location = new System.Drawing.Point(67, 38);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(370, 15);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Complete los datos operativos y comerciales del vuelo en la agencia.";

            //
            // panelCuerpo
            //
            this.panelCuerpo.BackColor = System.Drawing.Color.White;
            this.panelCuerpo.Controls.Add(this.lblCodigo);
            this.panelCuerpo.Controls.Add(this.txtCodigo);
            this.panelCuerpo.Controls.Add(this.lblAerolinea);
            this.panelCuerpo.Controls.Add(this.cboAerolinea);
            this.panelCuerpo.Controls.Add(this.lblOrigen);
            this.panelCuerpo.Controls.Add(this.cboOrigen);
            this.panelCuerpo.Controls.Add(this.lblDestino);
            this.panelCuerpo.Controls.Add(this.cboDestino);
            this.panelCuerpo.Controls.Add(this.lblFechaSalida);
            this.panelCuerpo.Controls.Add(this.dtpFechaSalida);
            this.panelCuerpo.Controls.Add(this.lblEscalas);
            this.panelCuerpo.Controls.Add(this.nudEscalas);
            this.panelCuerpo.Controls.Add(this.lblCapacidad);
            this.panelCuerpo.Controls.Add(this.nudCapacidad);
            this.panelCuerpo.Controls.Add(this.lblPrecio);
            this.panelCuerpo.Controls.Add(this.nudPrecio);
            this.panelCuerpo.Controls.Add(this.lblEstado);
            this.panelCuerpo.Controls.Add(this.cboEstado);
            this.panelCuerpo.Controls.Add(this.chkActivo);
            this.panelCuerpo.Controls.Add(this.lblInfoOcupacion);
            this.panelCuerpo.Controls.Add(this.lblObservaciones);
            this.panelCuerpo.Controls.Add(this.txtObservaciones);
            this.panelCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCuerpo.Location = new System.Drawing.Point(0, 68);
            this.panelCuerpo.Name = "panelCuerpo";
            this.panelCuerpo.Padding = new System.Windows.Forms.Padding(24, 18, 24, 18);
            this.panelCuerpo.Size = new System.Drawing.Size(560, 432);
            this.panelCuerpo.TabIndex = 1;

            //
            // lblCodigo
            //
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblCodigo.Location = new System.Drawing.Point(24, 18);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(107, 15);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código de vuelo: *";

            //
            // txtCodigo
            //
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCodigo.Location = new System.Drawing.Point(24, 38);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(235, 24);
            this.txtCodigo.TabIndex = 1;

            //
            // lblAerolinea
            //
            this.lblAerolinea.AutoSize = true;
            this.lblAerolinea.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblAerolinea.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblAerolinea.Location = new System.Drawing.Point(285, 18);
            this.lblAerolinea.Name = "lblAerolinea";
            this.lblAerolinea.Size = new System.Drawing.Size(69, 15);
            this.lblAerolinea.TabIndex = 2;
            this.lblAerolinea.Text = "Aerolínea: *";

            //
            // cboAerolinea
            //
            this.cboAerolinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAerolinea.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboAerolinea.FormattingEnabled = true;
            this.cboAerolinea.Location = new System.Drawing.Point(285, 38);
            this.cboAerolinea.Name = "cboAerolinea";
            this.cboAerolinea.Size = new System.Drawing.Size(240, 24);
            this.cboAerolinea.TabIndex = 3;

            //
            // lblOrigen
            //
            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblOrigen.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblOrigen.Location = new System.Drawing.Point(24, 76);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(127, 15);
            this.lblOrigen.TabIndex = 4;
            this.lblOrigen.Text = "Aeropuerto Origen: *";

            //
            // cboOrigen
            //
            this.cboOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrigen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboOrigen.FormattingEnabled = true;
            this.cboOrigen.Location = new System.Drawing.Point(24, 96);
            this.cboOrigen.Name = "cboOrigen";
            this.cboOrigen.Size = new System.Drawing.Size(235, 24);
            this.cboOrigen.TabIndex = 5;

            //
            // lblDestino
            //
            this.lblDestino.AutoSize = true;
            this.lblDestino.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblDestino.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblDestino.Location = new System.Drawing.Point(285, 76);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(132, 15);
            this.lblDestino.TabIndex = 6;
            this.lblDestino.Text = "Aeropuerto Destino: *";

            //
            // cboDestino
            //
            this.cboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestino.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDestino.FormattingEnabled = true;
            this.cboDestino.Location = new System.Drawing.Point(285, 96);
            this.cboDestino.Name = "cboDestino";
            this.cboDestino.Size = new System.Drawing.Size(240, 24);
            this.cboDestino.TabIndex = 7;

            //
            // lblFechaSalida
            //
            this.lblFechaSalida.AutoSize = true;
            this.lblFechaSalida.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFechaSalida.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFechaSalida.Location = new System.Drawing.Point(24, 134);
            this.lblFechaSalida.Name = "lblFechaSalida";
            this.lblFechaSalida.Size = new System.Drawing.Size(147, 15);
            this.lblFechaSalida.TabIndex = 8;
            this.lblFechaSalida.Text = "Fecha y hora de salida: *";

            //
            // dtpFechaSalida
            //
            this.dtpFechaSalida.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaSalida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaSalida.Location = new System.Drawing.Point(24, 154);
            this.dtpFechaSalida.Name = "dtpFechaSalida";
            this.dtpFechaSalida.Size = new System.Drawing.Size(235, 24);
            this.dtpFechaSalida.TabIndex = 9;

            //
            // lblEscalas
            //
            this.lblEscalas.AutoSize = true;
            this.lblEscalas.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblEscalas.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblEscalas.Location = new System.Drawing.Point(285, 134);
            this.lblEscalas.Name = "lblEscalas";
            this.lblEscalas.Size = new System.Drawing.Size(56, 15);
            this.lblEscalas.TabIndex = 10;
            this.lblEscalas.Text = "Escalas:";

            //
            // nudEscalas
            //
            this.nudEscalas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.nudEscalas.Location = new System.Drawing.Point(285, 154);
            this.nudEscalas.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            this.nudEscalas.Name = "nudEscalas";
            this.nudEscalas.Size = new System.Drawing.Size(110, 24);
            this.nudEscalas.TabIndex = 11;

            //
            // lblCapacidad
            //
            this.lblCapacidad.AutoSize = true;
            this.lblCapacidad.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblCapacidad.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblCapacidad.Location = new System.Drawing.Point(24, 192);
            this.lblCapacidad.Name = "lblCapacidad";
            this.lblCapacidad.Size = new System.Drawing.Size(127, 15);
            this.lblCapacidad.TabIndex = 12;
            this.lblCapacidad.Text = "Capacidad (asientos): *";

            //
            // nudCapacidad
            //
            this.nudCapacidad.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.nudCapacidad.Location = new System.Drawing.Point(24, 212);
            this.nudCapacidad.Maximum = new decimal(new int[] { 850, 0, 0, 0 });
            this.nudCapacidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCapacidad.Name = "nudCapacidad";
            this.nudCapacidad.Size = new System.Drawing.Size(235, 24);
            this.nudCapacidad.TabIndex = 13;
            this.nudCapacidad.Value = new decimal(new int[] { 150, 0, 0, 0 });

            //
            // lblPrecio
            //
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblPrecio.Location = new System.Drawing.Point(285, 192);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(114, 15);
            this.lblPrecio.TabIndex = 14;
            this.lblPrecio.Text = "Precio base (USD): *";

            //
            // nudPrecio
            //
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.nudPrecio.Location = new System.Drawing.Point(285, 212);
            this.nudPrecio.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudPrecio.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.Size = new System.Drawing.Size(240, 24);
            this.nudPrecio.TabIndex = 15;
            this.nudPrecio.Value = new decimal(new int[] { 250, 0, 0, 0 });

            //
            // lblEstado
            //
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblEstado.Location = new System.Drawing.Point(24, 250);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(107, 15);
            this.lblEstado.TabIndex = 16;
            this.lblEstado.Text = "Estado operativo:";

            //
            // cboEstado
            //
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(24, 270);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(235, 24);
            this.cboEstado.TabIndex = 17;

            //
            // chkActivo
            //
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.chkActivo.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.chkActivo.Location = new System.Drawing.Point(285, 272);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(183, 19);
            this.chkActivo.TabIndex = 18;
            this.chkActivo.Text = "Vuelo activo en la agencia";
            this.chkActivo.UseVisualStyleBackColor = true;

            //
            // lblInfoOcupacion
            //
            this.lblInfoOcupacion.AutoSize = true;
            this.lblInfoOcupacion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblInfoOcupacion.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblInfoOcupacion.Location = new System.Drawing.Point(24, 303);
            this.lblInfoOcupacion.Name = "lblInfoOcupacion";
            this.lblInfoOcupacion.Size = new System.Drawing.Size(390, 13);
            this.lblInfoOcupacion.TabIndex = 19;
            this.lblInfoOcupacion.Text = "Asientos comprometidos por reservaciones: 0. La capacidad no puede reducirse.";
            this.lblInfoOcupacion.Visible = false;

            //
            // lblObservaciones
            //
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblObservaciones.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblObservaciones.Location = new System.Drawing.Point(24, 326);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(92, 15);
            this.lblObservaciones.TabIndex = 20;
            this.lblObservaciones.Text = "Observaciones:";

            //
            // txtObservaciones
            //
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtObservaciones.Location = new System.Drawing.Point(24, 346);
            this.txtObservaciones.MaxLength = 300;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(501, 62);
            this.txtObservaciones.TabIndex = 21;

            //
            // panelBotones
            //
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 500);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 10, 24, 10);
            this.panelBotones.Size = new System.Drawing.Size(560, 54);
            this.panelBotones.TabIndex = 2;

            //
            // btnGuardar
            //
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(285, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 32);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar vuelo";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            //
            // btnCancelar
            //
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnCancelar.Location = new System.Drawing.Point(425, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 32);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            //
            // errorProvider
            //
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;

            //
            // FrmEditarVuelo
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(560, 554);
            this.Controls.Add(this.panelCuerpo);
            this.Controls.Add(this.panelCabecera);
            this.Controls.Add(this.panelBotones);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditarVuelo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vuelo";
            this.panelCabecera.ResumeLayout(false);
            this.panelCabecera.PerformLayout();
            this.panelCuerpo.ResumeLayout(false);
            this.panelCuerpo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEscalas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            this.panelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblIcono;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelCuerpo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblAerolinea;
        private System.Windows.Forms.ComboBox cboAerolinea;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.ComboBox cboOrigen;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.ComboBox cboDestino;
        private System.Windows.Forms.Label lblFechaSalida;
        private System.Windows.Forms.DateTimePicker dtpFechaSalida;
        private System.Windows.Forms.Label lblEscalas;
        private System.Windows.Forms.NumericUpDown nudEscalas;
        private System.Windows.Forms.Label lblCapacidad;
        private System.Windows.Forms.NumericUpDown nudCapacidad;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Label lblInfoOcupacion;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}

