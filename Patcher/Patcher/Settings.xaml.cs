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
using System.IO;

namespace Patcher
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        //this is the cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(main_window.launcher);
        }

        //this is the accept
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String[] res = Resolution.Text.Split('x');
            ushort freq = UInt16.Parse(Frequency.Text);
            ushort gamma = UInt16.Parse(Gamma.Text);
            string windowed="0";

            if (radio_windowed.IsChecked == true)
            {
                windowed = "1";
            }
            if (radio_full.IsChecked == true)
            {
                windowed = "0";
            }

            String[] config_lines={"WIDTH"+" "+ res[0],"HEIGHT"+" "+res[1], "BPP"+" "+"32",
                "FREQUENCY"+ " "+ Frequency.Text,
                "WINDOWED"+" " + windowed };

            File.Delete(@".\metin");
            string path = @".\metin";
            File.WriteAllLines(path, config_lines);


            

            this.NavigationService.Navigate(main_window.launcher);
        }

        
    }
}
