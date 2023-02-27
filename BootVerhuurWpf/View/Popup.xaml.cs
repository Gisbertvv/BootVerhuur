using System;
using System.Windows;


namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Popup : Window
    {
        public Popup()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin createAdmin = new Admin();
            createAdmin.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Member createMember = new Member();
            createMember.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            Close();
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings();
            this.Close();
            window.Show();

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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}

