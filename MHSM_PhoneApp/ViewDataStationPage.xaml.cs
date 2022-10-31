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
using System.Net.Http;
using Newtonsoft.Json;
using MHSM_PhoneApp.Entities;
using System.Net.Http.Headers;

namespace MHSM_PhoneApp
{
    public partial class ViewDataStationPage : PhoneApplicationPage
    {
        string StationId = string.Empty;
        public ViewDataStationPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            FromDatePicker.Value = null;
            FromTimePicker.Value = null;
            ToDatePicker.Value = null;
            ToTimePicker.Value = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("StationId", out StationId) && ToDatePicker.Value == null && ToTimePicker.Value == null && FromDatePicker.Value == null && FromTimePicker.Value == null)
            {
                LoadConfig();
                Search();
            }
        }
        private async void LoadStationData(string StationId, string elementId, string valuetypeId, string startDate, string endDate)
        {
            StationDataEntity resultContent = new StationDataEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.VTYPE_LIST + "?stationId=" + StationId + "&elementID=" + elementId + "&valuetypeID=" + valuetypeId + "&startDate=" + startDate + "&endTime=" + endDate + "&pageNumber=1&pageSize=100", "");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<StationDataEntity>(resultContentString);
            }
            var data = new List<ListDataStation>();
            foreach (var item in resultContent.Data.list)
            {
                data.Add(new ListDataStation() { VTYPE_NAME = item.VTYPE_NAME, TIMESTAMP_STRING = item.TIMESTAMP.ToString("dd/MM/yyyy HH:mm"), Value = item.Value, UnitName = item.UnitName, DataSave = item.ELEMENT_ID.ToString() + "," + item.VTYPE_ID.ToString() + "," + item.TIMESTAMP.ToString("dd/MM/yyyy HH:mm:ss") });
            }
            if (data.Count > 0)
            {
                txtTitle.Text = "Dữ liệu truyền về " + DateTime.ParseExact(startDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy") + " đến " + DateTime.ParseExact(endDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
            }
            else
            {
                txtTitle.Text = "Không có dữ liệu";
            }
            listBuilding.ItemsSource = data;
        }

        FilterStationEntity resultContent = new FilterStationEntity();
        private async void LoadConfig()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.DATA_FILTER + "?stationid=" + StationId);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<FilterStationEntity>(resultContentString);
            }
            List<string> dataMeasure = new List<string>();
            dataMeasure.Add("Yếu tố đo");
            foreach (var item in resultContent.Data.list_measure)
            {
                dataMeasure.Add(item.ELEMENT_NAME);
            }
            myElementList.ItemsSource = dataMeasure;
            List<string> dataValueType = new List<string>();
            dataValueType.Add("Giá trị đo");
            foreach (var item in resultContent.Data.list_valuetype)
            {
                dataValueType.Add(item.VTYPE_NAME);
            }
            myValueList.ItemsSource = dataValueType;
        }

        private void Search()
        {
            string elementId = "";
            string valuetypeId = "";
            if (myElementList.SelectedIndex > 0)
            {
                elementId = resultContent.Data.list_measure.Where(p => p.ELEMENT_NAME == myElementList.SelectedItem).FirstOrDefault().ELEMENT_ID.ToString();
            }
            if (myValueList.SelectedIndex > 0)
            {
                valuetypeId = resultContent.Data.list_valuetype.Where(p => p.VTYPE_NAME == myValueList.SelectedItem).FirstOrDefault().VTYPE_ID.ToString();
            }
            DateTime now = DateTime.Now;
            string from = now.AddDays(-10).ToString("dd/MM/yyyy HH:mm:ss");
            string to = now.ToString("dd/MM/yyyy HH:mm:ss");
            if (ToDatePicker.Value != null && ToTimePicker.Value != null && FromDatePicker.Value != null && FromTimePicker.Value != null)
            {
                from = ((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy") + " " + ((DateTime)FromTimePicker.Value).ToString("HH:mm:ss");
                to = ((DateTime)ToDatePicker.Value).ToString("dd/MM/yyyy") + " " + ((DateTime)ToTimePicker.Value).ToString("HH:mm:ss");
            }
            else
            {
                FromDatePicker.Value = null;
                FromTimePicker.Value = null;
                ToDatePicker.Value = null;
                ToTimePicker.Value = null;
            }
            LoadStationData(StationId, elementId, valuetypeId, from, to);
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            myElementList.SelectedIndex = 0;
            myValueList.SelectedIndex = 0;
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

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            NavigationService.Navigate(new Uri("/StationDetailDataPage.xaml?DataSave=" + tag + "&StationId=" + StationId, UriKind.Relative));
        }

        private void FromDatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (FromDatePicker.Value != null && FromTimePicker.Value != null)
            {
                DateTime myDate = DateTime.ParseExact(((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                       System.Globalization.CultureInfo.InvariantCulture).AddDays(10);
                if (ToDatePicker.Value != null && ToTimePicker.Value != null)
                {
                    if (myDate != DateTime.ParseExact(((DateTime)ToDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)ToTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss", System.Globalization.CultureInfo.InvariantCulture))
                    {
                        ToTimePicker.Value = null;
                        ToDatePicker.Value = myDate;
                        ToTimePicker.Value = myDate;
                    }
                }
                else
                {
                    ToTimePicker.Value = null;
                    ToDatePicker.Value = myDate;
                    ToTimePicker.Value = myDate;
                }
            }
        }

        private void FromTimePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (FromDatePicker.Value != null && FromTimePicker.Value != null)
            {
                DateTime myDate = DateTime.ParseExact(((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                       System.Globalization.CultureInfo.InvariantCulture).AddDays(10);
                if (ToDatePicker.Value != null && ToTimePicker.Value != null)
                {
                    if (myDate != DateTime.ParseExact(((DateTime)ToDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)ToTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss", System.Globalization.CultureInfo.InvariantCulture))
                    {
                        ToTimePicker.Value = null;
                        ToDatePicker.Value = myDate;
                        ToTimePicker.Value = myDate;
                    }
                }
                else
                {
                    ToTimePicker.Value = null;
                    ToDatePicker.Value = myDate;
                    ToTimePicker.Value = myDate;
                }
            }
        }

        private void ToDatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (ToDatePicker.Value != null && ToTimePicker.Value != null)
            {
                DateTime myDate = DateTime.ParseExact(((DateTime)ToDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)ToTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                       System.Globalization.CultureInfo.InvariantCulture).AddDays(-10);
                if (FromDatePicker.Value != null && FromTimePicker.Value != null)
                {
                    if (myDate != DateTime.ParseExact(((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                           System.Globalization.CultureInfo.InvariantCulture))
                    {
                        FromTimePicker.Value = null;
                        FromDatePicker.Value = myDate;
                        FromTimePicker.Value = myDate;
                    }
                }
                else
                {
                    FromTimePicker.Value = null;
                    FromDatePicker.Value = myDate;
                    FromTimePicker.Value = myDate;
                }
            }
        }

        private void ToTimePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (ToDatePicker.Value != null && ToTimePicker.Value != null)
            {
                DateTime myDate = DateTime.ParseExact(((DateTime)ToDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)ToTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                       System.Globalization.CultureInfo.InvariantCulture).AddDays(-10);
                if (FromDatePicker.Value != null && FromTimePicker.Value != null)
                {
                    if (myDate != DateTime.ParseExact(((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                           System.Globalization.CultureInfo.InvariantCulture))
                    {
                        FromTimePicker.Value = null;
                        FromDatePicker.Value = myDate;
                        FromTimePicker.Value = myDate;
                    }
                }
                else
                {
                    FromTimePicker.Value = null;
                    FromDatePicker.Value = myDate;
                    FromTimePicker.Value = myDate;
                }
            }
        }

        private void myElementList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myElementList.SelectedIndex > 0)
            {

                List<ValueTypeEntity> valueTypeList = resultContent.Data.list_valuetype.Where(v => v.ELEMENT_ID == resultContent.Data.list_measure.Where(m => m.ELEMENT_NAME == myElementList.SelectedItem).FirstOrDefault().ELEMENT_ID.ToString()).ToList();
                List<string> dataValueType = new List<string>();
                dataValueType.Add("Giá trị đo");
                foreach (var item in valueTypeList)
                {
                    dataValueType.Add(item.VTYPE_NAME);
                }
                myValueList.ItemsSource = dataValueType;
            }
            else
            {
                LoadConfig();
            }
        }

        private void myValueList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myValueList.SelectedIndex > 0)
            {

                List<MeasureEntity> elementList = resultContent.Data.list_measure.Where(m => m.ELEMENT_ID == resultContent.Data.list_valuetype.Where(v => v.VTYPE_NAME == myValueList.SelectedItem).FirstOrDefault().ELEMENT_ID.ToString()).ToList();
                List<string> dataMeasure = new List<string>();
                dataMeasure.Add("Yếu tố đo");
                foreach (var item in elementList)
                {
                    dataMeasure.Add(item.ELEMENT_NAME);
                }
                myElementList.ItemsSource = dataMeasure;
            }
            else
            {
                LoadConfig();
            }
        }
    }
}