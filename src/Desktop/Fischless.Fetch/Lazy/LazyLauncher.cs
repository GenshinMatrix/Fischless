using System.Diagnostics;

namespace Fischless.Fetch.Lazy;

public static class LazyLauncher
{
    public static async Task<int> LaunchAsync(string path, Func<string, Task> stdOutAsync = null!, Func<string, Task> stdErrAsync = null!)
    {
        return await Task.Run(() =>
        {
            try
            {
                Process p = Process.Start(new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    FileName = path,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                });

                if (p != null)
                {
                    if (stdOutAsync != null)
                    {
                        p.OutputDataReceived += (_, e) => stdOutAsync.Invoke(e.Data!);
                    }
                    if (stdErrAsync != null)
                    {
                        p.ErrorDataReceived += (_, e) => stdErrAsync.Invoke(e.Data!);
                    }
                }
                p!.WaitForExit();
                return p.ExitCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return int.MaxValue;
            }
        });
    }
}
