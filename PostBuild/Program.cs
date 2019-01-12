using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            string version = Application.ProductVersion;
			Console.WriteLine("Current version : " + version);
            string productName = "Libraries";
            string InstallerName = "Libraries.msi";

            Task.WhenAny(Task.Delay(1000));

            // Zip
            File.Delete($@"C:\WorkSpace\Personnal\website\html\files\Libraries\{productName}.zip");
            System.IO.Compression.ZipFile.CreateFromDirectory($@"C:\WorkSpace\Personnal\Librairies\EsseivaN\bin\Release\", $@"C:\WorkSpace\Personnal\website\html\files\{productName}\{productName}.zip");

            // Version.xml
            File.WriteAllText($@"C:\WorkSpace\Personnal\website\html\files\{productName}\version.xml", File.ReadAllText(@"C:\WorkSpace\Personnal\website\workspace\content\version_template.xml").Replace("{VERSION}", version).Replace("{PATH}", productName));

            // File sizes
            FileSize unit = 0;
            double FileSize = new FileInfo($@"C:\WorkSpace\Personnal\website\html\files\{productName}\{InstallerName}").Length;
            while (FileSize >= 1024)
            {
                FileSize = Math.Round(FileSize / 1024, 2);
                unit++;
            }
            string FileSizeString = $"{FileSize}{unit.ToString()}";

            unit = 0;
            FileSize = new FileInfo($@"C:\WorkSpace\Personnal\website\html\files\{productName}\{productName}.zip").Length;
            while (FileSize >= 1024)
            {
                FileSize = Math.Round(FileSize / 1024, 2);
                unit++;
            }
            string ZipSizeString = $"{FileSize}{unit.ToString()}";

            // Publish page
            File.Copy($@"C:\WorkSpace\Personnal\website\workspace\content\{productName}_template.txt", $@"C:\WorkSpace\Personnal\website\workspace\content\{productName}.txt", true);
            File.WriteAllText($@"C:\WorkSpace\Personnal\website\workspace\content\{productName}.txt", File.ReadAllText($@"C:\WorkSpace\Personnal\website\workspace\content\{productName}.txt").Replace("{VERSION}", Application.ProductVersion).Replace("{FILESIZE}", FileSizeString).Replace("{ZIPSIZE}", ZipSizeString));

            // Upload
            Process.Start("CMD", @"/C C:\WorkSpace\Personnal\website\workspace\WebEditAndUpload.bat").WaitForExit();
            Console.WriteLine($"POST BUILD Libraries SUCCESS");
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
