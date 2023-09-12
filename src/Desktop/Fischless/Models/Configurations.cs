using Fischless.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fischless.Models;

[Obfuscation]
public static class Configurations
{
    public static ConfigurationDefinition<string> Language { get; } = new(nameof(Language), string.Empty);
    public static ConfigurationDefinition<IEnumerable<Contact>> Contacts { get; } = new(nameof(Contacts), Array.Empty<Contact>());
}
