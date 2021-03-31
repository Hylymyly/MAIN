using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Threading;

namespace AntiService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.MainClass();
        }

        protected override void OnStop()
        {
        }
    }
    public static class Logger
    {
        //example
        static List<string> list = new List<string>();
        static List<string> list2 = new List<string>();
        static string resultofmonitoring = "";
        static StreamWriter streamWriter;
        static StreamReader streamReader;
        static string pathKatalog = "";
        static bool flagMon = false;
        static string pathforrasp = "";
        static bool raspflag = true;
        static string extactpath = Application.StartupPath + @"\Example2";
        //static string strangepath = @"C:\Users\yaros\Desktop\txt.txt";
        //static string createText = "";
        private static System.Timers.Timer aTimer = new System.Timers.Timer();
        public static void MainClass()
        {
            TcpClient socketForServer;
            try
            {
                var whileThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            try
                            {
                                //Console.WriteLine("Ожидание подключение...");
                                Thread.Sleep(2000);
                                socketForServer = new TcpClient("localHost", 10);
                                File.AppendAllText(@"C:\Users\yaros\Desktop\txt.txt", Application.StartupPath + @"\Example2");
                            }
                            catch
                            {
                                continue;
                            }
                            NetworkStream networkStream = socketForServer.GetStream();
                            streamWriter = new StreamWriter(networkStream);
                            streamReader = new StreamReader(networkStream);
                            //Console.WriteLine("\r\n*Клиент подключенный по порту №10*");
                            //Console.WriteLine("************************************");
                            //Console.WriteLine("************************************");
                            string code = "";
                            string strvod = streamReader.ReadLine();
                            if (strvod == "true")
                                flagMon = true;
                            else if (strvod == "true1")
                            {
                                aTimer.Stop();
                            }
                            else if (strvod == "truee")
                            {
                                raspflag = false;
                            }
                            else
                            {
                                var text = strvod.Split(' ');
                                pathKatalog = text[0];
                                code = text[1];
                            }
                            if (code == "000")
                            {
                                string paths = SwitchedC(code);
                                AntiLibrary.DataBaseAction.ConverttoBase(paths);
                                paths = "";
                                DeleteZip();
                                networkStream.Close();
                            }
                            else if (code == "004")
                            {
                                SwitchedC(code);
                                Thread threadMon = new Thread(MonitoringThread);
                                threadMon.Start();
                                networkStream.Close();
                            }
                            else if (code == "002")
                            {
                                string resScun = StartScanFile(pathKatalog);
                                AntiLibrary.DataBaseAction.ConverttoBase(resScun);
                                resScun = "";
                                DeleteZip();
                                networkStream.Close();
                            }
                            else if (code == "003")
                            {
                                pathforrasp = pathKatalog;
                                aTimer.Interval = 60000;
                                aTimer.Elapsed += OnTimedEvent;
                                aTimer.AutoReset = true;
                                aTimer.Enabled = true;
                                networkStream.Close();
                            }
                            else if (code == "005")
                            {
                                string flagmonitor = streamReader.ReadLine();
                                if (flagmonitor != "")
                                    flagMon = true;
                            }

                            Thread.Sleep(500);
                            //Console.WriteLine("Соединение разорвано\r\n");
                        }
                        catch
                        {
                            Thread.Sleep(100);
                            //Console.WriteLine("Сканирование завершено");
                        }
                    }
                });
                whileThread.Start();
            }
            catch (Exception e)
            {
            }
        }

        static string SwitchedC(string str)
        {
            string result = "";
            switch (str)
            {
                case "000":
                    result = StartScan();
                    break;
                case "001":
                    result = StartScan2();
                    break;
                case "004":
                    SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
                    sqlite.Open();
                    var command = sqlite.CreateCommand();
                    command.CommandText = @"SELECT * FROM BaseText";
                    using (var reader = command.ExecuteReader())
                    {
                        for (int i = 0; reader.Read(); i++)
                        {
                            list.Add(reader.GetString(1));
                        }
                    }
                    break;
            }
            return result;
        }
        static string FilesPE()
        {
            string res = "";

            string[] stri1 = Directory.GetFiles(pathKatalog, "*.*", SearchOption.AllDirectories);
            foreach (string paths in stri1)
            {
                if (paths.Substring(paths.LastIndexOf(".") + 1) == "zip")
                {
                    AntiLibrary.PEFILE.ZipUnPacked(paths, extactpath);
                    list2.Add(paths);
                }
                if (AntiLibrary.PEFILE.IsPEFile(paths) == true)
                {
                    res += paths + " ";
                }
            }
            string[] stri = Directory.GetFiles(Application.StartupPath + @"\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths1 in stri)
            {
                if (paths1.Substring(paths1.LastIndexOf(".") + 1) == "zip")
                {
                    AntiLibrary.PEFILE.ZipUnPacked(paths1, extactpath);
                    list2.Add(paths1);
                }
            }
            string[] stri2 = Directory.GetFiles(Application.StartupPath + @"\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths2 in stri2)
            {
                if (AntiLibrary.PEFILE.IsPEFile(paths2) == true)
                    res += paths2 + " ";
            }
            return res;
        }
        static string FilesPE2()
        {
            string res = "";

            string[] stri1 = Directory.GetFiles(pathforrasp, "*.*", SearchOption.AllDirectories);
            foreach (string paths in stri1)
            {
                Console.WriteLine(paths);
                if (paths.Substring(paths.LastIndexOf(".") + 1) == "zip")
                {
                    AntiLibrary.PEFILE.ZipUnPacked(paths, extactpath);
                    list2.Add(paths);
                }
                if (AntiLibrary.PEFILE.IsPEFile(paths) == true)
                    res += paths + " ";
            }
            string[] stri = Directory.GetFiles(Application.StartupPath + @"\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths1 in stri)
            {
                if (paths1.Substring(paths1.LastIndexOf(".") + 1) == "zip")
                {
                    AntiLibrary.PEFILE.ZipUnPacked(paths1, extactpath);
                    list2.Add(paths1);
                }
            }
            string[] stri2 = Directory.GetFiles(Application.StartupPath + @"\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths2 in stri2)
            {
                if (AntiLibrary.PEFILE.IsPEFile(paths2) == true)
                    res += paths2 + " ";
            }
            return res;
        }
        static void DeleteZip()
        {
            string[] stri2 = Directory.GetFiles(Application.StartupPath + @"\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths2 in stri2)
            {
                System.IO.File.Delete(paths2);
            }
        }
        static void MonitoringThread()
        {
            flagMon = false;
            string type = "Мониторинг";
            int j = 1;
            int i = 0;
            while (flagMon == false)
            {
                do
                {
                    for (int k = 0; k < list.Count; k++)
                    {
                        AntiLibrary.MonitoringO.MonitoringON(list[k]);
                        resultofmonitoring = AntiLibrary.MonitoringO.monitor;
                        //Console.WriteLine(resultofmonitoring);
                        if (resultofmonitoring != "")
                        {
                            AntiLibrary.DataBaseAction.ConverttoBase(resultofmonitoring);
                            if (AntiLibrary.Scaner.ScanerFile(AntiLibrary.Scaner.GetFileCode(resultofmonitoring, ref i)) == true)
                            {
                                AntiLibrary.DataBaseAction.AddToVirusBase(resultofmonitoring);
                                bool quar = AntiLibrary.Scaner.MoveToQuarantine(resultofmonitoring, false);
                                //if (quar == true)
                                //    Console.WriteLine("File moved to quarantine");
                                //else
                                //    Console.WriteLine("Error");
                            }
                        }
                    }
                } while (resultofmonitoring == "");
            }
            AntiLibrary.DataBaseAction.CreateReport(type, j);
        }
        static string StartScan()
        {
            raspflag = true;
            string type = "Сканирование папки/по расписанию";
            int j = 0;
            int i = 0;
            string resultofPE = FilesPE();//Все пути PE файлов
            var textofPE = resultofPE.Split(' ');
            foreach (string str in textofPE)
            {
                if (str != "")
                {
                    if (AntiLibrary.Scaner.ScanerFile(AntiLibrary.Scaner.GetFileCode(str, ref i)) == true)
                    {
                        j++;
                        AntiLibrary.DataBaseAction.AddToVirusBase(str);
                        bool quar = AntiLibrary.Scaner.MoveToQuarantine(str, false);
                    }
                }
                if (!raspflag)
                    break;
            }
            AntiLibrary.DataBaseAction.CreateReport(type, j);
            return resultofPE;
        }
        static string StartScanFile(string path)
        {
            string type = "Сканирование файла";
            int j = 0;
            int i = 0;
            if (AntiLibrary.PEFILE.IsPEFile(path) == true)
            {
                if (AntiLibrary.Scaner.ScanerFile(AntiLibrary.Scaner.GetFileCode(path, ref i)) == true)
                {
                    //Console.WriteLine("Virus founded");
                    j++;
                    AntiLibrary.DataBaseAction.AddToVirusBase(path);
                    bool quar = AntiLibrary.Scaner.MoveToQuarantine(path, false);
                    AntiLibrary.DataBaseAction.CreateReport(type, j);
                    return path;
                }
                else
                {
                    AntiLibrary.DataBaseAction.CreateReport(type, j);
                    return path; 
                }
            }
            AntiLibrary.DataBaseAction.CreateReport(type, j);
            return "";
        }
        static string StartScan2()
        {
            string type = "Сканирование папки/по расписанию";
            int j = 0;
            int i = 0;
            string resultofPE = FilesPE2();//Все пути PE файлов
            //Console.WriteLine("All files: " + resultofPE);
            var textofPE = resultofPE.Split(' ');
            foreach (string str in textofPE)
            {
                if (str != "")
                {
                    if (AntiLibrary.Scaner.ScanerFile(AntiLibrary.Scaner.GetFileCode(str, ref i)) == true)
                    {
                        //Console.WriteLine("Virus founded: " + str);
                        j++;
                        AntiLibrary.DataBaseAction.AddToVirusBase(str);
                        bool quar = AntiLibrary.Scaner.MoveToQuarantine(str, false);
                    }
                }
            }
            AntiLibrary.DataBaseAction.CreateReport(type, j);
            return resultofPE;
        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            AntiLibrary.DataBaseAction.InfoData();
            int hour1 = AntiLibrary.DataBaseAction.hours;
            int minute1 = AntiLibrary.DataBaseAction.minutes;
            int hour2 = Convert.ToInt32(DateTime.Now.ToString("HH"));
            int minute2 = Convert.ToInt32(DateTime.Now.ToString("mm"));
            //Console.WriteLine(hour1 + " " + minute1);
            //Console.WriteLine(hour2 + " " + minute2);

            if (hour1 == hour2 && minute1 == minute2)
            {
                pathforrasp = SwitchedC("001");
                AntiLibrary.DataBaseAction.ConverttoBase(pathforrasp);
                pathforrasp = "";
                DeleteZip();
                AntiLibrary.DataBaseAction.DeleteTableData();
            }
        }
    }
}
