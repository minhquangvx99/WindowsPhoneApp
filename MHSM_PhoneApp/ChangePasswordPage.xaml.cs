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
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MHSM_PhoneApp.Entities;
using System.Text.RegularExpressions;

namespace MHSM_PhoneApp
{
    public partial class ChangePasswordPage : PhoneApplicationPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
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

        public static bool IsValidPassword(string plainText)
        {
            Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[0-9]).{6,}$");
            Match match = regex.Match(plainText);
            return match.Success;
        }

        private async void btnConfirmChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string OldPassword;
            if (showOldPassword.IsChecked == true)
            {
                OldPassword = oldPasswordTxtBox.Text;
            }
            else
            {
                OldPassword = oldPasswordBox.Password;
            }
            string NewPassword;
            if (showNewPassword.IsChecked == true)
            {
                NewPassword = newPasswordTxtBox.Text;
            }
            else
            {
                NewPassword = newPasswordBox.Password;
            }
            string NewPasswordAgain;

            if (showNewPasswordAgain.IsChecked == true)
            {
                NewPasswordAgain = newPasswordAgainTxtBox.Text;
            }
            else
            {
                NewPasswordAgain = newPasswordAgainBox.Password;
            }
            if (string.IsNullOrWhiteSpace(OldPassword))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ");
            }
            else if (string.IsNullOrWhiteSpace(NewPassword))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới");
            }
            else if (string.IsNullOrWhiteSpace(NewPasswordAgain))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới");
            }
            else if (NewPassword != NewPasswordAgain)
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp");
            }
            else if (IsValidPassword(OldPassword) == false || IsValidPassword(NewPassword) == false || IsValidPassword(NewPasswordAgain) == false)
            {
                MessageBox.Show("Độ dài mật khẩu không được ít hơn 6 kí tự; phải bao gồm chữ hoa, chữ thường và số");
            }
            else
            {
                var post = new
                {
                    Username = ConfigClass.UserData.user.Username,
                    Password = OldPassword,
                    NewPassWord = NewPassword
                };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                    var result = await client.PostAsJsonAsync(ConfigClass.CHANGE_PASSWORD, post);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    var resultContent = JsonConvert.DeserializeObject<ResponseModel>(resultContentString);
                    if (resultContent.Success == true)
                    {
                        MessageBox.Show(resultContent.Message);
                        //ConfigClass.UserData = resultContent.Data.Data;
                        NavigationService.Navigate(new Uri("/SettingAccountPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show(resultContent.Message);
                    }
                }
            }
        }

        private void ShowOldPassword_Checked(object sender, RoutedEventArgs e)
        {
            oldPasswordImage.Source = new BitmapImage(new Uri("/Assets/Eye/eye_no.png", UriKind.Relative));
            oldPasswordTxtBox.Text = oldPasswordBox.Password;
            oldPasswordBox.Visibility = Visibility.Collapsed;
            oldPasswordTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowOldPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            oldPasswordImage.Source = new BitmapImage(new Uri("/Assets/Eye/eye.png", UriKind.Relative));
            oldPasswordBox.Password = oldPasswordTxtBox.Text;
            oldPasswordTxtBox.Visibility = Visibility.Collapsed;
            oldPasswordBox.Visibility = Visibility.Visible;
        }

        private void ShowNewPassword_Checked(object sender, RoutedEventArgs e)
        {
            newPasswordImage.Source = new BitmapImage(new Uri("/Assets/Eye/eye_no.png", UriKind.Relative));
            newPasswordTxtBox.Text = newPasswordBox.Password;
            newPasswordBox.Visibility = Visibility.Collapsed;
            newPasswordTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowNewPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            newPasswordImage.Source = new BitmapImage(new Uri("/Assets/Eye/eye.png", UriKind.Relative));
            newPasswordBox.Password = newPasswordTxtBox.Text;
            newPasswordTxtBox.Visibility = Visibility.Collapsed;
            newPasswordBox.Visibility = Visibility.Visible;
        }

        private void ShowNewPasswordAgain_Checked(object sender, RoutedEventArgs e)
        {
            newPasswordAgainImage.Source = new BitmapImage(new Uri("/Assets/Eye/eye_no.png", UriKind.Relative));
            newPasswordAgainTxtBox.Text = newPasswordAgainBox.Password;
            newPasswordAgainBox.Visibility = Visibility.Collapsed;
            newPasswordAgainTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowNewPasswordAgain_Unchecked(object sender, RoutedEventArgs e)
        {
            newPasswordAgainImage.Source = new BitmapImage(new Uri("/Assets/Eye/eye.png", UriKind.Relative));
            newPasswordAgainBox.Password = newPasswordAgainTxtBox.Text;
            newPasswordAgainTxtBox.Visibility = Visibility.Collapsed;
            newPasswordAgainBox.Visibility = Visibility.Visible;
        }
    }
}