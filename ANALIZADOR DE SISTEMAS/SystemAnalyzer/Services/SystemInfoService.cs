using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;
using SystemAnalyzer.Models;

namespace SystemAnalyzer.Services;

public sealed class SystemInfoService
{
    private const string WindowsKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
    private const string ProcessorKey = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0";

    public async Task<SystemInfo> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var memory = ReadMemoryStatus();
        var windows = ReadWindowsRegistry();
        var processor = ReadProcessorRegistry();
        var cpu = await ReadCpuUsageAsync(cancellationToken).ConfigureAwait(false);

        return new SystemInfo
        {
            ComputerName = SafeValue(Environment.MachineName),
            UserName = SafeValue(Environment.UserName),
            OperatingSystem = windows.ProductName != "No disponible" ? windows.ProductName : SafeValue(RuntimeInformation.OSDescription),
            OsProductName = windows.ProductName,
            OsDescription = SafeValue(RuntimeInformation.OSDescription),
            OsVersion = Environment.OSVersion.Version.ToString(),
            OsBuild = windows.Build,
            OsDisplayVersion = windows.DisplayVersion,
            Platform = SafeValue(Environment.OSVersion.Platform.ToString()),
            Architecture = Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits",
            ProcessorArchitecture = RuntimeInformation.OSArchitecture.ToString(),
            Processor = processor.Name,
            ProcessorManufacturer = processor.Manufacturer,
            ProcessorFrequency = processor.Frequency,
            LogicalProcessorCount = Environment.ProcessorCount,
            PhysicalCoreCount = ReadPhysicalCoreCount(),
            WindowsDirectory = SafeValue(Environment.GetFolderPath(Environment.SpecialFolder.Windows)),
            SystemDirectory = SafeValue(Environment.SystemDirectory),
            Uptime = TimeSpan.FromMilliseconds(Environment.TickCount64),
            TotalMemoryBytes = memory.Total,
            AvailableMemoryBytes = memory.Available,
            CpuUsagePercent = cpu
        };
    }

    private static (string ProductName, string DisplayVersion, string Build) ReadWindowsRegistry()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(WindowsKey);
            return (Value(key, "ProductName"), Value(key, "DisplayVersion"), Value(key, "CurrentBuildNumber"));
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException or IOException)
        {
            return ("No disponible", "No disponible", "No disponible");
        }
    }

    private static (string Name, string Manufacturer, string Frequency) ReadProcessorRegistry()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(ProcessorKey);
            var mhz = key?.GetValue("~MHz")?.ToString();
            return (
                Value(key, "ProcessorNameString").Trim(),
                Value(key, "VendorIdentifier"),
                int.TryParse(mhz, out var value) ? $"{value:N0} MHz" : "No disponible");
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException or IOException)
        {
            return ("No disponible", "No disponible", "No disponible");
        }
    }

    private static int? ReadPhysicalCoreCount()
    {
        try
        {
            uint length = 0;
            _ = GetLogicalProcessorInformationEx(0, IntPtr.Zero, ref length);
            if (length == 0 || Marshal.GetLastWin32Error() != 122) return null;
            var buffer = Marshal.AllocHGlobal((int)length);
            try
            {
                if (!GetLogicalProcessorInformationEx(0, buffer, ref length)) return null;
                var offset = 0;
                var cores = 0;
                while (offset < length)
                {
                    var entry = IntPtr.Add(buffer, offset);
                    var relationship = Marshal.ReadInt32(entry, 0);
                    var size = Marshal.ReadInt32(entry, 4);
                    if (size <= 0) return null;
                    if (relationship == 0) cores++;
                    offset += size;
                }
                return cores > 0 ? cores : null;
            }
            finally { Marshal.FreeHGlobal(buffer); }
        }
        catch (Exception ex) when (ex is Win32Exception or ExternalException or OutOfMemoryException)
        {
            return null;
        }
    }

    private static (ulong Total, ulong Available) ReadMemoryStatus()
    {
        var status = new MemoryStatusEx();
        return GlobalMemoryStatusEx(status) ? (status.TotalPhysical, status.AvailablePhysical) : (0, 0);
    }

    private static async Task<double?> ReadCpuUsageAsync(CancellationToken cancellationToken)
    {
        if (!GetSystemTimes(out var idleBefore, out var kernelBefore, out var userBefore)) return null;
        await Task.Delay(500, cancellationToken).ConfigureAwait(false);
        if (!GetSystemTimes(out var idleAfter, out var kernelAfter, out var userAfter)) return null;
        var idle = ToUInt64(idleAfter) - ToUInt64(idleBefore);
        var kernel = ToUInt64(kernelAfter) - ToUInt64(kernelBefore);
        var user = ToUInt64(userAfter) - ToUInt64(userBefore);
        var total = kernel + user;
        return total == 0 || idle > total ? null : Math.Clamp((total - idle) * 100d / total, 0, 100);
    }

    private static string Value(RegistryKey? key, string name) => SafeValue(key?.GetValue(name)?.ToString());
    private static string SafeValue(string? value) => string.IsNullOrWhiteSpace(value) ? "No disponible" : value;
    private static ulong ToUInt64(FileTime value) => ((ulong)value.HighDateTime << 32) | value.LowDateTime;

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatusEx buffer);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetSystemTimes(out FileTime idleTime, out FileTime kernelTime, out FileTime userTime);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetLogicalProcessorInformationEx(int relationshipType, IntPtr buffer, ref uint returnedLength);

    [StructLayout(LayoutKind.Sequential)]
    private struct FileTime { public uint LowDateTime; public uint HighDateTime; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private sealed class MemoryStatusEx
    {
        public uint Length = (uint)Marshal.SizeOf<MemoryStatusEx>();
        public uint MemoryLoad;
        public ulong TotalPhysical;
        public ulong AvailablePhysical;
        public ulong TotalPageFile;
        public ulong AvailablePageFile;
        public ulong TotalVirtual;
        public ulong AvailableVirtual;
        public ulong AvailableExtendedVirtual;
    }
}
