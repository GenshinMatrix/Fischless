using Fischless.Fetch.Launch;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Vanara.PInvoke;

namespace Fischless.Fetch.QueryProcess;

public static class QueryProcessProvider
{
    public static string? QueryFullProcessImageName()
    {
        if (GILauncher.TryGetProcess(out Process? process))
        {
            uint tid = User32.GetWindowThreadProcessId(process.MainWindowHandle, out uint pid);

            if (tid != 0)
            {
                using Kernel32.SafeHPROCESS hProcess = Kernel32.OpenProcess(new ACCESS_MASK(Kernel32.ProcessAccess.PROCESS_QUERY_INFORMATION), false, pid);

                if (!hProcess.IsInvalid)
                {
                    StringBuilder devicePath = new(260);
                    uint size = (uint)devicePath.Capacity;

                    if (Kernel32.QueryFullProcessImageName(hProcess, 0, devicePath, ref size))
                    {
                        return devicePath.ToString();
                    }
                    else
                    {
                        Debug.WriteLine($"Error getting process image file name. Error code: {Marshal.GetLastWin32Error()}");
                    }
                }
            }
        }
        return null;
    }
}
