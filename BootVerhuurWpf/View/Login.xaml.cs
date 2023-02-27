using System.Windows;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            Settings panel = new Settings();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginController login = new LoginController();
            login.GetLogin(txtUsernameOrEmail.Text, txtPassword.Password);
            bool s = login.GetLogin(txtUsernameOrEmail.Text, txtPassword.Password);


            if (s) { 
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }else if (!s)
            {
                MessageBox.Show("Gebruikersnaam/Email of wachtwoord is niet correct!");
            }
        }
    }
}
