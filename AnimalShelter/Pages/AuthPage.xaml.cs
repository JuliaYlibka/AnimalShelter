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
            //TODO: добавить изменение пароля пользователем. вариант 3. ссылка Изменить пароль? Ввод старого пароля, нового и сохранение в бд. Возможно добавить изменение логина, с тем же подходом.
        }

        private void ButAuth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTB.Text.Trim()) || string.IsNullOrEmpty(PasswordAuth.Password.Trim()))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }
            else { 

            using (var db = new AnimalShelterEntities())
            {
                var employees = db.Employee.AsNoTracking().ToList();
                var volunteers = db.Volunteer.AsNoTracking().ToList();

                var employee = employees.FirstOrDefault(em => em.Login == LoginTB.Text && em.Password == PasswordAuth.Password);
                var volunteer = volunteers.FirstOrDefault(v => v.Login == LoginTB.Text && v.Password == PasswordAuth.Password);

                if (employee != null)
                {
                    MessageBox.Show($"Добро пожаловать, {employee.First_name} {employee.Patronymic}!");
                    UserSession.UserPosition = employee.Position1.Name_position; 
                        if(UserSession.UserPosition== "Ветеринар" || UserSession.UserPosition == "Ассистент ветеринара") NavigationService?.Navigate(new MedicalRecordsPage());
                        else NavigationService?.Navigate(new AnimalsPage());
                    return;
                }

                else if (volunteer != null)
                {
                    MessageBox.Show($"Добро пожаловать, {volunteer.First_name} {volunteer.Patronymic}!");
                    UserSession.UserPosition = "Волонтёр";
                    UserSession.IDVolunteer = volunteer.ID_volunteer;
                    NavigationService?.Navigate(new AnimalsPage());
                    return;
                }
                else MessageBox.Show("Пользователь с такими данными не найден!");
            }
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
    }
}
