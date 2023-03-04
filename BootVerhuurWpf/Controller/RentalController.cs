using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using BootVerhuurWpf.Model;

namespace BootVerhuurWpf;

public class RentalController :Database
{
    //public int memberId = Int32.Parse(LoginController.id);
    /// <summary>
    /// For the test if there is no loginController.id, memberId is set to 0
    /// </summary>
    public int memberId = LoginController.id != null ? Int32.Parse(LoginController.id) : 0;

    public int NumberOfPeople { get; set; }
    public string BoatLevel { get; set; }
    public string BoatingLevel { get; set; }

    public bool SteeringWheel { get; set; }
    public string Status { get; set; }
    public int ID { get; set; }
    

    string Date1;
    string Date2;

    public List<string> begintimes = new List<string>();
    public List<string> endtimes = new List<string>();

    public int reservationscount;
    public List<int> reservationIDS = new List<int>();
    public List<int> reservationIDS2 = new List<int>();
    public int BoatID { get; set; }
    public string ReservationDate { get; set; }
    public string ReservationFrom { get; set; }
    public string ReservationUntil { get; set; }
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// For the test if there is no loginController.id, memberID is set to 0
    /// </summary>
    public int memberID = LoginController.id != null ? Int32.Parse(LoginController.id) : 0;
    // public int memberID = Int32.Parse(LoginController.id);

    public RentalController(string date1, string date2)
    {
        Date1 = date1;
        Date2 = date2;
    }

    public RentalController()
    {
   
    }

    /// <summary>
    /// Puts all the begin and end reservationtimes for a specific boatid and date in a list
    /// </summary>
    public void GetReservationTimes(int id, string reservationdate)
    {

        try
        {
            using (var connection = GetConnection())
            {
                String sql =
                    $"SELECT * FROM reservation where boat_id = {id} AND reservationDate = '{reservationdate}' AND status = 'Actief' ";
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
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
            MessageBox.Show(e.ToString());
        }
    }

    /// <summary>
    /// Inserts a reservation into the database 
    /// </summary>
    /// <param name="reservationdate"></param>
    /// <param name="reservationtime"></param>
    /// <param name="endtime"></param>
    /// <returns></returns>
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

                    String query =
                        $"insert into reservation values ({i}, {ID},'{reservationdate}' , '{reservationtime}', '{endtime}', GetDate(),{memberId},'Actief')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                }

                GetReservationTimes(ID, reservationdate);

                MessageBox.Show("Reservering is aangemaakt", "SUCCES");
                return true;
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
            return false;
        }
    }

    /// <summary>
    /// Puts all the information from the database in variables give a specific boat_id
    /// </summary>
    /// <param name="id"></param>
    public void CheckEverything(int id)
    {
        ID = id;
        try
        {
            using (var connection = GetConnection())
            {
                String query = $"SELECT * FROM boat where boat_id = {ID}";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoatLevel = reader.GetString(2);
                            Status = reader.GetString(4);
                            SteeringWheel = reader.GetBoolean(3);
                            NumberOfPeople = reader.GetInt32(1);

                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }

    /// <summary>
    /// gets the next id to insert the reservation
    /// </summary>
    public int GetReservationID()
    {
        int count = 0;
        try
        {
            using (var connection = GetConnection())
            {
                String query = $"select count(*) from reservation";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);

                        }
                    }
                }

                return count + 1;
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
            return -1;
        }
    }

    /// <summary>
    /// check how many reservations a member has on the availible dates
    /// </summary>
    public int GetMemberIdCountReservations(string reservationdate, string reservationdate2)
    {
        int count = 0;
        try
        {
            using (var connection = GetConnection())
            {
                String query =
                    $"Select count(*) from reservation where member_id = {memberId} and (reservationDate = '{reservationdate}' or reservationDate = '{reservationdate2}') And status = 'Actief'";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);

                        }
                    }
                }
            }

            return count;
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
            return -1;
        }
    }

    /// <summary>
    /// Gets the id for when the level is C. 
    /// </summary>
    public void GetRightId()
    {
        try
        {
            using (var connection = GetConnection())
            {

                string query = string.Empty;
                if (LoginController.boatingLevel.Equals("C"))
                {
                    query = $"SELECT TOP 1 * FROM boat where NOT level = 'D'";
                }
                else if (LoginController.boatingLevel.Equals("D"))
                {
                    query = $"SELECT TOP 1 * FROM boat";
                }
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ID = reader.GetInt32(0);
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// Gets all the information of specific boat and puts them into variables
    /// </summary>
    /// <param name="id"></param>
    public void GetBoatInfo(int id)
    {
        try
        {
            using (var connection = GetConnection())
            {

                string query = $"SELECT * FROM boat where boat_id = {id}";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoatingLevel = reader.GetString(2);
                            Status = reader.GetString(4);
                            SteeringWheel = reader.GetBoolean(3);
                            NumberOfPeople = reader.GetInt32(1);
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// Gets the total amout of boats that there are for the boating_level of the user
    /// </summary>
    public int GetCountBoats()
    {
        int count = 0;
        try
        {
            using (var connection = GetConnection())
            {

                string query = string.Empty;
                if (LoginController.boatingLevel.Equals("C"))
                {
                    query = $"SELECT Count(*) FROM boat where Not level = 'D'";
                }
                else if (LoginController.boatingLevel.Equals("D"))
                {
                    query = $"SELECT Count(*) FROM boat";
                }
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);

                        }
                    }
                }
                return count;
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
            return -1;
        }
    }

    /// <summary>
    /// Changes the status of the active reservation that have passed their reservation date.
    /// </summary>
    /// <param name="Date1"></param>
    /// <param name="Date2"></param>
    public void ChangeStatus(string Date1, string Date2)
    {
        try
        {

            using (var connection = GetConnection())
            {

                String query = $"Update reservation set status = 'Verlopen' where not status = 'Geanulleerd' and (Not reservationDate = '{Date1}' and Not reservationDate = '{Date2}') ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// fills and returns a list with the active reservation ids
    /// </summary>
    public void GetReservationIds()
    {
        reservationIDS2.Clear();
        try
        {
            using (var connection = GetConnection())
            {

                String sql = $"Select * from reservation where member_id = {memberID} And status = 'Actief'";
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservationIDS2.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// changes the status of the given reservation to 'Geanulleerd' in the database
    /// </summary>
    /// <param name="reservationid"></param>
    public void CancelReservation(int reservationid)
    {
        try
        {
            using (var connection = GetConnection())
            {

                String query = $"update reservation set status = 'Geanulleerd' where reservation_id = {reservationid}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }

    }

    /// <summary>
    /// gets all the information from a specific reservationid
    /// </summary>
    /// <param name="reservationid"></param>
    public void GetReservationInfo(int reservationid)
    {
        try
        {
            using (var connection = GetConnection())
            {
                String query = $"Select * from reservation where reservation_id = {reservationid}";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoatID = reader.GetInt32(1);
                            ReservationDate = reader.GetString(2);
                            ReservationFrom = reader.GetString(3);
                            ReservationUntil = reader.GetString(4);
                            CreatedAt = reader.GetDateTime(5);
                            Status = reader.GetString(7);
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// gets all the ids from all the reservations and puts them in a list
    /// </summary>
    public void GetReservationId()
    {
        reservationIDS.Clear();
        try
        {
            using (var connection = GetConnection())
            {

                String sql = $"Select * from reservation where member_id = {memberID}";
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservationIDS.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// Puts all the reservationids of active reservations the member had into a list
    /// </summary>
    public void GetActiveReservationId()
    {
        reservationIDS.Clear();
        try
        {
            using (var connection = GetConnection())
            {

                String sql = $"Select * from reservation where member_id = {memberID} and status = 'Actief'";
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservationIDS.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// Gets how many reservations there are
    /// </summary>
    public void GetCountReservations()
    {
        try
        {
            using (var connection = GetConnection())
            {

                String query = $"Select Count(*) from reservation where member_id = {memberID}";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservationscount = reader.GetInt32(0);
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    /// <summary>
    /// gets the count of all the active reservations a member has
    /// </summary>
    public void GetCountActiveReservations()
    {
        try
        {
            using (var connection = GetConnection())
            {

                String query = $"Select Count(*) from reservation where member_id = {memberID} and status = 'Actief'";
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservationscount = reader.GetInt32(0);
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}