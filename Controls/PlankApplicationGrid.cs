using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Winux.Controls
{
    internal class PlankApplicationGrid : Grid
    {
        private string name;

        public PlankApplicationGrid()
        {
            Loaded += ApplicationButton_Loaded;

            button.Click += Application_Click;
            button.Height = button.Width;

            stackPanel.Children.Add(button);
            Children.Add(stackPanel);
        }

        private readonly StackPanel stackPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center
        };

        private readonly ImageButton button = new ImageButton();

        private readonly Grid grid = new Grid
        {
            Background = (SolidColorBrush)Application.Current.Resources["ActiveBrush"],
            CornerRadius = new CornerRadius(1),
            Height = 2,
            Margin = new Thickness(12, 2, 12, 2),
        };

        private void ApplicationButton_Loaded(object sender, RoutedEventArgs e)
        {
            name = Name.Remove(Name.Length - 6);
            stackPanel.Name = name + "StackPanel";
            button.ImageSource = (SvgImageSource)Application.Current.Resources[App.IconTheme + name + "Icon"];
            button.Name = name;
            grid.Name = name + "Grid";
        }

        private void Application_Click(object sender, RoutedEventArgs e)
        {
            string appName = ((Button)sender).Name;

            Frame rootFrame = (Frame)Window.Current.Content;
            MainPage mainPage = (MainPage)rootFrame.Content;
            Grid WindowContent = (Grid)mainPage.FindName("WindowContent");

            foreach (var elem in WindowContent.Children)
            {
                var contentWindow = (ContentWindow)elem;
                if (contentWindow != null && contentWindow.Name != appName)
                {
                    contentWindow.Visibility = Visibility.Collapsed;
                }
            }

            Grid plank = (Grid)mainPage.FindName("Plank");
            IEnumerable<StackPanel> stackPanels = plank.Children.OfType<StackPanel>();
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
                stackPanel.Children.Add(grid);

                Type type = Type.GetType("Winux.Views." + name);
                if (type != null)
                {
                    object instance = Activator.CreateInstance(type);
                    Grid content = (Grid)contentWindow.FindName("ViewContent");
                    content.Children.Add((UIElement)instance);
                }
            }
            else
            {
                ContentWindow contentWindow = (ContentWindow)appWindow;
                if (contentWindow.Visibility == Visibility.Visible)
                {
                    contentWindow.Visibility = Visibility.Collapsed;
                    grid.Background = (SolidColorBrush)Application.Current.Resources["InActiveBrush"];
                    grid.Width = 12;
                }
                else
                {
                    contentWindow.Visibility = Visibility.Visible;
                    grid.Background = (SolidColorBrush)Application.Current.Resources["ActiveBrush"];
                    grid.Width = double.NaN;

                }
            }
        }
    }
}
