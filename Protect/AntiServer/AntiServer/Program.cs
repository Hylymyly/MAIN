using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Data.Sqlite;


namespace AntiServer
{
    static class PEFiles
    {
        public static void ZipUnPacked(string paths)
        {
            string extractPath = @"C:\Users\yaros\Desktop\Работы\Example2";
            using (ZipArchive archive = ZipFile.OpenRead(paths))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Gets the full path to ensure that relative segments are removed.
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                    // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                    // are case-insensitive.
                    if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                    {
                        entry.ExtractToFile(destinationPath);
                    }
                }
            }
        }

        static PEFile ToFileHeader(byte[] ar)
        {
            GCHandle handle = GCHandle.Alloc(ar, GCHandleType.Pinned);
            PEFile pfh = (PEFile)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PEFile));
            handle.Free();
            return pfh;
        }

        public static bool IsPEFile(string path)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(path), Encoding.ASCII))
            {
                /// Читаем первые 64 байта. Именно там находится заголовок.
                byte[] ar = br.ReadBytes(64);
                /// Заносим эти байты в структуру
                PEFile sig = ToFileHeader(ar);
                /// Переходим по смещению для чтения первых двух символов PE-заголовка
                br.BaseStream.Seek(sig.PEHeaderAddress, SeekOrigin.Begin);
                /// Читаем два символа и переводим их в строку
                string pe = new string(br.ReadChars(2));
                /// Возвращаем результат выполнения
                return sig.DosHeader == "MZ" && pe == "PE";
            }
        }
        [StructLayout(LayoutKind.Explicit)]
        struct PEFile
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            [FieldOffset(0)]
            public string DosHeader;
            [MarshalAs(UnmanagedType.I8)]
            [FieldOffset(0x3C)]
            public long PEHeaderAddress;
        }
    }
    static class MonitoringO
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
    static class Program
    {
        static List<string> list = new List<string>();
        static string resultofmonitoring = "";
        static StreamWriter streamWriter;
        static StreamReader streamReader;
        static string pathKatalog = "";
        #region dll
        [DllImport("kernel32.dll")]
        static extern bool ReleaseMutex(IntPtr hMutex);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
        [DllImport("kernel32.dll")]
        static extern bool ResetEvent(IntPtr hEvent);
        [DllImport("kernel32.dll")]
        static extern bool SetEvent(IntPtr hEvent);
        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenEvent(uint dwDesiredAccess, bool bInheritHandle, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll")]
        static public extern bool PulseEvent(IntPtr hEvent);
        #endregion
        static void Main(string[] args)
        {
            TcpClient socketForServer;
            while (true)
            {
                try
                {
                    try
                    {
                        Console.WriteLine("Ожидание подключение...");
                        Thread.Sleep(2000);

                        socketForServer = new TcpClient("localHost", 10);

                    }
                    catch
                    {
                        continue;
                    }

                    NetworkStream networkStream = socketForServer.GetStream();
                    streamWriter = new StreamWriter(networkStream);
                    streamReader = new StreamReader(networkStream);
                    Console.WriteLine("\r\n*Клиент подключенный по порту №10*");
                    Console.WriteLine("************************************");

                    Console.WriteLine("************************************");

                    string strvod = streamReader.ReadLine();
                    var text = strvod.Split(' ');
                    pathKatalog = text[0];
                    string code = text[1];

                    if (code == "000")
                    {
                        //Результат сканирования
                        string paths = SwitchedC(code);
                        string paths2 = paths;
                        ConverttoBase(paths2);
                        streamWriter.WriteLine(paths);
                        streamWriter.Flush();
                        paths = "";
                        //Стринга с вирусами
                        streamWriter.WriteLine("String of viruses");
                        streamWriter.Flush();

                        //Карантин
                        string answerBox = streamReader.ReadLine();
                        if (answerBox == "Yes")
                            Console.WriteLine("Перемещен в карантин");
                        else if (answerBox == "No")
                            Console.WriteLine("Удален");
                        /*Метод для удаления опеределенного файла*/
                        networkStream.Close();
                    }
                    else if (code == "004")
                    {
                        SwitchedC(code);
                        MonitoringThread();

                        string answerBox = streamReader.ReadLine();
                        if (answerBox == "Yes")
                            Console.WriteLine("Провести проверку");
                        else if (answerBox == "No")
                            Console.WriteLine("Не проводить проверку");
                        Console.WriteLine(resultofmonitoring);
                        networkStream.Close();
                    }
                    else if (code == "002")
                    {
                        string resScun = StartScanFile(pathKatalog);

                        streamWriter.WriteLine(resScun);
                        streamWriter.Flush();
                        string answerBox = streamReader.ReadLine();
                        if (answerBox == "Yes")
                            Console.WriteLine("Перемещен в карантин");
                        else if (answerBox == "No")
                            Console.WriteLine("Удален");
                        networkStream.Close();
                    }
                    else if (code == "003")
                    {
                        //Результат сканирования
                        string paths = SwitchedC("000");
                        string paths2 = paths;
                        ConverttoBase(paths2);
                        streamWriter.WriteLine(paths);
                        streamWriter.Flush();
                        paths = "";
                        //Стринга с вирусами
                        streamWriter.WriteLine("String of viruses");
                        streamWriter.Flush();

                        //Карантин
                        string answerBox = streamReader.ReadLine();
                        if (answerBox == "Yes")
                            Console.WriteLine("Перемещен в карантин");
                        else if (answerBox == "No")
                            Console.WriteLine("Удален");
                        /*Метод для удаления опеределенного файла*/
                        networkStream.Close();
                    }

                    Thread.Sleep(500);
                    Console.WriteLine("Соединение разорвано\r\n");

                }
                catch
                {
                    Console.WriteLine("Ошибка считывания");
                }
            }
        }

        static string SwitchedC(string str)
        {
            string result = "";
            switch (str)
            {
                case "000":
                    result = StartScan();
                    Console.WriteLine(result);
                    break;
                case "001":
                    result = StopScan();
                    break;
                case "002":
                    result = MoveToQ();
                    break;
                case "003":
                    result = RemoveFile();
                    break;
                case "004":
                    string paths = "";
                    //Thread thread1 = new Thread(MonitoringO.MonitoringON);
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
                    //Проверка файла на вирус
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
                Console.WriteLine(paths);
                if (paths.Substring(paths.LastIndexOf(".") + 1) == "zip")
                    PEFiles.ZipUnPacked(paths);
                if (PEFiles.IsPEFile(paths) == true)
                    res += paths + " ";
            }
            string[] stri = Directory.GetFiles(@"C:\Users\yaros\Desktop\Работы\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths1 in stri)
            {
                if (paths1.Substring(paths1.LastIndexOf(".") + 1) == "zip")
                {
                    PEFiles.ZipUnPacked(paths1);
                }
            }
            string[] stri2 = Directory.GetFiles(@"C:\Users\yaros\Desktop\Работы\Example2", "*.*", SearchOption.AllDirectories);
            foreach (string paths2 in stri2)
            {
                if (PEFiles.IsPEFile(paths2) == true)
                    res += paths2 + " ";
                System.IO.File.Delete(paths2);
            }
            return res;
        }
        static void MonitoringThread()
        {
            do
            {
                for (int k = 0; k < list.Count; k++)
                {
                    MonitoringO.MonitoringON(list[k]);
                    resultofmonitoring = MonitoringO.monitor;
                    //Console.WriteLine(resultofmonitoring);
                    if (resultofmonitoring != "")
                    {
                        streamWriter.WriteLine(resultofmonitoring);
                        streamWriter.Flush();
                    }
                }
            } while (resultofmonitoring == "");
        }
        static string StartScan()
        {
            return FilesPE();//Все пути PE файлов
            //Метод для обнаружения вирусов
            //Обнаружил вирус, записал путь, вывел на клиенте messageBox c путем "Yes","Nо", если да - в карантин, нет - пропустить
        }
        static string StartScanFile(string path)
        {
            if (PEFiles.IsPEFile(path) == true)
                Console.WriteLine("Сканирование файла");
            //Сканирование файла, если найден вирус - отправить данные об этом на клиент
            return "Yes";

        }
        static void ConverttoBase(string paths2)
        {
            var text2 = paths2.Split(' ');
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM FilesPE";
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            foreach (string path in text2)
            {
                if (path != "")
                {
                    command.CommandText = @"INSERT INTO FilesPE" + "(paths)" + "VALUES('" + path + "')";
                    command.ExecuteNonQuery();
                }
            }
            command = sqlite.CreateCommand();
            command.CommandText = @"update FilesPE
                   set id = (select count(*)
                   from FilesPE s2
                   where s2.id < FilesPE.id or
                         s2.id = FilesPE.id and s2.id <= FilesPE.id
                  )";
            command.ExecuteNonQuery();
            sqlite.Close();
        }
        static string StopScan()
        {
            string res = "Сканиbрование завершено";
            return res;
        }
        static string MoveToQ()
        {
            string res = "Карантин";
            return res;
        }
        static string RemoveFile()
        {
            string res = "Удален";
            return res;
        }
    }
}
