using System;

namespace EjerciciosParcial
{
    class Ejercicio3
    {
        static void Main(string[] args)
        {
            int[] numeros = new int[5];

            // Ingresar los números
            for (int i = 0; i < 5; i++)
            {
                Console.Write("Ingrese el número " + (i + 1) + ": ");
                numeros[i] = int.Parse(Console.ReadLine() ?? "0");
            }

            // Buscar el mayor
            // Corrección 1: Inicializar 'mayor' con la primera posición del arreglo (evita errores con números negativos)
            int mayor = numeros[0];

            for (int i = 1; i < 5; i++)
            {
                // Corrección 2: Cambiar el signo '<' por '>' para buscar el MAYOR
                if (numeros[i] > mayor)
                {
                    mayor = numeros[i];
                }
            }

            // Corrección 3: 'WriteLine' con 'L' mayúscula
            Console.WriteLine("\nEl número mayor es: " + mayor);
        }
    }
}