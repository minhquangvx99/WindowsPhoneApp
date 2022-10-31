using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MHSM_PhoneApp.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MHSM_PhoneApp
{
    public partial class StationDataDetailPage : PhoneApplicationPage
    {
        public StationDataDetailPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string listData = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("listData", out listData))
            {
                string[] arr = listData.Split('/');
                LoadDataStation(arr[0], arr[1], arr[2], long.Parse(arr[3]), long.Parse(arr[4]), arr[5], DateTime.ParseExact(arr[6], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture));
            }
        }
        private async void LoadDataStation(string stationName, string elementId, string vtypeId, long fromDate, long toDate, string vtypeName, DateTime timeStamp)
        {
            var post = new
            {
                STATION_NAME = stationName,
                ELEMENT_ID = elementId,
                VTYPE_ID = vtypeId,
                FROM_DATE = fromDate,
                TO_DATE = toDate,
                PAGEINDEX = 1,
                PAGESIZE = 100
            };
            StationDataDetailEntity resultContent = new StationDataDetailEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.DATA_MINING_REPORT, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<StationDataDetailEntity>(resultContentString);
            }
            var res = resultContent.Data.Where(s => s.VTYPE_NAME == vtypeName && s.TIMESTAMP == timeStamp).ToList();
            List<DataDetailForScreen> data = new List<DataDetailForScreen>();
            foreach (var item in res)
            {
                data.Add(new DataDetailForScreen() {Title="Mã trạm",Content=item.STATION_NO });
                data.Add(new DataDetailForScreen() { Title = "Tên trạm", Content = item.STATION_NAME });
                data.Add(new DataDetailForScreen() { Title = "Yếu tố đo", Content = item.ELEMENT_NAME });
                data.Add(new DataDetailForScreen() { Title = "Giá trị đo", Content = item.VTYPE_NAME });
                data.Add(new DataDetailForScreen() { Title = "Thời điểm đo", Content = item.TIMESTAMP.ToString("HH:mm dd/MM/yyyy") });
                data.Add(new DataDetailForScreen() { Title = "Giá trị đo tức thời", Content = item.VALUE.ToString() });
                data.Add(new DataDetailForScreen() { Title = "Đơn vị đo", Content = item.SYMBOL });
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