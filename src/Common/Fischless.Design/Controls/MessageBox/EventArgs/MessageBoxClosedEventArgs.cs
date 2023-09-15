using System.Windows;

namespace Fischless.Design.Controls;

public class MessageBoxClosedEventArgs : EventArgs
{
    internal MessageBoxClosedEventArgs(MessageBoxResult result)
    {
        Result = result;
    }

    public MessageBoxResult Result { get; }
}
