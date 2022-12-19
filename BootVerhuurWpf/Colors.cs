using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BoldReports.Windows.Data;
using BootVerhuur;

namespace BootVerhuurWpf
{
    class Colors:Database
    {
        public string[] GetColors()
        {
            string[] color = null;
            try
            {
                using (var connection = GetConnection())
                {
                    String sql = "SELECT primary_color, secondary_color, background_color FROM appSettings";
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

                    connection.Close();
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
