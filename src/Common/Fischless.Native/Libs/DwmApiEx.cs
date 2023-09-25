using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace Fischless.Native;

public static class DwmApiEx
{
    public static HRESULT DwmSetWindowAttribute(HWND hWnd, DWMWINDOWATTRIBUTE dwAttribute, int pvAttribute, int cbAttribute)
    {
        nint pvAttributePtr = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(pvAttributePtr, pvAttribute);

        try
        {
            return DwmApi.DwmSetWindowAttribute(
                hWnd,
                (DwmApi.DWMWINDOWATTRIBUTE)dwAttribute,
                pvAttributePtr,
                Marshal.SizeOf(typeof(int))
            );
        }
        finally
        {
            Marshal.FreeHGlobal(pvAttributePtr);
        }
    }
}
