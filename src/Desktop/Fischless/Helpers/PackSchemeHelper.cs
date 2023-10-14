using System;
using System.IO.Packaging;

namespace Fischless.Design.Helpers;

internal sealed class PackSchemeHelper
{
    public const string AssetsPrefix = "pack://application:,,,/Fischless;component/Assets/";
    public const string ImagesPrefix = "pack://application:,,,/Fischless;component/Assets/Images/";

    static PackSchemeHelper()
    {
        if (!UriParser.IsKnownScheme("pack"))
            _ = PackUriHelper.UriSchemePack;
    }
}
