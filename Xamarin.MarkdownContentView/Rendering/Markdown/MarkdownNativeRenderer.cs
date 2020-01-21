using System.Collections.Generic;
using System.Linq;
using Markdig;
using Xamarin.Forms;
using Xamarin.MarkdownContentView.Rendering.Markdown.Internal;

namespace Xamarin.MarkdownContentView.Rendering.Markdown
{
    public sealed class MarkdownNativeRenderer : IMarkdownNativeRenderer
    {
        private readonly IInnerRender _innerRender;

        public MarkdownNativeRenderer(IInnerRender innerRender)
        {
            _innerRender = innerRender;
        }

        public IEnumerable<View> Render(string markdown, MarkdownTheme theme)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return Enumerable.Empty<View>();
            }

            var pipeline = new MarkdownPipelineBuilder().UsePipeTables().Build();
            var blocks = Markdig.Markdown.Parse(markdown, pipeline);

            var result = blocks.AsEnumerable()
                .Select(block => _innerRender.Render(block, theme))
                .Where(view => view != null)
                .AsEnumerable();

            return result;
        }
    }
}