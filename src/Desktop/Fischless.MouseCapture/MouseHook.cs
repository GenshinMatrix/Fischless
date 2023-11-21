using System.Diagnostics;
using Vanara.Extensions;
using Vanara.PInvoke;

namespace Fischless.MouseCapture;

public class MouseHook : IDisposable
{
    public event EventHandler<MouseEventArgs> MouseMove = null!;

    public event EventHandler<MouseEventArgs> MouseDown = null!;

    public event EventHandler<MouseEventArgs> MouseUp = null!;

    private User32.SafeHHOOK hook = new(IntPtr.Zero);
    private User32.HookProc? hookProc;

    ~MouseHook()
    {
        Dispose();
    }

    public void Dispose()
    {
        User32.UnhookWindowsHookEx(hook);
    }

    public void Start()
    {
        hookProc = MouseHookCallback;
        hook = User32.SetWindowsHookEx(User32.HookType.WH_MOUSE_LL, hookProc, Kernel32.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

        User32.SetWindowsHookEx(User32.HookType.WH_MOUSE_LL, hookProc, IntPtr.Zero, (int)Kernel32.GetCurrentThreadId());

        if (hook.IsNull)
        {
            Stop();
            throw new SystemException("Failed to install mouse hook");
        }
    }

    public void Stop()
    {
        bool retKeyboard = true;

        if (!hook.IsNull)
        {
            retKeyboard = User32.UnhookWindowsHookEx(hook);
            hook = new(IntPtr.Zero);
        }

        if (!retKeyboard)
        {
            throw new SystemException("Failed to uninstall hook");
        }
    }

    private nint MouseHookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0)
        {
            User32.WindowMessage message = (User32.WindowMessage)wParam;
            User32.MSLLHOOKSTRUCT hookStruct = lParam.ToStructure<User32.MSLLHOOKSTRUCT>();

            switch (message)
            {
                case User32.WindowMessage.WM_MOUSEMOVE:
                    MouseMove?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, hookStruct.pt.x, hookStruct.pt.y, 0));
                    break;

                case User32.WindowMessage.WM_LBUTTONDOWN:
                case User32.WindowMessage.WM_RBUTTONDOWN:
                case User32.WindowMessage.WM_MBUTTONDOWN:
                    MouseDown?.Invoke(this, new MouseEventArgs(message.GetButton(), 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                    break;

                case User32.WindowMessage.WM_LBUTTONUP:
                case User32.WindowMessage.WM_RBUTTONUP:
                case User32.WindowMessage.WM_MBUTTONUP:
                    MouseUp?.Invoke(this, new MouseEventArgs(message.GetButton(), 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                    break;
            }
        }

        return User32.CallNextHookEx(hook, nCode, wParam, lParam);
    }
}

file static class MouseHookExtension
{
    public static MouseButtons GetButton(this User32.WindowMessage message)
    {
        return message switch
        {
            User32.WindowMessage.WM_LBUTTONDOWN or User32.WindowMessage.WM_LBUTTONUP => MouseButtons.Left,
            User32.WindowMessage.WM_RBUTTONDOWN or User32.WindowMessage.WM_RBUTTONUP => MouseButtons.Right,
            User32.WindowMessage.WM_MBUTTONDOWN or User32.WindowMessage.WM_MBUTTONUP => MouseButtons.Middle,
            _ => MouseButtons.None,
        };
    }
}
