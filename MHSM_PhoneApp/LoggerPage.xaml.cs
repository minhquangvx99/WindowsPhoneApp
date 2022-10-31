using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MHSM_PhoneApp.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp
{
    public partial class LoggerPage : PhoneApplicationPage
    {
        public LoggerPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string Id = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("Id", out Id))
            {
                LoadSensorDetail(int.Parse(Id));
            }
        }

        private async void LoadSensorDetail(int Id)
        {
            LoggerEntity resultContent = new LoggerEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                var result = await client.GetAsync(ConfigClass.LOGGER_DETAILS +"?id=" +Id);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<LoggerEntity>(resultContentString);
            }
            var data = new List<LoggerData>();
            if (resultContent.Data.STATUS == "1")
            {
                resultContent.Data.STATUS = "Hoạt động";
            }
            else
            {
                resultContent.Data.STATUS = "Không hoạt động";
            }
            data.Add(resultContent.Data);
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