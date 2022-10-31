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
    public partial class DisasterAnnouncementPage : PhoneApplicationPage
    {
        public DisasterAnnouncementPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            LoadProvince();
            LoadNotiType();
            List<string> dataStationName = new List<string>();
            dataStationName.Add("Quận/Huyện");
            myDistrictList.ItemsSource = dataStationName;
            FromDatePicker.Value = null;
            FromTimePicker.Value = null;
            ToDatePicker.Value = null;
            ToTimePicker.Value = null;
            Search();
        }

        private async void LoadDisasterAnnouncement(string provinceNo, string districtNo, int typeWarning, string startDate, string endDate)
        {
            var post = new PostDisasterAnnouncemenSearchAPI()
            {
                PROVINCE_NO = provinceNo,
                DISTRICT_NO = districtNo,
                TYPE_WARNING = typeWarning,
                STARTDATE = startDate,
                ENDATE = endDate,
                USER_ID = ConfigClass.UserData.user.UserID,
                PAGEINDEX = 1,
                PAGESIZE = 100
            };
            DisasterAnnouncementEntity resultContent = new DisasterAnnouncementEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_CALAMITY, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<DisasterAnnouncementEntity>(resultContentString);
            }
            var data = new List<DisasterAnnouncemenDataList>();
            foreach (var item in resultContent.Data.list)
            {
                var IsRead = new BitmapImage();
                if (item.STATE != 2)
                {
                    IsRead = new BitmapImage(new Uri("/Assets/IsRead/no.png", UriKind.Relative));
                }
                else
                {
                    IsRead = new BitmapImage(new Uri("/Assets/IsRead/yes.png", UriKind.Relative));
                }
                BitmapImage imagePath = new BitmapImage();
                if (item.URL_IMAGE_SPLIT.Count() > 0)
                {
                    imagePath = new BitmapImage(new Uri(item.URL_IMAGE_SPLIT[0]));
                }
                data.Add(new DisasterAnnouncemenDataList() { NOTIFICATION_ID = item.NOTIFICATION_ID, IsRead = IsRead, IconBitmapImage = imagePath, WARNING_TITLE = item.WARNING_TITLE, PROVINCE_NAME = item.PROVINCE_NAME });
            }
            listBuilding.ItemsSource = data;
        }

        NotiTypeEntity resultNotiType = new NotiTypeEntity();
        private async void LoadNotiType()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LIST_NOTI_TYPE);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultNotiType = JsonConvert.DeserializeObject<NotiTypeEntity>(resultContentString);
            }
            List<string> dataNotiType = new List<string>();
            dataNotiType.Add("Loại thông báo");
            foreach (var item in resultNotiType.Data)
            {
                dataNotiType.Add(item.NAME);
            }
            myNotiTypeList.ItemsSource = dataNotiType;
        }

        DistrictByProvinceEntity resultProvince = new DistrictByProvinceEntity();
        private async void LoadProvince()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_DISTRICT_BY_PROVINCE, "");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultProvince = JsonConvert.DeserializeObject<DistrictByProvinceEntity>(resultContentString);
            }
            List<string> dataProvince = new List<string>();
            dataProvince.Add("Tỉnh/Thành phố");
            foreach (var item in resultProvince.Data.list)
            {
                dataProvince.Add(item.PROVINCE_NAME);
            }
            myProvinceList.ItemsSource = dataProvince;
        }

        List<DistrictInProvinceData> resultDistrict = new List<DistrictInProvinceData>();
        private void LoadDistrict(string provinceNo)
        {
            resultDistrict = resultProvince.Data.list.Where(p => p.PROVINCE_NO == provinceNo).FirstOrDefault().DistrictInProvince;
            List<string> dataDistrict = new List<string>();
            dataDistrict.Add("Quận/Huyện");
            foreach (var item in resultDistrict)
            {
                dataDistrict.Add(item.DISTRICT_NAME);
            }
            myDistrictList.ItemsSource = dataDistrict;
        }

        private void myProvinceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string provinceNo = "";
            if (myProvinceList.SelectedIndex > 0)
            {
                provinceNo = resultProvince.Data.list.Where(p => p.PROVINCE_NAME == myProvinceList.SelectedItem).FirstOrDefault().PROVINCE_NO;
                LoadDistrict(provinceNo);
            }
            else
            {
                List<string> dataStationName = new List<string>();
                dataStationName.Add("Quận/Huyện");
                myDistrictList.ItemsSource = dataStationName;
            }
        }

        private void Search()
        {
            string provinceNo = "";
            string districtNo = "";
            int typeWarning = 0;
            if (myProvinceList.SelectedIndex > 0)
            {
                provinceNo = resultProvince.Data.list.Where(p => p.PROVINCE_NAME == myProvinceList.SelectedItem).FirstOrDefault().PROVINCE_NO;
            }
            if (myDistrictList.SelectedIndex > 0)
            {
                districtNo = resultDistrict.Where(p => p.DISTRICT_NAME == myDistrictList.SelectedItem).FirstOrDefault().DISTRICT_NO;
            }
            if (myNotiTypeList.SelectedIndex > 0)
            {
                typeWarning = resultNotiType.Data.Where(p => p.NAME == myNotiTypeList.SelectedItem).FirstOrDefault().ID;
            }
            string from = "";
            string to = "";
            if (ToDatePicker.Value != null && ToTimePicker.Value != null && FromDatePicker.Value != null && FromTimePicker.Value != null)
            {
                if ((DateTime)ToDatePicker.Value > ((DateTime)FromDatePicker.Value).AddMonths(1))
                {
                    MessageBox.Show("Từ thời gian bắt đầu đến thời gian kết thúc không được lớn hơn 1 tháng");
                }
                else
                {
                    from += ((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy");
                    from += " " + ((DateTime)FromTimePicker.Value).ToString("HH:mm:ss");
                    to += ((DateTime)ToDatePicker.Value).ToString("dd/MM/yyyy");
                    to += " " + ((DateTime)ToTimePicker.Value).ToString("HH:mm:ss");
                    LoadDisasterAnnouncement(provinceNo,districtNo,typeWarning, from, to);
                }
            }
            else
            {
                FromDatePicker.Value = null;
                FromTimePicker.Value = null;
                ToDatePicker.Value = null;
                ToTimePicker.Value = null;
                LoadDisasterAnnouncement(provinceNo, districtNo, typeWarning, from, to);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            myProvinceList.SelectedIndex = 0;
            myDistrictList.SelectedIndex = 0;
            myNotiTypeList.SelectedIndex = 0;
            FromDatePicker.Value = null;
            FromTimePicker.Value = null;
            ToDatePicker.Value = null;
            ToTimePicker.Value = null;
        }

        private void btnRefresh(object sender, RoutedEventArgs e)
        {
            Search();
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
        private void btnDisasterAnnouncementDetail_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/DisasterAnnouncementDetailPage.xaml?ID=" + tag, UriKind.Relative));
        }

    }
}