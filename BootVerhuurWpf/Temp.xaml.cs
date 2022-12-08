
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


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
        int count;
        public Temp()
        {
            InitializeComponent();
            showboats();
        }

        private void showboats()
        {
            List<Boat> boats = new List<Boat>();
            int id = 0;
            while (boats.Count < 6)
            {
                Checkeverything(id);
                boats.Add(new Boat(id, aantalp, stir, bootniveau));
                id++;
            }
            Boats.ItemsSource = boats;
        }

        public void Checkeverything(int id)
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
                    String sql = $"SELECT * FROM boat where boat_id = {id}";
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

            int i = Boats.SelectedIndex;

            BookBoat bk = new BookBoat(i);
            bk.Show();
            Close();
        }
    }
}
