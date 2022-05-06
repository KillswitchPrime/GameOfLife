using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GameOfLifeWPF.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        public void OnClickGameOfLife(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/GameOfLifePage.xaml", UriKind.Relative));
        }

        public void OnClickBriansBrain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/BriansBrainPage.xaml", UriKind.Relative));
        }
    }
}
