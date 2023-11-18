using System.Text;

namespace Fischless.Fetch.Launch;

public class GILaunchParameter
{
    public string? GamePath { get; set; } = null;
    public string? WorkingDirectory { get; set; } = null;

    public string? Server { get; set; } = null;
    public string? Prod { get; set; } = null;

    public bool? IsFullScreen { get; set; } = null;
    public bool? IsBorderless { get; set; } = null;
    public uint? ScreenWidth { get; set; } = null;
    public uint? ScreenHeight { get; set; } = null;
    public uint? Fps { get; set; } = null;

    public override string ToString()
    {
        StringBuilder sb = new();

        if (IsFullScreen != null)
        {
            sb.Append("-screen-fullscreen").Append(' ').Append(IsFullScreen.Value ? 1 : 0).Append(' ');
        }
        if (IsBorderless == true)
        {
            sb.Append("-popupwindow").Append(' ');
        }
        if (ScreenWidth != null)
        {
            sb.Append("-screen-width").Append(' ').Append(ScreenWidth).Append(' ');
        }
        if (ScreenHeight != null)
        {
            sb.Append("-screen-height").Append(' ').Append(ScreenHeight).Append(' ');
        }
        return sb.ToString();
    }
}
