using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace FileRenamer
{
    static class AppSettings
    {
        public static string Read()
        {
            Properties.Settings appSettings = Properties.Settings.Default;
            return appSettings.LastDirectory;
            
        }

        public static void Save(string directoryPath)
        {
            Properties.Settings appSettings = Properties.Settings.Default;
            appSettings.LastDirectory = directoryPath;
        }

        
    }
}
