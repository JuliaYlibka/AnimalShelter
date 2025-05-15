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

        SolidColorBrush PassiveBut = new SolidColorBrush(Colors.White);
        SolidColorBrush ActiveBut = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD50"));

        public MedicalRecordsPage()
        {
            InitializeComponent();
            Update();

        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {
            TB_Search.Clear();
            az = false;
            za=false;
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = PassiveBut;
            az = false; za = false;
            Update();
        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = ActiveBut;
            But_Sort_Down.Background = PassiveBut;
            az = true;za = false;
            Update();
        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {
            But_Sort_Up.Background = PassiveBut;
            But_Sort_Down.Background = ActiveBut;
            az = false; za = true;
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
            
            //TB_Nickname
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                currentRecords = currentRecords.Where(
                    x => (x.Animal1.Nickname != null && x.Animal1.Nickname.ToLower().Contains(searchText))).ToList();
            }
            ListRecords.ItemsSource = currentRecords;
            if (az) ListRecords.ItemsSource = currentRecords.OrderBy(x => x.Animal1.Nickname).ToList();
            if (za) ListRecords.ItemsSource = currentRecords.OrderByDescending(x => x.Animal1.Nickname).ToList();

        }
    }
}
