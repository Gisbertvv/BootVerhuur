using System.Data.SqlClient;
using System.Diagnostics;
using System.Management.Automation;



namespace BootVerhuur
{
    public class DatabaseConnectie
    {
        public SqlConnection Connection { get; set; }
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

