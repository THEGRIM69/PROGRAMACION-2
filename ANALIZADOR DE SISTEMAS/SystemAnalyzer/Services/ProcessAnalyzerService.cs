using System.ComponentModel;
using System.Diagnostics;
using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public sealed class ProcessAnalyzerService
{
    private readonly object _sampleLock = new();
    private Dictionary<int, CpuSample> _previousSamples = new();
    private long _previousTimestamp;

    public Task<IReadOnlyList<ProcessInfo>> GetProcessesAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run<IReadOnlyList<ProcessInfo>>(() => CaptureProcesses(cancellationToken), cancellationToken);
    }

    public Task<ProcessDetails?> GetDetailsAsync(int processId, double? lastCpuUsage, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                using var process = Process.GetProcessById(processId);
                var name = Read(() => process.ProcessName, "No disponible");
                var memory = Read(() => Math.Max(0, process.WorkingSet64), 0L);
                var threadCount = ReadNullable(() => process.Threads.Count);
                var startTime = ReadNullable(() => process.StartTime);
                var path = Read(() => process.MainModule?.FileName ?? "No disponible", "No disponible");
                return new ProcessDetails
                {
                    Id = processId,
                    Name = name,
                    WorkingSetBytes = memory,
                    CpuUsagePercent = lastCpuUsage,
                    ThreadCount = threadCount,
                    StartTime = startTime,
                    ExecutablePath = path
                };
            }
            catch (ArgumentException) { return null; }
            catch (InvalidOperationException) { return null; }
            catch (Win32Exception) { return null; }
        }, cancellationToken);
    }

    private IReadOnlyList<ProcessInfo> CaptureProcesses(CancellationToken cancellationToken)
    {
        Dictionary<int, CpuSample> previous;
        long previousTimestamp;
        lock (_sampleLock)
        {
            previous = _previousSamples;
            previousTimestamp = _previousTimestamp;
        }

        var now = Stopwatch.GetTimestamp();
        var elapsedMilliseconds = previousTimestamp == 0
            ? 0
            : (now - previousTimestamp) * 1000d / Stopwatch.Frequency;
        var currentSamples = new Dictionary<int, CpuSample>();
        var results = new List<ProcessInfo>();

        foreach (var process in Process.GetProcesses())
        {
            using (process)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    var name = process.ProcessName;
                    var memory = Math.Max(0, process.WorkingSet64);
                    var processorTime = ReadNullable(() => process.TotalProcessorTime);
                    var startTimeTicks = ReadNullable(() => process.StartTime.ToUniversalTime().Ticks);
                    double? cpu = null;

                    if (processorTime.HasValue)
                    {
                        var sample = new CpuSample(name, startTimeTicks, processorTime.Value);
                        currentSamples[process.Id] = sample;
                        if (elapsedMilliseconds >= 250 && previous.TryGetValue(process.Id, out var old) && SameProcess(old, sample))
                        {
                            var processorDelta = (processorTime.Value - old.TotalProcessorTime).TotalMilliseconds;
                            if (processorDelta >= 0)
                            {
                                var calculated = processorDelta / elapsedMilliseconds / Math.Max(1, Environment.ProcessorCount) * 100d;
                                if (double.IsFinite(calculated) && calculated >= 0)
                                    cpu = Math.Clamp(calculated, 0, 100);
                            }
                        }
                    }

                    results.Add(new ProcessInfo
                    {
                        Id = process.Id,
                        Name = string.IsNullOrWhiteSpace(name) ? "No disponible" : name,
                        WorkingSetBytes = memory,
                        CpuUsagePercent = cpu,
                        CpuStatus = processorTime.HasValue ? "Calculando..." : "No disponible"
                    });
                }
                catch (InvalidOperationException) { }
                catch (Win32Exception) { }
                catch (NotSupportedException) { }
            }
        }

        lock (_sampleLock)
        {
            _previousSamples = currentSamples;
            _previousTimestamp = now;
        }
        return results;
    }

    private static bool SameProcess(CpuSample old, CpuSample current)
    {
        if (!string.Equals(old.Name, current.Name, StringComparison.OrdinalIgnoreCase)) return false;
        return !old.StartTimeTicks.HasValue || !current.StartTimeTicks.HasValue || old.StartTimeTicks == current.StartTimeTicks;
    }

    private static T Read<T>(Func<T> reader, T fallback)
    {
        try { return reader(); }
        catch (InvalidOperationException) { return fallback; }
        catch (Win32Exception) { return fallback; }
        catch (NotSupportedException) { return fallback; }
    }

    private static T? ReadNullable<T>(Func<T> reader) where T : struct
    {
        try { return reader(); }
        catch (InvalidOperationException) { return null; }
        catch (Win32Exception) { return null; }
        catch (NotSupportedException) { return null; }
    }

    private sealed record CpuSample(string Name, long? StartTimeTicks, TimeSpan TotalProcessorTime);
}
