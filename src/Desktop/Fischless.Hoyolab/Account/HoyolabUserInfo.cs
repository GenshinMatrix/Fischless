using System.Text.Json.Serialization;

namespace Fischless.Hoyolab.Account;

public class HoyolabUserInfo
{
    [JsonPropertyName("uid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
    public int Uid { get; set; }

    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }

    [JsonPropertyName("introduce")]
    public string? Introduce { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; }

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("pendant")]
    public string? Pendant { get; set; }

    [JsonIgnore]
    public string? Cookie { get; set; }
}
