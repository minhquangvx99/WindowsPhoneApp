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
using Microsoft.Phone.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MHSM_PhoneApp
{
    public partial class SettingAccountPage : PhoneApplicationPage
    {
        public SettingAccountPage()
        {
            InitializeComponent();
            avatarSmall.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            avatar.Source = new BitmapImage(new Uri(ConfigClass.UserData.user.Avatar));
            tbFullName.Text = ConfigClass.UserData.user.FullName;
            txtPhonenumber.Text = ConfigClass.UserData.user.Phone;
            txtEmail.Text = ConfigClass.UserData.user.Email;
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ChangePasswordPage.xaml", UriKind.Relative));
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Completed += Camera_Complete;
            camera.Show();
        }

        private async void Camera_Complete(Object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                avatar.Source = bmp;
                //var post = new
                //{
                //    file = bmp,
                //};
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigClass.UserData.token);
                //    var result = await client.PostAsJsonAsync(ConfigClass.UPDATE_AVATAR + "?userID=" + ConfigClass.UserData.user.UserID, post);
                //    result.EnsureSuccessStatusCode();
                //}
            }
        }
    }
}