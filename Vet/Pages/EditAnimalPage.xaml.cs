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
    /// Логика взаимодействия для EditAnimalPage.xaml
    /// </summary>
    public partial class EditAnimalPage : Page
    {
        private DBEntities _dbContext;
        private int _animalId;

        public EditAnimalPage(int animalId)
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
            _animalId = animalId;
            LoadAnimalDetails();
        }

        private void LoadAnimalDetails()
        {
            var animal = _dbContext.Animals.FirstOrDefault(a => a.AnimalID == _animalId);

            if (animal == null)
            {
                MessageBox.Show("Животное не найдено.");
                NavigationService.GoBack();
                return;
            }

            NameTextBox.Text = animal.Name;
            SpeciesTextBox.Text = animal.Species;
            BreedTextBox.Text = animal.Breed;
            AgeTextBox.Text = animal.Age.ToString();
            GenderComboBox.SelectedIndex = animal.Gender == "Male" ? 0 : 1;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var animal = _dbContext.Animals.FirstOrDefault(a => a.AnimalID == _animalId);

            if (animal == null)
            {
                MessageBox.Show("Ошибка при сохранении данных.");
                return;
            }

            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(SpeciesTextBox.Text) ||
                string.IsNullOrWhiteSpace(BreedTextBox.Text) || string.IsNullOrWhiteSpace(AgeTextBox.Text) ||
                GenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!int.TryParse(AgeTextBox.Text, out int age) || age < 0)
            {
                MessageBox.Show("Возраст должен быть числом больше или равным 0.");
                return;
            }

            if (age > 50 || age <= 0)
            {
                MessageBox.Show("Возраст не правильный!");
                return;
            }

            animal.Name = NameTextBox.Text;
            animal.Species = SpeciesTextBox.Text;
            animal.Breed = BreedTextBox.Text;
            animal.Age = age;
            animal.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();

            _dbContext.SaveChanges();

            MessageBox.Show("Данные животного успешно сохранены.");
            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
