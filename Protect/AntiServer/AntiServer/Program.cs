using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AntiServer
{
    class Program
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
                    System.IO.StreamReader sr = new System.IO.StreamReader(pathFile);
                    Console.WriteLine(sr.ReadToEnd());
                    Console.WriteLine("Проверка");
                    Thread.Sleep(1000);

                    string str = "Проверено"+" "+pathKatalog+" "+pathFile+" "+code;
                    
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
       
    }
}
