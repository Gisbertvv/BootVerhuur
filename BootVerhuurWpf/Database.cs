//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
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

        /*public Database()
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
        }*/

        private  static string _connectionString;

        public Database()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["127.0.0.1, SA, Havermout1325, BootVerhuur"].ConnectionString;
        }


        protected static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}