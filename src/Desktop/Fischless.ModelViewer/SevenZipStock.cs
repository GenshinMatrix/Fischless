using Fischless.Compression;
using System.Diagnostics;
using System.IO;

namespace Fischless.ModelViewer;

public class SevenZipStock : IDisposable
{
    public Dictionary<string, Stream> ContentDict = new();

    public SevenZipStock(string path)
    {
        try
        {
            Dispose();
            ContentDict = ArchiveExtractor.ExtractFilesToMemory(path);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
    }

    public void Dispose()
    {
        foreach (var pair in ContentDict)
        {
            try
            {
                pair.Value.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        ContentDict.Clear();
    }
}
