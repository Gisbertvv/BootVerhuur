using System.Windows;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        

        private void Create_Admin(object sender, RoutedEventArgs e)
        {
            UserController.CreateAdmin(txtGebruikersnaam.Text, txtWachtwoord.Password, txtEmail.Text);
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

