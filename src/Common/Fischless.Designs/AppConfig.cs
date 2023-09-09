using Fischless.Hosting.Absraction;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Fischless.Designs;

internal class AppConfig
{
    public static T GetService<T>() where T : class
    {
        if (Application.Current is IHost app)
        {
            return app?.Services?.GetService<T>()!;
        }
        return null!;
    }

    public static object? GetService(Type type)
    {
        if (Application.Current is IHost app)
        {
            return app?.Services?.GetService(type)!;
        }
        return null!;
    }
}
