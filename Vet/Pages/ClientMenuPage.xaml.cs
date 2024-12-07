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

namespace Vet.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientMenuPage.xaml
    /// </summary>
    public partial class ClientMenuPage : Page
    {
        public ClientMenuPage()
        {
            InitializeComponent();
        }

        private void ManageAnimalsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageAnimalsPage());
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }

        private void CreateAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateAppointmentPage());
        }

        private void ViewAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientAppointmentsPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
