using System.Text;
using System.Windows;
using System.Text.Json;
using System.IO;

namespace TRPO_pr7
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Doctor CurrentDoctor = new Doctor();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = CurrentDoctor;
        }

        private void UserRegBtton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"{(int)DateTime.Now.Ticks}.json";
            string jsonString = JsonSerializer.Serialize(CurrentDoctor);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(File.ReadAllText(fileName));
        }
    }
}