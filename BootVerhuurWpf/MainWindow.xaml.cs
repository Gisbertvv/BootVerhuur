using Syncfusion.XlsIO.Implementation.PivotAnalysis;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            Login login = new Login();
            this.Close();
            login.Show();
        }



        private void Open_AdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel window = new AdminPanel();
            this.Close();
            window.Show();
            
        }

        private void reserve(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            this.Close();
            tp.Show();
            
        }
/*        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }*/

        private void Edit(object sender, RoutedEventArgs e)
        {
            Edit_member edit = new Edit_member();

            edit.Show();


        }
    }
}
