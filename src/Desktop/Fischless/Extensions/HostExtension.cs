using Fischless.Core;
using Fischless.Hosting.Absraction;
using Fischless.Plugin.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Fischless.Extensions;

public static class HostExtension
{
    public static IHost UsePlugins(this IHost app, IServiceCollection services)
    {
        IEnumerable<PluginBuilder> pluginBuilders = PluginProvider.Reload();

        foreach (PluginBuilder pluginBuilder in pluginBuilders)
        {
            pluginBuilder?.UseService(services);
        }
        return app;
    }

    public static IHost UseMuiLanguage(this IHost builder)
    {
        MuiLanguage.SetupLanguage();
        return builder;
    }

    public static IHost UseDispatcherUnhandledExceptionCatched(this IHost app, DispatcherUnhandledExceptionEventHandler handler = null!)
    {
        if (app != null)
        {
            if (handler != null)
            {
                if (app is Application appX)
                {
                    appX.DispatcherUnhandledException += handler;
                }
            }
            else
            {
                if (app is Application appX)
                {
                    appX.DispatcherUnhandledException += (object s, DispatcherUnhandledExceptionEventArgs e) =>
                    {
                        Log.Fatal("Application.DispatcherUnhandledException " + e?.Exception?.ToString() ?? string.Empty);
                        AppCenterProvider.TrackError(e?.Exception);
                        e!.Handled = true;
                    };
                }
            }
        }
        return app!;
    }

    public static void Forget(this IHost builder)
    {
    }
}
