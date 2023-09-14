using Fischless.Design.Extension;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(Bitmap), typeof(BitmapSource))]
public sealed class BitmapToBitmapSourceConverter : IValueConverter
{
    public static BitmapToBitmapSourceConverter Instance => new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Bitmap bitmap)
        {
            return bitmap.ToBitmapSource();
        }
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
