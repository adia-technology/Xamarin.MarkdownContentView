using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Markdig.Syntax;
using Xamarin.Forms;
using Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal
{
    public sealed class InnerRender : IInnerRender
    {
        private readonly Func<string, Task> _navigateToLink;

        public InnerRender(Func<string, Task> navigateToLink)
        {
            _navigateToLink = navigateToLink;
        }

        public InnerRender()
        {
            _navigateToLink = _ => Task.CompletedTask;
        }

        public View Render(IEnumerable<Block> blocks, MarkdownTheme theme)
        {
            var stack = new StackLayout()
            {
                Spacing = theme.Margin
            };

            foreach (var block in blocks)
            {
                var view = Render(block, theme);

                if (view == null)
                {
                    continue;
                }

                stack.Children.Add(view);
            }

            return stack;
        }

        public View Render<T>(T block, MarkdownTheme theme) where T : Block
        {
            switch (block)
            {
                case HeadingBlock heading:
                    return new Heading(_navigateToLink).Render(heading, theme);
                case ParagraphBlock paragraph:
                    return new Paragraph(_navigateToLink).Render(paragraph, theme);
                case ListBlock list:
                    return new List(this, _navigateToLink).Render(list, theme);
                case CodeBlock code:
                    return new Code(_navigateToLink).Render(code, theme);
                case ThematicBreakBlock @break:
                    return new ThematicBreak().Render(@break, theme);
                case Markdig.Extensions.Tables.Table table:
                    return new Table(this, _navigateToLink).Render(table, @theme);
                default:
                    return new Unknown().Render(block, theme);
            }
        }
    }
}