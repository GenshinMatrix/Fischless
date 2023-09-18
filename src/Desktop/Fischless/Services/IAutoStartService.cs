namespace Fischless.Services;

public interface IAutoStartService
{
    public string? GetAppName();
    public string? GetLaunchCommand();
    public void Enable();
    public bool IsEnabled();
    public void Disable();
    public void SetEnabled(bool enable);
}
