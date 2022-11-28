using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateAdmin createAdmin = new CreateAdmin();
            createAdmin.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateMember createMember = new CreateMember();
            createMember.Show();
            this.Close();
        }
        /*
public string ResponseText
{
  get { return ResponseTextBox.Text; }
  set { ResponseTextBox.Text = value; }
}

private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
{
  DialogResult = true;
}*/
    }
    }

