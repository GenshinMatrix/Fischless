using Fischless.Hosting.Absraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Windows;

namespace Fischless;

internal static class AppConfig
{
    public static string? AppName => Mui(PackName) ?? PackName;
    public static string? PackName => "Fischless";
    public static string? AutoStartCommand => "-autostart";
    public static string Website => "https://github.com/GenshinMatrix/Fischless";
    public static bool Preview => true;
    public static string? AppVersion { get; private set; }
    public static string LogFolder { get; internal set; }

    static AppConfig()
    {
        LoadConfiguration();
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
}
