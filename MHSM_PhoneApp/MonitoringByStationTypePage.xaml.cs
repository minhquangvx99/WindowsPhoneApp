using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp
{
    public partial class MonitoringByStationTypePage : PhoneApplicationPage
    {
        public MonitoringByStationTypePage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No entries in back navigation history.");
            }
        }

        private void btnAutomaticHydrology_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AutomaticHydrologyPage.xaml", UriKind.Relative));
        }

        private void btnWaterResources_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WaterResourcesPage.xaml", UriKind.Relative));
        }

        private void btnAutomaticMeteorology_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AutomaticMeteorologyPage.xaml", UriKind.Relative));
        }
    }
}