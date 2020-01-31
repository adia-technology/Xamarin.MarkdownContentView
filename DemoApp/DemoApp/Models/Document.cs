using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemoApp.Annotations;
using Xamarin.MarkdownContentView.Rendering.Markdown;

namespace DemoApp.Models
{
    public class Document : INotifyPropertyChanged
    {
        private string _markdown;
        private MarkdownTheme _theme;

        public string Markdown
        {
            get => _markdown;
            set
            {
                _markdown = value;
                OnPropertyChanged();
            }
        }

        public MarkdownTheme Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}