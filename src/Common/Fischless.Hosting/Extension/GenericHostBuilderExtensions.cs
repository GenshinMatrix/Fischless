﻿using Fischless.Hosting.Absraction;
using System;

namespace Fischless.Hosting.Extension;

public static class GenericHostBuilderExtensions
{
    public static IHostBuilder ConfigureWpfHostDefaults(this IHostBuilder builder, Action<IWpfHostBuilder> configure)
    {
        configure?.Invoke(new WpfHostBuilder(builder));
        return builder;
    }
}
