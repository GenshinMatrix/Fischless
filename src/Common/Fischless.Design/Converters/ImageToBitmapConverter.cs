using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(Image), typeof(Bitmap))]
public sealed class ImageToBitmapConverter : IValueConverter
{
    public static ImageToBitmapConverter Instance => new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Image image)
        {
            return new Bitmap(image);
        }
        else if (value is Bitmap)
        {
            return value;
        }
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
