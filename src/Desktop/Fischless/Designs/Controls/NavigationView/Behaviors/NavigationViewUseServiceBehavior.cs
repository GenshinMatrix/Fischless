﻿using Microsoft.Xaml.Behaviors;
using ModernWpf.Controls;
using Serilog;
using System;
using System.Reflection;

namespace Fischless.Designs.Controls;

public sealed class NavigationViewUseServiceBehavior : Behavior<NavigationView>
{
    public Type ServiceType { get; set; } = null!;

    protected override void OnAttached()
    {
        try
        {
            PropertyInfo navigationViewProperty = ServiceType?.GetProperty("NavigationView", BindingFlags.Public | BindingFlags.Static);
            navigationViewProperty?.SetValue(null!, AssociatedObject);
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
        base.OnAttached();
    }
}
