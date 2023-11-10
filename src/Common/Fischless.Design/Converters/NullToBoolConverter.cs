using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(bool))]
public class NullToBoolConverter : SingletonConverterBase<NullToBoolConverter>
{
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(NullToBoolConverter),
        new PropertyMetadata(false));

    public bool IsInverted
    {
        get { return (bool)this.GetValue(IsInvertedProperty); }
        set { this.SetValue(IsInvertedProperty, value); }
    }

    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null ^ this.IsInverted;
    }
}
