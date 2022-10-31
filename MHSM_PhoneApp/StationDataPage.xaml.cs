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
    public partial class StationDataPage : PhoneApplicationPage
    {
        public StationDataPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            FromDatePicker.Value = null;
            FromTimePicker.Value = null;
            ToDatePicker.Value = null;
            ToTimePicker.Value = null;
            LoadElementList();
            List<string> dataValueTypeList = new List<string>();
            dataValueTypeList.Add("Giá trị đo");
            myValueList.ItemsSource = dataValueTypeList;
        }

        private long Convert(string dateTime)
        {
            string[] dT = dateTime.Split('/');
            return long.Parse(dT[0] + dT[1] + dT[2] + dT[3] + dT[4] + dT[5]);
        }

        private async void LoadDataStation(string stationName, string elementId, string vtypeId, long fromDate, long toDate)
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
            List<StationDataDetail> data = new List<StationDataDetail>();
            if (resultContent.Data.Count > 0)
            {
                txtTitle3.Text = "Dữ liệu truyền về " + DateTime.ParseExact(fromDate.ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy") + " đến " + DateTime.ParseExact(toDate.ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                foreach (var item in resultContent.Data)
                {
                    data.Add(new StationDataDetail() { STATION_NAME = item.STATION_NAME + " " + item.PROVINCE_NAME, VTYPE_NAME = item.VTYPE_NAME, VALUE = item.VALUE, SYMBOL = item.SYMBOL, PROVINCE_NAME = stationName + "/" + elementId + "/" + vtypeId + "/" + fromDate.ToString() + "/" + toDate.ToString() + "/" + item.VTYPE_NAME + "/" + item.TIMESTAMP.ToString("yyyyMMddHHmmss") });
                }
            }
            else
            {
                txtTitle3.Text = "Không có dữ liệu";
            }
            listBuilding.ItemsSource = data;
        }

        ElementListEntity resultElementList = new ElementListEntity();
        private async void LoadElementList()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LIST_MEASURE_ELEMENT);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultElementList = JsonConvert.DeserializeObject<ElementListEntity>(resultContentString);
            }
            List<string> dataElementList = new List<string>();
            dataElementList.Add("Yếu tố đo");
            foreach (var item in resultElementList.Data)
            {
                dataElementList.Add(item.ELEMENT_NAME);
            }
            myElementList.ItemsSource = dataElementList;
        }

        ValueTypeListEntity resultValueTypeList = new ValueTypeListEntity();
        private async void LoadValueTypeList(string elementId)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.VTYPE_LIST_BY_ELEMENT + elementId);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultValueTypeList = JsonConvert.DeserializeObject<ValueTypeListEntity>(resultContentString);
            }
            List<string> dataValueTypeList = new List<string>();
            dataValueTypeList.Add("Giá trị đo");
            foreach (var item in resultValueTypeList.Data)
            {
                dataValueTypeList.Add(item.VTYPE_NAME);
            }
            myValueList.ItemsSource = dataValueTypeList;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string elementId = "";
            string vtypeId = "";
            if (myElementList.SelectedIndex > 0)
            {
                elementId = resultElementList.Data.Where(p => p.ELEMENT_NAME == myElementList.SelectedItem).FirstOrDefault().ELEMENT_ID;
                if (myValueList.SelectedIndex > 0)
                {
                    vtypeId = resultValueTypeList.Data.Where(p => p.VTYPE_NAME == myValueList.SelectedItem).FirstOrDefault().VTYPE_ID;
                }
                DateTime dateTime = DateTime.Now;
                long fromDate;
                long toDate;
                if (ToDatePicker.Value != null && ToTimePicker.Value != null && FromDatePicker.Value != null && FromTimePicker.Value != null)
                {
                    fromDate = Convert(((DateTime)FromDatePicker.Value).ToString("yyyy/MM/dd") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"));
                    toDate = Convert(((DateTime)ToDatePicker.Value).ToString("yyyy/MM/dd") + "/" + ((DateTime)ToTimePicker.Value).ToString("HH/mm/ss"));
                    LoadDataStation(txtStationName.Text, elementId, vtypeId, fromDate, toDate);
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn đủ thời gian bắt đầu và thời gian kết thúc");
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn yếu tố đo");
            }
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            txtStationName.Text = "";
            myElementList.SelectedIndex = 0;
            myValueList.SelectedIndex = 0;
            FromDatePicker.Value = null;
            FromTimePicker.Value = null;
            ToDatePicker.Value = null;
            ToTimePicker.Value = null;
        }

        private void myElementList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string elementId = "";
            if (myElementList.SelectedIndex > 0)
            {
                elementId = resultElementList.Data.Where(p => p.ELEMENT_NAME == myElementList.SelectedItem).FirstOrDefault().ELEMENT_ID;
                LoadValueTypeList(elementId);
            }
            else
            {
                List<string> dataValueTypeList = new List<string>();
                dataValueTypeList.Add("Giá trị đo");
                myValueList.ItemsSource = dataValueTypeList;
            }
        }

        private void btnDetailStationData_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/StationDataDetailPage.xaml?listData=" + tag, UriKind.Relative));
        }
    }
}