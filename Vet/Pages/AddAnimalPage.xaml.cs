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
    /// Логика взаимодействия для AddAnimalPage.xaml
    /// </summary>
    public partial class AddAnimalPage : Page
    {
        private DBEntities _dbContext;

        public AddAnimalPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void SaveAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string species = SpeciesTextBox.Text;
            string breed = BreedTextBox.Text;
            int age;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(species) || string.IsNullOrEmpty(breed) || !int.TryParse(AgeTextBox.Text, out age))
            {
                MessageBox.Show("Заполните все поля корректно.");
                return;
            }
            if (age > 50 || age <= 0)
            {
                MessageBox.Show("Возраст не правильный!");
                return;
            }
            var currentUserId = _dbContext.Clients.FirstOrDefault(a => a.UserID == CurrentUser.Instance.UserID).ClientID;

            Animals newAnimal = new Animals
            {
                Name = name,
                Species = species,
                Breed = breed,
                Age = age,
                ClientID = currentUserId
            };

            _dbContext.Animals.Add(newAnimal);
            _dbContext.SaveChanges();

            MessageBox.Show("Животное добавлено.");
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
