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
    /// Логика взаимодействия для EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        private DBEntities _dbContext;

        public EditProfilePage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadProfileData();
        }

        private void LoadProfileData()
        {
            var currentUser = CurrentUser.Instance;
            FullNameTextBox.Text = currentUser.FullName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.UserID == ConnectionClass.CurrentUser.UserID);

            if (currentUser != null)
            {
                currentUser.FullName = FullNameTextBox.Text;

                _dbContext.SaveChanges();
                MessageBox.Show("Профиль успешно обновлён.");
                NavigationService.GoBack();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
