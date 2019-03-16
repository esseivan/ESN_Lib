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
        private static string BackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VariablesReplacer_LastBackup");

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

        public static void ExportConfig(string Path, Config[] configs)
        {
            SettingsManager<Config> settingsManager = new SettingsManager<Config>();
            try
            {
                settingsManager.AddSettingRange(configs);
                settingsManager.Save(Path);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save config", ex);
            }
        }

        public static void ExportVariables(string Path, Dictionary<string, Variable> variables)
        {
            SettingsManager<Variable> settingsManager = new SettingsManager<Variable>((v) => v.Name);
            try
            {
                settingsManager.AddSettingRange(variables.Values.ToArray());
                settingsManager.Save(Path);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save config", ex);
            }
        }


        /// <summary>
        /// Execute all scripts from config
        /// </summary>
        public static void ExecuteScripts()
        {
            Console.WriteLine("Clearing temp folder...");
            // Create temp directory if not existing
            if (!Directory.Exists(BackupPath))
            {
                Directory.CreateDirectory(BackupPath);
            }
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(TempPath);
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
            }
            Console.WriteLine("Temp folder cleared !");

            // Run the replacement
            Console.WriteLine(ConfigList.Count + " replacement to do");
            for (int i = 0; i < ConfigList.Count; i++)
            {
                Config config = ConfigList[i];

                Console.WriteLine("Mode : " + config.Mode.ToString());
                switch (config.Mode)
                {
                    case Config.ReplacementMode.FileContent:
                        ReplaceFor_Content(config, false);
                        CopyFor_Content(config);
                        break;
                    case Config.ReplacementMode.NewFileContent:
                        ReplaceFor_Content(config, true);
                        CopyFor_Content(config);
                        break;
                    case Config.ReplacementMode.FileNames:
                        ReplaceFor_FileNames(config, false);
                        CopyFor_FileNames(config);
                        break;
                    case Config.ReplacementMode.SubFilesNames:
                        ReplaceFor_FileNames(config, true);
                        CopyFor_FileNames(config);
                        break;
                    default:
                        // Config disabled, quit
                        Console.WriteLine("Config disabled");
                        return;
                }
            }
        }

        private static string ReplaceText(string data, Config config)
        {
            // Set the settingsmanager to retrieve variables from content files
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
                    // Skip this file
                    Console.WriteLine("Invalid content file : " + contentfile + " :\n" + ex);
                    continue;
                }

                Console.WriteLine(loadedVars.Count + " variables found in " + contentfile);

                // For each variable in that file
                foreach (var loadedVar in loadedVars)
                {
                    var varData = loadedVar.Value.Data;
                    var varName = loadedVar.Key;

                    // If no name, skip
                    if (varName == string.Empty)
                    {
                        continue;
                    }

                    // Add delimiters
                    varName = "{" + varName + "}";


                    switch (config.FilterVariableMode)
                    {
                        // No filter
                        case Config.FilterMode.None:
                            // Search in data if found
                            if (data.Contains(varName))
                            {
                                // If found replace
                                Console.WriteLine("found variable " + varName + " in " + contentfile);
                                data = data.Replace(varName, varData);
                            }
                            break;

                        // Whitelist
                        case Config.FilterMode.Whitelist:
                            // check that this variable is whitelisted
                            if (config.Variables.Contains(varName))
                            {
                                // Search in data if found
                                if (data.Contains(varName))
                                {
                                    // If found replace
                                    Console.WriteLine("found variable " + varName + " in " + contentfile);
                                    data = data.Replace(varName, varData);
                                }
                            }
                            break;

                        // Blacklist
                        case Config.FilterMode.Blacklist:
                            // check that this variable is not in the blacklist
                            if (!config.Variables.Contains(varName))
                            {
                                // Search in data if found
                                if (data.Contains(varName))
                                {
                                    // If found replace
                                    Console.WriteLine("found variable " + varName + " in " + contentfile);
                                    data = data.Replace(varName, varData);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            } // End of foreach contentfile, replacement done for this config and datafile

            return data;
        }

        private static void ReplaceFor_Content(Config config, bool newFile)
        {
            // For each data file
            for (short i = 0; i < config.DataFiles.Length; i++)
            {
                // Retrieve the file path to the temp folder
                string dataFile = config.DataFiles[i];
                string dataPath = Path.Combine(TempPath, Path.GetFileName(dataFile));

                // Copy to temp path if not existing
                if (File.Exists(dataPath))
                {
                    File.Delete(dataPath);
                }
                File.Copy(dataFile, dataPath);

                // Get data from temp path
                string data = File.ReadAllText(dataPath);
                string sourceData = data;

                data = ReplaceText(data, config);

                // If any changes made, apply changes
                if (sourceData != data)
                {
                    File.WriteAllText(dataPath, data);
                    Console.WriteLine("Replacement done for : " + dataFile);
                }
                else
                {
                    Console.WriteLine("No replacement for " + dataFile + " in the current config");
                }
            }
        }

        private static void ReplaceFor_FileNames(Config config, bool SubIncluded)
        {
            string mainDirectory = config.DataDirectory;
            string tempDirectory = Path.Combine(TempPath, Path.GetFileName(mainDirectory));

            // Create directory and fill if not existing
            if (!Directory.Exists(tempDirectory))
            {
                // Create
                Directory.CreateDirectory(tempDirectory);

                // Copy files and folders to temp directory
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(mainDirectory, "*",
                    SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(mainDirectory, tempDirectory));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(mainDirectory, "*.*",
                    SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(mainDirectory, tempDirectory));
                }
            }
            Console.WriteLine("Temp folder cleared !");

            // Rename files
            foreach (string file in Directory.GetFiles(tempDirectory, "*.*",
                    (SubIncluded ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)))
            {
                ReplaceName(file, config, false);
            }
            // Rename folders
            RenameFolders(tempDirectory, config, SubIncluded);
        }

        private static void RenameFolders(string path, Config config, bool SubIncluded)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            // Get all directories
            foreach (DirectoryInfo dir in di.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
            {
                // Rename subfolders before
                if (SubIncluded)
                {
                    RenameFolders(dir.FullName, config);
                }
                ReplaceName(dir, config);
            }
        }

        private static void RenameFolders(string path, Config config)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            // Get all directories
            foreach (DirectoryInfo dir in di.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
            {
                // Rename subfolders before
                RenameFolders(dir.FullName, config);
                ReplaceName(dir, config);
            }
        }

        private static void ReplaceName(DirectoryInfo directoryInfo, Config config)
        {
            string sourceName = directoryInfo.FullName;
            string name = ReplaceText(directoryInfo.Name, config);
            if (Path.GetFileName(sourceName) != name)
            {
                Directory.Move(sourceName, Path.Combine(Path.GetDirectoryName(sourceName), name));
            }
        }

        private static void ReplaceName(string name, Config config, bool isFolder)
        {
            string sourceName = name;
            name = ReplaceText(Path.GetFileName(name), config);
            if (Path.GetFileName(sourceName) != name)
            {
                if (isFolder)
                {
                    Directory.Move(sourceName, Path.Combine(Path.GetDirectoryName(sourceName), name));
                }
                else
                {
                    File.Move(sourceName, Path.Combine(Path.GetDirectoryName(sourceName), name));
                }
            }
        }

        private static void CopyFor_Content(Config config)
        {
            // For each data file
            for (short i = 0; i < config.DataFiles.Length; i++)
            {
                // Retrieve the file paths
                string dataFile = config.DataFiles[i];
                string dataPath = Path.Combine(TempPath, Path.GetFileName(dataFile));
                string outputPath = string.Empty;

                // If folder not existing, abort
                if (!File.Exists(dataPath))
                {
                    return;
                }

                //If specific output path is set, apply
                if (config.OutputFiles.Length > i)
                {
                    if (config.OutputFiles.ElementAt(i) != string.Empty)
                    {
                        outputPath = config.OutputFiles.ElementAt(i);
                    }
                }

                // Otherwise, set the default one (output path + base filename)
                if (outputPath == string.Empty)
                {
                    outputPath = Path.Combine(Path.GetFullPath(config.OutputPath), Path.GetFileName(dataFile));
                }

                Console.WriteLine("Backing up to " + BackupPath);
                // If file existing, backup
                if (File.Exists(outputPath))
                {
                    string fileBackupPath = Path.Combine(BackupPath, Path.GetFileName(outputPath));
                    if (File.Exists(fileBackupPath))
                    {
                        File.Delete(fileBackupPath);
                    }

                    File.Move(outputPath, fileBackupPath);
                }

                File.WriteAllText(outputPath, File.ReadAllText(dataPath));
            }
        }

        private static void CopyFor_FileNames(Config config)
        {
            // Retrieve the paths
            string mainDirectory = config.DataDirectory;
            string tempDirectory = Path.Combine(TempPath, Path.GetFileName(mainDirectory));
            string outputDirectory = config.OutputPath;
            string backupDirectory = Path.Combine(BackupPath, Path.GetFileName(outputDirectory) + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));

            if (!Directory.Exists(tempDirectory))
            {
                return;
            }

            // If directory existing, backup
            if (Directory.Exists(outputDirectory))
            {
                if (Directory.Exists(backupDirectory))
                {
                    Directory.Delete(backupDirectory, true);
                }
                Console.WriteLine("Backing up to " + backupDirectory);
                // Move folder
                Directory.Move(outputDirectory, backupDirectory);
            }

            // Move new directory
            Directory.Move(tempDirectory, outputDirectory);
        }

        public class Variable
        {
            /// <summary>
            /// The data of the variable
            /// </summary>
            public string Data { get; set; }
            /// <summary>
            /// The name of the variable (used for export only)
            /// </summary>
            public string Name { get; set; }
        }

        public class Config
        {
            /// <summary>
            /// Determine the mode
            /// </summary>
            public ReplacementMode Mode { get; set; }
            /// <summary>
            /// Replace all the files and directory names variables present in the specified directory (sub included)
            /// </summary>
            public string DataDirectory { get; set; }
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
            /// Specify which variables should be replaced (true) or replace all (false)
            /// </summary>
            public FilterMode FilterVariableMode { get; set; }
            /// <summary>
            /// List of variables to be replaced
            /// </summary>
            public string[] Variables { get; set; }
            /// <summary>
            /// Output directory
            /// </summary>
            public string OutputPath { get; set; }

            /// <summary>
            /// Determine the mode
            /// </summary>
            public enum ReplacementMode
            {
                /// <summary>
                /// This config is disabled
                /// </summary>
                None = 0,
                /// <summary>
                /// Replace content of specified files
                /// </summary>
                FileContent = 1,
                /// <summary>
                /// Rename files and directories in the specified directory excluding subdirectories
                /// </summary>
                FileNames = 2,
                /// <summary>
                /// Rename files and directories in the specified directory including subdirectories
                /// </summary>
                SubFilesNames = 3,
                /// <summary>
                /// Same as file content but reuse the base one instead of the modified one
                /// </summary>
                NewFileContent = 4,
            }

            public enum FilterMode
            {
                /// <summary>
                /// Don't filter
                /// </summary>
                None = 0,
                /// <summary>
                /// Include the specified variables
                /// </summary>
                Whitelist = 1,
                /// <summary>
                /// Exclude the specified variables
                /// </summary>
                Blacklist = 2,
            }
        }
    }
}
