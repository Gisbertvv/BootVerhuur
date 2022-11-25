using BootVerhuur;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateMember.xaml
    /// </summary>
    public partial class CreateMember : Window
    {

        public Member Lid;

        public CreateMember()
        {
            InitializeComponent();
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            string caption;
            MessageBoxButton button;
            MessageBoxImage icon;
            MessageBoxResult result;

            if (!string.IsNullOrWhiteSpace(txtVoornaam.Text) && !string.IsNullOrWhiteSpace(txtAchternaam.Text) &&
                !string.IsNullOrWhiteSpace(txtGebruikersnaam.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !string.IsNullOrWhiteSpace(_txtTelefoonnummer.Text) && !string.IsNullOrWhiteSpace(Rol.Text) &&
                !string.IsNullOrWhiteSpace(Niveau.Text) && txtEmail.Text.Contains('@') 
                && txtEmail.Text.EndsWith(".nl") || txtEmail.Text.EndsWith(".com"))  
            {
                Lid = new Member(txtVoornaam.Text, txtAchternaam.Text, txtGebruikersnaam.Text, txtWachtwoord.Text, txtEmail.Text, _txtTelefoonnummer.Text, Rol.Text, Niveau.Text);

               messageBoxText = "User is aangemaakt";
                caption = "Succes";
                 button = MessageBoxButton.OK;
               icon = MessageBoxImage.Information;                           
            }
            else
            {
                 messageBoxText = "Een veld is niet goed ingevoerd";
                 caption = "Failed";
                 button = MessageBoxButton.OK;
                 icon = MessageBoxImage.Error;                 
            }
               result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }

        private void Check_Admin(object sender, RoutedEventArgs e)
        {
            if (Rol.Text.Equals("Admin"))
            {
                CreateAdmin createAdmin = new CreateAdmin();
                createAdmin.Show();
                this.Close();
            }
        }
    }

    
}
