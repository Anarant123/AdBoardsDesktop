using AdBoardsDesktop.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ComplainPage.xaml
    /// </summary>
    public partial class ComplainPage : Page
    {
        public ComplainPage()
        {
            InitializeComponent();

            getAds();
        }

        private void lvAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Context.AdNow = new Ad();
            Context.AdNow = (lvAds.SelectedItem as Ad);

            this.NavigationService.Navigate(new Uri("Views/AdminAdPage.xaml", UriKind.Relative));
        }

        private async void getAds()
        {
            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5228/Ads/GetAds");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Context.AdList = new AdListViewModel();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);
                Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Complaints.Count > 0).ToList();
                lvAds.ItemsSource = Context.AdList.Ads.ToList();
            }
        }
    }
}
