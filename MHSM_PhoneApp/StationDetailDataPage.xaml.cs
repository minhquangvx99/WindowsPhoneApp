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
    public partial class StationDetailDataPage : PhoneApplicationPage
    {
        public StationDetailDataPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string DataSave = string.Empty;
            string StationId = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("DataSave", out DataSave) && NavigationContext.QueryString.TryGetValue("StationId", out StationId))
            {
                LoadStationDetailData(DataSave, int.Parse(StationId));
            }
        }
        private async void LoadStationDetailData(string DataSave, int StationId)
        {
            var listData = DataSave.Split(',');
            DateTime now = DateTime.Now;
            StationDataEntity resultContent = new StationDataEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                string startDate = now.AddDays(-10).ToString("dd/MM/yyyy HH:mm:ss");
                string endTime = now.ToString("dd/MM/yyyy HH:mm:ss");
                var result = await client.PostAsJsonAsync(ConfigClass.VTYPE_LIST + "?stationId=" + StationId + "&elementID=" + listData[0] + "&valuetypeID=" + listData[1] + "&startDate=" + listData[2] + "&endTime=" + listData[2] + "&pageNumber=0&pageSize=100", "");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<StationDataEntity>(resultContentString);
            }
            DataOfStation resultContent1 = new DataOfStation();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.DETAIL_STATION + "40057");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent1 = JsonConvert.DeserializeObject<DataOfStation>(resultContentString);
            }
            var data = new List<ListDataStation>();
            var item = resultContent.Data.list.First();
                data.Add(new ListDataStation() { STATION_NAME = resultContent1.Data.STATION_NAME, PROVINCE_NAME =resultContent1.Data.PROVINCE_NAME, ELEMENT_NAME = item.ELEMENT_NAME, VTYPE_NAME = item.VTYPE_NAME, TIMESTAMP_STRING = item.TIMESTAMP.ToString("dd/MM/yyyy HH:mm"), Value = item.Value, UnitName = item.UnitName, DataSave = item.ELEMENT_ID.ToString() + "," + item.VTYPE_ID.ToString() + "," + item.TIMESTAMP.ToString("dd/MM/yyyy HH:mm:ss") });
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