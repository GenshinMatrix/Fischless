using System.Text.Json.Serialization;

namespace Fischless.Fetch.Datas.Material;

public class MaterialItem
{
    [JsonPropertyName("_id")]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }

    public string ItemType { get; set; }

    public string? MaterialType { get; set; }

    public string? TypeDescription { get; set; }

    public int RankLevel { get; set; }

    public int StackLimit { get; set; }

    public int Rank { get; set; }
}
