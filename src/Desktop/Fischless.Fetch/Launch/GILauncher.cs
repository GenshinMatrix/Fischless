using Fischless.Fetch.Regedit;
using Fischless.Fetch.Unlocker;
using System.Diagnostics;

namespace Fischless.Fetch.Launch;

public class GILauncher
{
    public const string RegionCN = "CN";
    public const string RegionOVERSEA = "OVERSEA";

    public const string ProcessNameCN = "YuanShen";
    public const string ProcessNameOVERSEA = "GenshinImpact";

    public const string FileNameCN = "YuanShen.exe";
    public const string FileNameOVERSEA = "GenshinImpact.exe";

    public const string FolderName = "Genshin Impact Game";

    public static bool TryGetProcess(out Process? process)
    {
        try
        {
            Process[] ps = Process.GetProcessesByName(ProcessNameCN);

            if (ps.Length <= 0)
            {
                ps = Process.GetProcessesByName(ProcessNameOVERSEA);
            }
            if (ps.Length > 0)
            {
                foreach (Process? p in ps)
                {
                    process = p;
                    return true;
                }
            }
        }
        catch
        {
        }
        process = null!;
        return false;
    }

    public static bool TryGetProcessRegion(out string region)
    {
        region = null!;
        try
        {
            Process[] ps = Process.GetProcessesByName(ProcessNameCN);

            if (ps.Length <= 0)
            {
                ps = Process.GetProcessesByName(ProcessNameOVERSEA);
            }
            else
            {
                region = RegionCN;
            }
            if (ps.Length > 0)
            {
                region ??= RegionOVERSEA;
                foreach (Process? p in ps)
                {
                    return true;
                }
            }
        }
        catch
        {
        }
        return false;
    }

    public static bool TryClose()
    {
        return TryGetProcess(out Process? p) && (p?.CloseMainWindow() ?? false);
    }

    public static bool TryKill()
    {
        bool got = TryGetProcess(out Process? p);
        p?.Kill();
        return got && p != null;
    }

    public static bool TryGetGamePath(out string gamePath)
    {
        string fileName = Path.Combine(GIRegedit.InstallPathCN ?? string.Empty, FolderName, FileNameCN);

        if (!File.Exists(fileName))
        {
            fileName = Path.Combine(GIRegedit.InstallPathOVERSEA ?? string.Empty, FolderName, FileNameOVERSEA);
        }
        return string.IsNullOrEmpty(gamePath = fileName);
    }

    public static async Task KillAsync(GIRelaunchMethod relaunchMethod = GIRelaunchMethod.None)
    {
        try
        {
            if (relaunchMethod switch
            {
                GIRelaunchMethod.Kill => await TryKillAsync(),
                GIRelaunchMethod.Close => await TryCloseAsync(),
                _ => false,
            })
            {
                if (!SpinWait.SpinUntil(() => TryGetProcess(out _), 15000))
                {
                    throw new Exception("Failed to kill Genshin Impact.");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public static async Task LaunchAsync(int? delayMs = null, GIRelaunchMethod relaunchMethod = GIRelaunchMethod.None, GILaunchParameter launchParameter = null!)
    {
        await KillAsync(relaunchMethod);

        if (delayMs != null)
        {
            await Task.Delay((int)delayMs);
        }

        launchParameter ??= new();

        string fileName = null!;

        if (string.IsNullOrWhiteSpace(launchParameter.GamePath))
        {
            _ = TryGetGamePath(out fileName);
        }
        else
        {
            FileInfo fileInfo = new(launchParameter.GamePath);

            if (fileInfo.Name.Equals(FileNameCN, StringComparison.OrdinalIgnoreCase)
             || fileInfo.Name.Equals(FileNameOVERSEA, StringComparison.OrdinalIgnoreCase))
            {
                fileName = launchParameter.GamePath;
            }

            if (!File.Exists(fileName))
            {
                if (string.IsNullOrEmpty(GIRegedit.InstallPathCN) && string.IsNullOrEmpty(GIRegedit.InstallPathOVERSEA))
                {
                    throw new Exception("Genshin Impact not installed.");
                }
                _ = TryGetGamePath(out fileName);
            }
        }

        if (string.IsNullOrEmpty(launchParameter.Server) || launchParameter.Server == RegionCN)
        {
            if (!string.IsNullOrEmpty(launchParameter.Prod))
            {
                GIRegedit.ProdCN = launchParameter.Prod;
            }
        }
        else if (launchParameter.Server == RegionOVERSEA)
        {
            if (!string.IsNullOrEmpty(launchParameter.Prod))
            {
                GIRegedit.ProdOVERSEA = launchParameter.Prod;
            }
        }

        Process gameProcess = Process.Start(new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = fileName,
            Arguments = launchParameter.ToString(),
            WorkingDirectory = launchParameter.WorkingDirectory ?? new FileInfo(fileName).DirectoryName,
            Verb = "runas",
        });

        if (launchParameter.Fps != null && launchParameter.Fps > 60)
        {
            try
            {
                await new GameFpsUnlocker(gameProcess).UnlockAsync(new UnlockTimingOptions(100, 20000, 3000));
            }
            catch
            {
            }
        }
    }

    public static async Task<bool> TryGetProcessAsync(Func<Process?, Task> callback = null!)
    {
        return await Task.Run(async () =>
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ProcessNameCN);

                if (processes.Length <= 0)
                {
                    processes = Process.GetProcessesByName(ProcessNameOVERSEA);
                }
                if (processes.Length > 0)
                {
                    foreach (Process? process in processes)
                    {
                        await callback?.Invoke(process)!;
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        });
    }

    public static async Task<bool> TryCloseAsync()
    {
        return await TryGetProcessAsync(p =>
        {
            p?.CloseMainWindow();
            return Task.CompletedTask;
        });
    }

    public static async Task<bool> TryKillAsync()
    {
        return await TryGetProcessAsync(p =>
        {
            p?.Kill();
            return Task.CompletedTask;
        });
    }
}
