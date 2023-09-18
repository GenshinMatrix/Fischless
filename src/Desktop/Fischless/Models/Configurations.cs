using Fischless.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fischless.Models;

[Obfuscation]
public static class Configurations
{
    public static ConfigurationDefinition<string> Language { get; } = new(nameof(Language), string.Empty);
    public static ConfigurationDefinition<bool> EnsureElevated { get; } = new(nameof(EnsureElevated), false);
    public static ConfigurationDefinition<bool> CloseToTray { get; } = new(nameof(CloseToTray), false);
    public static ConfigurationDefinition<bool> AutoMute { get; } = new(nameof(AutoMute), false);
    public static ConfigurationDefinition<IEnumerable<Contact>> Contacts { get; } = new(nameof(Contacts), Array.Empty<Contact>());
}
