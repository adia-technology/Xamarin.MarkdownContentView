using System.Linq;
using NUnit.Framework;
using Shouldly;
using Xamarin.Forms;
using Xamarin.MarkdownContentView.Rendering.Markdown;
using Xamarin.MarkdownContentView.Rendering.Markdown.Internal;

namespace Xamarin.MarkdownContentView.Tests.Rendering.Markdown
{
    [TestFixture]
    public class MarkdownNativeRendererTests
    {
        private MarkdownTheme _defaultTheme;
        private MarkdownNativeRenderer _markdownRenderer;

        [SetUp]
        public void Setup()
        {
            _defaultTheme = new MarkdownTheme("iOS");

            var innerRenderer = new InnerRender();
            _markdownRenderer = new MarkdownNativeRenderer(innerRenderer);
        }

        [Test]
        public void Render_EmptyMarkdown_ToEmptyViewCollection()
        {
            // when
            var result = _markdownRenderer.Render(string.Empty, _defaultTheme);

            // then
            result.ShouldBeEmpty();
        }

        [Test]
        public void Render_ParagraphMarkdown_ToViewCollection()
        {
            // given
            const string markdown =
                @"You acknowledge and voluntarily and expressly accept that your use of the Website is made under your sole and exclusive responsibility.";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();

            var view = result.FirstOrDefault() as Label;

            view.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            view.FormattedText.ToString().ShouldBe(markdown);
        }

        [Test]
        public void Render_HeaderMarkdown_ToViewCollection()
        {
            // given
            const string markdown = @"# Privacy Policy";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(1);

            var layout = result.FirstOrDefault() as StackLayout;

            layout.ShouldNotBeNull();

            var header = layout.Children.FirstOrDefault() as Label;

            header.ShouldNotBeNull();
            header.FormattedText.ToString().ShouldBe("Privacy Policy");
        }

        [Test]
        public void Render_ParagraphWithOneLinkMarkdown_ToViewCollection()
        {
            // given
            const string markdown =
                "Privacy policy link: [URL](https://private.policy.con) in paragraph.\nOther paragraph without link.";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(1);

            var view = result.FirstOrDefault() as Label;

            view.ShouldNotBeNull();
            view.FormattedText.ToString()
                .ShouldBe("Privacy policy link: URL in paragraph.\nOther paragraph without link.");
            view.GestureRecognizers.Count.ShouldBe(1);
        }

        [Test]
        public void Render_TwoParagraphsWithLinkMarkdown_ToViewCollection()
        {
            // given
            const string markdown =
                "First link: [FIRST URL](https://first.url.con) in paragraph.\n\nSecond link: [SECOND URL](https://second.url.con) in paragraph.";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(2);

            var firstParagraph = result.FirstOrDefault() as Label;

            firstParagraph.ShouldNotBeNull();
            firstParagraph.FormattedText.ToString().ShouldBe("First link: FIRST URL in paragraph.");
            firstParagraph.GestureRecognizers.Count.ShouldBe(1);

            var secondParagraph = result.LastOrDefault() as Label;

            secondParagraph.ShouldNotBeNull();
            secondParagraph.FormattedText.ToString().ShouldBe("Second link: SECOND URL in paragraph.");
            secondParagraph.GestureRecognizers.Count.ShouldBe(1);
        }

        [Test]
        public void Render_CodeMarkdown_ToViewCollection()
        {
            // given
            const string markdown = @"
            ```
            # code block
            print '3 tabs or'
            print 'indent'
            ```
            ";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(1);

            var frame = result.FirstOrDefault() as Frame;

            frame.ShouldNotBeNull();

            var label = frame.Content as Label;

            label.ShouldNotBeNull();
            label.Text.ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void Render_ListBoldMarkdown_ToViewCollection()
        {
            // given
            const string markdown = "* One\n* Two\n* Three";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();

            var root = result.FirstOrDefault() as StackLayout;

            root.ShouldNotBeNull();
            root.Children.Count.ShouldBe(3);
        }

        [Test]
        public void Render_NumericBoldMarkdown_ToViewCollection()
        {
            // given
            const string markdown = "1) One\n2) Two\n3) Three";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();

            var root = result.FirstOrDefault() as StackLayout;

            root.ShouldNotBeNull();
            root.Children.Count.ShouldBe(3);
        }

        [Test]
        public void Render_ThematicBreakBlockMarkdown_ToViewCollection()
        {
            // given
            const string markdown = "***";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();

            var root = result.FirstOrDefault() as BoxView;

            root.ShouldNotBeNull();
        }

        [Test]
        public void Render_ComplicatedMarkdown_ToViewCollection()
        {
            // given
            const string markdown = "## Heading\n"
                                    + "Some first paragraph with link [LINK](https://link.example).\n"
                                    + "***\n"
                                    + "## Second heading\n"
                                    + "1) **One**\n"
                                    + "2) Two\n"
                                    + "3) Three with a link [URL](https://otherlink.example)"
                                    + "***\n"
                                    + "# CODE"
                                    + "\t print('1')";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();
        }

        [Test]
        public void Render_TableMarkdown_ToViewCollection()
        {
            // given
            const string markdown = "Markdown | Less | Pretty\n"
                                    + "--- | --- | ---\n"
                                    + "One | Two | Three\n"
                                    + "1 | 2 | 3\n"
                                    + "[URL](https://otherlink.example) | **One** | `Italic`"
                                    + " | | ";

            // when
            var result = _markdownRenderer.Render(markdown, _defaultTheme).ToList();

            // then
            result.ShouldNotBeEmpty();

            var root = result.FirstOrDefault() as Grid;

            root.ShouldNotBeNull();
            root.Children.Count.ShouldBe(16);
        }
    }
}
