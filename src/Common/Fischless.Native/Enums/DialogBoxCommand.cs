using Vanara.PInvoke;

namespace Fischless.Native;

/// <summary>
/// Represents possible dialogbox command id values by the MB_GetString function.
/// </summary>
[PInvokeData("Winuser.h")]
[Flags]
public enum DialogBoxCommand : int
{
    IDOK = 0,
    IDCANCEL = 1,
    IDABORT = 2,
    IDRETRY = 3,
    IDIGNORE = 4,
    IDYES = 5,
    IDNO = 6,
    IDCLOSE = 7,
    IDHELP = 8,
    IDTRYAGAIN = 9,
    IDCONTINUE = 10,
}
