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
    static class Program
    {
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
                    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);
                    Console.WriteLine("\r\n*Клиент подключенный по порту №10*");
                    string strvod = streamReader.ReadLine();
                    var text = strvod.Split(' ');
                    string pathKatalog = text[0];
                    string pathFile = text[1];
                    string code = text[2];
                    string result;
                    System.IO.StreamReader sr = new System.IO.StreamReader(pathFile);
                    Console.WriteLine(sr.ReadToEnd());
                    Console.WriteLine("Проверка");
                    Thread.Sleep(1000);
                    
                    string str = "Проверено" + " " + pathKatalog + " " + pathFile + " " + SwitchedC(code); ;
                    
                    streamWriter.WriteLine(str);
                    streamWriter.Flush();
                    networkStream.Close();
                    Thread.Sleep(1000);
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
                    Console.WriteLine(result = StartScan());
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
            }
            return result;
        }
        static string StartScan()
        {
            string res = "";

            string[] stri1 = Directory.GetFiles(@"C:\Users\yaros\Desktop\Работы\Example", "*.*", SearchOption.AllDirectories);
            foreach (string paths in stri1)
            {
                if (paths.Substring(paths.LastIndexOf(".") + 1) == "zip")
                    PEFiles.ZipUnPacked(paths);
                res += paths + ": " + PEFiles.IsPEFile(paths) + "\r\n";
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
                res += paths2 + ": " + PEFiles.IsPEFile(paths2) + "\r\n";
                System.IO.File.Delete(paths2);
            }
            return res;
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
