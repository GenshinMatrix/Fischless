using System.Security.Principal;

namespace Fischless.Fetch;

internal static class RuntimeHelper
{
    public static bool IsElevated { get; } = GetElevated();

    private static bool GetElevated()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}
