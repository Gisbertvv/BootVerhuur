using System.Data.SqlClient;
using System.Windows;


namespace BootVerhuurWpf
{
    public class TempSql : Database
    {
        public int NumberOfPeople { get; set; }
        public string BoatingLevel { get; set; }
        public bool SteeringWheel { get; set; }
        public string Status { get; set; }
        public int ID { get; set; }

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

