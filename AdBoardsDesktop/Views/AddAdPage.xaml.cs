using AdBoardsDesktop.Models.db;
using AdBoardsDesktop.Models.DTO;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddAdPage.xaml
    /// </summary>
    public partial class AddAdPage : Page
    {
        AdDTO ad = new AdDTO();
        public AddAdPage()
        {
            InitializeComponent();
        }

        private async void btnAddPost_Click(object sender, RoutedEventArgs e)
        {
            ad.Name = tbAddingName.Text;
            ad.City = tbAddingCity.Text;
            ad.Date = DateTime.Now;
            ad.CotegorysId = cbAddingСategories.SelectedIndex + 1;
            ad.Description = tbAddingDescription.Text;
            ad.Price = Convert.ToInt32(tbAddingPrice.Text);
            if (rbBuy.IsChecked == true )
                ad.TypeOfAdId = 1;
            else
                ad.TypeOfAdId = 2;
            ad.PersonId = Context.UserNow.Id;
            if (ad.Photo == null)
                ad.Photo = File.ReadAllBytes("Resources\\drawable\\icon_image.png");

            var httpClient = new HttpClient();
            using StringContent jsonContent = new(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5228/Ads/Addition", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Ad a = JsonSerializer.Deserialize<Ad>(jsonResponse)!;
                Context.AdNow = a;

                MessageBox.Show("объявление успешно добавленно");
                this.NavigationService.Navigate(new Uri("Views/MyAdsPage.xaml", UriKind.Relative));
            }
            else
                MessageBox.Show("Что то пошло не так! \nОбъявление добавить не удалось...");
        }

        private void btnToBackFrame_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/AdsPage.xaml", UriKind.Relative));
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.* ";
            if (o.ShowDialog() == true)
            {
                try
                {
                    ad.Photo = File.ReadAllBytes(o.FileName);
                    imgAd.Source = LoadImage.Load(ad.Photo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
