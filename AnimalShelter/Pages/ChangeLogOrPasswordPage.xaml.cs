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
    /// Логика взаимодействия для ChangeLogOrPasswordPage.xaml
    /// </summary>
    public partial class ChangeLogOrPasswordPage : Page
    {
        public ChangeLogOrPasswordPage()
        {
            InitializeComponent();
        }

        private void But_LEFT_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_old_login_LEFT.Text.Trim()) || string.IsNullOrEmpty(PB_Password_LEFT.Password.Trim()) || string.IsNullOrEmpty(TB_new_login_LEFT.Text.Trim()))
            {
                MessageBox.Show("Заполните все поля слева!");
                return;
            }
            else
            {

                using (var db = new AnimalShelterEntities())
                {
                    var employees = db.Employee.AsNoTracking().ToList();
                    var volunteers = db.Volunteer.AsNoTracking().ToList();

                    var employee = employees.FirstOrDefault(em => em.Login == TB_old_login_LEFT.Text && em.Password == PB_Password_LEFT.Password);
                    var volunteer = volunteers.FirstOrDefault(v => v.Login == TB_old_login_LEFT.Text && v.Password == PB_Password_LEFT.Password);

                    if (employee != null)
                    {
                        employee.Login = TB_new_login_LEFT.Text.Trim();
                        db.Entry(employee).State = System.Data.Entity.EntityState.Modified; // Устанавливаем состояние как измененное
                        db.SaveChanges(); // Сохраняем изменения в БД
                        MessageBox.Show("Логин успешно изменен для сотрудника!");
                        NavigationService?.Navigate(new AuthPage());
                    }

                    else if (volunteer != null)
                    {
                        volunteer.Login = TB_new_login_LEFT.Text.Trim();
                        db.Entry(volunteer).State = System.Data.Entity.EntityState.Modified; // Устанавливаем состояние как измененное
                        db.SaveChanges(); // Сохраняем изменения в БД
                        MessageBox.Show("Логин успешно изменен для волонтера!");
                        NavigationService?.Navigate(new AuthPage());
                    }
                    else MessageBox.Show("Пользователь с такими данными не найден!");
                }
            }
        }

        private void But_RIGHT_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_login_RIGHT.Text.Trim()) || string.IsNullOrEmpty(PB_Old_Password_RIGHT.Password.Trim()) || string.IsNullOrEmpty(PB_new_Password_RIGHT.Password.Trim()))
            {
                MessageBox.Show("Заполните все поля справа!");
                return;
            }
            else
            {

                using (var db = new AnimalShelterEntities())
                {
                    var employees = db.Employee.AsNoTracking().ToList();
                    var volunteers = db.Volunteer.AsNoTracking().ToList();

                    var employee = employees.FirstOrDefault(em => em.Login == TB_login_RIGHT.Text && em.Password == PB_Old_Password_RIGHT.Password);
                    var volunteer = volunteers.FirstOrDefault(v => v.Login == TB_login_RIGHT.Text && v.Password == PB_Old_Password_RIGHT.Password);

                    if (employee != null)
                    {
                        employee.Password = PB_new_Password_RIGHT.Password.Trim();
                        db.Entry(employee).State = System.Data.Entity.EntityState.Modified; // Устанавливаем состояние как измененное
                        db.SaveChanges(); // Сохраняем изменения в БД
                        MessageBox.Show("Пароль успешно изменен для сотрудника!");
                        NavigationService?.Navigate(new AuthPage());
                    }

                    else if (volunteer != null)
                    {
                        volunteer.Password = PB_new_Password_RIGHT.Password.Trim();

                        db.Entry(volunteer).State = System.Data.Entity.EntityState.Modified; // Устанавливаем состояние как измененное
                        db.SaveChanges(); // Сохраняем изменения в БД
                        MessageBox.Show("Пароль успешно изменен для волонтера!");
                        NavigationService?.Navigate(new AuthPage());
                    }
                    else MessageBox.Show("Пользователь с такими данными не найден!");
                }
            }
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AuthPage());
        }
    }
}
