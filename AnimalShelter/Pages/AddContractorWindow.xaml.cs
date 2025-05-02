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
    /// Логика взаимодействия для AddContractorWindow.xaml
    /// </summary>
    public partial class AddContractorWindow : Window
    {
        private Contractor _current_contractor = new Contractor();
        public event Action ContractorAdded;
        public AddContractorWindow(Contractor Selected_contractor)
        {
            InitializeComponent();
            List<Contractor> All_contractors = AnimalShelterEntities.GetContext().Contractor.ToList();
            List<Contractor_type> All_Types = AnimalShelterEntities.GetContext().Contractor_type.ToList();
            CB_Contractor_type.ItemsSource = All_Types;
            TB_Title.Text = "Добавление данных о контрагенте";
            if (Selected_contractor != null)
            {
                _current_contractor = Selected_contractor;
                CB_Contractor_type.SelectedItem = Selected_contractor.Contractor_type1;
                TB_Name.Text = Selected_contractor.Name;
                TB_Contact_percon.Text = Selected_contractor.Contact_person;
                TB_Phone_number.Text = Selected_contractor.Phone_number;
                TB_Email.Text = Selected_contractor.Email;
                TB_Address.Text = Selected_contractor.Address;
                TB_INN.Text = Selected_contractor.INN;
                TB_Title.Text = "Изменение данных о контрагенте";

            }

            DataContext = _current_contractor;
        }

        private void But_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void But_Add_Breed_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на имя контрагента
            if (string.IsNullOrWhiteSpace(_current_contractor.Name))
                errors.AppendLine("Укажите имя контрагента!");
            else
                _current_contractor.Name = TB_Name.Text.Trim();

            // Проверка на номер телефона
            if (string.IsNullOrWhiteSpace(_current_contractor.Phone_number))
                errors.AppendLine("Укажите номер телефона!");
            else
                _current_contractor.Phone_number = TB_Phone_number.Text.Trim();

            // Проверка на email
            if (string.IsNullOrWhiteSpace(_current_contractor.Email))
                errors.AppendLine("Укажите email!");
            else
                _current_contractor.Email = TB_Email.Text.Trim();

            // Проверка на адрес
            if (string.IsNullOrWhiteSpace(_current_contractor.Address))
                errors.AppendLine("Укажите адрес!");
            else
                _current_contractor.Address = TB_Address.Text.Trim();

            // Проверка на ИНН
            if (string.IsNullOrWhiteSpace(_current_contractor.INN))
                errors.AppendLine("Укажите ИНН!");
            else
                _current_contractor.INN = TB_INN.Text.Trim();

            // Проверка на тип контрагента
            if (CB_Contractor_type.SelectedValue == null)
                errors.AppendLine("Укажите тип контрагента!");
            else
                _current_contractor.Contractor_type = (int)CB_Contractor_type.SelectedValue;

            _current_contractor.Contact_person = TB_Contact_percon.Text.Trim();
            // Проверка на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового контрагента в базу данных
            if (_current_contractor.ID_contractor == 0)
                AnimalShelterEntities.GetContext().Contractor.Add(_current_contractor);

            // Делаем попытку записи данных в БД о новом контрагенте
            try
            {
                AnimalShelterEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                ContractorAdded?.Invoke();
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
    }
}
