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
    /// Логика взаимодействия для CareLogsPage.xaml
    /// </summary>
    public partial class CareLogsPage : Page
    {
        List<Care_log> all_Logs = new List<Care_log>();
        AddcareLogWindow _add_Window;

        public CareLogsPage()
        {
            InitializeComponent();
            all_Logs = AnimalShelterEntities.GetContext().Care_log.ToList();
            var all_Types = AnimalShelterEntities.GetContext().Care_type.ToList();
            var all_Animals = AnimalShelterEntities.GetContext().Animal.ToList();
            var all_Statuses = AnimalShelterEntities.GetContext().Task_status.ToList();
            all_Types.Insert(0, new Care_type { Name_care_type = "Все" });
            all_Animals.Insert(0, new Animal { Nickname = "Все" });
            CB_Animal.ItemsSource = all_Animals;
            CB_Care_Type.ItemsSource = all_Types;
            CB_Animal.SelectedIndex = 0;
            CB_Care_Type.SelectedIndex = 0;
            ListAppointed.ItemsSource = all_Logs.Where(c => c.Task_status1.Name_task_status== "Назначено").ToList();
            ListDuring.ItemsSource = all_Logs.Where(c => c.Task_status1.Name_task_status== "В процессе").ToList();
            ListReady.ItemsSource = all_Logs.Where(c => c.Task_status1.Name_task_status== "Выполнено").ToList();

        }

        private void ListAppointed_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListAppointed.SelectedItem != null)
                {
                    var Selected = ListAppointed.SelectedItem as Care_log;

                    if (Selected != null)
                    {
                        if (_add_Window == null || !_add_Window.IsVisible)
                        {
                            _add_Window = new AddcareLogWindow(Selected);
                            _add_Window.Added += Update; // Подписываемся на событие
                            _add_Window.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления задачи уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найдено задачи для изменения.",
                                        "Warning",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void ListDuring_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListDuring.SelectedItem != null)
                {
                    var Selected = ListDuring.SelectedItem as Care_log;

                    if (Selected != null)
                    {
                        if (_add_Window == null || !_add_Window.IsVisible)
                        {
                            _add_Window = new AddcareLogWindow(Selected);
                            _add_Window.Added += Update; // Подписываемся на событие
                            _add_Window.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления задачи уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найдено задачи для изменения.",
                                        "Warning",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void ListReady_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ListReady.SelectedItem != null)
                {
                    var Selected = ListReady.SelectedItem as Care_log;

                    if (Selected != null)
                    {
                        if (_add_Window == null || !_add_Window.IsVisible)
                        {
                            _add_Window = new AddcareLogWindow(Selected);
                            _add_Window.Added += Update; // Подписываемся на событие
                            _add_Window.Show();
                        }
                        else
                        {
                            MessageBox.Show("Окно добавления задачи уже открыто.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не найдено задачи для изменения.",
                                        "Warning",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

        }

        private void CB_Animal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CB_Care_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }


        private void But_Clear_CB_Animal_Click(object sender, RoutedEventArgs e)
        {
            CB_Animal.SelectedIndex = 0;
            Update();
        }

        private void But_Clear_CB_Type_Click(object sender, RoutedEventArgs e)
        {
            CB_Care_Type.SelectedIndex = 0;
            Update();
        }
        private void Update()
        {
            all_Logs = AnimalShelterEntities.GetContext().Care_log.ToList();
            if(UserSession.UserPosition== "Волонтёр")
                all_Logs = all_Logs.Where(a=> a.Volunteer==(int)UserSession.IDVolunteer).ToList();

            if (CB_Animal.SelectedIndex > 0)
                all_Logs = all_Logs.Where(x => x.Animal ==(int) CB_Animal.SelectedValue).ToList();
            if(CB_Care_Type.SelectedIndex>0)
                all_Logs = all_Logs.Where(x => x.Care_type == (int)CB_Care_Type.SelectedValue).ToList();

            ListAppointed.ItemsSource = all_Logs.Where(c => c.Task_status1.Name_task_status == "Назначено").ToList();
            ListDuring.ItemsSource = all_Logs.Where(c => c.Task_status1.Name_task_status == "В процессе").ToList();
            ListReady.ItemsSource = all_Logs.Where(c => c.Task_status1.Name_task_status == "Выполнено").ToList();

        }
        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            if (_add_Window == null || !_add_Window.IsVisible)
            {
                _add_Window = new AddcareLogWindow(null);
                _add_Window.Added += Update; // Подписываемся на событие
                _add_Window.Show();
            }
            else
            {
                MessageBox.Show("Окно добавления задачи уже открыто.");
            }
        }
    }
}
