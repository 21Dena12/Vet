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
    /// Логика взаимодействия для ManageMedicationsPage.xaml
    /// </summary>
    public partial class ManageMedicationsPage : Page
    {
        private DBEntities _dbContext;

        public ManageMedicationsPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadMedications();
        }

        private void LoadMedications()
        {
            MedicationsDataGrid.ItemsSource = _dbContext.Medications.ToList();
        }

        private void AddMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddMedicationPage());
        }

        private void EditMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedicationsDataGrid.SelectedItem is Medications selectedMedication)
            {
                NavigationService.Navigate(new EditMedicationPage(selectedMedication.MedicationID));
            }
            else
            {
                MessageBox.Show("Выберите лекарство для редактирования.");
            }
        }

        private void DeleteMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedicationsDataGrid.SelectedItem is Medications selectedMedication)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить это лекарство?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _dbContext.Medications.Remove(selectedMedication);
                    _dbContext.SaveChanges();
                    LoadMedications();
                    MessageBox.Show("Лекарство успешно удалено.");
                }
            }
            else
            {
                MessageBox.Show("Выберите лекарство для удаления.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
