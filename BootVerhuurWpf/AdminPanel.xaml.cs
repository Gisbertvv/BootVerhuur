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
using System.Drawing;
using System.Resources;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        string pathLogo = @"C:/Users/gisbe/source/repos/BootVerhuur/BootVerhuurWpf/Images/Logo/";
        string pathBackground = @"C:/Users/gisbe/source/repos//BootVerhuur/BootVerhuurWpf/Images/Background/";
        string pathPDF = @"C:/Users/gisbe/source/repos/BootVerhuur/BootVerhuurWpf/PDF/";

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
            Edit_member edit = new Edit_member();
            edit.Show();
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

        private void OpenExplorer(string path, string fileName)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images only. | *.png;";

            DialogResult dialogResult = openFile.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(openFile.FileName);

                // assign safe name for saving
                string imgSafeName = fileName + ".png";

                // give generic banner name so only one file exists at a time
                string[] nameArray = imgSafeName.Split('.');
                string imgTempName = nameArray[0];
                string extension = nameArray[1];
                imgTempName = fileName;

                string pngString = imgTempName + ".png";

                // get debug folder path
                string appPath = path;

                // check if file path exits
                if (!System.IO.Directory.Exists(appPath))
                {
                    System.IO.Directory.CreateDirectory(appPath);
                }

                // if file exists, delete existing banner ad
                if (File.Exists(appPath + imgSafeName))
                {
                    File.Delete(appPath + imgSafeName);
                }

                // save new banner ad
                File.Copy(openFile.FileName, appPath + imgSafeName);

                // If the file was not a png, reopen file and save it as a png
                if (!extension.Equals("png"))
                {
                    // resave as png
                    System.Drawing.Image bannerImg = System.Drawing.Image.FromFile(appPath + imgSafeName);
                    bannerImg.Save(appPath + pngString, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void OpenExplorerPDF(string path, string fileName)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PDF only. | *.PDF;";

            DialogResult dialogResult = openFile.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFile.FileName;
                //MessageBox.Show(filePath);



                // assign safe name for saving
                string imgSafeName = fileName + ".pdf";

                // give generic banner name so only one file exists at a time
                string[] nameArray = imgSafeName.Split('.');
                string imgTempName = nameArray[0];
                string extension = nameArray[1];
                imgTempName = fileName;

                string pngString = imgTempName + ".pdf";

                // get debug folder path
                string appPath = path;

                // check if file path exits
                if (!System.IO.Directory.Exists(appPath))
                {
                    System.IO.Directory.CreateDirectory(appPath);
                }

                // if file exists, delete existing banner ad
                if (File.Exists(appPath + imgSafeName))
                {
                    File.Delete(appPath + imgSafeName);
                }

                // save new banner ad
                File.Copy(openFile.FileName, appPath + imgSafeName);
            }
        }

        private void UploadLogo(object sender, RoutedEventArgs e)
        {
            // Path and name of file are here
            OpenExplorer(pathLogo, "logo");
        }

        private void UploadBackground(object sender, RoutedEventArgs e)
        {
            // Path and name of file are here
            OpenExplorer(pathBackground, "background");
        }

        private void UploadPdf(object sender, RoutedEventArgs e)
        {
            // Path and name of file are here
            OpenExplorerPDF(pathPDF, "pdf");
        }
    }
}
    

