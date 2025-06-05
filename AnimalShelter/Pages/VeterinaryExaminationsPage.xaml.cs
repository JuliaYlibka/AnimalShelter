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
    /// Логика взаимодействия для VeterinaryExaminationsPage.xaml
    /// </summary>
    public partial class VeterinaryExaminationsPage : Page
    {
        List<Veterinary_examination> All_Veterinary_examinations = new List<Veterinary_examination>();
        private AddVetExaminationxaml _addWindow; // Переменная для хранения текущего окна

        public VeterinaryExaminationsPage()
        {
            InitializeComponent();
            if (UserSession.UserPosition != "Ветеринар" && UserSession.UserPosition != "Ассистент ветеринара")
                But_Add.Visibility = Visibility.Hidden;
            else
                But_Add.Visibility= Visibility.Visible;

            // Другие данные, если нужны
            var All_medical_record = AnimalShelterEntities.GetContext().Medical_record.ToList();
            var All_Animals = AnimalShelterEntities.GetContext().Animal.ToList();
            var All_Employees = AnimalShelterEntities.GetContext().Employee.Where(e=> e.Position1.ID_position == 2||e.Position1.ID_position==3).ToList();
            All_Animals.Insert(0, new Animal { Nickname = "Все" });
            All_Employees.Insert(0, new Employee { FullNameWithInitials = "Все", IsAllEmployees = true });
            // Устанавливаем источник данных для DataGrid
            CB_Employee.ItemsSource = All_Employees;
            CB_Animal.ItemsSource = All_Animals;
            CB_Animal.SelectedIndex = 0;
            CB_Employee.SelectedIndex = 0;
            Update();
        }

        private void Update()
        {
            // Загружаем все пожертвования в список
            All_Veterinary_examinations = AnimalShelterEntities.GetContext()
                            .Veterinary_examination
                            .OrderByDescending(x => x.ID_veterinary_examination)
                            .ToList();

            // Фильтрация по выбранному животному
            if (CB_Animal.SelectedItem != null && CB_Animal.SelectedIndex > 0) 
            {
                var selectedAnimal = (Animal)CB_Animal.SelectedItem;
                All_Veterinary_examinations = All_Veterinary_examinations
                    .Where(d => d.Medical_record1.Animal == selectedAnimal.ID_animal) 
                    .ToList();
            }

            // Фильтрация по выбранному ветеринару
            if (CB_Employee.SelectedItem != null && CB_Employee.SelectedIndex > 0) 
            {
                var selectedEmployee = (Employee)CB_Employee.SelectedItem;
                All_Veterinary_examinations = All_Veterinary_examinations
                    .Where(d => d.Employee.ID_employee == selectedEmployee.ID_employee) 
                    .ToList();
            }

            // Фильтрация по тексту поиска
            if (!string.IsNullOrWhiteSpace(TB_Search.Text))
            {
                string searchText = TB_Search.Text.ToLower();
                All_Veterinary_examinations = All_Veterinary_examinations.Where(d =>
                    (d.Conclusion != null && d.Conclusion.ToLower().Contains(searchText)|| 
                    d.Recommendation!=null && d.Recommendation.ToLower().Contains(searchText))
                ).ToList();
            }

            // Обновляем источник данных для DataGrid
             VetDataGrid.ItemsSource = All_Veterinary_examinations;
        }
      
        private void CB_Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void But_Clear_CB_Click(object sender, RoutedEventArgs e)
        {
            CB_Animal.SelectedIndex = 0;
            CB_Employee.SelectedIndex = 0;
            Update();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что окно не открыто
            if (_addWindow == null || !_addWindow.IsVisible)
            {
                _addWindow = new AddVetExaminationxaml();
                _addWindow.Added += Update; // Подписываемся на событие
                _addWindow.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления медицинской карты уже открыто.");
            }
        }

        private void CB_Animal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
