using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop.Models.db;
using AdBoardsDesktop.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Win32;
using System;
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
    /// Логика взаимодействия для AddAdPage.xaml
    /// </summary>
    public partial class AddAdPage : Page
    {
        AddAdModel ad = new AddAdModel();
        public AddAdPage()
        {
            InitializeComponent();
        }

        private async void btnAddPost_Click(object sender, RoutedEventArgs e)
        {
            ad.Name = tbAddingName.Text;
            ad.City = tbAddingCity.Text;
            ad.CategoryId = cbAddingСategories.SelectedIndex + 1;
            ad.Description = tbAddingDescription.Text;
            ad.Price = Convert.ToInt32(tbAddingPrice.Text);
            if (rbBuy.IsChecked == true)
                ad.AdTypeId = 1;
            else
                ad.AdTypeId = 2;

            Context.AdNow = await Context.Api.AddAd(ad);
            ad.Id = Context.AdNow.Id;
            //Context.AdNow = await Context.Api.UpdateAdPhoto(ad);

            if (Context.AdNow == null)
            {
                MessageBox.Show("Что то пошло не так! \nОбъявление добавить не удалось...");
                return;
            }

            MessageBox.Show("Объявление успешно добавленно");
            this.NavigationService.Navigate(new Uri("Views/MyAdsPage.xaml", UriKind.Relative));

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
                    imgAd.Source = LoadImage.Load(File.ReadAllBytes(o.FileName));
                    IFormFile file = new FormFile(new FileStream(o.FileName, FileMode.Open), 0, 0, "name", Path.GetFileName(o.FileName));
                    ad.Photo = file;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
