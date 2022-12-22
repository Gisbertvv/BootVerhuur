
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
        public Temp()
        {
            InitializeComponent();
            showboats();
        }

        private void showboats()
        {
            List<Boat> boats = new List<Boat>();
            GetRightId();
            GetCountboats();
            while (boats.Count < countboats)
            {
                GetBoats(id);
                boats.Add(new Boat(id, aantalp, stir, bootniveau, status));
                id++;
            }
            Boats.ItemsSource = boats;
        }
        /// <summary>
        /// Gets the total amout of boat that there are for boating level of the user
        /// </summary>
        public void GetCountboats()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";


                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string sql = string.Empty;
                    if (Login.boatingLevel.Equals("C"))
                    {
                        sql = $"SELECT COUNT(*) FROM boat where Not level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
                    {
                        sql = $"SELECT COUNT(*) FROM boat";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countboats = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        public void GetBoats(int id)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
               

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string sql = string.Empty;
                    if (Login.boatingLevel.Equals("C"))
                    {
                         sql = $"SELECT * FROM boat where boat_id = {id} AND Not level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
                        {
                        sql = $"SELECT * FROM boat where boat_id = {id}";
                    }
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                aantalp = reader.GetInt32(1);
                                bootniveau = reader.GetString(2);
                                stir = reader.GetBoolean(3);
                                status = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
        /// <summary>
        /// Gets the id for when the level is C. 
        /// </summary>
        public void GetRightId()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";


                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string sql = string.Empty;
                    if (Login.boatingLevel.Equals("C"))
                    {
                        sql = $"SELECT TOP 1 * FROM boat where NOT level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
                    {
                        sql = $"SELECT TOP 1 * FROM boat";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                 id = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        private void selectedboat(object sender, MouseButtonEventArgs e)
        {
           
            GetRightId();
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
            Close();
        }
    }
}
