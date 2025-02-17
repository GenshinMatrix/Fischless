using Newtonsoft.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

// https://github.com/dvaJi/genshin-data
string directoryPath = @"D:\GitHub\genshin-data\src\data\english\characters";
string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);
var jsons = jsonFiles.Select(jsonFile => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(jsonFile))).OrderBy(j => (int)j["release"]);

StringBuilder sb = new();

sb.AppendLine("using Fischless.Fetch.Datas.Core;");
sb.AppendLine("using Fischless.Fetch.ReShade;");
sb.AppendLine();
sb.AppendLine("namespace Fischless.Fetch.Datas.Snap;");
sb.AppendLine();
sb.AppendLine("public static class SnapCharacterProvider");
sb.AppendLine("{");
sb.AppendLine("    public static IEnumerable<SnapCharacterInfo> GetSnapCharacters()");
sb.AppendLine("    {");

foreach (dynamic json in jsons)
{
    if ((int)json["_id"] == 10000005)
    {
        continue;
    }

    sb.AppendLine("        yield return new SnapCharacterInfo()");
    sb.AppendLine("        {");
    sb.AppendLine($"            Id = {(int)json["_id"]},");
    sb.AppendLine($"            Name = \"{(string)json["name"]}\",");
    sb.AppendLine($"            Rarity = {(int)json["rarity"]},");
    sb.AppendLine($"            Gender = {((string)json["gender"]["id"] == "female" ? 1 : 0)},");
    sb.AppendLine($"            Element = ElementType.{((string)json["element"]["id"]).Capitalize()},");
    sb.AppendLine($"            WeaponType = WeaponType.{((string)json["weapon_type"]["id"]).Capitalize()},");
    sb.AppendLine($"            FaceIcon = \"UI_AvatarIcon_{ToFileName((string)json["id"])}.png\",");
    sb.AppendLine($"            SortId = {(int)json["release"]},");
    sb.AppendLine($"            TextureOverride = ReShadeIniMapper.Map({(int)json["_id"]}),");
    sb.AppendLine("        };");
    sb.AppendLine("");
}
sb.AppendLine("        ///");
sb.AppendLine("        yield return new SnapCharacterInfo()");
sb.AppendLine("        {");
sb.AppendLine("            Id = 10000005,");
sb.AppendLine("            Name = \"Aether\",");
sb.AppendLine("            Rarity = 5,");
sb.AppendLine("            Gender = 0,");
sb.AppendLine("            Element = ElementType.None,");
sb.AppendLine("            WeaponType = WeaponType.Sword,");
sb.AppendLine("            FaceIcon = \"UI_AvatarIcon_PlayerBoy.png\",");
sb.AppendLine("            SortId = 0,");
sb.AppendLine("            TextureOverride = ReShadeIniMapper.Map(10000005),");
sb.AppendLine("        };");
sb.AppendLine();
sb.AppendLine("        yield return new SnapCharacterInfo()");
sb.AppendLine("        {");
sb.AppendLine("            Id = 10000007,");
sb.AppendLine("            Name = \"Lumine\",");
sb.AppendLine("            Rarity = 5,");
sb.AppendLine("            Gender = 1,");
sb.AppendLine("            Element = ElementType.None,");
sb.AppendLine("            WeaponType = WeaponType.Sword,");
sb.AppendLine("            FaceIcon = \"UI_AvatarIcon_PlayerGirl.png\",");
sb.AppendLine("            SortId = 0,");
sb.AppendLine("            TextureOverride = ReShadeIniMapper.Map(10000007),");
sb.AppendLine("        };");
sb.AppendLine();
sb.AppendLine("        ///");
sb.AppendLine("        yield return new SnapCharacterInfo()");
sb.AppendLine("        {");
sb.AppendLine("            Id = -1,");
sb.AppendLine("            Name = \"Aozi\",");
sb.AppendLine("            Rarity = 4,");
sb.AppendLine("            Gender = 2,");
sb.AppendLine("            Element = ElementType.None,");
sb.AppendLine("            WeaponType = WeaponType.None,");
sb.AppendLine("            FaceIcon = \"UI_AvatarIcon_Aozi.png\",");
sb.AppendLine("            SortId = int.MaxValue,");
sb.AppendLine("            TextureOverride = ReShadeIniMapper.Map(-1),");
sb.AppendLine("        };");
sb.AppendLine();
sb.AppendLine("        yield return new SnapCharacterInfo()");
sb.AppendLine("        {");
sb.AppendLine("            Id = -2,");
sb.AppendLine("            Name = \"Paimon\",");
sb.AppendLine("            Rarity = 1,");
sb.AppendLine("            Gender = 2,");
sb.AppendLine("            Element = ElementType.None,");
sb.AppendLine("            WeaponType = WeaponType.None,");
sb.AppendLine("            FaceIcon = \"UI_AvatarIcon_Paimon.png\",");
sb.AppendLine("            SortId = -2,");
sb.AppendLine("            TextureOverride = ReShadeIniMapper.Map(-2),");
sb.AppendLine("        };");
sb.AppendLine("    }");
sb.AppendLine("}");

Console.WriteLine(sb.ToString());
Debugger.Break();

static string ToFileName(string id)
{
    if (id == "alhaitham")
    {
        id = "Alhatham";
    }
    else if (id == "amber")
    {
        id = "Ambor";
    }
    else if (id == "arataki_itto")
    {
        id = "Itto";
    }
    else if (id == "baizhu")
    {
        id = "Baizhuer";
    }
    else if (id == "baizhu")
    {
        id = "Baizhuer";
    }
    else if (id == "hu_tao")
    {
        id = "Hutao";
    }
    else if (id == "jean")
    {
        id = "Qin";
    }
    else if (id == "kaedehara_kazuha")
    {
        id = "Kazuha";
    }
    else if (id == "kamisato_ayaka")
    {
        id = "Ayaka";
    }
    else if (id == "kamisato_ayato")
    {
        id = "Ayato";
    }
    else if (id == "kirara")
    {
        id = "Momoka";
    }
    else if (id == "kujou_sara")
    {
        id = "Sara";
    }
    else if (id == "kuki_shinobu")
    {
        id = "Shinobu";
    }
    else if (id == "kuki_shinobu")
    {
        id = "Shinobu";
    }
    else if (id == "lynette")
    {
        id = "Linette";
    }
    else if (id == "lyney")
    {
        id = "Liney";
    }
    else if (id == "lan_yan")
    {
        id = "LanYan";
    }
    else if (id == "noelle")
    {
        id = "Noel";
    }
    else if (id == "ororon")
    {
        id = "Olorun";
    }
    else if (id == "raiden_shogun")
    {
        id = "Shougun";
    }
    else if (id == "sangonomiya_kokomi")
    {
        id = "Kokomi";
    }
    else if (id == "shikanoin_heizou")
    {
        id = "Heizo";
    }
    else if (id == "thoma")
    {
        id = "Tohma";
    }
    else if (id == "xianyun")
    {
        id = "Liuyun";
    }
    else if (id == "yae_miko")
    {
        id = "Yae";
    }
    else if (id == "yanfei")
    {
        id = "Feiyan";
    }
    else if (id == "yun_jin")
    {
        id = "Yunjin";
    }
    else if (id == "yumemizuki_mizuki")
    {
        id = "Mizuki";
    }

    return (char.ToUpper(id[0]) + id[1..]).Replace(" ", string.Empty);
}

[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
[SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
public static class StringExtensions
{
    public static string Capitalize(this object obj)
    {
        if (obj is null)
        {
            return string.Empty;
        }
        else if (obj is string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input[1..];
        }
        else
        {
            return obj.ToString()!.Capitalize();
        }
    }
}
