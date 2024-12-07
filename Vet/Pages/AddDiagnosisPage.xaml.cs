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
    /// Логика взаимодействия для AddDiagnosisPage.xaml
    /// </summary>
    public partial class AddDiagnosisPage : Page
    {
        private DBEntities _dbContext;
        private int _appointmentId;

        public AddDiagnosisPage(int appointmentId)
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            _appointmentId = appointmentId;
            LoadAppointmentDetails();
        }

        private void LoadAppointmentDetails()
        {
            var appointment = _dbContext.Appointments.FirstOrDefault(a => a.AppointmentID == _appointmentId);

            if (appointment == null)
            {
                MessageBox.Show("Запись не найдена.");
                NavigationService.GoBack();
                return;
            }
        }

        private void SaveDiagnosisButton_Click(object sender, RoutedEventArgs e)
        {
            var appointment = _dbContext.Appointments.FirstOrDefault(a => a.AppointmentID == _appointmentId);

            if (appointment != null)
            {
                if (string.IsNullOrWhiteSpace(DiagnosisTextBox.Text) || string.IsNullOrWhiteSpace(RecommendationsTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните диагноз и рекомендации.");
                    return;
                }

                appointment.Diagnosis = DiagnosisTextBox.Text;
                appointment.Recommendations = RecommendationsTextBox.Text;
                appointment.Status = "Завершено";

                _dbContext.SaveChanges();

                MessageBox.Show("Диагноз успешно сохранён.");
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении данных.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
