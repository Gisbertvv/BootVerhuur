using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media;
using Syncfusion.Pdf.Parsing;
using System.Diagnostics;
using System.IO;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            AdminPanel panel = new AdminPanel();
            InitializeComponent();
            Login.Closing += LoginWindow_Closing;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            bool s = login.getLogin(txtUsername.Text, txtPassword.Password);


            if (s) { 
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }else if (!s)
            {
                MessageBox.Show("Usename of password not correct");
            }
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename;
            if (File.Exists("..\\..\\..\\..\\lib\\ShutDownSSH.bat"))
            {
                Debug.WriteLine("Specified file exists.");
                filename = "..\\..\\..\\..\\lib\\ShutDownSSH.bat";
                Process process = new Process();
                process.StartInfo.FileName = filename;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.Close();
            }
            else
            {
                Debug.WriteLine("Specified file does not " +
                                  "exist in the current directory.");
            }
        }
    }
}
