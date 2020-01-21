using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown
{
    public interface IMarkdownNativeRenderer
    {
        IEnumerable<View> Render(string markdown, MarkdownTheme theme);
    }
}