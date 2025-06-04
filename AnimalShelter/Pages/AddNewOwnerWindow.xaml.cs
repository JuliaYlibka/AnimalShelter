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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewOwnerWindow.xaml
    /// </summary>
    public partial class AddNewOwnerWindow : Window
    {
        private New_owner _current_New_owner = new New_owner();
        private New_owner _selected_New_owner;
        public event Action Added;
        DateTime minDate = DateTime.Today.AddYears(-90);
        public AddNewOwnerWindow(New_owner Selected_New_owner)
        {
            InitializeComponent();
            _selected_New_owner = Selected_New_owner;
            var AllTypesOfHouse = AnimalShelterEntities.GetContext().Housing_type.ToList();
            var AllTypesOfcontractor = AnimalShelterEntities.GetContext().Contractor_type.ToList();

            var All_genders = AnimalShelterEntities.GetContext().Gender.ToList();
            CB_Gender.ItemsSource = All_genders;
            CB_Housing_type.ItemsSource = AllTypesOfHouse;
            CB_Contractor_type.ItemsSource = AllTypesOfcontractor;




            if (Selected_New_owner != null)
            {
                _current_New_owner = Selected_New_owner;
                CB_Contractor_type.SelectedItem = Selected_New_owner.Contractor_type1;
                CB_Gender.SelectedItem = Selected_New_owner.Gender1;
                CB_Housing_type.SelectedItem = Selected_New_owner.Housing_type1;
                DP_Date_of_birth.SelectedDate = Selected_New_owner.Date_of_birth;


            }

            DataContext = _current_New_owner;
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на фамилию
            if (string.IsNullOrWhiteSpace(_current_New_owner.Surname))
                errors.AppendLine("Укажите фамилию!");
            else
                _current_New_owner.Surname = TB_Surname.Text.Trim();

            // Проверка на имя
            if (string.IsNullOrWhiteSpace(_current_New_owner.First_name))
                errors.AppendLine("Укажите имя!");
            else
                _current_New_owner.First_name = TB_First_name.Text.Trim();

            // Проверка на пол
            if (CB_Gender.SelectedValue == null)
                errors.AppendLine("Укажите пол!");
            else
                _current_New_owner.Gender = (int)CB_Gender.SelectedValue;
            if (CB_Contractor_type.SelectedValue == null)
                errors.AppendLine("Укажите тип владельца!");
            else
                _current_New_owner.Contractor_type = (int)CB_Contractor_type.SelectedValue;

            if (CB_Housing_type.SelectedValue == null)
                errors.AppendLine("Укажите тип жилья!");
            else
                _current_New_owner.Housing_type = (int)CB_Housing_type.SelectedValue;


            // Проверка на дату рождения
            if (DP_Date_of_birth.SelectedDate == null || DP_Date_of_birth.SelectedDate == DateTime.MinValue)

                errors.AppendLine("Укажите дату рождения!");
            else if (DP_Date_of_birth.SelectedDate > DateTime.Today || DP_Date_of_birth.SelectedDate < minDate)
            {
                errors.AppendLine("Некорректная дата рождения!");
            }
            else
                _current_New_owner.Date_of_birth = DP_Date_of_birth.SelectedDate.Value;

            // Проверка на номер телефона
            if (string.IsNullOrWhiteSpace(_current_New_owner.Phone_number))
                errors.AppendLine("Укажите номер телефона!");
            else
                _current_New_owner.Phone_number = TB_Phone_number.Text.Trim();

            // Проверка на email
            if (string.IsNullOrWhiteSpace(_current_New_owner.Email))
                errors.AppendLine("Укажите email!");
            else
                _current_New_owner.Email = TB_Email.Text.Trim();

            // Проверка на адрес
            if (string.IsNullOrWhiteSpace(_current_New_owner.Address))
                errors.AppendLine("Укажите адрес!");
            else
                _current_New_owner.Address = TB_Address.Text;

            _current_New_owner.Patronymic = TB_Patronymic.Text.Trim();

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового волонтера в базу данных
            if (_current_New_owner.ID_new_owner == 0)
                AnimalShelterEntities.GetContext().New_owner.Add(_current_New_owner);

            // Делаем попытку записи данных в БД о новом волонтере
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                Added?.Invoke();
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

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
