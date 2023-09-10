using System.Text.Json.Serialization;

namespace Fischless.Hoyolab.Account;

internal class GenshinRoleInfoWrapper
{
    [JsonPropertyName("list")]
    public List<GenshinRoleInfo>? List { get; set; }
}
