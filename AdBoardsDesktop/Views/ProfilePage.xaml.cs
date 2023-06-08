using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Contracts.Responses;
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
using System.Windows.Media.Imaging;

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
            bool ValidateFields()
            {
                // Проверка поля tbBirthday
                DateTime birthday;
                if (!DateTime.TryParseExact(tbBirthday.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    MessageBox.Show("Введите корректную дату рождения в формате дд.мм.гггг.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка поля tbEmail
                if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
                {
                    MessageBox.Show("Введите корректный email.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка поля tbPhone
                if (string.IsNullOrWhiteSpace(tbPhoneNumber.Text) || !IsValidPhone(tbPhoneNumber.Text))
                {
                    MessageBox.Show("Введите корректный номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(tbNickName.Text))
                {
                    MessageBox.Show("Имя не корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(tbCityName.Text))
                {
                    MessageBox.Show("Имя не корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }

            // Вспомогательные методы для проверки email и номера телефона

            bool IsValidEmail(string email)
            {
                string pattern = @"^(\+)[1-9][0-9\-().]{9,15}$";
                Match match = Regex.Match(email, pattern);
                return match.Success;
            }

            bool IsValidPhone(string phone)
            {
                // Регулярное выражение для проверки корректности номера телефона
                // В данном примере, мы считаем корректными номера телефонов, состоящие из 10 цифр
                string pattern = @"^\d{11}$";

                // Проверка совпадения номера телефона с регулярным выражением
                Match match = Regex.Match(phone, pattern);

                return match.Success;
            }

            if (!ValidateFields())
                return;


            p.Name = tbNickName.Text;
            p.Birthday = Convert.ToDateTime(tbBirthday.Text);
            p.City = tbCityName.Text;
            p.Email = tbEmail.Text;
            p.Phone = tbPhoneNumber.Text;

            Person person = await Context.Api.PersonUpdate(p);
            if (p.Photo != null)
                person = await Context.Api.UpdatePersonPhoto(p);

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
