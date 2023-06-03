using AdBoardsDesktop.Models.db;
using AdBoardsDesktop.Models.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            PersonDTO person = new PersonDTO();

            if (tbPassword1.Password == tbPassword2.Password)
            {
                person.RightId = 1;
                person.Login = tbLogin.Text;

                try
                {
                    person.Birthday = Convert.ToDateTime(tbBirthday.Text);
                }
                catch 
                {
                    MessageBox.Show("Формат даты неверный!");
                }

                person.Phone = tbPhone.Text;
                person.Email = tbEmail.Text;
                person.Password = tbPassword1.Password;

                var httpClient = new HttpClient();
                using StringContent jsonContent = new(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5228/People/Registration", jsonContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    this.NavigationService.Navigate(new Uri("Views/AuthorizationPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Пользователь с данным логином или Email уже существует");
                }
            }
            else
            {
                MessageBox.Show("Пароли должны совпадать!");
            }
        }
    }
}
