namespace CalculadoraWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPantalla;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnIgual;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPantalla = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnIgual = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Pantalla
            this.txtPantalla.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.txtPantalla.Location = new System.Drawing.Point(12, 12);
            this.txtPantalla.Name = "txtPantalla";
            this.txtPantalla.ReadOnly = true;
            this.txtPantalla.Size = new System.Drawing.Size(236, 39);
            this.txtPantalla.TabIndex = 0;
            this.txtPantalla.Text = "0";
            this.txtPantalla.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // Arreglo con la disposición de botones
            string[,] botones = new string[,] {
                { "7", "8", "9", "/" },
                { "4", "5", "6", "*" },
                { "1", "2", "3", "-" },
                { "0", ".", "C", "+" }
            };

            int startX = 12, startY = 65;
            int width = 53, height = 45;
            int margin = 8;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string texto = botones[i, j];
                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                    btn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(width, height);
                    btn.Location = new System.Drawing.Point(startX + j * (width + margin), startY + i * (height + margin));
                    btn.Text = texto;
                    btn.UseVisualStyleBackColor = true;

                    if (texto == "C")
                    {
                        btn.Click += new System.EventHandler(this.BtnLimpiar_Click);
                    }
                    else if (texto == "+" || texto == "-" || texto == "*" || texto == "/")
                    {
                        btn.Click += new System.EventHandler(this.BotonOperador_Click);
                    }
                    else
                    {
                        btn.Click += new System.EventHandler(this.BotonNumero_Click);
                    }

                    this.Controls.Add(btn);
                }
            }

            // Botón Igual
            this.btnIgual.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnIgual.Location = new System.Drawing.Point(12, 277);
            this.btnIgual.Name = "btnIgual";
            this.btnIgual.Size = new System.Drawing.Size(236, 45);
            this.btnIgual.TabIndex = 1;
            this.btnIgual.Text = "=";
            this.btnIgual.UseVisualStyleBackColor = true;
            this.btnIgual.Click += new System.EventHandler(this.BtnIgual_Click);
            this.Controls.Add(this.btnIgual);

            // Formulario principal
            this.ClientSize = new System.Drawing.Size(260, 334);
            this.Controls.Add(this.txtPantalla);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculadora";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}