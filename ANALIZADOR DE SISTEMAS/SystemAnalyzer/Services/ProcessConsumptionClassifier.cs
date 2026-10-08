using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public static class ProcessConsumptionClassifier
{
    public const long NormalMemoryBytes = 150L * 1024 * 1024;
    public const long ModerateMemoryBytes = 500L * 1024 * 1024;
    public const long HighMemoryBytes = 1024L * 1024 * 1024;
    public const double ModerateRamSharePercent = 5;
    public const double HighRamSharePercent = 10;

    public static ConsumptionLevel Classify(long workingSetBytes, ulong totalMemoryBytes)
    {
        var share = totalMemoryBytes == 0 ? 0 : workingSetBytes * 100d / totalMemoryBytes;
        if (workingSetBytes >= HighMemoryBytes || share >= HighRamSharePercent) return ConsumptionLevel.High;
        if (workingSetBytes >= ModerateMemoryBytes || share >= ModerateRamSharePercent) return ConsumptionLevel.Moderate;
        if (workingSetBytes >= NormalMemoryBytes) return ConsumptionLevel.Normal;
        return ConsumptionLevel.Low;
    }
}
