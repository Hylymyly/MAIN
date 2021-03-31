using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Data.Sqlite;
using System.IO;
using System.IO.Compression;

namespace AntiLibrary
{
    public static class Scaner
    {
        static List<string> list1 = new List<string>();
        static List<string> list2 = new List<string>();
        static List<string> list3 = new List<string>();
        public static List<string> list4 = new List<string>();
        public static bool ScanerFile(byte[] arrayofbyte)
        {
            SignatureFind();
            for (int i = 0; i < arrayofbyte.Length-10; i++)
            {
                string temp = ByteString(arrayofbyte, i, 10);
                foreach(string str in list2)
                {
                    temp = ByteString(arrayofbyte, i, (str.Length)/2);
                    if (temp.Equals(str)) return true;
                }
            }
            return false;
        }
        public static void ZipArchive(List<string> list)
        {
            ZipFind();
            foreach (string str in list)
            {
                using (ZipArchive archive = ZipFile.OpenRead(str))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        foreach (string str1 in list3)
                        {
                            if (entry.FullName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase).Equals(str1) ||
                                entry.FullName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase).Equals(str1))
                            {
                                list4.Add(str);
                            }
                        }
                    }
                }
            }
        }
        static void ZipFind()
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Viruse";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    list3.Add(reader.GetString(1));
                }
            }
            sqlite.Close();
            list3.Clear();
        }
        static void SignatureFind()
        {
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Signature";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    list2.Add(reader.GetString(1));
                }
            }
            sqlite.Close();
        }
        static string ByteString(byte[] arrayofbyte, int index, int length)
        {
            string result = BitConverter.ToString(arrayofbyte, index, length).Replace("-", "");
            return result;
        }

        public static void DeleteFile(int id)
        {
            list1.Clear();
            SqliteConnection sqlite = new SqliteConnection("Data Source=C:\\BaseData\\BasDate.db");
            sqlite.Open();
            var command = sqlite.CreateCommand();
            command.CommandText = @"SELECT * FROM Viruses where id = " + id;
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; reader.Read(); i++)
                {
                    list1.Add(reader.GetString(1));
                }
            }
            sqlite.Close();
            string path = list1[0];
            File.Delete(path);
        }
        public static byte[] GetFileCode(string path, ref int offset)
        {
            Stream stream = File.OpenRead(path);
            bool txtIsFound = false;
            while (!txtIsFound)
            {
                switch ((char)stream.ReadByte())
                {
                    case '.': //.text
                        if ((char)stream.ReadByte() == 't' &&
                            (char)stream.ReadByte() == 'e' &&
                            (char)stream.ReadByte() == 'x' &&
                            (char)stream.ReadByte() == 't') txtIsFound = true;
                        break;
                    default:
                        break;
                }
                if (stream.Position == stream.Length - 1) break;
            }
            stream.Position += 10;

            byte[] array = new byte[4];
            stream.Read(array, 0, 4);
            Array.Reverse(array, 0, array.Length);
            int rawDataSize = BitConverter.ToInt32(array, 0);
            //Console.WriteLine("rawDataSize = " + rawDataSize);

            stream.Read(array, 0, 4);
            Array.Reverse(array, 0, array.Length);
            int rawDataPosition = BitConverter.ToInt32(array, 0);
            //Console.WriteLine("rawDataPosition = " + rawDataPosition);

            offset = rawDataPosition;
            array = new byte[rawDataSize];
            stream.Position = rawDataPosition;
            stream.Read(array, 0, rawDataSize - 1);
            //Console.WriteLine("rawData: " + BitConverter.ToString(array));

            stream.Close();

            return array;
        }
        public static bool MoveToQuarantine(string path, bool Back)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Open))
                {
                    stream.Position = 0;
                    if (Back == true) stream.WriteByte(0x4D);
                    else stream.WriteByte(0x66);
                    stream.Close();
                }
            }
            catch (Exception) { return false; }
            return true;
        }
    }
}
