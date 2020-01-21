using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.MarkdownContentView.Rendering.Markdown;
using Xamarin.MarkdownContentView.Rendering.Markdown.Internal;

namespace Xamarin.MarkdownContentView
{
    public sealed class MarkdownContentView : ContentView
    {
        private static readonly MarkdownTheme DefaultTheme = new MarkdownTheme.LightMarkdownTheme(Device.RuntimePlatform);

        private readonly IMarkdownNativeRenderer _markdownRenderer;

        public MarkdownContentView()
        {
            var innerRender = new InnerRender(OnNavigateToLink);
            _markdownRenderer = new MarkdownNativeRenderer(innerRender);
        }

        public MarkdownContentView(IMarkdownNativeRenderer markdownRenderer)
        {
            _markdownRenderer = markdownRenderer;
        }

        public string Markdown
        {
            get => (string)GetValue(MarkdownProperty);
            set => SetValue(MarkdownProperty, value);
        }

        public static readonly BindableProperty MarkdownProperty = BindableProperty.Create(nameof(Markdown),
            typeof(string), typeof(MarkdownContentView), null, propertyChanged: OnMarkdownChanged);

        public MarkdownTheme Theme
        {
            get => (MarkdownTheme)GetValue(ThemeProperty);
            set => SetValue(ThemeProperty, value);
        }

        public static readonly BindableProperty ThemeProperty = BindableProperty.Create(nameof(Theme), typeof(MarkdownTheme), typeof(MarkdownContentView), DefaultTheme, propertyChanged: OnMarkdownChanged);

        public event EventHandler<string> NavigateToLink;

        private Task OnNavigateToLink(string link)
        {
            NavigateToLink?.Invoke(this, link);
            return Task.CompletedTask;
        }

        private static void OnMarkdownChanged(BindableObject @object, object oldValue, object newValue)
        {
            var view = @object as MarkdownContentView;
            view?.RenderMarkdown();
        }

        private void RenderMarkdown()
        {
            var stackLayout = new StackLayout();

            if (!string.IsNullOrEmpty(Markdown))
            {
                foreach (var view in _markdownRenderer.Render(Markdown, Theme))
                {
                    stackLayout.Children.Add(view);
                }
            }

            Content = stackLayout;
        }
    }
}
