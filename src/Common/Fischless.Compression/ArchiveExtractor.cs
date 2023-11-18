using SharpCompress.Archives;

namespace Fischless.Compression;

public static class ArchiveExtractor
{
    public static Dictionary<string, Stream> ExtractFilesToMemory(string path)
    {
        using Stream stream = File.OpenRead(path);
        return ExtractFilesToMemory(stream);
    }

    public static Dictionary<string, Stream> ExtractFilesToMemory(Stream stream)
    {
        Dictionary<string, Stream> fileContents = [];
        using IArchive archive = ArchiveFactory.Open(stream);

        foreach (var entry in archive.Entries)
        {
            if (!entry.IsDirectory)
            {
                using Stream entryStream = entry.OpenEntryStream();
                string key = entry.Key.Replace('/', '\\');
                MemoryStream memoryStream = new();

                entryStream.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                fileContents[key] = memoryStream;
            }
        }
        return fileContents;
    }
}

file static class StreamExtension
{
    public static void SaveToFile(this Stream stream, string filePath)
    {
        Directory.CreateDirectory(new FileInfo(filePath).DirectoryName);
        using FileStream fileStream = new(filePath, FileMode.Create);
        byte[] buffer = new byte[4096];
        int bytesRead;
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            fileStream.Write(buffer, 0, bytesRead);
        }
    }
}
