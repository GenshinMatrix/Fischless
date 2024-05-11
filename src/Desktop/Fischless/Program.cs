using Fischless.Fetch.Responsive;
using Fischless.Hosting;
using Fischless.Hosting.Absraction;
using Fischless.Hosting.Extension;
using System;

namespace Fischless;

internal class Program
{
    [STAThread]
    internal static void Main(string[] args)
    {
        ResponsiveTester.Benchmark();
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWpfHostDefaults(wpfBuilder =>
            {
                wpfBuilder.UseStartup<Startup>();
            });
}
