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
                Margin = new Thickness(theme.Margin, 0, 0, 0),
            };

            View bullet;

            if (list.IsOrdered)
            {
                bullet = new Label
                {
                    Text = $"{index}.",
                    FontSize = theme.Paragraph.FontSize,
                    TextColor = theme.Paragraph.ForegroundColor,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    LineBreakMode = LineBreakMode.NoWrap,
                    Margin = new Thickness(0, 3, 0, 0)
                };
            }
            else
            {
                bullet = new BoxView
                {
                    WidthRequest = 4,
                    HeightRequest = 4,
                    Margin = new Thickness(0, 10, 0, 0),
                    BackgroundColor = theme.Paragraph.ForegroundColor,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Center
                };
            }

            horizontalStack.Children.Add(bullet);
            horizontalStack.Children.Add(_render.Render(itemBlock.AsEnumerable(), theme));

            stack.Children.Add(horizontalStack);

            return stack;
        }
    }
}