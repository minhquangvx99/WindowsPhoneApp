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
    public partial class StationInfoPage : PhoneApplicationPage
    {
        string IsActive = string.Empty;
        string StationId = string.Empty;
        public StationInfoPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("StationId", out StationId) && NavigationContext.QueryString.TryGetValue("IsActive", out IsActive))
            {
                LoadStation(StationId, int.Parse(IsActive));
            }
        }

        private async void LoadStation(string StationId, int IsActive)
        {
            var post = new PostStation()
            {
                IS_ACTIVE = IsActive,
                PAGEINDEX = 1,
                PAGESIZE = 9999
            };
            StationInfo resultContent = new StationInfo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_STATION, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                var dataStation = JsonConvert.DeserializeObject<StationEntity>(resultContentString);
                resultContent = dataStation.Data.list.Where(a => a.STATION_ID == StationId).FirstOrDefault();
            }
            var data = new List<StationInfo>();
            if (resultContent.METHOD == "TD")
            {
                resultContent.METHOD = "Tự động";
            }
            else{
                resultContent.METHOD = "Thủ công";
            }
            if (resultContent.DATA_TYPE_LOGGER_ID == 0)
            {
                resultContent.DATA_TYPE_LOGGER = "Trạm không có loại logger";
            }
            if (resultContent.STATION_HEIGHT == "0")
            {
                resultContent.STATION_HEIGHT = "";
            }
            resultContent.CREATE_DATE_STRING = resultContent.CREATE_DATE.ToString("dd/mm/yyyy");
            resultContent.MODIFIED_DATE_STRING = resultContent.MODIFIED_DATE.ToString("dd/mm/yyyy");
            if (resultContent.IS_ACTIVE == 1)
            {
                resultContent.IS_ACTIVE_STRING = "Hoạt động";
            }
            else
            {
                resultContent.IS_ACTIVE_STRING = "Không hoạt động";
            }
            resultContent.MONITORING_SYSTEM_TYPE_STRING = ConfigClass.MONITORING_SYSTEM[resultContent.MONITORING_SYSTEM_TYPE];
            data.Add(resultContent);
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

        private void btnSensorList_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/SensorListPage.xaml?StationNo=" + tag + "&IsActive=" + IsActive.ToString(), UriKind.Relative));
        }

        private void btnLoggerType_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            if (int.Parse(tag.ToString()) != 0)
            {
                NavigationService.Navigate(new Uri("/LoggerPage.xaml?Id=" + tag, UriKind.Relative));
            }
        }

        private void btnViewDataStation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ViewDataStationPage.xaml?StationId=" + StationId, UriKind.Relative));
        }
    }
}