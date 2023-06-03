using AdBoardsDesktop.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AdsPage.xaml
    /// </summary>
    public partial class AdsPage : Page
    {
        public AdsPage()
        {
            InitializeComponent();

            getAds();
        }

        private async void btnUseFilter_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            string responseContent;

            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5228/Ads/GetAds");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            responseContent = await response.Content.ReadAsStringAsync();
            result = response.IsSuccessStatusCode;

            if (result)
            {
                Context.AdList = new AdListViewModel();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);

                if (!string.IsNullOrEmpty(tbPriceFrom.Text))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Price >= Convert.ToInt32(tbPriceFrom.Text)).ToList();
                if (!string.IsNullOrEmpty(tbPriceUpTo.Text))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Price <= Convert.ToInt32(tbPriceUpTo.Text)).ToList();
                if (!string.IsNullOrEmpty(tbCity.Text))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.City == tbCity.Text).ToList();
                if (cbСategories.SelectedIndex != 0)
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.CotegorysId == cbСategories.SelectedIndex).ToList();
                if (Convert.ToBoolean(rbBuy.IsChecked))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.TypeOfAdId == 1).ToList();
                else if (Convert.ToBoolean(rbSell.IsChecked))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.TypeOfAdId == 2).ToList();

                lvAds.ItemsSource = Context.AdList.Ads;
            }
            else
            {
                MessageBox.Show("С данными фильтрами ничего не найдено");
            }
        }

        private void lvAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Context.AdNow = new Ad();
            Context.AdNow = (lvAds.SelectedItem as Ad);

            this.NavigationService.Navigate(new Uri("Views/AdPage.xaml", UriKind.Relative));
        }

        private void tbPriceFrom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; 
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
                lvAds.ItemsSource = Context.AdList.Ads.ToList();
            }
        }

        private void btnDropFilter_Click(object sender, RoutedEventArgs e)
        {
            tbCity.Text = "";
            tbPriceFrom.Text = "";
            tbPriceUpTo.Text = "";
            cbСategories.SelectedIndex = 0;
            getAds();
        }
    }
}
