using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Winux.Controls
{
    internal class ImageTextButton : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ImageTextButton()
        {
            Background = new SolidColorBrush(Colors.Transparent);
            Padding = new Thickness(0);

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Content = stackPanel;
            stackPanel.Children.Add(_image);
            stackPanel.Children.Add(_textBlock);
        }

        private readonly Image _image = new Image
        {
            Margin = new Thickness(4, 0, 2, 0)
        };

        private readonly TextBlock _textBlock = new TextBlock
        {
            FontSize = 12,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(2, 0, 4, 0),
            VerticalAlignment = VerticalAlignment.Center,
        };

        private double _imageWidth = 16;
        public double ImageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; _image.Width = value; OnPropertyChanged("ImageWidth"); }
        }

        private double _imageHeight = 16;
        public double ImageHeight
        {
            get { return _imageHeight; }
            set { _imageHeight = value; _image.Height = value; OnPropertyChanged("ImageHeight"); }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; _image.Source = value; OnPropertyChanged("ImageSource"); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; _textBlock.Text = App.ResourceLoader.GetString(value); OnPropertyChanged("Text"); }
        }
    }
}
