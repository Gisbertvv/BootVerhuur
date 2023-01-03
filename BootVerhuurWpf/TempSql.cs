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

namespace BootVerhuurWpf
{
    public class TempSql : Database
    {
        public int aantalp { get; set; }
        public string bootniveau { get; set; }
        public bool stir { get; set; }
        public string status { get; set; }
        public int id { get; set; }

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
                    if (Login.boatingLevel.Equals("C"))
                    {
                        query = $"SELECT TOP 1 * FROM boat where NOT level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
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
                                id = reader.GetInt32(0);
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
        /// Gets the total amout of boats that there are for the boating_level of the user
        /// </summary>
        public int GetCountboats()
        {
            int count = 0;  
            try
            {
                using (var connection = GetConnection())
                {

                    string query = string.Empty;
                    if (Login.boatingLevel.Equals("C"))
                    {
                        query = $"SELECT Count(*) FROM boat where Not level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
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
    }
}

