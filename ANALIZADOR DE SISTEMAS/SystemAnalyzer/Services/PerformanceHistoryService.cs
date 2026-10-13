using System.Security;
using System.Text.Json;
using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public sealed class PerformanceHistoryService
{
    public const int MaximumSamples = 1_000;
    public static readonly TimeSpan RetentionPeriod = TimeSpan.FromDays(30);
    public static readonly TimeSpan TemporaryFileMinimumAge = TimeSpan.FromHours(24);
    private static readonly TimeSpan LockTimeout = TimeSpan.FromSeconds(15);
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly string _directoryPath;
    private readonly string _filePath;
    private readonly string _lockFilePath;

    public PerformanceHistoryService(string? directoryPath = null)
    {
        _directoryPath = Path.GetFullPath(directoryPath ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SystemAnalyzer"));
        _filePath = Path.Combine(_directoryPath, "performance-history.json");
        _lockFilePath = Path.Combine(_directoryPath, "performance-history.lock");
    }

    public Task<HistoryLoadResult> LoadAsync(CancellationToken cancellationToken = default) =>
        RunLockedAsync(() =>
        {
            CleanupAbandonedTemporaryFiles();
            return LoadCore();
        }, cancellationToken);

    public Task<HistoryLoadResult> AppendAsync(
        PerformanceSample sample,
        CancellationToken cancellationToken = default)
    {
        if (!IsValid(sample))
            throw new ArgumentException("La muestra contiene valores no válidos.", nameof(sample));

        return RunLockedAsync(() =>
        {
            CleanupAbandonedTemporaryFiles();
            var loaded = LoadCore();
            if (loaded.Status is HistoryReadStatus.Corrupt or HistoryReadStatus.Inaccessible or HistoryReadStatus.ReadError)
                throw new IOException(loaded.Warning ?? "No fue posible conservar el historial existente.");

            var samples = loaded.Samples
                .Where(existing => existing.TimestampUtc != sample.TimestampUtc)
                .Append(sample)
                .OrderBy(existing => existing.TimestampUtc)
                .ToList();
            ApplyRetention(samples, DateTimeOffset.UtcNow);
            WriteCore(samples, cancellationToken);
            return new HistoryLoadResult(samples, loaded.Warning, HistoryReadStatus.Valid);
        }, cancellationToken);
    }

    public Task ClearAsync(CancellationToken cancellationToken = default) =>
        RunLockedAsync(() =>
        {
            CleanupAbandonedTemporaryFiles();
            if (File.Exists(_filePath)) File.Delete(_filePath);
            return true;
        }, cancellationToken);

    public static bool IsValid(PerformanceSample? sample)
    {
        if (sample is null || sample.TimestampUtc.Offset != TimeSpan.Zero ||
            sample.TimestampUtc < DateTimeOffset.UnixEpoch ||
            sample.TimestampUtc > DateTimeOffset.UtcNow.AddMinutes(5)) return false;
        return IsValidPercentage(sample.CpuPercent) && IsValidPercentage(sample.RamPercent) &&
               (sample.CpuPercent.HasValue || sample.RamPercent.HasValue);
    }

    public static bool IsValidPercentage(double? value) =>
        !value.HasValue || (double.IsFinite(value.Value) && value.Value is >= 0 and <= 100);

    private Task<T> RunLockedAsync<T>(Func<T> operation, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            Directory.CreateDirectory(_directoryPath);
            using var lockHandle = AcquireFileLock(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            return operation();
        }, cancellationToken);
    }

    private FileStream AcquireFileLock(CancellationToken cancellationToken)
    {
        var deadline = DateTime.UtcNow.Add(LockTimeout);
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var remaining = deadline - DateTime.UtcNow;
            if (remaining <= TimeSpan.Zero)
                throw new IOException("Se agotó el tiempo de espera para acceder al historial local.");
            try
            {
                return new FileStream(
                    _lockFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None,
                    bufferSize: 1, FileOptions.None);
            }
            catch (IOException)
            {
                var wait = remaining < TimeSpan.FromMilliseconds(100)
                    ? remaining
                    : TimeSpan.FromMilliseconds(100);
                if (cancellationToken.WaitHandle.WaitOne(wait))
                    cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }

    private HistoryLoadResult LoadCore()
    {
        if (!File.Exists(_filePath))
            return new HistoryLoadResult(Array.Empty<PerformanceSample>(), null, HistoryReadStatus.Missing);

        try
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (stream.Length == 0)
                return new HistoryLoadResult(Array.Empty<PerformanceSample>(),
                    "El archivo de historial está vacío y se preservó sin cambios.", HistoryReadStatus.Corrupt);

            var deserialized = JsonSerializer.Deserialize<List<PerformanceSample>>(stream, JsonOptions);
            if (deserialized is null)
                return new HistoryLoadResult(Array.Empty<PerformanceSample>(),
                    "El historial no contiene una lista válida y se preservó sin cambios.", HistoryReadStatus.Corrupt);

            var valid = deserialized.Where(IsValid)
                .GroupBy(sample => sample.TimestampUtc)
                .Select(group => group.Last())
                .OrderBy(sample => sample.TimestampUtc)
                .ToList();
            ApplyRetention(valid, DateTimeOffset.UtcNow);
            var rejected = deserialized.Count - valid.Count;
            return new HistoryLoadResult(valid, rejected > 0
                ? $"Se omitieron {rejected:N0} muestras inválidas o duplicadas."
                : null, HistoryReadStatus.Valid);
        }
        catch (JsonException)
        {
            return new HistoryLoadResult(Array.Empty<PerformanceSample>(),
                "El archivo de historial está dañado y se preservó sin cambios.", HistoryReadStatus.Corrupt);
        }
        catch (UnauthorizedAccessException)
        {
            return new HistoryLoadResult(Array.Empty<PerformanceSample>(),
                "El historial existe, pero no está accesible.", HistoryReadStatus.Inaccessible);
        }
        catch (SecurityException)
        {
            return new HistoryLoadResult(Array.Empty<PerformanceSample>(),
                "El historial existe, pero no está accesible.", HistoryReadStatus.Inaccessible);
        }
        catch (IOException)
        {
            return new HistoryLoadResult(Array.Empty<PerformanceSample>(),
                "Ocurrió un error transitorio al leer el historial.", HistoryReadStatus.ReadError);
        }
    }

    private void WriteCore(IReadOnlyList<PerformanceSample> samples, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_directoryPath);
        var temporaryPath = Path.Combine(_directoryPath, $"performance-history.{Guid.NewGuid():N}.tmp");
        try
        {
            using (var stream = new FileStream(
                temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None,
                bufferSize: 4096, FileOptions.WriteThrough))
            {
                JsonSerializer.Serialize(stream, samples, JsonOptions);
                stream.Flush(true);
            }

            cancellationToken.ThrowIfCancellationRequested();
            File.Move(temporaryPath, _filePath, true);
        }
        finally
        {
            TryDeleteTemporaryFile(temporaryPath);
        }
    }

    private void CleanupAbandonedTemporaryFiles()
    {
        if (!Directory.Exists(_directoryPath)) return;
        var oldestAllowed = DateTime.UtcNow.Subtract(TemporaryFileMinimumAge);
        try
        {
            foreach (var path in Directory.EnumerateFiles(
                         _directoryPath, "performance-history.*.tmp", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    if (IsOwnedTemporaryFile(path) && File.GetLastWriteTimeUtc(path) < oldestAllowed)
                        File.Delete(path);
                }
                catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or SecurityException)
                {
                    // La limpieza es auxiliar: nunca debe impedir leer un historial válido.
                }
            }
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or SecurityException)
        {
            // La carpeta puede perder acceso temporalmente; la operación principal decidirá su resultado.
        }
    }

    private static bool IsOwnedTemporaryFile(string path)
    {
        const string prefix = "performance-history.";
        const string suffix = ".tmp";
        var name = Path.GetFileName(path);
        if (!name.StartsWith(prefix, StringComparison.Ordinal) ||
            !name.EndsWith(suffix, StringComparison.Ordinal) ||
            name.Length != prefix.Length + 32 + suffix.Length) return false;
        return name.AsSpan(prefix.Length, 32).ToString().All(Uri.IsHexDigit);
    }

    private static void TryDeleteTemporaryFile(string path)
    {
        try
        {
            if (IsOwnedTemporaryFile(path) && File.Exists(path)) File.Delete(path);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or SecurityException)
        {
            // Una ejecución posterior limpiará el temporal cuando tenga al menos 24 horas.
        }
    }

    private static void ApplyRetention(List<PerformanceSample> samples, DateTimeOffset nowUtc)
    {
        var oldestAllowed = nowUtc.Subtract(RetentionPeriod);
        samples.RemoveAll(sample => sample.TimestampUtc < oldestAllowed);
        if (samples.Count > MaximumSamples)
            samples.RemoveRange(0, samples.Count - MaximumSamples);
    }
}

public enum HistoryReadStatus { Missing, Valid, Corrupt, Inaccessible, ReadError }

public sealed record HistoryLoadResult(
    IReadOnlyList<PerformanceSample> Samples,
    string? Warning,
    HistoryReadStatus Status);
