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
using Microsoft.Office.Interop.Word;
using System.Windows.Xps.Packaging;
using System.IO;
using System.Windows.Xps;

namespace AnimalShelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAdoptionPage.xaml
    /// </summary>
    public partial class AddAdoptionPage : System.Windows.Controls.Page
    {
        private bool _isLoading = true; // Flag to prevent opening on load
        List<Adoption> adoptions = AnimalShelterEntities.GetContext().Adoption.ToList();
        List<New_owner> owners = AnimalShelterEntities.GetContext().New_owner.ToList();
        List<Animal> animals = AnimalShelterEntities.GetContext().Animal.ToList();
        private Adoption _current_adoption = new Adoption();


        public AddAdoptionPage(Adoption Selected_adoption)
        {
            InitializeComponent();
            But_Generate_contract.IsEnabled = true;

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
            ShowDocument();


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
            But_Generate_contract.IsEnabled = false;
            var currentOwner = _current_adoption.New_owner1;
            string fileName = "C:\\Users\\acer\\Desktop\\AnimalsShelterDocuments\\Сгенерированный договор усыновления_ID_" + _current_adoption.ID_adoption + "_" + _current_adoption.Animal1.Nickname + ".docx";
            // Проверяем, открыт ли файл, и закрываем его, если да
            if (IsFileOpen(fileName))
            {
                // Закрываем файл
                CloseFile(fileName);
            }
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            // Создаем новый документ
            Document doc = wordApp.Documents.Add();
            doc.Content.Font.Name = "Courier New"; // Устанавливаем шрифт
            doc.Content.Font.Size = 10; // Размер шрифта

            // Добавляем абзацы с заголовком и датой
            AddParagraph(doc, "ДОГОВОР N ______", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "ПЕРЕДАЧИ ЖИВОТНОГО ИЗ ПРИЮТА ДЛЯ", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "ЖИВОТНЫХ В СОБСТВЕННОСТЬ", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "(ЗАПОЛНЯЕТСЯ НА КАЖДОЕ ЖИВОТНОЕ)", WdParagraphAlignment.wdAlignParagraphCenter);

            // Добавляем дату
            Paragraph dateDoc = doc.Content.Paragraphs.Add();

            // Получаем текущую дату

            string dateText = $"\"{_current_adoption.Date_of_adoption:dd}\" {_current_adoption.Date_of_adoption:MM} {_current_adoption.Date_of_adoption:yyyy} г.";
            dateDoc.Range.Text = dateText;
            dateDoc.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

            dateDoc.Range.InsertParagraphAfter();

            // Добавляем информацию о приюте
            AddParagraph(doc, "Приют _____________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "в лице руководителя _______________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "и:", WdParagraphAlignment.wdAlignParagraphJustify);


            //Добавляем информацию о новом владельце
            string fio = currentOwner.Surname + " " + currentOwner.First_name + " " + currentOwner.Patronymic;

            //Для физических лиц:
            if (_current_adoption.New_owner1.Contractor_type == 1)
            {
                AddParagraph(doc, "Для физических лиц:", WdParagraphAlignment.wdAlignParagraphJustify);

                AddParagraph(doc, "ФИО: " + fio + ";", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "Адрес: " + currentOwner.Address + ";", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "Телефон: " + currentOwner.Phone_number + ";", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "Паспортные данные: _________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "Адрес, по которому будет проживать животное: _________________________________________________________________________________________________________,", WdParagraphAlignment.wdAlignParagraphJustify);
            }
            //Для юридических лиц:
            else
            {
                AddParagraph(doc, "Для юридических лиц:", WdParagraphAlignment.wdAlignParagraphJustify);

                AddParagraph(doc, "Организация ________________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "Адрес: " + currentOwner.Address + ";", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "Телефон: " + currentOwner.Phone_number + ";", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "В лице руководителя и (ФИО): " + fio + ",", WdParagraphAlignment.wdAlignParagraphJustify);
            }









            AddParagraph(doc, "именуемый(ая) в дальнейшем \"Получатель\", с другой стороны, договорились о нижеследующем:", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "1. Приют безвозмездно передает в собственность Получателю животное.", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "2. Вместе с животным Приют передает Получателю ветеринарный паспорт животного.", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "3. К договору прилагаются фотографии передаваемого животного, которые содержатся в Приложении 1 к настоящему Договору и являются его неотъемлемой частью.", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4. Получатель обязуется:", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4.1. Нести все расходы по содержанию животного (включая, помимо прочего, расходы на ветеринарное обслуживание и, при необходимости, лечение животного);", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4.2. Обеспечить животному условия содержания, соответствующие санитарным и ветеринарным требованиям, законодательству и особенностям данного животного (в том числе особенностям его здоровья), включая:", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "A. Ежедневное правильное питание;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "B. Круглосуточный доступ животного к воде;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "C. Выгул не менее 1 (одного) раза в сутки, физическую и умственную активность в объеме, необходимом животному.", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4.3. Не оставлять животное без ухода, присмотра и попечения на срок более одних суток;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4.4. По требованию Приюта направлять Приюту фотографии и видео животного;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4.5. Не допускать жестокого обращения с животным;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "4.6. Возвратить животное Приюту в случае отказа от настоящего Договора.", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "5. Приют обязуется:", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "5.1. Сообщить Получателю известные Приюту сведения о животном, необходимые для его содержания и ухода за ним, в том числе состояние здоровья животного, особенности его содержания, характера и поведения, информацию о видах и сроках проведения необходимой вакцинации животного и обработках против паразитов;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "5.2. По просьбе Получателя безвозмездно консультировать получателя по вопросам содержания и воспитания животного;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "5.3. Принять животное от Получателя в случае отказа Получателем от настоящего Договора.", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "6. Во всех случаях неисполнения обязательств по настоящему Договору Стороны несут ответственность в соответствии с законодательством Российской Федерации;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "7. Получатель обязуется в случае невозможности дальнейшего содержания животного, ни при каких обстоятельствах не выбрасывать животное, не усыплять, а в случае передачи новым владельцам известить Приют для переоформления договора;", WdParagraphAlignment.wdAlignParagraphLeft);
            AddParagraph(doc, "8. Прекращение настоящего Договора не освобождает Стороны от ответственности за его нарушение, а также от выполнения обязательств, возникших до момента такого прекращения.", WdParagraphAlignment.wdAlignParagraphLeft);

            //Данные о животном:
            var _currentAnimal = _current_adoption.Animal1;
            AddParagraph(doc, "Данные о животном: ", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Вид животного: " + _currentAnimal.Species1.Name_species + ";", WdParagraphAlignment.wdAlignParagraphJustify);
            if( _currentAnimal.Breed!=null)
            AddParagraph(doc, "Порода: " + _currentAnimal.Breed1.Name_breed + ";", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Окрас:_____________________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Пол: " + _currentAnimal.Gender1.Name_gender + ";", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Особые приметы:____________________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Дата рождения(приблизительно): " + _currentAnimal.Date_of_birth.ToString("dd.MM.yyyy") + ";", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Кличка(на момент заключения настоящего Договора): " + _currentAnimal.Nickname + ";", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Номер чипа(если есть): _____________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Клеймо / несмываемые метки(если есть):\n____________________________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Наличие ветеринарных показаний к кормлению: \n_______________________________________________________________________________________________________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "Особенности характера и особые условия содержания и ухода: \n________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________;", WdParagraphAlignment.wdAlignParagraphJustify);

            
            Paragraph para = doc.Content.Paragraphs.Add();
            para.Range.InsertBreak(WdBreakType.wdPageBreak);

            AddParagraph(doc, "Передал в собственность: ", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "(Название организации)", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "(Адрес)", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "(Телефон)", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "(ФИО руководителя)", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "(ФИО исполнителя)", WdParagraphAlignment.wdAlignParagraphCenter);
            AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
            AddParagraph(doc, "(Подпись)", WdParagraphAlignment.wdAlignParagraphCenter);


            //Для физических лиц:
            if (_current_adoption.New_owner1.Contractor_type == 1)
            {
                AddParagraph(doc, "Для физических лиц: ", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(ФИО)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Адрес)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Телефон)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Паспортные данные)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Подпись)", WdParagraphAlignment.wdAlignParagraphCenter);


            }
            //Для юридических лиц:
            else
            {
                AddParagraph(doc, "Для юридических лиц: ", WdParagraphAlignment.wdAlignParagraphJustify);

                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Название организации)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Адрес)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Телефон)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(ФИО руководителя)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(ФИО исполнителя)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "___________________________________________________________________________", WdParagraphAlignment.wdAlignParagraphJustify);
                AddParagraph(doc, "(Подпись)", WdParagraphAlignment.wdAlignParagraphCenter);
                AddParagraph(doc, "М.П.", WdParagraphAlignment.wdAlignParagraphCenter);

            }


            // Сохраняем документ
            object fileNameObj = fileName;
            doc.SaveAs2(ref fileNameObj);
            doc.Close();
            wordApp.Quit();

            Console.WriteLine("Документ успешно создан!");
            _current_adoption.Contract = fileName;
            But_Generate_contract.IsEnabled = true;
            Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
        }
        //private XpsDocument ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName)
        //{
        //    Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
        //    Document doc = null;

        //    try
        //    {
        //        doc = wordApplication.Documents.Open(wordDocName);
        //        doc.SaveAs2(xpsDocName, WdSaveFormat.wdFormatXPS);
        //        return new XpsDocument(xpsDocName, FileAccess.Read);
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show($"Ошибка при конвертации: {exp.Message}");
        //    }
        //    finally
        //    {
        //        if (doc != null)
        //        {
        //            doc.Close(false);
        //        }
        //        wordApplication.Quit();
        //    }
        //    return null;
        //}

        // Метод для добавления абзаца
        static void AddParagraph(Document doc, string text, WdParagraphAlignment alignment)
        {
            Paragraph paragraph = doc.Content.Paragraphs.Add();
            paragraph.Range.Text = text;
            paragraph.Alignment = alignment;
            paragraph.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
            paragraph.Range.InsertParagraphAfter();
        
        }
        private bool IsFileOpen(string fileName)
        {
            try
            {
                // Получаем открытую версию Word
                var wordApp = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");

                // Проверяем открытые документы
                foreach (Document doc in wordApp.Documents)
                {
                    if (doc.FullName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true; // Файл открыт
                    }
                }
            }
            catch
            {
                // Word не запущен
            }
            return false; // Файл не открыт
        }

        private void CloseFile(string fileName)
        {
            var wordApp = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            foreach (Document doc in wordApp.Documents)
            {
                if (doc.FullName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    doc.Close(WdSaveOptions.wdSaveChanges);
                    break; // Закрываем и выходим
                }
            }
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
        private void ShowDocument()
        {
            string fileName = _current_adoption.Contract;
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                MessageBox.Show("Файл документа не найден.");
                return;
            }

            // Определите расширение файла
            string extension = Path.GetExtension(fileName).ToLower();
            string xpsFileName = fileName.Replace(extension, ".xps");

            // Закрытие открытого файла, если он уже открыт
            if (IsFileOpen(fileName))
            {
                CloseFile(fileName);
            }

            // Конвертация документа
            try
            {
                if (extension == ".docx")
                {
                    ConvertWordDocToXPSDoc(fileName, xpsFileName);
                }
                

                // Открытие XPS-документа в DocumentViewer
                using (XpsDocument xpsDoc = new XpsDocument(xpsFileName, FileAccess.Read))
                {
                    var fixedDocSeq = xpsDoc.GetFixedDocumentSequence();
                    DV_Contract.Document = fixedDocSeq; // Устанавливаем документ в DocumentViewer
                }
                DV_Contract.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                DV_Contract.Visibility = Visibility.Hidden;

            }
        }



        private XpsDocument ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName)
        {
            Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
            Document doc = null;

            try
            {
                doc = wordApplication.Documents.Open(wordDocName);
                doc.SaveAs2(xpsDocName, WdSaveFormat.wdFormatXPS);
                return new XpsDocument(xpsDocName, System.IO.FileAccess.Read);
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Ошибка при конвертации: {exp.Message}");
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(false);
                }
                wordApplication.Quit();
            }
            return null;
        }

    }
}
