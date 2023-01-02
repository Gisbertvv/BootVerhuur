//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace BootVerhuurWpf
{
    public class Database
    {
        static SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder();

        public Database()
        {
            ///Information for database connection

            _builder.DataSource = "127.0.0.1";
            _builder.UserID = "sa";
            _builder.Password = "Havermout1325";
            _builder.InitialCatalog = "BootVerhuur";
        }

        protected static SqlConnection GetConnection()
        {
            return new SqlConnection(_builder.ConnectionString);
        }
        public void OpenConnnection()
        {

            try
            {
                string filename;
                if (File.Exists("..\\..\\..\\..\\lib\\Test.bat"))
                {
                    Console.WriteLine("Specified file exists.");
                    filename = "..\\..\\..\\..\\lib\\Test.bat";
                    Process process = new Process();
                    process.StartInfo.FileName = filename;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.Close();
                }
                else
                {
                    Console.WriteLine("Specified file does not " +
                                      "exist in the current directory.");
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine(e.ToString());
            }
        }
    }
}
