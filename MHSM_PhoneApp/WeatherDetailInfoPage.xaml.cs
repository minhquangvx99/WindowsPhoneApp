using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using MHSM_PhoneApp.Entities;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Globalization;

namespace MHSM_PhoneApp
{
    public partial class WeatherDetailInfoPage : PhoneApplicationPage
    {
        public WeatherDetailInfoPage()
        {
            InitializeComponent();
            if (ConfigClass.UserData.user != null)
            {
                avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadWeatherDetail();
        }

        private async void LoadWeatherDetail()
        {
            dynamic resultContent;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN_WEATHER);
                var result = await client.GetAsync(ConfigClass.WEATHER_DETAIL);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<Object>(resultContentString);
            }
            var rs = resultContent.foreCastData;
            var list = JsonConvert.DeserializeObject<Dictionary<string, WeatherDetailData>>(rs.ToString());
            Dictionary<string, List<WeatherDetailData>> weatherDetailData = new Dictionary<string, List<WeatherDetailData>>();
            List<WeatherDetailData> weatherDetailDataList = new List<WeatherDetailData>();
            string key = "key";
            foreach (var item in list)
            {
                //DateTime date = DateTime.Parse(item.Key);

                string[] dateStrArr = item.Key.Split(' ');
                string dateStr = "";
                string[] d = dateStrArr[0].Split('/');
                string dt = "";
                if (int.Parse(d[0]) < 10)
                {
                    dt += "0" + d[0];
                }
                else
                {
                    dt += d[0];
                }
                dt += "/";
                if (int.Parse(d[1]) < 10)
                {
                    dt += "0" + d[1];
                }
                else
                {
                    dt += d[1];
                }
                dt += "/" +d[2];
                if (dateStrArr[2] == "PM")
                {
                    dateStr = dt + (int.Parse(dateStrArr[1].Substring(0, 1)) + 12).ToString() + dateStrArr[1].Substring(1);
                }
                else
                {
                    dateStr = dt + "0" + dateStrArr[1];
                }
                DateTime date = DateTime.ParseExact(dateStr, "MM/dd/yyyyHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                if (date.ToString("HH") == "01")
                {
                    date = date.AddDays(-1);
                }
                string dateString = date.ToString("dd/MM/yyyy");
                string dayOfWeek = date.DayOfWeek.ToString();
                string keyNew = "";
                if (dayOfWeek == "Monday")
                {
                    keyNew = "Thứ Hai" + ", " + dateString;
                }
                else if (dayOfWeek == "Tuesday")
                {
                    keyNew = "Thứ Ba" + ", " + dateString;
                }
                else if (dayOfWeek == "Wednesday")
                {
                    keyNew = "Thứ Tư" + ", " + dateString;
                }
                else if (dayOfWeek == "Thursday")
                {
                    keyNew = "Thứ Năm" + ", " + dateString;
                }
                else if (dayOfWeek == "Friday")
                {
                    keyNew = "Thứ Sáu" + ", " + dateString;
                }
                else if (dayOfWeek == "Saturday")
                {
                    keyNew = "Thứ Bảy" + ", " + dateString;
                }
                else if (dayOfWeek == "Sunday")
                {
                    keyNew = "Chủ Nhật" + ", " + dateString;
                }
                if (keyNew != key)
                {
                    if (weatherDetailDataList.Count > 0)
                    {
                        weatherDetailData.Add(key, weatherDetailDataList);
                    }
                    weatherDetailDataList = new List<WeatherDetailData>();
                    weatherDetailDataList.Add(item.Value);
                    key = keyNew;
                }
                else
                {
                    weatherDetailDataList.Add(item.Value);
                }
            }
            weatherDetailData.Add(key, weatherDetailDataList);
            Dictionary<string, List<WeatherDetailData>> weatherDetailDataForScreen = new Dictionary<string, List<WeatherDetailData>>();
            List<WeatherDetailData> weatherDetailDataListForScreen = new List<WeatherDetailData>();
            foreach (var item in weatherDetailData)
            {
                weatherDetailDataListForScreen = new List<WeatherDetailData>();
                foreach (var item1 in item.Value)
                {
                    string checkSession = item1.dateForeCast.ToString("HH");
                    if (checkSession == "01")
                    {
                        item1.sessionOfDay = "Đêm";
                    }
                    else if (checkSession == "07")
                    {
                        item1.sessionOfDay = "Sáng";
                    }
                    else if (checkSession == "13")
                    {
                        item1.sessionOfDay = "Chiều";
                    }
                    else if (checkSession == "19")
                    {
                        item1.sessionOfDay = "Tối";
                    }
                    item1.relativeHumidity = "Độ ẩm: " + Math.Round(double.Parse(item1.relativeHumidity), 2).ToString() + "%";
                    item1.windDirection = "Gió: " + item1.windDirection + " " + item1.windSpeed + " Km/h";
                    item1.pressure = "Áp suất: " + item1.pressure + " hPa";
                    item1.temperature = item1.temperature + "℃";
                    item1.dewPoint = "Điểm sương: " + item1.dewPoint + "℃";
                    item1.icon.iconWeather = new BitmapImage(new Uri(item1.icon.iconLink));
                    item1.temperatureMin = item1.temperatureMin + "°/" + item1.temperatureMax + "°";
                    weatherDetailDataListForScreen.Add(item1);
                }
                weatherDetailDataForScreen.Add(item.Key, weatherDetailDataListForScreen);
            }
            listBuilding.ItemsSource = weatherDetailDataForScreen;
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