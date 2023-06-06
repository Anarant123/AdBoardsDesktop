using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop.Models.db;
using System;
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
            PersonReg person = new PersonReg();
            person.Login = tbLogin.Text;
            person.Phone = tbPhone.Text;
            person.Email = tbEmail.Text;
            person.Password = tbPassword1.Password;
            person.ConfirmPassword = tbPassword2.Password;

            if (person.Password != person.ConfirmPassword)
            {
                MessageBox.Show("Пароли должны совпадать");
                return;
            }

            try
            {
                person.Birthday = Convert.ToDateTime(tbBirthday.Text);
            }
            catch
            {
                MessageBox.Show("Формат даты неверный!");
            }

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
