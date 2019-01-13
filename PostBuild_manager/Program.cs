using System;
using System.Diagnostics;
using System.IO;

namespace EsseivaN.Tools
{
    public class PostBuild
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid arguments");
                Console.WriteLine("Example of use :");
                Console.WriteLine("<config file full path>");
                return;
            }

            string fileVersionPath,
                templateBaseName,
                productBaseName,
                zipBaseName,
                installerName,
                silentInstallerName,
                binFolderPath,
                webFolderPath,
                version;

            DateTime date;

            // Get config/
            string configPath = args[0];
            string data = File.ReadAllText(configPath);
            data = data.Replace("\r", "");
            string[] datas = data.Split('\n');

            if (datas.Length < 8)
            {
                Console.WriteLine("Invalid config file");
                return;
            }

            // Path of the file to read version and date (from binFolderPath)
            fileVersionPath = datas[0].Replace("\"", "");

            // Path of the bin release folder (from the config base)
            binFolderPath = datas[1].Replace("\"", "");

            // template name
            templateBaseName = datas[2].Replace("\"", "");

            // product name, used for the version.xml file
            productBaseName = datas[3].Replace("\"", "");

            // zip name
            zipBaseName = datas[4].Replace("\"", "");

            // Path of the web folder (from the config base)
            webFolderPath = datas[5].Replace("\"", "");

            // Installer file name (without extension)
            installerName = datas[6].Replace("\"", "");

            // Silent installer file name (without extension)
            silentInstallerName = datas[7].Replace("\"", "");

            // Read the general config file
            configPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.cfg";
            data = File.ReadAllText(configPath);
            data = data.Replace("\r", "");
            datas = data.Split('\n');
            // Base destination path
            string baseDestinationPath = datas[0].Replace("\"", "");
            // templates file
            string websiteWorkspacePath = datas[1].Replace("\"", "");
            // post run cmd
            string postRun = datas[2].Replace("\"", "");

            // Get the creation time
            FileInfo fileInfo = new FileInfo(fileVersionPath);
            date = fileInfo.LastWriteTime;
            Console.WriteLine("Creation date : " + date);

            // Get the version
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(fileVersionPath);
            version = fileVersionInfo.ProductVersion;
            Console.WriteLine("Version : " + version);

            // Create a zipped file of the output
            string zip_dest = $@"{baseDestinationPath}{webFolderPath}\{zipBaseName}.zip";
            string zip_source = binFolderPath + @"\";
            Console.WriteLine("Deleting previous zip file : " + zip_dest);
            Console.WriteLine("Creating zip file from : " + zip_source + "\n\t to : " + zip_dest);
            File.Delete(zip_dest);
            System.IO.Compression.ZipFile.CreateFromDirectory(zip_source, zip_dest);

            // Version.xml
            string version_dest = $@"{baseDestinationPath}{webFolderPath}\version.xml";
            string version_template = $@"{websiteWorkspacePath}\version_template.xml";
            Console.WriteLine("Modifying version file : " + version_dest + "\n\t by : " + version_template);
            File.WriteAllText(version_dest, File.ReadAllText(version_template).Replace("{VERSION}", version).Replace("{PATH}", webFolderPath.Replace(@"\", "/")).Replace("{NAME}", productBaseName).Replace("{SILENTFILENAME}", silentInstallerName));

            // File sizes
            string installer_dest = $@"{baseDestinationPath}{webFolderPath}\{installerName}";
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
            string template_source = $@"{websiteWorkspacePath}{templateBaseName}_template.txt";
            string template_new = $@"{websiteWorkspacePath}{templateBaseName}.txt";
            Console.WriteLine("Copying template : " + template_source + "\n\t to : " + template_new);
            File.Copy(template_source, template_new, true);
            File.WriteAllText(template_new, File.ReadAllText(template_new).Replace("{VERSION}", version).Replace("{FILESIZE}", FileSizeString).Replace("{ZIPSIZE}", ZipSizeString).Replace("{DATE}", date.ToString("yyyy/MM/dd")));

            // Upload
            Process.Start("CMD", postRun).WaitForExit();
            Console.WriteLine($"POST BUILD {productBaseName.ToUpper()} SUCCESS");
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
