using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using Windows.Phone.Media.Capture;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MHSM_PhoneApp.Entities;
using System.Text.RegularExpressions;

namespace MHSM_PhoneApp
{
    public partial class SendAlertPage : PhoneApplicationPage
    {

        public SendAlertPage()
        {
            InitializeComponent();
            btnSendAlert1.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadNotiType();
        }

        public static bool IsEmail(string email)
        {
            string strRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(strRegex);
            return regex.IsMatch(email);
        }

        public static bool IsPhone(string mobile)
        {
            string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            return Regex.IsMatch(mobile, motif);
        }

        private async void LoadNotiType()
        {
            NotiTypeEntity resultContent = new NotiTypeEntity();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                var result = await client.GetAsync(ConfigClass.LIST_NOTI_TYPE);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                resultContent = JsonConvert.DeserializeObject<NotiTypeEntity>(resultContentString);
            }
            myList.ItemsSource = resultContent.Data;
        }

        private async void btnStartSendAlert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else if (IsEmail(txtEmail.Text) == false || IsPhone(txtPhone.Text) == false)
            {
                MessageBox.Show("Email hoặc số điện thoại không đúng định dạng");
            }
            else
            {
                var NotiType = (myList.SelectedIndex + 1).ToString();
                var post = new FormUrlEncodedContent(new[]
{
    new KeyValuePair<string, string>("data.USERNAME", txtName.Text), 
    new KeyValuePair<string, string>("data.PHONENUMBER", txtPhone.Text),
    new KeyValuePair<string, string>("data.EMAIL", txtEmail.Text),
    new KeyValuePair<string, string>("data.TYPE", NotiType),
    new KeyValuePair<string, string>("data.CONTENT", txtContent.Text)
});
                ResponseModel resultContent = new ResponseModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigClass.DOMAIN);
                    var result = await client.PostAsync(ConfigClass.SEND_WARNING, post);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    resultContent = JsonConvert.DeserializeObject<ResponseModel>(resultContentString);
                }
                if (resultContent.Success)
                {
                    MessageBox.Show("Gửi cảnh báo thành công");
                     txtName.Text="";
                    txtPhone.Text="";
                    txtEmail.Text="";
                    myList.SelectedIndex = 0;
                    txtContent.Text="";
                }
                else
                {
                    MessageBox.Show("Gửi cảnh báo không thành công");
                }
            }
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Completed += Camera_Complete;
            camera.Show();
        }

        private void Camera_Complete(Object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                image.Source = bmp;
            }
        }

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -100)
            {
                MoveViewWindow(-420);
            }
            else
            {
                MoveViewWindow(0);
            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = left;
            ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(LayoutRoot, Math.Min(Math.Max(-840, Canvas.GetLeft(LayoutRoot) + e.DeltaManipulation.Translation.X), 0));
        }

        double initialPosition;
        bool _viewMoved = false;
        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(LayoutRoot);
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (_viewMoved)
                return;
            if (Math.Abs(initialPosition - left) < 100)
            {
                //bouncing back
                MoveViewWindow(initialPosition);
                return;
            }
            //change of state
            if (initialPosition - left > 0)
            {
                //slide to the left
                if (initialPosition > -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(-840);
            }
            else
            {
                //slide to the right
                if (initialPosition < -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(0);
            }

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
        }

        private void btnWeatherInformation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WeatherInformationPage.xaml", UriKind.Relative));
        }

        private void btnSendAlert1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SendAlertPage.xaml", UriKind.Relative));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
        }

    }
}