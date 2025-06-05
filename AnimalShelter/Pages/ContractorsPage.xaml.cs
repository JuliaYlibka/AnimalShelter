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
    /// Логика взаимодействия для ContractorsPage.xaml
    /// </summary>
    public partial class ContractorsPage : Page
    {
        List<Contractor> contractors= AnimalShelterEntities.GetContext().Contractor.ToList();
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50")); 
        private bool _only_FIZ;
        private bool _only_EYR;
        private bool az;
        private bool za;
        private AddContractorWindow _addContractorWindow; // Переменная для хранения текущего окна
        public ContractorsPage()
        {
            InitializeComponent();
            var allTypes = AnimalShelterEntities.GetContext().Contractor_type.ToList();
            Clear();
        }
        private void Clear() 
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;

            But_FIZ.Background = PassiveBut;
            But_EYR.Background = PassiveBut;
            But_All.Background = ActiveBut;

            TB_Search.Clear();
            az = false;
            za = false;
            _only_EYR = false;
            _only_FIZ = false;
            Update();
        }
        private void Update()
        {
            // Получаем всех контрагентов
            contractors = AnimalShelterEntities.GetContext().Contractor.ToList();

            // Фильтрация по типу контрагента
            if (_only_EYR)
                contractors = contractors.Where(x => x.Contractor_type == 2).ToList();
            else if (_only_FIZ)
                contractors = contractors.Where(x => x.Contractor_type == 1).ToList();

            // Поиск по введенному тексту
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                contractors = contractors.Where(
                    x => (x.Name != null && x.Name.ToLower().Contains(searchText)) ||
                         (x.Contact_person != null && x.Contact_person.ToLower().Contains(searchText)) ||
                         (x.Phone_number != null && x.Phone_number.ToLower().Contains(searchText)) ||
                         (x.Email != null && x.Email.ToLower().Contains(searchText)) ||
                         (x.Address != null && x.Address.ToLower().Contains(searchText)) ||
                         (x.INN != null && x.INN.ToLower().Contains(searchText))
                ).ToList();
            }

            // Сортировка
            if (az)
                contractors = contractors.OrderBy(x => x.Name).ToList();
            else if (za)
                contractors = contractors.OrderByDescending(x => x.Name).ToList();

            // Установка источника данных
            ListContractors.ItemsSource = contractors;
        }
        private void ListContractors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListContractors.SelectedItem != null)
                {
                    var Selected_contractor = ListContractors.SelectedItem as Contractor;

                    if (Selected_contractor != null)
                    {
                        if (_addContractorWindow == null || !_addContractorWindow.IsVisible)
                        {
                            _addContractorWindow = new AddContractorWindow(Selected_contractor);
                            _addContractorWindow.ContractorAdded += Update; // Подписываемся на событие
                            _addContractorWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления контрагента уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найден контрагент для изменения.",
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
            var contractor = (Contractor)button.DataContext; 

            if (!string.IsNullOrEmpty(contractor.Email))
            {
                // Копируем email в буфер обмена
                Clipboard.SetText(contractor.Email);
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
            var contractor = (Contractor)button.DataContext; 

            // Проверяем, что email не пустой
            if (!string.IsNullOrEmpty(contractor.Phone_number))
            {
                // Копируем email в буфер обмена
                Clipboard.SetText(contractor.Phone_number);
                MessageBox.Show("Номер телефона скопирован в буфер обмена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Номер телефона не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void But_All_Click(object sender, RoutedEventArgs e)
        {
            But_FIZ.Background = PassiveBut;
            But_EYR.Background = PassiveBut;
            But_All.Background = ActiveBut;
            _only_EYR = false; _only_FIZ = false;
            Update();
        }

        private void But_FIZ_Click(object sender, RoutedEventArgs e)
        {
            But_FIZ.Background = ActiveBut;
            But_EYR.Background = PassiveBut;
            But_All.Background = PassiveBut;
            _only_EYR = false; _only_FIZ = true;
            Update();
        }

        private void But_EYR_Click(object sender, RoutedEventArgs e)
        {
            But_FIZ.Background = PassiveBut;
            But_EYR.Background = ActiveBut;
            But_All.Background = PassiveBut;
            _only_EYR = true; _only_FIZ = false;
            Update();
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void But_Add_contractor_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_addContractorWindow == null || !_addContractorWindow.IsVisible)
            {
                _addContractorWindow = new AddContractorWindow(null);
                _addContractorWindow.ContractorAdded += Update; // Подписываемся на событие
                _addContractorWindow.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления контрагента уже открыто.");
            }
        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
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
    }
}
