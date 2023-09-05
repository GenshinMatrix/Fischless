using System.Runtime.InteropServices;
using System.Security;

namespace Fischless.Native;

public static class NtDll
{
    [SecurityCritical]
    [DllImport(Lib.NTdll, SetLastError = true, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int RtlGetVersion(out OSVERSIONINFOEX versionInfo);
}
