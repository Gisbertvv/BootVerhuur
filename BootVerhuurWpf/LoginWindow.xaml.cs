using System.Windows;

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

        /*protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
        }*/

    }
}
