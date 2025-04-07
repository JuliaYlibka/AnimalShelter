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
                var users = db.Employee.AsNoTracking().ToList();

                var user = users.FirstOrDefault(u => u.Login == LoginTB.Text && u.Password == PasswordAuth.Password);

                if (user == null)
                {
                    MessageBox.Show("Пользователя с такими данными не найден!");
                    return;
                }

                MessageBox.Show("Пользователь успешно найден!");

                NavigationService?.Navigate(new AnimalsPage());  

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
    }
}
