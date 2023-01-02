using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using System.Data.SqlClient;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateMember.xaml
    /// </summary>
    public partial class CreateMember : Window
    {
        public CreateMember()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

   
        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            CreateFellow.CreateMember(txtVoornaam.Text, txtAchternaam.Text, txtGebruikersnaam.Text, txtWachtwoord.Password, txtEmail.Text, _txtTelefoonnummer.Text, Rol.Text, Niveau.Text);
        }

        private void Check_Admin(object sender, RoutedEventArgs e)
        {
            if (Rol.Text.Equals("Admin"))
            {
                CreateAdmin createAdmin = new CreateAdmin();
                createAdmin.Show();
                Close();
            }
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel window = new AdminPanel();

            window.Show();
            Close();
        }

        private void OpenEditUserPanel(object sender, RoutedEventArgs e)
        {
            Edit_member edit = new Edit_member();

            edit.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


    }
}

