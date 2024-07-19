using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Fischless.SpaceX.Core;

internal class Keyboard
{
    private const int WH_KEYBOARD_LL = 13;

    private delegate int HookHandle(int nCode, int wParam, IntPtr lParam);

    public delegate void ProcessKeyHandle(HookStruct param, out bool handle);

    private static int _hHookValue = 0;

    private HookHandle? _KeyBoardHookProcedure;

    [StructLayout(LayoutKind.Sequential)]
    public class HookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    [DllImport("user32.dll")]
    private static extern int SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern bool UnhookWindowsHookEx(int idHook);

    [DllImport("user32.dll")]
    private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern int GetCurrentThreadId();

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string name);

    [DllImport("user32.dll")]
    internal static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public uint type;
        public InputUnion inputUnion;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mouseInput;

        [FieldOffset(0)]
        public KEYBDINPUT keyboardInput;

        [FieldOffset(0)]
        public HARDWAREINPUT hardwareInput;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    public const int INPUT_MOUSE = 0;
    public const int INPUT_KEYBOARD = 1;
    public const int INPUT_HARDWARE = 2;
    public const uint KEYEVENTF_KEYDOWN = 0x0000;
    public const uint KEYEVENTF_KEYUP = 0x0002;
    public const ushort VK_CONTROL = 0x11;
    public const ushort VK_C = 0x43;

    private nint _hookWindowPtr = IntPtr.Zero;

    public Keyboard()
    {
    }

    private static ProcessKeyHandle? _clientMethod;

    public void InstallHook(ProcessKeyHandle clientMethod)
    {
        _clientMethod = clientMethod;

        if (_hHookValue == 0)
        {
            _KeyBoardHookProcedure = new HookHandle(OnHookProc);

            _hookWindowPtr = GetModuleHandle(Process.GetCurrentProcess()?.MainModule?.ModuleName ?? string.Empty);

            _hHookValue = SetWindowsHookEx(WH_KEYBOARD_LL, _KeyBoardHookProcedure, _hookWindowPtr, 0);

            if (_hHookValue == 0)
            {
                UninstallHook();
            }
        }
    }

    public static void UninstallHook()
    {
        if (_hHookValue != 0)
        {
            bool ret = UnhookWindowsHookEx(_hHookValue);
            if (ret)
            {
                _hHookValue = 0;
            }
        }
    }

    private static int OnHookProc(int nCode, int wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            HookStruct? hookStruct = (HookStruct?)Marshal.PtrToStructure(lParam, typeof(HookStruct));

            if (_clientMethod != null && hookStruct != null)
            {
                _clientMethod(hookStruct, out bool handle);
                if (handle)
                {
                    return 1;
                }
            }
        }
        return CallNextHookEx(_hHookValue, nCode, wParam, lParam);
    }
}
