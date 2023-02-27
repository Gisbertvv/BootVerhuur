using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Edit_member.xaml
    /// </summary>
    public partial class Edit : Window
    {
        
        UserController updateMemberTable = new UserController();
        public Edit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            updateMemberTable.UpdateTable(datagrid1);

        }

        public void ShowTable()
        {
            updateMemberTable.UpdateTable(datagrid1);
        }

        private void ReloadBTN_Click(object sender, RoutedEventArgs e)
        {
            updateMemberTable.UpdateTable(datagrid1);
        }

        // Code om database gegevens te weergeven in de textboxen
        private void Datagrid1SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void UpdateBTNClick(object sender, RoutedEventArgs e)
        {
            UserController edit = new UserController();
            edit.EditUser(first_nameTXTBX.Text, last_nameTXTBX.Text, emailTXTBX.Text, phoneTXTBX.Text, boating_levelTXTBX.Text, usernameTXTBX.Text, passwordTXTBX.Text, IDTXTBOX.Text);
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Weet u zeker dat u deze gebruiker permanent wilt verwijderen?",
                    "Confirmatie",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                UserController delete = new UserController();
                delete.DeleteUser(IDTXTBOX.Text);
            }
        }

/*        protected override void OnClosed(EventArgs e)
        {

            Application.Current.Shutdown();
        }*/

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings();

            window.Show();
            Close();
        }

        private void OpenCreateUserPanel(object sender, RoutedEventArgs e)
        {
            Popup popup = new Popup();
            popup.ShowDialog();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            Close();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}