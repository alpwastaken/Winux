using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Winux.Controls;

namespace Winux
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public string LiveTime => DateTime.Now.ToString("HH:mm:ss");

        public MainPage()
        {
            InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var appTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;
            appTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            Window.Current.SetTitleBar(WingPanel);

            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, e) => OnPropertyChanged("LiveTime");
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Applications_Click(object sender, RoutedEventArgs e)
        {
            string appName = ((Button)sender).Name;
            foreach (var contentWindow in WindowContent.Children.OfType<ContentWindow>())
            {
                if (contentWindow != null && contentWindow.Name != appName)
                {
                    contentWindow.Visibility = Visibility.Collapsed;
                }
            }

            IEnumerable<StackPanel> stackPanels = Plank.Children.OfType<StackPanel>();
            foreach (var stackPanel in stackPanels)
            {
                if (stackPanel.Children.Count > 0)
                {
                    foreach (var appButton in stackPanel.Children.OfType<PlankApplicationGrid>())
                    {
                        if (appButton != null && appButton.Name != Name)
                        {
                            string _appName = appButton.Name.Remove(appButton.Name.Length - 6);
                            StackPanel appStackPanel = (StackPanel)appButton.FindName(_appName + "StackPanel");
                            Grid appGrid = (Grid)appStackPanel.FindName(_appName + "Grid");
                            if (appGrid != null)
                            {
                                appGrid.Background = (SolidColorBrush)Application.Current.Resources["InActiveBrush"];
                                appGrid.Width = 12;
                            }
                        }
                    }
                }
            }

            var appWindow = (UIElement)WindowContent.FindName(appName);
            if (!WindowContent.Children.Contains(appWindow))
            {
                ContentWindow contentWindow = new ContentWindow { Name = appName };
                WindowContent.Children.Add(contentWindow);
                ApplicationsOpened.Margin = new Thickness(Applications.ActualWidth + 8, 8, 8, 8);
                ApplicationsOpened.Visibility = Visibility.Visible;
            }
            else
            {
                ContentWindow contentWindow = (ContentWindow)appWindow;
                if (contentWindow.Visibility == Visibility.Visible)
                {
                    contentWindow.Visibility = Visibility.Collapsed;
                }
                else
                {
                    contentWindow.Visibility = Visibility.Visible;
                }
            }
        }
    }
}