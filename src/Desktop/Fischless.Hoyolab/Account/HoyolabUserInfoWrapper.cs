using System.Text.Json.Serialization;

namespace Fischless.Hoyolab.Account;

internal class HoyolabUserInfoWrapper
{
    [JsonPropertyName("user_info")]
    public HoyolabUserInfo? HoyolabUserInfo { get; set; }
}
