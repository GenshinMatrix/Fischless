using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.Datas.Material;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fischless.Fetch.Datas.Character;

public partial class CharacterInfo
{
    [JsonPropertyName("_id")]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Rarity { get; set; }

    public int Gender { get; set; }

    public string Affiliation { get; set; }

    public string ConstllationName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ElementType Element { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WeaponType WeaponType { get; set; }

    public string Birthday { get; set; }

    public string Card { get; set; }

    public string FaceIcon { get; set; }

    public string SideIcon { get; set; }

    public string GachaCard { get; set; }

    public string GachaSplash { get; set; }

    public string CvChinese { get; set; }

    public string CvJapanese { get; set; }

    public string CvEnglish { get; set; }

    public string CvKorean { get; set; }

    public double HpBase { get; set; }

    public double AttackBase { get; set; }

    public double DefenseBase { get; set; }

    public int SortId { get; set; }

    [JsonConverter(typeof(BeginTimeJsonConverter))]
    public DateTime BeginTime { get; set; }

    public List<CharacterTalent>? Talents { get; set; }

    public List<CharacterConstellation>? Constellations { get; set; }

    public List<CharacterPromotion>? Promotions { get; set; }

    public NameCard? NameCard { get; set; }

    public Food? Food { get; set; }

    public List<CharacterOutfit>? Outfits { get; set; }

    public List<CharacterStory>? Stories { get; set; }

    public List<CharacterVoice>? Voices { get; set; }
}

public class BeginTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;

            if (root.TryGetProperty("$date", out var dateProp) && dateProp.ValueKind == JsonValueKind.String)
            {
                var dateString = dateProp.GetString();
                if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ssZ",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal,
                    out DateTime dateTime))
                {
                    return dateTime;
                }
            }
        }

        throw new JsonException("Unable to parse BeginTime");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
