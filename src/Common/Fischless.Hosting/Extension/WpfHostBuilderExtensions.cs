using Fischless.Hosting.Absraction;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Fischless.Hosting.Extension;

public static class WpfHostBuilderExtensions
{
    public static IWpfHostBuilder UseStartup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods)] TStartup>(this IWpfHostBuilder wpfHostBuilder) where TStartup : class
    {
        IServiceCollection services = wpfHostBuilder.HostBuilder.Services;

        services.AddSingleton<IApplicationBuilder>(new ApplicationBuilder());
        services.AddSingleton<IWpfHostEnvironment>(new WpfHostEnvironment(wpfHostBuilder.HostBuilder.CommandLineBuilder));
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        TStartup? startup = (TStartup?)Activator.CreateInstance(typeof(TStartup));

        if (startup != null)
        {
            MethodInfo[] methods = typeof(TStartup).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                object[] paramValues = new object[parameters.Length];

                for (int i = default; i < parameters.Length; i++)
                {
                    paramValues[i] = serviceProvider.GetRequiredService(parameters[i].ParameterType);
                }

                method.Invoke(startup, paramValues);
            }
        }
        return wpfHostBuilder;
    }
}
