using System.IO;

namespace Fischless.GamepadCapture.RawInput;

public interface IBinary
{
    void ToBinary(BinaryWriter bw);
}

public class IBinaryException : IOException
{
    public IBinaryException(string message) : base(message)
    {
    }
}
