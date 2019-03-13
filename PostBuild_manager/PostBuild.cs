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
                Exit(ExitCodes.InvalidArguments);
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
                version,
                description;

            DateTime date;

            // Get config
            string configPath = args[0];
            // Check if file exists
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine("Invalid config file path ! : " + configPath);
                Exit(ExitCodes.InvalidPath_Config);
                return;
            }
            string data = string.Empty;
            // Try reading content
            try
            {
                data = File.ReadAllText(configPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to read config file ! : " + ex);
                Exit(ExitCodes.UnableRead_Config);
                return;
            }
            data = data.Replace("\r", "");
            string[] datas = data.Split('\n');
            if (datas.Length < 8)
            {
                Console.WriteLine("Invalid config file");
                Exit(ExitCodes.Invalid_Config);
                return;
            }

            // Path of the file to read version and date
            fileVersionPath = datas[0].Replace("\"", "");

            // Path of the bin release folder (from the config base)
            binFolderPath = datas[1].Replace("\"", "");

            // template name
            templateBaseName = datas[2].Replace("\"", "");

            // product name, used for the infos.xml file
            productBaseName = datas[3].Replace("\"", "");

            // zip name
            zipBaseName = datas[4].Replace("\"", "");

            // Path of the web folder (from the config base)
            webFolderPath = datas[5].Replace("\"", "");

            // Installer file name (without extension)
            installerName = datas[6].Replace("\"", "");

            // Silent installer file name (without extension)
            silentInstallerName = datas[7].Replace("\"", "");

            // Description on one line
            description = datas[8].Replace("\"", "");

            // Read the general config file
            configPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.cfg";
            // Check if file exists
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine("Invalid general config file path ! : " + configPath);
                Exit(ExitCodes.InvalidPath_GeneralConfig);
                return;
            }
            // Try reading content
            try
            {
                data = File.ReadAllText(configPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to read general config file ! : " + ex);
                Exit(ExitCodes.UnableRead_GeneralConfig);
                return;
            }
            data = data.Replace("\r", "");
            datas = data.Split('\n');
            if (datas.Length < 3)
            {
                Console.WriteLine("Invalid general config file !");
                Exit(ExitCodes.Invalid_GeneralConfig);
                return;
            }
            // Base destination path
            string baseDestinationPath = datas[0].Replace("\"", "");
            // templates file
            string websiteWorkspacePath = datas[1].Replace("\"", "");
            // post run cmd
            string postRun = datas[2].Replace("\"", "");

            // Get the creation time and version
            if (!File.Exists(fileVersionPath))
            {
                Console.Error.WriteLine("Invalid file version path ! : " + fileVersionPath);
                Exit(ExitCodes.InvalidPath_FileVersion);
                return;
            }
            FileInfo fileInfo = new FileInfo(fileVersionPath);
            date = fileInfo.LastWriteTime;
            Console.WriteLine("Creation date : " + date);
            if (File.Exists(binFolderPath + "\\config_version.txt"))
            {
                version = File.ReadAllText(binFolderPath + "\\config_version.txt").Replace("\r","").Split('\n')[0];
            }
            else
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(fileVersionPath);
                version = fileVersionInfo.ProductVersion;
            }
            Console.WriteLine("Version : " + version);

            // Create a zipped file of the output
            string zip_dest = $@"{baseDestinationPath}{webFolderPath}\{zipBaseName}.zip";
            string zip_source = binFolderPath + @"\";
            if (!Directory.Exists(zip_source))
            {
                Console.Error.WriteLine("Invalid zip source directory ! : " + zip_source);
                Exit(ExitCodes.InvalidPath_ZipSource);
                return;
            }
            Console.WriteLine("Deleting previous zip file : " + zip_dest);
            Console.WriteLine("Creating zip file from : " + zip_source + "\n\t to : " + zip_dest);
            if (File.Exists(zip_dest))
            {
                File.Delete(zip_dest);
            }

            try
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(zip_source, zip_dest);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to create zip file ! : " + ex);
                Exit(ExitCodes.UnableWrite_ZipDest);
                return;
            }

            // File sizes
            string installer_dest = $@"{baseDestinationPath}{webFolderPath}\{installerName}";
            if (!File.Exists(installer_dest))
            {
                Console.Error.WriteLine("Invalid installer destionation path ! : " + installer_dest);
                Exit(ExitCodes.InvalidPath_Installer);
                return;
            }
            Console.WriteLine("Getting file size of " + installer_dest);
            FileSize unit = 0;
            double FileSize = new FileInfo(installer_dest).Length;
            while (FileSize >= 1024)
            {
                FileSize = Math.Round(FileSize / 1024, 2);
                unit++;
            }
            string FileSizeString = $"{GetFileSize(installer_dest)}{unit.ToString()}";

            if (!File.Exists(zip_dest))
            {
                Console.Error.WriteLine("Invalid zip destionation path ! : " + zip_dest);
                Exit(ExitCodes.InvalidPath_ZipDest);
                return;
            }
            Console.WriteLine("Getting file size of " + zip_dest);
            unit = 0;
            FileSize = new FileInfo(zip_dest).Length;
            while (FileSize >= 1024)
            {
                FileSize = Math.Round(FileSize / 1024, 2);
                unit++;
            }
            string ZipSizeString = $"{FileSize}{unit.ToString()}";

            // Infos.xml
            string infos_dest = $@"{baseDestinationPath}{webFolderPath}\infos.xml";
            string infos_template = $@"{websiteWorkspacePath}\infos_template.xml";
            if (!File.Exists(infos_template))
            {
                Console.Error.WriteLine("Invalid version template file path ! : " + infos_template);
                Exit(ExitCodes.InvalidPath_VersionTemplate);
                return;
            }
            Console.WriteLine("Modifying info file : " + infos_dest + "\n\t by : " + infos_template);
            try
            {
                File.WriteAllText(infos_dest, File.ReadAllText(infos_template).Replace("{VERSION}", version)
                    .Replace("{PATH}", webFolderPath.Replace(@"\", "/"))
                    .Replace("{NAME}", productBaseName)
                    .Replace("{SILENTFILENAME}", silentInstallerName)
                    .Replace("{FILENAME}", installerName)
                    .Replace("{SIZE}", ZipSizeString)
                    .Replace("{DATE}", date.ToString("yyyy/MM/dd"))
                    .Replace("{DESCRIPTION}", description));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to create modified version file ! : " + ex);
                Exit(ExitCodes.UnableWrite_Version);
                return;
            }

            // Publish page
            string template_source = $@"{websiteWorkspacePath}{templateBaseName}_template.txt";
            string template_new = $@"{websiteWorkspacePath}{templateBaseName}.txt";
            if (!File.Exists(template_source))
            {
                Console.Error.WriteLine("Invalid publish template path ! : " + template_source);
                Exit(ExitCodes.InvalidPath_PublishTemplate);
                return;
            }
            Console.WriteLine("Copying template : " + template_source + "\n\t to : " + template_new);
            try
            {
                File.Copy(template_source, template_new, true);
                File.WriteAllText(template_new, File.ReadAllText(template_new).Replace("{VERSION}", version)
                    .Replace("{FILESIZE}", FileSizeString)
                    .Replace("{ZIPSIZE}", ZipSizeString)
                    .Replace("{DATE}", date.ToString("yyyy/MM/dd")));

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to create modified publish file ! : " + ex);
                Exit(ExitCodes.UnableWrite_Publish);
                return;
            }

            // Upload
            Process uploadProcess = Process.Start("CMD", postRun);
            uploadProcess.WaitForExit();
            if (uploadProcess.ExitCode == 0)
            {
                Console.WriteLine($"POST BUILD {productBaseName.ToUpper()} SUCCESS");
            }
            else
            {
                Console.WriteLine("Unable to upload : ExitCode : " + uploadProcess.ExitCode);
                Exit(ExitCodes.Upload_Error_Start + uploadProcess.ExitCode);
                return;
            }
        }

        private static void Exit(int exitCode)
        {
            Environment.ExitCode = exitCode;
            Environment.Exit(Environment.ExitCode);
        }

        private static void Exit(ExitCodes exitCode)
        {
            Exit((int)exitCode);
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

        enum ExitCodes
        {
            Unknown = 99,
            InvalidArguments = 1,

            InvalidPath_Config = 100,
            InvalidPath_GeneralConfig = 101,
            InvalidPath_FileVersion = 102,
            InvalidPath_ZipSource = 103,
            InvalidPath_ZipDest = 104,
            InvalidPath_VersionTemplate = 105,
            InvalidPath_Installer = 106,
            InvalidPath_PublishTemplate = 107,

            UnableRead_Config = 200,
            UnableRead_GeneralConfig = 201,

            UnableWrite_ZipDest = 300,
            UnableWrite_Version = 301,
            UnableWrite_Publish = 302,

            Invalid_Config = 300,
            Invalid_GeneralConfig = 301,

            Upload_Error_Start = 1000,

        }
    }
}
