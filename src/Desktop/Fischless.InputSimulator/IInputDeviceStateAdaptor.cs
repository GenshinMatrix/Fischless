using Vanara.PInvoke;

namespace Fischless.InputSimulator;

public interface IInputDeviceStateAdaptor
{
    public bool IsKeyDown(User32.VK keyCode);
    public bool IsKeyUp(User32.VK keyCode);
    public bool IsHardwareKeyDown(User32.VK keyCode);
    public bool IsHardwareKeyUp(User32.VK keyCode);
    public bool IsTogglingKeyInEffect(User32.VK keyCode);
}
