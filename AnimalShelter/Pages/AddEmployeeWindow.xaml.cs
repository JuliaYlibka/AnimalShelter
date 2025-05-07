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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private Employee _current_employee = new Employee();
        private Employee _selected_employee;
        public event Action EmployeeAdded;
        DateTime minDate = DateTime.Today.AddYears(-90);
        public AddEmployeeWindow(Employee Selected_employee)
        {
            InitializeComponent();
            _selected_employee = Selected_employee;
            var All_volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            var All_genders = AnimalShelterEntities.GetContext().Gender.ToList();
            var All_positions = AnimalShelterEntities.GetContext().Position.ToList();
            CB_Gender.ItemsSource = All_genders;
            CB_Position.ItemsSource = All_positions;
            DP_Date_of_birth.SelectedDate = null;
            DP_Date_of_hire.SelectedDate = null;
            if (Selected_employee != null)
            {
                _current_employee = Selected_employee;
                CB_Position.SelectedItem = Selected_employee.Position1;
                CB_Gender.SelectedItem = Selected_employee.Gender1;
                TB_Surname.Text = Selected_employee.Surname;
                TB_First_name.Text = Selected_employee.First_name;
                TB_Patronymic.Text = Selected_employee.Patronymic;
                DP_Date_of_birth.SelectedDate = Selected_employee.Date_of_birth;
                DP_Date_of_hire.SelectedDate = Selected_employee.Date_of_hire;
                TB_Login.Text = Selected_employee.Login;
                TB_Address.Text = Selected_employee.Address;
                TB_Phone_number.Text = Selected_employee.Phone_number;
                TB_Email.Text = Selected_employee.Email;
                TB_Password.Visibility = Visibility.Hidden;
                Text_Password.Visibility = Visibility.Hidden;
                TB_Login.IsReadOnly = true;


            }

            DataContext = _current_employee;
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на фамилию
            if (string.IsNullOrWhiteSpace(_current_employee.Surname))
                errors.AppendLine("Укажите фамилию сотрудника!");
            else
                _current_employee.Surname = TB_Surname.Text.Trim();

            // Проверка на имя
            if (string.IsNullOrWhiteSpace(_current_employee.First_name))
                errors.AppendLine("Укажите имя сотрудника!");
            else
                _current_employee.First_name = TB_First_name.Text.Trim();

            // Проверка на пол
            if (CB_Gender.SelectedValue == null)
                errors.AppendLine("Укажите пол сотрудника!");
            else
                _current_employee.Gender = (int)CB_Gender.SelectedValue;
            if (CB_Position.SelectedValue == null)
                errors.AppendLine("Укажите должность сотрудника!");
            else
                _current_employee.Position = (int) CB_Position.SelectedValue;

            // Проверка на дату рождения
            if (DP_Date_of_birth.SelectedDate == null || DP_Date_of_birth.SelectedDate == DateTime.MinValue)

                errors.AppendLine("Укажите дату рождения сотрудника!");
            else if (DP_Date_of_birth.SelectedDate > DateTime.Today || DP_Date_of_birth.SelectedDate < minDate)
            {
                errors.AppendLine("Некорректная дата рождения сотрудника!");
            }
            else
                _current_employee.Date_of_birth = DP_Date_of_birth.SelectedDate.Value;

            // Проверка на номер телефона
            if (string.IsNullOrWhiteSpace(_current_employee.Phone_number))
                errors.AppendLine("Укажите номер телефона!");
            else
                    _current_employee.Phone_number = TB_Phone_number.Text.Trim();

            // Проверка на email
            if (string.IsNullOrWhiteSpace(_current_employee.Email))
                errors.AppendLine("Укажите email!");
            else
                _current_employee.Email = TB_Email.Text.Trim();

            // Проверка на адрес
            if (string.IsNullOrWhiteSpace(_current_employee.Address))
                errors.AppendLine("Укажите адрес!");
            else
                _current_employee       .Address = TB_Address.Text;

            // Проверка на дату найма
            if (DP_Date_of_hire.SelectedDate == null || DP_Date_of_hire.SelectedDate == DateTime.MinValue)
                errors.AppendLine("Укажите дату найма сотрудника!");
            else if (DP_Date_of_birth.SelectedDate > DateTime.Today || DP_Date_of_birth.SelectedDate < minDate)
            {
                errors.AppendLine("Некорректная дата найма сотрудника!");
            }
            else
                _current_employee.Date_of_hire = DP_Date_of_hire.SelectedDate.Value;

            // Проверка на логин
            if (string.IsNullOrWhiteSpace(_current_employee.Login))
                errors.AppendLine("Укажите логин!");
            else
                _current_employee.Login = TB_Login.Text;

            // Проверка на пароль
            if (_selected_employee == null)
            {
                if (string.IsNullOrWhiteSpace(TB_Password.Password))
                    errors.AppendLine("Укажите пароль!");
                else
                    _current_employee.Password = TB_Password.Password;
            }
            else _current_employee.Password = _selected_employee.Password;



            _current_employee.Patronymic = TB_Patronymic.Text.Trim();

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового волонтера в базу данных
            if (_current_employee.ID_employee == 0)
                AnimalShelterEntities.GetContext().Employee.Add(_current_employee);

            // Делаем попытку записи данных в БД о новом волонтере
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                EmployeeAdded?.Invoke();
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
