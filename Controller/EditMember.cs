﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
//using BootVerhuur;

namespace BootVerhuurWpf
{
    internal class EditMember :Database
    {
        private string HashPassword(string newPassword)
        {
            var newSalt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, newSalt);
            return newHashedPassword;
        }

        public void EditUser(string firstname, string lastname, string email, string phoneNumber, string level, string username, string password, string id)
        {
            try
            {

                using (var connection = GetConnection())
                {
                    String updateQuery = "UPDATE member SET first_name='" + firstname + "',last_name='" +
                                         lastname + "',email='" + email +
                                         "',phone_number='" + phoneNumber + "',boating_level='" +
                                         level + "',username='" + username +
                                         "',password='" + HashPassword(password) + "' WHERE id = '" + id + "'";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0),
                                    reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                    reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            MessageBox.Show("De gebruiker is aangepast!");
        }
        public void DeleteUser(string id){
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = "DELETE FROM member WHERE id = '" + id + "'";
                    SqlCommand sqlCmd = new SqlCommand(query, connection);
                    sqlCmd.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("howdydoody");
            }
        }

        public void UpdateTable(DataGrid datagrid1)
        {
            try
            {
                using (var connection = GetConnection())
                {
                     DataTable dt = new DataTable("member");
                    connection.Open();
                    String query =
                        "SELECT id,first_name,last_name,phone_number,email,boating_level,role,username,password FROM member";
                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);



                    adapter.Fill(dt);

                    datagrid1.ItemsSource = dt.DefaultView;

                    adapter.Update(dt);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Expres leeg gelaten omdat hij anders een "fout"melding geeft als je de tabel opnieuwd laadt voor de eerste keer
                /*MessageBox.Show("ex.Message");*/
            }
        }
    }
}
