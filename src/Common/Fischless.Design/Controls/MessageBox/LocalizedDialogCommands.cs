using Fischless.Native;
using System.Text.RegularExpressions;

namespace Fischless.Design.Controls;

internal static class LocalizedDialogCommands
{
    public static string GetString(DialogBoxCommand wBtn)
    {
        string src = NativeMethods.MB_GetString(wBtn)?.TrimStart('&')!;
        return Regex.Replace(src, @"\([^)]*\)", string.Empty).Replace("&", string.Empty);
    }
}
