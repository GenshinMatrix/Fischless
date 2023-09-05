namespace Fischless.Hosting.Absraction;

public interface IWpfHostEnvironment
{
    public ICommandLineBuilder? CommandLineBuilder { get; set; }

    public bool IsDevelopment() =>
#if DEBUG
        true;
#else
        false;
#endif
}
