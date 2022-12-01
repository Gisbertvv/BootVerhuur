using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuur
{
    internal class Database
    {
        
        public void test()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "TestDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\n Query data example:");
                    Console.WriteLine("==============================\n");

                    String sql = "SELECT * FROM Inventory";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                            }
                        }
                    }

                }
            }
            catch (SqlException e)
            {

                Console.WriteLine(e.ToString());
            }
            Console.ReadLine(); 
        }

        public void standardQeury(SqlConnection connection)
        {
            String sql = "SELECT name, collation_name FROM sys.databases";
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
        }
    }
}
