using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media;
using Syncfusion.Pdf.Parsing;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static string id;
        public static string firstName;
        public static string lastName;
        public static string phoneNumber;
        public static string email;
        public static string boatingLevel;
        public static string role;

        public LoginWindow()
        {
            AdminPanel panel = new AdminPanel();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login(txtUsername.Text, txtPassword.Password);
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }
    }
}
