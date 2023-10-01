namespace Fischless.Fetch.Lazy;

public sealed class LazyInput
{
    public string Uid { get; set; } = null!;
    public string DateTime { get; set; } = null!;
    public bool Today { get; set; } = false;
}
