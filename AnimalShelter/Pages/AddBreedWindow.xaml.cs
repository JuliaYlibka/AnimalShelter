using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBreedWindow.xaml
    /// </summary>
    public partial class AddBreedWindow : Window
    {
        private Breed _current_breed = new Breed();
        public event Action BreedAdded;

        public AddBreedWindow(Breed Selected_breed)
        {
            InitializeComponent();
            List<Breed> All_breeds = AnimalShelterEntities.GetContext().Breed.ToList();
            List<Species> All_Species = AnimalShelterEntities.GetContext().Species.ToList();
            CB_Species.ItemsSource = All_Species;
            Title_TB.Text = "Добавление данных о породе";
            if (Selected_breed != null)
            {
                _current_breed = Selected_breed;
                CB_Species.SelectedItem = Selected_breed.Species1;
                TB_Name_breed.Text = Selected_breed.Name_breed;
                Title_TB.Text = "Изменение данных о породе";
            }

            DataContext = _current_breed;
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void But_Add_Breed_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на название породы
            if (string.IsNullOrWhiteSpace(_current_breed.Name_breed))
                errors.AppendLine("Укажите название породы!");
            else _current_breed.Name_breed = TB_Name_breed.Text.Trim();

            // Проверка на вид животного
            if (CB_Species.SelectedItem == null)
                errors.AppendLine("Укажите вид животного!");
            else
                _current_breed.Species = (int)CB_Species.SelectedValue;
            
            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current_breed.ID_breed == 0)
                AnimalShelterEntities.GetContext().Breed.Add(_current_breed);

            //Делаем попытку записи данных в БД о новом пользователе
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                BreedAdded?.Invoke();
                Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Ошибка обновления: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void OnlyLetters_PreviewKeyDown(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsLetter))
            {
                e.Handled = true;
            }
        }
    }
}
