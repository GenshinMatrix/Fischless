using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(ImageSource))]
public sealed class UriToImageSourceConverter : SingletonConverterBase<UriToImageSourceConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int? decodePixelWidth = null;
        if (parameter is string pixel)
        {
            decodePixelWidth = int.Parse(pixel);
        }

        if (value is Uri uri)
        {
            return uri.ToImageSource(decodePixelWidth);
        }
        else if (value is string uriString)
        {
            return new Uri(uriString).ToImageSource(decodePixelWidth);
        }
        return null!;
    }
}

file static class ImageExtension
{
    public static ImageSource ToImageSource(this Uri imageUri, int? decodePixelWidth = null)
    {
        BitmapImage imageSource = new();

        imageSource.BeginInit();
        imageSource.CacheOption = BitmapCacheOption.OnLoad;
        imageSource.UriSource = imageUri;
        if (decodePixelWidth != null)
        {
            imageSource.DecodePixelWidth = decodePixelWidth.Value;
        }
        imageSource.EndInit();
        imageSource.Freeze();
        return imageSource;
    }
}
