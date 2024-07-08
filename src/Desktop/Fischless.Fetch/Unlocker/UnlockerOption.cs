namespace Fischless.Fetch.Unlocker;

public sealed class GameFpsUnlockerOption
{
    public static Lazy<GameFpsUnlockerOption> Default { get; } = new();

    public int? TargetFps { get; set; } = 120;

    public int FindModuleDelay { get; set; } = 100;

    public int FindModuleLimit { get; set; } = 2000;

    public int FpsDelay { get; set; } = 2000;
}
