﻿using System;

namespace Fischless.Design.Controls;

public sealed class MessageBoxClosingDeferral
{
    private readonly Action _handler;

    internal MessageBoxClosingDeferral(Action handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public void Complete()
    {
        _handler();
    }
}
