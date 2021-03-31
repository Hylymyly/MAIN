using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AntiLibrary
{
    public static class PEFILE
    {
        public static void ZipUnPacked(string paths,string extractPath)
        {
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
}
