using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
using System.Windows.Shapes;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для SourceAddWindow.xaml
    /// </summary>
    public partial class SourceAddWindow : Window
    {
        private Source_of_receipt _current_source = new Source_of_receipt();
        public event Action SourceAdded;
        public SourceAddWindow(Source_of_receipt Selected_Source)
        {
            InitializeComponent();
            List<Source_of_receipt> All_Sources = AnimalShelterEntities.GetContext().Source_of_receipt.ToList();
            Title_TB.Text = "Добавление данных об источнике";
            if (Selected_Source != null)
            {
                _current_source = Selected_Source;
                TB_Name.Text = Selected_Source.Name_source_of_receipt;
                Title_TB.Text = "Изменение данных об источнике";

            }
            DataContext = _current_source;
        }

        private void But_Add_Source_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на название породы
            if (string.IsNullOrWhiteSpace(_current_source.Name_source_of_receipt))
                errors.AppendLine("Укажите название источника!");
            else _current_source.Name_source_of_receipt = TB_Name.Text.Trim();

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current_source.ID_source_of_receipt == 0)
                AnimalShelterEntities.GetContext().Source_of_receipt.Add(_current_source);

            //Делаем попытку записи данных в БД о новом пользователе
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                SourceAdded?.Invoke();
                Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Ошибка обновления: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnlyLetters_PreviewKeyDown(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsLetter))
            {
                e.Handled = true;
            }
        }
    }
}
