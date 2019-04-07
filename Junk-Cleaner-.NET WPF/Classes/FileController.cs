using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Junk_Cleaner_.NET_WPF
{
    public class FileController
    {
        private delegate void displayProgress();
        private delegate void displayFiles();
        private delegate void displayDir();
        private Thread trdInitialSearch;
        private Thread trdExecuteJob;
        private Thread trdUpdateUI;
        private ProgressBar progressBar;
        private DataGrid grdDisplay;
        private Expander expDirectory;
        private int intStatusPrc;
        private List<Files> files;
        private bool isEnded;
        private string path;
        private long totalFiles;
        private long totalFolders;
        private long totalSize;

        private long currentFile;

        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="path">Search for files inside this path</param>
        /// <param name="files">List of files to be manipulated</param>
        /// <param name="progressBar">The ProgressBar to be updated</param>
        /// <param name="grdDisplay">The DataGrid to be updated</param>
        public FileController(string path, List<Files> files, ProgressBar progressBar, DataGrid grdDisplay, Expander expDirectory)
        {
            isEnded = false;
            intStatusPrc = 0;
            totalFiles = 0;
            totalFolders = 0;
            currentFile = 0;
            totalSize = 0;
            initialSearch();
            this.path = path;
            this.files = files;
            this.progressBar = progressBar;
            this.grdDisplay = grdDisplay;
            this.expDirectory = expDirectory;
            SearchForFiles();
        }


        public void SearchForFiles()
        {
            try
            {
                files.Clear();
                trdUpdateUI = new Thread(new ThreadStart(UpdateUIRunner));
                trdUpdateUI.IsBackground = true;
                trdUpdateUI.Start();
                trdExecuteJob = new Thread(new ThreadStart(SearchRunner));
                trdExecuteJob.IsBackground = true;
                trdExecuteJob.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void updateProgressStatus()
        {
            try
            {
                if (totalFiles != 0)
                {
                    intStatusPrc = (int)((currentFile * 100) / totalFiles);
                    progressBar.Value = intStatusPrc;
                }
                else progressBar.Value = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void updateDataGrid()
        {
            try
            {

                grdDisplay.ItemsSource = null;
                grdDisplay.ItemsSource = files;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void shortDataGrid()
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(grdDisplay.ItemsSource);
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("length", ListSortDirection.Descending));
                view.Refresh();
                //else grdDisplay.ItemsSource = files.OrderByDescending(o => o.length).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void updateExpander()
        {
            try
            {
                string strOutput = "Location : {0} \r\nSize     : {3} {4}\r\nContains : {1} Files, {2} Folders";
                string strTotalSize = "(" + String.Format("{0:#,##0}", totalSize) + " bytes)";
                expDirectory.Header = string.Format(strOutput, path, totalFiles, totalFolders, Globals.sizeFix(totalSize, 2), strTotalSize);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateUIRunner()
        {
            try
            {
                while (!isEnded)
                {
                    progressBar.Dispatcher.Invoke(new displayProgress(updateProgressStatus));
                    grdDisplay.Dispatcher.Invoke(new displayFiles(updateDataGrid));
                    expDirectory.Dispatcher.Invoke(new displayDir(updateExpander));
                    Thread.Sleep(10);
                }
                progressBar.Dispatcher.Invoke(new displayProgress(updateProgressStatus));
                grdDisplay.Dispatcher.Invoke(new displayFiles(updateDataGrid));
                grdDisplay.Dispatcher.Invoke(new displayFiles(shortDataGrid));
                expDirectory.Dispatcher.Invoke(new displayDir(updateExpander));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SearchRunner()
        {
            try
            {
                Thread.Sleep(500);
                FillListFiles(path);
                isEnded = true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void initialSearch()
        {
            try
            {
                trdInitialSearch = new Thread(new ThreadStart(UpdateTotalFilesFolders));
                trdInitialSearch.IsBackground = true;
                trdInitialSearch.Start();  
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateTotalFilesFolders()
        {
            try
            {
                FindTotalFiles(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FindTotalFiles(string path)
        {
            try
            {
                if (path.Length > 248)
                    return;
                DirectoryInfo dir = new DirectoryInfo(path);
                totalFiles += dir.GetFiles().Length;
                DirectoryInfo[] dirs = dir.GetDirectories();
                totalFolders += dirs.Length;
                foreach (DirectoryInfo subdir in dirs)
                    try{ FindTotalFiles(subdir.FullName); }
                    catch (UnauthorizedAccessException){ continue; }
            }
            catch (PathTooLongException) { throw; }
            catch (DirectoryNotFoundException) { throw; }
            catch (Exception)
            {
                throw;
            }
        }

        public void FillListFiles(string path)
        {
            try
            {
                if (path.Length > 248)
                    return;
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists) throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + path);

                FileInfo[] filesIO = dir.GetFiles();
                foreach (FileInfo file in filesIO)
                    try
                    {
                        Files f = new Files(file.Name, Path.Combine(path, file.Name), file.Length);
                        files.Add(f);
                        totalSize += file.Length;
                        currentFile++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }

                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                    try { FillListFiles(subdir.FullName); }
                    catch (UnauthorizedAccessException) { continue; }

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message + " Directory upload Error");
                throw;
            }
        }
    }
}

//FileIOPermission permis = new FileIOPermission(FileIOPermissionAccess.AllAccess, file.DirectoryName);
//bool all = true;
//try
//{
//    permis.Demand();
//    permis.PermitOnly();

//}
//catch (System.Security.SecurityException ex)
//{
//    all = false;
//}
//if (all)