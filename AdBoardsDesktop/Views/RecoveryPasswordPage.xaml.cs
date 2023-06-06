using AdBoardsDesktop.Models.db;
using AdBoards.ApiClient.Contracts.Responses;
using AdBoards.ApiClient.Extensions;
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
            Context.Api.Recover(tbLogin.Text);

            this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
        }
    }
}
