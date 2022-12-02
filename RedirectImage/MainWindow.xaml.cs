using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedirectImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string oldDirectory = "D:\\OOSDDb\\BootVerhuur\\RedirectImage\\oldFolderImages\\";
            string newDirectory = "D:\\OOSDDb\\BootVerhuur\\RedirectImage\\RedirectedImages\\";

            DirectoryInfo directoryInfo = new DirectoryInfo(newDirectory);

            // If directory doesn't exist, create one
            if (directoryInfo.Exists == false)
            {
                Directory.CreateDirectory(newDirectory);
            }

            List<string> damageImages = Directory.GetFiles($"{oldDirectory}", "*.*", SearchOption.AllDirectories).ToList();

            foreach (string file in damageImages)
            {
                FileInfo damageFile = new FileInfo(file);
                // Only redirects when filenames are not the same with new directory
                if (new FileInfo(directoryInfo + "\\" + damageFile.Name).Exists == false)
                {
                   damageFile.MoveTo(directoryInfo + "\\" + damageFile.Name);
                }
            }
        }
    }
}
