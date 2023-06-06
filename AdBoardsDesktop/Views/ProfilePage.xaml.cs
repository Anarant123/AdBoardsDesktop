using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Contracts.Responses;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop.Models.db;
using AdBoardsDesktop.Models.DTO;
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
using static System.Net.Mime.MediaTypeNames;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        EditPersonModel p = new EditPersonModel();
        public ProfilePage()
        {
            InitializeComponent();

            var person = Context.UserNow.Person;

            tbNickName.Text = person.Name;
            tbBirthday.Text = person.Birthday.ToString().Substring(0, 10);
            tbCityName.Text = person.City;
            tbEmail.Text = person.Email;
            tbPhoneNumber.Text = person.Phone;
            imgPerson.Source = new BitmapImage(new Uri(person.PhotoName));
        }

        private void btnSetPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.* ";
            if (o.ShowDialog() == true)
            {
                try
                {
                    imgPerson.Source = LoadImage.Load(File.ReadAllBytes(o.FileName));

                    var stream = new FileStream(o.FileName, FileMode.Open);
                    var formFile = new FormFile(stream, 0, stream.Length, "streamFile", Path.GetFileName(o.FileName));

                    p.Photo = formFile;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            p.Name = tbNickName.Text;
            p.Birthday = Convert.ToDateTime(tbBirthday.Text);
            p.City = tbCityName.Text;
            p.Email = tbEmail.Text;
            p.Phone = tbPhoneNumber.Text;

            await Context.Api.PersonUpdate(p);
            var person = await Context.Api.UpdatePersonPhoto(p);

            if (person == null) 
            {
                MessageBox.Show("Что то пошло не так");
                return;
            }

            Context.UserNow.Person = person;
            MessageBox.Show("Вы успешно изменили данные профиля!");
        }

        private void btnResetInfo_Click(object sender, RoutedEventArgs e)
        {
            var person = Context.UserNow.Person;

            tbNickName.Text = person.Name;
            tbBirthday.Text = person.Birthday.ToString().Substring(0, 10);
            tbCityName.Text = person.City;
            tbEmail.Text = person.Email;
            tbPhoneNumber.Text = person.Phone;
            imgPerson.Source = new BitmapImage(new Uri(person.PhotoName));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Context.UserNow = null;
            this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
        }
    }
}
