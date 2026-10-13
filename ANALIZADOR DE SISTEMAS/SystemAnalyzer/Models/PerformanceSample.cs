namespace SystemAnalyzer.Models;

public sealed class PerformanceSample
{
    public DateTimeOffset TimestampUtc { get; init; }
    public double? CpuPercent { get; init; }
    public double? RamPercent { get; init; }
}
