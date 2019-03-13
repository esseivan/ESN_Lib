using System;
using System.IO;

namespace EsseivaN.Tools
{
    public class MiscTools
    {
        public enum FileSize
        {
            B = 0,
            kB = 1,
            MB = 2,
            GB = 3,
            TB = 4,
        }

        public static string GetFileSize(string path)
        {
            FileSize unit = 0;
            double fileSize = new FileInfo(path).Length;
            while (fileSize >= 1024)
            {
                fileSize = Math.Round(fileSize / 1024, 2);
                unit++;
            }
            return $"{fileSize}{unit.ToString()}";
        }

    }
}
