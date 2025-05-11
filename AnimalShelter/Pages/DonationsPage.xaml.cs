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
            var All_Donations = AnimalShelterEntities.GetContext().Donation.ToList();
            var All_Contractors = AnimalShelterEntities.GetContext().Contractor.ToList();
            var All_Volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            var All_Type_Donations = AnimalShelterEntities.GetContext().Donation_type.ToList();
            All_Type_Donations.Insert(0, new Donation_type { Name_donation_type = "Все" });
            DonationDataGrid.ItemsSource = All_Donations;
            CB_Type_Donate.ItemsSource = All_Type_Donations;
            CB_Type_Person.SelectedIndex = 0;
            CB_Type_Donate.SelectedIndex = 0;

        }

        private void DonationDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO: изменение пожертвования
        }

        private void DP_Start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void DP_End_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            //TODO: добавление пожертвования
            AddDonationWindow addwindow = new AddDonationWindow();
            addwindow.Show();

        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void CB_Type_Donate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CB_Type_Person_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();

        }

        private void Update()
        {
            // Загружаем все пожертвования в список
            var All_Donations = AnimalShelterEntities.GetContext().Donation.ToList();

            // Фильтрация по дате начала
            if (DP_Start.SelectedDate.HasValue)
            {
                DateTime startDate = DP_Start.SelectedDate.Value;
                All_Donations = All_Donations.Where(d => d.Date_of_donation >= startDate).ToList();
            }

            // Фильтрация по дате окончания
            if (DP_End.SelectedDate.HasValue)
            {
                DateTime endDate = DP_End.SelectedDate.Value;
                All_Donations = All_Donations.Where(d => d.Date_of_donation <= endDate).ToList();
            }

            // Фильтрация по типу вносителя
            if (CB_Type_Person.SelectedIndex > 0) // Пропустить "Все"
            {
                string selectedType = CB_Type_Person.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;

                if (selectedType == "Контрагент")
                {
                    All_Donations = All_Donations.Where(d => d.Type == "Контрагент").ToList();
                }
                else if (selectedType == "Волонтёр")
                {
                    All_Donations = All_Donations.Where(d => d.Type == "Волонтёр").ToList();
                }
            }

            // Фильтрация по типу пожертвования
            if (CB_Type_Donate.SelectedIndex != 0) // Пропустить "Все"
            {
                var selectedDonationType = (Donation_type)CB_Type_Donate.SelectedItem;
                if (selectedDonationType != null) // Проверка на null
                {
                    All_Donations = All_Donations.Where(d => d.Donation_type1.ID_donation_type == selectedDonationType.ID_donation_type).ToList();
                }
            }

            // Фильтрация по тексту поиска
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                All_Donations = All_Donations.Where(d =>
                    (d.PersonName != null && d.PersonName.ToLower().Contains(searchText)) ||
                    (d.Contractor1 != null &&  d.Contractor1.Phone_number != null && d.Contractor1.Phone_number.ToLower().Contains(searchText)) ||
                    (d.Contractor1 != null && d.Contractor1.Email != null && d.Contractor1.Email.ToLower().Contains(searchText)) ||
                    (d.Volunteer1 != null && d.Volunteer1.Phone_number != null && d.Volunteer1.Phone_number.ToLower().Contains(searchText)) ||
                    (d.Volunteer1 != null && d.Volunteer1.Email != null && d.Volunteer1.Email.ToLower().Contains(searchText)) ||
                    (d.Description != null && d.Description.ToLower().Contains(searchText))
                ).ToList();
            }

            // Обновляем источник данных для DataGrid
            DonationDataGrid.ItemsSource = All_Donations;
        }


        private void But_Clear_CB_Click(object sender, RoutedEventArgs e)
        {
            CB_Type_Donate.SelectedIndex = 0;
            CB_Type_Person.SelectedIndex = 0;
            Update();
        }

        private void But_Clear_Date_Click(object sender, RoutedEventArgs e)
        {
            DP_End.SelectedDate = null;
            DP_Start.SelectedDate = null;
            Update();
        }
    }
}
