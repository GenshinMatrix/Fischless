using Microsoft.Win32;

namespace Fischless.Fetch.Settings;

public sealed class ResolutionSettings
{
    private string? height_name = null;
    private string? width_name = null;
    private string? fullscreen_name = null;

    public int Height { get; private set; }
    public int Width { get; private set; }
    public bool FullScreen { get; private set; }

    public ResolutionSettings()
    {
        using RegistryKey hk = GenshinRegistry.GetRegistryKey();
        string[] names = hk.GetValueNames();

        foreach (string name in names)
        {
            if (name.Contains("Width"))
            {
                width_name = name;
            }
            if (name.Contains("Height"))
            {
                height_name = name;
            }
            if (name.Contains("Fullscreen"))
            {
                fullscreen_name = name;
            }
        }
        Read();
    }

    private void Read()
    {
        using RegistryKey hk = GenshinRegistry.GetRegistryKey();

        Height = Convert.ToInt32(hk.GetValue(height_name));
        Width = Convert.ToInt32(hk.GetValue(width_name));
        FullScreen = Convert.ToInt32(hk.GetValue(fullscreen_name)) == 1;
    }
}
