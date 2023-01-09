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
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            bool s = login.getLogin(txtUsernameOrEmail.Text, txtPassword.Password);


            if (s) { 
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }else if (!s)
            {
                MessageBox.Show("Username/email of password not correct");
            }
        }

        /*protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
        }*/

    }
}
