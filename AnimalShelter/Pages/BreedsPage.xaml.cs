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

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для BreedsPage.xaml
    /// </summary>
    public partial class BreedsPage : Page
    {
        List<Breed> All_breeds = AnimalShelterEntities.GetContext().Breed.ToList();
        List<Species> All_Species = AnimalShelterEntities.GetContext().Species.ToList();
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        private bool _only_cat;
        private bool _only_dog;
        private bool az;
        private bool za;
        private AddBreedWindow _addBreedWindow; // Переменная для хранения текущего окна

        public BreedsPage()
        {
            InitializeComponent();
            Clear();

        }
        private void ListBreeds_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListBreeds.SelectedItem != null)
                {
                    var Selected_breed = ListBreeds.SelectedItem as Breed;

                    if (Selected_breed != null)
                    {
                        if (_addBreedWindow == null || !_addBreedWindow.IsVisible)
                        {
                            _addBreedWindow = new AddBreedWindow(Selected_breed);
                            _addBreedWindow.BreedAdded += Update; // Подписываемся на событие
                            _addBreedWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления породы уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найдена порода для изменения.",
                                        "Warning",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            
        }

        private void But_Cats_Click(object sender, RoutedEventArgs e)
        {
            But_Cats.Background = ActiveBut;
            But_Dogs.Background = PassiveBut;
            But_All.Background = PassiveBut;
            _only_dog = false;
            _only_cat = true;
            Update();
        }

        private void But_Dogs_Click(object sender, RoutedEventArgs e)
        {
            But_Dogs.Background = ActiveBut;
            But_Cats.Background = PassiveBut;
            But_All.Background = PassiveBut;

            _only_cat = false;
            _only_dog=true;
            Update();
        }

        private void TB_Breed_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void But_All_Click(object sender, RoutedEventArgs e)
        {
            _only_dog = false;
            _only_cat = false;
            Update();
            But_Dogs.Background = PassiveBut;
            But_Cats.Background = PassiveBut;
            But_All.Background = ActiveBut;
        }

        private void But_Sort_Name_species_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Name_species_Up.Background = ActiveBut;
            But_Sort_Name_species_Down.Background = PassiveBut;
            za = false;
            az = true;
            Update();            
        }

        private void But_Sort_Name_species_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Name_species_Down.Background = ActiveBut;
            But_Sort_Name_species_Up.Background = PassiveBut;
            az=false;
            za = true;

            Update();
        }
        private void Update()
        {
            All_breeds = AnimalShelterEntities.GetContext().Breed.ToList();
            if (_only_cat) All_breeds = AnimalShelterEntities.GetContext().Breed.Where(x => x.Species == 2).ToList();
            else if (_only_dog) All_breeds = AnimalShelterEntities.GetContext().Breed.Where(x => x.Species == 1).ToList();

            if (TB_Breed.Text.Trim().Length != 0)
            {
                
                All_breeds = All_breeds.Where(x => x.Name_breed.ToLower().Contains(TB_Breed.Text.ToLower())).ToList();
            }

            ListBreeds.ItemsSource = All_breeds;
            if (az) ListBreeds.ItemsSource = All_breeds.OrderBy(x => x.Name_breed).ToList();
            else if (za) ListBreeds.ItemsSource = All_breeds.OrderByDescending(x => x.Name_breed).ToList();
            else return;
        }
        private void But_Add_Breed_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_addBreedWindow == null || !_addBreedWindow.IsVisible)
            {
                _addBreedWindow = new AddBreedWindow(null);
                _addBreedWindow.BreedAdded += Update; // Подписываемся на событие
                _addBreedWindow.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления породы уже открыто.");
            }

        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            But_Sort_Name_species_Up.Background = PassiveBut;
            But_Sort_Name_species_Down.Background = PassiveBut;

            But_Cats.Background = PassiveBut;
            But_Dogs.Background = PassiveBut;
            But_All.Background = ActiveBut;

            TB_Breed.Clear();
            az = false;
            za = false;
            _only_cat = false;
            _only_dog = false;
            Update();
        }

        private void But_Delete_Source_of_recipe_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            T parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }

        private void But_Delete_Breed_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, которая была нажата
            Button deleteButton = sender as Button;
            if (deleteButton != null)
            {
                // Находим родительский элемент ListViewItem
                var listViewItem = FindParent<ListViewItem>(deleteButton);
                if (listViewItem != null)
                {
                    var breedToDelete = listViewItem.Content as Breed;
                    if (breedToDelete != null)
                    {
                        // Проверяем, есть ли животные с данной породой
                        var animalsUsingBreed = AnimalShelterEntities.GetContext().Animal.Where(a => a.Breed == breedToDelete.ID_breed).ToList();
                        if (animalsUsingBreed.Any())
                        {
                            MessageBox.Show("Ошибка удаления: Данная порода уже используется в системе, удалить её нельзя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return; // Прекращаем выполнение метода, если порода используется
                        }

                        // Подтверждаем удаление
                        MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить породу: {breedToDelete.Name_breed}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                // Удаляем из базы данных
                                var context = AnimalShelterEntities.GetContext();
                                context.Breed.Remove(breedToDelete);
                                context.SaveChanges();

                                Update();
                            }
                            catch (Exception ex)
                            {
                                // Обработка других возможных исключений
                                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }
    }
}
