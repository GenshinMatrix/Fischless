using System.Text.Json.Serialization;

namespace Fischless.Fetch.Datas.Snap;

public partial class SnapCharacterOutfit
{
    [JsonPropertyName("_id")]
    public int Id { get; set; }

    public int CharacterId { get; set; }

    public string Name { get; set; }

    public bool IsDefault { get; set; }

    public string? Card { get; set; }

    public string? FaceIcon { get; set; }

    public string? SideIcon { get; set; }

    public string? GachaSplash { get; set; }

    public string? SkinHash { get; set; }
}
