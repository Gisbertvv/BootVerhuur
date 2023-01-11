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

            if (Login.role == "admin" || Login.role == "Admin")
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

            if (Login.boatingLevel == "A" || Login.boatingLevel == "a" || Login.boatingLevel == "B" || Login.boatingLevel == "b")
            {
                btn3.Visibility = Visibility.Hidden;
                btn4.Visibility = Visibility.Hidden;
            }
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            PDFWindow window = new PDFWindow();
            window.Show();
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel window = new AdminPanel();
            this.Close();
            window.Show();
            
        }

        private void OpenCreateUserPanel(object sender, RoutedEventArgs e)
        {
            Create popup = new Create();
            popup.Show();
            Close();
        }

        private void OpenEditUserPanel(object sender, RoutedEventArgs e)
        {
            Edit_member edit = new Edit_member();
            edit.Show();
            Close();
        }

        private void OpenReservePanel(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            this.Close();
            tp.Show();
            
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }

        private void ShowMemberReservations(object sender, RoutedEventArgs e)
        {
            MemberReservations memberReservations = new MemberReservations();
            memberReservations.Show();
            Close();
        }
    }
}
