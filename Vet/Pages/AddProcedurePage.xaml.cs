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
    /// Логика взаимодействия для AddProcedurePage.xaml
    /// </summary>
    public partial class AddProcedurePage : Page
    {
        private DBEntities _dbContext;

        public AddProcedurePage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var procedure = new Procedures
                {
                    Name = NameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    Price = decimal.Parse(CostTextBox.Text)
                };

                _dbContext.Procedures.Add(procedure);
                _dbContext.SaveChanges();

                MessageBox.Show("Процедура успешно добавлена.");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении процедуры: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
