namespace SystemAnalyzer.Models;

public sealed class DriveInfoModel
{
    public string Name { get; init; } = "No disponible";
    public long TotalBytes { get; init; }
    public long AvailableBytes { get; init; }
    public string DriveType { get; init; } = "No disponible";

    public long UsedBytes => Math.Max(0, TotalBytes - AvailableBytes);
    public double UsagePercent => TotalBytes <= 0 ? 0 : UsedBytes * 100d / TotalBytes;
}
