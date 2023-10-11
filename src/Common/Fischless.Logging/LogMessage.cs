namespace Fischless.Logging;

public sealed class LoggerMessage
{
    public string Message { get; set; } = string.Empty;

    public LoggerMessage(string message)
    {
        Message = message;
    }

    public override string ToString() => Message;
}
