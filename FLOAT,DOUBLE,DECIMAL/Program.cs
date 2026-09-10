using System;

namespace CalculoEmpleados
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Cálculo de Planilla ===");

            // Bucle para procesar los datos de 5 empleados
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\n--- Ingrese los datos del Empleado {i} ---");

                // Solicitar datos
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Salario base: $");
                decimal salarioBase = decimal.Parse(Console.ReadLine());

                Console.Write("Horas extras trabajadas: ");
                // Usamos double o decimal para las horas en caso de que haya fracciones (ej. 2.5 horas)
                decimal horasExtras = decimal.Parse(Console.ReadLine());

                Console.Write("Pago por hora extra: $");
                decimal pagoPorHora = decimal.Parse(Console.ReadLine());

                // Cálculos correspondientes
                decimal pagoHorasExtras = horasExtras * pagoPorHora;
                decimal salarioBruto = salarioBase + pagoHorasExtras;
                decimal descuento = salarioBruto * 0.10m; // La 'm' indica que es un valor decimal
                decimal salarioNeto = salarioBruto - descuento;

                // Mostrar los resultados
                Console.WriteLine($"\n>> Resumen de pago para {nombre} <<");
                Console.WriteLine($"- Pago por horas extras: ${pagoHorasExtras:F2}");
                Console.WriteLine($"- Salario bruto:         ${salarioBruto:F2}");
                Console.WriteLine($"- Descuento (10%):       ${descuento:F2}");
                Console.WriteLine($"- Salario neto:          ${salarioNeto:F2}");
                Console.WriteLine("------------------------------------------");
            }

            Console.WriteLine("\nProceso finalizado. Presione cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}