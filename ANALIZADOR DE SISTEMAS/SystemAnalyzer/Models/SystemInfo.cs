namespace SystemAnalyzer.Models;

public sealed class SystemInfo
{
    public string ComputerName { get; init; } = "No disponible";
    public string OperatingSystem { get; init; } = "No disponible";
    public string Architecture { get; init; } = "No disponible";
    public string Processor { get; init; } = "No disponible";
    public string OsProductName { get; init; } = "No disponible";
    public string OsDescription { get; init; } = "No disponible";
    public string OsVersion { get; init; } = "No disponible";
    public string OsBuild { get; init; } = "No disponible";
    public string OsDisplayVersion { get; init; } = "No disponible";
    public string Platform { get; init; } = "No disponible";
    public string ProcessorArchitecture { get; init; } = "No disponible";
    public string ProcessorManufacturer { get; init; } = "No disponible";
    public string ProcessorFrequency { get; init; } = "No disponible";
    public int LogicalProcessorCount { get; init; }
    public int? PhysicalCoreCount { get; init; }
    public string UserName { get; init; } = "No disponible";
    public string WindowsDirectory { get; init; } = "No disponible";
    public string SystemDirectory { get; init; } = "No disponible";
    public TimeSpan Uptime { get; init; }
    public ulong TotalMemoryBytes { get; init; }
    public ulong AvailableMemoryBytes { get; init; }
    public double? CpuUsagePercent { get; init; }

    public double MemoryUsagePercent => TotalMemoryBytes == 0
        ? 0
        : (TotalMemoryBytes - AvailableMemoryBytes) * 100d / TotalMemoryBytes;
}
