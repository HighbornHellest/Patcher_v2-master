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
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            String[] res = Resolution.Text.Split('x');
            ushort freq = UInt16.Parse(Frequency.Text);
            ushort gamma = UInt16.Parse(Gamma.Text);
            string windowed = "0";
            bool IME_GAME = false;

            //lang
            var langCode = Patcher.Properties.Settings.Default.languageCode;
            switch (langCode)
            {
                case "hu-HU":
                    langCode = "0";
                    break;
                case "en-US":
                    langCode = "1";
                    break;
                default:
                    langCode = "1";
                break;
            }

            if (radio_windowed.IsChecked == true)
            {
                windowed = "1";
            }
            else if (radio_full.IsChecked == true)
            {
                windowed = "0";
            }
            else if (radio_borderless.IsChecked == true)
            {
                windowed = "3";
            }
           /* if ((bool)IME_GAME.IsChecked)
            {
                IME_GAME = true;
            }*/

            String[] config_lines={"LANGCODE "+langCode, "WIDTH"+" "+ res[0],"HEIGHT"+" "+res[1], "BPP"+" "+"32",
                "FREQUENCY"+ " "+ Frequency.Text, "SOFTWARE_CURSOR" + (bool)SOFT_CUR.IsChecked,
                "OBJECT_CULLUNG 1", "MUSIC_VOLUME "+ BG_SCROLL.Value,
                "VOICE_VOLUME " + EF_SCROLL.Value, "USE_DEFAULT_IME" + IME_GAME,
                "FOG_MODE_ON 1", "SNOW_MODE_ON 1", "SHOW_MOBLEVEL 1", "SHOW_MOBAIFLAG 1",
                "WINDOWED"+" " + windowed };

            File.Delete(@".\metin");
            string path = @".\metin";
            File.WriteAllLines(path, config_lines);

            //File.Delete(@".\User\Language.ycfg");
            System.IO.File.WriteAllText(@"User\Language.ycfg",langCode);


            

            this.NavigationService.Navigate(main_window.launcher);
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }


    }
}
