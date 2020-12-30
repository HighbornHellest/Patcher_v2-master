using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Patcher - Updater ~~~");
            Console.WriteLine("\r\nPatcher is downloading! Please wait...");

            WebClient DLPatcher = new WebClient();
            DLPatcher.Proxy = null;
            DLPatcher.DownloadFile(new Uri(String.Format("{0}update/Patcher.exe", Config.PatchserverURL)), "Patcher.exe.tmp");

            Console.WriteLine("Patcher has been downloaded!");

            Console.WriteLine("\r\nOld patcher will be deleted ...");
            if (File.Exists(Config.PatcherEXE))
            {
                File.Delete(Config.PatcherEXE);
            }
            File.Move("Patcher.exe.tmp", Config.PatcherEXE);
            Console.WriteLine("Alter Patcher wurde gelöscht!");

            Console.WriteLine("\r\nOld patcher has been deleted!");
            try
            {
                Process.Start(Config.PatcherEXE);
            }
            catch
            {
                Console.WriteLine("Patcher cannot be executed for unknown reasons!");
                Environment.Exit(1);
            }
            Environment.Exit(0);
        }
    }
}
