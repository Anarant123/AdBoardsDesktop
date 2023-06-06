using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop.Models.db;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Win32;
using System;
using System.IO;
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
        AddAdModel ad = new AddAdModel();
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
            ad.Id = Context.AdNow.Id;
            ad.Name = tbName.Text;
            ad.Description = tbDescription.Text;
            ad.CategoryId = cbСategories.SelectedIndex + 1;
            if (rbBuy.IsChecked == true)
                ad.AdTypeId = 1;
            else if (rbSell.IsChecked == true)
                ad.AdTypeId = 2;
            ad.Price = Convert.ToInt32(tbPrice.Text);
            ad.City = tbCity.Text;

            await Context.Api.AdUpdate(ad);
            var a = await Context.Api.UpdateAdPhoto(ad);

            if (a == null)
            {
                MessageBox.Show("Что то пошло не так\n");
                return;
            }

            Context.AdNow = a;
            MessageBox.Show("Вы успешно изменили объявление!");
        }

        private async void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            var result = await Context.Api.DeleteAd(Context.AdNow.Id);

            if (result)
            {
                this.NavigationService.Navigate(new Uri("Views/MyAdsPage.xaml", UriKind.Relative));
                return;
            }
            MessageBox.Show("Что то пошло не так...");
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
