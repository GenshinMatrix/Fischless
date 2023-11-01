namespace Fischless.Native;

public static class Macro
{
    public static int LOW_ORDER(int param) => param & 0xffff;

    public static int HIGH_ORDER(int param) => param >> 16;

    internal static ushort HIWORD(nint value) => (ushort)((((long)value) >> 0x10) & 0xFFFF);

    internal static ushort HIWORD(uint value) => (ushort)(value >> 0x10);

    public static ushort LOWORD(uint value) => (ushort)(value & 0xFFFF);

    public static ushort LOWORD(nint value) => (ushort)((long)value & 0xFFFF);

    public static byte LOWBYTE(ushort value) => (byte)(value & 0xFF);

    public static byte HIGHBYTE(ushort value) => (byte)(value >> 8);

    public static int GET_WHEEL_DELTA_WPARAM(nint wParam) => (short)HIWORD(wParam);

    public static int GET_WHEEL_DELTA_WPARAM(uint wParam) => (short)HIWORD(wParam);

    /// <summary>
    /// <remark>#define GET_X_LPARAM(lp) ((int)(short)LOWORD(lp))</remark>
    /// </summary>
    public static int GET_X_LPARAM(nint wParam) => LOWORD(wParam);

    public static int GET_X_LPARAM(int lParam) => lParam & 0xffff;

    /// <summary>
    /// <remark>#define GET_Y_LPARAM(lp) ((int)(short)HIWORD(lp))</remark>
    /// </summary>
    public static int GET_Y_LPARAM(IntPtr wParam) => HIWORD(wParam);

    public static int GET_Y_LPARAM(int lParam) => lParam >> 16;

    /// <summary>
    /// <remark>#define MAKELPARAM(l, h) ((LPARAM) MAKELONG(l, h))</remark>
    /// </summary>
    public static int MAKELPARAM(int l, int h) => MAKELONG(l, h);

    /// <summary>
    /// <remark>#define MAKELONG(a, b) ((LONG)(((WORD)(a)) | ((DWORD)((WORD)(b))) << 16))</remark>
    /// </summary>
    public static int MAKELONG(int a, int b) => a | (b << 16);
}
