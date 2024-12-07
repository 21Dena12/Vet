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
    /// Логика взаимодействия для CreateAppointmentPage.xaml
    /// </summary>
    public partial class CreateAppointmentPage : Page
    {
        private DBEntities _dbContext;

        public CreateAppointmentPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadAnimals();
            LoadAvailableTimes();
        }

        private void LoadAnimals()
        {
            var currentUserId = _dbContext.Clients.FirstOrDefault(a => a.UserID == CurrentUser.Instance.UserID).ClientID;
            var animals = _dbContext.Animals.Where(a => a.ClientID == currentUserId).ToList();
            AnimalComboBox.ItemsSource = animals;
            AnimalComboBox.DisplayMemberPath = "Name";
            AnimalComboBox.SelectedValuePath = "AnimalID";
        }

        private void LoadAvailableTimes()
        {
            // Предположим, что приёмы возможны с 9:00 до 18:00 с интервалом 1 час
            for (int hour = 9; hour < 18; hour++)
            {
                TimeComboBox.Items.Add($"{hour}:00");
            }
        }

        private void CreateAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalComboBox.SelectedItem == null || DatePicker.SelectedDate == null || TimeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            int animalId = (int)AnimalComboBox.SelectedValue;
            DateTime selectedDate = DatePicker.SelectedDate.Value;
            string selectedTime = TimeComboBox.SelectedItem.ToString();

            DateTime appointmentDateTime = DateTime.Parse($"{selectedDate.ToShortDateString()} {selectedTime}");

            // Проверка занятости времени
            var isTimeTaken = _dbContext.Appointments.Any(a => a.AppointmentDate == appointmentDateTime);
            if (isTimeTaken)
            {
                MessageBox.Show("Это время уже занято. Пожалуйста, выберите другое.");
                return;
            }

            // Создание записи
            var newAppointment = new Appointments
            {
                AnimalID = animalId,
                AppointmentDate = appointmentDateTime,
                Status = "Запланировано",
                VeterinarianID = null
            };

            _dbContext.Appointments.Add(newAppointment);
            _dbContext.SaveChanges();

            MessageBox.Show("Запись успешно создана.");
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
