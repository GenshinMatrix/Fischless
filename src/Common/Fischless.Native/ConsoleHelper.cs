using Vanara.PInvoke;

namespace Fischless.Native;

public static class ConsoleHelper
{
    private static HWND hWnd;

    public static void Alloc()
    {
        Kernel32.AllocConsole();
        hWnd = Kernel32.GetConsoleWindow();
        Console.Title = "Fischless Console";
    }

    public static void Show()
    {
        if (hWnd != IntPtr.Zero)
        {
            User32.ShowWindow(hWnd, ShowWindowCommand.SW_SHOWNORMAL);
        }
    }

    public static void Hide()
    {
        if (hWnd != IntPtr.Zero)
        {
            User32.ShowWindow(hWnd, ShowWindowCommand.SW_HIDE);
        }
    }
}
