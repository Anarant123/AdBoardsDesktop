using AdBoardsDesktop.Models.db;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminAdPage.xaml
    /// </summary>
    public partial class AdminAdPage : Page
    {
        public AdminAdPage()
        {
            InitializeComponent();

            //tbName.Text = Context.AdNow.Name;
            //tbDescription.Text = Context.AdNow.Description;
            //tbPhone.Text = Context.AdNow.Person.Phone;
            //tbCity.Text = Context.AdNow.City;
            //tbPrice.Text = Context.AdNow.Price.ToString();
            //imgAd.Source = LoadImage.Load(Context.AdNow.Photo);
            //imgSalesman.Source = LoadImage.Load(Context.AdNow.Person.Photo);
            //lblSalesman.Text = Context.AdNow.Person.Name;
        }

        private async void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:5228/Ads/Delete?id={Context.AdNow.Id}");
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                this.NavigationService.Navigate(new Uri("Views/ComplainPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Что то пошло не так...");
            }
        }

        private async void btnWithdrawTheComplaint_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:5228/Complaint/Delete?AdId={Context.AdNow.Id}");
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Жалоба снята");
                this.NavigationService.Navigate(new Uri("Views/ComplainPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Что то пошло не так...");
            }
        }
    }
}
