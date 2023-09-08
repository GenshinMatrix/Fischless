﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fischless.Designs.Converters;

internal sealed class IsNullConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
