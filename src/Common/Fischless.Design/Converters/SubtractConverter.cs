﻿using System.Globalization;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(object))]
public class SubtractConverter : SingletonConverterBase<SubtractConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (double.TryParse(value?.ToString(), NumberStyles.Any, culture, out var basis)
            && double.TryParse(parameter?.ToString(), NumberStyles.Any, culture, out var subtract))
        {
            return basis - subtract;
        }

        return UnsetValue;
    }
}
