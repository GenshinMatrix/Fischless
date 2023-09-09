using Fischless.Hosting.Absraction;

namespace Fischless.Hosting;

public class WpfHostEnvironment : IWpfHostEnvironment
{
    public ICommandLineBuilder? CommandLineBuilder { get; set; }

    public WpfHostEnvironment(ICommandLineBuilder? commandLineBuilder)
    {
        CommandLineBuilder = commandLineBuilder;
    }

    public bool IsDevelopment() =>
#if DEBUG
        true;
#else
        false;
#endif
}
