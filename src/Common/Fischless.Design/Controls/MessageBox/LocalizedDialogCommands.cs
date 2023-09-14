using System.Text.RegularExpressions;
using Vanara.PInvoke;

namespace Fischless.Design.Controls;

internal static class LocalizedDialogCommands
{
    public static string GetString(User32.MB_RESULT command)
    {
        string src = User32.MB_GetString((uint)command)?.TrimStart('&')!;
        return Regex.Replace(src, @"\([^)]*\)", string.Empty).Replace("&", string.Empty);
    }
}
