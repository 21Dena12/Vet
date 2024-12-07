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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private DBEntities _dbContext;

        public RegisterPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string fullName = FullNameTextBox.Text;

            if (_dbContext.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Пользователь с таким именем уже существует.");
                return;
            }

            // По умолчанию новая регистрация — это клиент
            var newUser = new Users
            {
                Username = username,
                PasswordHash = password, // Здесь добавьте хеширование
                FullName = fullName,
                RoleID = 3 // Роль "Client"
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            MessageBox.Show("Регистрация прошла успешно!");
            NavigationService.Navigate(new LoginPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
