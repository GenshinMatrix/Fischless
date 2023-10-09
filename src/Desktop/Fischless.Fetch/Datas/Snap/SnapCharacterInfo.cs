using Fischless.Fetch.Datas.Core;
using System.Text.Json.Serialization;

namespace Fischless.Fetch.Datas.Snap;

public partial class SnapCharacterInfo
{
    [JsonPropertyName("_id")]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public int Rarity { get; set; }

    public int Gender { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ElementType Element { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WeaponType WeaponType { get; set; }

    public string Card { get; set; }

    public string FaceIcon { get; set; }

    public string SideIcon { get; set; }

    public string GachaCard { get; set; }

    public string GachaSplash { get; set; }

    public int SortId { get; set; }

    public DateTime BeginTime { get; set; }
    
    public List<SnapCharacterOutfit>? Outfits { get; set; }
}
