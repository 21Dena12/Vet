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
    /// Логика взаимодействия для ViewAppointmentsPage.xaml
    /// </summary>
    public partial class ViewAppointmentsPage : Page
    {
        private DBEntities _dbContext;

        public ViewAppointmentsPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var appointments = _dbContext.Appointments
                .Select(a => new
                {
                    ClientName = a.Animals.Clients.Users.FullName,
                    AnimalName = a.Animals.Name,
                    Status = a.Status,
                    Diagnosis = a.Diagnosis
                })
                .ToList();

            AppointmentsListView.ItemsSource = appointments;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
