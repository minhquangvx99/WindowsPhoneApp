using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp
{
    public partial class StationMonitoringPage : PhoneApplicationPage
    {
        public StationMonitoringPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            btnStationMonitoring.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            tbFullName.Text = ConfigClass.UserData.user.FullName;
            tbEmail.Text = ConfigClass.UserData.user.Email;
        }

        private void btnStationActivity_Click(object sender, RoutedEventArgs e)
        {
            string uriString = ConfigClass.Uri;
            Uri uri = new Uri(uriString, UriKind.Absolute);
            var success = Windows.System.Launcher.LaunchUriAsync(uri);
            //NavigationService.Navigate(new Uri("/StationActivityPage.xaml", UriKind.Relative));
        }

        private void btnStationData_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StationDataPage.xaml", UriKind.Relative));
        }

        private void btnMonitoringByStationType_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MonitoringByStationTypePage.xaml", UriKind.Relative));
        }

        private void btnStationDataMining_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StationDataMiningPage.xaml", UriKind.Relative));
        }
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -100)
            {
                MoveViewWindow(-420);
            }
            else
            {
                MoveViewWindow(0);
            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = left;
            ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(LayoutRoot, Math.Min(Math.Max(-840, Canvas.GetLeft(LayoutRoot) + e.DeltaManipulation.Translation.X), 0));
        }

        double initialPosition;
        bool _viewMoved = false;
        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(LayoutRoot);
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (_viewMoved)
                return;
            if (Math.Abs(initialPosition - left) < 100)
            {
                //bouncing back
                MoveViewWindow(initialPosition);
                return;
            }
            //change of state
            if (initialPosition - left > 0)
            {
                //slide to the left
                if (initialPosition > -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(-840);
            }
            else
            {
                //slide to the right
                if (initialPosition < -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(0);
            }

        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginHomePage.xaml", UriKind.Relative));
        }

        private void btnStationManager_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StationManagerPage.xaml", UriKind.Relative));
        }

        private void btnDataCollectionSupport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DataCollectionSupportPage.xaml", UriKind.Relative));
        }

        private void btnStationMonitoring_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StationMonitoringPage.xaml", UriKind.Relative));
        }

        private void btnWeatherInformation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginWeatherInformationPage.xaml", UriKind.Relative));
        }

        private void btnSendAlert1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginSendAlertPage.xaml", UriKind.Relative));
        }

        private void btnAccountSetting_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingAccountPage.xaml", UriKind.Relative));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
        }
    }
}