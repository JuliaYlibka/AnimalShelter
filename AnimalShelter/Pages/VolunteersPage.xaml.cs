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
        private bool _only_FIZ;
        private bool _only_EYR;
        private bool az;
        private bool za;

        public VolunteersPage()
        {
            InitializeComponent(); 
            ListVolunteers.ItemsSource = volunteers;

        }

        private void ListVolunteers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void But_Email_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_Phone_Copy_Click(object sender, RoutedEventArgs e)
        {

        }


        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void But_Sort_Up_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_Sort_Down_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_Clear_Click(object sender, RoutedEventArgs e)
        {

        }


        private void But_Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
