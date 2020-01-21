using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig.Syntax.Inlines;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal
{
    public class BaseViewInnerRenderer
    {
        private Func<string, Task> NavigateToLink { get; }

        private List<KeyValuePair<string, string>> _links;

        protected BaseViewInnerRenderer(Func<string, Task> navigateToLink)
        {
            NavigateToLink = navigateToLink;

            _links = new List<KeyValuePair<string, string>>();
        }

        protected FormattedString CreateFormatted(FormatParameters parameters)
        {
            var formattedString = new FormattedString();

            foreach (var inline in parameters.ContainerInline)
            {
                var spans = CreateSpans(inline, 
                    parameters.Family, 
                    parameters.Attributes, 
                    parameters.ForegroundColor,
                    parameters.BackgroundColor, 
                    parameters.Size, 
                    parameters.Theme);

                if (spans == null)
                {
                    continue;
                }

                foreach (var span in spans)
                {
                    formattedString.Spans.Add(span);
                }
            }

            return formattedString;
        }

        protected void AttachLinks(View view)
        {
            if (!_links.Any())
            {
                return;
            }

            var blockLinks = _links;
            
            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => { await NavigateToLink(blockLinks.First().Value); })
            });

            _links = new List<KeyValuePair<string, string>>();
        }

        private Span[] CreateSpans(
            Inline inline, 
            string family, 
            FontAttributes attributes, 
            Color foregroundColor,
            Color backgroundColor, 
            float size,
            MarkdownTheme theme)
        {
            switch (inline)
            {
                case LiteralInline literal:
                    return new[]
                    {
                        new Span
                        {
                            Text = literal.Content.Text.Substring(literal.Content.Start, literal.Content.Length),
                            FontAttributes = attributes,
                            ForegroundColor = foregroundColor,
                            BackgroundColor = backgroundColor,
                            FontSize = size,
                            FontFamily = family,
                            LineHeight = theme.LineHeight
                        }
                    };

                case EmphasisInline emphasis:
                    var childAttributes = attributes | (emphasis.DelimiterCount == 2 ? FontAttributes.Bold : FontAttributes.Italic);

                    return emphasis.SelectMany(x =>
                        CreateSpans(x, family, childAttributes, foregroundColor, backgroundColor, size, theme)).ToArray();

                case LineBreakInline _: 
                    return new[] {new Span {Text = "\n"}};

                case LinkInline link:
                    var url = link.Url;
                    var spans = link.SelectMany(x => CreateSpans(
                        x,
                        theme.Link.FontFamily ?? family,
                        theme.Link.Attributes,
                        theme.Link.ForegroundColor, 
                        theme.Link.BackgroundColor,
                        size,
                        theme)).ToArray();

                    _links.Add(new KeyValuePair<string, string>(string.Join(string.Empty, spans.Select(x => x.Text)), url));

                    return spans;
                
                default:
                    return Enumerable.Empty<Span>().ToArray();
            }
        }

        protected class FormatParameters
        {
            public ContainerInline ContainerInline { get; set; }

            public string Family { get; set; }

            public FontAttributes Attributes { get; set; }
            
            public Color ForegroundColor { get; set; }

            public Color BackgroundColor { get; set; }

            public float Size { get; set; }
            
            public MarkdownTheme Theme { get; set; }
        }
    }
}