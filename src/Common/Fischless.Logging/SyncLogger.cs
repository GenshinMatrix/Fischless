using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Fischless.Logging;

public sealed class SyncLogger : ILogger, IDisposable
{
    public static bool IsEnabled { get; private set; } = false;
    public LogLevel Level { get; set; } = LogLevel.Trace;

    internal readonly object WriterLock = new();

    internal string ApplicationLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
    internal TextWriterTraceListener TraceListener = null!;

    public SyncLogger()
    {
        IsEnabled = true;
    }

    public void Dispose()
    {
        IsEnabled = false;
    }

    public void CloseAndFlush()
    {
        IsEnabled = false;
        Flash();
    }

    public void Flash()
    {
        lock (WriterLock)
        {
            TraceListener.Flush();
        }
    }

    private static string GetCallerInfo()
    {
        StackTrace stackTrace = new();
        MethodBase methodBase = stackTrace.GetFrame(3)?.GetMethod()!;
        if (methodBase.GetCustomAttribute<LoggerMethodAttribute>() != null)
        {
            methodBase = stackTrace.GetFrame(4)?.GetMethod()!;
        }
        string? className = methodBase?.DeclaringType?.Name;
        return className + "|" + methodBase?.Name;
    }

    private void Log(string type, string message)
    {
        StringBuilder sb = new();

        sb.Append(type)
          .Append("|")
          .Append(DateTime.Now.ToString(@"yyyy-MM-dd|HH:mm:ss.fff", CultureInfo.InvariantCulture))
          .Append("|")
          .Append(GetCallerInfo())
          .Append("|")
          .Append(message);

        System.Diagnostics.Debug.WriteLine(sb.ToString());
        if (IsEnabled)
        {
            lock (WriterLock)
            {
                TraceListener.WriteLine(sb.ToString());
                TraceListener.Flush();
            }
        }
    }

    public void None(params object[] values)
    {
    }

    public void Trace(params object[] values)
    {
        if (Level <= LogLevel.Trace)
        {
            Log("TRACE", string.Join(" ", values));
        }
    }

    public void Debug(params object[] values)
    {
        if (Level <= LogLevel.Debug)
        {
            Log("DEBUG", string.Join(" ", values));
        }
    }

    public void Information(params object[] values)
    {
        if (Level <= LogLevel.Information)
        {
            Log("INFO", string.Join(" ", values));
        }
    }

    public void Warning(params object[] values)
    {
        if (Level <= LogLevel.Warning)
        {
            Log("ERROR", string.Join(" ", values));
        }
    }

    public void Error(params object[] values)
    {
        if (Level <= LogLevel.Error)
        {
            Log("ERROR", string.Join(" ", values));
        }
    }

    public void Critical(params object[] values)
    {
        if (Level <= LogLevel.Critical)
        {
            Log("FATAL", string.Join(" ", values));
        }
    }

    public void Exception(Exception e, string? message = null)
    {
        Log(
            (message ?? string.Empty) + Environment.NewLine +
            e?.Message + Environment.NewLine +
            "Inner exception: " + Environment.NewLine +
            e?.InnerException?.Message + Environment.NewLine +
            "Stack trace: " + Environment.NewLine +
            e?.StackTrace,
            "ERROR");
        Debugger.Break();
    }
}
