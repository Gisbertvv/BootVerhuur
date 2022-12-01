using System.Data.SqlClient;
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
        public void SSH()
        {
            using var ps = PowerShell.Create();
            ps.AddScript("ssh -L 1433:localhost:1433 student@145.44.233.236").Invoke();
            
        }
    }
}
