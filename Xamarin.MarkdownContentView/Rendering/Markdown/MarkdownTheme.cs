using Xamarin.Forms;

namespace Xamarin.MarkdownContentView.Rendering.Markdown
{
    public class MarkdownTheme
    {
        public MarkdownTheme(string runtimePlatform)
        {
            Paragraph = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                FontSize = 14,
            };

            Heading1 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                BorderSize = 1,
                FontSize = 26,
            };

            Heading2 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                BorderSize = 1,
                FontSize = 22,
            };

            Heading3 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 20,
            };

            Heading4 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 22,
            };

            Heading5 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 18,
            };

            Heading6 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 16,
            };

            Link = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                FontSize = 14,
            };

            Code = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                FontSize = 14,
            };

            Separator = new MarkdownStyle
            {
                BorderSize = 2,
            };

            switch (runtimePlatform)
            {
                case Device.iOS:
                    Code.FontFamily = "Courier";
                    break;

                case Device.Android:
                    Code.FontFamily = "monospace";
                    break;
            }
        }

        public Color BackgroundColor { get; set; }

        public MarkdownStyle Paragraph { get; set; } 

        public MarkdownStyle Heading1 { get; set; } 

        public MarkdownStyle Heading2 { get; set; } 

        public MarkdownStyle Heading3 { get; set; } 

        public MarkdownStyle Heading4 { get; set; } 

        public MarkdownStyle Heading5 { get; set; }

        public MarkdownStyle Heading6 { get; set; }

        public MarkdownStyle Separator { get; set; }

        public MarkdownStyle Link { get; set; }

        public MarkdownStyle Code { get; set; }

        public float Margin { get; set; } = 10;

        public double LineHeight { get; set; } = 1.2;

        public class LightMarkdownTheme : MarkdownTheme
        {
            public LightMarkdownTheme(string runtimePlatform) : base(runtimePlatform)
            {
                BackgroundColor = DefaultBackgroundColor;
                Paragraph.ForegroundColor = DefaultTextColor;
                Heading1.ForegroundColor = DefaultTextColor;
                Heading1.BorderColor = DefaultSeparatorColor;
                Heading2.ForegroundColor = DefaultTextColor;
                Heading2.BorderColor = DefaultSeparatorColor;
                Heading3.ForegroundColor = DefaultTextColor;
                Heading4.ForegroundColor = DefaultTextColor;
                Heading5.ForegroundColor = DefaultTextColor;
                Heading6.ForegroundColor = DefaultTextColor;
                Link.ForegroundColor = DefaultAccentColor;
                Code.ForegroundColor = DefaultTextColor;
                Code.BackgroundColor = DefaultCodeBackground;
                Separator.BorderColor = DefaultSeparatorColor;
            }

            public static readonly Color DefaultBackgroundColor = Color.FromHex("#ffffff");

            public static readonly Color DefaultAccentColor = Color.FromHex("#0366d6");

            public static readonly Color DefaultTextColor = Color.FromHex("#24292e");

            public static readonly Color DefaultCodeBackground = Color.FromHex("#f6f8fa");

            public static readonly Color DefaultSeparatorColor = Color.FromHex("#eaecef");
        }
    }
}