using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

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

        private MiscTools() { }

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

        public static async Task<bool> DownloadFile(string webPath, string storePath, string fileName)
        {
            WebClient webClient = new WebClient();
            await webClient.DownloadFileTaskAsync(new Uri(webPath), storePath + fileName + ".msi");
            FileInfo info = new FileInfo(storePath + fileName + ".msi");

            return (info.Length != 0);
        }

        public static async Task<bool> DownloadAndRunFile(string webPath, string storePath, string fileName)
        {
            WebClient webClient = new WebClient();
            await webClient.DownloadFileTaskAsync(new Uri(webPath), storePath + fileName + ".msi");
            FileInfo info = new FileInfo(storePath + fileName + ".msi");
            if (info.Length != 0)
            {
                var process = Process.Start(storePath + fileName + ".msi");
                await Task.Delay(300);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
