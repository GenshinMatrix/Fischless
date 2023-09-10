namespace Fischless.Hoyolab;

public class HoyolabException : Exception
{
    public int ReturnCode { get; init; }

    public HoyolabException(string? message) : base(message)
    {
    }

    public HoyolabException(int returnCode, string? message) : base($"{message} ({returnCode})")
    {
        ReturnCode = returnCode;
    }
}
