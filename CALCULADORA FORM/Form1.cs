using System;
using System.Windows.Forms;

namespace CalculadoraWinForms
{
    public partial class Form1 : Form
    {
        private double valorActual = 0;
        private string operador = "";
        private bool esNUEVAOperacion = true;

        public Form1()
        {
            InitializeComponent();
        }

        // Evento compartido para todos los botones numéricos (0-9) y la coma/punto
        private void BotonNumero_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            if (txtPantalla.Text == "0" || esNUEVAOperacion)
            {
                txtPantalla.Text = "";
                esNUEVAOperacion = false;
            }

            if (boton.Text == ".")
            {
                if (!txtPantalla.Text.Contains("."))
                    txtPantalla.Text += ".";
            }
            else
            {
                txtPantalla.Text += boton.Text;
            }
        }

        // Evento para los operadores (+, -, *, /)
        private void BotonOperador_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            if (valorActual != 0 && !esNUEVAOperacion)
            {
                btnIgual.PerformClick();
            }

            operador = boton.Text;
            valorActual = double.Parse(txtPantalla.Text);
            esNUEVAOperacion = true;
        }

        // Evento para calcular el resultado (=)
        private void BtnIgual_Click(object sender, EventArgs e)
        {
            double segundoValor = double.Parse(txtPantalla.Text);
            double resultado = 0;

            switch (operador)
            {
                case "+": resultado = valorActual + segundoValor; break;
                case "-": resultado = valorActual - segundoValor; break;
                case "*": resultado = valorActual * segundoValor; break;
                case "/":
                    if (segundoValor != 0)
                        resultado = valorActual / segundoValor;
                    else
                        MessageBox.Show("No se puede dividir por cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            txtPantalla.Text = resultado.ToString();
            valorActual = 0;
            operador = "";
            esNUEVAOperacion = true;
        }

        // Evento para limpiar la pantalla (C)
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtPantalla.Text = "0";
            valorActual = 0;
            operador = "";
            esNUEVAOperacion = true;
        }
    }
}