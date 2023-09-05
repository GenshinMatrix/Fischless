﻿using System;
using Fischless.Hosting;
using Fischless.Hosting.Absraction;
using Fischless.Hosting.Extension;

namespace Fischless;

internal class Program
{
    [STAThread]
    internal static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWpfHostDefaults(wpfBuilder =>
            {
                wpfBuilder.UseStartup<Startup>();
            });
}
