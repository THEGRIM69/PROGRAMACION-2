using System;

namespace EjerciciosParcial
{
    class Ejercicio1
    {
        public static void Main1(string[] args)
        {
            int[] numeros = new int[10];

            // Lectura de los 10 números
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Ingrese el número {i + 1}: ");
                numeros[i] = int.Parse(Console.ReadLine());
            }

            // Inicializamos mayor y menor con el primer elemento del vector
            int mayor = numeros[0];
            int menor = numeros[0];

            // Recorremos el vector para comparar
            for (int i = 1; i < 10; i++)
            {
                if (numeros[i] > mayor)
                {
                    mayor = numeros[i];
                }

                if (numeros[i] < menor)
                {
                    menor = numeros[i];
                }
            }

            Console.WriteLine($"\nEl número mayor es: {mayor}");
            Console.WriteLine($"El número menor es: {menor}");
        }
    }
}