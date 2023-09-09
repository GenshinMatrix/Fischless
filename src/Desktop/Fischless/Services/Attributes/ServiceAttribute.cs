using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fischless.Services.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ServiceAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;

    public ServiceAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
