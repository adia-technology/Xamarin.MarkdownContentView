using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using DemoApp.Models;
using Xamarin.Forms;
using Xamarin.MarkdownContentView.Rendering.Markdown;

namespace DemoApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Document _vm;

        public MainPage()
        {
            InitializeComponent();

            _vm = new Document() {Theme = new MarkdownTheme.LightMarkdownTheme(Device.RuntimePlatform)};

            BindingContext = _vm;

            _vm.Markdown = ReadResource("Example.md");
        }

        private string ReadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = name;

            if (!name.StartsWith(nameof(DemoApp)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(name));
            }

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
