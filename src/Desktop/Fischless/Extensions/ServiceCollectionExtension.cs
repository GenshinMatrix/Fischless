using Fischless.Hosting.Absraction;
using Microsoft.Extensions.DependencyInjection;

namespace Fischless.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPlugins(this IServiceCollection services, IHost app)
    {
        app.UsePlugins(services)
           .Forget();
        return services;
    }
}
