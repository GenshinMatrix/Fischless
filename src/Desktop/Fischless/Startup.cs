using Fischless.Extensions;
using Fischless.Hosting.Absraction;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Fischless;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        IHost app = new App()
            .UseDispatcherUnhandledExceptionCatched();

        services.AddSingleton(app)
                .AddSingleton(Log.Logger)
                .AddLogging(c => c.AddSerilog(Log.Logger))
                .AddPlugins(app);
    }

    public void Configure(IApplicationBuilder app, IWpfHostEnvironment env)
    {
        app.UseLogger()
           .UseSingleInstance("FischlessFischless")
           .UseMapper()
           .UseAppCenter()
           .UseDpiAware()
           .UseDomainUnhandledExceptionCatched()
           .UseUnobservedTaskExceptionCatched()
           .Forget();

        Log.Information($"Welcome to Starward v{AppConfig.AppVersion}|System: {Environment.OSVersion}|Command Line: {Environment.CommandLine}");

        if (env.IsDevelopment())
        {
        }
    }
}
