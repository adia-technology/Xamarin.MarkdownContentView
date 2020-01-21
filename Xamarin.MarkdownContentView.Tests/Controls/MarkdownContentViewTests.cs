using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Xamarin.Forms;
using Xamarin.MarkdownContentView.Rendering.Markdown;

namespace Xamarin.MarkdownContentView.Tests.Controls
{
    [TestFixture]
    public class MarkdownContentViewTests
    {
        private IMarkdownNativeRenderer _nativeMarkdownNativeRenderer;

        [SetUp]
        public void Setup()
        {
            _nativeMarkdownNativeRenderer = Substitute.For<IMarkdownNativeRenderer>();
        }

        [Test]
        public void MarkdownSet_EmptyString_ShouldNotCallRender()
        {
            // given
            var control = new MarkdownContentView(_nativeMarkdownNativeRenderer);

            // when
            control.Markdown = string.Empty;

            // then
            _nativeMarkdownNativeRenderer.Received(0).Render(string.Empty, Arg.Any<MarkdownTheme>());
        }

        [Test]
        public void MarkdownSet_ValidString_ShouldNotCallRender()
        {
            // given
            var control = new MarkdownContentView(_nativeMarkdownNativeRenderer);

            // when
            control.Markdown = "Some text";

            // then
            _nativeMarkdownNativeRenderer.Received(1).Render("Some text", Arg.Any<MarkdownTheme>());
        }

        [Test]
        public void ThemeSet_EmptyMarkDown_ShouldNotCallRender()
        {
            // given
            var theme = new MarkdownTheme("Android");
            var control = new MarkdownContentView(_nativeMarkdownNativeRenderer);

            // when
            control.Theme = theme;

            // then
            _nativeMarkdownNativeRenderer.Received(0).Render(string.Empty, theme);
        }

        [Test]
        public void MarkdownSet_ValidMarkdown_ShouldNotCallRender()
        {
            // given
            const string markdown = "This is a paragraph";
            var theme = new MarkdownTheme("Android");
            var view = new BoxView();

            _nativeMarkdownNativeRenderer.Render(markdown, Arg.Any<MarkdownTheme>()).Returns(new[] {view});

            // when
            var control = new MarkdownContentView(_nativeMarkdownNativeRenderer)
            {
                Theme = theme,
                Markdown = markdown
            };

            // then
            _nativeMarkdownNativeRenderer.Received(1).Render(markdown, theme);
            var content = control.Content as StackLayout;

            content.ShouldNotBeNull();
            content.Children.ShouldNotBeEmpty();
            _ = content.Children.Contains(view);
        }
    }
}