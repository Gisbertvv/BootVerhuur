using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BootVerhuurWpf
{  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        //BookBoat bookBoat = new BookBoat();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AdminPanel panel = new AdminPanel();
            Color color = (Color)ColorConverter.ConvertFromString(panel.GetColors()[2]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            gridje.Background = solidColorBrush;

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
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            PDFWindow window = new PDFWindow();
            window.Show();
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel window = new AdminPanel();

            window.Show();
            Close();
        }


        private void OpenCreateUserPanel(object sender, RoutedEventArgs e)
        {
            Create popup = new Create();
            popup.ShowDialog();
            Close();
        }

        private void OpenEditUserPanel(object sender, RoutedEventArgs e)
        {

        }

        private void OpenReservePanel(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            Close();
        }
    }
}
