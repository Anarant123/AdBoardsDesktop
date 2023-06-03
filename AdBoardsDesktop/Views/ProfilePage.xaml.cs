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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        PersonDTO p = new PersonDTO();
        public ProfilePage()
        {
            InitializeComponent();

            tbNickName.Text = Context.UserNow.Name;
            tbBirthday.Text = Context.UserNow.Birthday.ToString().Substring(0, 10);
            tbCityName.Text = Context.UserNow.City;
            tbEmail.Text = Context.UserNow.Email;
            tbPhoneNumber.Text = Context.UserNow.Phone;
            imgPerson.Source = LoadImage.Load(Context.UserNow.Photo);
        }

        private void btnSetPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.* ";
            if (o.ShowDialog() == true)
            {
                try
                {
                    p.Photo = File.ReadAllBytes(o.FileName);
                    imgPerson.Source = LoadImage.Load(p.Photo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            p.Login = Context.UserNow.Login;
            p.Name = tbNickName.Text;
            p.Birthday = Convert.ToDateTime(tbBirthday.Text);
            p.City = tbCityName.Text;
            p.Email = tbEmail.Text;
            p.Phone = tbPhoneNumber.Text;

            var httpClient = new HttpClient();
            using StringContent jsonContent = new(JsonSerializer.Serialize(p), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync("http://localhost:5228/People/Update", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Person p = JsonSerializer.Deserialize<Person>(jsonResponse)!;
                Context.UserNow = p;
                MessageBox.Show("Вы успешно изменили данные профиля!");
            }
            else
            {
                MessageBox.Show("Что то пошло не так");
            }
        }

        private void btnResetInfo_Click(object sender, RoutedEventArgs e)
        {
            tbNickName.Text = Context.UserNow.Name;
            tbBirthday.Text = Context.UserNow.Birthday.ToString().Substring(0, 10);
            tbCityName.Text = Context.UserNow.City;
            tbEmail.Text = Context.UserNow.Email;
            tbPhoneNumber.Text = Context.UserNow.Phone;
            imgPerson.Source = LoadImage.Load(Context.UserNow.Photo);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Context.UserNow = null;
            this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
        }
    }
}
