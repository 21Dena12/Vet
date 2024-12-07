using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vet.Classes;
using Vet.DBModel;

namespace Vet.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private DBEntities _dbContext;

        public LoginPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Используем LINQ для поиска пользователя
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == password);

            if (user != null)
            {
                CurrentUser.Instance.UserID = user.UserID;
                CurrentUser.Instance.FullName = user.FullName;
                CurrentUser.Instance.Role = user.Roles.RoleName;

                MessageBox.Show($"Добро пожаловать, {CurrentUser.Instance.FullName}!");

                // Навигация по роли
                switch (CurrentUser.Instance.Role)
                {
                    case "Administrator":
                        NavigationService.Navigate(new AdminPage());
                        break;
                    case "Veterinarian":
                        NavigationService.Navigate(new VetMenuPage());
                        break;
                    case "Client":
                        NavigationService.Navigate(new ClientMenuPage());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неправильное имя пользователя или пароль.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRPage("https://mck-ktits.ru"));
        }
    }
}
