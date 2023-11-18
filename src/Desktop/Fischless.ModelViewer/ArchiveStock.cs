using Fischless.Compression;
using System.Collections.Frozen;
using System.Diagnostics;
using System.IO;

namespace Fischless.ModelViewer;

public sealed class ArchiveStock : IDisposable
{
    public FrozenDictionary<string, Stream> ContentDict = null!;

    public ArchiveStock(string path)
    {
        Dispose();
        try
        {
            ContentDict = ArchiveExtractor.ExtractFilesToMemory(path).ToFrozenDictionary();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
    }

    public void Dispose()
    {
        if (ContentDict == null)
        {
            return;
        }
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
        ContentDict = null!;
    }
}
