using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Fischless.ModelViewer.Extensions;

namespace Fischless.ModelViewer.MMD;

public class PMXProvider : IDisposable
{
    private ArchiveStock stock;
    private string path = null;

    public void Dispose()
    {
        path = null;
        stock?.Dispose();
        stock = null;
    }

    private static bool IsArchive(string path)
    {
        FileInfo fi = new(path);

        if (fi.Extension.ToLower() == ".zip" || fi.Extension.ToLower() == ".7z")
        {
            return true;
        }
        return false;
    }

    public void Load(string path)
    {
        Dispose();

        if (IsArchive(path))
        {
            stock = new(path);
        }
    }

    public async Task<Stream> GetPMX(Func<string[], Task<string>> selector)
    {
        string[] pmxs = stock.ContentDict?.Keys.Where(k => k.ToLower().EndsWith(".pmx")).ToArray();

        if (pmxs == null || pmxs.Length <= 0)
        {
            return null;
        }

        if (pmxs.Length == 1 || selector == null)
        {
            path = pmxs.First();
        }
        else
        {
            path = await selector.Invoke(pmxs);
        }
        if (path == null)
        {
            return null;
        }
        return stock.ContentDict[path];
    }

    public async Task<PMXFormat> GetPMXFormat(string path, Func<string[], Task<string>> selector = null)
    {
        if (IsArchive(path))
        {
            Stream pmxStream = await GetPMX(selector);

            if (pmxStream == null)
            {
                return null;
            }
            return PMXLoaderScript.Import(pmxStream);
        }
        return PMXLoaderScript.Import(path);
    }

    public ImageSource GetTexture(string folder, string texturePath)
    {
        string ext = new FileInfo(texturePath).Extension.ToLower();

        if (stock == null)
        {
            string path = Path.Combine(folder, texturePath);

            if (PfimxExtension.IsPfimx(ext))
            {
                return PfimxExtension.ToBitmapImage(path);
            }
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
        else
        {
            static string GetRelativePath(string relativeFrom, string path)
            {
                string[] pathSplitted = path.Replace('/', '\\').ToLower().Split('\\');

                if (pathSplitted.Length >= 2)
                {
                    string[] pathNew = new string[pathSplitted.Length - 1];

                    Array.Copy(pathSplitted, pathNew, pathSplitted.Length - 1);

                    return $"{string.Join("\\", pathNew)}\\{relativeFrom.Replace('/', '\\').ToLower()}";
                }
                else
                {
                    return relativeFrom.Replace('/', '\\').ToLower();
                }
            }

            string key = GetRelativePath(texturePath, path);

            Debug.WriteLine($"[LoadKey] {key}");
            Stream image = stock.ContentDict[key];

            image.Seek(0, SeekOrigin.Begin);
            if (PfimxExtension.IsPfimx(ext))
            {
                return PfimxExtension.ToBitmapImage(image);
            }
            return BitmapExtension.ToBitmapImage(image);
        }
    }
}
