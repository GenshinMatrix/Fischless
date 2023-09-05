using Fischless.Hosting.Absraction;

namespace Fischless.Hosting;

public class Host
{
    public static IHostBuilder CreateDefaultBuilder(string[]? args)
    {
        HostBuilder builder = new();
        return builder.ConfigureDefaults(args);
    }
}
