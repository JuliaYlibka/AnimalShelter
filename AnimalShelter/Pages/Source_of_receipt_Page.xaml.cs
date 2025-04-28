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
    /// Логика взаимодействия для Source_of_receipt_Page.xaml
    /// </summary>
    public partial class Source_of_receipt_Page : Page

    {
        List<Source_of_receipt> All_Source = AnimalShelterEntities.GetContext().Source_of_receipt.ToList();
        private SourceAddWindow _addSourceWindow; // Переменная для хранения текущего окна

        private bool az;
        private bool za;
        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));
        public Source_of_receipt_Page()
        {
            InitializeComponent();
            Update();
            List_Source_of_receipt.ItemsSource = All_Source;

        }

        private void But_Add_Source_of_receipt_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_addSourceWindow == null || !_addSourceWindow.IsVisible)
            {
                _addSourceWindow = new SourceAddWindow(null);
                _addSourceWindow.SourceAdded += Update; // Подписываемся на событие
                _addSourceWindow.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления породы уже открыто.");
            }
        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = ActiveBut;
            But_Sort_Down.Background = PassiveBut;
            za = false;
            az = true;

            Update();
        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Down.Background = ActiveBut;
           But_Sort_Up.Background = PassiveBut;
            az = false;
            za = true;

            Update();
        }

        private void TB_Source_TextChanged(object sender, TextChangedEventArgs e)
        {

            Update();
        }
        private void Update()
        {
            All_Source = AnimalShelterEntities.GetContext().Source_of_receipt.ToList();

            if (TB_Source.Text.Trim().Length != 0)
            {

                All_Source = All_Source.Where(x => x.Name_source_of_receipt.ToLower().Contains(TB_Source.Text.ToLower())).ToList();
            }

            List_Source_of_receipt.ItemsSource = All_Source;
            if (az) List_Source_of_receipt.ItemsSource = All_Source.OrderBy(x => x.Name_source_of_receipt).ToList();
            else if (za) List_Source_of_receipt.ItemsSource = All_Source.OrderByDescending(x => x.Name_source_of_receipt).ToList();
            else return;
        }

        private void List_Source_of_receipt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {
                if (List_Source_of_receipt.SelectedItem != null)
                {
                    var Selected_source = List_Source_of_receipt.SelectedItem as Source_of_receipt;

                    if (Selected_source != null)
                    {
                        if (_addSourceWindow == null || !_addSourceWindow.IsVisible)
                        {
                            _addSourceWindow = new SourceAddWindow(Selected_source);
                            _addSourceWindow.SourceAdded += Update; // Подписываемся на событие
                            _addSourceWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найден источник для изменения.",
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

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            az = false;
            za = false;
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            TB_Source.Clear();
            Update();
        }
    }
}
