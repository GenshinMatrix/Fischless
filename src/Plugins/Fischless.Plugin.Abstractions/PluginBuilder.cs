using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Fischless.Plugin.Abstractions;

public sealed class PluginBuilder
{
    public IPlugin Plugin { get; set; }
    public IEnumerable<Type> PluginPageTypes { get; set; }

    public PluginBuilder(IPlugin plugin)
    {
        Plugin = plugin;
        PluginPageTypes = plugin.GetType().Assembly.GetExportedTypes()
            .Where(t => typeof(IPluginPage).IsAssignableFrom(t));
    }

    public void UseService(IServiceCollection services)
    {
        foreach (Type type in PluginPageTypes)
        {
            if (type.GetCustomAttribute<PluginPageAttribute>() is PluginPageAttribute attr)
            {
                switch (attr.Lifetime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(type);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(type);
                        break;

                    case ServiceLifetime.Singleton:
                        services.AddSingleton(type);
                        break;
                }
            }
        }
    }

    public IEnumerable<IPluginPage> Build()
    {
        foreach (Type type in PluginPageTypes)
        {
            yield return Activator.CreateInstance(type) as IPluginPage;
        }
    }
}
