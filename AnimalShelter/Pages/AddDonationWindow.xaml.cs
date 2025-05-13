using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        private Donation _current_Donation = new Donation();
        private Donation _selected_Donation;
        public event Action DonationAdded;
        private bool _ignore_Amount = false;
        AddVolunteerWindow _add_Volunteer_Window;
        AddContractorWindow _add_Contractor_Window;

        public AddDonationWindow(Donation Selected_Donation)
        {
            InitializeComponent();
            _selected_Donation = Selected_Donation;
            if (_current_Donation.Date_of_donation == DateTime.MinValue)
            {
                _current_Donation.Date_of_donation = DateTime.Today; 
            }
            var All_type_of_Donations = AnimalShelterEntities.GetContext().Donation_type.ToList();
            var AllContractors = AnimalShelterEntities.GetContext().Contractor.ToList();
            var AllVolunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            CB_Contractor.ItemsSource = AllContractors;
            CB_Volunteer.ItemsSource = AllVolunteers;
            CB_Type_of_Donation.ItemsSource = All_type_of_Donations;
            

            if (Selected_Donation != null)
            {
                _current_Donation = Selected_Donation;

                CB_Volunteer.SelectedItem = Selected_Donation.Volunteer1;
                CB_Contractor.SelectedItem = Selected_Donation.Contractor1;
                CB_Type_of_Donation.SelectedItem = Selected_Donation.Donation_type1;
                DP_Create_Donation.SelectedDate = Selected_Donation.Date_of_donation;
                if(Selected_Donation.Amount!=null) TB_Amount.Text = Selected_Donation.Amount.ToString();
                if (Selected_Donation.Description != null) TB_Description.Text = Selected_Donation.Description.ToString();
            }
            DataContext = _current_Donation;
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
            StringBuilder errors = new StringBuilder();

            // Проверка на дату пожертвования
            if (DP_Create_Donation.SelectedDate == null || DP_Create_Donation.SelectedDate == DateTime.MinValue)
                errors.AppendLine("Укажите дату пожертвования!");
            else
                _current_Donation.Date_of_donation = DP_Create_Donation.SelectedDate.Value;

            // Проверка на тип пожертвования
            if (CB_Type_of_Donation.SelectedValue == null)
                errors.AppendLine("Укажите тип пожертвования!");
            else
                _current_Donation.Donation_type = (int)CB_Type_of_Donation.SelectedValue;
           
            decimal amount=0;
            if (!_ignore_Amount)
            {
                if (string.IsNullOrWhiteSpace(TB_Amount.Text) || !decimal.TryParse(TB_Amount.Text, out amount) || amount <= 0)
                {
                    errors.AppendLine("Укажите корректную сумму пожертвования!");
                }
                else
                {
                    _current_Donation.Amount = amount;
                }
            }
            else _current_Donation.Amount = null;

            _current_Donation.Description = TB_Description.Text.Trim();

            // Проверка на контрагента
            if ((CB_Contractor.SelectedValue == null && CB_Volunteer.SelectedValue == null) ||
                (CB_Contractor.SelectedValue != null && CB_Volunteer.SelectedValue != null))
            {
                errors.AppendLine("Инвестор должен быть заполнен. Это может быть либо волонтёр, либо контрагент!");
            }
            else
            {
                // Если вы выбрали волонтера, сохраняем его
                if (CB_Volunteer.SelectedValue != null)
                {
                    _current_Donation.Volunteer = (int)CB_Volunteer.SelectedValue;
                }
                // Если вы выбрали контрагента, сохраняем его
                else if (CB_Contractor.SelectedValue != null)
                {
                    _current_Donation.Contractor = (int)CB_Contractor.SelectedValue;
                }
            }
            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового пожертвования в базу данных
            if (_current_Donation.ID_donation == 0)
                AnimalShelterEntities.GetContext().Donation.Add(_current_Donation);

            // Делаем попытку записи данных в БД о новом пожертвовании
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                DonationAdded?.Invoke();
                Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Ошибка обновления: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void But_Add_Volunteer_Click(object sender, RoutedEventArgs e)
        {

            // Проверка, что окно не открыто
            if (_add_Volunteer_Window == null || !_add_Volunteer_Window.IsVisible)
            {
                _add_Volunteer_Window = new AddVolunteerWindow(null);
                _add_Volunteer_Window.VolunteerAdded += Update; // Подписываемся на событие
                _add_Volunteer_Window.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления волонтёра уже открыто.");
            }
        }

        private void But_Add_Contractor_Click(object sender, RoutedEventArgs e)
        {
            if (_add_Contractor_Window == null || !_add_Contractor_Window.IsVisible)
            {
                _add_Contractor_Window = new AddContractorWindow(null);
                _add_Contractor_Window.ContractorAdded += Update; // Подписываемся на событие
                _add_Contractor_Window.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления контрагента уже открыто.");
            }
        }

        private void CB_Type_of_Donation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CB_Type_of_Donation.SelectedIndex != 0)
            {
                TB_Amount.Cursor = Cursors.No;
                TB_Amount.IsReadOnly = true;
                _ignore_Amount=true;
            }
            else
            {
                TB_Amount.Cursor = Cursors.Arrow; TB_Amount.IsReadOnly = false; _ignore_Amount = false;
            }
        }
        private void Update()
        {
            // Обновление списков волонтеров и контрагентов
            var AllContractors = AnimalShelterEntities.GetContext().Contractor.ToList();
            var AllVolunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            CB_Contractor.ItemsSource = AllContractors;
            CB_Volunteer.ItemsSource = AllVolunteers;
        }
    }
}
