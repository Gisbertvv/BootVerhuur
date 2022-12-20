using BoldReports.RDL.DOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
using BootVerhuur;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;
using WindowStartupLocation = System.Windows.WindowStartupLocation;
using Syncfusion.CompoundFile.DocIO.Net;
using Windows.System.Profile;
using System.IO;
using System.IO.Packaging;
using Microsoft.Identity.Client;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }



        private void AdminPanelInfo(object sender, RoutedEventArgs e)
        {

            if (PrimaryColor != null && SecondaryColor != null && BackgroundColor != null)
            {
                SetThemeColors(PrimaryColor.Color.ToString(), SecondaryColor.Color.ToString(),
                    BackgroundColor.Color.ToString());
            }

            MessageBoxResult dresult =
                MessageBox.Show("Om de wijzigingen toe te passen moet de applicatie opnieuw opgestart worden", "Alert", MessageBoxButton.YesNo);
            if (dresult == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public static void SetThemeColors(string PrimaryColor, string SecondaryColor, string BackgroundColor)
        {
            Colors.SetPrimaryColor(PrimaryColor);
            Colors.SetSecondaryColor(SecondaryColor);
            Colors.SetBackgroundColor(BackgroundColor);
        }

    

        private void OpenCreateUserPanel(object sender, RoutedEventArgs e)
        {
            Create popup = new Create();
            popup.ShowDialog();
            Close();
        }

        private void OpenEditUserPanel(object sender, RoutedEventArgs e)
        {

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

        private void OpenExplorer(string path, string fileName)
        {
            //Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            //bool? response = openFileDialog.ShowDialog();

            //if (response == true)
            //{
            //    string filePath = openFileDialog.FileName;

            //    return filePath;
            //}

            //return null;
            
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    // Get selected file name
                    var getFileName = System.IO.Path.GetFileName(dialog.FileName);
                    
                    // Change file name

                    //if (File.Exists(getFileName))
                    //{
                        System.IO.File.Move(getFileName, fileName);
                    //}
                    path = path + fileName;
                    File.Copy(dialog.FileName, path);
                }
            }
            catch(Exception ex) {
                
            }
        }
        private void UploadLogo(object sender, RoutedEventArgs e)
        {
            // Path and name of file are here
            OpenExplorer("D:\\OOSDDb\\BootVerhuur\\BootVerhuurWpf\\Logo\\", "logo1.png");

            //string fileLogo = OpenExplorer();
            //MessageBox.Show(fileLogo);
        }

        private void UploadBackground(object sender, RoutedEventArgs e)
        {
            // Path and name of file are here
            OpenExplorer("D:\\OOSDDb\\BootVerhuur\\BootVerhuurWpf\\BackgroundImage\\", "background1.png");

            //string fileBackground = OpenExplorer();
            //MessageBox.Show(fileBackground);

        }
    }
}
    

