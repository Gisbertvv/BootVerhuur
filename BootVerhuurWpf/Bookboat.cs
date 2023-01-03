﻿using System;
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
                using (var connection = GetConnection())
                {
                    String sql = $"SELECT * FROM reservation where boat_id = {id} AND reservationDate = '{reservationdate}' AND status = 'Actief' ";
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

                        String query = $"insert into reservation values ({i}, {Id},'{reservationdate}' , '{reservationtime}', '{endtime}', GetDate(),{memberId},'Actief')";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }

                    }
                    Getreservationtimes(Id, reservationdate);

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
        public void Checkeverything(int id)
        {
            Id = id;
            try
            {
                using (var connection = GetConnection())
                {
                    String query = $"SELECT * FROM boat where boat_id = {Id}";
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bootniveau = reader.GetString(2);
                                status = reader.GetString(4);
                                stir = reader.GetBoolean(3);
                                aantalp = reader.GetInt32(1);

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
            int count =0;
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
                    return count +1;
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return-1;
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
                    String query = $"Select count(*) from reservation where member_id = {memberId} and (reservationDate = '{reservationdate}' or reservationDate = '{reservationdate2}') And status = 'Actief'";
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
    }
    }

