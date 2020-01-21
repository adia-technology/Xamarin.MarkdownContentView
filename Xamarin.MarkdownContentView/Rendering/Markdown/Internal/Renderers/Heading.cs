using System;
using System.Threading.Tasks;
using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public class Heading : BaseViewInnerRenderer, IBlockRenderer<HeadingBlock>
    {
        public Heading(Func<string, Task> navigateToLink) : base(navigateToLink)
        {
        }

        public View Render(HeadingBlock block, MarkdownTheme theme)
        {
            var style = GetHeadingStyle(block, theme);

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

            if (style.BorderSize <= 0)
            {
                return label;
            }

            AttachLinks(label);

            var headingStack = new StackLayout();

            headingStack.Children.Add(label);
            headingStack.Children.Add(new BoxView
            {
                HeightRequest = style.BorderSize,
                BackgroundColor = style.BorderColor,
            });

            return headingStack;
        }

        private static MarkdownStyle GetHeadingStyle(HeadingBlock block, MarkdownTheme theme)
        {
            MarkdownStyle style;

            switch (block.Level)
            {
                case 1:
                    style = theme.Heading1;
                    break;
                case 2:
                    style = theme.Heading2;
                    break;
                case 3:
                    style = theme.Heading3;
                    break;
                case 4:
                    style = theme.Heading4;
                    break;
                case 5:
                    style = theme.Heading5;
                    break;
                default:
                    style = theme.Heading6;
                    break;
            }

            return style;
        }
    }
}