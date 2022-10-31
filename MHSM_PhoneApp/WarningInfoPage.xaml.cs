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
using System.Text;
using System.Net.Http.Headers;

namespace MHSM_PhoneApp
{
    public partial class WarningInfoPage : PhoneApplicationPage
    {
        public WarningInfoPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string ID = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("ID", out ID))
            {
                LoadAlert(int.Parse(ID));
            }
        }
        private async void LoadAlert(int ID)
        {
            DetaiWarning resultContent = new DetaiWarning();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.WARNING_THRESHOLD_DETAILS +ID.ToString());
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<WarningDetailInfo>(resultContentString).Data.list;
            }
            var data = new List<WarningInfo>();
            data.Add(new WarningInfo() { Title = "Tên trạm", Content = resultContent.STATION_NAME });
            data.Add(new WarningInfo() { Title = "Thời gian xuất hiện cảnh báo", Content = resultContent.STARTTIME_WARNING.ToString("dd/MM/yyyy hh:mm") });
            data.Add(new WarningInfo() { Title = "Quận/Huyện", Content = resultContent.DISTRICTS_NAME });
            data.Add(new WarningInfo() { Title = "Tỉnh/Thành phố", Content = resultContent.PROVINCE_NAME });
            data.Add(new WarningInfo() { Title = "Yếu tố đo", Content = resultContent.ELEMENT_NAME });
            data.Add(new WarningInfo() { Title = "Giá trị đo", Content = resultContent.VTYPE_NAME });
            data.Add(new WarningInfo() { Title = "Thông số đo", Content = resultContent.VALUE.ToString() });
            data.Add(new WarningInfo() { Title = "Đơn vị đo", Content = resultContent.UNIT_NAME });
            data.Add(new WarningInfo() { Title = "Cấp độ", Content = resultContent.WARNING_LEVEL.ToString() });
            data.Add(new WarningInfo() { Title = "Người nhận cảnh báo", Content = resultContent.USER_RECEIVE_WARNING });
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