using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Winux.Controls
{
    internal class ImageButton : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ImageButton()
        {
            Background = new SolidColorBrush(Colors.Transparent);
            Height = _imageHeight + 4;
            Padding = new Thickness(0);
            VerticalAlignment = VerticalAlignment.Center;
            Width = _imageWidth + 4;

            _image.Height = _imageHeight;
            _image.Width = _imageWidth;

            Content = _image;
        }

        private readonly Image _image = new Image
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        private double _imageWidth = 48;
        public double ImageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; _image.Width = value; OnPropertyChanged("ImageWidth"); }
        }

        private double _imageHeight = 48;
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
    }
}
