using AdBoardsDesktop.Models.db;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace AdBoardsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            getPeople();
        }

        private async void getPeople()
        {
            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5228/People/GetPeople");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Context.AdList = new AdListViewModel();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var people = JsonSerializer.Deserialize<List<Person>>(responseContent, options);
                dgPeople.ItemsSource = people.ToList();
            }
        }

        private async void btnDropClient_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:5228/People/Delete?Login={tbLogin.Text}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Пользователь успешно удален");
                getPeople();
            }
            else
            {
                MessageBox.Show("Что то пошло не так...");
            }
        }
    }
}
