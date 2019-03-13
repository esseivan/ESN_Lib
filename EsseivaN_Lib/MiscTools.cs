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

        public static async Task<bool> DownloadFile(string webPath)
        {
            return await DownloadFile(webPath,
                Path.GetTempPath(),
                Path.GetFileName(webPath),
                Path.GetExtension(webPath),
                false);
        }

        public static async Task<bool> DownloadFile(string webPath, bool RunAfterDownload)
        {
            return await DownloadFile(webPath,
                Path.GetTempPath(),
                Path.GetFileName(webPath),
                Path.GetExtension(webPath),
                RunAfterDownload);
        }

        public static async Task<bool> DownloadFile(string webPath, string fileName, bool RunAfterDownload)
        {
            return await DownloadFile(webPath,
                Path.GetTempPath(),
                fileName,
                Path.GetExtension(webPath),
                RunAfterDownload);
        }

        public static async Task<bool> DownloadFile(string webPath, string storePath, string fileName, bool RunAfterDownload)
        {
            return await DownloadFile(webPath,
                storePath,
                fileName,
                Path.GetExtension(webPath),
                RunAfterDownload);
        }

        public static async Task<bool> DownloadFile(string webPath, string storePath, string fileName, string extension, bool RunAfterDownload)
        {
            string filePath = Path.ChangeExtension(Path.Combine(storePath, Path.GetFileNameWithoutExtension(fileName)), extension);
            WebClient webClient = new WebClient();
            await webClient.DownloadFileTaskAsync(new Uri(webPath), filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Length != 0)
            {
                if (RunAfterDownload)
                {
                    var process = Process.Start(filePath);
                    await Task.Delay(300);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
