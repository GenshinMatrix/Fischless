using Fischless.Hosting.Absraction;
using Microsoft.Extensions.DependencyInjection;

namespace Fischless.Hosting;

public class HostBuilder : IHostBuilder
{
    public ICommandLineBuilder? CommandLineBuilder { get; set; }
    public IServiceCollection Services { get; set; } = null!;

    public IHostBuilder ConfigureDefaults(string[]? args)
    {
        CommandLineBuilder = new CommandLineBuilder(args);
        Services = new ServiceCollection();
        Services.AddSingleton(Services);
        Services.AddSingleton(CommandLineBuilder);
        return this;
    }

    public IHost Build()
    {
        ServiceProvider serviceProvider = Services.BuildServiceProvider();
        IHost host = serviceProvider.GetRequiredService<IHost>();

        host.Services = serviceProvider;
        return host;
    }
}
