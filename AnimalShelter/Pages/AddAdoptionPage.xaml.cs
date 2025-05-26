using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xceed.Words.NET;
using static Xceed.Words.NET.DocX;
using DocumentFormat.OpenXml.Math;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAdoptionPage.xaml
    /// </summary>
    public partial class AddAdoptionPage : Page
    {
        private bool _isLoading = true; // Flag to prevent opening on load
        List<Adoption> adoptions = AnimalShelterEntities.GetContext().Adoption.ToList();
        List<New_owner> owners = AnimalShelterEntities.GetContext().New_owner.ToList();
        List<Animal> animals = AnimalShelterEntities.GetContext().Animal.ToList();
        private Adoption _current_adoption = new Adoption();


        public AddAdoptionPage(Adoption Selected_adoption)
        {
            InitializeComponent();
            adoptions = AnimalShelterEntities.GetContext().Adoption.ToList();
            owners = AnimalShelterEntities.GetContext().New_owner.ToList();
            
            animals = AnimalShelterEntities.GetContext().Animal.ToList();
            var statuses = AnimalShelterEntities.GetContext().Adoption_status.ToList();
            CB_Owner.ItemsSource = owners;
            CB_Animal.ItemsSource = animals;
            CB_Status.ItemsSource = statuses;
            DP_Date_of_adoption.SelectedDate = DateTime.Now;

            if (_current_adoption.Date_of_adoption == DateTime.MinValue)
            {
                _current_adoption.Date_of_adoption = DateTime.Today;
            }
            if (Selected_adoption != null)
            {
                _current_adoption = Selected_adoption;
                CB_Owner.SelectedItem = Selected_adoption.New_owner1;
                CB_Status.SelectedItem = Selected_adoption.Adoption_status1;
                CB_Animal.SelectedItem = Selected_adoption.Animal1;

            }

            DataContext = _current_adoption;
            this.Loaded += Page_Loaded;
            _isLoading = false;


        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Закрываем выпадающий список, если он открыт
            CB_Animal.IsDropDownOpen = false;
            CB_Owner.IsDropDownOpen = false;
        }

        private void CB_Animal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;

            if (_isLoading) return; // Prevent action if loading

            if (tb.SelectionStart != 0)
            {
                CB_Animal.SelectedItem = null; // Reset selected item if typing
            }

            if ((tb.SelectionStart == 0 && CB_Animal.SelectedItem == null) || CB_Animal.SelectedItem == _current_adoption.Animal1)
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

        private void CB_Owner_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;

            if (_isLoading) return;

            if (tb.SelectionStart != 0)
            {
                CB_Owner.SelectedItem = null;
            }

            if ((tb.SelectionStart == 0 && CB_Owner.SelectedItem == null) || CB_Owner.SelectedItem == _current_adoption.New_owner1)
            {
                CB_Owner.IsDropDownOpen = false;
            }

            CB_Owner.IsDropDownOpen = true;
            if (CB_Owner.SelectedItem == null)
            {
                // Filter the items based on the text
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Owner.ItemsSource);
                cv.Filter = s =>
                {
                    var owner = s as New_owner;
                    return owner != null &&
                           (owner.First_name.IndexOf(CB_Owner.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                            owner.Surname.IndexOf(CB_Owner.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
                };
            }

            // Установите курсор в конец текста
            tb.SelectionStart = tb.Text.Length;
            tb.SelectionLength = 0; // Сброс выделения
        }

        private void But_Add_Owner_Click(object sender, RoutedEventArgs e)
        {
            //TODO: добавление нового владельца
        }

        

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            
            // Проверка на вид животного
            if (CB_Animal.SelectedItem == null)
                errors.AppendLine("Укажите животного!");
            else
                _current_adoption.Animal = (int)CB_Animal.SelectedValue;

            // Проверка на пол животного
            if (CB_Owner.SelectedItem == null)
                errors.AppendLine("Укажите нового владельца!");
            else
                _current_adoption.New_owner = (int)CB_Owner.SelectedValue;

            // Проверка на дату рождения
            if (DP_Date_of_adoption.SelectedDate == null || DP_Date_of_adoption.SelectedDate == DateTime.MinValue)
                errors.AppendLine("Укажите дату!");
            else if (DP_Date_of_adoption.SelectedDate > DateTime.Today)
            {
                errors.AppendLine("Дата не может быть позже сегодняшнего дня!");
            }
            else
                _current_adoption.Date_of_adoption = (DateTime)DP_Date_of_adoption.SelectedDate;

           
            // Проверка на источник поступления
            if (CB_Status.SelectedValue == null)
                errors.AppendLine("Укажите статус усыновления!");
            else
                _current_adoption.Adoption_status = (int)CB_Status.SelectedValue;




            _current_adoption.Comment = TB_Comment.Text.Trim();

            

            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            if (_current_adoption.ID_adoption == 0)
                AnimalShelterEntities.GetContext().Adoption.Add(_current_adoption);

            //Делаем попытку записи данных в БД
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");

                NavigationService?.Navigate(new AdoptionsPage());

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

        private void But_Path_contract_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "All Files (*.*)|*.*"; 
            if (openFileDialog.ShowDialog() == true)
            {
                _current_adoption.Contract = openFileDialog.FileName;

                MessageBox.Show("Путь к файлу контракта успешно сохранен:\n" + _current_adoption.Contract);
            }

        }

        private void But_Open_contract_Click(object sender, RoutedEventArgs e)
        {
            if(_current_adoption.Contract!= null) 
            OpenFile(_current_adoption.Contract);

        }

        private void But_Generate_contract_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OpenFile(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && System.IO.File.Exists(filePath))
            {
                // Создаем новый процесс для открытия файла
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Файл не найден или путь к файлу неверный.");
            }
        }
    }
}
