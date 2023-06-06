using AdBoardsDesktop.Models.db;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AdPage.xaml
    /// </summary>
    public partial class AdPage : Page
    {
        bool isF = false;
        public AdPage()
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

        public AdPage(bool isFavorites)
        {
            InitializeComponent();

            //isF = isFavorites;
            //btnAddToFavorite.Content = "Удалить из избранного";

            //tbName.Text = Context.AdNow.Name;
            //tbDescription.Text = Context.AdNow.Description;
            //tbPhone.Text = Context.AdNow.Person.Phone;
            //tbCity.Text = Context.AdNow.City;
            //tbPrice.Text = Context.AdNow.Price.ToString();
            //imgAd.Source = LoadImage.Load(Context.AdNow.Photo);
            //imgSalesman.Source = LoadImage.Load(Context.AdNow.Person.Photo);
            //lblSalesman.Text = Context.AdNow.Person.Name;
        }

        private async void btnAddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            //if (isF)
            //{
            //    var httpClient = new HttpClient();
            //    using HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:5228/Favorites/Delete?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
            //    var jsonResponse = await response.Content.ReadAsStringAsync();

            //    this.NavigationService.Navigate(new Uri("Views/FavoritesAdsPage.xaml", UriKind.Relative));
            //}
            //else
            //{
            //    if (Context.UserNow == null)
            //    {
            //        this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
            //        return;
            //    }

            //    var httpClient = new HttpClient();
            //    var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/Favorites/Addition?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
            //    var response = await httpClient.SendAsync(request);


            //    if (response.IsSuccessStatusCode)
            //    {
            //        MessageBox.Show("Объявление добавленно в избранное");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Объявление уже в избранном");
            //    }
            //}
        }

        private async void btnToComplain_Click(object sender, RoutedEventArgs e)
        {
            //if (Context.UserNow == null)
            //{
            //    this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
            //    return;
            //}
            //var httpClient = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/Complaint/Addition?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
            //var response = await httpClient.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    MessageBox.Show("Жалоба успешно отправленна");
            //}
            //else
            //{
            //    MessageBox.Show("Вы уже пожаловались");
            //}
        }
    }
}
