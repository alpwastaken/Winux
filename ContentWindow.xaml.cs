using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Winux
{
    public sealed partial class ContentWindow : UserControl
    {
        private MainPage mainPage;

        public ContentWindow()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = App.ResourceLoader.GetString(Name);
            Frame rootFrame = (Frame)Window.Current.Content;
            mainPage = (MainPage)rootFrame.Content;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            Grid appGrid = (Grid)mainPage.FindName(Name + "Grid");
            if (appGrid != null)
            {
                appGrid.Background = (SolidColorBrush)Application.Current.Resources["InActiveBrush"];
                appGrid.Width = 12;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Grid windowContent = (Grid)mainPage.FindName("WindowContent");
            windowContent.Children.Remove(this);
            if (Name != "Applications")
            {
                StackPanel stackpanel = (StackPanel)mainPage.FindName(Name + "StackPanel");
                Grid appGrid = (Grid)mainPage.FindName(Name + "Grid");
                stackpanel.Children.Remove(appGrid);
            }
            else
            {
                Rectangle applicationsOpened = (Rectangle)mainPage.FindName("ApplicationsOpened");
                applicationsOpened.Visibility = Visibility.Collapsed;
            }
        }
    }
}