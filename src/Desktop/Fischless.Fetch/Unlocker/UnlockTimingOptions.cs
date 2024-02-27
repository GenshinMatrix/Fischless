namespace Fischless.Fetch.Unlocker;

internal readonly struct UnlockTimingOptions(int findModuleDelayMilliseconds, int findModuleLimitMilliseconds, int adjustFpsDelayMilliseconds)
{
    public readonly TimeSpan FindModuleDelay = TimeSpan.FromMilliseconds(findModuleDelayMilliseconds);

    public readonly TimeSpan FindModuleLimit = TimeSpan.FromMilliseconds(findModuleLimitMilliseconds);

    public readonly TimeSpan AdjustFpsDelay = TimeSpan.FromMilliseconds(adjustFpsDelayMilliseconds);
}
