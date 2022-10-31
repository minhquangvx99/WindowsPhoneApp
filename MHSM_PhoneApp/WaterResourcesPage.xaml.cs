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
    public partial class WaterResourcesPage : PhoneApplicationPage
    {
        public WaterResourcesPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            LoadStationWaterResources("","","");
        }

        private async void LoadStationWaterResources(string stationName, string sizeNo, string provinceNo)
        {
            var post = new
            {
                STATION_NAME = stationName,
                SITE_NO = sizeNo,
                PROVINCE_NO = provinceNo,
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
            var data = new List<DataForScreen>();
            foreach (var item in resultContent.Data)
            {
                string[] dateTime = item.DATE_TIME.Split(' ');
                data.Add(new DataForScreen() { STATION_NAME = item.STATION_NAME, PROVINCE_NAME = item.PROVINCE_NAME, DAY = dateTime[1], HOUR = dateTime[0] });
            }
            listBuilding.ItemsSource = data;
        }

        SizeEntity resultSize = new SizeEntity();
        private async void LoadSize()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LIST_SITE);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultSize = JsonConvert.DeserializeObject<SizeEntity>(resultContentString);
            }
            List<string> dataSize = new List<string>();
            dataSize.Add("Khu vực");
            foreach (var item in resultSize.Data)
            {
                dataSize.Add(item.SITE_NAME);
            }
            mySizeList.ItemsSource = dataSize;
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

        private void Search()
        {
            string provinceNo = "";
            string siteNo = "";
            if (mySizeList.SelectedIndex >= 0 && mySizeList.SelectedItem != "Khu vực")
            {
                siteNo = resultSize.Data.Where(p => p.SITE_NAME == mySizeList.SelectedItem).FirstOrDefault().SITE_NO;
            }
            if (myProvinceList.SelectedIndex >= 0 && myProvinceList.SelectedItem != "Tỉnh/Thành phố")
            {
                provinceNo = resultProvince.Data.Where(p => p.PROVINCE_NAME == myProvinceList.SelectedItem).FirstOrDefault().PROVINCE_NO;
            }
            LoadStationWaterResources(txtStationName.Text, siteNo, provinceNo);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnRefresh(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            txtStationName.Text = "";
            myProvinceList.SelectedIndex = 0;
            mySizeList.SelectedIndex = 0;
        }

        private void mySizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mySizeList.SelectedIndex > 0)
            {

                List<ProvinceData> province = resultProvince.Data.Where(p => p.SITE_NO == resultSize.Data.Where(s => s.SITE_NAME == mySizeList.SelectedItem).FirstOrDefault().SITE_NO).ToList();
                List<string> dataProvince = new List<string>();
                dataProvince.Add("Tỉnh/Thành phố");
                foreach (var item in province)
                {
                    dataProvince.Add(item.PROVINCE_NAME);
                }
                myProvinceList.ItemsSource = dataProvince;
            }
            else
            {
                LoadProvince();
            }

        }
        private void myProvinceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myProvinceList.SelectedIndex > 0)
            {
                SizeData size = resultSize.Data.Where(s => s.SITE_NO == resultProvince.Data.Where(p => p.PROVINCE_NAME == myProvinceList.SelectedItem).FirstOrDefault().SITE_NO).FirstOrDefault();
                List<string> dataSize = new List<string>();
                dataSize.Add("Khu vực");
                dataSize.Add(size.SITE_NAME);
                mySizeList.ItemsSource = dataSize;
            }
            else
            {
                LoadSize();
            }
        }

        private void btnDetailStationWaterResources_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/WaterResourcesDetailPage.xaml?StationName=" + tag, UriKind.Relative));
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