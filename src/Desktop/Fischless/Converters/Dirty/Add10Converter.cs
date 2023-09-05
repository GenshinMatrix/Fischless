﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fischless.Converters;

internal sealed class Add10Converter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value + 10;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
