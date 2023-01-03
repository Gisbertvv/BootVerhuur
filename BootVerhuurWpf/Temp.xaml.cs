
using Syncfusion.CompoundFile.DocIO.Native;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;


namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Temp.xaml
    /// </summary>
    public partial class Temp : Window
    {

        int aantalp;
        string bootniveau;
        bool stir;
        string status;
        int countboats;
        int id;
        TempSql tempSql = new TempSql();
        public Temp()
        {
            InitializeComponent();
            showboats();
        }
        /// <summary>
        /// Fills the datagrid with all the boats for the level of the member
        /// </summary>
        private void showboats()
        {
            List<Boat> boats = new List<Boat>();
            tempSql.GetRightId();
            id = tempSql.id;
            countboats = tempSql.GetCountboats();
            while (boats.Count < countboats)
            {
                tempSql.GetBoatInfo(id);
                aantalp = tempSql.aantalp;
                bootniveau = tempSql.bootniveau;
                stir = tempSql.stir;
                status = tempSql.status;
                boats.Add(new Boat(id, aantalp, stir, bootniveau, status));
                id++;
            }
            Boats.ItemsSource = boats;
        }
        /// <summary>
        /// When boat is selected opens new window. With the information of the boat_id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedboat(object sender, MouseButtonEventArgs e)
        {

            tempSql.GetRightId();
            id = tempSql.id;
            int i = Boats.SelectedIndex;

            i += id;

            BookBoats bk = new BookBoats(i);
            bk.Show();
            Close();
        }

        private void Member_reservations(object sender, RoutedEventArgs e)
        {
            MemberReservations memberreserveration = new MemberReservations();
            memberreserveration.Show();
        }
            private void AccidentReport(object sender, RoutedEventArgs e)
            {
                PDFWindow window = new PDFWindow();
                window.Show();
            }

            private void Logout(object sender, RoutedEventArgs e)
            {
                LoginWindow window = new LoginWindow();
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

