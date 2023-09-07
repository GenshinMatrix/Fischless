using Fischless.Configuration;
using System.Reflection;

namespace Fischless.Models;

[Obfuscation]
public static class Configurations
{
    public static ConfigurationDefinition<string> Language { get; } = new(nameof(Language), string.Empty);
}
