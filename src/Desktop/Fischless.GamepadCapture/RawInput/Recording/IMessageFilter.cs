namespace Fischless.GamepadCapture.RawInput;

public interface IMessageFilter
{
    public void PreFilterMessage(nint hWnd, int msg, nint wParam, nint lParam, ref bool handled);
}
