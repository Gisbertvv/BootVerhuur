using System.Windows;


namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateMember.xaml
    /// </summary>
    public partial class Member : Window
    {
        public Member()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

   
        private void ButtonAddUserClick(object sender, RoutedEventArgs e)
        {
            UserController.CreateMember(txtVoornaam.Text, txtAchternaam.Text, txtGebruikersnaam.Text, txtWachtwoord.Password, txtEmail.Text, _txtTelefoonnummer.Text, Rol.Text, Niveau.Text);
        }

        private void CheckAdmin(object sender, RoutedEventArgs e)
        {
            if (Rol.Text.Equals("Admin"))
            {
                Admin createAdmin = new Admin();
                createAdmin.Show();
                Close();
            }
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings();

            window.Show();
            Close();
        }

        private void OpenEditUserPanel(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit();

            edit.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            Close();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


    }
}

