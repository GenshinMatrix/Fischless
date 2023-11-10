using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(bool))]
public class MinMaxValueToBoolConverter : SingletonConverterBase<MinMaxValueToBoolConverter>
{
    public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register(
            nameof(MaxValue),
            typeof(object),
            typeof(MinMaxValueToBoolConverter),
            new PropertyMetadata(defaultValue: null));

    public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register(
            nameof(MinValue),
            typeof(object),
            typeof(MinMaxValueToBoolConverter),
            new PropertyMetadata(defaultValue: null));

    public object MaxValue
    {
        get => this.GetValue(MaxValueProperty);
        set => this.SetValue(MaxValueProperty, value);
    }

    public object MinValue
    {
        get => this.GetValue(MinValueProperty);
        set => this.SetValue(MinValueProperty, value);
    }

    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IComparable comparable)
        {
            return UnsetValue;
        }

        if (this.MinValue is not IComparable)
        {
            throw new ArgumentException("MinValue must implement IComparable interface", nameof(this.MinValue));
        }

        if (this.MaxValue is not IComparable)
        {
            throw new ArgumentException("MaxValue must implement IComparable interface", nameof(this.MaxValue));
        }

        var minValue = System.Convert.ChangeType(this.MinValue, comparable.GetType());
        var maxValue = System.Convert.ChangeType(this.MaxValue, comparable.GetType());

        return (comparable.CompareTo(minValue) >= 0 && comparable.CompareTo(maxValue) <= 0);
    }
}
