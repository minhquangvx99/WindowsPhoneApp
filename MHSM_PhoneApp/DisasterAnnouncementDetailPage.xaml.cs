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
    public partial class DisasterAnnouncementDetailPage : PhoneApplicationPage
    {
        public DisasterAnnouncementDetailPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string ID;
            if (NavigationContext.QueryString.TryGetValue("ID", out ID))
            {
                LoadDisasterAnnouncementDetail(int.Parse(ID));
            }
        }
        private async void LoadDisasterAnnouncementDetail(int ID)
        {
            DisasterAnnouncementDetailEntity resultContent = new DisasterAnnouncementDetailEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.CALAMITY_DETAILS + ID + "&user_id=" + ConfigClass.UserData.user.UserID,"");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<DisasterAnnouncementDetailEntity>(resultContentString);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.UPDATE_STATE_NOTI + "NotificationID="+ ID + "&type=2", "");
                result.EnsureSuccessStatusCode();
            }
            DisasterAnnouncemenDataList disasterAnnouncemenDataList = resultContent.Data.list;
            var data = new List<WarningInfo>();
            data.Add(new WarningInfo() { Title = "Người thông báo", Content = disasterAnnouncemenDataList.USER_NAME });
            data.Add(new WarningInfo() { Title = "Thời gian xuất hiện cảnh báo", Content = disasterAnnouncemenDataList.STARTTIME_WARNING.ToString("dd/MM/yyyy hh:mm") });
            data.Add(new WarningInfo() { Title = "Kinh độ", Content = disasterAnnouncemenDataList.LONGTITUDE });
            data.Add(new WarningInfo() { Title = "Vĩ độ", Content = disasterAnnouncemenDataList.LATTITUDE });
            data.Add(new WarningInfo() { Title = "Quận/Huyện", Content = disasterAnnouncemenDataList.DISTRICTS_NAME });
            data.Add(new WarningInfo() { Title = "Tỉnh/Thành phố", Content = disasterAnnouncemenDataList.PROVINCE_NAME });
            data.Add(new WarningInfo() { Title = "Số điện thoại người thông báo", Content = disasterAnnouncemenDataList.PHONENUMBER });
            data.Add(new WarningInfo() { Title = "Email người thông báo", Content = disasterAnnouncemenDataList.EMAIL });
            data.Add(new WarningInfo() { Title = "Loại thông báo", Content = disasterAnnouncemenDataList.WARNING_TITLE });
            data.Add(new WarningInfo() { Title = "Nội dung", Content = disasterAnnouncemenDataList.WARNING_CONTENT });
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