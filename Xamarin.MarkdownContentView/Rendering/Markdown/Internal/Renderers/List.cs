using System;
using System.Linq;
using System.Threading.Tasks;
using Markdig.Syntax;
using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public class List : BaseViewInnerRenderer, IBlockRenderer<ListBlock>
    {
        private readonly IInnerRender _render;

        public List(IInnerRender render, Func<string, Task> navigateToLink) : base(navigateToLink)
        {
            _render = render;
        }

        public View Render(ListBlock block, MarkdownTheme theme)
        {
            var stack = new StackLayout()
            {
                Spacing = theme.Margin
            };

            for (var i = 0; i < block.Count(); i++)
            {
                var item = block.ElementAt(i);

                if (item is ListItemBlock itemBlock)
                {
                    stack.Children.Add(Render(block, i + 1, itemBlock, theme));
                }
            }

            return stack;
        }

        private View Render(ListBlock list, int index, ListItemBlock itemBlock, MarkdownTheme theme)
        {
            var stack = new StackLayout()
            {
                Spacing = theme.Margin
            };

            var horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(theme.Margin, 0, 0, 0)
            };

            var bullet = new Label
            {
                Text = list.IsOrdered ? $"{index}." : "\u2981",
                FontSize = theme.Paragraph.FontSize,
                TextColor = theme.Paragraph.ForegroundColor,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.NoWrap,
                Margin = theme.ListItemThickness
            };

            horizontalStack.Children.Add(bullet);
            horizontalStack.Children.Add(_render.Render(itemBlock.AsEnumerable(), theme));

            stack.Children.Add(horizontalStack);

            return stack;
        }
    }
}