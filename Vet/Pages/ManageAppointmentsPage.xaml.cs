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
    /// Логика взаимодействия для ManageAppointmentsPage.xaml
    /// </summary>
    public partial class ManageAppointmentsPage : Page
    {
        private DBEntities _dbContext;

        public ManageAppointmentsPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var appointments = _dbContext.Appointments
                .Where(a => a.Status != "Завершено")
                .Select(a => new
                {
                    a.AppointmentID,
                    AnimalName = a.Animals.Name,
                    a.AppointmentDate,
                    a.Status,
                    FullName = a.Animals.Clients.Users.FullName
                })
                .ToList();

            AppointmentsListView.ItemsSource = appointments;
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAppointment = AppointmentsListView.SelectedItem as dynamic;
            if (selectedAppointment == null)
            {
                MessageBox.Show("Выберите запись для изменения статуса.");
                return;
            }

            int appointmentId = selectedAppointment.AppointmentID;
            var appointment = _dbContext.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);

            if (appointment != null)
            {
                appointment.Status = "В процессе";
                _dbContext.SaveChanges();
                LoadAppointments();
                MessageBox.Show("Статус обновлён.");
            }
        }

        private void AddDiagnosisButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAppointment = AppointmentsListView.SelectedItem as dynamic;
            if (selectedAppointment == null)
            {
                MessageBox.Show("Выберите запись для добавления диагноза.");
                return;
            }

            int appointmentId = selectedAppointment.AppointmentID;
            NavigationService.Navigate(new AddDiagnosisPage(appointmentId));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
