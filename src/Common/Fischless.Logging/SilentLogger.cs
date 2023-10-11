using System.Diagnostics.CodeAnalysis;

namespace Fischless.Logging;

[SuppressMessage("Style", "IDE0060:")]
[SuppressMessage("Style", "IDE0079:")]
public sealed class SilentLogger : ILogger
{
    public void None(params object[] values)
    {
    }

    public void Trace(params object[] values)
    {
    }

    public void Debug(params object[] values)
    {
    }

    public void Information(params object[] values)
    {
    }

    public void Warning(params object[] values)
    {
    }

    public void Error(params object[] values)
    {
    }

    public void Critical(params object[] values)
    {
    }

    public void Exception(Exception e, string message = null)
    {
    }
}
