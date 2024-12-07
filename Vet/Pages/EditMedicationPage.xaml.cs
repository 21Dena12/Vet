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
    /// Логика взаимодействия для EditMedicationPage.xaml
    /// </summary>
    public partial class EditMedicationPage : Page
    {
        private DBEntities _dbContext;
        private Medications _medication;

        public EditMedicationPage(int medicationId)
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            _medication = _dbContext.Medications.Find(medicationId);

            if (_medication != null)
            {
                NameTextBox.Text = _medication.Name;
                QuantityTextBox.Text = _medication.Quantity.ToString();
                PriceTextBox.Text = _medication.UnitPrice.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _medication.Name = NameTextBox.Text;
                _medication.Quantity = int.Parse(QuantityTextBox.Text);
                _medication.UnitPrice = int.Parse(PriceTextBox.Text);

                _dbContext.SaveChanges();

                MessageBox.Show("Лекарство успешно обновлено.");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании лекарства: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
