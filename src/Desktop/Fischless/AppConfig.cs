using Fischless.Hosting.Absraction;
using Fischless.Native;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Windows;

namespace Fischless;

internal static class AppConfig
{
    public static string? AppName => "Fischless";
    public static string? AppVersion { get; private set; }
    public static string LogFile { get; internal set; }
    public static AppCenterSecret AppCenterSecret { get; private set; }
    public static string UserId => AppCenterSecret.UserId;

    static AppConfig()
    {
        LoadAppCenterSecret();
        LoadConfiguration();
    }

    private static void LoadAppCenterSecret()
    {
        AppCenterSecret = new AppCenterSecret()
        {
            Owner = "GenshinMatrix",
            AppName =
#if DEBUG
                "Fischless_Debug",
#else
                "Fischless_Release",
#endif
            UserId = HardInfoHelper.ComputerIdentityMD5,
            AppSecret =
#if DEBUG
                "8323035c-2874-45d7-bb2a-9f96ba612595",
#else
                "36ccce6a-a3e1-4024-8fbd-193a2231af2d",
#endif
        };
    }

    private static void LoadConfiguration()
    {
        AppVersion = typeof(AppConfig).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }

    public static T GetService<T>() where T : class
    {
        if (Application.Current is IHost app)
        {
            return app.Services?.GetService<T>()!;
        }
        return null!;
    }

    public static object? GetService(Type type)
    {
        if (Application.Current is IHost app)
        {
            return app.Services?.GetService(type)!;
        }
        return null!;
    }

    public static ILogger<T> GetLogger<T>() where T : class
    {
        return GetService<ILogger<T>>()!;
    }

    public static Serilog.ILogger GetLogger()
    {
        return GetService<Serilog.ILogger>()!;
    }
}
