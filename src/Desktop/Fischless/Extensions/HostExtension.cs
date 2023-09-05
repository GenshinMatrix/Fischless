using Fischless.Hosting.Absraction;
using Serilog;
using System.Windows;
using System.Windows.Threading;

namespace Fischless.Extensions;

public static class HostExtension
{
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
}
