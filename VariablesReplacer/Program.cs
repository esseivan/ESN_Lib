using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Tools.VariablesReplacer
{
    public class Program
    {
        /// <summary>
        /// List of ScriptConfigs
        /// </summary>
        public static List<ScriptConfig> ScriptConfigList { get; set; } = new List<ScriptConfig>();

        static void Main(string[] args)
        {
            Run(args);
        }

        public static void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Missing arguments : Enter config path");
                return;
            }

            // If generate template argument
            if (args.Contains("--GT"))
            {
                GenerateTemplates();
                return;
            }

            Console.WriteLine("Importing config file");
            Initialize(args);

            Console.WriteLine("Executing script !");
            try
            {
                ExecuteScripts();
                Console.WriteLine("Complete !");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("ERROR!\nVerify that folders are not open in Windows Explorer !\n" + ex);
                Console.ResetColor();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        public static void Run(string args)
        {
            Run(new string[] { args });
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public static void Initialize(string ConfigPath)
        {
            Tools.Initialize();
            ScriptConfigList.AddRange(Tools.ImportConfig(ConfigPath).Values);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public static void Initialize(string[] ConfigPaths)
        {
            Tools.Initialize();
            foreach (string ConfigPath in ConfigPaths)
            {
                ScriptConfigList.AddRange(Tools.ImportConfig(ConfigPath).Values);
            }
        }

        /// <summary>
        /// Execute all scripts from ScriptConfig
        /// </summary>
        public static void ExecuteScripts()
        {
            // Sort according to run priority
            List<ScriptConfig> sortedList = ScriptConfigList.OrderBy(o => o.RunPriority).Reverse().ToList();

            ScriptConfig scriptConfig;
            for (int i = 0; i < sortedList.Count; i++)
            {
                scriptConfig = sortedList.ElementAt(i);

                // Run the replacement
                if (scriptConfig.Mode == ScriptConfig.ReplacementMode.FileContent)
                {
                    Replacer.Replace_FileContent(scriptConfig);
                }
                else if (scriptConfig.Mode == ScriptConfig.ReplacementMode.FileNames)
                {
                    Replacer.Replace_Names(scriptConfig);
                }

                if (!scriptConfig.ConfigAfter)
                {
                    Replacer.CopyToOutput(scriptConfig);
                }
            }
        }

        /// <summary>
        /// Generate template files
        /// </summary>
        public static void GenerateTemplates()
        {
            string cfgpath = Path.Combine(Environment.CurrentDirectory, "config.cfg");
            string varpath = Path.Combine(Environment.CurrentDirectory, "var.cfg");
            Tools.ExportConfig(cfgpath, new ScriptConfig[] { new ScriptConfig()
            {
                Mode = ScriptConfig.ReplacementMode.FileContent,
                FilterVariableMode = ScriptConfig.FilterMode.None,
                Variables = new string[] {"C:\\TBD" },
                ContentFiles = new string[] {"C:\\TBD" },
                DataFiles = new string[] {"C:\\TBD" },
                OutputFiles = new string[] {"C:\\TBD" },
                DataPath = "C:\\TBD" ,
                OutputPath = "C:\\TBD" ,
                RunPriority = 1,
                ConfigAfter = false,
                ConfigBefore = false,
                IncludeSubDirectories = true,
            } });
            Tools.ExportVariables(varpath, new Dictionary<string, Variable>()
            {
                {"Test",new Variable() {
                    Data = "DATA",
                    Name = "NAME",
                } }
            });
        }
    }
}
