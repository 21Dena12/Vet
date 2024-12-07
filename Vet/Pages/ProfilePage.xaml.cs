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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private DBEntities _dbContext;

        public ProfilePage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadProfile();
        }

        private void LoadProfile()
        {
            var currentUser = CurrentUser.Instance;
            FullNameTextBox.Text = currentUser.FullName;
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
