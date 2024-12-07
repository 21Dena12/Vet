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
    /// Логика взаимодействия для ManageAnimalsPage.xaml
    /// </summary>
    public partial class ManageAnimalsPage : Page
    {
        private DBEntities _dbContext;

        public ManageAnimalsPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            var currentUserId = _dbContext.Clients.FirstOrDefault(a => a.UserID == CurrentUser.Instance.UserID).ClientID;
            var animals = _dbContext.Animals
                .Where(a => a.ClientID == currentUserId)
                .ToList();
            AnimalsListView.ItemsSource = animals;
        }

        private void AddAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAnimalPage());
        }

        private void EditAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAnimal = AnimalsListView.SelectedItem as Animals;
            if (selectedAnimal == null)
            {
                MessageBox.Show("Выберите животное для редактирования.");
                return;
            }
            NavigationService.Navigate(new EditAnimalPage(selectedAnimal.AnimalID));
        }

        private void DeleteAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAnimal = AnimalsListView.SelectedItem as Animals;
            if (selectedAnimal == null)
            {
                MessageBox.Show("Выберите животное для удаления.");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить это животное?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Animals.Remove(selectedAnimal);
                _dbContext.SaveChanges();
                LoadAnimals();
                MessageBox.Show("Животное удалено.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
