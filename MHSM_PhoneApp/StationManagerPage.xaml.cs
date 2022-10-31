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
using MHSM_PhoneApp.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MHSM_PhoneApp
{
    public partial class StationManagerPage : PhoneApplicationPage
    {
        int IsActive;
        public StationManagerPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            btnStationManager.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            btnActive.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            btnActive.Background = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
            tbFullName.Text = ConfigClass.UserData.user.FullName;
            tbEmail.Text = ConfigClass.UserData.user.Email;
            IsActive = 1;
            LoadStation(1, "", "", "");
            LoadProject();
            LoadSize();
        }

        private async void LoadStation(int IsActive, string stationName, string projectId, string siteNo)
        {
            var post = new PostStation()
            {
                STATION_NO_OR_NAME = stationName,
                PROJECT_ID = projectId,
                SITE_NO = siteNo,
                IS_ACTIVE = IsActive,
                PAGEINDEX = 1,
                PAGESIZE = 100
            };
            StationEntity resultContent = new StationEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_STATION, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<StationEntity>(resultContentString);
            }
            listBuilding.ItemsSource = resultContent.Data.list;
        }

        ProjectEntity resultProject = new ProjectEntity();
        private async void LoadProject()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LIST_PROJECT);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultProject = JsonConvert.DeserializeObject<ProjectEntity>(resultContentString);
            }
            List<string> dataProject = new List<string>();
            dataProject.Add("Dự án");
            foreach (var item in resultProject.Data)
            {
                dataProject.Add(item.PROJECT_NAME);
            }
            myProjectList.ItemsSource = dataProject;
        }

        SizeEntity resultSize = new SizeEntity();
        private async void LoadSize()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LIST_SITE);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultSize = JsonConvert.DeserializeObject<SizeEntity>(resultContentString);
            }
            List<string> dataSize = new List<string>();
            dataSize.Add("Khu vực");
            foreach (var item in resultSize.Data)
            {
                dataSize.Add(item.SITE_NAME);
            }
            mySizeList.ItemsSource = dataSize;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string projectId = "";
            string siteNo = "";
            if (myProjectList.SelectedIndex > 0)
            {
                projectId = resultProject.Data.Where(p => p.PROJECT_NAME == myProjectList.SelectedItem).FirstOrDefault().PROJECT_ID.ToString();
            }
            if (mySizeList.SelectedIndex > 0)
            {
                siteNo = resultSize.Data.Where(p => p.SITE_NAME == mySizeList.SelectedItem).FirstOrDefault().SITE_NO;
            }
            LoadStation(IsActive, txtStationName.Text, projectId, siteNo);
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            txtStationName.Text = "";
            myProjectList.SelectedIndex = 0;
            mySizeList.SelectedIndex = 0;
        }

        private void btnDetailStation_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/StationInfoPage.xaml?StationId=" + tag + "&IsActive=" + IsActive.ToString(), UriKind.Relative));
        }

        private void btnActive_Click(object sender, RoutedEventArgs e)
        {
            IsActive = 1;
            btnActive.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            btnActive.Background = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
            btnNoActive.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            btnNoActive.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            LoadStation(1, "", "", "");
        }
        private void btnNoActive_Click(object sender, RoutedEventArgs e)
        {
            IsActive = 0;
            btnActive.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            btnActive.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            btnNoActive.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            btnNoActive.Background = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
            LoadStation(0, "", "", "");
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