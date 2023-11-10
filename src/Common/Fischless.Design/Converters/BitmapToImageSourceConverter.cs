using Fischless.Design.Extension;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(Bitmap), typeof(ImageSource))]
public sealed class BitmapToImageSourceConverter : SingletonConverterBase<BitmapToImageSourceConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Bitmap bitmap)
        {
            return bitmap.ToImageSource();
        }
        return null!;
    }
}
