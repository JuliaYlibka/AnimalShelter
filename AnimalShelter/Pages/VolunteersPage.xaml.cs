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
    /// Логика взаимодействия для VolunteersPage.xaml
    /// </summary>
    public partial class VolunteersPage : Page
    {
        List<Volunteer> volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        AddVolunteerWindow _add_Window;
        private bool az;
        private bool za;
        private bool _only_M;
        private bool _only_W;

        public VolunteersPage()
        {
            InitializeComponent(); 
            Clear();

        }

        private void ListVolunteers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListVolunteers.SelectedItem != null)
                {
                    var Selected_volunteer = ListVolunteers.SelectedItem as Volunteer;

                    if (Selected_volunteer != null)
                    {
                        if (_add_Window == null || !_add_Window.IsVisible)
                        {
                            _add_Window = new AddVolunteerWindow(Selected_volunteer);
                            _add_Window.VolunteerAdded += Update; // Подписываемся на событие
                            _add_Window.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления волонтёра уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найден волонтёр для изменения.",
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

        private void But_Email_Copy_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Получаем родительский элемент DataTemplate, чтобы получить доступ к данным
            var _volunteer = (Volunteer)button.DataContext;

            if (!string.IsNullOrEmpty(_volunteer.Email))
            {
                // Копируем email в буфер обмена
                Clipboard.SetText(_volunteer.Email);
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
            var _volunteer = (Volunteer)button.DataContext;

            if (!string.IsNullOrEmpty(_volunteer.Phone_number))
            {
                // Копируем email в буфер обмена
                Clipboard.SetText(_volunteer.Phone_number);
                MessageBox.Show("Номер телефона скопирован в буфер обмена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Номер телефона не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = ActiveBut;
            But_Sort_Down.Background = PassiveBut;
            az = true; za = false;
            Update();
        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = ActiveBut;
            az = false; za = true;
            Update();
        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }


        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_add_Window == null || !_add_Window.IsVisible)
            {
                _add_Window = new AddVolunteerWindow(null);
                _add_Window.VolunteerAdded += Update; // Подписываемся на событие
                _add_Window.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления волонтёра уже открыто.");
            }
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
            // Получаем всех контрагентов
            volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            if (_only_W)
                volunteers = volunteers.Where(x => x.Gender == 1).ToList();
            else if (_only_M)
                volunteers = volunteers.Where(x => x.Gender == 2).ToList();

            // Поиск по введенному тексту
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                volunteers = volunteers.Where(
                    x => (x.First_name != null && x.First_name.ToLower().Contains(searchText)) ||
                         (x.Surname != null && x.Surname.ToLower().Contains(searchText)) ||
                         (x.Phone_number != null && x.Phone_number.ToLower().Contains(searchText)) ||
                         (x.Email != null && x.Email.ToLower().Contains(searchText)) ||
                         (x.Address != null && x.Address.ToLower().Contains(searchText))
                ).ToList();
            }

            // Сортировка
            if (az)
                volunteers = volunteers.OrderBy(x => x.Surname).ToList();
            else if (za)
                volunteers = volunteers.OrderByDescending(x => x.Surname).ToList();

            // Установка источника данных
            ListVolunteers.ItemsSource = volunteers;
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

        private void But_All_Click(object sender, RoutedEventArgs e)
        {
            But_W.Background = PassiveBut;
            But_M.Background = PassiveBut;
            But_All.Background = ActiveBut;
            _only_M = false; _only_W = false;
            Update();
        }
    }
}
