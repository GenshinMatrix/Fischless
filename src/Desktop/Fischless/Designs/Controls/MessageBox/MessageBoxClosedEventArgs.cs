using System;
using System.Windows;

namespace Fischless.Designs.Controls;

public class MessageBoxClosedEventArgs : EventArgs
{
    internal MessageBoxClosedEventArgs(MessageBoxResult result)
    {
        Result = result;
    }

    public MessageBoxResult Result { get; }
}
