using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            string oldLogin = TB_old_login_LEFT.Text.Trim();
            string password = PB_Password_LEFT.Password.Trim();
            string newLogin = TB_new_login_LEFT.Text.Trim();

            if (string.IsNullOrEmpty(oldLogin) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newLogin))
            {
                MessageBox.Show("Заполните все поля слева!");
                return;
            }

            if (oldLogin == newLogin)
            {
                MessageBox.Show("Новый логин не должен совпадать со старым!");
                return;
            }

            else { 
                using (var db = new AnimalShelterEntities())
                {
                    var employees = db.Employee.AsNoTracking().ToList();
                    var volunteers = db.Volunteer.AsNoTracking().ToList();

                    // Проверка, существует ли уже новый логин
                    bool loginExists = employees.Any(em => em.Login == newLogin) || volunteers.Any(v => v.Login == newLogin);
                    if (loginExists)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!");
                        return;
                    }

                    var employee = employees.FirstOrDefault(em => em.Login == oldLogin && em.Password == GetHash(password));
                    var volunteer = volunteers.FirstOrDefault(v => v.Login == oldLogin && v.Password == GetHash(password));

                    if (employee != null)
                    {
                        employee.Login = newLogin;
                        db.Entry(employee).State = System.Data.Entity.EntityState.Modified; // Устанавливаем состояние как измененное
                        db.SaveChanges(); // Сохраняем изменения в БД
                        MessageBox.Show("Логин успешно изменен для сотрудника!");
                        NavigationService?.Navigate(new AuthPage());
                    }

                    else if (volunteer != null)
                    {
                        volunteer.Login = newLogin;
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
            string login = TB_login_RIGHT.Text.Trim();
            string oldPassword = PB_Old_Password_RIGHT.Password.Trim();
            string newPassword = PB_new_Password_RIGHT.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Заполните все поля справа!");
                return;
            }

            if (oldPassword == newPassword)
            {
                MessageBox.Show("Новый пароль не должен совпадать со старым!");
                return;
            }

            else
            {

                using (var db = new AnimalShelterEntities())
                {
                    var employees = db.Employee.AsNoTracking().ToList();
                    var volunteers = db.Volunteer.AsNoTracking().ToList();

                    var employee = employees.FirstOrDefault(em => em.Login == login && em.Password == GetHash(oldPassword));
                    var volunteer = volunteers.FirstOrDefault(v => v.Login == login && v.Password == GetHash(oldPassword));

                    if (employee != null)
                    {
                        employee.Password = GetHash(newPassword).Trim();
                        db.Entry(employee).State = System.Data.Entity.EntityState.Modified; // Устанавливаем состояние как измененное
                        db.SaveChanges(); // Сохраняем изменения в БД
                        MessageBox.Show("Пароль успешно изменен для сотрудника!");
                        NavigationService?.Navigate(new AuthPage());
                    }

                    else if (volunteer != null)
                    {
                        volunteer.Password = GetHash(newPassword).Trim();
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

        public static string GetHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                using (var hash = SHA1.Create())
                {
                    return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x
                        => x.ToString("X2")));
                }
            }
        }
    }
}
