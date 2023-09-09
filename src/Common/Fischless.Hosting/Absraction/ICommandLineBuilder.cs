using System.Collections.Specialized;

namespace Fischless.Hosting.Absraction;

public interface ICommandLineBuilder
{
    public StringDictionary Parameters { get; set; }
}
