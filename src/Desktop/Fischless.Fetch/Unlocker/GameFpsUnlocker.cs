using System.Diagnostics;

namespace Fischless.Fetch.Unlocker;

internal sealed class GameFpsUnlocker(Process gameProcess)
{
    private readonly Process gameProcess = gameProcess;
    private int targetFps;

    public GameFpsUnlocker SetTargetFps(int targetFps)
    {
        this.targetFps = targetFps;
        return this;
    }

    public async Task UnlockAsync(GameFpsUnlockerOption options, CancellationTokenSource cts = null!)
    {
        options.TargetFps = targetFps;
        await Task.Run(() => GameFpsUnlockerImpl.Start(options, pid: (uint)gameProcess.Id, cts: cts));
    }
}
