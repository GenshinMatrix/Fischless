using Fischless.Compression;
using System.Diagnostics;
using System.IO;

namespace Fischless.ModelViewer;

public sealed class ArchiveStock : IDisposable
{
    public Dictionary<string, Stream> ContentDict = new();

    public ArchiveStock(string path)
    {
        Dispose();
        try
        {
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
