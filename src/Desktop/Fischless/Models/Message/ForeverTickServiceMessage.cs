using System;

namespace Fischless.Models;

public sealed class ForeverTickServiceMessage
{
    public ForeverTickMethod Method { get; set; } = default;
    public object? Params { get; set; } = null;
}

public enum ForeverTickMethod
{
    None,
    CheckLaunch,
}
