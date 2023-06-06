using AdBoardsDesktop.Models.db;
using AdBoards.ApiClient;
using AdBoards.ApiClient.Extensions;
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
            Context.UserNow = await Context.Api.Authorize(tbLogin.Text, tbPassword.Password);
            if (Context.UserNow == null)
            {
                MessageBox.Show("Что то пошло не так!");
                return;
            }

            Context.Api.Jwt = Context.UserNow.Token;

            if (Context.UserNow.Person.Right.Id == 2)
                this.NavigationService.Navigate(new Uri("Views/ProfilePage.xaml", UriKind.Relative));
            else
            {
                Window w = Window.GetWindow(this);
                w.Hide();
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/RecoveryPasswordPage.xaml", UriKind.Relative));
        }
    }
}
