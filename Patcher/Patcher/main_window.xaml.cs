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
using System.Threading;


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
            var langCode = Patcher.Properties.Settings.Default.languageCode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langCode);

            /*  if (Patcher.Properties.Settings.Default.languageCode == "en-US")
              {
                  LangBox.SelectedIndex = 0;
              }*/
            InitializeComponent();
            switch (Patcher.Properties.Settings.Default.languageCode)
            {
                case "hu-HU":
                    {
                        LangBox.SelectedIndex = 0;
                        break;
                    }
                case "en-US":
                    {
                        LangBox.SelectedIndex = 1;
                        break;
                    }
                
                default:
                    {
                        LangBox.SelectedIndex = 1;
                        break;
                    }
            }
            


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
            Environment.Exit(0);
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

        private void LangBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (LangBox.SelectedIndex)
            {

                case 0:
                    Properties.Settings.Default.languageCode = "hu-HU";
                    Properties.Settings.Default.Save();
                    break;

                case 1:
                    Properties.Settings.Default.languageCode = "en-US";
                    Properties.Settings.Default.Save();
                    break;

                

                default:
                    Properties.Settings.Default.languageCode = "en-US";
                    Properties.Settings.Default.Save();
                    break;

               
            }
            
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

