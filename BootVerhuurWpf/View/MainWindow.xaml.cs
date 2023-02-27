using System.Windows;
namespace BootVerhuurWpf
{  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (LoginController.role == "admin" || LoginController.role == "Admin")
            {
                btn0.Visibility = Visibility.Visible;
                btn1.Visibility = Visibility.Visible;
                btn2.Visibility = Visibility.Visible;
                btn3.Visibility = Visibility.Hidden;
                btn4.Visibility = Visibility.Hidden;
            }
            else
            {
                btn0.Visibility = Visibility.Hidden;
                btn1.Visibility = Visibility.Hidden;
                btn2.Visibility = Visibility.Hidden;
                btn3.Visibility = Visibility.Visible;
                btn4.Visibility = Visibility.Visible;
            }

            if (LoginController.boatingLevel == "A" || LoginController.boatingLevel == "a" || LoginController.boatingLevel == "B" || LoginController.boatingLevel == "b")
            {
                btn3.Visibility = Visibility.Hidden;
                btn4.Visibility = Visibility.Hidden;
            }
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            PDF window = new PDF();
            window.Show();
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings();
            this.Close();
            window.Show();
            
        }

        private void OpenCreateUserPanel(object sender, RoutedEventArgs e)
        {
            Popup popup = new Popup();
            popup.Show();
            Close();
        }

        private void OpenEditUserPanel(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit();
            edit.Show();
            Close();
        }

        private void OpenReservePanel(object sender, RoutedEventArgs e)
        {
            List tp = new List();
            this.Close();
            tp.Show();
            
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            Close();
        }

        private void ShowMemberReservations(object sender, RoutedEventArgs e)
        {
            Reservations memberReservations = new Reservations();
            memberReservations.Show();
            Close();
        }
    }
}
