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
    public partial class WaterResourcesDetailPage : PhoneApplicationPage
    {
        public WaterResourcesDetailPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string StationName = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("StationName", out StationName))
            {
                LoadStationWaterResourcesDetail(StationName);
            }
        }

        private async void LoadStationWaterResourcesDetail(string StationName)
        {
            var post = new
            {
                STATION_NAME = StationName,
                PROJECT_ID = ConfigClass.waterResource
            };
            MonitoringEntity resultContent = new MonitoringEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.MONITORING_DATA, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<MonitoringEntity>(resultContentString);
            }
            var data = new List<DataDetailForScreen>();
            data.Add(new DataDetailForScreen() { Title = "Mã trạm", Content = resultContent.Data.First().STATION_NO, Content1 = "" });
            data.Add(new DataDetailForScreen() { Title = "Tên trạm", Content = resultContent.Data.First().STATION_NAME, Content1 = "" });
            data.Add(new DataDetailForScreen() { Title = "Thời điểm đo", Content = resultContent.Data.First().DATE_TIME, Content1 = "" });
            foreach (var item in resultContent.Data.First().LIST_VALUE_TYPE)
            {
                data.Add(new DataDetailForScreen() { Title = item.VTYPE_NAME, Content = item.VALUE, Content1 = item.SYMBOL });
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

    }
}