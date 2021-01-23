using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Security.Cryptography;



namespace Patcher
{
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Launcher : Page
    {
        private bool IsPatched { get; set; }

        public Launcher()
        {
            InitializeComponent();
            TextPatchinfo.Text = "Patcher Updated by Highborn";
            this.IsPatched = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
      
        }

        private bool IsWin7OrHigher()
        {
            OperatingSystem OS = Environment.OSVersion;
            if (OS.Version.Major >= 6)
            {
                if (OS.Version.Minor >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        




        private void BtnSettingsClick(object sender, MouseButtonEventArgs e)
        {

            this.NavigationService.Navigate(new Settings());
           
        }

        private void BtnHomepageClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Config.HomepageURL);
        }

        private void BtnPlayClick(object sender, MouseButtonEventArgs e)
        {
            BtnPlay.IsEnabled = false;


            if (this.IsPatched)
            {
                this.StartGame();
                Console.WriteLine("Game is starting");
            }
            else
            {
                Console.WriteLine("starting downloadworker");
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += delegate
                {
                    this.GetPatchlist();
                };
                bgWorker.RunWorkerAsync();
            }
        }

        private void SetLblFile(string FileName)
        {
            LblFile.Dispatcher.Invoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate
            {
                LblFile.Content = String.Format("File: {0}", FileName);
                return null;
            }), null);
        }

        private void AddTextToList(string Text)
        {
            TextPatchinfo.Dispatcher.Invoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate
            {
                TextPatchinfo.Text += String.Format("{0}\r\n", Text);
                return null;
            }), null);
            ScrollText.Dispatcher.Invoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate
            {
                ScrollText.ScrollToEnd();
                return null;
            }), null);
        }

        private void EventDownloadProgres(object sender, DownloadProgressChangedEventArgs e)
        {
            LblFilePercent.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                LblFilePercent.Content = String.Format("{0}%", e.ProgressPercentage);
                return null;
            }), null);
            ProgressFile.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                ProgressFile.Width = 391 * e.ProgressPercentage / 100;
                return null;
            }), null);
        }

        private void SetTotalStatus(int FileNr, int TotalFiles)
        {
            int percent = 0;
            try
            {
                percent = 100 * FileNr / TotalFiles;
            }
            catch { }
            LblTotal.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                LblTotal.Content = String.Format("Total: {0} of {1} files", FileNr, TotalFiles);
                return null;
            }), null);
            LblTotalPercent.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                LblTotalPercent.Content = String.Format("{0}%", percent);
                return null;
            }), null);
            ProgressTotal.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                ProgressTotal.Width = 391 * percent / 100;
                return null;
            }), null);
        }

        private void GetPatchlist()
        {
            this.SetLblFile("Patchlist");
            this.AddTextToList("Getting patchlist...");

            WebClient DlPatchlist = new WebClient();
            DlPatchlist.Proxy = null;
            DlPatchlist.DownloadProgressChanged += new DownloadProgressChangedEventHandler(EventDownloadProgres);
            DlPatchlist.DownloadFileCompleted += delegate
            {

                this.AddTextToList("Patchlist has been downloaded.\r\n");

                this.ParsePatchlist();

                this.AddTextToList("reee");

            };
            DlPatchlist.DownloadFileAsync(new Uri(String.Format("{0}filelist/filelist.xml", Config.PatchserverURL)), "patchlist.xml");
            
        }

        private void ParsePatchlist()
        {
            PatchList Patchlist = PatchList.LoadFromXml("patchlist.xml");
            File.Delete("patchlist.xml");

            this.SetTotalStatus(0, Patchlist.PatchFiles.Count);

            this.DeleteFiles(Patchlist.DeleteFiles);
            this.CreateDirectories(Patchlist.PatchDirectories);
            this.PatchFile(0, Patchlist.PatchFiles);
        }

        private void DeleteFiles(List<DeleteFile> DeleteFiles)
        {
            foreach (var DeleteFile in DeleteFiles)
            {
                if (File.Exists(DeleteFile.Name))
                {
                    File.Delete(DeleteFile.Name);
                    this.AddTextToList(String.Format("{0} Has been deleted.\r\n", DeleteFile.Name));
                }
            }
        }

        private void CreateDirectories(List<PatchDirectory> PatchDirectories)
        {
            foreach (var PatchDirectory in PatchDirectories)
            {
                if (!Directory.Exists(PatchDirectory.Name))
                {
                    Directory.CreateDirectory(PatchDirectory.Name);
                    this.AddTextToList(String.Format("{0} has been done.\r\n", PatchDirectory.Name));
                }
            }
        }

        public string GetFileHash(string file)
        {
            if (!File.Exists(file))
                return null;
            using (var fStream = File.OpenRead(file))
                return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(fStream)).Replace("-", "").ToLower();
        }

        private void DownloadFile(int FileNr, List<PatchFile> PatchFiles)
        {
            this.SetLblFile(PatchFiles[FileNr].Name);
            this.AddTextToList(String.Format("Download {0}...", PatchFiles[FileNr].Name));

            WebClient FileDownload = new WebClient();
            FileDownload.Proxy = null;
            FileDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(EventDownloadProgres);
            FileDownload.DownloadFileCompleted += delegate
            {
                this.AddTextToList(String.Format("{0} downloaded.\r\n", PatchFiles[FileNr].Name));
                FileNr++;
                this.PatchFile(FileNr, PatchFiles);
            };
            FileDownload.DownloadFileAsync(new Uri(String.Format("{0}client/{1}", Config.PatchserverURL, PatchFiles[FileNr].Name)), PatchFiles[FileNr].Name);
        }

        private void PatchFile(int FileNr, List<PatchFile> PatchFiles)
        {
            if (this.IsWin7OrHigher())
            {
              
            }
            if (FileNr >= PatchFiles.Count)
            {
                this.SetTotalStatus(FileNr, PatchFiles.Count);
                this.IsPatched = true;
                this.StartGame();
            }
            else
            {
                this.SetTotalStatus(FileNr, PatchFiles.Count);
                this.AddTextToList(String.Format("Check {0}...", PatchFiles[FileNr].Name));

                if (File.Exists(PatchFiles[FileNr].Name))
                {
                    if (this.GetFileHash(PatchFiles[FileNr].Name) != PatchFiles[FileNr].Hash)
                    {
                        this.DownloadFile(FileNr, PatchFiles);
                    }
                    else
                    {
                        this.AddTextToList(String.Format("{0} is actual\r\n", PatchFiles[FileNr].Name));
                        FileNr++;
                        this.PatchFile(FileNr, PatchFiles);
                    }
                }
                else
                {
                    this.DownloadFile(FileNr, PatchFiles);
                }
            }
        }

        private void StartGame()
        {

            this.AddTextToList("Game is Starting.\r\n");

            Process proc = new Process();
            proc.StartInfo.FileName = Config.BinaryName;
            proc.StartInfo.UseShellExecute = true;
            try
            {
                proc.Start();
            }
            catch
            {
                MessageBox.Show(String.Format("File {0} doesn't exist.", Config.BinaryName), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (this.IsWin7OrHigher())
            {
              
            }

            BtnPlay.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                BtnPlay.IsEnabled = true;
                return null;
            }), null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Updater.exe"))
            {
                File.Delete("Updater.exe");
            }
            WebClient wbClient = new WebClient();
            wbClient.Proxy = null;
          //  wbClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(VersionDownload); //updater?
            wbClient.DownloadStringAsync(new Uri(String.Format("{0}admin/index.php?ajax&version", Config.PatchserverURL)));
        }

        public void VersionDownload(Object sender, DownloadStringCompletedEventArgs e) //updater
        {
            if (!e.Cancelled && e.Error == null)
            {
                string result = (string)e.Result;
                string[] lines = result.Split('\n');

                if (lines[0] != System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
                {
                    BtnPlay.IsEnabled = false;
                    MessageBox.Show("New patcher version is available, is downloading pleasae be patient. This may take a while!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    LblFile.Content = "Data: Updater.exe";

                    WebClient FileDownload = new WebClient();
                    FileDownload.Proxy = null;
                    FileDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(EventDownloadProgres);
                    FileDownload.DownloadFileCompleted += delegate
                    {
                        try
                        {
                            Process.Start(@"Updater.exe");
                            
                        }
                        catch
                        {
                            MessageBox.Show("Updater.exe was not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    };
                    FileDownload.DownloadFileAsync(new Uri(String.Format(@"Updater.exe", Config.PatchserverURL)), "Updater.exe");
                }
            }

        }
    }





}
