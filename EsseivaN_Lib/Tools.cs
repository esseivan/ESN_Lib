using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN.Tools
{
    public class Tools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        public static void RunAsAdmin(Process process)
        {   //Vista or higher check
            if (Environment.OSVersion.Version.Major >= 6)
            {
                if (process.StartInfo.Verb == string.Empty)
                    process.StartInfo.Verb = "runas";
                else
                    process.StartInfo.Verb += "runas";

                process.Start();
            }
            else
                throw new SystemException("OS version not supported");
        }

        /// <summary>
        /// Close the app and run the selected one
        /// </summary>
        public static void RunAsAdmin(IAdminForm app, string arguments)
        {   //Vista or higher check
            if (Environment.OSVersion.Version.Major >= 6)
            {

                // Get path
                string path = app.GetAppPath();
                Console.WriteLine(path);

                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(path, arguments);
                p.StartInfo = psi;

                if (p.StartInfo.Verb == string.Empty)
                    p.StartInfo.Verb = "runas";
                else
                    p.StartInfo.Verb += "runas";

                p.Start();
                Application.Exit();
            }
            else
                throw new SystemException("OS version not supported");
        }

        /// <summary>
        /// Convert decimal format to engineer format
        /// </summary>
        public static string DecimalToEngineer(double Value)
        {
            return DecimalToEngineer(Value, 3);
        }

        /// <summary>
        /// Convert decimal format to engineer format
        /// </summary>
        public static string DecimalToEngineer(double Value, int Digits)
        {
            if (double.IsInfinity(Value) || double.IsNaN(Value))
                return null;

            bool isNeg = false;

            if (Value < 0)
            {
                isNeg = true;
                Value = -Value;
            }

            if (Value == 0)
            {
                return "0";
            }

            string Output = "";
            short PowS = 0;

            while (Value < 1)
            {
                Value *= 1000;
                PowS--;
            }

            while (Value >= 1000)
            {
                Value /= 1000;
                PowS++;
            }

            Value = Math.Round(Value, Digits);

            switch (PowS)
            {
                case -8:
                    Output = $"{Value}y";
                    break;
                case -7:
                    Output = $"{Value}z";
                    break;
                case -6:
                    Output = $"{Value}a";
                    break;
                case -5:
                    Output = $"{Value}f";
                    break;
                case -4:
                    Output = $"{Value}p";
                    break;
                case -3:
                    Output = $"{Value}n";
                    break;
                case -2:
                    Output = $"{Value}μ";
                    break;
                case -1:
                    Output = $"{Value}m";
                    break;
                case 0:
                    Output = $"{Value}";
                    break;
                case 1:
                    Output = $"{Value}k";
                    break;
                case 2:
                    Output = $"{Value}M";
                    break;
                case 3:
                    Output = $"{Value}G";
                    break;
                case 4:
                    Output = $"{Value}T";
                    break;
                case 5:
                    Output = $"{Value}P";
                    break;
                case 6:
                    Output = $"{Value}E";
                    break;
                case 7:
                    Output = $"{Value}Z";
                    break;
                case 8:
                    Output = $"{Value}Y";
                    break;
                default:
                    Output = $"{Value} * 10^{PowS}";
                    break;
            }

            if (isNeg)
                Output = "-" + Output;

            return Output;
        }

        /// <summary>
        /// Convert engineer format to decimal
        /// </summary>
        public static double EngineerToDecimal(string Text)
        {
            if (Text == string.Empty || Text == null)
            {
                //MessageBox.Show("Missing value", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return double.NaN;
            }

            short PowS = 0;

            char PowSString = Text.LastOrDefault();
            if (double.TryParse(Text, out double temp))
            {
                return temp;
            }

            if (!double.TryParse(Text.Remove(Text.Length - 1, 1), out double Value))
            {
                //MessageBox.Show("Invalid resistor value format\n" + Text.Remove(Text.Length - 1, 1), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return double.NaN;
            }

            while (Value < 1)
            {
                Value *= 1000;
                PowS--;
            }

            while (Value >= 1000)
            {
                Value /= 1000;
                PowS++;
            }

            switch (PowSString)
            {
                case 'm':
                    PowS -= 1;
                    break;
                case 'k':
                    PowS += 1;
                    break;
                case 'M':
                    PowS += 2;
                    break;
                case 'G':
                    PowS += 3;
                    break;
                default:
                    {
                        //MessageBox.Show("Invalid resistor value format.\nAccepted prefixes are 'm', 'k', 'M', 'G'. Use as following :\n24.56k", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return double.NaN;
                    }
            }

            Value *= Math.Pow(10, 3 * PowS);

            return Value;
        }

        /// <summary>
        /// Get error percent
        /// </summary>
        public static double GetErrorPercent(double Value, double TrueValue)
        {
            if (TrueValue == 0 || double.IsNaN(TrueValue))
                return double.NaN;

            return (100 * (Value - TrueValue) / TrueValue);
        }

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

    public interface IAdminForm
    {
        string GetAppPath();
    }
}
