using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Fischless.Fetch.Unlocker;

internal class GameFpsUnlockerImpl
{
    private static class Interop
    {
        [DllImport("Fischless.UnityPatch.dll", EntryPoint = "unlock")]
        public static extern int Unlock(int pid, int targetFPS);
    }

    public static unsafe void Start(GameFpsUnlockerOption option, string? gamePath = null, uint? pid = null, CancellationTokenSource? cts = null)
    {
        if (!option.TargetFps.HasValue)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(gamePath) && pid == null)
        {
            return;
        }

        int targetPid = (int)pid!.Value;
        int targetFps = option.TargetFps.Value;
        int ret = Interop.Unlock(targetPid, targetFps);

        Debug.WriteLine("[Unlocker] Unlock ret is " + ret);
    }
}
