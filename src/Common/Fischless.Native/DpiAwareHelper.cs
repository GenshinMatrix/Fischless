using Vanara.PInvoke;

namespace Fischless.Native;

public static class DpiAwareHelper
{
    public static bool SetProcessDpiAwareness()
    {
        if (OsVersionHelper.IsWindows81_OrGreater)
        {
            if (SHCore.SetProcessDpiAwareness(SHCore.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE) == 0)
            {
                return true;
            }
        }
        return false;
    }
}
