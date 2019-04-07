using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Binding = System.Windows.Data.Binding;

namespace Junk_Cleaner_.NET_WPF
{
    /// <summary>
    /// Interaction logic for pgJunkCleaner.xaml
    /// </summary>
    public partial class pgJunkCleaner : Page
    {
        public List<Files> lstFiles = new List<Files>();
        public Files Files { get; }
        public pgJunkCleaner()
        {
            InitializeComponent();
            grdFileInfo.ItemsSource = lstFiles;
        }


        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FileController SearchInDirectory = new FileController(dialog.SelectedPath, lstFiles, pgbFilesStatus, grdFileInfo, expDirectory);
                }
            }
        }
    }
}
