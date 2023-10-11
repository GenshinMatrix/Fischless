using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;

namespace Fischless.Logging;

public sealed class LoggerConfiguration
{
    private string _logFolder;
    private string _fileName;
    private LoggerType _type = LoggerType.Async;
    private LogLevel _level = LogLevel.Trace;
    private bool _loggerCreated;

    public static LoggerConfiguration CreateDefault()
    {
        return new LoggerConfiguration();
    }

    public LoggerConfiguration UseType(LoggerType type = LoggerType.Async)
    {
        _type = type;
        return this;
    }

    public LoggerConfiguration UseLevel(LogLevel level = LogLevel.Trace)
    {
        _level = level;
        return this;
    }

    public LoggerConfiguration WriteToFile(string logFolder, string fileName)
    {
        _logFolder = logFolder;
        _fileName = fileName;
        return this;
    }

    public ILogger CreateLogger()
    {
        if (_type == LoggerType.Silent)
        {
            throw new ArgumentException(nameof(LoggerType));
        }
        else if (_type == LoggerType.Async)
        {
            return CreateAsyncLogger();
        }
        else if (_type == LoggerType.Sync)
        {
            return CreateSyncLogger();
        }
        throw new ArgumentOutOfRangeException(nameof(LoggerType));
    }

    public ILogger CreateSyncLogger()
    {
        if (_loggerCreated) throw new Exception("Duplicated logger created.");
        _loggerCreated = true;

        SyncLogger logger = new()
        {
            ApplicationLogPath = _logFolder ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"),
        };

        if (!Directory.Exists(logger.ApplicationLogPath))
        {
            _ = Directory.CreateDirectory(logger.ApplicationLogPath);
        }

        string logFilePath = Path.Combine(logger.ApplicationLogPath, _fileName ?? DateTime.Now.ToString(@"yyyyMMdd", CultureInfo.InvariantCulture) + ".log");
        logger.TraceListener = new TextWriterTraceListener(logFilePath);
        logger.Level = _level;
        return logger;
    }

    public ILogger CreateAsyncLogger()
    {
        if (_loggerCreated) throw new Exception("Duplicated logger created.");
        _loggerCreated = true;

        AsyncLogger logger = new()
        {
            ApplicationLogPath = _logFolder ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"),
        };

        if (!Directory.Exists(logger.ApplicationLogPath))
        {
            _ = Directory.CreateDirectory(logger.ApplicationLogPath);
        }

        string logFilePath = Path.Combine(logger.ApplicationLogPath, _fileName ?? DateTime.Now.ToString(@"yyyyMMdd", CultureInfo.InvariantCulture) + ".log");
        logger.TraceListener = new TextWriterTraceListener(logFilePath);
        logger.Level = _level;
        return logger;
    }
}
