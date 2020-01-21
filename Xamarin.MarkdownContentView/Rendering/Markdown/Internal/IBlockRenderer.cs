using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal
{
    public interface IBlockRenderer<T> where T : Block
    {
        View Render(T block, MarkdownTheme theme);
    }
}