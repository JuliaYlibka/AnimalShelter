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
    /// Логика взаимодействия для AdoptionsPage.xaml
    /// </summary>
    public partial class AdoptionsPage : Page
    {
        List<Adoption> Adoptions = AnimalShelterEntities.GetContext().Adoption.ToList();
        List<Adoption_status> Statuses = AnimalShelterEntities.GetContext().Adoption_status.ToList();
        List<Animal> Animals = AnimalShelterEntities.GetContext().Animal.ToList();
        List<New_owner> Owners = AnimalShelterEntities.GetContext().New_owner.ToList();
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        private bool Sort_Up_Animal;
        private bool Sort_Down_Animal;
        private bool Sort_Up_Owner;
        private bool Sort_Down_Owner;

        public AdoptionsPage()
        {
            InitializeComponent();
            Owners = AnimalShelterEntities.GetContext().New_owner.ToList();
            Animals = AnimalShelterEntities.GetContext().Animal.ToList();

            ListAdoptions.ItemsSource = Adoptions;
            Statuses.Insert(0, new Adoption_status { Name_adoption_status = "Все статусы" });
            CB_Status.ItemsSource = Statuses;
            
            Clear();

        }

        private void ListAdoptions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListAdoptions.SelectedItem != null)
                {
                    var Selected_adoption = ListAdoptions.SelectedItem as Adoption;

                    if (Selected_adoption != null)
                    {
                        NavigationService?.Navigate(new AddAdoptionPage(Selected_adoption));
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найдено усыновление.",
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

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = ActiveBut;
            But_Sort_Animal_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Animal_Down.Background = PassiveBut;
            Sort_Up_Owner = true; Sort_Down_Owner = false;Sort_Down_Animal = false;Sort_Up_Animal = false;
            Update();
        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background =PassiveBut ;
            But_Sort_Animal_Up.Background = PassiveBut;
            But_Sort_Down.Background = ActiveBut ;
            But_Sort_Animal_Down.Background = PassiveBut;
            Sort_Up_Owner = false; Sort_Down_Owner = true; Sort_Down_Animal = false; Sort_Up_Animal = false;
            Update();
        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService?.Navigate(new AddAdoptionPage(null));
        }
        private void Update()
        {
            //загружаем всех пользователей в список
            Adoptions = AnimalShelterEntities.GetContext().Adoption.ToList();
            //CB_Status
            if (CB_Status.SelectedIndex != 0)
                Adoptions = Adoptions.Where(x => x.Adoption_status == CB_Status.SelectedIndex).ToList();

            //TB_Nickname
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                Adoptions = Adoptions.Where(
                    x => (x.New_owner1.First_name != null && x.New_owner1.First_name.ToLower().Contains(searchText)) ||
                         (x.New_owner1.Surname != null && x.New_owner1.Surname.ToLower().Contains(searchText)) ||
                         (x.New_owner1.Patronymic != null && x.New_owner1.Patronymic.ToLower().Contains(searchText)) ||
                         (x.New_owner1.Phone_number != null && x.New_owner1.Phone_number.ToLower().Contains(searchText)) ||
                         (x.New_owner1.Email != null && x.New_owner1.Email.ToLower().Contains(searchText)) ||
                         (x.Animal1.Nickname != null && x.Animal1.Nickname.ToLower().Contains(searchText))
                ).ToList();
            }
            ListAdoptions.ItemsSource = Adoptions;
            if (Sort_Up_Animal) ListAdoptions.ItemsSource = Adoptions.OrderBy(x => x.Animal1.Nickname).ToList();
            if (Sort_Down_Animal) ListAdoptions.ItemsSource = Adoptions.OrderByDescending(x => x.Animal1.Nickname).ToList();
            if (Sort_Up_Owner) ListAdoptions.ItemsSource = Adoptions.OrderBy(x => x.New_owner1.Surname).ToList();
            if (Sort_Down_Owner) ListAdoptions.ItemsSource = Adoptions.OrderByDescending(x => x.New_owner1.Surname).ToList();
        }
        private void Clear()
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Animal_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Animal_Down.Background = PassiveBut;
            CB_Status.SelectedIndex = 0;


            TB_Search.Clear();
            Sort_Down_Owner = false;
            Sort_Down_Animal = false;
            Sort_Up_Owner = false;
            Sort_Up_Animal = false;

            Update();
        }

        private void But_Sort_Animal_Up_Click(object sender, RoutedEventArgs e) 
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Animal_Up.Background = ActiveBut ;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Animal_Down.Background = PassiveBut;
            Sort_Up_Owner = false; Sort_Down_Owner = false; Sort_Down_Animal = false; Sort_Up_Animal = true;
            Update();
        }

        private void But_Sort_Animal_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Animal_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Animal_Down.Background = ActiveBut;
            Sort_Up_Owner = false; Sort_Down_Owner = false; Sort_Down_Animal = true; Sort_Up_Animal = false;
            Update();
        }

        private void CB_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
