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
using System.Windows.Threading;
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

            if (e.Content is AuthPage || e.Content is ChangeLogOrPasswordPage)
            {
                MenuBar.Visibility = Visibility.Hidden;
            }
            else {
                MenuBar.Visibility= Visibility.Visible;
            }

            if (!(e.Content is AuthPage))
            {
                switch (UserSession.UserPosition)
                {
                    case "Ветеринар":
                        AnimalsMenu.Visibility = Visibility.Collapsed;
                        ContractorsMenu.Visibility = Visibility.Collapsed;
                        Donation.Visibility = Visibility.Collapsed;
                        Adoption.Visibility = Visibility.Collapsed;
                        Animals_VET.Visibility = Visibility.Collapsed;
                        Volunteer_VET.Visibility = Visibility.Collapsed;
                        Medical_record_VET.Visibility = Visibility.Visible;
                        Veterinary_examination_VET.Visibility = Visibility.Visible;



                        break;
                    case "Ассистент ветеринара":
                        AnimalsMenu.Visibility = Visibility.Collapsed;
                        ContractorsMenu.Visibility = Visibility.Collapsed;
                        Donation.Visibility = Visibility.Collapsed;
                        Adoption.Visibility = Visibility.Collapsed;
                        Animals_VET.Visibility = Visibility.Collapsed;
                        Volunteer_VET.Visibility = Visibility.Collapsed;


                        Medical_record_VET.Visibility = Visibility.Visible;
                        Veterinary_examination_VET.Visibility = Visibility.Visible;


                        break;
                    case "Куратор":
                        AnimalsMenu.Visibility = Visibility.Collapsed;
                        ContractorsMenu.Visibility = Visibility.Collapsed;
                        Donation.Visibility = Visibility.Collapsed;
                        Adoption.Visibility = Visibility.Collapsed;


                        Animals_VET.Visibility = Visibility.Visible;
                        Care_log_VET.Visibility = Visibility.Visible;
                        Volunteer_VET.Visibility= Visibility.Visible;

                        Medical_record_VET.Visibility = Visibility.Collapsed;
                        Veterinary_examination_VET.Visibility = Visibility.Collapsed;

                        break;
                    case "Волонтёр":
                        AnimalsMenu.Visibility = Visibility.Collapsed;
                        ContractorsMenu.Visibility = Visibility.Collapsed;
                        Donation.Visibility = Visibility.Collapsed;
                        Adoption.Visibility = Visibility.Collapsed;


                        Animals_VET.Visibility = Visibility.Visible;
                        Care_log_VET.Visibility = Visibility.Visible;
                        Volunteer_VET.Visibility= Visibility.Collapsed;

                        Medical_record_VET.Visibility = Visibility.Collapsed;
                        Veterinary_examination_VET.Visibility = Visibility.Collapsed;

                        break;
                    default:
                        AnimalsMenu.Visibility = Visibility.Visible;
                        ContractorsMenu.Visibility = Visibility.Visible;
                        Donation.Visibility = Visibility.Visible;
                        Adoption.Visibility = Visibility.Visible;

                        Animals_VET.Visibility = Visibility.Collapsed;

                        Medical_record_VET.Visibility = Visibility.Collapsed;
                        Veterinary_examination_VET.Visibility = Visibility.Collapsed;
                        Care_log_VET.Visibility = Visibility.Collapsed;
                        Volunteer_VET.Visibility = Visibility.Collapsed;
                        break;


                }
            }
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
            MainFrame.Navigate(new VeterinaryExaminationsPage());
        }

        private void Medical_record_Click(object sender, RoutedEventArgs e) {
            
            MainFrame.Navigate(new MedicalRecordsPage());
            
        }

        private void Care_log_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CareLogsPage());

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

        private void Log_out_But_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AuthPage());
            UserSession.UserPosition = string.Empty;
            UserSession.IDVolunteer = null;

        }
    }
}
