using Microsoft.Extensions.DependencyInjection;

namespace Fischless.Hosting.Absraction;

public interface IHostBuilder
{
    public ICommandLineBuilder? CommandLineBuilder { get; set; }
    public IServiceCollection Services { get; set; }

    public IHostBuilder ConfigureDefaults(string[]? args);

    public IHost Build();
}
