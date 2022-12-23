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
                OpenConnnection();
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = string.Empty;
                    if (Login.boatingLevel.Equals("C"))
                    {
                        query = $"SELECT TOP 1 * FROM boat where NOT level = 'D'";
                    }
                    else if (Login.boatingLevel.Equals("D"))
                    {
                        query = $"SELECT TOP 1 * FROM boat";
                    }
                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();
                    boat.Load(sqlCmd.ExecuteReader());

                    id = (int)boat.Rows[0]["boat_id"];
                                                                                     
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
        public void GetBoatInfo(int id)
        {
            OpenConnnection();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                      string  query = $"SELECT * FROM boat where boat_id = {id}";
                    

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    DataTable boat = new DataTable();
                    boat.Load(sqlCmd.ExecuteReader());

                    bootniveau = boat.Rows[0]["level"].ToString();
                    status = boat.Rows[0]["availability"].ToString();
                    stir = (bool)boat.Rows[0]["stir"];
                    aantalp = (int)boat.Rows[0]["capacity"];

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
        /// <summary>
        /// Gets the total amout of boats that there are for the boating_level of the user
        /// </summary>
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

