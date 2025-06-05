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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AnimalPage.xaml
    /// </summary>
    public partial class AnimalPage : Page
    {
        private Animal _current_animal= new Animal();
        AddVolunteerWindow _add_Window;
        private bool _isLoading = true; // Flag to prevent opening on load
        private AddMedicalRecordWindow _addMedicalRecordWindow; // Переменная для хранения текущего окна медицинской карты

        DateTime mindate= DateTime.Today.AddYears(-50);

        public AnimalPage(Animal Selected_animal)
        {
            InitializeComponent();
            var All_volunteers = AnimalShelterEntities.GetContext().Volunteer.ToList();
            var All_species = AnimalShelterEntities.GetContext().Species.ToList();
            var All_genders = AnimalShelterEntities.GetContext().Gender.ToList();
            var All_statuses = AnimalShelterEntities.GetContext().Animal_status.ToList();
            var All_sources_of_receipt = AnimalShelterEntities.GetContext().Source_of_receipt.ToList();
            var All_breeds = AnimalShelterEntities.GetContext().Breed.ToList();
            All_breeds.Insert(0, new Breed { ID_breed = 0, Name_breed = "Не указано" });


            CB_Volunteer.ItemsSource= All_volunteers;
            CB_Species.ItemsSource= All_species;
            CB_Gender.ItemsSource= All_genders;
            CB_Status.ItemsSource= All_statuses;
            CB_Source_of_receipt.ItemsSource    = All_sources_of_receipt;
            CB_Breed.ItemsSource= All_breeds;
            CB_Volunteer.ItemsSource = All_volunteers;
            if(DP_Date_of_birth.SelectedDate==DateTime.MinValue) 
            DP_Date_of_birth.DisplayDate = mindate;
            
            DP_Date_of_registration.SelectedDate = DateTime.Today; //TODO: разобраться почему не устанавливается сегодняшняя дата в дейтпикере
            if (_current_animal.Date_of_registration == DateTime.MinValue)
            {
                _current_animal.Date_of_registration = DateTime.Today;
            }
            if (Selected_animal != null)
            {
                _current_animal= Selected_animal;
                CB_Volunteer.SelectedItem = Selected_animal.Volunteer1;
                CB_Status.SelectedItem=Selected_animal.Animal_status1;
                CB_Species.SelectedItem=Selected_animal.Species1;
                CB_Source_of_receipt.SelectedItem=Selected_animal.Source_of_receipt1;
                CB_Gender.SelectedItem=Selected_animal.Gender1;
                if(Selected_animal.Breed1 != null) CB_Breed.SelectedItem=Selected_animal.Breed1;
                if(Selected_animal.Breed1==null) CB_Breed.SelectedIndex=0;

            }

            DataContext = _current_animal;
            if (UserSession.UserPosition == "Волонтёр")
            {
                TB_Nickname.IsReadOnly = true;
                CB_Species.IsEnabled = false;
                CB_Breed.IsEnabled = false; 
                CB_Gender.IsEnabled = false;
                CB_Source_of_receipt.IsEnabled = false;
                CB_Status.IsEnabled = false;
                DP_Date_of_birth.PreviewMouseDown += DatePicker_PreviewMouseDown;
                DP_Date_of_registration.PreviewMouseDown += DatePicker_PreviewMouseDown;
                CB_Volunteer.IsEnabled = false;
                But_Volunteer.Visibility=Visibility.Hidden;
                But_Medical_record.Visibility=Visibility.Collapsed;
                But_Add_photo.Visibility=Visibility.Collapsed;
                But_Delete_photo.Visibility = Visibility.Collapsed;
                Border_Add.Visibility = Visibility.Collapsed;
                Border_Delete.Visibility = Visibility.Collapsed;
            }
            _isLoading = false;

            this.Loaded += AnimalPage_Loaded;

        }
        private void AnimalPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Закрываем выпадающий список, если он открыт
            CB_Volunteer.IsDropDownOpen = false;
        }
        private void DatePicker_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true; // Запретить взаимодействие
        }
        private void CB_Volunteer_TextChanged(object sender, TextChangedEventArgs e) 
        {
            var tb = (TextBox)e.OriginalSource;

            if (_isLoading) return; // Prevent action if loading

            if (tb.SelectionStart != 0)
            {
                CB_Volunteer.SelectedItem = null; // Reset selected item if typing
            }

            if ((tb.SelectionStart == 0 && CB_Volunteer.SelectedItem == null) || CB_Volunteer.SelectedItem == _current_animal.Volunteer1)
            {
                CB_Volunteer.IsDropDownOpen = false; // Close dropdown if no selection
            }

            CB_Volunteer.IsDropDownOpen = true; // Open dropdown if item is not selected
            if (CB_Volunteer.SelectedItem == null)
            {
                // Filter the items based on the text
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Volunteer.ItemsSource);
                cv.Filter = s =>
                {
                    var volunteer = s as Volunteer;
                    return volunteer != null &&
                           (volunteer.First_name.IndexOf(CB_Volunteer.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                            volunteer.Surname.IndexOf(CB_Volunteer.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }
        }

        private void But_Volunteer_Click(object sender, RoutedEventArgs e)
        {
            
            
                try
                {
                    if (CB_Volunteer.SelectedItem != null)
                    {
                        var Selected_volunteer = CB_Volunteer.SelectedItem as Volunteer;

                        if (Selected_volunteer != null)
                        {
                            if (_add_Window == null || !_add_Window.IsVisible)
                            {
                                _add_Window = new AddVolunteerWindow(Selected_volunteer);
                                _add_Window.Show();
                            }
                            else
                            {
                                MessageBox.Show("Окно  волонтёра уже открыто.");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Ошибка: Не найден волонтёр для просмотра.",
                                            "Warning",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            

        }

        private void But_Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на кличку
            if (string.IsNullOrWhiteSpace(_current_animal.Nickname))
                errors.AppendLine("Укажите кличку животного!");
            else _current_animal.Nickname=TB_Nickname.Text.Trim();

            // Проверка на вид животного
            if (CB_Species.SelectedItem == null)
                errors.AppendLine("Укажите вид животного!");
            else
                _current_animal.Species = (int)CB_Species.SelectedValue;

            // Проверка на пол животного
            if (CB_Gender.SelectedItem == null)
                errors.AppendLine("Укажите пол животного!");
            else
                _current_animal.Gender = (int)CB_Gender.SelectedValue;

            // Проверка на дату рождения
            if (DP_Date_of_birth.SelectedDate == null || DP_Date_of_birth.SelectedDate==DateTime.MinValue)
                errors.AppendLine("Укажите дату рождения животного!");
            else if (DP_Date_of_birth.SelectedDate > DateTime.Today)
            {
                errors.AppendLine("Дата рождения не может быть позже сегодняшнего дня.");
            }
            else
                _current_animal.Date_of_birth = (DateTime)DP_Date_of_birth.SelectedDate;

            // Проверка на дату регистрации
            if (DP_Date_of_registration.SelectedDate == null || DP_Date_of_registration.SelectedDate == DateTime.MinValue)
            {
                errors.AppendLine("Укажите дату регистрации животного!");
            }
            else if (DP_Date_of_registration.SelectedDate > DateTime.Today)
            {
                errors.AppendLine("Дата регистрации не может быть позже сегодняшнего дня.");
            }
            else
                _current_animal.Date_of_registration = (DateTime)DP_Date_of_registration.SelectedDate;

            // Проверка на источник поступления
            if (CB_Source_of_receipt.SelectedValue == null)
                errors.AppendLine("Укажите источник поступления животного!");
            else
                _current_animal.Source_of_receipt = (int)CB_Source_of_receipt.SelectedValue;

            // Проверка на статус животного
            if (CB_Status.SelectedItem == null)
                errors.AppendLine("Укажите актуальный статус животного!");
            else
                _current_animal.Animal_status = (int)CB_Status.SelectedValue;

            if(CB_Breed.SelectedValue!=null) _current_animal.Breed=(int)CB_Breed.SelectedValue;
            if (CB_Breed.SelectedIndex == 0) _current_animal.Breed = null;
            _current_animal.Note = TB_Note.Text.Trim();

            if(CB_Volunteer.SelectedValue!=null) _current_animal.Volunteer = (int)CB_Volunteer.SelectedValue;


            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            if (_current_animal.ID_animal == 0)
                AnimalShelterEntities.GetContext().Animal.Add(_current_animal);

            //Делаем попытку записи данных в БД о новом пользователе
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");

                NavigationService?.Navigate(new AnimalsPage());

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

        private void But_Medical_record_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                // Находим медицинскую карту для текущего животного
                var medicalRecord = AnimalShelterEntities.GetContext().Medical_record
                    .FirstOrDefault(record => record.Animal == _current_animal.ID_animal);

                // Проверяем, есть ли медицинская карта для животного
                if (medicalRecord != null)
                {
                    // Если карта найдена, переходим на страницу медицинской карты
                    if (_addMedicalRecordWindow == null || !_addMedicalRecordWindow.IsVisible)
                    {
                        _addMedicalRecordWindow = new AddMedicalRecordWindow(medicalRecord);
                        _addMedicalRecordWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Окно редактирования медицинской карты уже открыто.");
                    }
                }
                else
                {
                    MessageBox.Show("У этого животного еще нет медицинской карты.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

        }

        private void But_Add_photo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            if (_current_animal.Photo == null)
            {
                result = MessageBox.Show("Вы уверены, что хотите добавить фотографию?",
                                   "Подтверждение добавления фотографии",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Warning);
            }
            else if (_current_animal.Photo != null)
            {
                 result = MessageBox.Show("Вы уверены, что хотите изменить фотографию? Отменить действие будет невозможно!",
                                   "Подтверждение изменения фотографии",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Warning);
            }
            else return;
            if (result == MessageBoxResult.Yes)
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Фильтр для изображений

                if (fileDialog.ShowDialog() == true)
                {
                    // Получаем путь к файлу
                    string filePath = fileDialog.FileName;

                    // Загружаем изображение
                    ImageSource imageSource = new BitmapImage(new Uri(filePath));

                    // Устанавливаем загруженное изображение в элемент Image
                    Image_Animal.Source = imageSource;

                    // Сохраняем путь к изображению в объекте _current_animal
                    _current_animal.Photo = filePath; // Убедитесь, что у вас есть соответствующее свойство в классе Animal
                }
            }
        }

        private void But_Delete_photo_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить фотографию? Отменить действие будет невозможно!",
                                   "Подтверждение удаления",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _current_animal.Photo = null; // Сбрасываем путь к фотографии в объекте
                Image_Animal.Source = new BitmapImage(new Uri("/Res/DefaultPhoto.png", UriKind.Relative));
            }
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
