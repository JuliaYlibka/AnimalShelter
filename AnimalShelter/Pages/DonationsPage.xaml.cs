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
    /// Логика взаимодействия для DonationsPage.xaml
    /// </summary>
    public partial class DonationsPage : Page
    {
        public DonationsPage()
        {
            InitializeComponent();
            var don= AnimalShelterEntities.GetContext().Donation.ToList();
            var All_Contractors = AnimalShelterEntities.GetContext().Contractor.ToList();
            var All_Volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();

            DonationDataGrid.ItemsSource = don;
            
        }

        private void DonationDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
