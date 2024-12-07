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
    /// Логика взаимодействия для AddMedicationPage.xaml
    /// </summary>
    public partial class AddMedicationPage : Page
    {
        private DBEntities _dbContext;

        public AddMedicationPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var medication = new Medications
                {
                    Name = NameTextBox.Text,
                    Quantity = int.Parse(QuantityTextBox.Text),
                    MinimumStockLevel = 0,
                    UnitPrice = decimal.Parse(PriceTextBox.Text)
                };

                _dbContext.Medications.Add(medication);
                _dbContext.SaveChanges();

                MessageBox.Show("Лекарство успешно добавлено.");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении лекарства: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
