using System;
using System.Threading.Tasks;
using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public class Paragraph : BaseViewInnerRenderer, IBlockRenderer<ParagraphBlock>
    {
        public Paragraph(Func<string, Task> navigateToLink) : base(navigateToLink)
        {
        }

        public View Render(ParagraphBlock block, MarkdownTheme theme)
        {
            var style = theme.Paragraph;

            var formatParameters = new FormatParameters()
            {
                ContainerInline = block.Inline,
                Family = style.FontFamily,
                Attributes = style.Attributes,
                ForegroundColor = style.ForegroundColor,
                BackgroundColor = style.BackgroundColor,
                Size = style.FontSize,
                Theme = theme 
            };

            var label = new Label
            {
                FormattedText = CreateFormatted(formatParameters)
            };

            AttachLinks(label);

            return label;
        }
    }
}