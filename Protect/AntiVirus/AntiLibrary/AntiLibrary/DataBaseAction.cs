using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace AntiLibrary
{
    public static class DataBaseAction
    {
        public static int hours;
        public static int minutes;
        public static void ConverttoBase(string paths2)
        {
            var text2 = paths2.Split(' ');
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            /*var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM FilesPE";
            command.ExecuteNonQuery();*/
            var command = sqlite.CreateCommand();
            foreach (string path in text2)
            {
                if (path != "")
                {
                    command.CommandText = @"INSERT INTO FilesPE" + "(paths)" + "VALUES('" + path + "')";
                    command.ExecuteNonQuery();
                }
            }
            command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM FilesPE WHERE id < (SELECT MAX(id) FROM FilesPE AS T1 WHERE FilesPE.paths = T1.paths);";
            command.ExecuteNonQuery();
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
        public static void DeleteTable()
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM FilesPE";
            command.ExecuteNonQuery();
            sqlite.Close();
        }
        public static void DeleteTableData()
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM DataAndTime";
            command.ExecuteNonQuery();
            sqlite.Close();
        }
        public static void DeleteTableVirus()
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM Viruses";
            command.ExecuteNonQuery();
            sqlite.Close();
        }

        public static void AddToVirusBase(string path)
        {
            var text2 = path.Split(' ');
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            /*var command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM FilesPE";
            command.ExecuteNonQuery();*/
            var command = sqlite.CreateCommand();
            foreach (string paths in text2)
            {
                if (path != "")
                {
                    command.CommandText = @"INSERT INTO Viruses" + "(path)" + "VALUES('" + paths + "')";
                    command.ExecuteNonQuery();
                }
            }
            command = sqlite.CreateCommand();
            command.CommandText = @"DELETE FROM Viruses WHERE id < (SELECT MAX(id) FROM Viruses AS T1 WHERE Viruses.path = T1.path);";
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"update Viruses
                   set id = (select count(*)
                   from Viruses s2
                   where s2.id < Viruses.id or
                         s2.id = Viruses.id and s2.id <= Viruses.id
                  )";
            command.ExecuteNonQuery();
            sqlite.Close();
        }

        public static void CreateData(int hour,int minutes)
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            DateTime data = DateTime.Now;
            var command = sqlite.CreateCommand();
            command.CommandText = @"INSERT INTO DataAndTime" + "(hour,minute)" + "VALUES('" + hour + "','" + minutes + "')";
            command.ExecuteNonQuery();
            sqlite.Close();
        }
        public static void InfoData()
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM DataAndTime";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    hours = Convert.ToInt32(reader.GetString(0));
                    minutes = Convert.ToInt32(reader.GetString(1));
                }
            }
            sqlite.Close();
        }
        public static void CreateReport(string typrofscan,int numberofviruses)
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            DateTime data = DateTime.Now;

            var command = sqlite.CreateCommand();
            command.CommandText = @"INSERT INTO Report" + "(time,typeofscan,countofviruses)" + "VALUES('" + data + "','" + typrofscan + "'," + numberofviruses + ")";
            command.ExecuteNonQuery();
            command = sqlite.CreateCommand();
            command.CommandText = @"update Report
                   set id = (select count(*)
                   from Report s2
                   where s2.id < Report.id or
                         s2.id = Report.id and s2.id <= Report.id
                  )";
            command.ExecuteNonQuery();
            sqlite.Close();
        }
        public static void InTableMonitor(TextBox textbox,TextBox textBox1)
        {
            string str = textbox.Text;
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

            textBox1.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textBox1.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        public static void OutTableMonitor(TextBox textbox, TextBox textbox1)
        {
            int j = Convert.ToInt32(textbox.Text);

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

            textbox1.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textbox1.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        public static void UpdateTableScanedFiles(TextBox textbox)
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"update FilesPE
                   set id = (select count(*)
                   from FilesPE s2
                   where s2.id < FilesPE.id or
                         s2.id = FilesPE.id and s2.id <= FilesPE.id
                  )";
            command.ExecuteNonQuery();

            command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM FilesPE";

            textbox.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textbox.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        public static void UpdateTableViruse(TextBox textbox)
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"update Viruses
                   set id = (select count(*)
                   from Viruses s2
                   where s2.id < Viruses.id or
                         s2.id = Viruses.id and s2.id <= Viruses.id
                  )";
            command.ExecuteNonQuery();

            command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Viruses";

            textbox.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textbox.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }
        public static void DeleteValueFromViruse(TextBox textbox,TextBox textbox2)
        {
            int j = Convert.ToInt32(textbox.Text);
            AntiLibrary.Scaner.DeleteFile(j);
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
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

            textbox2.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textbox2.Text += reader.GetString(0) + " " + reader.GetString(1) + "\r\n";
                }
            }
            sqlite.Close();
        }

        public static void UpdateReport(TextBox textbox)
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"update Report
                   set id = (select count(*)
                   from Report s2
                   where s2.id < Report.id or
                         s2.id = Report.id and s2.id <= Report.id
                  )";
            command.ExecuteNonQuery();

            command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Report";

            textbox.Text = "";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    textbox.Text += reader.GetString(0) + " Дата: " + reader.GetString(1) + " Тип: " + reader.GetString(2) + " Кол. вирусов: " + reader.GetString(3) + "\r\n";
                }
            }
            sqlite.Close();
        }
    }
}
