using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Net.Http;
using MHSM_PhoneApp.Entities;
using System.Text;
using System.Net.Http.Headers;

namespace MHSM_PhoneApp
{
    public partial class LoginHomePage : PhoneApplicationPage
    {
        public LoginHomePage()
        {
            InitializeComponent();
            txtTitle2.Text = "Xin chào " + ConfigClass.UserData.user.FullName + "!";
            btnHome.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            tbFullName.Text = ConfigClass.UserData.user.FullName;
            tbEmail.Text = ConfigClass.UserData.user.Email;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadAlert();
        }

        private async void LoadAlert()
        {
            WarningEntity resultContent = new WarningEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_HOME_WARNING + ConfigClass.UserData.user.UserID.ToString(), "");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<WarningEntity>(resultContentString);
            }
            var data = new List<NowDataWarning>();
            foreach (var item in resultContent.Data.list)
            {
                BitmapImage imagePath = new BitmapImage();
                if (item.Type == 2)
                {
                    if (item.UrlImage.Count() > 0)
                    {
                        imagePath = new BitmapImage(new Uri(item.UrlImage[0]));
                    }
                }
                else
                {
                    imagePath = new BitmapImage(new Uri(item.Icon));
                }
                string StationProvinceName;
                if (item.StationName == null)
                {
                    StationProvinceName= item.ProvinceName ;
                }
                else{
                    StationProvinceName= item.StationName + ", " + item.ProvinceName ;
                }
                data.Add(new NowDataWarning() { DataSave = item.ID.ToString() + "," + item.Type, Title = item.Title, Icon = imagePath, StationProvinceName = StationProvinceName });
            }
            if (data.Count > 0)
            {
                txtTitle3.Text = "Cảnh báo";
            }
            else
            {
                txtTitle3.Text = "";
            }
            listBuilding.ItemsSource = data;
        }

        private void btnDetailWarning_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            string[] arr = tag.ToString().Split(',');
            if (arr[1] == "2")
            {
                NavigationService.Navigate(new Uri("/DisasterAnnouncementDetailPage.xaml?ID=" + arr[0], UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/WarningInfoPage.xaml?ID=" + arr[0], UriKind.Relative));
            }
        }

        private void btnRefresh(object sender, RoutedEventArgs e)
        {
            LoadAlert();
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