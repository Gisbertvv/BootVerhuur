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
            new Login(txtUsername.Text, txtPassword.Password);
            Close();
        }
    }
}
