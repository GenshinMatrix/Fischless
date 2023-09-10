using System.Text;

namespace Fischless.Fetch.Launch;

public class GILaunchParameter
{
    public string? Server { get; set; } = null;
    public string? Prod { get; set; } = null;

    public bool? IsFullScreen { get; set; } = null;
    public int? ScreenWidth { get; set; } = null;
    public int? ScreenHeight { get; set; } = null;

    public override string ToString()
    {
        StringBuilder sb = new();

        if (IsFullScreen != null)
        {
            sb.Append("-screen-fullscreen").Append(' ').Append(IsFullScreen.Value ? 1 : 0);
        }
        if (ScreenWidth != null)
        {
            sb.Append("-screen-width").Append(' ').Append(ScreenWidth);
        }
        if (ScreenHeight != null)
        {
            sb.Append("-screen-height").Append(' ').Append(ScreenHeight);
        }
        return sb.ToString();
    }
}
