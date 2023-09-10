using Serilog;
using Windows.System;

namespace Fischless.Fetch.Launch;

public static class HyperionLauncher
{
    /// <summary>
    /// Windows Subsystem for Android™️
    /// Windows 11 Only Android Hoyolab Application
    /// </summary>
    public const string Android = "wsa://com.mihoyo.hyperion";

    /// <summary>
    /// Chinese Server Hoyolab named MiYouShe
    /// </summary>
    public const string Web = "https://www.miyoushe.com/ys/";

    public static async Task LaunchAsync(string type = null!)
    {
        try
        {
            await Launcher.LaunchUriAsync(new Uri(type ?? Android));
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
    }
}
