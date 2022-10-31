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
using MHSM_PhoneApp.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MHSM_PhoneApp
{
    public partial class SensorListPage : PhoneApplicationPage
    {
        string StationNo = string.Empty;
        string IsActive = string.Empty;
        public SensorListPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("StationNo", out StationNo) && NavigationContext.QueryString.TryGetValue("IsActive", out IsActive))
            {
                LoadSensor(StationNo, int.Parse(IsActive));
            }
        }

        private async void LoadSensor(string StationNo, int IsActive)
        {
            var post = new PostSensor()
            {
                STATION_NO = StationNo,
                IS_ACTIVE = IsActive,
                PAGEINDEX = 1,
                PAGESIZE = 100
            };
            SensorEntity resultContent = new SensorEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_SENSOR, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<SensorEntity>(resultContentString);
            }
            var data = new List<SensorList>();
            foreach (var item in resultContent.Data.list)
            {
                var ActiveImage = new BitmapImage();
                if (item.IS_ACTIVE_CONFIG == 1)
                {
                    ActiveImage = new BitmapImage(new Uri("/Assets/Active/active.png", UriKind.Relative)); 
                }
                else
                {
                    ActiveImage = new BitmapImage(new Uri("/Assets/Active/noActive.png", UriKind.Relative)); 
                }
                data.Add(new SensorList() { ACTIVE_IMAGE = ActiveImage, SENSOR_NAME = item.SENSOR_NAME, PROJECT_NO = "Dự án " + item.PROJECT_NO, ELEMENT_NAME = item.ELEMENT_NAME, SENSOR_NO = item.SENSOR_NO });
            }
            listBuilding.ItemsSource = data;
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
        private void btnSensorDetail_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/SensorDetailPage.xaml?SensorNo=" + tag + "&StationNo=" + StationNo + "&IsActive=" + IsActive.ToString(), UriKind.Relative));
        }
    }
}