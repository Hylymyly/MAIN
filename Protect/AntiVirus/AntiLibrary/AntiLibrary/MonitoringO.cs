using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AntiLibrary
{
    public static class MonitoringO
    {
        public static string monitor = "";
        public static void MonitoringON(object obj)
        {
            var path = (string)obj;
            var watcher = new FileSystemWatcher(path);

            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Changed += OnChanged;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            monitor = $"{e.FullPath}";
        }
    }
}
