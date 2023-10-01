using Newtonsoft.Json;
using System.Diagnostics;

namespace Fischless.Fetch.Settings;

internal class InputDataSettings
{
    public InputDataConfig? data = null;
    public double MouseSensitivity => data!.MouseSensitivity;
    public MouseFocusSenseIndex MouseFocusSenseIndex => (MouseFocusSenseIndex)data!.MouseFocusSenseIndex;
    public MouseFocusSenseIndex MouseFocusSenseIndexY => (MouseFocusSenseIndex)data!.MouseFocusSenseIndexY;

    public InputDataSettings(MainJson data)
    {
        Load(data);
    }

    public void Load(MainJson data)
    {
        try
        {
            this.data = JsonConvert.DeserializeObject<InputDataConfig>(data.InputData);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
    }
}

internal class InputDataConfig
{
    [JsonProperty("mouseSensitivity")]
    public double MouseSensitivity { get; set; }

    [JsonProperty("mouseFocusSenseIndex")]
    public int MouseFocusSenseIndex { get; set; }

    [JsonProperty("mouseFocusSenseIndexY")]
    public int MouseFocusSenseIndexY { get; set; }
}

public enum MouseFocusSenseIndex
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
}
