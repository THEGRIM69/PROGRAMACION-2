namespace SystemAnalyzer.Models;

public sealed class ProcessInfo
{
    public int Id { get; init; }
    public string Name { get; init; } = "No disponible";
    public long WorkingSetBytes { get; init; }
}
