using System.IO;
using System.Text;

namespace Fischless.GamepadCapture.RawInput;

public class Source : IBinary
{
    public int Count { get; private set; }
    public string Name { get; private set; }
    public string DeviceInterface { get; private set; }

    public Source(int count, string name, string device_interface = "")
    {
        Count = count;
        Name = name?.Trim()!;
        DeviceInterface = device_interface?.Trim()!;
    }

    static bool CfgMgrName(string interface_name, out string name, out string instance_name)
    {
        name = null!;
        instance_name = null!;

        uint type;
        int size = 0;

        int ret = CfgMgr32.CM_Get_Device_Interface_Property(
            interface_name,
            ref CfgMgr32.DEVPKEY_Device_InstanceId,
            out type,
            null!, ref size, 0
        );
        if (ret != CfgMgr32.CR_BUFFER_SMALL || type != CfgMgr32.DEVPROP_TYPE_STRING) return false;

        StringBuilder buffer = new StringBuilder(size);

        ret = CfgMgr32.CM_Get_Device_Interface_Property(
            interface_name,
            ref CfgMgr32.DEVPKEY_Device_InstanceId,
            out type,
            buffer, ref size, 0
        );
        if (ret != CfgMgr32.CR_SUCCESS || type != CfgMgr32.DEVPROP_TYPE_STRING) return false;

        instance_name = buffer.ToString();

        ret = CfgMgr32.CM_Locate_DevNode(out int devInst, instance_name, Locate_DevNode_Flags.CM_LOCATE_DEVNODE_PHANTOM);
        if (ret != 0) return false;

        size = 0;

        ret = CfgMgr32.CM_Get_DevNode_Property(
            devInst,
            ref CfgMgr32.DEVPKEY_Device_BusTypeGuid,
            out type,
            null, ref size, 0
        );
        if (ret != CfgMgr32.CR_BUFFER_SMALL || type != CfgMgr32.DEVPROP_TYPE_GUID) return false;

        Guid guid = new Guid();

        ret = CfgMgr32.CM_Get_DevNode_Property(
            devInst,
            ref CfgMgr32.DEVPKEY_Device_BusTypeGuid,
            out type,
            ref guid, ref size, 0
        );
        if (ret != CfgMgr32.CR_SUCCESS || type != CfgMgr32.DEVPROP_TYPE_GUID) return false;

        if (guid == CfgMgr32.GUID_BUS_TYPE_HID) return false;

        size = 0;

        ret = CfgMgr32.CM_Get_DevNode_Property(
            devInst,
            ref CfgMgr32.DEVPKEY_NAME,
            out type,
            null!, ref size, 0
        );
        if (ret != CfgMgr32.CR_BUFFER_SMALL || type != CfgMgr32.DEVPROP_TYPE_STRING) return false;

        buffer = new StringBuilder(size);

        ret = CfgMgr32.CM_Get_DevNode_Property(
            devInst,
            ref CfgMgr32.DEVPKEY_NAME,
            out type,
            buffer, ref size, 0
        );
        if (ret != CfgMgr32.CR_SUCCESS || type != CfgMgr32.DEVPROP_TYPE_STRING) return false;

        name = buffer.ToString();
        return true;
    }

    static bool HIDProductName(string interface_name, out string name)
    {
        var productString = new StringBuilder(2000);
        name = null!;

        var hFile = Kernel32.CreateFile(interface_name, 0, 3, IntPtr.Zero, 3, 0, IntPtr.Zero);

        if (hFile == IntPtr.Zero)
            return false;

        try
        {
            if (!Hid.HidD_GetProductString(hFile, productString, productString.Capacity))
                return false;
        }
        finally
        {
            Kernel32.CloseHandle(hFile);
        }

        name = productString.ToString();
        return true;
    }

    public static Source FromHandle(long handle, int count)
    {
        string name = "Unknown device";
        string interface_name = $"hDevice=0x{handle:X08}";

        if (User32.GetRawInputDeviceInfo(new nint(handle), RawInputCommand.DeviceName, out byte[] interface_bytes))
        {
            interface_name = Encoding.UTF8.GetString(interface_bytes.TakeWhile(i => i != 0).ToArray());

            if (!CfgMgrName(interface_name, out name, out string instance_name) &&
                !HIDProductName(interface_name, out name)
            )
            {
                name = instance_name ?? interface_name;
            }
        }

        return new Source(count, name, interface_name);
    }

    public void AppendIndex(int i)
    {
        Name += $" [{i + 1}]";
    }

    public override string ToString() => Name;

    public void ToBinary(BinaryWriter bw)
    {
        bw.Write(Count);
        bw.Write(Name);
        bw.Write(DeviceInterface);
    }

    public static Source FromBinary(BinaryReader br, uint fileVersion)
    {
        return new Source(
            br.ReadInt32(),
            br.ReadString(),
            br.ReadString()
        );
    }
}
