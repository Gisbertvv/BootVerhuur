using System;
using System.Collections.Generic;
using System.IO;
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
using System.Security.Cryptography;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AccidentReportWindow : Window
    {
        private int count;
        string folderName;
        string destinationFile;
        private string today = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        public AccidentReportWindow()
        {
            InitializeComponent();
        }

        private void TopLeftButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ThirdParty_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void CreateDirectory() 
        {
            folderName = $@"C:\Users\rosal\source\repos\BootVerhuur\Schadeformuliers\SF {today} {count}";
            // If directory does not exist, create it
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
                count += 1;
            }
            else
            {
                while (Directory.Exists(folderName))
                {
                    count++;
                    folderName = $@"C:\Users\rosal\source\repos\BootVerhuur\Schadeformuliers\SF {today} {count}";
                }
                Directory.CreateDirectory(folderName);
                count += 1;
            }
        }
         
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            CreateDirectory();
        }   
    }
}
