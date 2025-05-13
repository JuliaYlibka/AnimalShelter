using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AnimalShelter.Pages;

namespace AnimalShelter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected override void OnClosing(CancelEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите выйти из приложения?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
                e.Cancel = true;
        }

        public MainWindow()
        {
            InitializeComponent();

            //TODO: ПОСЛЕ РАЗРАБОТКИ изменить титлы страниц на русский язык согласно требованиям. проверить разметку на страницах вывода списка, поле фильтрация немного бродит!
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            this.MaxHeight = double.PositiveInfinity;
            this.MaxWidth = double.PositiveInfinity;

            if (!(e.Content is Page page)) return;
            this.Title = $"Приют для животных: {page.Title}";

            if (e.Content is AuthPage)
            {
                MenuBar.Visibility = Visibility.Hidden;
                this.Width = 1020;
                this.Height = 576;
                this.ResizeMode = ResizeMode.NoResize;

            }
            //if (page is Pages.AuthPage)
            //    ButReturn.Visibility = Visibility.Hidden;
            else {
                MenuBar.Visibility= Visibility.Visible;
                this.ResizeMode = ResizeMode.CanResize;

            }
            //ButReturn.Visibility = Visibility.Visible;
        }


        private void Back_But_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) 
                MainFrame.GoBack();
        }

        private void Animals_Click(object sender, RoutedEventArgs e) 
        {
            MainFrame.Navigate(new AnimalsPage());
        }

        private void Veterinary_examination_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Medical_record_Click(object sender, RoutedEventArgs e) {
            
            MainFrame.Navigate(new MedicalRecordsPage());
            
        }

        private void Care_log_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Breeds_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BreedsPage());

        }

        private void Source_of_receipt_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Source_of_receipt_Page());

        }

        private void Contractor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ContractorsPage());

        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeesPage());
            
        }

        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new VolunteersPage());

        }

        private void New_owner_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OwnersPage());

        }

        private void Donation_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DonationsPage());

        }

        private void Adoption_Click(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new AdoptionsPage());

        }

    }
}
