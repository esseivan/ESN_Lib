using System;
using System.Diagnostics;
using System.IO;

namespace EsseivaN.Tools
{
    public class PostBuild
    {
        public static void Main(string[] args)
        {
            string filePath,
                productName,
                installerName,
                folderPath,
                version;

            DateTime date;

            filePath = productName = installerName = folderPath = string.Empty;

            for (int i = 0; i < args.Length;)
            {
                switch (args[i++])
                {
                    case "/f":
                    case "/F":
                    case "-F":
                    case "-f":  // Path of file to determine product version and date
                        filePath = args[i++].Replace("\"", "");
                        break;
                    case "/pn":
                    case "/PN":
                    case "-PN":
                    case "-pn": // Product name
                        productName = args[i++].Replace("\"", "");
                        break;
                    case "/in":
                    case "/IN":
                    case "-IN":
                    case "-in": // Installer name
                        installerName = args[i++].Replace("\"", "");
                        break;
                    case "/fo":
                    case "/FO":
                    case "-FO":
                    case "-fo": // Full folder path to get the output
                        folderPath = args[i++].Replace("\"", "");
                        break;
                    default:
                        Console.WriteLine("Invalid arguments");
                        Console.WriteLine("Example of use :");
                        Console.WriteLine("<path> -e <exe path> -pn <product name> -in <installer name> -fo <folder path>");
                        break;
                }
            }

            // Check that all is set
            if (filePath == string.Empty || productName == string.Empty || installerName == string.Empty || folderPath == string.Empty)
            {
                return;
            }

            // Read the config file
            string configPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.cfg";
            string data = File.ReadAllText(configPath);
            data = data.Replace("\r", "");
            string[] datas = data.Split('\n');
            // Base destination path
            string destPath = datas[0].Replace("\"", "");
            // templates file
            string templatesPath = datas[1].Replace("\"", "");
            // post run cmd
            string postRun = datas[2].Replace("\"", "");

            // Get the creation time
            FileInfo fileInfo = new FileInfo(filePath);
            date = fileInfo.LastWriteTime;
            Console.WriteLine("Creation date : " + date);

            // Get the version
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
            version = fileVersionInfo.ProductVersion;
            Console.WriteLine("Version : " + version);

            // Create a zipped file of the output
            string zip_dest = $@"{destPath}{productName}\{productName}.zip";
            string zip_source = folderPath + "\\";
            Console.WriteLine("Deleting previous zip file : " + zip_dest);
            Console.WriteLine("Creating zip file from : " + zip_source + "\n\t to : " + zip_dest);
            File.Delete(zip_dest);
            System.IO.Compression.ZipFile.CreateFromDirectory(zip_source, zip_dest);

            // Version.xml
            string version_dest = $@"{destPath}{productName}\version.xml";
            string version_template = $"{templatesPath}version_template.xml";
            Console.WriteLine("Modifying version file : " + version_dest + "\n\t by : " + version_template);
            File.WriteAllText(version_dest, File.ReadAllText(version_template).Replace("{VERSION}", version).Replace("{PATH}", productName));

            // File sizes
            string installer_dest = $@"{destPath}{productName}\{installerName}";
            Console.WriteLine("Getting file size of " + installer_dest);
            FileSize unit = 0;
            double FileSize = new FileInfo(installer_dest).Length;
            while (FileSize >= 1024)
            {
                FileSize = Math.Round(FileSize / 1024, 2);
                unit++;
            }
            string FileSizeString = $"{FileSize}{unit.ToString()}";

            Console.WriteLine("Getting file size of " + zip_dest);
            unit = 0;
            FileSize = new FileInfo(zip_dest).Length;
            while (FileSize >= 1024)
            {
                FileSize = Math.Round(FileSize / 1024, 2);
                unit++;
            }
            string ZipSizeString = $"{FileSize}{unit.ToString()}";

            // Publish page
            string template_source = $@"{templatesPath}{productName}_template.txt";
            string template_new = $@"{templatesPath}{productName}.txt";
            Console.WriteLine("Copying template : " + template_source + "\n\t to : " + template_new);
            File.Copy(template_source, template_new, true);
            File.WriteAllText(template_new, File.ReadAllText(template_new).Replace("{VERSION}", version).Replace("{FILESIZE}", FileSizeString).Replace("{ZIPSIZE}", ZipSizeString).Replace("{DATE}", date.ToString("yyyy/MM/dd")));

            // Upload
            Process.Start("CMD", postRun).WaitForExit();
            Console.WriteLine($"POST BUILD {productName.ToUpper()} SUCCESS");
        }
        enum FileSize
        {
            B = 0,
            KB = 1,
            MB = 2,
            GB = 3,
            TB = 4,
        }
    }
}
