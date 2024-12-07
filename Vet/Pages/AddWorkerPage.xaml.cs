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
    /// Логика взаимодействия для AddWorkerPage.xaml
    /// </summary>
    public partial class AddWorkerPage : Page
    {
        private DBEntities _dbContext;

        public AddWorkerPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(role) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            //Users newUser = new Users
            //{
            //    FullName = fullName,
            //    Role = role,
            //    Password = password
            //};

            //_dbContext.Users.Add(newUser);
            //_dbContext.SaveChanges();

            //MessageBox.Show("Сотрудник добавлен.");
            //NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
