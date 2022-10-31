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
using System.Net.Http.Headers;
using MHSM_PhoneApp.Entities;
using System.Net.Http;
using Newtonsoft.Json;

namespace MHSM_PhoneApp
{
    public partial class ThresholdWarningPage : PhoneApplicationPage
    {
        public ThresholdWarningPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            LoadProvince();
            List<string> dataStationName = new List<string>();
            dataStationName.Add("Tên trạm");
            myStationNameList.ItemsSource = dataStationName;
            FromDatePicker.Value = null;
            FromTimePicker.Value = null;
            ToDatePicker.Value = null;
            ToTimePicker.Value = null;
            Search();
        }

        private async void LoadThresholdWarning(int stationId, string provinceNo, string startDate, string endDate)
        {
            var post = new PostSearchAPI()
            {
                ST_ID = stationId,
                PROVINCE_NO = provinceNo,
                STARTDATE = startDate,
                ENDATE = endDate,
                UserID = ConfigClass.UserData.user.UserID,
                PAGEINDEX = 1,
                PAGESIZE = 100
            };
            ThresholdWarningEntity resultContent = new ThresholdWarningEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.LIST_WARNING_THRESHOLD, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<ThresholdWarningEntity>(resultContentString);
            }
            var data = new List<ThresholdWarningDataList>();
            if (resultContent.Data != null)
            {
                foreach (var item in resultContent.Data.list)
                {
                    var IsRead = new BitmapImage();
                    if (item.STATE == 1)
                    {
                        IsRead = new BitmapImage(new Uri("/Assets/IsRead/no.png", UriKind.Relative));
                    }
                    else
                    {
                        IsRead = new BitmapImage(new Uri("/Assets/IsRead/yes.png", UriKind.Relative));
                    }
                    data.Add(new ThresholdWarningDataList() { Notification_ID = item.Notification_ID, IsRead = IsRead, TITLE_SPL = item.TITLE_SPL, STATION_NAME = "Trạm " + item.STATION_NAME, IconBitmapImage = new BitmapImage(new Uri(item.ICON)) });
                }
            }
            listBuilding.ItemsSource = data;
        }

        ProvinceEntity resultProvince = new ProvinceEntity();
        private async void LoadProvince()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LIST_PROVINCE);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultProvince = JsonConvert.DeserializeObject<ProvinceEntity>(resultContentString);
            }
            List<string> dataProvince = new List<string>();
            dataProvince.Add("Tỉnh/Thành phố");
            foreach (var item in resultProvince.Data)
            {
                dataProvince.Add(item.PROVINCE_NAME);
            }
            myProvinceList.ItemsSource = dataProvince;
        }

        StationEntity resultStationName = new StationEntity();
        private async void LoadStationName(string provinceNo)
        {
            var post = new PostStation()
            {
                PROVINCE_NO = provinceNo,
                IS_ACTIVE = 1,
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
                resultStationName = JsonConvert.DeserializeObject<StationEntity>(resultContentString);
            }
            List<string> dataStationName = new List<string>();
            dataStationName.Add("Tên trạm");
            foreach (var item in resultStationName.Data.list)
            {
                dataStationName.Add(item.STATION_NAME);
            }
            myStationNameList.ItemsSource = dataStationName;
        }

        private void myProvinceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string provinceNo = "";
            if (myProvinceList.SelectedIndex > 0)
            {
                provinceNo = resultProvince.Data.Where(p => p.PROVINCE_NAME == myProvinceList.SelectedItem).FirstOrDefault().PROVINCE_NO;
                LoadStationName(provinceNo);
            }
            else
            {
                List<string> dataStationName = new List<string>();
                dataStationName.Add("Tên trạm");
                myStationNameList.ItemsSource = dataStationName;
            }
        }

        private void Search()
        {
            string provinceNo = "";
            int stationId = 0;
            if (myProvinceList.SelectedIndex > 0)
            {
                provinceNo = resultProvince.Data.Where(p => p.PROVINCE_NAME == myProvinceList.SelectedItem).FirstOrDefault().PROVINCE_NO.ToString();
            }
            if (myStationNameList.SelectedIndex > 0)
            {
                stationId = int.Parse(resultStationName.Data.list.Where(p => p.STATION_NAME == myStationNameList.SelectedItem).FirstOrDefault().STATION_ID);
            }
            string from = "";
            string to = "";
            if (ToDatePicker.Value != null && ToTimePicker.Value != null&&FromDatePicker.Value != null && FromTimePicker.Value != null)
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
                    LoadThresholdWarning(stationId, provinceNo, from, to);
                }
            }
            else
            {
                FromDatePicker.Value = null;
                FromTimePicker.Value = null;
                ToDatePicker.Value = null;
                ToTimePicker.Value = null;
                LoadThresholdWarning(stationId, provinceNo, from, to);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            myProvinceList.SelectedIndex = 0;
            myStationNameList.SelectedIndex = 0;
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
        private void btnWarningDetail_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/WarningInfoPage.xaml?ID=" + tag, UriKind.Relative));
        }

    }
}