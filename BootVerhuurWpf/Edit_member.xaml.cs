using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Edit_member.xaml
    /// </summary>
    public partial class Edit_member : Window
    {
        DataTable dt = new DataTable("member");

        public Edit_member()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            showTable();

        }

        public void showTable()
        {
            DataTable dt = new DataTable("member");
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";
            builder.UserID = "sa";
            builder.Password = "Havermout1325";
            builder.InitialCatalog = "BootVerhuur";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();
                String query =
                    "SELECT id,first_name,last_name,phone_number,email,boating_level,role,username,password FROM member";
                SqlCommand sqlCmd = new SqlCommand(query, connection);

                sqlCmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);



                adapter.Fill(dt);

                datagrid1.ItemsSource = dt.DefaultView;

                adapter.Update(dt);


                connection.Close();
            }
            catch (Exception ex)
            {
                // Expres leeg gelaten omdat hij anders een "fout"melding geeft als je de tabel opnieuwd laadt voor de eerste keer
                /*MessageBox.Show("ex.Message");*/
            }
        }

        private void reloadBTN_Click(object sender, RoutedEventArgs e)
        {
            showTable();
        }

        // Code om database gegevens te weergeven in de textboxen
        private void datagrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = datagrid1.SelectedItem;

            IDTXTBOX.Text = (datagrid1.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            first_nameTXTBX.Text = (datagrid1.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            last_nameTXTBX.Text = (datagrid1.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            phoneTXTBX.Text = (datagrid1.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            emailTXTBX.Text = (datagrid1.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
            boating_levelTXTBX.Text = (datagrid1.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
            usernameTXTBX.Text = (datagrid1.SelectedCells[7].Column.GetCellContent(data) as TextBlock).Text;
            passwordTXTBX.Text = (datagrid1.SelectedCells[8].Column.GetCellContent(data) as TextBlock).Text;
        }

        // Code om de database te updaten met de data uit de textboxen
        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";
            builder.UserID = "sa";
            builder.Password = "Havermout1325";
            builder.InitialCatalog = "BootVerhuur";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String updateQuery = "UPDATE member SET first_name='" + this.first_nameTXTBX.Text + "',last_name='" +
                                     this.last_nameTXTBX.Text + "',email='" + this.emailTXTBX.Text +
                                     "',phone_number='" + this.phoneTXTBX.Text + "',boating_level='" +
                                     this.boating_levelTXTBX.Text + "',username='" + this.usernameTXTBX.Text +
                                     "',password='" + this.passwordTXTBX.Text + "' WHERE id = '"+this.IDTXTBOX.Text+"'";
               
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                        }
                    }
                }

            }

            MessageBox.Show("Gebruiker is aangepast!");
        }
    }
}