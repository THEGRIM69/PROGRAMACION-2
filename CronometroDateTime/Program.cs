using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== CRONÓMETRO CON DATETIME ===");
        Console.WriteLine("Presiona ENTER para iniciar el cronómetro...");
        Console.ReadLine();

        // 1. Guardamos la hora de inicio
        DateTime tiempoInicio = DateTime.Now;
        bool ejecutando = true;

        Console.Clear();
        Console.WriteLine("Cronómetro en marcha... Presiona cualquier tecla para detenerlo.\n");

        // Hilo secundario para detectar si el usuario presiona una tecla para detener el cronómetro
        Thread hiloEspera = new Thread(() =>
        {
            Console.ReadKey(true);
            ejecutando = false;
        });
        hiloEspera.Start();

        // 2. Bucle principal que calcula y muestra el tiempo transcurrido
        while (ejecutando)
        {
            // Calculamos la diferencia entre el instante actual y el tiempo de inicio
            TimeSpan transcurrido = DateTime.Now - tiempoInicio;

            // Muestra el tiempo formato: Horas:Minutos:Segundos.Milisegundos
            Console.SetCursorPosition(0, 3);
            Console.Write($"Tiempo: {transcurrido.Hours:D2}:{transcurrido.Minutes:D2}:{transcurrido.Seconds:D2}.{transcurrido.Milliseconds:D3}");

            // Pequeña pausa para evitar sobrecargar la CPU
            Thread.Sleep(50);
        }

        // 3. Tiempo final al detener el proceso
        DateTime tiempoFin = DateTime.Now;
        TimeSpan tiempoTotal = tiempoFin - tiempoInicio;

        Console.WriteLine("\n\n---------------------------------");
        Console.WriteLine("¡Cronómetro detenido!");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Hora de inicio: {tiempoInicio:HH:mm:ss.fff}");
        Console.WriteLine($"Hora de fin:    {tiempoFin:HH:mm:ss.fff}");
        Console.WriteLine($"Tiempo total:   {tiempoTotal.Hours:D2}:{tiempoTotal.Minutes:D2}:{tiempoTotal.Seconds:D2}.{tiempoTotal.Milliseconds:D3}");
        Console.WriteLine("---------------------------------");
    }
}