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
    /// Логика взаимодействия для AnimalPage.xaml
    /// </summary>
    public partial class AnimalPage : Page
    {
        public AnimalPage()
        {
            InitializeComponent();
            var allVolunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            CB_Volunteer.ItemsSource = allVolunteers;
        }

        private void CB_Volunteer_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                CB_Volunteer.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && CB_Volunteer.SelectedItem == null)
            {
                CB_Volunteer.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }

            CB_Volunteer.IsDropDownOpen = true;
            if (CB_Volunteer.SelectedItem == null)
            {
                // Если элемент не выбран менять фильтр
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Volunteer.ItemsSource);
                cv.Filter = s =>
                {
                    var volunteer = s as Volunteer; // Замените Volunteer на вашу модель волонтера
                    return volunteer != null &&
                           (volunteer.First_name.IndexOf(CB_Volunteer.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                            volunteer.Surname.IndexOf(CB_Volunteer.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }
        }

        private void But_Volunteer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
