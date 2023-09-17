using YamlDotNet.Serialization;

namespace Fischless.Fetch.Lazy;

internal class LazyOutputSerializer
{
    public static string SerializeObject<T>(T obj)
    {
        Serializer serializer = new();
        return serializer.Serialize(obj!);
    }

    public static T DeserializeObject<T>(string input)
    {
        IDeserializer deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        return deserializer.Deserialize<T>(input);
    }

    public static bool SerializeFile<T>(string fileName, T obj)
    {
        bool ret = false;

        try
        {
            Serializer serializer = new();
            string str = serializer.Serialize(obj!);
            using StreamWriter sw = File.CreateText(fileName);

            sw.Write(str);
            sw.Flush();
            ret = true;
        }
        catch (Exception e)
        {
            _ = e;
        }
        return ret;
    }

    public static T DeserializeFile<T>(string fileName)
    {
        T info = default!;

        try
        {
            Deserializer deserializer = new();
            using StreamReader reader = new(fileName);
            info = deserializer.Deserialize<T>(reader);
        }
        catch (Exception e)
        {
            _ = e;
        }
        return info;
    }
}
