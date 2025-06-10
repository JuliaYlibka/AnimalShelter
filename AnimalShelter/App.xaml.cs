using AnimalShelter.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AnimalShelter
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DispatcherTimer timer;
        private TimeSpan Threshold = TimeSpan.FromMinutes(7); // 7 минут бездействия
        private DateTime lastInputTime;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeTimer();

            // Подписка на глобальные события ввода
            EventManager.RegisterClassHandler(typeof(Window), UIElement.PreviewMouseMoveEvent, new MouseEventHandler(UserInputDetected), true);
            EventManager.RegisterClassHandler(typeof(Window), UIElement.PreviewKeyDownEvent, new KeyEventHandler(UserInputDetected), true);
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = Threshold;
            timer.Tick += Timer_Tick;
            timer.Start();
            lastInputTime = DateTime.Now;
        }

        private void UserInputDetected(object sender, EventArgs e)
        {
            lastInputTime = DateTime.Now;
            timer.Stop();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (Current.MainWindow is MainWindow main)
            {
                if (main.MainFrame.Content is Pages.AuthPage)
                {
                    timer.Start(); // Просто перезапускаем таймер
                    return;
                }

                MessageBox.Show("Вы были неактивны в течение последних 7 минут. Выполняется возврат на страницу авторизации.");

                // Закрытие всех окон кроме главного
                foreach (Window window in Current.Windows)
                {
                    if (window != Current.MainWindow)
                    {
                        window.Close();
                    }
                }

                main.MainFrame.Navigate(new Pages.AuthPage());
                main.MenuBar.Visibility = Visibility.Hidden;
                UserSession.UserPosition = string.Empty;
                UserSession.IDVolunteer = null;
            }

            timer.Start();
        }
    }
}
