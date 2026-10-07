using System.Diagnostics;
using System.ComponentModel;
using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public sealed class ProcessAnalyzerService
{
    public Task<IReadOnlyList<ProcessInfo>> GetProcessesAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run<IReadOnlyList<ProcessInfo>>(() =>
        {
            var results = new List<ProcessInfo>();
            foreach (var process in Process.GetProcesses())
            {
                using (process)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    try
                    {
                        results.Add(new ProcessInfo
                        {
                            Id = process.Id,
                            Name = string.IsNullOrWhiteSpace(process.ProcessName) ? "No disponible" : process.ProcessName,
                            WorkingSetBytes = Math.Max(0, process.WorkingSet64)
                        });
                    }
                    catch (InvalidOperationException) { }
                    catch (Win32Exception) { }
                    catch (NotSupportedException) { }
                }
            }
            return results;
        }, cancellationToken);
    }
}
