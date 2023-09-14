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
                MessageBoxIcon.Info => FontSymbols.Info,
                MessageBoxIcon.Success => FontSymbols.Accept,
                MessageBoxIcon.Warning => FontSymbols.Warning,
                MessageBoxIcon.Error => FontSymbols.Cancel,
                MessageBoxIcon.Question => FontSymbols.Unknown,
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
