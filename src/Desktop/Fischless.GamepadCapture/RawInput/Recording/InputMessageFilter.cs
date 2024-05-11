namespace Fischless.GamepadCapture.RawInput;

/// <summary>
/// WPF message filter.
/// </summary>
public class InputMessageFilter : IMessageFilter
{
    public void PreFilterMessage(nint hWnd, int msg, nint wParam, nint lParam, ref bool handled)
    {
        if (msg == User32.WM_INPUT)
        {
            Listener.Process(lParam);
            handled = true;
        }
    }
}
