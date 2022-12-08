using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class CreateAdmin : Window
    {
        MainWindow mainWindow = new MainWindow();
        public Admin admin;
        bool digits = false;
        bool special = false;


        public CreateAdmin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private static bool IsEmailValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        private bool ContainsDigit(string wachtwoord)
        {
            foreach (char l in wachtwoord)
            {
                if (Char.IsDigit(l))
                {
                    digits = true;
                }

                if (digits)
                {
                    break;
                }
            }
            return digits;
        }

        private bool ContainsSpecial(string wachtwoord)
        {
            string regexItem = @"\|!#$%&/+-()=?»«@£§€{}.-;'<>_,";

            foreach (var item in regexItem)
            {
                if (wachtwoord.Contains(item))
                {
                    special = true;
                }
                if (special)
                {
                    break;
                }
            }
            return special;
        }


        private void Create_Admin(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            string caption;
            MessageBoxButton button;
            MessageBoxImage icon;
            MessageBoxResult result;
            ContainsDigit(txtWachtwoord.Password.ToString());
            ContainsSpecial(txtWachtwoord.Password.ToString());

            if (string.IsNullOrWhiteSpace(txtGebruikersnaam.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtWachtwoord.Password.ToString()))
            {
                messageBoxText = "Alle velden moeten ingevuld zijn";
                caption = "FAILED: Één of meerdere velden zijn leeg";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else if (!IsEmailValid(txtEmail.Text) || (!txtEmail.Text.EndsWith(".nl") && !txtEmail.Text.EndsWith(".com")))
            {
                messageBoxText = "Email is ongeldig";
                caption = "FAILED: Email Ongeldig";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else if (!digits || !special || txtWachtwoord.Password.ToString().Length <= 7)
            {
                messageBoxText = "Wachtwoord moet 8 tekens lang zijn, een cijfer en een speciale teken bevatten";
                caption = "FAILD: Wachtwoord voldoet niet aan de eisen";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else
            {
               admin= new Admin(txtGebruikersnaam.Text, txtWachtwoord.Password.ToString(), txtEmail.Text);
                messageBoxText = "Admin is aangemaakt";
                caption = "SUCCES";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Information;
            }
            result = MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
