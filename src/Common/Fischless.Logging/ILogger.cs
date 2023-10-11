namespace Fischless.Logging;

public interface ILogger
{
    public void None(params object[] values);
    public void Trace(params object[] values);
    public void Debug(params object[] values);
    public void Information(params object[] values);
    public void Warning(params object[] values);
    public void Error(params object[] values);
    public void Critical(params object[] values);
    public void Exception(Exception e, string message = null!);
}
