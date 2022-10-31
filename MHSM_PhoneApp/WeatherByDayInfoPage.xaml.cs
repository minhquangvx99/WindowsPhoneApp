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

namespace MHSM_PhoneApp
{
    public partial class WeatherByDayInfoPage : PhoneApplicationPage
    {
        public WeatherByDayInfoPage()
        {
            InitializeComponent();
            if (ConfigClass.UserData.user != null)
            {
                avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadWeatherByDay();
        }

        private async void LoadWeatherByDay()
        {
            dynamic resultContent;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN_WEATHER);
                var result = await client.GetAsync(ConfigClass.WEATHER_BY_DAY);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<Object>(resultContentString);
            }
            var rs = resultContent.foreCastData;
            var list = JsonConvert.DeserializeObject<Dictionary<string, WeatherDetailData>>(rs.ToString());
            List<WeatherDetailData> weatherDetailDataList = new List<WeatherDetailData>();
            foreach (var item in list)
            {
                if (item.Value.dateForeCast.DayOfWeek.ToString() == "Monday")
                {
                    item.Value.dayOfWeek = "Thứ Hai";
                }
                else if (item.Value.dateForeCast.DayOfWeek.ToString() == "Tuesday")
                {
                    item.Value.dayOfWeek = "Thứ Ba";
                }
                else if (item.Value.dateForeCast.DayOfWeek.ToString() == "Wednesday")
                {
                    item.Value.dayOfWeek = "Thứ Tư";
                }
                else if (item.Value.dateForeCast.DayOfWeek.ToString() == "Thursday")
                {
                    item.Value.dayOfWeek = "Thứ Năm";
                }
                else if (item.Value.dateForeCast.DayOfWeek.ToString() == "Friday")
                {
                    item.Value.dayOfWeek = "Thứ Sáu";
                }
                else if (item.Value.dateForeCast.DayOfWeek.ToString() == "Saturday")
                {
                    item.Value.dayOfWeek = "Thứ Bảy";
                }
                else if (item.Value.dateForeCast.DayOfWeek.ToString() == "Sunday")
                {
                    item.Value.dayOfWeek = "Chủ Nhật";
                }
                item.Value.date = item.Value.dateForeCast.ToString("dd/MM/yyyy");
                item.Value.relativeHumidity = "Độ ẩm: " + Math.Round(double.Parse(item.Value.relativeHumidity), 2).ToString() + "%";
                item.Value.windDirection = "Gió: " + item.Value.windDirection + " " + item.Value.windSpeed + " Km/h";
                item.Value.pressure = "Áp suất: " + item.Value.pressure + " hPa";
                item.Value.temperature = item.Value.temperature + "℃";
                item.Value.dewPoint = "Điểm sương: " + item.Value.dewPoint + "℃";
                item.Value.icon.iconWeather = new BitmapImage(new Uri(item.Value.icon.iconLink));
                item.Value.temperatureMin = item.Value.temperatureMin + "°/" + item.Value.temperatureMax + "°";
                weatherDetailDataList.Add(item.Value);
            }
            listBuilding.ItemsSource = weatherDetailDataList;
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