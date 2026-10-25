namespace AgendaDeViajes.Forms
{
    partial class FrmEditarReservacion
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
            this.panelFranja = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelSeparador = new System.Windows.Forms.Panel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblVuelo = new System.Windows.Forms.Label();
            this.cboVuelo = new System.Windows.Forms.ComboBox();
            this.lblPasajero = new System.Windows.Forms.Label();
            this.cboPasajero = new System.Windows.Forms.ComboBox();
            this.lblFechaReserva = new System.Windows.Forms.Label();
            this.dtpFechaReserva = new System.Windows.Forms.DateTimePicker();
            this.lblAsientos = new System.Windows.Forms.Label();
            this.numAsientos = new System.Windows.Forms.NumericUpDown();
            this.lblTarifa = new System.Windows.Forms.Label();
            this.cboTarifa = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.chkSeguro = new System.Windows.Forms.CheckBox();
            this.lblServicios = new System.Windows.Forms.Label();
            this.clbServicios = new System.Windows.Forms.CheckedListBox();
            this.lblTotalEstimado = new System.Windows.Forms.Label();
            this.txtTotalEstimado = new System.Windows.Forms.TextBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblAvisoIntegracion = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();

            this.panelFondo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAsientos)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelFondo
            // 
            this.panelFondo.BackColor = System.Drawing.Color.White;
            this.panelFondo.Controls.Add(this.panelFranja);
            this.panelFondo.Controls.Add(this.lblTitulo);
            this.panelFondo.Controls.Add(this.lblSubtitulo);
            this.panelFondo.Controls.Add(this.panelSeparador);
            this.panelFondo.Controls.Add(this.lblCodigo);
            this.panelFondo.Controls.Add(this.txtCodigo);
            this.panelFondo.Controls.Add(this.lblVuelo);
            this.panelFondo.Controls.Add(this.cboVuelo);
            this.panelFondo.Controls.Add(this.lblPasajero);
            this.panelFondo.Controls.Add(this.cboPasajero);
            this.panelFondo.Controls.Add(this.lblFechaReserva);
            this.panelFondo.Controls.Add(this.dtpFechaReserva);
            this.panelFondo.Controls.Add(this.lblAsientos);
            this.panelFondo.Controls.Add(this.numAsientos);
            this.panelFondo.Controls.Add(this.lblTarifa);
            this.panelFondo.Controls.Add(this.cboTarifa);
            this.panelFondo.Controls.Add(this.lblEstado);
            this.panelFondo.Controls.Add(this.cboEstado);
            this.panelFondo.Controls.Add(this.chkSeguro);
            this.panelFondo.Controls.Add(this.lblServicios);
            this.panelFondo.Controls.Add(this.clbServicios);
            this.panelFondo.Controls.Add(this.lblTotalEstimado);
            this.panelFondo.Controls.Add(this.txtTotalEstimado);
            this.panelFondo.Controls.Add(this.panelBotones);
            this.panelFondo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFondo.Location = new System.Drawing.Point(0, 0);
            this.panelFondo.Name = "panelFondo";
            this.panelFondo.Size = new System.Drawing.Size(640, 580);
            this.panelFondo.TabIndex = 0;

            // 
            // panelFranja
            // 
            this.panelFranja.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranja.Location = new System.Drawing.Point(0, 0);
            this.panelFranja.Name = "panelFranja";
            this.panelFranja.Size = new System.Drawing.Size(640, 4);
            this.panelFranja.TabIndex = 0;

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.lblTitulo.Location = new System.Drawing.Point(24, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(235, 21);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "REGISTRO DE RESERVACIÓN";

            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblSubtitulo.Location = new System.Drawing.Point(25, 42);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(435, 15);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Asigne un vuelo programado, pasajero titular, cupo de asientos, tarifa y servicios.";

            // 
            // panelSeparador
            // 
            this.panelSeparador.BackColor = System.Drawing.Color.FromArgb(232, 237, 242);
            this.panelSeparador.Location = new System.Drawing.Point(25, 66);
            this.panelSeparador.Name = "panelSeparador";
            this.panelSeparador.Size = new System.Drawing.Size(590, 1);
            this.panelSeparador.TabIndex = 3;

            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblCodigo.Location = new System.Drawing.Point(25, 80);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(134, 15);
            this.lblCodigo.TabIndex = 4;
            this.lblCodigo.Text = "Código de reservación:*";

            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCodigo.Location = new System.Drawing.Point(28, 99);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PlaceholderText = "Ej. RES-0006";
            this.txtCodigo.Size = new System.Drawing.Size(265, 23);
            this.txtCodigo.TabIndex = 5;

            // 
            // lblFechaReserva
            // 
            this.lblFechaReserva.AutoSize = true;
            this.lblFechaReserva.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFechaReserva.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFechaReserva.Location = new System.Drawing.Point(330, 80);
            this.lblFechaReserva.Name = "lblFechaReserva";
            this.lblFechaReserva.Size = new System.Drawing.Size(107, 15);
            this.lblFechaReserva.TabIndex = 6;
            this.lblFechaReserva.Text = "Fecha de emisión:*";

            // 
            // dtpFechaReserva
            // 
            this.dtpFechaReserva.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaReserva.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaReserva.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaReserva.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaReserva.Location = new System.Drawing.Point(333, 99);
            this.dtpFechaReserva.Name = "dtpFechaReserva";
            this.dtpFechaReserva.Size = new System.Drawing.Size(282, 23);
            this.dtpFechaReserva.TabIndex = 7;

            // 
            // lblVuelo
            // 
            this.lblVuelo.AutoSize = true;
            this.lblVuelo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblVuelo.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblVuelo.Location = new System.Drawing.Point(25, 134);
            this.lblVuelo.Name = "lblVuelo";
            this.lblVuelo.Size = new System.Drawing.Size(117, 15);
            this.lblVuelo.TabIndex = 8;
            this.lblVuelo.Text = "Vuelo programado:*";

            // 
            // cboVuelo
            // 
            this.cboVuelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVuelo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboVuelo.FormattingEnabled = true;
            this.cboVuelo.Location = new System.Drawing.Point(28, 153);
            this.cboVuelo.Name = "cboVuelo";
            this.cboVuelo.Size = new System.Drawing.Size(587, 23);
            this.cboVuelo.TabIndex = 9;
            this.cboVuelo.SelectedIndexChanged += new System.EventHandler(this.ActualizarCalculoTotal);

            // 
            // lblPasajero
            // 
            this.lblPasajero.AutoSize = true;
            this.lblPasajero.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblPasajero.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblPasajero.Location = new System.Drawing.Point(25, 188);
            this.lblPasajero.Name = "lblPasajero";
            this.lblPasajero.Size = new System.Drawing.Size(95, 15);
            this.lblPasajero.TabIndex = 10;
            this.lblPasajero.Text = "Pasajero titular:*";

            // 
            // cboPasajero
            // 
            this.cboPasajero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPasajero.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboPasajero.FormattingEnabled = true;
            this.cboPasajero.Location = new System.Drawing.Point(28, 207);
            this.cboPasajero.Name = "cboPasajero";
            this.cboPasajero.Size = new System.Drawing.Size(587, 23);
            this.cboPasajero.TabIndex = 11;

            // 
            // lblAsientos
            // 
            this.lblAsientos.AutoSize = true;
            this.lblAsientos.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblAsientos.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblAsientos.Location = new System.Drawing.Point(25, 242);
            this.lblAsientos.Name = "lblAsientos";
            this.lblAsientos.Size = new System.Drawing.Size(127, 15);
            this.lblAsientos.TabIndex = 12;
            this.lblAsientos.Text = "Cantidad de asientos:*";

            // 
            // numAsientos
            // 
            this.numAsientos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numAsientos.Location = new System.Drawing.Point(28, 261);
            this.numAsientos.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numAsientos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numAsientos.Name = "numAsientos";
            this.numAsientos.Size = new System.Drawing.Size(120, 23);
            this.numAsientos.TabIndex = 13;
            this.numAsientos.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numAsientos.ValueChanged += new System.EventHandler(this.ActualizarCalculoTotal);

            // 
            // lblTarifa
            // 
            this.lblTarifa.AutoSize = true;
            this.lblTarifa.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblTarifa.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblTarifa.Location = new System.Drawing.Point(180, 242);
            this.lblTarifa.Name = "lblTarifa";
            this.lblTarifa.Size = new System.Drawing.Size(89, 15);
            this.lblTarifa.TabIndex = 14;
            this.lblTarifa.Text = "Clase de tarifa:*";

            // 
            // cboTarifa
            // 
            this.cboTarifa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTarifa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTarifa.FormattingEnabled = true;
            this.cboTarifa.Location = new System.Drawing.Point(183, 261);
            this.cboTarifa.Name = "cboTarifa";
            this.cboTarifa.Size = new System.Drawing.Size(200, 23);
            this.cboTarifa.TabIndex = 15;
            this.cboTarifa.SelectedIndexChanged += new System.EventHandler(this.ActualizarCalculoTotal);

            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblEstado.Location = new System.Drawing.Point(415, 242);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(51, 15);
            this.lblEstado.TabIndex = 16;
            this.lblEstado.Text = "Estado:*";

            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(418, 261);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(197, 23);
            this.cboEstado.TabIndex = 17;

            // 
            // chkSeguro
            // 
            this.chkSeguro.AutoSize = true;
            this.chkSeguro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.chkSeguro.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.chkSeguro.Location = new System.Drawing.Point(28, 301);
            this.chkSeguro.Name = "chkSeguro";
            this.chkSeguro.Size = new System.Drawing.Size(395, 19);
            this.chkSeguro.TabIndex = 18;
            this.chkSeguro.Text = "Incluir seguro de viaje internacional (+$30.00 USD por cada asiento)";
            this.chkSeguro.UseVisualStyleBackColor = true;
            this.chkSeguro.CheckedChanged += new System.EventHandler(this.ActualizarCalculoTotal);

            // 
            // lblServicios
            // 
            this.lblServicios.AutoSize = true;
            this.lblServicios.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblServicios.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblServicios.Location = new System.Drawing.Point(25, 332);
            this.lblServicios.Name = "lblServicios";
            this.lblServicios.Size = new System.Drawing.Size(265, 15);
            this.lblServicios.TabIndex = 19;
            this.lblServicios.Text = "Servicios adicionales (+$25.00 USD c/u, opcional):";

            // 
            // clbServicios
            // 
            this.clbServicios.CheckOnClick = true;
            this.clbServicios.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.clbServicios.FormattingEnabled = true;
            this.clbServicios.Location = new System.Drawing.Point(28, 351);
            this.clbServicios.Name = "clbServicios";
            this.clbServicios.Size = new System.Drawing.Size(587, 85);
            this.clbServicios.TabIndex = 20;
            this.clbServicios.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbServicios_ItemCheck);

            // 
            // lblTotalEstimado
            // 
            this.lblTotalEstimado.AutoSize = true;
            this.lblTotalEstimado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblTotalEstimado.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblTotalEstimado.Location = new System.Drawing.Point(25, 452);
            this.lblTotalEstimado.Name = "lblTotalEstimado";
            this.lblTotalEstimado.Size = new System.Drawing.Size(126, 15);
            this.lblTotalEstimado.TabIndex = 21;
            this.lblTotalEstimado.Text = "Importe total estimado:";

            // 
            // txtTotalEstimado
            // 
            this.txtTotalEstimado.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTotalEstimado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtTotalEstimado.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.txtTotalEstimado.Location = new System.Drawing.Point(157, 446);
            this.txtTotalEstimado.Name = "txtTotalEstimado";
            this.txtTotalEstimado.ReadOnly = true;
            this.txtTotalEstimado.Size = new System.Drawing.Size(180, 27);
            this.txtTotalEstimado.TabIndex = 22;
            this.txtTotalEstimado.Text = "$0.00 USD";

            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.panelBotones.Controls.Add(this.lblAvisoIntegracion);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 522);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(640, 58);
            this.panelBotones.TabIndex = 23;

            // 
            // lblAvisoIntegracion
            // 
            this.lblAvisoIntegracion.AutoSize = true;
            this.lblAvisoIntegracion.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblAvisoIntegracion.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblAvisoIntegracion.Location = new System.Drawing.Point(25, 22);
            this.lblAvisoIntegracion.Name = "lblAvisoIntegracion";
            this.lblAvisoIntegracion.Size = new System.Drawing.Size(218, 13);
            this.lblAvisoIntegracion.TabIndex = 0;
            this.lblAvisoIntegracion.Text = "Etapa frontend · Formulario de registro activo";

            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 216, 224);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.btnCancelar.Location = new System.Drawing.Point(395, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;

            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(510, 14);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 30);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // 
            // FrmEditarReservacion
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(640, 580);
            this.Controls.Add(this.panelFondo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditarReservacion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar nueva reservación";
            this.panelFondo.ResumeLayout(false);
            this.panelFondo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAsientos)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFondo;
        private System.Windows.Forms.Panel panelFranja;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelSeparador;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblVuelo;
        private System.Windows.Forms.ComboBox cboVuelo;
        private System.Windows.Forms.Label lblPasajero;
        private System.Windows.Forms.ComboBox cboPasajero;
        private System.Windows.Forms.Label lblFechaReserva;
        private System.Windows.Forms.DateTimePicker dtpFechaReserva;
        private System.Windows.Forms.Label lblAsientos;
        private System.Windows.Forms.NumericUpDown numAsientos;
        private System.Windows.Forms.Label lblTarifa;
        private System.Windows.Forms.ComboBox cboTarifa;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.CheckBox chkSeguro;
        private System.Windows.Forms.Label lblServicios;
        private System.Windows.Forms.CheckedListBox clbServicios;
        private System.Windows.Forms.Label lblTotalEstimado;
        private System.Windows.Forms.TextBox txtTotalEstimado;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblAvisoIntegracion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
    }
}

