using Fischless.Configuration;
using Fischless.Design.Themes;
using Fischless.Helpers;
using Fischless.Hosting.Absraction;
using Fischless.Logging;
using Fischless.Mapper;
using Fischless.Models;
using Fischless.Native;
using Microsoft.AppCenter;
using System;
using System.Threading.Tasks;

namespace Fischless.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseElevated(this IApplicationBuilder app)
    {
        if (Configurations.EnsureElevated.Get())
        {
            RuntimeHelper.EnsureElevated();
        }
        return app;
    }

    public static IApplicationBuilder UseSingleInstance(this IApplicationBuilder app, string instanceName, Action<bool> callback = null!)
    {
        RuntimeHelper.CheckSingleInstance(instanceName, callback);
        return app;
    }

    public static IApplicationBuilder UseMapper(this IApplicationBuilder app)
    {
        MapperAssemblyResolver.Register();
        return app;
    }

    public static IApplicationBuilder UseAppCenter(this IApplicationBuilder app, string userId = null!, string appSecret = null!)
    {
        if (new Version(AppCenter.SdkVersion).Major < 5)
        {
            AppCenterProvider.Start(userId ?? HardInfoHelper.ComputerIdentityMD5, appSecret ?? AppConfig.AppCenterSecret.AppSecret);
        }
        else
        {
            AppCenterProvider.StartPrepare(userId ?? HardInfoHelper.ComputerIdentityMD5, appSecret ?? AppConfig.AppCenterSecret.AppSecret);
        }
        return app;
    }

    public static IApplicationBuilder UseDpiAware(this IApplicationBuilder app)
    {
        _ = DpiAwareHelper.SetProcessDpiAwareness();
        return app;
    }

    public static IApplicationBuilder UseDomainUnhandledExceptionCatched(this IApplicationBuilder app, UnhandledExceptionEventHandler handler = null!)
    {
        if (handler != null)
        {
            AppDomain.CurrentDomain.UnhandledException += handler;
        }
        else
        {
            AppDomain.CurrentDomain.UnhandledException += (object s, UnhandledExceptionEventArgs e) =>
            {
                Log.Critical("AppDomain.CurrentDomain.UnhandledException " + e?.ExceptionObject?.ToString() ?? string.Empty);
                AppCenterProvider.TrackError(e?.ExceptionObject as Exception);
            };
        }
        return app;
    }

    public static IApplicationBuilder UseUnobservedTaskExceptionCatched(this IApplicationBuilder app, EventHandler<UnobservedTaskExceptionEventArgs> handler = null!)
    {
        if (handler != null)
        {
            TaskScheduler.UnobservedTaskException += handler;
        }
        else
        {
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Log.Critical("TaskScheduler.UnobservedTaskException " + e?.Exception?.ToString() ?? string.Empty);
                AppCenterProvider.TrackError(e?.Exception);
                e?.SetObserved();
            };
        }
        return app;
    }
    
    public static IApplicationBuilder UseConfiguration(this IApplicationBuilder app)
    {
        ConfigurationManager.Setup(SpecialPathHelper.GetPath("config.yaml"));
        return app;
    }

    public static IApplicationBuilder UseTheme(this IApplicationBuilder app)
    {
        if (Configurations.ThemeTextFontFamily.Get() > 0)
        {
            ThemeFontFamilyProvider.ChangeFontFamily((ThemeTextFontFamily)Configurations.ThemeTextFontFamily.Get());
        }

        if (Configurations.IsUseThemeCursor)
        {
            ThemeCursorProvider.ChangeCursor(new Uri("pack://application:,,,/Fischless;component/Assets/Images/UI_Img_Cursor_PC.png"));
        }
        return app;
    }

    public static IApplicationBuilder UseProtocol(this IApplicationBuilder app)
    {
        UrlProtocolHelper.Register();
        return app;
    }

    public static void Forget(this IApplicationBuilder builder)
    {
    }
}
