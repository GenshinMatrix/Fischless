﻿using Fischless.Fetch.Datas.Character;
using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.Datas.Snap;
using LiteDB;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Fischless.SnapCharacter;

internal static class SnapCharacterBenchmark
{
    public static void Action()
    {
        List<SnapCharacterInfo> snapCharacters = new(999);

        try
        {
            foreach (string json in GetTxtStrings())
            {
                if (string.IsNullOrEmpty(json))
                {
                    return;
                }

                CharacterInfo character = System.Text.Json.JsonSerializer.Deserialize<CharacterInfo>(json);
                SnapCharacterInfo snapCharacter = TryMapProperties<SnapCharacterInfo>(character);

                if (character.Outfits != null && character.Outfits.Count > 0)
                {
                    foreach (var outfit in character.Outfits)
                    {
                        SnapCharacterOutfit snapOutfit = TryMapProperties<SnapCharacterOutfit>(outfit);

                        snapCharacter.Outfits ??= new();
                        snapCharacter.Outfits.Add(snapOutfit);
                    }
                }

                snapCharacters.Add(snapCharacter);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }

        if (snapCharacters.Any())
        {
            string code = GenerateSnapCharacterProviderCode(snapCharacters);

            File.WriteAllText("SnapCharacterProvider.cs", code);
        }
    }

    private static IEnumerable<string> GetTxtStrings()
    {
        using LiteDatabase db = new(@"C:\Users\ema\Documents\Xunkong\Database\GenshinData.db");
        ILiteCollection<BsonDocument> characterInfoCollection = db.GetCollection("CharacterInfo");
        IEnumerable<BsonDocument>? result = characterInfoCollection.FindAll();

        foreach (BsonDocument? document in result)
        {
            object? bsonObject = JsonConvert.DeserializeObject(document.ToString());
            string jsonString = JsonConvert.SerializeObject(bsonObject, Formatting.Indented);
            yield return jsonString;
        }
    }

    private static string GenerateSnapCharacterProviderCode(IEnumerable<SnapCharacterInfo> snapCharacters)
    {
        StringBuilder code = new();

        code.AppendLine("using Fischless.Fetch.Datas.Core;");
        code.AppendLine("using Fischless.Fetch.Datas.Snap;");
        code.AppendLine();
        code.AppendLine("namespace Fischless.Fetch.Datas.Snap;");
        code.AppendLine();
        code.AppendLine("public static class SnapCharacterProvider");
        code.AppendLine("{");
        code.AppendLine("    public static IEnumerable<SnapCharacterInfo> GetSnapCharacters()");
        code.AppendLine("    {");

        foreach (var character in snapCharacters)
        {
            code.AppendLine("        yield return new SnapCharacterInfo()");
            code.AppendLine("        {");

            foreach (var property in character.GetType().GetProperties())
            {
                var value = property.GetValue(character);
                var valueType = property.PropertyType;

                if (valueType == typeof(string))
                {
                    if (value is string valueString && valueString.StartsWith("http"))
                    {
                        value = Path.GetFileName(valueString);
                    }
                    code.AppendLine($"            {property.Name} = \"{value}\",");
                }
                else if (valueType == typeof(bool))
                {
                    code.AppendLine($"            {property.Name} = {value.ToString().ToLower()},");
                }
                else if (valueType == typeof(ElementType))
                {
                    code.AppendLine($"            {property.Name} = ElementType.{value.ToString()},");
                }
                else if (valueType == typeof(WeaponType))
                {
                    code.AppendLine($"            {property.Name} = WeaponType.{value.ToString()},");
                }
                else if (valueType == typeof(DateTime))
                {
                    code.AppendLine($"            {property.Name} = \"{((DateTime)value!).ToString("yyyy-MM-ddTHH:mm:ss.fffffff0Z")}\".ParseExactDateTime(),");
                }
                else if (valueType == typeof(List<SnapCharacterOutfit>))
                {
                    var outfitList = (List<SnapCharacterOutfit>)value;
                    code.AppendLine($"            {property.Name} = new List<SnapCharacterOutfit>");
                    code.AppendLine("            {");

                    foreach (var outfit in outfitList)
                    {
                        code.AppendLine("                new SnapCharacterOutfit()");
                        code.AppendLine("                {");

                        foreach (var outfitProperty in outfit.GetType().GetProperties())
                        {
                            var outfitValue = outfitProperty.GetValue(outfit);
                            var outfitValueType = outfitProperty.PropertyType;

                            if (outfitValueType == typeof(string))
                            {
                                if (outfitValue is string valueString && valueString.StartsWith("http"))
                                {
                                    outfitValue = Path.GetFileName(valueString);
                                }
                                code.AppendLine($"                    {outfitProperty.Name} = \"{outfitValue}\",");
                            }
                            else if (outfitValueType == typeof(bool))
                            {
                                code.AppendLine($"                    {outfitProperty.Name} = {outfitValue.ToString().ToLower()},");
                            }
                            else
                            {
                                code.AppendLine($"                    {outfitProperty.Name} = {outfitValue ?? "null"},");
                            }
                        }

                        code.AppendLine("                },");
                    }

                    code.AppendLine("            },");
                }
                else
                {
                    code.AppendLine($"            {property.Name} = {value ?? "null"},");
                }
            }

            code.AppendLine("        };");
        }

        code.AppendLine("    }");
        code.AppendLine("}");

        return code.ToString();
    }

    private static T TryMapProperties<T>(object source)
    {
        T target = Activator.CreateInstance<T>();
        Type sourceType = source.GetType();
        Type targetType = typeof(T);

        PropertyInfo[] sourceProperties = sourceType.GetProperties();
        PropertyInfo[] targetProperties = targetType.GetProperties();

        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            PropertyInfo targetProperty = Array.Find(targetProperties, p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

            if (targetProperty != null && targetProperty.CanWrite)
            {
                object value = sourceProperty.GetValue(source);
                targetProperty.SetValue(target, value);
            }
        }

        return target;
    }
}
