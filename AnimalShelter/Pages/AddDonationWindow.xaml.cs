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
using System.Windows.Shapes;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddDonationWindow.xaml
    /// </summary>
    public partial class AddDonationWindow : Window
    {
        public AddDonationWindow()
        {
            //TODO: закончить локигу добавления
            InitializeComponent();
            var All_type_of_Donations = AnimalShelterEntities.GetContext().Donation_type.ToList();
            var AllContractors = AnimalShelterEntities.GetContext().Contractor.ToList();
            var AllVolunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            CB_Contractor.ItemsSource = AllContractors;
            CB_Volunteer.ItemsSource = AllVolunteers;
            CB_Type_of_Donation.ItemsSource = All_type_of_Donations;
        }

        private void CB_Volunteer_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                CB_Volunteer.SelectedItem = null; // Если набирается текст сбросить выбранный элемент
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
                    var volunteer = s as Volunteer;
                    return volunteer != null &&
                           (volunteer.First_name.IndexOf(CB_Volunteer.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                            volunteer.Surname.IndexOf(CB_Volunteer.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }
        }

        private void CB_Contractor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                CB_Contractor.SelectedItem = null; // Если набирается текст сбросить выбранный элемент
            }
            if (tb.SelectionStart == 0 && CB_Contractor.SelectedItem == null)
            {
                CB_Contractor.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }

            CB_Contractor.IsDropDownOpen = true;
            if (CB_Contractor.SelectedItem == null)
            {
                // Если элемент не выбран менять фильтр
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Contractor.ItemsSource);
                cv.Filter = s =>
                {
                    var contractor = s as Contractor;
                    return contractor != null &&
                           (contractor.Name.IndexOf(CB_Contractor.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
