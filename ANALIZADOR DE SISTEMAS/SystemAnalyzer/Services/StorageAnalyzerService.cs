using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public sealed class StorageAnalyzerService
{
    public Task<DriveInfoModel?> GetSystemDriveAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var root = Path.GetPathRoot(Environment.SystemDirectory);
                if (string.IsNullOrWhiteSpace(root)) return null;
                var drive = new DriveInfo(root);
                if (!drive.IsReady) return null;
                return new DriveInfoModel
                {
                    Name = drive.Name.TrimEnd('\\'),
                    TotalBytes = drive.TotalSize,
                    AvailableBytes = drive.AvailableFreeSpace
                };
            }
            catch (OperationCanceledException) { throw; }
            catch (IOException) { return null; }
            catch (UnauthorizedAccessException) { return null; }
            catch (ArgumentException) { return null; }
        }, cancellationToken);
    }

    public Task<IReadOnlyList<DriveInfoModel>> GetReadyDrivesAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run<IReadOnlyList<DriveInfoModel>>(() =>
        {
            var results = new List<DriveInfoModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    if (!drive.IsReady) continue;
                    results.Add(new DriveInfoModel
                    {
                        Name = drive.Name.TrimEnd('\\'),
                        DriveType = drive.DriveType.ToString(),
                        TotalBytes = drive.TotalSize,
                        AvailableBytes = drive.AvailableFreeSpace
                    });
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
            return results;
        }, cancellationToken);
    }
}
