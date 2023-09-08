﻿using System;

namespace Fischless.Designs.Controls;

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
