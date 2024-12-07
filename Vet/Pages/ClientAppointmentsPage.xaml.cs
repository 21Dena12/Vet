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
    /// Логика взаимодействия для ClientAppointmentsPage.xaml
    /// </summary>
    public partial class ClientAppointmentsPage : Page
    {
        private DBEntities _dbContext;

        public ClientAppointmentsPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var currentUserId = _dbContext.Clients.FirstOrDefault(a => a.UserID == CurrentUser.Instance.UserID).ClientID;
            var appointments = _dbContext.Appointments
                .Where(a => a.Animals.ClientID == currentUserId)
                .Select(a => new
                {
                    a.AppointmentID,
                    AnimalName = a.Animals.Name,
                    a.AppointmentDate,
                    a.Status
                })
                .ToList();

            AppointmentsListView.ItemsSource = appointments;
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAppointment = AppointmentsListView.SelectedItem as dynamic;
            if (selectedAppointment == null)
            {
                MessageBox.Show("Выберите запись для удаления.");
                return;
            }

            int appointmentId = selectedAppointment.AppointmentID;
            var appointment = _dbContext.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);

            if (appointment != null)
            {
                _dbContext.Appointments.Remove(appointment);
                _dbContext.SaveChanges();
                LoadAppointments();
                MessageBox.Show("Запись удалена.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
