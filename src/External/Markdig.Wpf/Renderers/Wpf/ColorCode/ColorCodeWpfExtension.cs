// Copyright (c) ema All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using ColorCode.Styling;
using Markdig.Renderers;
using Markdig.Renderers.Wpf;

namespace Markdig.Wpf.ColorCode;

public class ColorCodeWpfExtension : IMarkdownExtension
{
    private readonly StyleDictionary _styleDictionary;

    /// <summary>
    ///     Creates a new <see cref="ColorCodeWpfExtension"/> with the specified <paramref name="styleDictionary"/>.
    /// </summary>
    /// <param name="styleDictionary">A dictionary indicating how to style the code.</param>
    public ColorCodeWpfExtension(StyleDictionary styleDictionary) => _styleDictionary = styleDictionary;

    /// <summary>
    ///     Sets up this extension for the specified pipeline.
    /// </summary>
    /// <param name="pipeline">The pipeline.</param>
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
    }

    /// <summary>
    ///     Sets up this extension for the specified renderer.
    /// </summary>
    /// <param name="pipeline">The pipeline used to parse the document.</param>
    /// <param name="renderer">The renderer.</param>
    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not WpfRenderer wpfRenderer)
        {
            return;
        }

        var codeBlockRenderer = wpfRenderer.ObjectRenderers.FindExact<CodeBlockRenderer>();

        if (codeBlockRenderer != null)
        {
            wpfRenderer.ObjectRenderers.Remove(codeBlockRenderer);
        }
        else
        {
            codeBlockRenderer = new CodeBlockRenderer();
        }

        wpfRenderer.ObjectRenderers.AddIfNotAlready(
            new ColorCodeBlockRenderer(
                codeBlockRenderer,
                _styleDictionary
            )
        );
    }
}
