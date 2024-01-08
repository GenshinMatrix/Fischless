using Fischless.Fetch.Datas.Snap;
using System.Text.RegularExpressions;
using static Fischless.Fetch.ReShade.ReShadeSentimentalString;

namespace Fischless.Fetch.ReShade;

public static partial class ReShadeFolderWalker
{
    public static bool IsUseTextureImage { get; set; } = false;

    public static async IAsyncEnumerable<ReShadeFolder> EnumerateFolder(string entryFolder)
    {
        if (!Directory.Exists(entryFolder))
        {
            yield break;
        }

        foreach (string subfolder in Directory.EnumerateDirectories(entryFolder))
        {
            // Real yield
            {
                IEnumerable<string> iniFiles = Directory.EnumerateFiles(subfolder)
                    .Where(file => Path.GetExtension(file).Equals(EXT_INI, StringComparison.OrdinalIgnoreCase));

                if (!iniFiles.Any())
                {
                    // Fast exit
                    goto GOTO_GET_FOLDER;
                }

                IEnumerable<ReShadeFolderIni> ini = iniFiles
                    .Select(file => (file, text: File.ReadAllText(file)))
                    .Select(tuple => new ReShadeFolderIni()
                    {
                        FileName = new FileInfo(tuple.file).Name,
                        FilePath = tuple.file,
                        IsEnabled = !tuple.file.StartsWith(DISABLED),
                        IsMerged = DetectIniIsMerged(tuple.file, tuple.text),
                        TextureOverride = DetectIniTextureOverride(tuple.file, tuple.text),
                    });

                string folderName = new DirectoryInfo(subfolder).Name;
                ReShadeFolder folder = new()
                {
                    FolderName = folderName,
                    FolderPath = subfolder.Replace('/', Path.DirectorySeparatorChar),
                    IsEnabled = !folderName.StartsWith(DISABLED),
                    Inis = ini.ToArray(),
                };

#if DEBUG
                IEnumerable<string> imageFiles = EnumerateFolderImage(subfolder);
                folder.Images = imageFiles.ToArray();
#endif

                yield return folder;

                if (folder.Inis.Any(ini => ini.IsMerged))
                {
                    continue;
                }
            }

        GOTO_GET_FOLDER:
            // Recursion yield
            {
                await foreach (ReShadeFolder folder in EnumerateFolder(subfolder))
                {
                    yield return folder;
                }
            }
        }
    }

    public static IEnumerable<string> EnumerateFolderImage(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            return Array.Empty<string>();
        }

        if (IsUseTextureImage)
        {
            IEnumerable<string> imageFiles = Directory.EnumerateFiles(folderPath)
                .Where(file => Path.GetExtension(file).ToLower() switch
                {
                    EXT_PNG or EXT_JPG or EXT_JPEG or EXT_BMP or EXT_DDS => true,
                    _ => false,
                })
                .OrderBy(file => Path.GetExtension(file).ToLower() switch
                {
                    EXT_PNG => 1,
                    EXT_JPG or EXT_JPEG => 2,
                    EXT_BMP => 3,
                    EXT_DDS => 4,
                    _ => int.MaxValue,
                });
            return imageFiles;
        }
        else
        {
            IEnumerable<string> imageFiles = Directory.EnumerateFiles(folderPath)
                .Where(file => Path.GetExtension(file).ToLower() switch
                {
                    EXT_PNG or EXT_JPG or EXT_JPEG or EXT_BMP => true,
                    _ => false,
                })
                .OrderBy(file => Path.GetExtension(file).ToLower() switch
                {
                    EXT_PNG => 1,
                    EXT_JPG or EXT_JPEG => 2,
                    EXT_BMP => 3,
                    _ => int.MaxValue,
                });
            return imageFiles;
        }
    }

    public static bool DetectIniIsMerged(string file, string text)
    {
        if (file?.EndsWith(MERGED_INI, StringComparison.OrdinalIgnoreCase) ?? false)
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(text)
         && text.TrimStart().StartsWith(MERGED_COMMENT, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        return false;
    }

    public static string DetectIniTextureOverride(string file, string text)
    {
        using StringReader reader = new(text);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            Match match = TextureOverrideRegex().Match(line);

            if (match.Success)
            {
                string result = match.Groups[1].Value.Trim();

                if (!SnapCharacterProvider.GetSnapCharacters().Any(avatar =>
                {
                    if (!string.IsNullOrWhiteSpace(avatar.TextureOverride))
                    {
                        foreach (string tex in avatar.TextureOverride.SplitTextureOverride())
                        {
                            if (tex.EndsWith('$'))
                            {
                                if (result?.StartsWith(tex[..^1], StringComparison.OrdinalIgnoreCase) ?? false)
                                {
                                    if (result.Length >= tex.Length + 2 && char.IsUpper(result.Substring(tex.Length + 1, 1)[0]))
                                    {
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                if (result?.StartsWith(tex, StringComparison.OrdinalIgnoreCase) ?? false)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }))
                {
                    continue;
                }
                return result;
            }
        }
        return null!;
    }

    [GeneratedRegex(TextureOverridePattern)]
    private static partial Regex TextureOverrideRegex();
}
