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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            LoginTB.Focus();
        }

        private void ButAuth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTB.Text) || string.IsNullOrEmpty(PasswordAuth.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            using (var db = new AnimalShelterEntities())
            {
                var employees = db.Employee.AsNoTracking().ToList();
                var volunteers = db.Volunteer.AsNoTracking().ToList();

                var employee = employees.FirstOrDefault(em => em.Login == LoginTB.Text && em.Password == PasswordAuth.Password);
                var volunteer = volunteers.FirstOrDefault(v => v.Login == LoginTB.Text && v.Password == PasswordAuth.Password);

                if (employee != null)
                {
                    MessageBox.Show($"Добро пожаловать, {employee.First_name} {employee.Patronymic}!");
                    NavigationService?.Navigate(new AnimalsPage());
                    return;
                }

                if (volunteer != null)
                {
                    MessageBox.Show($"Добро пожаловать, {volunteer.First_name} {volunteer.Patronymic}!");
                    NavigationService?.Navigate(new AnimalsPage());
                    return;
                }

                MessageBox.Show("Пользователь с такими данными не найден!");
            }
        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintLogin.Visibility = Visibility.Visible;
            if (LoginTB.Text.Length > 0)
            {
                txtHintLogin.Visibility = Visibility.Hidden;
            }
        }

        private void PasswordAuth_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility = Visibility.Visible;
            if (PasswordAuth.Password.Length > 0)
            {
                txtHintPassword.Visibility = Visibility.Hidden;
            }
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, была ли нажата клавиша Enter
            if (e.Key == Key.Enter)
            {
                // Вызываем метод обработки нажатия кнопки "Вход"
                ButAuth_Click(sender, e);
            }
        }

        private void ButReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegistrationPage());
        }
    }
}
