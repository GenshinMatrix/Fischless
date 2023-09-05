﻿using ColorCode.Common;
using ColorCode.Parsing;
using ColorCode.Styling;
using ColorCode.Wpf.Common;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ColorCode.Wpf
{
    /// <summary>
    /// Creates a <see cref="RichTextBoxFormatter"/>, for rendering Syntax Highlighted code to a RichTextBlock.
    /// </summary>
    public class RichTextBoxFormatter : CodeColorizerBase
    {
        /// <summary>
        /// Creates a <see cref="RichTextBoxFormatter"/>, for rendering Syntax Highlighted code to a RichTextBlock.
        /// </summary>
        /// <param name="style">The Custom styles to Apply to the formatted Code.</param>
        /// <param name="languageParser">The language parser that the <see cref="RichTextBoxFormatter"/> instance will use for its lifetime.</param>
        public RichTextBoxFormatter(StyleDictionary? style = null, ILanguageParser? languageParser = null) : base(style, languageParser)
        {
        }

        /// <summary>
        /// Adds Syntax Highlighted Source Code to the provided RichTextBlock.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="richText">The Control to add the Text to.</param>
        public void FormatRichTextBox(string sourceCode, ILanguage language, RichTextBox richText)
        {
            var paragraph = new Paragraph();
            richText.Document.Blocks.Add(paragraph);
            FormatInlines(sourceCode, language, paragraph.Inlines);
        }

        /// <summary>
        /// Adds Syntax Highlighted Source Code to the provided InlineCollection.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="inlineCollection">InlineCollection to add the Text to.</param>
        public void FormatInlines(string sourceCode, ILanguage language, InlineCollection inlineCollection)
        {
            InlineCollection = inlineCollection;
            languageParser.Parse(sourceCode, language, (parsedSourceCode, captures) => Write(parsedSourceCode, captures));
        }

        private InlineCollection? InlineCollection { get; set; }

        protected override void Write(string parsedSourceCode, IList<Scope> scopes)
        {
            var styleInsertions = new List<TextInsertion>();

            foreach (Scope scope in scopes)
                GetStyleInsertionsForCapturedStyle(scope, styleInsertions);

            styleInsertions.SortStable((x, y) => x.Index.CompareTo(y.Index));

            int offset = 0;

            Scope? previousScope = null;

            foreach (var styleinsertion in styleInsertions)
            {
                var text = parsedSourceCode.Substring(offset, styleinsertion.Index - offset);
                CreateSpan(text, previousScope);
                if (!string.IsNullOrWhiteSpace(styleinsertion.Text))
                {
                    CreateSpan(text, previousScope);
                }
                offset = styleinsertion.Index;

                previousScope = styleinsertion.Scope;
            }

            var remaining = parsedSourceCode.Substring(offset);
            // Ensures that those loose carriages don't run away!
            if (remaining != "\r")
            {
                CreateSpan(remaining, null);
            }
        }

        private void CreateSpan(string text, Scope? scope)
        {
            var span = new Span();
            var run = new Run
            {
                Text = text
            };

            // Styles and writes the text to the span.
            if (scope != null) StyleRun(run, scope);
            span.Inlines.Add(run);

            InlineCollection?.Add(span);
        }

        private void StyleRun(Run run, Scope scope)
        {
            string? foreground = null;
            string? background = null;
            bool italic = false;
            bool bold = false;

            if (Styles.Contains(scope.Name))
            {
                Styling.Style style = Styles[scope.Name];

                foreground = style.Foreground;
                background = style.Background;
                italic = style.Italic;
                bold = style.Bold;
            }

            if (!string.IsNullOrWhiteSpace(foreground))
                run.Foreground = foreground.GetSolidColorBrush();

            //Background isn't supported, but a workaround could be created.

            if (italic)
                run.FontStyle = FontStyles.Italic;

            if (bold)
                run.FontWeight = FontWeights.Bold;
        }

        private void GetStyleInsertionsForCapturedStyle(Scope scope, ICollection<TextInsertion> styleInsertions)
        {
            styleInsertions.Add(new TextInsertion
            {
                Index = scope.Index,
                Scope = scope
            });

            foreach (Scope childScope in scope.Children)
                GetStyleInsertionsForCapturedStyle(childScope, styleInsertions);

            styleInsertions.Add(new TextInsertion
            {
                Index = scope.Index + scope.Length
            });
        }
    }
}