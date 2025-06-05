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
using System.Xml.Linq;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedicalRecordsPage.xaml
    /// </summary>
    public partial class MedicalRecordsPage : Page
    {
        System.Collections.Generic.List<AnimalShelter.Medical_record> currentRecords = AnimalShelterEntities.GetContext().Medical_record.ToList();
        private bool az;
        private bool za;
        private bool Dateaz;
        private bool Dateza;
        private bool Only_castr;

        private bool Only_Not_castr;
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        private AddMedicalRecordWindow _addWindow; // Переменная для хранения текущего окна

        public MedicalRecordsPage()
        {
            InitializeComponent();
            var currentStatuses = AnimalShelterEntities.GetContext().Animal_status.ToList();

            currentStatuses.Insert(0, new Animal_status { Name_animal_status = "Все статусы" });
            CB_Status.ItemsSource = currentStatuses;
            CB_Status.SelectedIndex = 1;
            Clear();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_addWindow == null || !_addWindow.IsVisible)
            {
                _addWindow = new AddMedicalRecordWindow(null);
                _addWindow.Added += Update; // Подписываемся на событие
                _addWindow.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления медицинской карты уже открыто.");
            }
        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Date_Up.Background = PassiveBut;
            But_Sort_Date_Down.Background = PassiveBut;

            CB_Castr.IsChecked = false;
            CB_NotCastr.IsChecked = false;

            az = false; za = false;
            Dateaz = false; Dateza = false;
            Only_castr = false; Only_Not_castr = false;
            TB_Search.Clear();

            Update();
        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = ActiveBut;
            But_Sort_Date_Down.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Date_Up.Background = PassiveBut;
            az = true;za = false; 
            Dateaz = false;Dateza = false; 
            Update();
        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Date_Up.Background = PassiveBut;
            But_Sort_Date_Down.Background = PassiveBut;
            But_Sort_Down.Background = ActiveBut;
            az = false; za = true;
            Dateaz = false; Dateza = false;

            Update();
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            //загружаем всех пользователей в список
            currentRecords = AnimalShelterEntities.GetContext().Medical_record.ToList();

            if (Only_castr) currentRecords = currentRecords.Where(x => x.Sterilized == true).ToList(); 
            if (Only_Not_castr) currentRecords = currentRecords.Where(x => x.Sterilized == false).ToList();
            if (CB_Status.SelectedIndex != 0)
                currentRecords = currentRecords.Where(x => x.Animal1.Animal_status == CB_Status.SelectedIndex).ToList();
            //TB_Nickname
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                currentRecords = currentRecords.Where(
                    x => (x.Animal1.Nickname != null && x.Animal1.Nickname.ToLower().Contains(searchText))).ToList();
            }
            ListRecords.ItemsSource = currentRecords;
            if (az) ListRecords.ItemsSource = currentRecords.OrderBy(x => x.Animal1.Nickname).ToList();
            if (Dateaz) ListRecords.ItemsSource = currentRecords.OrderBy(x => x.Last_update_date).ToList();
            if (za) ListRecords.ItemsSource = currentRecords.OrderByDescending(x => x.Animal1.Nickname).ToList();
            if (Dateza) ListRecords.ItemsSource = currentRecords.OrderByDescending(x => x.Last_update_date).ToList();

        }

        private void But_Sort_Date_Up_Click(object sender, RoutedEventArgs e)
        {
            az=false;
            za=false;
            Dateza = false;
            Dateaz = true;

            But_Sort_Date_Up.Background = ActiveBut;
            But_Sort_Date_Down.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Up.Background = PassiveBut;
            Update();

        }

        private void But_Sort_Date_Down_Click(object sender, RoutedEventArgs e)
        {
            az = false;
            za = false;
            Dateza = true;
            Dateaz = false;

            But_Sort_Date_Up.Background = PassiveBut;
            But_Sort_Date_Down.Background = ActiveBut;
            But_Sort_Down.Background = PassiveBut;
            But_Sort_Up.Background = PassiveBut;
            Update();

        }

        private void CB_Castr_Checked(object sender, RoutedEventArgs e)
        {
            CB_NotCastr.IsChecked = false;
            Only_castr = true;
            Only_Not_castr = false;
            Update();


        }

        private void CB_Castr_Unchecked(object sender, RoutedEventArgs e)
        {
            Only_castr = false;
            Update();

        }

        private void CB_NotCastr_Checked(object sender, RoutedEventArgs e)
        {
            CB_Castr.IsChecked = false;
            Only_castr = false;
            Only_Not_castr = true;
            Update();

        }

        private void CB_NotCastr_Unchecked(object sender, RoutedEventArgs e)
        {
            Only_Not_castr = false;
            Update();

        }

        private void ListRecords_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListRecords.SelectedItem != null)
                {
                    var Selected = ListRecords.SelectedItem as Medical_record;

                    if (Selected != null)
                    {
                        if (_addWindow == null || !_addWindow.IsVisible)
                        {
                            _addWindow = new AddMedicalRecordWindow(Selected);
                            _addWindow.Added += Update; // Подписываемся на событие
                            _addWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно изменения медицинской карты уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найдена медицинская карта для изменения.",
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

        private void CB_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();

        }
    }
}
