using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Tools
{
    public class Replacer
    {
        /// <summary>
        /// List of configs
        /// </summary>
        public static List<Config> ConfigList { get; set; } = new List<Config>();
        /// <summary>
        /// Temp output path
        /// </summary>
        private static string TempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VariablesReplacer");

        /// <summary>
        /// Import config from specified file
        /// </summary>
        /// <param name="Path"></param>
        public static void ImportConfig(string Path)
        {
            SettingsManager<Config> settingsManager = new SettingsManager<Config>();
            try
            {
                ConfigList.AddRange(settingsManager.Load(Path).Values);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load config", ex);
            }
        }

        /// <summary>
        /// Execute all scripts from config
        /// </summary>
        public static void ExecuteScripts()
        {
            Console.WriteLine(ConfigList.Count + " replacement to do");
            foreach (var parameter in ConfigList)
            {
                ExecuteScript(parameter);
            }
        }

        /// <summary>
        /// Execute a script from the config
        /// </summary>
        /// <param name="config"></param>
        public static void ExecuteScript(Config config)
        {
            // Create output directory if not existing
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }

            // Delete old files in output directory
            foreach (string htmlFile in config.DataFiles)
            {
                File.Delete(Path.Combine(TempPath, Path.GetFileName(htmlFile)));
            }
            // For each data file
            for (short i = 0; i < config.DataFiles.Length; i++)
            {
                // Retrieve the file path t the temp folder
                string dataFile = config.DataFiles[i];
                string dataPath = Path.Combine(TempPath, Path.GetFileName(dataFile));

                // If temp file already existing (because already edited), reopen it. Otherwise, open the base file
                string data = File.Exists(dataPath) ? File.ReadAllText(dataPath) : File.ReadAllText(dataFile);

                // Set the settingsmanager to retrieve data
                SettingsManager<Variable> variableSettingsManager = new SettingsManager<Variable>();

                // For each content file
                foreach (string contentfile in config.ContentFiles)
                {
                    // Load variables
                    Dictionary<string, Variable> loadedVars = null;
                    try
                    {
                        loadedVars = variableSettingsManager.Load(contentfile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid content file : " + contentfile);
                        continue;
                    }

                    Console.WriteLine(loadedVars.Count + " variables found in " + contentfile);

                    // For each variable in that file
                    foreach (var loadedVar in loadedVars)
                    {
                        var varData = loadedVar.Value.Data;
                        var varName = loadedVar.Key;

                        if (varName == string.Empty)
                        {
                            continue;
                        }

                        varName = "{" + varName + "}";

                        // Check if the variable is in the current config
                        if (config.Variables.Contains(varName))
                        {
                            Console.WriteLine("found variable " + varName + " in " + contentfile);
                            // Replace the variable by the data
                            data = data.Replace(varName, varData);
                        }
                    }
                }

                // If output not specified for this file, output to temp folder
                string path = string.Empty;
                //If specific output path is set, apply
                if (config.OutputFiles.Length > i)
                {
                    if (config.OutputFiles.ElementAt(i) != string.Empty)
                    {
                        path = config.OutputFiles.ElementAt(i);
                    }
                }

                // Otherwise, set the default one (output path + base filename)
                if (path == string.Empty)
                {
                    path = Path.Combine(Path.GetFullPath(config.OutputPath), Path.GetFileName(dataFile));
                }

                File.WriteAllText(path, data);
                Console.WriteLine("Replacement done for : " + dataFile);
            }
        }

        public class Variable
        {
            /// <summary>
            /// The data of the variable
            /// </summary>
            public string Data { get; set; }
        }

        public class Config
        {
            /// <summary>
            /// Files to be replaced
            /// </summary>
            public string[] DataFiles { get; set; }
            /// <summary>
            /// Output files path
            /// </summary>
            public string[] OutputFiles { get; set; }
            /// <summary>
            /// Files to get variables from
            /// </summary>
            public string[] ContentFiles { get; set; }
            /// <summary>
            /// List of variables to be replaced
            /// </summary>
            public string[] Variables { get; set; }
            /// <summary>
            /// Output directory
            /// </summary>
            public string OutputPath { get; set; }
        }
    }
}
