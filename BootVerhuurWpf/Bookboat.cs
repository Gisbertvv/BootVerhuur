using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Syncfusion.Windows.Shared;
using Windows.Networking;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BootVerhuurWpf
{
    public class Bookboat : Database
    {
        public int memberId = Int32.Parse(Login.id);
        public int aantalp { get; set; }
        public string bootniveau { get; set; }
        public bool stir { get; set; }
        public string status { get; set; }

        List<string> begintimes = new List<string>();
        List<string> endtimes = new List<string>();


        public void Checkeverything(int id)
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = $"SELECT * FROM boat where boat_id = {id}";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();

                    boat.Load(sqlCmd.ExecuteReader());

                    
                    bootniveau = boat.Rows[0]["level"].ToString();
                    status = boat.Rows[0]["availability"].ToString();
                    stir = (bool)boat.Rows[0]["stir"];
                    aantalp = (int)boat.Rows[0]["capacity"];

                    connection.Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

 
        public int GetReservationID()
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = $"select * from reservation";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable reservations = new DataTable();

                    reservations.Load(sqlCmd.ExecuteReader());

                    return reservations.Rows.Count + 1;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
        public int GetMemberIdCountReservations(string reservationdate, string reservationdate2)
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = $"Select * from reservation where member_id = @memberId and (reservationDate = '@reservationdate1' or reservationDate = '@reservationdate2')";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@memberId", memberId);
                    sqlCmd.Parameters.AddWithValue("@reservationdate1", reservationdate);
                    sqlCmd.Parameters.AddWithValue("@reservationdate2", reservationdate2);

                    DataTable reservations = new DataTable();

                    reservations.Load(sqlCmd.ExecuteReader());

                    return reservations.Rows.Count;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }


        public Bookboat() 
        {
            
        }


        /*
                 public Login(string username, string password)
        {
            OpenConnnection();
            try
            {
             
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = "SELECT * FROM member WHERE username=@username AND password=@password";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);
                        

                        //Check if username and password are correct
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@username", username);
                    sqlCmd.Parameters.AddWithValue("@password", password);

                    //DataTable dataTable = new DataTable();
                    DataTable userTable = new DataTable();

                    userTable.Load(sqlCmd.ExecuteReader());

                    if (userTable.Rows.Count == 1)
                    {
                        id = userTable.Rows[0]["id"].ToString();
                        firstName = userTable.Rows[0]["first_name"].ToString();
                        lastName = userTable.Rows[0]["last_name"].ToString();
                        phoneNumber = userTable.Rows[0]["phone_number"].ToString();
                        email = userTable.Rows[0]["email"].ToString();
                        boatingLevel = userTable.Rows[0]["boating_level"].ToString();
                        role = userTable.Rows[0]["role"].ToString();

                        //id = dataTable.Columns.Add("id").ColumnName;
                        //id = dataTable.Columns.Contains();
                        connection.Close();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usename of password not correct");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
         */
    }
}
