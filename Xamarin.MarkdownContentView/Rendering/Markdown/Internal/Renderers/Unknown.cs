using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public sealed class Unknown : IBlockRenderer<Block>
    {
        public View Render(Block block, MarkdownTheme theme)
        {
            return new BoxView();
        }
    }
}
