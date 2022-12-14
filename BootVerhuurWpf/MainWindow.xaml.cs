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
            if (Login.role == "admin" || Login.role == "Admin")
            {
                btn5.Visibility = Visibility.Visible;
                btn0.Visibility = Visibility.Visible;
            }
            else
            {
                btn5.Visibility = Visibility.Hidden;
                btn0.Visibility = Visibility.Hidden;
            }

            

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Create popup = new Create();
            popup.ShowDialog();
            this.Close();
        }

        private void OpenA_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TogglePopupButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            PDFWindow window = new PDFWindow();
            //AccidentReportWindow window = new AccidentReportWindow();
            //PdfWindow window = new PdfWindow();

            window.Show();
        }


        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Open_AdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel window = new AdminPanel();

            window.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Open_BookBoat(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }

    }
}
