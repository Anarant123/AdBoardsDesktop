using AdBoardsDesktop.Models.db;
using AdBoardsDesktop.Models.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для MyAdPage.xaml
    /// </summary>
    public partial class MyAdPage : Page
    {
        AdDTO ad = new AdDTO();
        public MyAdPage()
        {
            InitializeComponent();

            tbName.Text = Context.AdNow.Name;
            tbDescription.Text = Context.AdNow.Description;
            cbСategories.SelectedIndex = Context.AdNow.AdType.Id;
            tbCity.Text = Context.AdNow.City;
            tbPrice.Text = Context.AdNow.Price.ToString();
            imgAd.Source = new BitmapImage(new Uri(Context.AdNow.PhotoName));
            if (Context.AdNow.AdType.Id == 1)
                rbBuy.IsChecked = true;
            else
                rbSell.IsChecked = true;
        }

        private async void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //ad.Id = Context.AdNow.Id;
            //ad.Name = tbName.Text;
            //ad.Description = tbDescription.Text;
            //ad.CotegorysId = cbСategories.SelectedIndex + 1;
            //if (rbBuy.IsChecked == true)
            //    ad.TypeOfAdId = 1;
            //else if(rbSell.IsChecked == true)
            //    ad.TypeOfAdId = 2;
            //ad.Price = Convert.ToInt32(tbPrice.Text);
            //ad.City = tbCity.Text;
            //if (ad.Photo == null)
            //    ad.Photo = Context.AdNow.Photo;

            //var httpClient = new HttpClient();
            //using StringContent jsonContent = new(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json");
            //using HttpResponseMessage response = await httpClient.PutAsync("http://localhost:5228/Ads/Update", jsonContent);
            //using HttpResponseMessage r = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
            //var jsonResponse = await response.Content.ReadAsStringAsync();
            //var jsonResponser = await r.Content.ReadAsStringAsync();

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Ad a = JsonSerializer.Deserialize<Ad>(jsonResponse)!;
            //    a.Person = Context.UserNow;
            //    Context.AdNow = a;
            //    Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(jsonResponser);

            //    MessageBox.Show("Изменения успешно установленны!");
            //}
            //else
            //{
            //    MessageBox.Show("Что то пошло не так!");
            //}
        }

        private async void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            //var httpClient = new HttpClient();
            //using HttpResponseMessage responseD = await httpClient.DeleteAsync($"http://localhost:5228/Ads/Delete?id={Context.AdNow.Id}");
            //using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
            //var responseContent = await response.Content.ReadAsStringAsync();

            //if (response.IsSuccessStatusCode)
            //{
            //    Context.AdList = new AdListViewModel();

            //    Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent);

            //    this.NavigationService.Navigate(new Uri("Views/MyAdsPage.xaml", UriKind.Relative));
            //}
            //else
            //{
            //    MessageBox.Show("Что то пошло не так...");
            //}
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
