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

namespace Vet.Pages
{
    /// <summary>
    /// Логика взаимодействия для VetMenuPage.xaml
    /// </summary>
    public partial class VetMenuPage : Page
    {
        public VetMenuPage()
        {
            InitializeComponent();
        }

        private void ManageAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageAppointmentsPage());
        }

        private void ManageMedicationsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageMedicationsPage());
        }

        private void ManageProceduresButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageProceduresPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionClass.CurrentUser = null;
            NavigationService.Navigate(new LoginPage());
        }
    }
}
