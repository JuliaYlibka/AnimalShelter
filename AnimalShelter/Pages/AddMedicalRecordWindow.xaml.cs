using AnimalShelter.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace AnimalShelter
{
    /// <summary>
    /// Логика взаимодействия для AddMedicalRecordWindow.xaml
    /// </summary>
    public partial class AddMedicalRecordWindow : Window
    {
        private Medical_record _current_medical_record = new Medical_record();
        private Medical_record _selected_medical_record;
        List<Animal> animals = AnimalShelterEntities.GetContext().Animal.ToList();
        private bool _isLoading = true; 


        public event Action Added;
        DateTime Today = DateTime.Today;
        public AddMedicalRecordWindow(Medical_record Selected_medical_record)
        {
            InitializeComponent();
            _selected_medical_record = Selected_medical_record;
            animals = AnimalShelterEntities.GetContext().Animal.ToList();
            var All_Medical_record = AnimalShelterEntities.GetContext().Medical_record.ToList();
            CB_Animal.ItemsSource = animals;
            if (Selected_medical_record != null)
            {
                _current_medical_record = Selected_medical_record;
                CB_Animal.SelectedItem = Selected_medical_record.Animal1;
                CB_Animal.IsEnabled = false;
            }
            else
            {
                // Фильтруем животных, у которых нет медицинских карт
                var context = AnimalShelterEntities.GetContext();

                animals = context.Animal
                    .Where(animal => !context.Medical_record
                        .Any(record => record.Animal == animal.ID_animal)).ToList();
                CB_Animal.ItemsSource = animals;
            }

            DataContext = _current_medical_record;
            this.Loaded += Page_Loaded;
            _isLoading = false;


        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Закрываем выпадающий список, если он открыт
            CB_Animal.IsDropDownOpen = false;
        }
        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            // Проверка на вид животного
            if (CB_Animal.SelectedItem == null)
                errors.AppendLine("Укажите животного!");
            else
                _current_medical_record.Animal = (int)CB_Animal.SelectedValue;

            if (!decimal.TryParse(TB_Height.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal height))
            {
                errors.AppendLine("Рост должен быть числом с точкой в качестве десятичного разделителя!");
            }

            else if (height <= 0 || height >= 500)
                errors.AppendLine("Рост не может быть меньше 0 и больше 500.");
            else
                _current_medical_record.Height = height;

            if (!decimal.TryParse(TB_Weight.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal weight))
            {
                errors.AppendLine("Вес должен быть числом с точкой в качестве десятичного разделителя!");
            }
            
            else if (weight <= 0 || weight >= 500)
                errors.AppendLine("Вес не может быть меньше 0 и 500.");
            else
                _current_medical_record.Weight = weight;

            _current_medical_record.Last_update_date = (DateTime)Today;

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current_medical_record.ID_medical_record == 0)
                AnimalShelterEntities.GetContext().Medical_record.Add(_current_medical_record);

            //Делаем попытку записи данных в БД
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                Added.Invoke();
                this.Close();
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

        private void CB_Animal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;

            if (_isLoading) return; // Prevent action if loading

            if (tb.SelectionStart != 0)
            {
                CB_Animal.SelectedItem = null; // Reset selected item if typing
            }

            if ((tb.SelectionStart == 0 && CB_Animal.SelectedItem == null) || CB_Animal.SelectedItem == _current_medical_record.Animal1)
            {
                CB_Animal.IsDropDownOpen = false; // Close dropdown if no selection
            }

            CB_Animal.IsDropDownOpen = true; // Open dropdown if item is not selected
            if (CB_Animal.SelectedItem == null)
            {
                // Filter the items based on the text
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Animal.ItemsSource);
                cv.Filter = s =>
                {
                    var _animal = s as Animal;
                    return _animal != null &&
                           (_animal.Nickname.IndexOf(CB_Animal.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }

            // Установите курсор в конец текста
            tb.SelectionStart = tb.Text.Length;
            tb.SelectionLength = 0; // Сброс выделения
        }

        private void OnlyNumbersAndDot_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                e.Handled = true;
                return;
            }

            string input = e.Text;

            // Разрешаем только цифры и точку
            if (!char.IsDigit(input, 0) && input != ".")
            {
                e.Handled = true;
                return;
            }

            // Если вводится точка, проверяем, есть ли она уже в тексте
            if (input == "." && textBox.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // Точка не может быть первой (курсору нужно быть > 0)
            if (input == "." && textBox.CaretIndex == 0)
            {
                e.Handled = true;
                return;
            }

            e.Handled = false; // Разрешаем ввод
        }

    }
}
