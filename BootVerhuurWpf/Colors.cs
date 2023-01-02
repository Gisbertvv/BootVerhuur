using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BoldReports.Windows.Data;
//using BootVerhuur;

namespace BootVerhuurWpf
{
    class Colors:Database
    {
        public string[] GetColors()
        {
            /// <summary>
            ///  Returns a the primary color, secondary color, background color from the database
            /// </summary>
            /// <returns>string array with colors (primary, secondary, background) </returns>
            string[] color = null;
            try
            {
                using (var connection = GetConnection())
                {
     
                    String sql = "SELECT primary_color, secondary_color, background_color FROM appSettings";
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                color = new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                            }
                        }
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
            /// <summary>
            ///  Sets the the primary color to the database
            /// </summary>
            try
            {
                using (var connection = GetConnection())
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
        /// <summary>
        ///  Sets the the secondary color to the database
        /// </summary>
            try
            {
            using (var connection = GetConnection())
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
        /// <summary>
        ///  Sets the the background color to the database
        /// </summary>
            try
            {
                using (var connection = GetConnection()) 
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
