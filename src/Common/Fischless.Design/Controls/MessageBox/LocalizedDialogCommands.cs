using Fischless.Native;
using System.Text.RegularExpressions;
using Vanara.InteropServices;
using Vanara.PInvoke;

namespace Fischless.Design.Controls;

internal static class LocalizedDialogCommands
{
    public static string GetString(DialogBoxCommand wBtn)
    {
        StrPtrUni strPtrUni = User32.MB_GetString((uint)wBtn);
        string src = strPtrUni.ToString()?.TrimStart('&')!;
        return Regex.Replace(src, @"\([^)]*\)", string.Empty).Replace("&", string.Empty);
    }
}
