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
using AnimalShelter.Pages;

namespace AnimalShelter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            this.MaxHeight = double.PositiveInfinity;
            this.MaxWidth = double.PositiveInfinity;
            if (!(e.Content is Page page)) return;
            this.Title = $"Приют для животных: {page.Title}";
            if (e.Content is AuthPage)
            {
                this.Width = 1020;
                this.Height = 576;
                this.MaxHeight = 576;
                this.MaxWidth = 1020;
                this.MinHeight = 576;
                this.MinWidth = 1020;
            }
            //if (page is Pages.AuthPage)
            //    ButReturn.Visibility = Visibility.Hidden;
            //else
            //    ButReturn.Visibility = Visibility.Visible;
        }

        //private void ButReturn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MainFrame.CanGoBack) MainFrame.GoBack();
        //}
    }
}
