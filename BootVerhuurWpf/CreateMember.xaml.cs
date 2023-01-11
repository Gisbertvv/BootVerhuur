using System.Windows;


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

   
        private void ButtonAddUserClick(object sender, RoutedEventArgs e)
        {
            CreateFellow.CreateMember(txtVoornaam.Text, txtAchternaam.Text, txtGebruikersnaam.Text, txtWachtwoord.Password, txtEmail.Text, _txtTelefoonnummer.Text, Rol.Text, Niveau.Text);
        }

        private void CheckAdmin(object sender, RoutedEventArgs e)
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

        private void BackClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


    }
}

