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
    public partial class WaterResourcesDataMiningDetailPage : PhoneApplicationPage
    {     
        string ST_ID = string.Empty;
        public WaterResourcesDataMiningDetailPage()
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
            if (NavigationContext.QueryString.TryGetValue("ST_ID", out ST_ID) && ToDatePicker.Value == null && ToTimePicker.Value == null && FromDatePicker.Value == null && FromTimePicker.Value == null)
            {
                Search();
            }
        }

        private async void LoadStationWaterResourcesDataMiningDetail(string ST_ID, long fromDate, long toDate)
        {
            var post = new
            {
                ST_ID = int.Parse(ST_ID),
                PROJECT_ID = ConfigClass.waterResource,
                FROM_DATE = fromDate,
                TO_DATE = toDate,
                PAGEINDEX = 1,
                PAGESIZE = 100
            };
            DataMiningDetailEntity resultContent = new DataMiningDetailEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.PostAsJsonAsync(ConfigClass.EXPLOIT_DATA, post);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<DataMiningDetailEntity>(resultContentString);
            }
            if (resultContent.Data.Count > 0)
            {
                txtTitle3.Text = "Tài nguyên nước";
            }
            else
            {
                txtTitle3.Text = "Không có dữ liệu";
            }
            listBuilding.ItemsSource = resultContent.Data;
        }

        private long Convert(DateTime dateTime)
        {
            string[] dT = dateTime.ToString("yyyy/MM/dd/HH/mm/ss").Split('/');
            return long.Parse(dT[0] + dT[1] + dT[2] + dT[3] + dT[4] + dT[5]);
        }
        private long Convert1(string dateTime)
        {
            string[] dT = dateTime.Split('/');
            return long.Parse(dT[0] + dT[1] + dT[2] + dT[3] + dT[4] + dT[5]);
        }

        private void Search()
        {
            DateTime dateTime = DateTime.Now;
            long from = Convert(dateTime.AddMinutes(-15));
            long to = Convert(dateTime);
            if (ToDatePicker.Value != null && ToTimePicker.Value != null && FromDatePicker.Value != null && FromTimePicker.Value != null)
            {
                from = Convert1(((DateTime)FromDatePicker.Value).ToString("yyyy/MM/dd") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"));
                to = Convert1(((DateTime)ToDatePicker.Value).ToString("yyyy/MM/dd") + "/" + ((DateTime)ToTimePicker.Value).ToString("HH/mm/ss"));
            }
            else
            {
                FromDatePicker.Value = null;
                FromTimePicker.Value = null;
                ToDatePicker.Value = null;
                ToTimePicker.Value = null;
            }
            LoadStationWaterResourcesDataMiningDetail(ST_ID, from, to);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
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

        private void FromDatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (FromDatePicker.Value != null && FromTimePicker.Value != null)
            {
                DateTime myDate = DateTime.ParseExact(((DateTime)FromDatePicker.Value).ToString("dd/MM/yyyy") + "/" + ((DateTime)FromTimePicker.Value).ToString("HH/mm/ss"), "dd/MM/yyyy/HH/mm/ss",
                                       System.Globalization.CultureInfo.InvariantCulture).AddMinutes(15);
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
                                       System.Globalization.CultureInfo.InvariantCulture).AddMinutes(15);
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
                                       System.Globalization.CultureInfo.InvariantCulture).AddMinutes(-15);
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
                                       System.Globalization.CultureInfo.InvariantCulture).AddMinutes(-15);
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
    }
}