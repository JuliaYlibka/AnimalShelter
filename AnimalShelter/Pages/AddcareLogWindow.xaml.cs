using System;
using System.Collections;
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
    /// Логика взаимодействия для AddcareLogWindow.xaml
    /// </summary>
    public partial class AddcareLogWindow : Window
    {
        List<Volunteer> volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
        List<Employee> employees = AnimalShelterEntities.GetContext().Employee.ToList();
        List<Animal> animals = AnimalShelterEntities.GetContext().Animal.ToList();
        List<Care_type> types = AnimalShelterEntities.GetContext().Care_type.ToList();
        List<Task_status> statuses = AnimalShelterEntities.GetContext().Task_status.ToList();
        private Care_log _current = new Care_log();
        public event Action Added;
        DateTime today = DateTime.Today;


        AddVolunteerWindow _add_Volunteer_Window;
        AddEmployeeWindow _add_Employee_Window;



        public AddcareLogWindow(Care_log Selected)
        {
            InitializeComponent();
            volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            employees = AnimalShelterEntities.GetContext().Employee.ToList();
            animals = AnimalShelterEntities.GetContext().Animal.ToList();
            types = AnimalShelterEntities.GetContext().Care_type.ToList();
            statuses = AnimalShelterEntities.GetContext().Task_status.ToList();
            CB_Employee.ItemsSource = employees;
            CB_Animal.ItemsSource = animals;
            CB_Care_type.ItemsSource = types;
            CB_Volunteer.ItemsSource = volunteers;
            CB_Task_status.ItemsSource = statuses;
            if (Selected != null)
            {
                CB_Employee.SelectedItem = Selected.Employee1;
                CB_Animal.SelectedItem = Selected.Animal1;
                CB_Care_type.SelectedItem = Selected.Care_type1;
                CB_Volunteer.SelectedItem = Selected.Volunteer1;
                CB_Task_status.SelectedItem = Selected.Task_status1;
                _current = Selected;
                
            }
            DataContext = _current;
            if(UserSession.UserPosition== "Волонтёр" && _current.ID_care_log!=0)
            {
                CB_Employee.IsEnabled = false;
                CB_Animal.IsEnabled = false;
                CB_Care_type.IsEnabled = false;
                CB_Volunteer.IsEnabled = false;
                But_Add_Employee.Visibility=Visibility.Hidden;
                But_Add_Volunteer.Visibility = Visibility.Hidden;
            }
            else if(UserSession.UserPosition== "Волонтёр" && _current.ID_care_log==0)
            {
                CB_Employee.IsEnabled = false;
                CB_Animal.IsEnabled = true;
                CB_Care_type.IsEnabled = true;
                CB_Volunteer.IsEnabled = false;

                But_Add_Employee.Visibility = Visibility.Hidden;
                But_Add_Volunteer.Visibility = Visibility.Hidden;
                CB_Volunteer.SelectedItem = AnimalShelterEntities.GetContext().Volunteer.FirstOrDefault( v=> v.ID_volunteer== UserSession.IDVolunteer);
            }
            else
            {
                CB_Employee.IsEnabled = true;
                CB_Animal.IsEnabled =true;
                CB_Care_type.IsEnabled = true;
                CB_Volunteer.IsEnabled = true;

                But_Add_Employee.Visibility = Visibility.Visible;
                But_Add_Volunteer.Visibility = Visibility.Visible;
            }

            this.Loaded += Page_Loaded;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Закрываем выпадающий список, если он открыт
            CB_Animal.IsDropDownOpen = false;
        }
        private void But_Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_add_Employee_Window == null || !_add_Employee_Window.IsVisible)
            {
                _add_Employee_Window = new AddEmployeeWindow(null);
                _add_Employee_Window.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления сотрудника уже открыто.");
            }
        }

        private void But_Add_Volunteer_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_add_Volunteer_Window == null || !_add_Volunteer_Window.IsVisible)
            {
                _add_Volunteer_Window = new AddVolunteerWindow(null);
                _add_Volunteer_Window.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления волонтёра уже открыто.");
            }
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            // Проверка на тип пожертвования
            if (CB_Animal.SelectedValue == null)
                errors.AppendLine("Укажите животное!");
            else
                _current.Animal = (int)CB_Animal.SelectedValue;

            if (CB_Care_type.SelectedValue == null)
                errors.AppendLine("Укажите вид ухода!");
            else
                _current.Care_type = (int)CB_Care_type.SelectedValue;

            if (CB_Employee.SelectedValue != null)
                
                _current.Employee = (int)CB_Employee.SelectedValue;

            if (CB_Volunteer.SelectedValue == null)
                errors.AppendLine("Укажите волонтера!");
            else
                _current.Volunteer = (int)CB_Volunteer.SelectedValue;
            if (CB_Task_status.SelectedValue == null)
                errors.AppendLine("Укажите статус задачи!");
            else
                _current.Task_status = (int)CB_Task_status.SelectedValue;

            string comment = TB_Description.Text.Trim();
            if (!string.IsNullOrEmpty(comment))  _current.Comment = comment; 

            
            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового пожертвования в базу данных
            if (_current.ID_care_log == 0) { 
                _current.Date_care_log = DateTime.Now;

                AnimalShelterEntities.GetContext().Care_log.Add(_current);
            }

            // Делаем попытку записи данных в БД о новом пожертвовании
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

        private void CB_Animal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;

            if (tb.SelectionStart != 0)
            {
                CB_Animal.SelectedItem = null; // Reset selected item if typing
            }

            if ((tb.SelectionStart == 0 && CB_Animal.SelectedItem == null) || CB_Animal.SelectedItem == _current.Animal1)
            {
                CB_Animal.IsDropDownOpen = false; // Close dropdown if no selection
            }

            CB_Animal.IsDropDownOpen = true; // Open dropdown if item is not selected
            if (CB_Animal.SelectedItem == null)
            {
                // Filter the items based on the text
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Animal.ItemsSource);
                cv.Filter = s =>
                {
                    var _animal = s as Animal;
                    return _animal != null &&
                           (_animal.Nickname.IndexOf(CB_Animal.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }

            // Установите курсор в конец текста
            tb.SelectionStart = tb.Text.Length;
            tb.SelectionLength = 0; // Сброс выделения
        }
    }
}
