using ColorCode;
using ColorCode.Styling;
using ColorCode.Wpf;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Wpf;
using Markdig.Renderers.Xaml.Blocks;
using Markdig.Syntax;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace Markdig.Wpf.ColorCode;

public class ColorCodeBlockRenderer : WpfObjectRenderer<CodeBlock>
{
    private readonly CodeBlockRenderer _underlyingCodeBlockRenderer;
    private readonly StyleDictionary _styleDictionary;

    /// <summary>
    ///     Create a new <see cref="ColorCodeBlockRenderer"/> with the specified <paramref name="underlyingCodeBlockRenderer"/> and <paramref name="styleDictionary"/>.
    /// </summary>
    /// <param name="underlyingCodeBlockRenderer">The underlying CodeBlockRenderer to handle unsupported languages.</param>
    /// <param name="styleDictionary">A StyleDictionary for custom styling.</param>
    public ColorCodeBlockRenderer(CodeBlockRenderer underlyingCodeBlockRenderer = null!, StyleDictionary styleDictionary = null!)
    {
        _underlyingCodeBlockRenderer = underlyingCodeBlockRenderer ?? new();
        _styleDictionary = styleDictionary;
    }

    /// <summary>
    ///     Writes the specified <paramref name="codeBlock"/> to the <paramref name="renderer"/>.
    /// </summary>
    /// <param name="renderer">The renderer.</param>
    /// <param name="codeBlock">The code block to render.</param>
    protected override void Write(WpfRenderer renderer, CodeBlock codeBlock)
    {
        if (codeBlock is not FencedCodeBlock fencedCodeBlock ||
            codeBlock.Parser is not FencedCodeBlockParser fencedCodeBlockParser)
        {
            _underlyingCodeBlockRenderer.Write(renderer, codeBlock);

            return;
        }

        var language = ExtractLanguage(fencedCodeBlock, fencedCodeBlockParser);

        if (language is null)
        {
            _underlyingCodeBlockRenderer.Write(renderer, codeBlock);

            return;
        }

        var code = ExtractCode(codeBlock);
        var formatter = new RichTextBoxFormatter(_styleDictionary);
        var paragraph = new Paragraph();
        paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);
        formatter.FormatInlines(code, language, paragraph.Inlines);

        //renderer.WriteBlock(paragraph);
        renderer.WriteBlock(paragraph.ToRounded(4));
    }

    private static ILanguage? ExtractLanguage(IFencedBlock fencedCodeBlock, FencedCodeBlockParser parser)
    {
        var languageId = fencedCodeBlock.Info!.Replace(parser.InfoPrefix!, string.Empty);

        return string.IsNullOrWhiteSpace(languageId) ? null : Languages.FindById(languageId);
    }

    private static string ExtractCode(LeafBlock leafBlock)
    {
        var code = new StringBuilder();
        var lines = leafBlock.Lines.Lines;
        var totalLines = lines.Length;

        for (var index = 0; index < totalLines; index++)
        {
            var line = lines[index];
            var slice = line.Slice;

            if (slice.Text == null)
            {
                continue;
            }

            var lineText = slice.Text.Substring(slice.Start, slice.Length);

            if (index > 0)
            {
                code.AppendLine();
            }

            code.Append(lineText);
        }

        return code.ToString();
    }
}