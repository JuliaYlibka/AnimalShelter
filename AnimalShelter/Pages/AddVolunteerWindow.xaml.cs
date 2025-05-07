using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AddVolunteerWindow.xaml
    /// </summary>
    public partial class AddVolunteerWindow : Window
    {
        private Volunteer _current_volunteer = new Volunteer();
        private Volunteer _selected_volunteer;
        public event Action VolunteerAdded;
        DateTime minDate = DateTime.Today.AddYears(-90);
        public AddVolunteerWindow( Volunteer Selected_volunteer)
        {
            InitializeComponent();
            _selected_volunteer = Selected_volunteer;
            var All_volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            var All_genders = AnimalShelterEntities.GetContext().Gender.ToList();
            CB_Gender.ItemsSource = All_genders;
            DP_Date_of_birth.SelectedDate = null;
            DP_Date_of_hire.SelectedDate = null;
            if (Selected_volunteer != null)
            {
                _current_volunteer = Selected_volunteer;
                CB_Gender.SelectedItem = Selected_volunteer.Gender1;
                TB_Surname.Text = Selected_volunteer.Surname;
                TB_First_name.Text = Selected_volunteer.First_name;
                TB_Patronymic.Text = Selected_volunteer.Patronymic;
                DP_Date_of_birth.SelectedDate = Selected_volunteer.Date_of_birth;
                DP_Date_of_hire.SelectedDate = Selected_volunteer.Date_of_hire;
                TB_Login.Text = Selected_volunteer.Login;
                TB_Address.Text =  Selected_volunteer.Address;
                TB_Phone_number.Text = Selected_volunteer.Phone_number;
                TB_Email.Text = Selected_volunteer.Email;
                TB_Password.Visibility = Visibility.Hidden;
                Text_Password.Visibility = Visibility.Hidden;
                TB_Login.IsReadOnly = true;
            }

            DataContext = _current_volunteer;
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на фамилию
            if (string.IsNullOrWhiteSpace(_current_volunteer.Surname))
                errors.AppendLine("Укажите фамилию волонтёра!");
            else
                _current_volunteer.Surname = TB_Surname.Text.Trim();

            // Проверка на имя
            if (string.IsNullOrWhiteSpace(_current_volunteer.First_name))
                errors.AppendLine("Укажите имя волонтёра!");
            else
                _current_volunteer.First_name = TB_First_name.Text.Trim();

            // Проверка на пол
            if (CB_Gender.SelectedValue == null)
                errors.AppendLine("Укажите пол волонтёра!");
            else
                _current_volunteer.Gender = (int)CB_Gender.SelectedValue;

            // Проверка на дату рождения
            if (DP_Date_of_birth.SelectedDate == null || DP_Date_of_birth.SelectedDate == DateTime.MinValue)

                errors.AppendLine("Укажите дату рождения волонтёра!");
            else if (DP_Date_of_birth.SelectedDate > DateTime.Today || DP_Date_of_birth.SelectedDate<minDate)
            {
                errors.AppendLine("Некорректная дата рождения волонтёра!");
            }
            else
                _current_volunteer.Date_of_birth = DP_Date_of_birth.SelectedDate.Value;

            // Проверка на номер телефона
            if (string.IsNullOrWhiteSpace(_current_volunteer.Phone_number))
                errors.AppendLine("Укажите номер телефона!");
            else
                _current_volunteer.Phone_number = TB_Phone_number.Text.Trim();

            // Проверка на email
            if (string.IsNullOrWhiteSpace(_current_volunteer.Email))
                errors.AppendLine("Укажите email!");
            else
                _current_volunteer.Email = TB_Email.Text.Trim();

            // Проверка на адрес
            if (string.IsNullOrWhiteSpace(_current_volunteer.Address))
                errors.AppendLine("Укажите адрес!");
            else
                _current_volunteer.Address = TB_Address.Text;

            // Проверка на дату найма
            if (DP_Date_of_hire.SelectedDate == null || DP_Date_of_hire.SelectedDate == DateTime.MinValue)
                errors.AppendLine("Укажите дату найма волонтёра!");
            else if (DP_Date_of_birth.SelectedDate > DateTime.Today || DP_Date_of_birth.SelectedDate < minDate)
            {
                errors.AppendLine("Некорректная дата найма волонтёра!");
            }
            else
                _current_volunteer.Date_of_hire = DP_Date_of_hire.SelectedDate.Value;

            // Проверка на логин
            if (string.IsNullOrWhiteSpace(_current_volunteer.Login))
                errors.AppendLine("Укажите логин!");
            else
                _current_volunteer.Login = TB_Login.Text;

            // Проверка на пароль
            if (_selected_volunteer == null)
            {
                if (string.IsNullOrWhiteSpace(TB_Password.Password))
                    errors.AppendLine("Укажите пароль!");
                else
                    _current_volunteer.Password = TB_Password.Password;
            }
            else _current_volunteer.Password = _selected_volunteer.Password;



            _current_volunteer.Patronymic = TB_Patronymic.Text.Trim();

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового волонтера в базу данных
            if (_current_volunteer.ID_volunteer == 0)
                AnimalShelterEntities.GetContext().Volunteer.Add(_current_volunteer);

            // Делаем попытку записи данных в БД о новом волонтере
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                VolunteerAdded?.Invoke();
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
    }
}
