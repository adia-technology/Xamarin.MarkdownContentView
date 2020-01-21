using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public sealed class ThematicBreak : IBlockRenderer<ThematicBreakBlock>
    {
        public View Render(ThematicBreakBlock block, MarkdownTheme theme)
        {
            var style = theme.Separator;

            return new BoxView
            {
                HeightRequest = style.BorderSize,
                BackgroundColor = style.BorderColor,
            };
        }
    }
}
