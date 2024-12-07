using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для ManageWorkersPage.xaml
    /// </summary>
    public partial class ManageWorkersPage : Page
    {
        private DBEntities _dbContext;

        public ManageWorkersPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadWorkers();
        }

        private void LoadWorkers()
        {
            // Загружаем работников с ролями "Ветеринар" или "Администратор"
            var workers = _dbContext.Users.ToList();

            WorkersListView.ItemsSource = workers;
        }

        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddWorkerPage());
        }

        private void EditWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedWorker = WorkersListView.SelectedItem;
            if (selectedWorker == null)
            {
                MessageBox.Show("Выберите работника для редактирования.");
                return;
            }

            NavigationService.Navigate(new EditWorkerPage((Users)selectedWorker));
        }

        private void DeleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = WorkersListView.SelectedItem as Users;
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить выбранного сотрудника?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Users.Remove(selectedUser);
                _dbContext.SaveChanges();
                LoadWorkers();
                MessageBox.Show("Сотрудник удален.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
