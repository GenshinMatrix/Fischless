namespace Fischless.Fetch.Unlocker;

public interface IGameFpsUnlocker
{
    public int TargetFps { get; set; }

    public Task UnlockAsync(UnlockTimingOptions options);
}
