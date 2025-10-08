using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace TRPO_pr7
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Doctor InputDoctor = new Doctor();
        Doctor CurrentDoctor = new Doctor();
        Patient InputPatient = new Patient();
        Patient EditPatient = new Patient();
        Patient FoundPatient = new Patient();
        FileСounter FileCounter = new FileСounter();
        private void GetFilesCount()
        {
            int doctorCount = Directory.GetFiles("..\\net8.0-windows\\Doctor").Length;
            int patientCount = Directory.GetFiles("..\\net8.0-windows\\Patient").Length;
            FileCounter.TotalFiles = $"Всего файлов: {doctorCount + patientCount}     Докторов: {doctorCount}     Пациентов: {patientCount}";
        }
        private void CopyPatientProperties(Patient source, Patient destination)
        {
            destination.LastName = source.LastName;
            destination.Name = source.Name;
            destination.MiddleName = source.MiddleName;
            destination.Birthday = source.Birthday;
            destination.LastAppointment = source.LastAppointment;
        }

        public MainWindow()
        {
            InitializeComponent();
            RegDoctor.DataContext = InputDoctor;
            AutorDoctor.DataContext = InputDoctor;
            AddPatient.DataContext = InputPatient;
            FindPatient.DataContext = InputPatient;
            InfoPatient.DataContext = FoundPatient;
            EditPatientForm.DataContext = EditPatient;
            FilesCount.DataContext = FileCounter;
            GetFilesCount();
        }

        private void UserRegButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputDoctor.Name != "" && InputDoctor.LastName != "" && InputDoctor.MiddleName != "" && InputDoctor.Password != ""
                && InputDoctor.Specialization != "" && InputDoctor.Password == InputDoctor.Сonfirmation)
            {
                int countFile = Directory.GetFiles("..\\net8.0-windows\\Doctor").Length;
                string fileName = $"..\\net8.0-windows\\Doctor\\D_{countFile.ToString("D5")}.json";
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(InputDoctor, options);
                File.WriteAllText(fileName, jsonString);
                GetFilesCount();
                MessageBox.Show($"Регистрация успешна! Ваш логин D_{countFile.ToString("D5")}",
                    "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Все поля обязательны для заполнения. Пароль должен совпадать с подтверждением",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserEnterButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"..\\net8.0-windows\\Doctor\\D_{InputDoctor.ID}.json";
            if (InputDoctor.ID == "" || InputDoctor.Password == "")
            {
                MessageBox.Show("Все поля обязательны для заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Не существует такого пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var jsonString = File.ReadAllText(fileName);
            if (JsonSerializer.Deserialize<Doctor>(jsonString).Password != InputDoctor.Password)
            {
                MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CurrentDoctor = JsonSerializer.Deserialize<Doctor>(jsonString);
            Info.DataContext = CurrentDoctor;
            MessageBox.Show("Вход выполнен успешно!", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDoctor.Name == null)
            {
                MessageBox.Show("Необходимо зайти врачу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string fileName = $"..\\net8.0-windows\\Patient\\P_{InputPatient.ID}.json";
            if (InputPatient.ID == "")
            {
                MessageBox.Show("Необходимо заполнить идентификатор пациента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Не существует такого пациента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var jsonString = File.ReadAllText(fileName);
            FoundPatient = JsonSerializer.Deserialize<Patient>(jsonString);
            EditPatient = JsonSerializer.Deserialize<Patient>(jsonString);
            InfoPatient.DataContext = FoundPatient;
            EditPatientForm.DataContext = EditPatient;
            FoundPatient.ID = InputPatient.ID;
            EditPatient.ID = InputPatient.ID;
            MessageBox.Show("Поиск пациента выполнен успешно!", "Поиск", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDoctor.Name == null)
            {
                MessageBox.Show("Необходимо зайти врачу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (InputPatient.Name != "" && InputPatient.LastName != "" && InputPatient.MiddleName != "" 
                && InputPatient.Birthday != "" && InputPatient.LastAppointment != "")
            {
                int countFile = Directory.GetFiles("..\\net8.0-windows\\Patient").Length;
                string fileName = $"..\\net8.0-windows\\Patient\\P_{countFile.ToString("D7")}.json";
                var options = new JsonSerializerOptions { WriteIndented = true };
                InputPatient.LastDoctor = $"{CurrentDoctor.LastName} {CurrentDoctor.Name} {CurrentDoctor.MiddleName}";
                string jsonString = JsonSerializer.Serialize(InputPatient, options);
                File.WriteAllText(fileName, jsonString);
                GetFilesCount();
                MessageBox.Show($"Добавление успешно! Информация в P_{countFile.ToString("D7")}",
                    "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Все поля обязательны для заполнения",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void InspectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDoctor.Name == null)
            {
                MessageBox.Show("Необходимо зайти врачу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditPatient.ID == null)
            {
                MessageBox.Show("Необходимо выбрать пациента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (FoundPatient.Diagnosis != "" && FoundPatient.Recomendations != "")
            {
                int countFile = Directory.GetFiles("..\\net8.0-windows\\Patient").Length;
                string fileName = $"..\\net8.0-windows\\Patient\\P_{FoundPatient.ID}.json";
                var options = new JsonSerializerOptions { WriteIndented = true };
                FoundPatient.LastDoctor = $"{CurrentDoctor.LastName} {CurrentDoctor.Name} {CurrentDoctor.MiddleName}";
                FoundPatient.LastAppointment = DateTime.Today.ToString("d");
                string jsonString = JsonSerializer.Serialize(FoundPatient, options);
                File.WriteAllText(fileName, jsonString);
                MessageBox.Show($"Информация об осмотре изменена успешно! Информация в P_{FoundPatient.ID}",
                    "Осмотр", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Все поля обязательны для заполнения",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EditPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDoctor.Name == null)
            {
                MessageBox.Show("Необходимо зайти врачу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditPatient.ID == null)
            {
                MessageBox.Show("Необходимо выбрать пациента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditPatient.Name != "" && EditPatient.LastName != "" && EditPatient.MiddleName != ""
                && EditPatient.Birthday != "" && EditPatient.LastAppointment != "")
            {
                string fileName = $"..\\net8.0-windows\\Patient\\P_{EditPatient.ID}.json";
                var options = new JsonSerializerOptions { WriteIndented = true };
                FoundPatient.LastDoctor = EditPatient.LastDoctor = $"{CurrentDoctor.LastName} {CurrentDoctor.Name} {CurrentDoctor.MiddleName}";
                CopyPatientProperties(EditPatient, FoundPatient);
            }
            else
                MessageBox.Show("Все поля обязательны для заполнения",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ResetPatientButton_Click(object sender, RoutedEventArgs e)
        {
            string id = EditPatient.ID;
            if (CurrentDoctor.Name == null)
            {
                MessageBox.Show("Необходимо зайти врачу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditPatient.ID == null)
            {
                MessageBox.Show("Необходимо выбрать пациента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string fileName = $"..\\net8.0-windows\\Patient\\P_{EditPatient.ID}.json";
            var jsonString = File.ReadAllText(fileName);
            EditPatient = JsonSerializer.Deserialize<Patient>(jsonString);
            FoundPatient.ID = id;
            EditPatient.ID = id;
            MessageBox.Show("Поиск пациента выполнен успешно!", "Поиск", MessageBoxButton.OK, MessageBoxImage.Information);
            CopyPatientProperties(EditPatient, FoundPatient);
            EditPatientForm.DataContext = EditPatient;
        }
    }              
}