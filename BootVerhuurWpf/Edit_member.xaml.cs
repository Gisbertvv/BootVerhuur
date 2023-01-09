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
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Edit_member.xaml
    /// </summary>
    public partial class Edit_member : Window
    {
        
        EditMember updateMemberTable = new EditMember();
        public Edit_member()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            updateMemberTable.UpdateTable(datagrid1);

        }

        public void showTable()
        {
            updateMemberTable.UpdateTable(datagrid1);
        }

        private void reloadBTN_Click(object sender, RoutedEventArgs e)
        {
            updateMemberTable.UpdateTable(datagrid1);
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

            /*datagrid1.Columns[0].Visibility = Visibility.Hidden;*/
        }

        // Code om de database te updaten met de data uit de textboxen
        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
            EditMember edit = new EditMember();
            edit.EditUser(first_nameTXTBX.Text, last_nameTXTBX.Text, emailTXTBX.Text, phoneTXTBX.Text, boating_levelTXTBX.Text, usernameTXTBX.Text, passwordTXTBX.Text, IDTXTBOX.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Weet u zeker dat u deze gebruiker permanent wilt verwijderen?",
                    "Confirmatie",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                EditMember delete = new EditMember();
                delete.DeleteUser(IDTXTBOX.Text);
            }
        }

/*        protected override void OnClosed(EventArgs e)
        {

            Application.Current.Shutdown();
        }*/

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel window = new AdminPanel();

            window.Show();
            Close();
        }

        private void OpenCreateUserPanel(object sender, RoutedEventArgs e)
        {
            Create popup = new Create();
            popup.ShowDialog();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}