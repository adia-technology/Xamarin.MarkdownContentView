using System.Collections.Generic;
using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal
{
    public interface IInnerRender
    {
        View Render(IEnumerable<Block> blocks, MarkdownTheme theme);

        View Render<T>(T block, MarkdownTheme theme) where T : Block;
    }
}