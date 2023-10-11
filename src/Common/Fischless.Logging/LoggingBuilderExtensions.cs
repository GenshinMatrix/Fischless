using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fischless.Logging;

/// <summary>
/// Extends <see cref="ILoggingBuilder"/> with configuration methods.
/// </summary>
public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddLogger(this ILoggingBuilder builder, ILogger? logger = null)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        if (logger != null)
        {
            builder.Services.AddSingleton(logger);
        }
        return builder;
    }
}
