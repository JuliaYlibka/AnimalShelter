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
    /// Логика взаимодействия для AddVetExaminationxaml.xaml
    /// </summary>
    public partial class AddVetExaminationxaml : Window
    {
        public event Action Added;
        private Veterinary_examination _current_Veterinary_examination = new Veterinary_examination();
        private bool _isLoading = true;
        List<Medical_record> All_Medical_records = new List<Medical_record>(); 

        List<Animal> animals = AnimalShelterEntities.GetContext().Animal.ToList();
        DateTime Today = DateTime.Today;

        public AddVetExaminationxaml()
        {
            InitializeComponent();
            animals = AnimalShelterEntities.GetContext().Animal.ToList();
            All_Medical_records = AnimalShelterEntities.GetContext().Medical_record.ToList();
            CB_Animal.ItemsSource = animals;

            DataContext = _current_Veterinary_examination;
            this.Loaded += Page_Loaded;
            _isLoading = false;
            var All_Employees = AnimalShelterEntities.GetContext().Employee.Where(e => e.Position1.ID_position == 2 || e.Position1.ID_position == 3).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Закрываем выпадающий список, если он открыт
            CB_Animal.IsDropDownOpen = false;
        }
        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на вид животного
            if (CB_Animal.SelectedItem == null)
                errors.AppendLine("Укажите животного!");
            else { 
                _current_Veterinary_examination.Medical_record1 = All_Medical_records.FirstOrDefault(m=> m.Animal == (int)CB_Animal.SelectedValue);
                if (_current_Veterinary_examination.Medical_record1 == null)
                    errors.AppendLine("Не удалось найти медицинскую книгу! Убедитесь, что у данного животного она есть");
                
            }
            
            _current_Veterinary_examination.Veterinarian = (int)UserSession.IDUser;
            _current_Veterinary_examination.Date_of_veterinary_examination = (DateTime)Today;

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current_Veterinary_examination.ID_veterinary_examination == 0)
                AnimalShelterEntities.GetContext().Veterinary_examination.Add(_current_Veterinary_examination);

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

        private void CB_Animal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;

            if (_isLoading) return; // Prevent action if loading

            if (tb.SelectionStart != 0)
            {
                CB_Animal.SelectedItem = null; // Reset selected item if typing
            }

            if ((tb.SelectionStart == 0 && CB_Animal.SelectedItem == null) )
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
    }
}
