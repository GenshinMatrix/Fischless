using System.Diagnostics;

namespace Fischless.Fetch.ReShade;

[DebuggerDisplay("{FolderPath}")]
public class ReShadeFolder
{
    public string FolderName { get; set; }

    public string FolderPath { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ReShadeFolderIni[] Inis { get; set; } = Array.Empty<ReShadeFolderIni>();

    public string[] Images { get; set; } = Array.Empty<string>();
}

public class ReShadeFolderIni
{
    public string FileName { get; set; }

    public string FilePath { get; set; }

    public bool IsEnabled { get; set; } = true;

    public bool IsMerged { get; set; } = false;

    public string TextureOverride { get; set; }
}
