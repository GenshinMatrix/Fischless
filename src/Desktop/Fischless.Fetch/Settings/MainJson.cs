using Newtonsoft.Json;

namespace Fischless.Fetch.Settings;

public class MainJson
{
    [JsonProperty("deviceLanguageType")]
    public int DeviceLanguageType { get; set; }

    [JsonProperty("deviceVoiceLanguageType")]
    public int DeviceVoiceLanguageType { get; set; }

    [JsonProperty("inputData")]
    public string? InputData { get; set; }
}
