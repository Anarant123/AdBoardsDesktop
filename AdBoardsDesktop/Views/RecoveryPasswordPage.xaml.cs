using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для RecoveryPasswordPage.xaml
    /// </summary>
    public partial class RecoveryPasswordPage : Page
    {
        public RecoveryPasswordPage()
        {
            InitializeComponent();
        }

        private async void btnRecover_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/People/RecoveryPassword?Login={tbLogin.Text}");
            var response = await httpClient.SendAsync(request);

            this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
        }
    }
}
