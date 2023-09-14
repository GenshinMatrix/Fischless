using Fischless.Design.Extension;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(Bitmap), typeof(ImageSource))]
public sealed class BitmapToImageSourceConverter : IValueConverter
{
    public static BitmapToImageSourceConverter Instance => new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Bitmap bitmap)
        {
            return bitmap.ToImageSource();
        }
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
