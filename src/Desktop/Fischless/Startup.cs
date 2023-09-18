using Fischless.Extensions;
using Fischless.Hosting.Absraction;
using Fischless.Services;
using Fischless.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Fischless;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        IHost app = new App()
            .UseDispatcherUnhandledExceptionCatched()
            .UseMuiLanguage();

        services.AddSingleton(app)
                .AddSingleton(Log.Logger)
                .AddLogging(c => c.AddSerilog(Log.Logger))
                .AddPlugins(app)
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<PageHome>()
                .AddTransient<PageSettings>()
                .AddTransient<IAutoStartService, AutoStartRegistyService>();
    }

    public void Configure(IApplicationBuilder app, IWpfHostEnvironment env)
    {
        app.UseLogger()
           .UseConfiguration()
           .UseSingleInstance("Fischless")
           .UseMapper()
           .UseAppCenter()
           .UseDpiAware()
           .UseDomainUnhandledExceptionCatched()
           .UseUnobservedTaskExceptionCatched()
           .Forget();

        Log.Information($"Welcome to Fischless v{AppConfig.AppVersion}|System: {Environment.OSVersion}|Command Line: {Environment.CommandLine}");

        if (env.IsDevelopment())
        {
        }
    }
}
