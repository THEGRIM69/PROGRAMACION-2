namespace SystemAnalyzer.Models;

public sealed class ProcessInfo
{
    public int Id { get; init; }
    public string Name { get; init; } = "No disponible";
    public long WorkingSetBytes { get; init; }
    public double? CpuUsagePercent { get; init; }
    public string CpuStatus { get; init; } = "Calculando...";
    public string Status { get; init; } = "Ejecutándose";
    public ConsumptionLevel Consumption { get; set; } = ConsumptionLevel.Low;
}

public enum ConsumptionLevel
{
    Low,
    Normal,
    Moderate,
    High
}
