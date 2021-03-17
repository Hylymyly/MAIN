using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiCLient
{
    public partial class Form1 : Form
    {
        #region dll
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
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        public const int SM_MOUSEPRESENT = 19;
        public const int SMCMOUSEBUTTONS = 43;
        public const int SM_MOUSEWHEELPRESENT = 75;
        #endregion
        static TcpListener tcpListener = new TcpListener(10);
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Listeners();
            tcpListener.Stop();
        }

        private void Listeners()
        {
            string txt, pathKatalog, pathFile, code, res;
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                int i = 0;
                NetworkStream networkStream = new NetworkStream(socketForClient);
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
                System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);
                string str = "C:\\Users\\yaros C:\\Users\\yaros\\Desktop\\txt.txt 000";
                streamWriter.WriteLine(str);
                streamWriter.Flush();
                Thread.Sleep(1000);
                while (i != 1)
                    {
                    i++;
                    string theString = streamReader.ReadLine();
                    //string theString = "1 1 1 1";
                    var text = theString.Split(' ');
                    txt = text[0];
                    pathKatalog = text[1];
                    pathFile = text[2];
                    code = text[3];                   
                    label6.Text = "Код функции: " + "001" + " выполнено: " + code;
                    label3.Text = "Сообщение клиента: " + socketForClient.RemoteEndPoint + " " + txt;
                    label4.Text = "Путь каталога: " + pathKatalog;
                    label5.Text = "Путь файла: " + pathFile;
                    }
                
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }
            socketForClient.Close();
        }
    }
}
