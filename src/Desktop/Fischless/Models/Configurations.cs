﻿using Fischless.Configuration;
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
    public static ConfigurationDefinition<bool> IsUseFps { get; } = new(nameof(IsUseFps), false);
    public static ConfigurationDefinition<uint> Fps { get; } = new(nameof(Fps), 60);
    public static ConfigurationDefinition<bool> IsUseGamePath { get; } = new(nameof(IsUseGamePath), false);
    public static ConfigurationDefinition<string> GamePath { get; } = new(nameof(GamePath), string.Empty);
    public static ConfigurationDefinition<bool> IsUseResolution { get; } = new(nameof(IsUseResolution), false);
    public static ConfigurationDefinition<uint> ResolutionWidth { get; } = new(nameof(ResolutionWidth), 1920);
    public static ConfigurationDefinition<uint> ResolutionHeight { get; } = new(nameof(ResolutionHeight), 1080);
    public static ConfigurationDefinition<bool> IsUseFullScreen { get; } = new(nameof(IsUseFullScreen), false);
    public static ConfigurationDefinition<bool> IsUseBorderless { get; } = new(nameof(IsUseBorderless), false);
    public static ConfigurationDefinition<bool> IsUseLazy { get; } = new(nameof(IsUseLazy), false);
    public static ConfigurationDefinition<bool> IsUseReShade { get; } = new(nameof(IsUseReShade), false);
    public static ConfigurationDefinition<bool> IsUseReShadeSlient { get; } = new(nameof(IsUseReShadeSlient), false);
    public static ConfigurationDefinition<bool> IsUseGameRunningHint { get; } = new(nameof(IsUseGameRunningHint), false);
    public static ConfigurationDefinition<int> ThemeTextFontFamily { get; } = new(nameof(ThemeTextFontFamily), 0);
    public static ConfigurationDefinition<bool> IsUseThemeCursor { get; } = new(nameof(IsUseThemeCursor), false);
    public static ConfigurationDefinition<bool> IsUseSmallerSize { get; } = new(nameof(IsUseSmallerSize), false);
    public static ConfigurationDefinition<string> ReShadePath { get; } = new(nameof(ReShadePath), string.Empty);
}
