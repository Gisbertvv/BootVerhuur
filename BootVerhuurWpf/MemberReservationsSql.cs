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
using Syncfusion.Windows.Controls.Cells;
using System.Reflection.PortableExecutable;
using BoldReports.RDL.DOM;
using System.Collections;
using System.Data.SqlTypes;

namespace BootVerhuurWpf
{
    public class MemberReservationsSql : Database
    {

        public int reservationscount;
        public List<int> reservationids = new List<int>();
        public List<int> reservationids2 = new List<int>();
        public int boatId { get; set; }
        public string reservationDate { get; set; }
        public string reservationFrom { get; set; }
        public string reservationUntil { get; set; }
        public DateTime createdAt { get; set; }
        public string status { get; set; }
        public int memberId = Int32.Parse(Login.id);



        public void GetReservationIds()
        {
            reservationids2.Clear();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = $"Select * from reservation where member_id = {memberId} And status = 'Actief'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservationids2.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CancelReservation(int reservationid)
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String query = $"update reservation set status = 'Geanulleerd' where reservation_id = {reservationid}";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();

                    boat.Load(sqlCmd.ExecuteReader());
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// gets all the information from a specific reservationid
        /// </summary>
        /// <param name="reservationid"></param>
        public void GetReservationInfo(int reservationid)
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String query = $"Select * from reservation where reservation_id = {reservationid}";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();

                    boat.Load(sqlCmd.ExecuteReader());
                    boatId = (int)boat.Rows[0]["boat_id"];
                    reservationDate = boat.Rows[0]["reservationDate"].ToString();
                    reservationFrom = boat.Rows[0]["reservationFrom"].ToString();
                    reservationUntil = boat.Rows[0]["reservationUntil"].ToString();
                    createdAt = (DateTime)boat.Rows[0]["createdAt"];
                    status = boat.Rows[0]["status"].ToString();
                }                                       
                }
            
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// gets all the ids from all the reservations and puts them in a list
        /// </summary>
        public void GetReservationId()
        {
            reservationids.Clear();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = $"Select * from reservation where member_id = {memberId}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservationids.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Gets how many reservations there are
        /// </summary>
        public void GetCountReservations()
        {
            OpenConnnection();
            try
            {
                
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String query = $"Select * from reservation where member_id = {memberId}";



                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();

                    boat.Load(sqlCmd.ExecuteReader());

                    reservationscount = boat.Rows.Count;

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }
}

        /*
        public int GetCountboats()
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = string.Empty;
                    if (Login.boatingLevel.Equals("C"))
                    {
                        query = $"SELECT * FROM boat where Not level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
                    {
                        query = $"SELECT * FROM boat";
                    }

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();

                    boat.Load(sqlCmd.ExecuteReader());

                    return boat.Rows.Count;


                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return 0;

            }
        }
    }
}
        */
