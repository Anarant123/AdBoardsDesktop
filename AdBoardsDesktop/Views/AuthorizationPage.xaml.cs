using AdBoardsDesktop.Models.db;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/RegistrationPage.xaml", UriKind.Relative));
        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/People/Authorization?login={tbLogin.Text}&password={tbPassword.Password}");
            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Person user = new Person();
                user = JsonSerializer.Deserialize<Person>(responseContent)!;

                Context.UserNow = user;

                if (user.RightId == 1)
                    this.NavigationService.Navigate(new Uri("Views/ProfilePage.xaml", UriKind.Relative));
                else
                {
                    Window w = Window.GetWindow(this);
                    w.Hide();
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }    
            }
            else
            {
                MessageBox.Show("Что то пошло не так!");
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/RecoveryPasswordPage.xaml", UriKind.Relative));
        }
    }
}
