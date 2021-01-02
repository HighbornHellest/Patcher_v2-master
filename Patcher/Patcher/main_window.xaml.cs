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
using System.Windows.Shapes;
using System.Windows.Navigation;


namespace Patcher
{
    /// <summary>
    /// Interaction logic for main_window.xaml
    /// </summary>
    /// 
   
    public partial class main_window : Window
    {
        public main_window()
        {
            InitializeComponent();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void BtnCloseClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizeClick(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public static dynamic launcher = new Launcher();

        public void Mainframe_Loaded(object sender, RoutedEventArgs e)
        {
            
            mainframe.NavigationService.Navigate(launcher);
        }


        //ez updateli magát a patchert, erre még nincs semmi megcsinálnva
        
      /*  public void VersionDownload(Object sender, DownloadStringCompletedEventArgs e)
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
                            Process.Start("Updater.exe");
                            //  this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Updated.exe was not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    };
                    FileDownload.DownloadFileAsync(new Uri(String.Format("{0}update/Updater.exe", Config.PatchserverURL)), "Updater.exe");
                }
            }
        }*/

    }

   
}

