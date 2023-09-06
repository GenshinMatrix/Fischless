using Microsoft.Extensions.DependencyInjection;

namespace Fischless.Plugin.Abstractions;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class PluginPageAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; set; }

    public PluginPageAttribute()
    {
    }

    public PluginPageAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
