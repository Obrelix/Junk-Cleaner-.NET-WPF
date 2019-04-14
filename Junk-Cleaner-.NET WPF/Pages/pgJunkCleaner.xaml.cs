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
using Path = System.IO.Path;
using CheckBox = System.Windows.Controls.CheckBox;

namespace Junk_Cleaner_.NET_WPF
{
    /// <summary>
    /// Interaction logic for pgJunkCleaner.xaml
    /// </summary>
    public partial class pgJunkCleaner : Page
    {
        private List<string> lstPaths = new List<string>();
        private List<ErazedElements> lstErazedElements;
        private PathController fileController;
        public pgJunkCleaner()
        {
            InitializeComponent();
            string strAppDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;

            lstErazedElements = new List<ErazedElements>()
            {
                new ErazedElements("Windows Temp Files", grdAppElements, new List<string>()
                {
                    @"C:\Windows\Temp",
                    Path.Combine(strAppDataPath, @"Local\Temp")
                }),
                new ErazedElements("Google Chrome", grdAppElements, new List<string>()
                {
                    Path.Combine(strAppDataPath, @"Local\Google\Chrome\User Data")
                }),
                new ErazedElements("Mozilla Firefox", grdAppElements, new List<string>()
                {
                    Path.Combine(strAppDataPath, @"Local\Mozilla\Firefox\Profiles"),
                    Path.Combine(strAppDataPath, @"Roaming\Mozilla\Firefox\Profiles")
                }),
                new ErazedElements("Opera", grdAppElements, new List<string>()
                {
                    Path.Combine(strAppDataPath, @"Local\Opera\Opera"),
                    Path.Combine(strAppDataPath, @"Roaming\Opera\Opera")
                }),
                new ErazedElements("Internet Explorer", grdAppElements, new List<string>()
                {
                    Path.Combine(strAppDataPath, @"Local\Microsoft\Intern~1"),
                    Path.Combine(strAppDataPath, @"Local\Microsoft\Windows\History"),
                    Path.Combine(strAppDataPath, @"Local\Microsoft\Windows\Tempor~1"),
                    Path.Combine(strAppDataPath, @"Roaming\Microsoft\Windows\Cookies")
                }),
                new ErazedElements("Macromedia Flash Player", grdAppElements, new List<string>()
                {
                    Path.Combine(strAppDataPath, @"Roaming\Macromedia\Flashp~1")
                })

            };
        }


        public void changeSkinMode()
        {
            try
            {
                gbxAnalysisInfo.Foreground = Globals.LabelColor;
                expJunkFiles.Foreground = Globals.LabelColor;
                tbApplications.Foreground = Globals.LabelColor;
                foreach (ErazedElements element in lstErazedElements) element.changeSkinMode();
                if (!(fileController is null)) fileController.changeSkinMode();
            }
            catch (Exception){throw;}
        }

        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            //using (var dialog = new FolderBrowserDialog())
            //{
            //    DialogResult result = dialog.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //    }
            //}
            fileController = new PathController(lstErazedElements, pgbFilesStatus, grdFileInfo, txtTotalStatus, txtProcessStatus);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
