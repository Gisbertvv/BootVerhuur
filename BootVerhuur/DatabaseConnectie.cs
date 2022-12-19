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
        public SqlConnection Connection { get; set; }
        public void OpenConnnection()
        {

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "BootVerhuur";
                SqlConnection connection = new(builder.ConnectionString);
                Connection = connection;
                Connection.Open();

            }
            catch (SqlException e)
            {

                Console.WriteLine(e.ToString());
            }
        }

    }
}

