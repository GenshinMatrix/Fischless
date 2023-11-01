using Fischless.Hosting.Absraction;

namespace Fischless.Hosting;

public class WpfHostBuilder : IWpfHostBuilder
{
    public IHostBuilder HostBuilder { get; set; }

    public WpfHostBuilder(IHostBuilder hostBuilder)
    {
        HostBuilder = hostBuilder;
    }
}
