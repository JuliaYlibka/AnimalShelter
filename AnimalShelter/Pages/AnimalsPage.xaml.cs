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
    /// Логика взаимодействия для AnimalsPage.xaml
    /// </summary>
    public partial class AnimalsPage : Page
    {
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        System.Collections.Generic.List<AnimalShelter.Animal> currentAnimals = AnimalShelterEntities.GetContext().Animal.ToList();

        public AnimalsPage()
        {
            InitializeComponent();
            var currentStatuses = AnimalShelterEntities.GetContext().Animal_status.ToList();
            currentStatuses.Insert(0, new Animal_status { Name_animal_status = "Все статусы" }); 

            ListAnimals.ItemsSource = currentAnimals;
            CB_Status.ItemsSource = currentStatuses;
            CB_Status.SelectedIndex = 0;
            //Update();
        }

        private void ListAnimals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new AnimalPage());
        }

        private void But_Sort_Nickname_Up_Click(object sender, RoutedEventArgs e)
        {
            ListAnimals.ItemsSource = currentAnimals.OrderBy(x => x.Nickname).ToList();
            But_Sort_Nickname_Up.Background = ActiveBut; 
            But_Sort_Nickname_Down.Background = PassiveBut;
            But_Sort_Age_Up.Background = PassiveBut;
            But_Sort_Age_Down.Background = PassiveBut;

        }

        private void But_Sort_Nickname_Down_Click(object sender, RoutedEventArgs e)
        {
            ListAnimals.ItemsSource = currentAnimals.OrderByDescending(x => x.Nickname).ToList();
            But_Sort_Nickname_Up.Background = PassiveBut;
            But_Sort_Nickname_Down.Background = ActiveBut;
            But_Sort_Age_Up.Background = PassiveBut;
            But_Sort_Age_Down.Background = PassiveBut;
        }

        private void But_Sort_Age_Up_Click(object sender, RoutedEventArgs e)
        {
            ListAnimals.ItemsSource = currentAnimals.OrderBy(x => x.Age).ToList();
            But_Sort_Nickname_Up.Background = PassiveBut;
            But_Sort_Nickname_Down.Background = PassiveBut;
            But_Sort_Age_Up.Background =ActiveBut ;
            But_Sort_Age_Down.Background = PassiveBut;
        }

        private void But_Sort_Age_Down_Click(object sender, RoutedEventArgs e)
        {
            ListAnimals.ItemsSource = currentAnimals.OrderByDescending(x => x.Age).ToList();
            But_Sort_Nickname_Up.Background = PassiveBut;
            But_Sort_Nickname_Down.Background = PassiveBut;
            But_Sort_Age_Up.Background = PassiveBut;
            But_Sort_Age_Down.Background =ActiveBut ;
        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Nickname_Up.Background = PassiveBut;
            But_Sort_Nickname_Down.Background = PassiveBut;
            But_Sort_Age_Up.Background = PassiveBut;
            But_Sort_Age_Down.Background = PassiveBut;
            CB_Status.SelectedIndex = 0;
            TB_Nickname.Clear();
            currentAnimals = AnimalShelterEntities.GetContext().Animal.ToList();

            ListAnimals.ItemsSource = currentAnimals;

        }

        private void TB_Nickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void CB_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private  void Update()
        {
            //загружаем всех пользователей в список
            currentAnimals = AnimalShelterEntities.GetContext().Animal.ToList();
            //CB_Status
            if (CB_Status.SelectedIndex != 0)
                currentAnimals = currentAnimals = currentAnimals.Where(x => x.Animal_status == CB_Status.SelectedIndex).ToList();
            
            //TB_Nickname
            if (TB_Nickname.Text.Trim().Length != 0)
            {
                currentAnimals = currentAnimals.Where(x => x.Nickname.ToLower().Contains(TB_Nickname.Text.ToLower())).ToList();
            }



            ListAnimals.ItemsSource = currentAnimals;

        }
    }
}
