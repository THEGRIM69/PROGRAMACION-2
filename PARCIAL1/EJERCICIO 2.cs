using System;

namespace EjerciciosParcial
{
    class Ejercicio2
    {
        static void Main1(string[] args)
        {
            DateTime[] fechas = new DateTime[5];

            // 1 y 2. Pedir las 5 fechas y guardarlas en un vector DateTime
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Ingrese la fecha de nacimiento {i + 1} (yyyy-MM-dd): ");
                fechas[i] = DateTime.Parse(Console.ReadLine());
            }

            // 3. Mostrar todas las fechas ingresadas
            Console.WriteLine("\n--- Fechas ingresadas ---");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Persona {i + 1}: {fechas[i].ToShortDateString()}");
            }

            // 4 y 5. Determinar y mostrar la fecha más antigua
            DateTime masAntigua = fechas[0];

            for (int i = 1; i < 5; i++)
            {
                if (fechas[i] < masAntigua)
                {
                    masAntigua = fechas[i];
                }
            }

            Console.WriteLine($"\nLa fecha de nacimiento más antigua es: {masAntigua.ToShortDateString()}");
        }
    }
}