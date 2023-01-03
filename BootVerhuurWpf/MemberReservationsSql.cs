using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
                    using(SqlCommand command= new SqlCommand(query, connection))
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
            reservationids2.Clear();
            try
            {
                using (var connection = GetConnection())
                {

                    String sql = $"Select * from reservation where member_id = {memberId} And status = 'Actief'";
                   connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
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
                MessageBox.Show(e.ToString());
            }
        }
        /// <summary>
        /// changes the stauts of the given reservation to 'Geanulleerd' in the database
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
                                boatId = reader.GetInt32(1);
                                reservationDate = reader.GetString(2);
                                reservationFrom = reader.GetString(3);
                                reservationUntil = reader.GetString(4);
                                createdAt = reader.GetDateTime(5);
                                status = reader.GetString(7);
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
            reservationids.Clear();
            try
            {
                using (var connection = GetConnection())
                {

                    String sql = $"Select * from reservation where member_id = {memberId}";
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
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
                MessageBox.Show(e.ToString());
            }
        }
        /// <summary>
        /// Puts all the reservationids of active reservations the member had into a list
        /// </summary>
        public void GetActiveReservationId()
        {
            reservationids.Clear();
            try
            {
                using (var connection = GetConnection())
                {

                    String sql = $"Select * from reservation where member_id = {memberId} and status = 'Actief'";
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
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

                    String query = $"Select Count(*) from reservation where member_id = {memberId}";
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

                    String query = $"Select Count(*) from reservation where member_id = {memberId} and status = 'Actief'";
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
}

