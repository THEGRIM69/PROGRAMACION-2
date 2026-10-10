namespace SystemAnalyzer.Models;

public enum DiagnosticStatus
{
    Correct,
    Attention,
    Critical,
    NoData
}

public sealed class DiagnosticResult
{
    public string Indicator { get; init; } = string.Empty;
    public DiagnosticStatus Status { get; init; }
    public string ObservedValue { get; init; } = "No disponible";
    public string Explanation { get; init; } = string.Empty;
    public string Recommendation { get; init; } = string.Empty;
    public string Source { get; init; } = string.Empty;
    public bool IsPartial { get; init; }
}

public sealed class DiagnosticReport
{
    public DiagnosticStatus OverallStatus { get; init; }
    public bool IsPartial { get; init; }
    public string Summary { get; init; } = string.Empty;
    public IReadOnlyList<DiagnosticResult> Results { get; init; } = Array.Empty<DiagnosticResult>();
}
