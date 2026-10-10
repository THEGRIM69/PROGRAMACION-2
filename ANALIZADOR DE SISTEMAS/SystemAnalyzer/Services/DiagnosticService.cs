using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public sealed class DiagnosticService
{
    public DiagnosticReport Evaluate(
        SystemInfo? system,
        IReadOnlyList<DriveInfoModel>? drives,
        IReadOnlyList<ProcessInfo>? processes)
    {
        var results = new[]
        {
            EvaluateCpu(system?.CpuUsagePercent),
            EvaluateMemory(system),
            EvaluateStorage(drives),
            EvaluateProcesses(processes, system?.TotalMemoryBytes ?? 0)
        };
        var available = results.Where(result => result.Status != DiagnosticStatus.NoData).ToArray();
        var partial = available.Length != results.Length || results.Any(result => result.IsPartial);
        if (available.Length == 0)
        {
            return new DiagnosticReport
            {
                OverallStatus = DiagnosticStatus.NoData,
                IsPartial = true,
                Summary = "No hay mediciones válidas suficientes para resumir el estado del equipo.",
                Results = results
            };
        }

        var status = available.MaxBy(result => Severity(result.Status))!.Status;
        var conclusion = status switch
        {
            DiagnosticStatus.Critical => "Hay una medición en nivel crítico que merece revisión.",
            DiagnosticStatus.Attention => "Hay mediciones que conviene observar y revisar.",
            _ => "Las mediciones disponibles están dentro de los umbrales preventivos."
        };
        return new DiagnosticReport
        {
            OverallStatus = status,
            IsPartial = partial,
            Summary = partial
                ? $"Evaluación parcial. {conclusion} Algunos indicadores no pudieron evaluarse."
                : conclusion,
            Results = results
        };
    }

    internal static DiagnosticResult EvaluateCpu(double? usage)
    {
        if (!IsValidPercent(usage)) return NoData("Procesador", "Uso de CPU", "No se obtuvo una muestra válida de CPU.");
        var value = usage!.Value;
        var status = Classify(value, 75, 90);
        return Result("Procesador", status, $"{value:0.#}% en esta muestra",
            "La medición refleja actividad momentánea y no demuestra daño físico del procesador.",
            status == DiagnosticStatus.Correct
                ? "Continúa observando el uso durante tus tareas habituales."
                : "Cierra solo aplicaciones que reconozcas y observa si el uso elevado se mantiene.",
            "Uso de CPU · SystemInfoService");
    }

    internal static DiagnosticResult EvaluateMemory(SystemInfo? system)
    {
        if (system is null || system.TotalMemoryBytes == 0 || system.AvailableMemoryBytes > system.TotalMemoryBytes)
            return NoData("Memoria RAM", "Memoria física", "Los valores total y disponible no forman una medición válida.");
        var used = system.TotalMemoryBytes - system.AvailableMemoryBytes;
        var usage = used * 100d / system.TotalMemoryBytes;
        if (!IsValidPercent(usage)) return NoData("Memoria RAM", "Memoria física", "No fue posible calcular un porcentaje válido.");
        var status = Classify(usage, 75, 90);
        return Result("Memoria RAM", status, $"{usage:0.#}% utilizado ({FormatBytes(used)} de {FormatBytes(system.TotalMemoryBytes)})",
            "Un consumo alto indica presión de memoria en este momento; no demuestra una falla del módulo físico.",
            status == DiagnosticStatus.Correct
                ? "No se requiere una acción inmediata basada en esta muestra."
                : "Revisa las aplicaciones abiertas y observa si el consumo continúa elevado.",
            "Memoria física · SystemInfoService");
    }

    internal static DiagnosticResult EvaluateStorage(IReadOnlyList<DriveInfoModel>? drives)
    {
        if (drives is null || drives.Count == 0)
            return NoData("Almacenamiento", "Unidades accesibles", "No se recibieron unidades listas y accesibles.");
        var valid = drives
            .Where(drive => HasValidStorageData(drive.TotalBytes, drive.AvailableBytes))
            .Select(drive => (Drive: drive, Usage: (drive.TotalBytes - drive.AvailableBytes) * 100d / drive.TotalBytes))
            .ToArray();
        if (valid.Length == 0)
            return NoData("Almacenamiento", "Unidades accesibles", "Las unidades detectadas no contienen datos de capacidad consistentes.");
        var worst = valid.MaxBy(item => item.Usage);
        var status = Classify(worst.Usage, 80, 90);
        var invalidCount = drives.Count - valid.Length;
        var coverage = invalidCount == 0 ? $"{valid.Length} unidad(es) evaluada(s)" : $"{valid.Length} evaluada(s), {invalidCount} sin datos válidos";
        return Result("Almacenamiento", status, $"{worst.Usage:0.#}% máximo · {coverage}",
            "La clasificación describe ocupación de espacio, no desgaste ni salud física del dispositivo.",
            status == DiagnosticStatus.Correct
                ? "Conserva espacio para actualizaciones y copias de seguridad periódicas."
                : "Revisa manualmente archivos grandes y usa las herramientas oficiales de Windows si necesitas liberar espacio.",
            "Capacidad de unidades · StorageAnalyzerService", invalidCount > 0);
    }

    internal static DiagnosticResult EvaluateProcesses(IReadOnlyList<ProcessInfo>? processes, ulong totalMemoryBytes)
    {
        if (processes is null || processes.Count == 0)
            return NoData("Procesos", "Procesos en ejecución", "No se obtuvo una lista válida de procesos.");
        var valid = processes.Where(process => process.WorkingSetBytes >= 0 && !string.IsNullOrWhiteSpace(process.Name)).ToArray();
        if (valid.Length == 0)
            return NoData("Procesos", "Procesos en ejecución", "No se obtuvo una lista válida de procesos.");
        var invalidCount = processes.Count - valid.Length;
        var highest = valid.MaxBy(process => process.WorkingSetBytes)!;
        var level = ProcessConsumptionClassifier.Classify(highest.WorkingSetBytes, totalMemoryBytes);
        var status = level == ConsumptionLevel.High ? DiagnosticStatus.Attention : DiagnosticStatus.Correct;
        return Result("Procesos", status, $"Mayor uso de RAM: {highest.Name} · {FormatBytes((ulong)highest.WorkingSetBytes)}",
            "Es una observación de consumo. No clasifica el proceso como amenaza, innecesario ni defectuoso.",
            status == DiagnosticStatus.Attention
                ? "Verifica que reconoces la aplicación y observa su consumo antes de tomar cualquier acción."
                : "No se recomienda finalizar procesos basándose únicamente en esta muestra.",
            "Procesos y memoria de trabajo · ProcessAnalyzerService", invalidCount > 0);
    }

    internal static bool HasValidStorageData(long totalBytes, long availableBytes) =>
        totalBytes > 0 && availableBytes >= 0 && availableBytes <= totalBytes;

    private static DiagnosticStatus Classify(double value, double attention, double critical) =>
        value >= critical ? DiagnosticStatus.Critical : value >= attention ? DiagnosticStatus.Attention : DiagnosticStatus.Correct;

    private static bool IsValidPercent(double? value) => value.HasValue && double.IsFinite(value.Value) && value.Value >= 0 && value.Value <= 100;
    private static int Severity(DiagnosticStatus status) => status switch { DiagnosticStatus.Critical => 3, DiagnosticStatus.Attention => 2, DiagnosticStatus.Correct => 1, _ => 0 };

    private static DiagnosticResult NoData(string indicator, string source, string explanation) =>
        Result(indicator, DiagnosticStatus.NoData, "No disponible", explanation,
            "Vuelve a actualizar. Si el dato sigue ausente, interpreta el diagnóstico como parcial.", source);

    private static DiagnosticResult Result(string indicator, DiagnosticStatus status, string value, string explanation, string recommendation, string source, bool isPartial = false) => new()
    {
        Indicator = indicator, Status = status, ObservedValue = value, Explanation = explanation,
        Recommendation = recommendation, Source = source, IsPartial = isPartial
    };

    private static string FormatBytes(ulong bytes)
    {
        const double gigabyte = 1024d * 1024 * 1024;
        const double megabyte = 1024d * 1024;
        return bytes >= gigabyte ? $"{bytes / gigabyte:0.##} GB" : $"{bytes / megabyte:0.##} MB";
    }
}
