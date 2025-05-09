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
    /// Логика взаимодействия для OwnersPage.xaml
    /// </summary>
    public partial class OwnersPage : Page
    {
        List<New_owner> New_owners = AnimalShelterEntities.GetContext().New_owner.ToList();
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        //AddVolunteerWindow _add_Window;
        private bool az;
        private bool za;
        private bool _only_M;
        private bool _only_W;
        public OwnersPage()
        {
            InitializeComponent();
            New_owners = AnimalShelterEntities.GetContext().New_owner.ToList();
            List_new_owners.ItemsSource = New_owners;
            //TODO: добавить фильтр по типу жилья ?


        }

        private void List_new_owners_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO: изменение нового владельца
        }

        private void But_Email_Copy_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Получаем родительский элемент DataTemplate, чтобы получить доступ к данным
            var _new_owner = (New_owner)button.DataContext;

            if (!string.IsNullOrEmpty(_new_owner.Email))
            {
                // Копируем email в буфер обмена
                Clipboard.SetText(_new_owner.Email);
                MessageBox.Show("Email скопирован в буфер обмена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Email не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void But_Phone_Copy_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Получаем родительский элемент DataTemplate, чтобы получить доступ к данным
            var _new_owner = (New_owner)button.DataContext;

            if (!string.IsNullOrEmpty(_new_owner.Phone_number))
            {
                // Копируем email в буфер обмена
                Clipboard.SetText(_new_owner.Phone_number);
                MessageBox.Show("Номер телефона скопирован в буфер обмена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Номер телефона не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void But_All_Click(object sender, RoutedEventArgs e)
        {
            But_W.Background = PassiveBut;
            But_M.Background = PassiveBut;
            But_All.Background = ActiveBut;
            _only_M = false; _only_W = false;
            Update();
        }

        private void But_M_Click(object sender, RoutedEventArgs e)
        {
            But_M.Background = ActiveBut;
            But_W.Background = PassiveBut;
            But_All.Background = PassiveBut;
            _only_W = false; _only_M = true;
            Update();
        }

        private void But_W_Click(object sender, RoutedEventArgs e)
        {
            But_W.Background = ActiveBut;
            But_M.Background = PassiveBut;
            But_All.Background = PassiveBut;
            _only_M = false; _only_W = true;
            Update();
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();

        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = ActiveBut;
            az = false; za = true;
            Update();
        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = ActiveBut;
            But_Sort_Down.Background = PassiveBut;
            az = true; za = false;
            Update();
        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();

        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            //TODO: добавление нового владельца

        }
        private void Clear()
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;

            But_M.Background = PassiveBut;
            But_W.Background = PassiveBut;
            But_All.Background = ActiveBut;

            _only_M = false;
            _only_W = false;

            TB_Search.Clear();
            az = false;
            za = false;
            Update();
        }
        private void Update()
        {
            //TODO: добавить пол в бд 
            // Получаем всех контрагентов
            New_owners = AnimalShelterEntities.GetContext().New_owner.ToList();
            //if (_only_W)
            //    //New_owners = New_owners.Where(x => x.Gender == 1).ToList();
            //else if (_only_M)
                //New_owners = New_owners.Where(x => x.Gender == 2).ToList();

            // Поиск по введенному тексту
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                New_owners = New_owners.Where(
                    x => (x.First_name != null && x.First_name.ToLower().Contains(searchText)) ||
                         (x.Surname != null && x.Surname.ToLower().Contains(searchText)) ||
                         (x.Phone_number != null && x.Phone_number.ToLower().Contains(searchText)) ||
                         (x.Email != null && x.Email.ToLower().Contains(searchText)) ||
                         (x.Address != null && x.Address.ToLower().Contains(searchText))
                ).ToList();
            }

            // Сортировка
            if (az)
                New_owners = New_owners.OrderBy(x => x.Surname).ToList();
            else if (za)
                New_owners = New_owners.OrderByDescending(x => x.Surname).ToList();

            // Установка источника данных
            List_new_owners.ItemsSource = New_owners;
        }
    }
}
