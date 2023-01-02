using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace BootVerhuurWpf
{
    public class Bookboat : Database
    {
        public int memberId = Int32.Parse(Login.id);
        public int aantalp { get; set; }
        public string bootniveau { get; set; }
        public bool stir { get; set; }
        public string status { get; set; }
        public int Id { get; set; }
        string Date1;
        string Date2;

       public List<string> begintimes = new List<string>();
        public List<string> endtimes = new List<string>();

        public Bookboat(string date1, string date2)
        {
            Date1 = date1;
            Date2 = date2;
        }

        /// <summary>
        /// puts all the begin and end reservationtimes for a specific boatid and date in a list
        /// </summary>
        public void Getreservationtimes(int id, string reservationdate)
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
                    String sql = $"SELECT * FROM reservation where boat_id = {id} AND reservationDate = '{reservationdate}' AND status = 'Actief' ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                begintimes.Add(reader.GetString(3));
                                endtimes.Add(reader.GetString(4));
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
        public bool InsertReservation(string reservationdate, string reservationtime, string endtime)
        {
            try
            {
                if (GetMemberIdCountReservations(Date1, Date2) == 2)
                {
                    MessageBox.Show("Je kunt maximaal 2 reserveringen hebben");
                    return false;
                }
                else
                {
                    int i = GetReservationID();

                    using (var connection = GetConnection())
                    {
                        connection.Open();

                        String query = $"insert into reservation values ({i}, {Id},'{reservationdate}' , '{reservationtime}', '{endtime}', GetDate(),{memberId},'Actief')";

                        SqlCommand sqlCmd = new SqlCommand(query, connection);

                        sqlCmd.CommandType = System.Data.CommandType.Text;

                        DataTable boat = new DataTable();

                        boat.Load(sqlCmd.ExecuteReader());

                        Getreservationtimes(Id, reservationdate);
                        
                        MessageBox.Show("Reservering is aangemaakt", "SUCCES");
                        return true;

                    }
                }
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }


        public void Checkeverything(int id)
        {
            Id = id;
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = $"SELECT * FROM boat where boat_id = {Id}";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();

                    boat.Load(sqlCmd.ExecuteReader());


                    bootniveau = boat.Rows[0]["level"].ToString();
                    status = boat.Rows[0]["availability"].ToString();
                    stir = (bool)boat.Rows[0]["stir"];
                    aantalp = (int)boat.Rows[0]["capacity"];

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// gets the next id to insert the reservation
        /// </summary>
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

        /// <summary>
        /// check how many reservations a member has on the availible dates
        /// </summary>
        public int GetMemberIdCountReservations(string reservationdate, string reservationdate2)
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = $"Select * from reservation where member_id = {memberId} and (reservationDate = '{reservationdate}' or reservationDate = '{reservationdate2}') And status = 'Actief'";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    
                    sqlCmd.CommandType = System.Data.CommandType.Text;

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



    }
    }

