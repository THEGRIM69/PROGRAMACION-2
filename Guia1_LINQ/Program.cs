using System;
using System.Collections.Generic;
using System.Linq;

class Estudiante
{
    public string Carnet { get; set; }
    public string Nombre { get; set; }
    public string Asignatura { get; set; }
    public double Nota1 { get; set; }
    public double Nota2 { get; set; }
    public double Nota3 { get; set; }
}

class Program
{
    static void Main()
    {
        // === CREAR Y AGREGAR ESTUDIANTES ===
        List<Estudiante> estudiantes = new List<Estudiante>
        {
            new Estudiante { Carnet = "20250001", Nombre = "Carlos", Asignatura = "Programacion", Nota1 = 8, Nota2 = 7, Nota3 = 9 },
            new Estudiante { Carnet = "20250002", Nombre = "Ana", Asignatura = "Programacion", Nota1 = 5, Nota2 = 6, Nota3 = 5 },
            new Estudiante { Carnet = "20250003", Nombre = "Maria", Asignatura = "Programacion", Nota1 = 9, Nota2 = 10, Nota3 = 9 },
            new Estudiante { Carnet = "20250004", Nombre = "Jose", Asignatura = "Programacion", Nota1 = 4, Nota2 = 5, Nota3 = 6 },
            new Estudiante { Carnet = "20250005", Nombre = "Luis", Asignatura = "Programacion", Nota1 = 7, Nota2 = 8, Nota3 = 7 },
            new Estudiante { Carnet = "20250006", Nombre = "Sofia", Asignatura = "Programacion", Nota1 = 10, Nota2 = 9, Nota3 = 10 }
        };

        // 1. WHERE: Operaciones solicitadas
        Console.WriteLine("==========================================");
        Console.WriteLine("1. WHERE - ESTUDIANTES CON NOTA 1 MAYOR A 6");
        Console.WriteLine("==========================================");
        var aprobadosNota1 = estudiantes.Where(e => e.Nota1 > 6);
        foreach (var e in aprobadosNota1)
        {
            Console.WriteLine($"{e.Carnet} - {e.Nombre} - Nota 1: {e.Nota1}");
        }

        Console.WriteLine("\n==========================================");
        Console.WriteLine("1. WHERE - ESTUDIANTES CON NOTA FINAL >= 6");
        Console.WriteLine("==========================================");
        var aprobados = estudiantes.Where(e => (e.Nota1 + e.Nota2 + e.Nota3) / 3 >= 6);
        foreach (var e in aprobados)
        {
            double promedio = (e.Nota1 + e.Nota2 + e.Nota3) / 3;
            Console.WriteLine($"{e.Carnet} - {e.Nombre} - Nota final: {promedio:F2}");
        }

        // 2. SELECT: Mostrar únicamente el nombre y la nota final
        Console.WriteLine("\n==========================================");
        Console.WriteLine("2. SELECT - NOMBRE Y NOTA FINAL");
        Console.WriteLine("==========================================");
        var informacion = estudiantes.Select(e => new
        {
            Nombre = e.Nombre,
            NotaFinal = (e.Nota1 + e.Nota2 + e.Nota3) / 3
        });
        foreach (var e in informacion)
        {
            Console.WriteLine($"{e.Nombre} - Nota final: {e.NotaFinal:F2}");
        }

        // 3. COUNT: Determinar cuántos estudiantes aprobaron
        Console.WriteLine("\n==========================================");
        Console.WriteLine("3. COUNT - CANTIDAD DE APROBADOS");
        Console.WriteLine("==========================================");
        int cantidadAprobados = estudiantes.Count(e => (e.Nota1 + e.Nota2 + e.Nota3) / 3 >= 6);
        Console.WriteLine($"Cantidad de estudiantes aprobados: {cantidadAprobados}");

        // 4. SUM: Calcular la suma de las notas finales
        Console.WriteLine("\n==========================================");
        Console.WriteLine("4. SUM - SUMA DE NOTAS FINALES");
        Console.WriteLine("==========================================");
        double sumaNotas = estudiantes.Sum(e => (e.Nota1 + e.Nota2 + e.Nota3) / 3);
        Console.WriteLine($"Suma de todas las notas finales: {sumaNotas:F2}");

        // 5. ORDERBY: Ordenar de menor a mayor según la nota final
        Console.WriteLine("\n==========================================");
        Console.WriteLine("5. ORDERBY - MENOR A MAYOR POR NOTA FINAL");
        Console.WriteLine("==========================================");
        var ordenAscendente = estudiantes.OrderBy(e => (e.Nota1 + e.Nota2 + e.Nota3) / 3);
        foreach (var e in ordenAscendente)
        {
            double promedio = (e.Nota1 + e.Nota2 + e.Nota3) / 3;
            Console.WriteLine($"{e.Nombre} - Nota final: {promedio:F2}");
        }

        // 6. ORDERBYDESCENDING: Ordenar de mayor a menor según la nota final
        Console.WriteLine("\n==========================================");
        Console.WriteLine("6. ORDERBYDESCENDING - MAYOR A MENOR POR NOTA FINAL");
        Console.WriteLine("==========================================");
        var ordenDescendente = estudiantes.OrderByDescending(e => (e.Nota1 + e.Nota2 + e.Nota3) / 3);
        foreach (var e in ordenDescendente)
        {
            double promedio = (e.Nota1 + e.Nota2 + e.Nota3) / 3;
            Console.WriteLine($"{e.Nombre} - Nota final: {promedio:F2}");
        }

        // 7. BÚSQUEDA EXACTA: Buscar por número de carnet
        Console.WriteLine("\n==========================================");
        Console.WriteLine("7. BÚSQUEDA EXACTA POR CARNET");
        Console.WriteLine("==========================================");
        Console.Write("Ingrese el carnet a buscar: ");
        string carnetBuscado = Console.ReadLine();

        var estudianteEncontrado = estudiantes.FirstOrDefault(e => e.Carnet == carnetBuscado);

        if (estudianteEncontrado != null)
        {
            double promedio = (estudianteEncontrado.Nota1 + estudianteEncontrado.Nota2 + estudianteEncontrado.Nota3) / 3;
            Console.WriteLine("\nEstudiante encontrado:");
            Console.WriteLine($"Carnet: {estudianteEncontrado.Carnet}");
            Console.WriteLine($"Nombre: {estudianteEncontrado.Nombre}");
            Console.WriteLine($"Asignatura: {estudianteEncontrado.Asignatura}");
            Console.WriteLine($"Nota 1: {estudianteEncontrado.Nota1}");
            Console.WriteLine($"Nota 2: {estudianteEncontrado.Nota2}");
            Console.WriteLine($"Nota 3: {estudianteEncontrado.Nota3}");
            Console.WriteLine($"Nota final: {promedio:F2}");
        }
        else
        {
            Console.WriteLine("No se encontró ningún estudiante con ese carnet.");
        }
    }
}