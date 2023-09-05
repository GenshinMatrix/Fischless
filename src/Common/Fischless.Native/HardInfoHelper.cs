using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Fischless.Native;

public static class HardInfoHelper
{
    public static string ComputerIdentity => $"{CpuSerialNumber},{BaseBoardSerialNumber},{BiosSerialNumber}";
    public static readonly string ComputerIdentityMD5 = MD5CryptoHelper.ComputeHash(ComputerIdentity);
    public static string CpuSerialNumber => GetHardWareInfo("Win32_Processor", "SerialNumber");
    public static string BiosSerialNumber => GetHardWareInfo("Win32_BIOS", "SerialNumber");
    public static string BaseBoardSerialNumber => GetHardWareInfo("Win32_BaseBoard", "SerialNumber");

    public static string GetHardWareInfo(string path, string name)
    {
        try
        {
            using ManagementClass managementClass = new(path);
            using ManagementObjectCollection mn = managementClass.GetInstances();
            PropertyDataCollection properties = managementClass.Properties;

            foreach (PropertyData property in properties)
            {
                if (property.Name == name)
                {
                    foreach (ManagementObject m in mn.Cast<ManagementObject>())
                    {
                        return m.Properties[property.Name].Value.ToString();
                    }
                }
            }
        }
        catch
        {
        }
        return string.Empty;
    }
}

file static class MD5CryptoHelper
{
    public static string ComputeHash(string s, Encoding? encoding = null!)
    {
        return ComputeHash((encoding ?? Encoding.UTF8).GetBytes(s));
    }

    public static string ComputeHash(byte[] buffer)
    {
        byte[] output = MD5.HashData(buffer);
        return BitConverter.ToString(output).Replace("-", string.Empty);
    }
}
