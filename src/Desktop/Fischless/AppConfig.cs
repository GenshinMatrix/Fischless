using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Windows;

namespace Fischless;

internal static class AppConfig
{
    public static string? AppVersion { get; private set; }
    public static string LogFile { get; internal set; }

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
        return (Application.Current as App)?.Services?.GetService<T>()!;
    }

    public static ILogger<T> GetLogger<T>() where T : class
    {
        return (Application.Current as App)?.Services?.GetService<ILogger<T>>()!;
    }

    public static Serilog.ILogger GetLogger()
    {
        return (Application.Current as App)?.Services?.GetService<Serilog.ILogger>()!;
    }

    public record ILoggerRecord { }
}
