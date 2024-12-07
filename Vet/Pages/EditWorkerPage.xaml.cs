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
    /// Логика взаимодействия для EditWorkerPage.xaml
    /// </summary>
    public partial class EditWorkerPage : Page
    {
        private DBEntities _dbContext;
        private Users _selectedUser;

        public EditWorkerPage(Users selectedUser)
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            _selectedUser = selectedUser;

            FullNameTextBox.Text = _selectedUser.FullName;
            RoleComboBox.SelectedItem = RoleComboBox.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(i => i.Content.ToString() == _selectedUser.Roles.RoleName);
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            switch (role)
            {
                case "Администратор":
                    _selectedUser.RoleID = 1;
                    break;
                case "Ветеринар":
                    _selectedUser.RoleID = 2;
                    break;
                case "Клиент":
                    _selectedUser.RoleID = 3;
                    break;
            }

            _dbContext.SaveChanges();
            MessageBox.Show("Изменения сохранены.");
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
