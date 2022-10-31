using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MHSM_PhoneApp.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MHSM_PhoneApp
{
    public partial class LoginPage : PhoneApplicationPage
    {
        // Constructor
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string Password;
            if (showPassword.IsChecked == true)
            {
                Password = passwordTxtBox.Text;
            }
            else{
                Password = passwordBox.Password;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Không được để trống tên đăng nhập");
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Không được để trống mật khẩu");
            }
            else
            {
                var loginModel = new LoginModel()
                {
                    Username = txtUsername.Text,
                    Password = Password,
                };
                ResponseModel resultContent = new ResponseModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                    var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ConfigClass.LOGIN, content);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    resultContent = JsonConvert.DeserializeObject<ResponseModel>(resultContentString);
                }
                if (resultContent.Success == true)
                {
                    ConfigClass.UserData = resultContent.Data.Data;
                    NavigationService.Navigate(new Uri("/LoginHomePage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show(resultContent.Message);
                }
            }
        }

        private void btnLoginCus_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
        }
    }
}