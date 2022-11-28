using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuur
{
    public class DatabaseConnectie
    {
        static void Main(string[] args)
        {

			try
			{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
				builder.DataSource = "localhost";
				builder.UserID = "SA";
				builder.Password = "Havermout1325";
				builder.InitialCatalog = "BootVerhuur";

				using(SqlConnection connection = new SqlConnection(builder.ConnectionString))
				{
					Console.WriteLine("\n Query data example:");
					Console.WriteLine("==============================\n");

					String sql = "SELECT * FROM accidentReportPhoto";
					using(SqlCommand command = new SqlCommand(sql, connection))
					{
						connection.Open();
						using(SqlDataReader reader = command.ExecuteReader())
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
    }
}
