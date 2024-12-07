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
    /// Логика взаимодействия для ManageProceduresPage.xaml
    /// </summary>
    public partial class ManageProceduresPage : Page
    {
        private DBEntities _dbContext;

        public ManageProceduresPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadProcedures();
        }

        private void LoadProcedures()
        {
            ProceduresDataGrid.ItemsSource = _dbContext.Procedures.ToList();
        }

        private void AddProcedureButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProcedurePage());
        }

        private void EditProcedureButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProceduresDataGrid.SelectedItem is Procedures selectedProcedure)
            {
                NavigationService.Navigate(new EditProcedurePage(selectedProcedure.ProcedureID));
            }
            else
            {
                MessageBox.Show("Выберите процедуру для редактирования.");
            }
        }

        private void DeleteProcedureButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProceduresDataGrid.SelectedItem is Procedures selectedProcedure)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эту процедуру?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _dbContext.Procedures.Remove(selectedProcedure);
                    _dbContext.SaveChanges();
                    LoadProcedures();
                    MessageBox.Show("Процедура успешно удалена.");
                }
            }
            else
            {
                MessageBox.Show("Выберите процедуру для удаления.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
