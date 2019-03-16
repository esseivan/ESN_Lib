using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsseivaN.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Missing arguments : Enter config path");
                return;
            }

            // If generate template argument
            if(args.Contains("--GT"))
            {
                GenerateTemplates();
                return;
            }

            Console.WriteLine("Importing " + (args.Length) + " config file");
            foreach (string path in args)
            {
                Replacer.ImportConfig(path);
            }
            Console.WriteLine("Executing script !");
            try
            {
                Replacer.ExecuteScripts();
                Console.WriteLine("Complete !");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("ERROR. Verify that folders are not open in Windows Explorer !\n" + ex);
                Console.ResetColor();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private static void GenerateTemplates()
        {
            string cfgpath = Path.Combine(Environment.CurrentDirectory, "config.cfg");
            string varpath = Path.Combine(Environment.CurrentDirectory, "var.cfg");
            Replacer.ExportConfig(cfgpath, new Replacer.Config[] { new Replacer.Config()
            {
                Mode = Replacer.Config.ReplacementMode.FileContent,
                FilterVariableMode = Replacer.Config.FilterMode.None,
                Variables = new string[] {"C:\\TBD" },
                ContentFiles = new string[] {"C:\\TBD" },
                DataFiles = new string[] {"C:\\TBD" },
                OutputFiles = new string[] {"C:\\TBD" },
                DataDirectory = "C:\\TBD" ,
                OutputPath = "C:\\TBD" ,
            } });
            Replacer.ExportVariables(varpath, new Dictionary<string, Replacer.Variable>()
            {
                {"Test",new Replacer.Variable() {
                    Data = "DATA",
                    Name = "NAME",
                } }
            });
        }
    }
}
