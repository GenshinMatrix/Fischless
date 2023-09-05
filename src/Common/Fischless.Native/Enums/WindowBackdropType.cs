namespace Fischless.Native;

public enum WindowBackdropType
{
    /// <summary>
    /// No backdrop effect.
    /// </summary>
    None,

    /// <summary>
    /// Sets <c>DWMWA_SYSTEMBACKDROP_TYPE</c> to <see langword="0"></see>.
    /// </summary>
    Auto,

    /// <summary>
    /// Windows 11 Mica effect.
    /// </summary>
    Mica,

    /// <summary>
    /// Windows Acrylic effect.
    /// </summary>
    Acrylic,

    /// <summary>
    /// Windows 11 wallpaper blur effect.
    /// </summary>
    Tabbed,
}
