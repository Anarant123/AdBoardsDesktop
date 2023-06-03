using AdBoardsDesktop.Models.db;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для StatsPage.xaml
    /// </summary>
    public partial class StatsPage : Page
    {
        public StatsPage()
        {
            InitializeComponent();

            getCountOfPerson();
            getAds();
        }

        private async void getCountOfPerson()
        {
            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5228/People/GetCountOfClient");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                lblCountOfClient.Text = $"Всего пользователей в системе: {responseContent}";
            }
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
                    IgnoreNullValues = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);
                lblCountOfAd.Text += " " + Context.AdList.Ads.Count;
                lblCountOfC1.Text += " " + Context.AdList.Ads.Where(x => x.CotegorysId == 1).Count();
                lblCountOfC2.Text += " " + Context.AdList.Ads.Where(x => x.CotegorysId == 2).Count();
                lblCountOfC3.Text += " " + Context.AdList.Ads.Where(x => x.CotegorysId == 3).Count();
                lblCountOfC4.Text += " " + Context.AdList.Ads.Where(x => x.CotegorysId == 4).Count();
                lblCountOfC5.Text += " " + Context.AdList.Ads.Where(x => x.CotegorysId == 5).Count();
                lblCountOfT1.Text += " " + Context.AdList.Ads.Where(x => x.TypeOfAdId == 1).Count();
                lblCountOfT2.Text += " " + Context.AdList.Ads.Where(x => x.TypeOfAdId == 2).Count();
            }
        }
    }
}
