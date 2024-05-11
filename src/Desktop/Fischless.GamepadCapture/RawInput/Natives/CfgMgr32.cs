using System.Runtime.InteropServices;
using System.Text;

namespace Fischless.GamepadCapture.RawInput;

internal static class CfgMgr32
{
    public static readonly int CR_SUCCESS = 0x00;
    public static readonly int CR_BUFFER_SMALL = 0x1A;

    public static readonly uint DEVPROP_TYPE_STRING = 0x12;
    public static readonly uint DEVPROP_TYPE_GUID = 0x0D;

    public static Guid GUID_BUS_TYPE_HID = new(0xeeaf37d0, 0x1963, 0x47c4, 0xaa, 0x48, 0x72, 0x47, 0x6d, 0xb7, 0xcf, 0x49);

    public static DEVPROPKEY DEVPKEY_Device_InstanceId = new()
    {
        fmtid = new Guid(0x78c34fc8, 0x104a, 0x4aca, 0x9e, 0xa4, 0x52, 0x4d, 0x52, 0x99, 0x6e, 0x57),
        pid = 256
    };

    public static DEVPROPKEY DEVPKEY_Device_BusTypeGuid = new()
    {
        fmtid = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0),
        pid = 21
    };

    public static DEVPROPKEY DEVPKEY_NAME = new()
    {
        fmtid = new Guid(0xb725f130, 0x47ef, 0x101a, 0xa5, 0xf1, 0x02, 0x60, 0x8c, 0x9e, 0xeb, 0xac),
        pid = 10
    };

    [DllImport(Libs.CfgMgr32, CharSet = CharSet.Unicode)]
    public static extern int CM_Get_Device_Interface_Property(
        string pszDeviceInterface,
        ref DEVPROPKEY PropertyKey,
        out uint PropertyType,
        StringBuilder PropertyBuffer,
        ref int PropertyBufferSize,
        uint ulFlags
    );

    [DllImport(Libs.CfgMgr32, CharSet = CharSet.Unicode)]
    public static extern int CM_Locate_DevNode(
        out int pdnDevInst,
        string pDeviceID,
        Locate_DevNode_Flags ulFlags
    );

    [DllImport(Libs.CfgMgr32, CharSet = CharSet.Unicode)]
    public static extern int CM_Get_DevNode_Property(
        int dnDevInst,
        ref DEVPROPKEY PropertyKey,
        out uint PropertyType,
        StringBuilder PropertyBuffer,
        ref int PropertyBufferSize,
        uint ulFlags
    );

    [DllImport(Libs.CfgMgr32, CharSet = CharSet.Unicode)]
    public static extern int CM_Get_DevNode_Property(
        int dnDevInst,
        ref DEVPROPKEY PropertyKey,
        out uint PropertyType,
        ref Guid PropertyBuffer,
        ref int PropertyBufferSize,
        uint ulFlags
    );
}
