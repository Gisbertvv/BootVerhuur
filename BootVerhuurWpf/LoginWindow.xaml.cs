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
        public LoginWindow()
        {
            AdminPanel panel = new AdminPanel();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool s = Login.getLogin(txtUsername.Text, txtPassword.Password);

            if (s) { 
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }else if (!s)
            {
                MessageBox.Show("Usename of password not correct");
            }
        }
    }
}
