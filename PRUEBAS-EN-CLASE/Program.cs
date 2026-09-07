using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ProyectoConsolaEstilizado
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            string[] opciones = new string[]
            {
                "Conversión Implícita (int -> long)",
                "Conversión Explícita (double -> int)",
                "Operaciones Matemáticas (Clase Math)",
                "Cálculo de Precio con IVA (Float)",
                "Conversor de Número a Texto (0-5)",
                "Calculadora de Cálculo Numérico (Derivada/Integral)",
                "Cálculo de Edad y Días Transcurridos",
                "Convertidor de Zonas Horarias (El Salvador al Mundo)",
                "Calculadora de Días Hábiles",
                "Cronómetro Digital",
                "Cronómetro con Registro de Vueltas",
                "Sistema Marcador de Personal (Asistencia)",
                "Salir del Programa"
            };

            int seleccion = 0;
            bool ejecutando = true;

            while (ejecutando)
            {
                Console.Clear();
                DibujarBannerMenu();

                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == seleccion)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"  [>] {i + 1,2}. {opciones[i],-52}  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"      {i + 1,2}. {opciones[i]}");
                        Console.ResetColor();
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n┌──────────────────────────────────────────────────────────┐");
                Console.WriteLine("│ Usa [▲/▼] para navegar y presiona [ENTER] para ejecutar. │");
                Console.WriteLine("└──────────────────────────────────────────────────────────┘");
                Console.ResetColor();

                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.UpArrow)
                {
                    seleccion--;
                    if (seleccion < 0) seleccion = opciones.Length - 1;
                }
                else if (tecla.Key == ConsoleKey.DownArrow)
                {
                    seleccion++;
                    if (seleccion >= opciones.Length) seleccion = 0;
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.CursorVisible = true;

                    if (seleccion == opciones.Length - 1)
                    {
                        ejecutando = false;
                        MostrarHeaderModulo("SALIR DEL SISTEMA", ConsoleColor.DarkRed);
                        Console.WriteLine("  ¡Gracias por utilizar el sistema!");
                        Console.WriteLine();
                    }
                    else
                    {
                        EjecutarModulo(seleccion + 1);
                        PausarModulo();
                    }

                    Console.CursorVisible = false;
                }
            }
        }

        static void DibujarBannerMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            PANEL PRINCIPAL DE EJERCICIOS C#              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void MostrarHeaderModulo(string titulo, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {titulo,-56}║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void PausarModulo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("────────────────────────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Presiona cualquier tecla para regresar al menú...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        static void EjecutarModulo(int id)
        {
            switch (id)
            {
                case 1: ModuloConversionImplicita.Ejecutar(); break;
                case 2: ModuloConversionExplicita.Ejecutar(); break;
                case 3: ModuloClaseMath.Ejecutar(); break;
                case 4: ModuloCalculoIva.Ejecutar(); break;
                case 5: ModuloNumeroTexto.Ejecutar(); break;
                case 6: ModuloCalculoNumerico.Ejecutar(); break;
                case 7: ModuloCalculoEdad.Ejecutar(); break;
                case 8: ModuloZonasHorarias.Ejecutar(); break;
                case 9: ModuloDiasHabiles.Ejecutar(); break;
                case 10: ModuloCronometroSimple.Ejecutar(); break;
                case 11: ModuloCronometroVueltas.Ejecutar(); break;
                case 12: ModuloMarcadorPersonal.Ejecutar(); break;
            }
        }
    }

    // ==========================================
    // MÓDULOS DEL PROYECTO (DISEÑO ESTILIZADO)
    // ==========================================

    public static class ModuloConversionImplicita
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 01: CONVERSIÓN IMPLÍCITA", ConsoleColor.Green);

            int numeroEntero = 42;
            long numeroLargo = numeroEntero;

            Console.WriteLine("  ┌──────────────────────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  │  Valor original (int)   : {numeroEntero,-25} │");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  │  Valor convertido (long): {numeroLargo,-25} │");
            Console.ResetColor();
            Console.WriteLine("  └──────────────────────────────────────────────────────┘");

            Console.WriteLine("\n  Explicación: La conversión de 'int' a 'long' es implícita");
            Console.WriteLine("  debido a que el tipo 'long' posee mayor rango numérico.");
        }
    }

    public static class ModuloConversionExplicita
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 02: CONVERSIÓN EXPLÍCITA", ConsoleColor.Yellow);

            double sueldoOriginal = 1500.75;
            int sueldoEntero = (int)sueldoOriginal;

            Console.WriteLine("  ┌──────────────────────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  │  Sueldo original (double) : ${sueldoOriginal,-23:F2} │");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  │  Sueldo truncado (int)    : ${sueldoEntero,-23} │");
            Console.ResetColor();
            Console.WriteLine("  └──────────────────────────────────────────────────────┘");

            Console.WriteLine("\n  Explicación: El operador (int) fuerza la conversión");
            Console.WriteLine("  descartando la fracción decimal sin redondear.");
        }
    }

    public static class ModuloClaseMath
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 03: USO DE LA CLASE MATH", ConsoleColor.Magenta);

            double numero = 125.0;
            double raizCuadrada = Math.Sqrt(numero);
            double raizCubica = Math.Pow(numero, 1.0 / 3.0);

            Console.WriteLine("  ┌──────────────────────────────────────────────────────┐");
            Console.WriteLine($"  │  Número Analizado  : {numero,-31} │");
            Console.WriteLine("  ├──────────────────────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  │  Raíz Cuadrada     : {raizCuadrada,-31:F4} │");
            Console.WriteLine($"  │  Raíz Cúbica       : {raizCubica,-31:F4} │");
            Console.ResetColor();
            Console.WriteLine("  └──────────────────────────────────────────────────────┘");
        }
    }

    public static class ModuloCalculoIva
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 04: CÁLCULO DE PRECIO CON IVA", ConsoleColor.Green);

            float precioProducto = 45.50f;
            float porcentajeIva = 0.16f;
            float montoIva = precioProducto * porcentajeIva;
            float precioTotal = precioProducto + montoIva;

            Console.WriteLine("  ┌──────────────────────────────────────────┐");
            Console.WriteLine($"  │  Precio Base : ${precioProducto,10:F2}               │");
            Console.WriteLine($"  │  Monto IVA   : ${montoIva,10:F2}               │");
            Console.WriteLine("  ├──────────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  │  TOTAL       : ${precioTotal,10:F2}               │");
            Console.ResetColor();
            Console.WriteLine("  └──────────────────────────────────────────┘");
        }
    }

    public static class ModuloNumeroTexto
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 05: NUMERO A TEXTO (0 - 5)", ConsoleColor.Blue);

            Console.Write("  Ingresa un número entero (0 al 5): ");
            string entrada = Console.ReadLine() ?? "";
            
            string[] palabras = { "Cero", "Uno", "Dos", "Tres", "Cuatro", "Cinco" };

            Console.WriteLine();
            if (int.TryParse(entrada, out int num) && num >= 0 && num <= 5)
            {
                Console.WriteLine("  ┌──────────────────────────────────────────┐");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  │  Resultado en texto: {palabras[num],-19} │");
                Console.ResetColor();
                Console.WriteLine("  └──────────────────────────────────────────┘");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [!] Error: Valor fuera de rango o inválido.");
                Console.ResetColor();
            }
        }
    }

    public static class ModuloCalculoNumerico
    {
        static double F(double x) => Math.Pow(x, 2);

        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 06: CÁLCULO NUMÉRICO f(x) = x²", ConsoleColor.DarkYellow);

            Console.WriteLine("  1. Calcular Derivada en un punto");
            Console.WriteLine("  2. Calcular Integral definida");
            Console.Write("\n  Selecciona una opción (1 o 2): ");
            string opcion = Console.ReadLine() ?? "";

            Console.WriteLine();
            if (opcion == "1")
            {
                Console.Write("  Ingresa el valor de 'x': ");
                if (double.TryParse(Console.ReadLine(), out double x))
                {
                    double h = 0.000001;
                    double derivada = (F(x + h) - F(x - h)) / (2 * h);
                    Console.WriteLine("\n  ┌──────────────────────────────────────────┐");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  │  f'({x}): {derivada,-30:F6} │");
                    Console.ResetColor();
                    Console.WriteLine("  └──────────────────────────────────────────┘");
                }
            }
            else if (opcion == "2")
            {
                Console.Write("  Límite inferior (a): ");
                double.TryParse(Console.ReadLine(), out double a);
                Console.Write("  Límite superior (b): ");
                double.TryParse(Console.ReadLine(), out double b);

                int n = 1000;
                double h = (b - a) / n;
                double suma = 0;
                for (int i = 0; i <= n; i++)
                {
                    double x = a + i * h;
                    double y = F(x);
                    suma += (i == 0 || i == n) ? y : 2 * y;
                }
                double integral = (h / 2) * suma;

                Console.WriteLine("\n  ┌──────────────────────────────────────────┐");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  │  ∫ x² dx [{a},{b}] : {integral,-20:F6} │");
                Console.ResetColor();
                Console.WriteLine("  └──────────────────────────────────────────┘");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [!] Opción no válida.");
                Console.ResetColor();
            }
        }
    }

    public static class ModuloCalculoEdad
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 07: CÁLCULO DE EDAD", ConsoleColor.Cyan);

            Console.Write("  Ingresa tu fecha de nacimiento (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime fechaNacimiento))
            {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - fechaNacimiento.Year;
                if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
                TimeSpan diferencia = hoy - fechaNacimiento;

                Console.WriteLine("\n  ┌──────────────────────────────────────────┐");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  │  Edad actual       : {edad,-3} años            │");
                Console.WriteLine($"  │  Días transcurridos: {diferencia.TotalDays,-12:N0} días   │");
                Console.ResetColor();
                Console.WriteLine("  └──────────────────────────────────────────┘");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [!] Formato de fecha incorrecto.");
                Console.ResetColor();
            }
        }
    }

    public static class ModuloZonasHorarias
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 08: ZONAS HORARIAS MUNDIALES", ConsoleColor.DarkMagenta);

            try
            {
                TimeZoneInfo zonaElSalvador = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
                DateTimeOffset horaElSalvador = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, zonaElSalvador);

                Console.WriteLine($"  Base El Salvador: {horaElSalvador:dd/MM/yyyy - hh:mm tt}\n");

                var paises = new (string Pais, string TimeZoneId)[]
                {
                    ("España (Madrid)", "Romance Standard Time"),
                    ("Argentina (Buenos Aires)", "Argentina Standard Time"),
                    ("México (Ciudad de México)", "Central Standard Time (Mexico)"),
                    ("Estados Unidos (NY)", "Eastern Standard Time"),
                    ("Japón (Tokio)", "Tokyo Standard Time"),
                    ("Reino Unido (Londres)", "GMT Standard Time")
                };

                Console.WriteLine("  ┌──────────────────────────┬────────────────────────┬─────────┐");
                Console.WriteLine("  │ PAÍS / CIUDAD            │ HORA RESULTANTE        │ DIF.    │");
                Console.WriteLine("  ├──────────────────────────┼────────────────────────┼─────────┤");

                foreach (var (pais, timeZoneId) in paises)
                {
                    try
                    {
                        TimeZoneInfo zonaDestino = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                        DateTimeOffset horaDestino = TimeZoneInfo.ConvertTime(horaElSalvador, zonaDestino);
                        double diferenciaHoras = (horaDestino.Offset - horaElSalvador.Offset).TotalHours;
                        string textoDif = diferenciaHoras >= 0 ? $"+{diferenciaHoras}h" : $"{diferenciaHoras}h";

                        Console.WriteLine($"  │ {pais,-24} │ {horaDestino:dd/MM/yyyy hh:mm tt} │ {textoDif,7} │");
                    }
                    catch
                    {
                        Console.WriteLine($"  │ {pais,-24} │ No disponible          │ --      │");
                    }
                }
                Console.WriteLine("  └──────────────────────────┴────────────────────────┴─────────┘");
            }
            catch (Exception ex)
            {
                Console.WriteLine("  [!] Error procesando zonas horarias: " + ex.Message);
            }
        }
    }

    public static class ModuloDiasHabiles
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 09: CÁLCULO DE DÍAS HÁBILES", ConsoleColor.Green);

            DateTime fechaInicio = DateTime.Now;
            int diasASumar = 10;
            DateTime fechaFinal = SumarDiasHabiles(fechaInicio, diasASumar);

            Console.WriteLine("  ┌────────────────────────────────────────────────────────┐");
            Console.WriteLine($"  │  Fecha actual       : {fechaInicio:dd/MM/yyyy (ddd),-31} │");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  │  +10 Días Hábiles   : {fechaFinal:dd/MM/yyyy (ddd),-31} │");
            Console.ResetColor();
            Console.WriteLine("  └────────────────────────────────────────────────────────┘");
        }

        static DateTime SumarDiasHabiles(DateTime fecha, int dias)
        {
            DateTime actual = fecha;
            int agregados = 0;
            while (agregados < dias)
            {
                actual = actual.AddDays(1);
                if (actual.DayOfWeek != DayOfWeek.Saturday && actual.DayOfWeek != DayOfWeek.Sunday)
                    agregados++;
            }
            return actual;
        }
    }

    public static class ModuloCronometroSimple
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 10: CRONÓMETRO DIGITAL", ConsoleColor.Yellow);
            Console.WriteLine("  Controles: [Espacio] Pausar/Reanudar | [R] Reiniciar | [ESC] Salir\n");

            TimeSpan tiempoAcumulado = TimeSpan.Zero;
            DateTime inicio = DateTime.Now;
            bool corriendo = true;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(intercept: true).Key;
                    if (tecla == ConsoleKey.Escape) break;
                    else if (tecla == ConsoleKey.Spacebar)
                    {
                        if (corriendo) tiempoAcumulado += DateTime.Now - inicio;
                        else inicio = DateTime.Now;
                        corriendo = !corriendo;
                    }
                    else if (tecla == ConsoleKey.R)
                    {
                        tiempoAcumulado = TimeSpan.Zero;
                        inicio = DateTime.Now;
                    }
                }

                TimeSpan tiempoTotal = corriendo ? tiempoAcumulado + (DateTime.Now - inicio) : tiempoAcumulado;
                Console.SetCursorPosition(0, 8);
                string estado = corriendo ? "[CORRIENDO]" : "[PAUSADO]  ";
                Console.ForegroundColor = corriendo ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"  Tiempo: {tiempoTotal:hh\\:mm\\:ss\\.ff} {estado}");
                Console.ResetColor();
                Thread.Sleep(50);
            }
        }
    }

    public static class ModuloCronometroVueltas
    {
        public static void Ejecutar()
        {
            Program.MostrarHeaderModulo("MÓDULO 11: CRONÓMETRO CON VUELTAS", ConsoleColor.Yellow);
            Console.WriteLine("  Controles: [Espacio] Pausar | [L] Registrar Vuelta | [R] Reiniciar | [ESC] Salir\n");

            TimeSpan tiempoAcumulado = TimeSpan.Zero;
            DateTime inicio = DateTime.Now;
            bool corriendo = true;
            List<(int Numero, TimeSpan TiempoTotal, TimeSpan TiempoLap)> vueltas = new();
            TimeSpan ultimoTiempoLap = TimeSpan.Zero;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(intercept: true).Key;
                    if (tecla == ConsoleKey.Escape) break;
                    else if (tecla == ConsoleKey.Spacebar)
                    {
                        if (corriendo) tiempoAcumulado += DateTime.Now - inicio;
                        else inicio = DateTime.Now;
                        corriendo = !corriendo;
                    }
                    else if (tecla == ConsoleKey.L && corriendo)
                    {
                        TimeSpan tiempoActual = tiempoAcumulado + (DateTime.Now - inicio);
                        TimeSpan tiempoLap = tiempoActual - ultimoTiempoLap;
                        ultimoTiempoLap = tiempoActual;
                        vueltas.Add((vueltas.Count + 1, tiempoActual, tiempoLap));
                    }
                    else if (tecla == ConsoleKey.R)
                    {
                        tiempoAcumulado = TimeSpan.Zero;
                        inicio = DateTime.Now;
                        ultimoTiempoLap = TimeSpan.Zero;
                        vueltas.Clear();
                    }
                }

                TimeSpan tiempoTotal = corriendo ? tiempoAcumulado + (DateTime.Now - inicio) : tiempoAcumulado;
                Console.SetCursorPosition(0, 8);
                string estado = corriendo ? "[CORRIENDO]" : "[PAUSADO]  ";
                
                string totalStr = $"{tiempoTotal.Hours:D2}:{tiempoTotal.Minutes:D2}:{tiempoTotal.Seconds:D2}.{tiempoTotal.Milliseconds / 10:D2}";
                Console.WriteLine($"  Tiempo: {totalStr} {estado}\n");

                Console.WriteLine("  ┌──────────┬──────────────────┬──────────────────┐");
                Console.WriteLine("  │ VUELTA   │ TIEMPO VUELTA    │ TIEMPO TOTAL     │");
                Console.WriteLine("  ├──────────┼──────────────────┼──────────────────┤");

                for (int i = vueltas.Count - 1; i >= 0; i--)
                {
                    var v = vueltas[i];
                    string lapStr = $"{v.TiempoLap.Minutes:D2}:{v.TiempoLap.Seconds:D2}.{v.TiempoLap.Milliseconds / 10:D2}";
                    string lapTotalStr = $"{v.TiempoTotal.Hours:D2}:{v.TiempoTotal.Minutes:D2}:{v.TiempoTotal.Seconds:D2}.{v.TiempoTotal.Milliseconds / 10:D2}";
                    
                    Console.WriteLine($"  │ Vuelta {v.Numero,-2} │ {lapStr,-16} │ {lapTotalStr,-16} │");
                }
                Console.WriteLine("  └──────────┴──────────────────┴──────────────────┘");

                Thread.Sleep(50);
            }
        }
    }

    public static class ModuloMarcadorPersonal
    {
        public class RegistroAsistencia
        {
            public string EmpleadoId { get; set; } = string.Empty;
            public DateTime HoraEntrada { get; set; }
            public DateTime? HoraSalida { get; set; }
        }

        public static void Ejecutar()
        {
            List<RegistroAsistencia> historial = new();
            bool dentroModulo = true;

            while (dentroModulo)
            {
                Console.Clear();
                Program.MostrarHeaderModulo("MÓDULO 12: MARCADOR DE ASISTENCIA", ConsoleColor.Green);
                Console.WriteLine($"  Fecha Actual: {DateTime.Now:dd/MM/yyyy - hh:mm:ss tt}\n");
                Console.WriteLine("  1. Marcar Entrada");
                Console.WriteLine("  2. Marcar Salida");
                Console.WriteLine("  3. Ver Historial");
                Console.WriteLine("  4. Volver al Menú Principal");
                Console.Write("\n  Selecciona una opción (1-4): ");

                string opcion = Console.ReadLine() ?? "";
                switch (opcion)
                {
                    case "1": MarcarEntrada(historial); break;
                    case "2": MarcarSalida(historial); break;
                    case "3": MostrarHistorial(historial); break;
                    case "4": dentroModulo = false; break;
                    default:
                        Console.WriteLine("\n  [!] Opción no válida.");
                        Pausar();
                        break;
                }
            }
        }

        static void MarcarEntrada(List<RegistroAsistencia> historial)
        {
            Console.Clear();
            Program.MostrarHeaderModulo("REGISTRO DE ENTRADA", ConsoleColor.Green);
            Console.Write("  Código de Empleado: ");
            string idEmpleado = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(idEmpleado))
            {
                Console.WriteLine("\n  [!] El ID no puede estar vacío.");
                Pausar();
                return;
            }

            var registroPendiente = historial.FirstOrDefault(r => r.EmpleadoId == idEmpleado && r.HoraSalida == null);
            if (registroPendiente != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  [!] El empleado {idEmpleado} ya posee entrada activa.");
                Console.ResetColor();
            }
            else
            {
                DateTime ahora = DateTime.Now;
                historial.Add(new RegistroAsistencia { EmpleadoId = idEmpleado, HoraEntrada = ahora });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [√] Entrada registrada para {idEmpleado} a las {ahora:hh:mm:ss tt}");
                Console.ResetColor();
            }
            Pausar();
        }

        static void MarcarSalida(List<RegistroAsistencia> historial)
        {
            Console.Clear();
            Program.MostrarHeaderModulo("REGISTRO DE SALIDA", ConsoleColor.Red);
            Console.Write("  Código de Empleado: ");
            string idEmpleado = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(idEmpleado))
            {
                Console.WriteLine("\n  [!] El ID no puede estar vacío.");
                Pausar();
                return;
            }

            var registro = historial.FirstOrDefault(r => r.EmpleadoId == idEmpleado && r.HoraSalida == null);
            if (registro == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [!] No existe entrada sin salida para el empleado: {idEmpleado}");
                Console.ResetColor();
            }
            else
            {
                DateTime ahora = DateTime.Now;
                registro.HoraSalida = ahora;
                TimeSpan tiempoTrabajado = ahora - registro.HoraEntrada;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [√] Salida registrada para {idEmpleado}.");
                Console.WriteLine($"  Tiempo laborado: {tiempoTrabajado.Hours}h {tiempoTrabajado.Minutes}m {tiempoTrabajado.Seconds}s");
                Console.ResetColor();
            }
            Pausar();
        }

        static void MostrarHistorial(List<RegistroAsistencia> historial)
        {
            Console.Clear();
            Program.MostrarHeaderModulo("HISTORIAL DE ASISTENCIA", ConsoleColor.Cyan);

            if (!historial.Any())
            {
                Console.WriteLine("  No existen registros aún.");
            }
            else
            {
                Console.WriteLine("  ┌──────────────┬────────────┬─────────────┬─────────────┬──────────────┐");
                Console.WriteLine("  │ EMPLEADO     │ FECHA      │ ENTRADA     │ SALIDA      │ TIEMPO       │");
                Console.WriteLine("  ├──────────────┼────────────┼─────────────┼─────────────┼──────────────┤");

                foreach (var r in historial)
                {
                    string entradaStr = r.HoraEntrada.ToString("hh:mm:ss tt");
                    string salidaStr = r.HoraSalida.HasValue ? r.HoraSalida.Value.ToString("hh:mm:ss tt") : "-- PENDIENTE --";
                    string tiempoStr = r.HoraSalida.HasValue ? $"{(r.HoraSalida.Value - r.HoraEntrada).Hours:D2}h {(r.HoraSalida.Value - r.HoraEntrada).Minutes:D2}m" : "--";
                    Console.WriteLine($"  │ {r.EmpleadoId,-12} │ {r.HoraEntrada:dd/MM/yyyy} │ {entradaStr,-11} │ {salidaStr,-11} │ {tiempoStr,-12} │");
                }
                Console.WriteLine("  └──────────────┴────────────┴─────────────┴─────────────┴──────────────┘");
            }
            Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\n  Presiona ENTER para continuar...");
            Console.ReadLine();
        }
    }
}