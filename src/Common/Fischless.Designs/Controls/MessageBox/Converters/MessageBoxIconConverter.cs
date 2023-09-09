using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fischless.Designs.Controls;

[ValueConversion(typeof(MessageBoxIcon), typeof(string))]
public sealed class MessageBoxIconConverter : IValueConverter
{
    public static MessageBoxIconConverter Instance => new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is MessageBoxIcon icon)
        {
            return icon switch
            {
                MessageBoxIcon.Info => "\xf4a2",
                MessageBoxIcon.Success => "\xf297",
                MessageBoxIcon.Warning => "\xf868",
                MessageBoxIcon.Error => "\xf368",
                MessageBoxIcon.Question => "\xf63c",
                MessageBoxIcon.None or _ => string.Empty,
            };
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
