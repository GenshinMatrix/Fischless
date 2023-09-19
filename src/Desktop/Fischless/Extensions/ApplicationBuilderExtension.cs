using Fischless.Configuration;
using Fischless.Helpers;
using Fischless.Hosting.Absraction;
using Fischless.Mapper;
using Fischless.Models;
using Fischless.Native;
using Microsoft.AppCenter;
using Serilog;
using System;
using System.IO;
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
                Log.Fatal("AppDomain.CurrentDomain.UnhandledException " + e?.ExceptionObject?.ToString() ?? string.Empty);
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
                Log.Fatal("TaskScheduler.UnobservedTaskException " + e?.Exception?.ToString() ?? string.Empty);
                AppCenterProvider.TrackError(e?.Exception);
                e?.SetObserved();
            };
        }
        return app;
    }

    public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
    {
        string logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Fischless\log");
        Directory.CreateDirectory(logFolder);
        AppConfig.LogFile = Path.Combine(logFolder, $"Fischless_{DateTime.Now:yyMMdd_HHmmss}.log");
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(path: AppConfig.LogFile, outputTemplate: "{Timestamp:HH:mm:ss.fff}|{Level:u4}|{SourceContext}|{Message}{NewLine}{Exception}")
            .Enrich.FromLogContext()
            .CreateLogger();
        return app;
    }

    public static IApplicationBuilder UseConfiguration(this IApplicationBuilder app)
    {
        ConfigurationManager.Setup(SpecialPathHelper.GetPath("config.yaml"));
        return app;
    }

    public static void Forget(this IApplicationBuilder builder)
    {
    }
}
