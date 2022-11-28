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

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class CreateAdmin : Window
    {
        public Admin admin { get; set; }
        public CreateAdmin()
        {
            InitializeComponent();
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            CreateMember createMember = new CreateMember();
            createMember.Show();
            this.Close();
        }

        private void Create_Admin(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            string caption;
            MessageBoxButton button;
            MessageBoxImage icon;
            MessageBoxResult result;

            bool digits = false;
            bool special = false;

            string regexItem = @"\|!#$%&/+-()=?»«@£§€{}.-;'<>_,";

           
            foreach (char l in txtWachtwoord.Text)
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

            foreach( var item in regexItem)
            {    

                if (txtWachtwoord.Text.Contains(item))
                {
                    special = true;
                }
                if (special)
                {
                    break;
                }
            }
            if (!IsEmailValid(txtEmail.Text) || (!txtEmail.Text.EndsWith(".nl") && !txtEmail.Text.EndsWith(".com")))
            {
                messageBoxText = "Email is ongeldig";
                caption = "FAILED: Email Ongeldig";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else if (!digits || !special || txtWachtwoord.Text.Length <= 7)
            {
                messageBoxText = "Wachtwoord moet 8 tekens lang zijn, een cijfer en een speciale teken bevatten";
                caption = "FAILD: Wachtwoord voldoet niet aan de eisen";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else if (string.IsNullOrWhiteSpace(txtGebruikersnaam.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtWachtwoord.Text))
            {
                messageBoxText = "Alle velden moeten ingevuld zijn";
                caption = "FAILED: Één of meerdere velden zijn leeg";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else
            {
                admin = new Admin(txtGebruikersnaam.Text, txtWachtwoord.Text, Rol.Text, txtEmail.Text);
                messageBoxText = "Admin is aangemaakt";
                caption = "SUCCES";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Information;
            }
            result = MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
