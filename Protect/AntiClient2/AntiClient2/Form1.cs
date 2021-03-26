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
using System.IO;

namespace AntiClient2
{
    public partial class Form1 : Form
    {
        static TcpListener tcpListener = new TcpListener(10);
        Thread thread;
        Thread thread2;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            progressBar1.Value = 0;
            thread2 = new Thread(new ThreadStart(ScunCatalog));
            thread2.Start();
            //ScunCatalog();
            button1.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button2.Enabled = true;
        }
        public void ScunCatalog()
        {
            string theString, theString1;
            tcpListener.Start();
            Socket socketForClient = tcpListener.AcceptSocket();
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

                textBox2.Text = "";
                while (i != 1)
                {
                    i++;
                    theString = streamReader.ReadLine();//Пути к проверенным файлам
                    theString1 = streamReader.ReadLine();//Пути к вирусам

                    var text = theString.Split(' ');
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;
                    int value = 0;
                    for (int j = 0; j < text.Length - 1; j++)
                    {
                        value = value + (100 / (text.Length - 1));
                        textBox10.Invoke((ThreadStart)delegate ()
                        {
                            textBox2.Text += (j + 1) + " " + text[j] + "\r\n";
                        });

                        SetProgressBarValue(progressBar1, value);
                        Thread.Sleep(100);
                    }
                    textBox2.Invoke((ThreadStart)delegate ()
                    {
                        textBox2.Text += theString1;
                    });
                    SetProgressBarValue(progressBar1, 100);
                }
                //Обнаружение пути вируса
                string viruspath = "Viruse's path";
                if (viruspath != "")
                {
                    DialogResult box = MessageBox.Show("Выберите действия с файлом " + viruspath + "?\r\n" + "Yes - поместить в карантин\r\nNo - удалить\r\nCancel - пропустить ", "Внимание вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    switch (box)
                    {
                        case DialogResult.Yes:
                            string answerBox = "Yes";
                            streamWriter.WriteLine(answerBox);
                            streamWriter.Flush();
                            break;
                        case DialogResult.No:
                            string answerBox1 = "No";
                            streamWriter.WriteLine(answerBox1);
                            streamWriter.Flush();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
                theString = "";
                theString1 = "";
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }

            socketForClient.Close();
        }
        public void ScunCatalog2()
        {
            string theString, theString1;
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


                textBox10.Text = "";
                while (i != 1)
                {
                    i++;
                    theString = streamReader.ReadLine();//Пути к проверенным файлам
                    theString1 = streamReader.ReadLine();//Пути к вирусам

                    var text = theString.Split(' ');
                    progressBar3.Minimum = 0;
                    progressBar3.Maximum = 100;
                    int value = 0;
                    for (int j = 0; j < text.Length - 1; j++)
                    {
                        value=value+(100/(text.Length-1));
                        textBox10.Invoke((ThreadStart)delegate ()
                        {
                            textBox10.Text += (j + 1) + " " + text[j] + "\r\n";
                        });

                        SetProgressBarValue(progressBar3, value);
                        Thread.Sleep(100);
                    }
                    textBox10.Invoke((ThreadStart)delegate ()
                    {
                        textBox10.Text += theString1;
                    });
                    SetProgressBarValue(progressBar3, 100);
                    
                }
                //Обнаружение пути вируса
                string viruspath = "Viruse's path";
                if (viruspath != "")
                {
                    DialogResult box = MessageBox.Show("Выберите действия с файлом " + viruspath + "?\r\n" + "Yes - поместить в карантин\r\nNo - удалить\r\nCancel - пропустить ", "Внимание вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    switch (box)
                    {
                        case DialogResult.Yes:
                            string answerBox = "Yes";
                            streamWriter.WriteLine(answerBox);
                            streamWriter.Flush();
                            break;
                        case DialogResult.No:
                            string answerBox1 = "No";
                            streamWriter.WriteLine(answerBox1);
                            streamWriter.Flush();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
                theString = "";
                theString1 = "";
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }
            socketForClient.Close();
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

                string changefile = streamReader.ReadLine();

                if (changefile != "")
                {
                    DialogResult box1 = MessageBox.Show("Файл " + changefile + " был изменен\r\nХотите провести проверку?", "Внимание вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    switch (box1)
                    {
                        case DialogResult.Yes:
                            string answerBox = "Yes";
                            streamWriter.WriteLine(answerBox);
                            streamWriter.Flush();
                            break;
                        case DialogResult.No:
                            string answerBox1 = "No";
                            streamWriter.WriteLine(answerBox1);
                            streamWriter.Flush();
                            break;
                    }
                }

                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }

            socketForClient.Close();
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
            thread = new Thread(new ThreadStart(ScunMonitoring));
            thread.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = textBox3.Text;
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"INSERT INTO BaseText" + "(path)" + "VALUES('" + str + "')";
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"update BaseText
                   set id = (select count(*)
                   from BaseText s2
                   where s2.id < BaseText.id or
                         s2.id = BaseText.id and s2.id <= BaseText.id
                  )";
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM BaseText";

            textBox5.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textBox5.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int j = Convert.ToInt32(textBox4.Text);

            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM BaseText WHERE id = " + j;
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"update BaseText
                   set id = (select count(*)
                   from BaseText s2
                   where s2.id < BaseText.id or
                         s2.id = BaseText.id and s2.id <= BaseText.id
                  )";
            command.ExecuteNonQuery();

            command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM BaseText";

            textBox5.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textBox5.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
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
                string answerVirus = streamReader.ReadLine();
                if (answerVirus == "Yes")
                {
                    DialogResult box = MessageBox.Show("Выберите действия с файлом " + theString + "?\r\n" + "Yes - поместить в карантин\r\nNo - удалить\r\nCancel - пропустить ", "Внимание вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    switch (box)
                    {
                        case DialogResult.Yes:
                            string answerBox = "Yes";
                            streamWriter.WriteLine(answerBox);
                            streamWriter.Flush();
                            break;
                        case DialogResult.No:
                            string answerBox1 = "No";
                            streamWriter.WriteLine(answerBox1);
                            streamWriter.Flush();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }

                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }

            socketForClient.Close();
            tcpListener.Stop();
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
            button9.Invoke((ThreadStart)delegate ()
            {
                button9.Enabled = false;
            });
            progressBar3.Invoke((ThreadStart)delegate ()
            {
                progressBar3.Value = 0;
            });
            label15.Text = "Время установлено, \r\nожидайте окончания\r\n сканирования";
            Thread thread3 = new Thread(new ThreadStart(ScunRaspisanie));
            thread3.Start();
            //ScunRaspisanie();
            

        }
        private void ScunRaspisanie()
        {
            int hours = Convert.ToInt32(textBox8.Text);
            int minutes = Convert.ToInt32(textBox9.Text);

            DateTime when = new DateTime(2021, 3, 26, hours, minutes, 00);

            DateTime now = DateTime.Now;
            if (when <= now)
            {
                ScunCatalog2();
            }
            else
            {
                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer(
                    o => { ScunCatalog2(); timer.Dispose(); },
                    null,
                    when - now,
                    TimeSpan.Zero);
            }
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
    }
}
