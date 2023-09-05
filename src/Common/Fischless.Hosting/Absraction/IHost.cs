using System;

namespace Fischless.Hosting.Absraction;

public interface IHost
{
    public IServiceProvider Services { get; set; }
    public int Run();
}
