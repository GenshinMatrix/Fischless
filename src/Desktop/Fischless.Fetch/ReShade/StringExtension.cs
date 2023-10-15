namespace Fischless.Fetch.ReShade;

public static class StringExtension
{
    public static bool IsEnabledFolderPath(this string folderPath)
    {
        return !IsDisabledFolderPath(folderPath);
    }

    public static bool IsDisabledFolderPath(this string folderPath)
    {
        return new DirectoryInfo(folderPath).Name?.StartsWith(ReShadeSentimentalString.DISABLED, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public static string RemoveDisabledPrefix(this string fileName)
    {
        while (fileName.StartsWith(ReShadeSentimentalString.DISABLED, StringComparison.OrdinalIgnoreCase))
        {
            fileName = fileName[ReShadeSentimentalString.DISABLED.Length..];
        }
        return fileName;
    }

    public static string[] SplitTextureOverride(this string textureOverride)
    {
        return textureOverride.Split(';', ',', '|');
    }
}
