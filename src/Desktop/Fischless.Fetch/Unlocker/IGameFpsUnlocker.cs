namespace Fischless.Fetch.Unlocker;

internal interface IGameFpsUnlocker
{
    ValueTask UnlockAsync(UnlockTimingOptions options, IProgress<UnlockerStatus> progress, CancellationToken token = default);
}
