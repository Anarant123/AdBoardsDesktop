using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop.Models.db;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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

            tbAddingCity.Text = Context.UserNow.Person.City;
        }

        private async void btnAddPost_Click(object sender, RoutedEventArgs e)
        {
            int price;
            bool ValidateFields()
            {
                if (!int.TryParse(tbAddingPrice.Text, out price) || price < 0)
                {
                    MessageBox.Show("Некорректная цена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                if (string.IsNullOrWhiteSpace(tbAddingName.Text))
                {
                    MessageBox.Show("Название объявления является обязательным полем.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(tbAddingCity.Text))
                {
                    MessageBox.Show("Город является обязательным полем.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }


            if (!ValidateFields())
                return;

            ad.Name = tbAddingName.Text;
            ad.City = tbAddingCity.Text;
            ad.CategoryId = cbAddingСategories.SelectedIndex + 1;
            ad.Description = tbAddingDescription.Text;
            ad.Price = price;
            if (rbBuy.IsChecked == true)
                ad.AdTypeId = 1;
            else
                ad.AdTypeId = 2;

            ad.Id = (await Context.Api.AddAd(ad)).Id;
            Context.AdNow = await Context.Api.UpdateAdPhoto(ad);

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

                    var stream = new FileStream(o.FileName, FileMode.Open);
                    var formFile = new FormFile(stream, 0, stream.Length, "streamFile", Path.GetFileName(o.FileName));

                    ad.Photo = formFile;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
