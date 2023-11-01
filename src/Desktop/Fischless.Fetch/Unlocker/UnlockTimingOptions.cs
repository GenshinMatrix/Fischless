namespace Fischless.Fetch.Unlocker;

public readonly struct UnlockTimingOptions
{
    public readonly TimeSpan FindModuleDelay;
    public readonly TimeSpan FindModuleLimit;
    public readonly TimeSpan AdjustFpsDelay;

    public UnlockTimingOptions(int findModuleDelayMilliseconds, int findModuleLimitMilliseconds, int adjustFpsDelayMilliseconds)
    {
        FindModuleDelay = TimeSpan.FromMilliseconds(findModuleDelayMilliseconds);
        FindModuleLimit = TimeSpan.FromMilliseconds(findModuleLimitMilliseconds);
        AdjustFpsDelay = TimeSpan.FromMilliseconds(adjustFpsDelayMilliseconds);
    }
}
