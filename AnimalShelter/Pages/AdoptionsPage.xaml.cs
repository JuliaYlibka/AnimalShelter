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
        public AdoptionsPage()
        {
            InitializeComponent();
            var Adoptions = AnimalShelterEntities.GetContext().Adoption.ToList();
            var Statuses = AnimalShelterEntities.GetContext().Adoption_status.ToList();
            var Animals = AnimalShelterEntities.GetContext().Animal.ToList();
            var Owners = AnimalShelterEntities.GetContext().New_owner.ToList();
            ListAdoptions.ItemsSource = Adoptions;

        }

        private void ListAdoptions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
