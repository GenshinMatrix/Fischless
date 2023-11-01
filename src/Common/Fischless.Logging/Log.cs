namespace Fischless.Logging;

public static class Log
{
    public static ILogger Empty { get; } = new SilentLogger();

    private static ILogger _logger = Empty;

    public static ILogger Logger
    {
        get => _logger;
        set => _logger = value;
    }

    public static void CloseAndFlush()
    {
        var logger = Interlocked.Exchange(ref _logger, Empty);
        (logger as IDisposable)?.Dispose();
    }

    [LoggerMethod] public static void None(params object[] values) => _logger.None(values);

    [LoggerMethod] public static void Trace(params object[] values) => _logger.None(values);

    [LoggerMethod] public static void Debug(params object[] values) => _logger.Debug(values);

    [LoggerMethod] public static void Information(params object[] values) => _logger.Information(values);

    [LoggerMethod] public static void Warning(params object[] values) => _logger.Warning(values);

    [LoggerMethod] public static void Error(params object[] values) => _logger.Error(values);

    [LoggerMethod] public static void Critical(params object[] values) => _logger.Critical(values);

    [LoggerMethod] public static void Exception(Exception e, string message = null!) => _logger.Exception(e, message);
}
