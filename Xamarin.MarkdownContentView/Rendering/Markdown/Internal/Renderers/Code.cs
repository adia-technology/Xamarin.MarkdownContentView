using System;
using System.Threading.Tasks;
using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public sealed class Code :  BaseViewInnerRenderer, IBlockRenderer<CodeBlock>
    {
        public Code(Func<string, Task> navigateToLink) : base(navigateToLink)
        {
        }

        public View Render(CodeBlock block, MarkdownTheme theme)
        {
            var style = theme.Code;

            var label = new Label()
            {
                TextColor = style.ForegroundColor,
                FontAttributes = style.Attributes,
                FontFamily = style.FontFamily,
                FontSize = style.FontSize,
                Text = string.Join(Environment.NewLine, block.Lines),
            };

            var frame = new Frame()
            {
                CornerRadius = 3,
                HasShadow = false,
                Padding = theme.Margin,
                BackgroundColor = style.BackgroundColor,
                Content = label
            };

            return frame;
        }
    }
}
