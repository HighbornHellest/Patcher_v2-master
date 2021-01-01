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
           // mainframe.NavigationService.Navigate(null);
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

  

        public void Mainframe_Loaded(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new Launcher());
        }
    }

   
}

