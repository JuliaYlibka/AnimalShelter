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
    /// Логика взаимодействия для MedicalRecordsPage.xaml
    /// </summary>
    public partial class MedicalRecordsPage : Page
    {
        System.Collections.Generic.List<AnimalShelter.Medical_record> currentRecords = AnimalShelterEntities.GetContext().Medical_record.ToList();

        public MedicalRecordsPage()
        {
            InitializeComponent();

            currentRecords = AnimalShelterEntities.GetContext().Medical_record.ToList();

            ListRecords.ItemsSource = currentRecords;
        }
    }
}
