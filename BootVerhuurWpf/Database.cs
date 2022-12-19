//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BootVerhuur
{
    public class Database
    {
        static SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder();

        public Database()
        {
            try
            {
                _builder.DataSource = "127.0.0.1";
                _builder.UserID = "SA";
                _builder.Password = "Havermout1325";
                _builder.InitialCatalog = "BootVerhuur";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
   
        public string[] GetColors()
        {
            string[] color = null;
            try
            {
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                using (connection)
                {
                    //SQL quary
                    String sql = "SELECT primary_color, secondary_color, background_color FROM appSettings";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                color = new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                            }
                        }
                        connection.Close();
                    }
                }
            }

            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            return color;
        }

        public static void SetPrimaryColor(string PrimaryColor)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                using (connection)
                {
                    //SQL query
                    String sql = $"UPDATE appSettings SET primary_color='{PrimaryColor}'";


                    using (SqlCommand command = new SqlCommand(sql, connection))
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
        public static void SetSecondaryColor(string SecondaryColor)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                using (connection)
                {
                    //SQL query
                    String sql = $"UPDATE appSettings SET secondary_color ='{SecondaryColor}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
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

        public static void SetBackgroundColor(string BackgroundColor)
        {

            try
            {
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                using (connection)
                {
                    //SQL query
                    String sql = $"UPDATE appSettings SET background_color ='{BackgroundColor}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
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
    }
}