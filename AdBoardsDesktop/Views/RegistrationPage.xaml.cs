using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop.Models.db;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            DateTime? birthday = null;

            bool ValidateFields()
            {
                // Проверка поля tbLogin
                if (string.IsNullOrWhiteSpace(tbLogin.Text))
                {
                    MessageBox.Show("Введите логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка поля tbBirthday
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
                if (string.IsNullOrWhiteSpace(tbPhone.Text) || !IsValidPhone(tbPhone.Text))
                {
                    MessageBox.Show("Введите корректный номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка полей tbPassword1 и tbPassword2
                if (string.IsNullOrWhiteSpace(tbPassword1.Password) || string.IsNullOrWhiteSpace(tbPassword2.Password))
                {
                    MessageBox.Show("Введите пароль в оба поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (tbPassword1.Password != tbPassword2.Password)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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


            PersonReg person = new PersonReg();
            person.Login = tbLogin.Text;
            person.Phone = tbPhone.Text;
            person.Email = tbEmail.Text;
            person.Password = tbPassword1.Password;
            person.ConfirmPassword = tbPassword2.Password;
            person.Birthday = Convert.ToDateTime(birthday);


            var result = await Context.Api.Registr(person);

            if (result)
            {
                this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
                return;
            }

            MessageBox.Show("Пользователь с данным логином или Email уже существует");
        }
    }
}
