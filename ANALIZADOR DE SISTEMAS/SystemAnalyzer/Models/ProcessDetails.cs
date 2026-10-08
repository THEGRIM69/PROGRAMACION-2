namespace SystemAnalyzer.Models;

public sealed class ProcessDetails
{
    public int Id { get; init; }
    public string Name { get; init; } = "No disponible";
    public long WorkingSetBytes { get; init; }
    public double? CpuUsagePercent { get; init; }
    public int? ThreadCount { get; init; }
    public DateTime? StartTime { get; init; }
    public string ExecutablePath { get; init; } = "No disponible";
}
