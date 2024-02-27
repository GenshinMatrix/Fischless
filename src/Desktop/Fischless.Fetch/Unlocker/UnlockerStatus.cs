namespace Fischless.Fetch.Unlocker;

internal sealed class UnlockerStatus
{
    public string Description { get; set; } = default!;

    public FindModuleResult FindModuleState { get; set; }

    public bool IsUnlockerValid { get; set; } = true;

    public nuint FpsAddress { get; set; }
}
