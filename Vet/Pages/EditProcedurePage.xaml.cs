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
    /// Логика взаимодействия для EditProcedurePage.xaml
    /// </summary>
    public partial class EditProcedurePage : Page
    {
        private DBEntities _dbContext;
        private Procedures _procedure;

        public EditProcedurePage(int procedureId)
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            _procedure = _dbContext.Procedures.Find(procedureId);

            if (_procedure != null)
            {
                NameTextBox.Text = _procedure.Name;
                DescriptionTextBox.Text = _procedure.Description;
                CostTextBox.Text = _procedure.Price.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _procedure.Name = NameTextBox.Text;
                _procedure.Description = DescriptionTextBox.Text;
                _procedure.Price = decimal.Parse(CostTextBox.Text);

                _dbContext.SaveChanges();

                MessageBox.Show("Процедура успешно обновлена.");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании процедуры: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
