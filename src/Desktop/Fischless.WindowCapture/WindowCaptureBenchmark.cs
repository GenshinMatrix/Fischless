using System.Diagnostics;
using Vanara.PInvoke;

namespace Fischless.WindowCapture;

public sealed class WindowCaptureBenchmark
{
    public static void Action()
    {
        foreach (CaptureMode mode in Enum.GetValues(typeof(CaptureMode)))
        {
            _ = Task.Run(async () =>
            {
                IWindowCapture capture = WindowCaptureFactory.Create(mode);

                _ = User32.ShowWindow(GetHwnd(), ShowWindowCommand.SW_RESTORE);
                capture.Start(GetHwnd());
                await Task.Delay(1234);
                using Bitmap frameFull = capture.Capture();

                if (frameFull != null)
                {
                    frameFull.Save($"Benchmark_{mode}_{frameFull.Width}x{frameFull.Height}.jpg");

                    using Bitmap frameCrop = capture.Capture(10, 10, 200, 400);
                    frameCrop?.Save($"Benchmark_{mode}_{frameFull.Width}x{frameFull.Height}_crop200x400.jpg");
                }
                else
                {
                    Debugger.Break();
                }
            });
        }
    }

    private static nint GetHwnd()
    {
        Process[] processes = Process.GetProcessesByName("YuanShen");

        if (processes.Length <= 0)
        {
            processes = Process.GetProcessesByName("Genshin Impact");
        }
        if (processes.Length <= 0)
        {
            processes = Process.GetProcessesByName("GenshinImpact");
        }
        if (processes.Length > 0)
        {
            foreach (Process? process in processes)
            {
                return process.MainWindowHandle;
            }
        }
        return IntPtr.Zero;
    }
}
