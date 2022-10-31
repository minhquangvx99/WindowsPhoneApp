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
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MHSM_PhoneApp
{
    public partial class SensorDetailPage : PhoneApplicationPage
    {
        public SensorDetailPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string SensorNo = string.Empty;
            string StationNo = string.Empty;
            string IsActive = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("SensorNo", out SensorNo) && NavigationContext.QueryString.TryGetValue("StationNo", out StationNo) && NavigationContext.QueryString.TryGetValue("IsActive", out IsActive))
            {
                LoadSensorDetail(SensorNo, StationNo, int.Parse(IsActive));
            }
        }

        private async void LoadSensorDetail(string SensorNo, string StationNo, int IsActive)
        {
            var post = new PostSensor()
            {
                SENSOR_NO= SensorNo,
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
            if (resultContent.Data.list.FirstOrDefault().IS_ACTIVE_CONFIG == 1)
            {
                resultContent.Data.list.FirstOrDefault().IS_ACTIVE_CONFIG_STRING = "Hoạt động";
            }
            else
            {
                resultContent.Data.list.FirstOrDefault().IS_ACTIVE_CONFIG_STRING = "Không hoạt động";
            }
            listBuilding.ItemsSource = resultContent.Data.list;
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
    }
}