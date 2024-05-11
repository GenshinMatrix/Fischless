using Fischless.GamepadCapture.DirectInput;
using Fischless.GamepadCapture.RawInput;
using System.Windows;
using System.Windows.Interop;

namespace Fischless.GamepadCapture;

/// <summary>
/// https://hardwaretester.com/gamepad
/// </summary>
public partial class MainWindow : Window
{
    private DIInputDevice directInputDevice = new();
    private IMessageFilter messageFilter = new InputMessageFilter();

    public MainWindow()
    {
        InitializeComponent();

        Loaded += OnLoaded;
        Closed += OnClosed;

        Task.Run(() =>
        {
            directInputDevice.Initialize(IntPtr.Zero);

            while (true)
            {
                directInputDevice.GetKeyboardUpdates();
                directInputDevice.GetKJoystickUpdates();
            }
        });
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        nint hWnd = new WindowInteropHelper(this).Handle;
        if (HwndSource.FromHwnd(hWnd) is HwndSource hwndSource)
        {
            hwndSource.AddHook(WndProc);
            Listener.Start(hWnd);
        }
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        Listener.Stop();
    }

    private nint WndProc(nint hWnd, int msg, nint wParam, nint lParam, ref bool handled)
    {
        messageFilter.PreFilterMessage(hWnd, msg, wParam, lParam, ref handled);
        return IntPtr.Zero;
    }
}
