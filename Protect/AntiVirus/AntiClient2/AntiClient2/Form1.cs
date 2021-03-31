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
using Microsoft.Data.Sqlite;
using AntiLibrary;
using System.IO;

namespace AntiClient2
{
    public partial class Form1 : Form
    {
        static TcpListener tcpListener = new TcpListener(10);
        Thread thread;
        Thread thread2;
        System.Threading.Timer timer = null;
        Thread thread3;
        static bool flagrasp;
        static List<string> list3 = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(Application.StartupPath);
            pictureBox1.Visible = true;
            pictureBox1.Image = Image.FromFile(Application.StartupPath+@"\xxx.gif");
            label3.Text = "Сканирование началось, резульnаты смотрите\r\n на вкладке - 'Сканированные файлы'";
            button1.Visible = true;
            button3.Enabled = true;
            //textBox1.Enabled = false;
            progressBar1.Value = 0;
            thread2 = new Thread(new ThreadStart(ScunCatalog));
            thread2.Start();
        }
        public void ScunCatalog()
        {
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            Console.WriteLine("****");
            if (socketForClient.Connected)
            {
                int i = 0;
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                StreamReader streamReader = new StreamReader(networkStream);
                string pathtokat = textBox1.Text;
                string str = pathtokat + " 000";//Передача кода функции и пути к каталогу
                streamWriter.WriteLine(str);
                streamWriter.Flush();
                while (i != 1)
                {
                    i++;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;
                    int value = 0;
                    for (int j = 0; j < 100; j++)
                    {
                        value++;
                        SetProgressBarValue(progressBar1, value);
                        Thread.Sleep(100);
                    }
                    SetProgressBarValue(progressBar1, 100);
                }

                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
            }
            tcpListener.Stop();
            pictureBox1.Invoke((ThreadStart)delegate ()
            {
                pictureBox1.Visible = false;
            });

        }
        public void ScunCatalog2()
        {
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                int i = 0;
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                StreamReader streamReader = new StreamReader(networkStream);
                string pathtokat = textBox7.Text;
                string str = pathtokat + " 003";//Передача кода функции и пути к каталогу
                streamWriter.WriteLine(str);
                streamWriter.Flush();

                while (i != 1)
                {
                    i++;
                    progressBar3.Minimum = 0;
                    progressBar3.Maximum = 100;
                    int value = 0;
                    for (int j = 0; j < 100; j++)
                    {
                        value++;
                        SetProgressBarValue(progressBar3, value);
                        Thread.Sleep(100);
                    }
                    SetProgressBarValue(progressBar3, 100);
                }
             
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
            }
            button9.Invoke((ThreadStart)delegate ()
            {
                button9.Enabled = true;
            });
            button10.Invoke((ThreadStart)delegate ()
            {
                button10.Enabled = false;
            });
            tcpListener.Stop();
            pictureBox3.Invoke((ThreadStart)delegate ()
            {
                pictureBox3.Visible = true;
                pictureBox3.Image = Image.FromFile(Application.StartupPath + @"\rrr.gif");
            });
            Thread.Sleep(2000);
            pictureBox3.Invoke((ThreadStart)delegate ()
            {
                pictureBox3.Visible = false;
            });
        }
        public void ScunMonitoring()
        {
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                StreamReader streamReader = new StreamReader(networkStream);

                string str = "C:\\Users\\yaros 004";//Передача кода функции и пути к каталогу
                streamWriter.WriteLine(str);
                streamWriter.Flush();

                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
            }
            tcpListener.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label6.Text = "Мониторинг начался. Для результатов просмотрите вкладку - 'Просканированные файлы'";
            thread = new Thread(new ThreadStart(ScunMonitoring));
            thread.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.InTableMonitor(textBox3, textBox5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.OutTableMonitor(textBox4, textBox5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //pictureBox2.Visible = true;
            //pictureBox2.Image = Image.FromFile(@"C:\Users\yaros\Desktop\rrr.gif");
            button7.Enabled = false;
            label16.Text = "Сканирование началось";
            progressBar2.Value = 0;
            progressBar2.Maximum = 10;
            progressBar2.Minimum = 0;
            progressBar2.Step = 1;
            string theString;
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                int i = 0;
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                StreamReader streamReader = new StreamReader(networkStream);

                theString = textBox6.Text;
                string str = theString + " 002";//Передача кода функции и пути к каталогу
                streamWriter.WriteLine(str);
                streamWriter.Flush();
                for (i = 0; i < 10; i++)
                {
                    Thread.Sleep(50);
                    progressBar2.PerformStep();
                }
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }
            socketForClient.Close();
            tcpListener.Stop();
            button7.Enabled = true;
            label16.Text = "Сканирование завершено. Результаты смотрите во вкладке \r\n                     'Сканированные файлы'";
            //pictureBox2.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                textBox7.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            flagrasp = true;
            button9.Invoke((ThreadStart)delegate ()
            {
                button9.Enabled = false;
            });
            button10.Invoke((ThreadStart)delegate ()
            {
                button10.Enabled = true;
            });
            progressBar3.Invoke((ThreadStart)delegate ()
            {
                progressBar3.Value = 0;
            });
            int hours = Convert.ToInt32(textBox8.Text);
            int minutes = Convert.ToInt32(textBox9.Text);
            AntiLibrary.DataBaseAction.CreateData(hours, minutes);
            label15.Text = "Время установливается, \r\nожидайте время\r\n сканирования\r\n  Результаты сканирования\r\n  во вкладке\r\n   'Сканированные файлы'";
            thread3 = new Thread(new ThreadStart(ScunCatalog2));
            thread3.Start();
            //ScunRaspisanie();


        }
        private void SetProgressBarValue(ProgressBar prb, int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action<ProgressBar, int>)SetProgressBarValue, prb, value);
            }
            else
            {
                prb.Value = value;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.UpdateTableScanedFiles(textBox11);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            label6.Text = " ";
            label6.Text = "Мониторинг завершен";
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                string flag = "true";
                streamWriter.WriteLine(flag);
                streamWriter.Flush();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
            }
            tcpListener.Stop();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label5.Text = " ";
            label5.Text = "Сканирование отменено";
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                string flag = "true1";
                streamWriter.WriteLine(flag);
                streamWriter.Flush();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
            }
            tcpListener.Stop();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.DeleteTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = " ";
            label3.Text = "Сканирование завершено";
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                string flag = "truee";
                streamWriter.WriteLine(flag);
                streamWriter.Flush();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
            }
            tcpListener.Stop();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.UpdateTableViruse(textBox2);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.DeleteTableVirus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.DeleteValueFromViruse(textBox10, textBox2);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            AntiLibrary.DataBaseAction.UpdateReport(textBox12);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            list3.Clear();
            int j = Convert.ToInt32(textBox10.Text);
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Viruses where id = " + j;
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    list3.Add(reader.GetString(1));
                }
            }
            string path = list3[0];
            AntiLibrary.Scaner.MoveToQuarantine(path, true);
            command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM Viruses WHERE id = " + j;
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"update Viruses
                   set id = (select count(*)
                   from Viruses s2
                   where s2.id < Viruses.id or
                         s2.id = Viruses.id and s2.id <= Viruses.id
                  )";
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Viruses";
            textBox2.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textBox2.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Jcnfyjdkty");
        }
    }
}
