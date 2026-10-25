namespace AgendaDeViajes.Forms
{
    partial class FrmEditarPasajero
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
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblNombres = new System.Windows.Forms.Label();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.lblApellidos = new System.Windows.Forms.Label();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblNacionalidad = new System.Windows.Forms.Label();
            this.cboNacionalidad = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.chkAsistencia = new System.Windows.Forms.CheckBox();
            this.lblServicios = new System.Windows.Forms.Label();
            this.clbServicios = new System.Windows.Forms.CheckedListBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblAvisoIntegracion = new System.Windows.Forms.Label();

            this.panelFondo.SuspendLayout();
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
            this.panelFondo.Controls.Add(this.lblDocumento);
            this.panelFondo.Controls.Add(this.txtDocumento);
            this.panelFondo.Controls.Add(this.lblNombres);
            this.panelFondo.Controls.Add(this.txtNombres);
            this.panelFondo.Controls.Add(this.lblApellidos);
            this.panelFondo.Controls.Add(this.txtApellidos);
            this.panelFondo.Controls.Add(this.lblEmail);
            this.panelFondo.Controls.Add(this.txtEmail);
            this.panelFondo.Controls.Add(this.lblTelefono);
            this.panelFondo.Controls.Add(this.txtTelefono);
            this.panelFondo.Controls.Add(this.lblFechaNacimiento);
            this.panelFondo.Controls.Add(this.dtpFechaNacimiento);
            this.panelFondo.Controls.Add(this.lblNacionalidad);
            this.panelFondo.Controls.Add(this.cboNacionalidad);
            this.panelFondo.Controls.Add(this.lblCategoria);
            this.panelFondo.Controls.Add(this.cboCategoria);
            this.panelFondo.Controls.Add(this.chkAsistencia);
            this.panelFondo.Controls.Add(this.lblServicios);
            this.panelFondo.Controls.Add(this.clbServicios);
            this.panelFondo.Controls.Add(this.panelBotones);
            this.panelFondo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFondo.Location = new System.Drawing.Point(0, 0);
            this.panelFondo.Name = "panelFondo";
            this.panelFondo.Size = new System.Drawing.Size(620, 560);
            this.panelFondo.TabIndex = 0;

            // 
            // panelFranja
            // 
            this.panelFranja.BackColor = System.Drawing.Color.FromArgb(45, 156, 219);
            this.panelFranja.Location = new System.Drawing.Point(0, 0);
            this.panelFranja.Name = "panelFranja";
            this.panelFranja.Size = new System.Drawing.Size(620, 4);
            this.panelFranja.TabIndex = 0;

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(22, 34, 47);
            this.lblTitulo.Location = new System.Drawing.Point(24, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(200, 21);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "REGISTRO DE PASAJERO";

            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(107, 122, 140);
            this.lblSubtitulo.Location = new System.Drawing.Point(25, 42);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(425, 15);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Ingrese los datos personales, categoría y preferencias de viaje del pasajero.";

            // 
            // panelSeparador
            // 
            this.panelSeparador.BackColor = System.Drawing.Color.FromArgb(232, 237, 242);
            this.panelSeparador.Location = new System.Drawing.Point(25, 66);
            this.panelSeparador.Name = "panelSeparador";
            this.panelSeparador.Size = new System.Drawing.Size(570, 1);
            this.panelSeparador.TabIndex = 3;

            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblDocumento.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblDocumento.Location = new System.Drawing.Point(25, 80);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(147, 15);
            this.lblDocumento.TabIndex = 4;
            this.lblDocumento.Text = "Documento de identidad:*";

            // 
            // txtDocumento
            // 
            this.txtDocumento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDocumento.Location = new System.Drawing.Point(28, 99);
            this.txtDocumento.MaxLength = 30;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.PlaceholderText = "Ej. P01234567 o DUI/Pasaporte";
            this.txtDocumento.Size = new System.Drawing.Size(265, 23);
            this.txtDocumento.TabIndex = 5;

            // 
            // lblNacionalidad
            // 
            this.lblNacionalidad.AutoSize = true;
            this.lblNacionalidad.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblNacionalidad.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblNacionalidad.Location = new System.Drawing.Point(320, 80);
            this.lblNacionalidad.Name = "lblNacionalidad";
            this.lblNacionalidad.Size = new System.Drawing.Size(87, 15);
            this.lblNacionalidad.TabIndex = 6;
            this.lblNacionalidad.Text = "Nacionalidad:*";

            // 
            // cboNacionalidad
            // 
            this.cboNacionalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNacionalidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNacionalidad.FormattingEnabled = true;
            this.cboNacionalidad.Location = new System.Drawing.Point(323, 99);
            this.cboNacionalidad.Name = "cboNacionalidad";
            this.cboNacionalidad.Size = new System.Drawing.Size(272, 23);
            this.cboNacionalidad.TabIndex = 7;

            // 
            // lblNombres
            // 
            this.lblNombres.AutoSize = true;
            this.lblNombres.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblNombres.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblNombres.Location = new System.Drawing.Point(25, 134);
            this.lblNombres.Name = "lblNombres";
            this.lblNombres.Size = new System.Drawing.Size(65, 15);
            this.lblNombres.TabIndex = 8;
            this.lblNombres.Text = "Nombres:*";

            // 
            // txtNombres
            // 
            this.txtNombres.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombres.Location = new System.Drawing.Point(28, 153);
            this.txtNombres.MaxLength = 50;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.PlaceholderText = "Nombres completos";
            this.txtNombres.Size = new System.Drawing.Size(265, 23);
            this.txtNombres.TabIndex = 9;

            // 
            // lblApellidos
            // 
            this.lblApellidos.AutoSize = true;
            this.lblApellidos.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblApellidos.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblApellidos.Location = new System.Drawing.Point(320, 134);
            this.lblApellidos.Name = "lblApellidos";
            this.lblApellidos.Size = new System.Drawing.Size(64, 15);
            this.lblApellidos.TabIndex = 10;
            this.lblApellidos.Text = "Apellidos:*";

            // 
            // txtApellidos
            // 
            this.txtApellidos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtApellidos.Location = new System.Drawing.Point(323, 153);
            this.txtApellidos.MaxLength = 50;
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.PlaceholderText = "Apellidos completos";
            this.txtApellidos.Size = new System.Drawing.Size(272, 23);
            this.txtApellidos.TabIndex = 11;

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblEmail.Location = new System.Drawing.Point(25, 188);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(115, 15);
            this.lblEmail.TabIndex = 12;
            this.lblEmail.Text = "Correo electrónico:*";

            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(28, 207);
            this.txtEmail.MaxLength = 80;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "ejemplo@dominio.com";
            this.txtEmail.Size = new System.Drawing.Size(265, 23);
            this.txtEmail.TabIndex = 13;

            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblTelefono.Location = new System.Drawing.Point(320, 188);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(61, 15);
            this.lblTelefono.TabIndex = 14;
            this.lblTelefono.Text = "Teléfono:*";

            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTelefono.Location = new System.Drawing.Point(323, 207);
            this.txtTelefono.MaxLength = 25;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.PlaceholderText = "+503 7000-0000";
            this.txtTelefono.Size = new System.Drawing.Size(272, 23);
            this.txtTelefono.TabIndex = 15;

            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblFechaNacimiento.Location = new System.Drawing.Point(25, 242);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(127, 15);
            this.lblFechaNacimiento.TabIndex = 16;
            this.lblFechaNacimiento.Text = "Fecha de nacimiento:*";

            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(28, 261);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(265, 23);
            this.dtpFechaNacimiento.TabIndex = 17;

            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblCategoria.Location = new System.Drawing.Point(320, 242);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(68, 15);
            this.lblCategoria.TabIndex = 18;
            this.lblCategoria.Text = "Categoría:*";

            // 
            // cboCategoria
            // 
            this.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoria.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboCategoria.FormattingEnabled = true;
            this.cboCategoria.Location = new System.Drawing.Point(323, 261);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.Size = new System.Drawing.Size(272, 23);
            this.cboCategoria.TabIndex = 19;

            // 
            // chkAsistencia
            // 
            this.chkAsistencia.AutoSize = true;
            this.chkAsistencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.chkAsistencia.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.chkAsistencia.Location = new System.Drawing.Point(28, 301);
            this.chkAsistencia.Name = "chkAsistencia";
            this.chkAsistencia.Size = new System.Drawing.Size(370, 19);
            this.chkAsistencia.TabIndex = 20;
            this.chkAsistencia.Text = "Requiere asistencia especial de abordaje / movilidad reducida";
            this.chkAsistencia.UseVisualStyleBackColor = true;

            // 
            // lblServicios
            // 
            this.lblServicios.AutoSize = true;
            this.lblServicios.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblServicios.ForeColor = System.Drawing.Color.FromArgb(30, 42, 56);
            this.lblServicios.Location = new System.Drawing.Point(25, 332);
            this.lblServicios.Name = "lblServicios";
            this.lblServicios.Size = new System.Drawing.Size(212, 15);
            this.lblServicios.TabIndex = 21;
            this.lblServicios.Text = "Servicios adicionales preferidos (opcional):";

            // 
            // clbServicios
            // 
            this.clbServicios.CheckOnClick = true;
            this.clbServicios.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.clbServicios.FormattingEnabled = true;
            this.clbServicios.Location = new System.Drawing.Point(28, 351);
            this.clbServicios.Name = "clbServicios";
            this.clbServicios.Size = new System.Drawing.Size(567, 94);
            this.clbServicios.TabIndex = 22;

            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.panelBotones.Controls.Add(this.lblAvisoIntegracion);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 502);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(620, 58);
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
            this.btnCancelar.Location = new System.Drawing.Point(375, 14);
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
            this.btnGuardar.Location = new System.Drawing.Point(490, 14);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 30);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // 
            // FrmEditarPasajero
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(620, 560);
            this.Controls.Add(this.panelFondo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditarPasajero";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar nuevo pasajero";
            this.panelFondo.ResumeLayout(false);
            this.panelFondo.PerformLayout();
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
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label lblApellidos;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblNacionalidad;
        private System.Windows.Forms.ComboBox cboNacionalidad;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.CheckBox chkAsistencia;
        private System.Windows.Forms.Label lblServicios;
        private System.Windows.Forms.CheckedListBox clbServicios;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblAvisoIntegracion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
    }
}

