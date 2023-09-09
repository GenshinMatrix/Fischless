using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Resources;

namespace Fischless.Globalization.Helpers;

internal static class ResourceHelper
{
    static ResourceHelper()
    {
        if (!UriParser.IsKnownScheme("pack"))
            _ = PackUriHelper.UriSchemePack;
    }

    public static bool HasResource(string uriString)
    {
        try
        {
            using Stream stream = GetStream(uriString);
            _ = stream;
            return true;
        }
        catch
        {
        }
        return false;
    }

    public static Stream GetStream(string uriString)
    {
        Uri uri = new(uriString);
        StreamResourceInfo info = Application.GetResourceStream(uri);
        return info?.Stream!;
    }
}
