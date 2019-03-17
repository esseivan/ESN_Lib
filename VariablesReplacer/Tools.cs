using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Tools.VariablesReplacer
{
    public class Tools
    {
        private static SettingsManager<ScriptConfig> ScriptConfigSettingsManager = new SettingsManager<ScriptConfig>();
        private static SettingsManager<Variable> VariableSettingsManager = new SettingsManager<Variable>((v) => v.Name);
        /// <summary>
        /// Backup directory
        /// </summary>
        public static readonly string BackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VariablesReplacer_LastBackup");
        /// <summary>
        /// Temp directory
        /// </summary>
        public static readonly string TempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VariablesReplacer");

        /// <summary>
        /// Initialize
        /// </summary>
        public static void Initialize()
        {
            // Create backup directory if not existing
            if (!Directory.Exists(BackupPath))
            {
                Directory.CreateDirectory(BackupPath);
            }
            // Create temp directory if not existing
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
        }

        /// <summary>
        /// Clear all files in temp folder
        /// </summary>
        public static void ClearTempFolder()
        {
            Console.WriteLine("Clearing temp folder...");
            // Create temp directory if not existing
            DirectoryInfo di = new DirectoryInfo(Tools.TempPath);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
            Console.WriteLine("Temp folder cleared !");
        }

        /// <summary>
        /// Copy specified folder to temp directory
        /// </summary>
        public static string CopyFolderToTemp(string path)
        {
            string tempDirectory = GetTempPath(path);
            // Create directory and fill if not existing
            if (!Directory.Exists(tempDirectory))
            {
                // Create
                Directory.CreateDirectory(tempDirectory);

                // Copy files and folders to temp directory
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(path, "*",
                    SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(path, tempDirectory));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(path, "*.*",
                    SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(path, tempDirectory));
                }
            }
            Console.WriteLine("Temp folder cleared !");
            return tempDirectory;
        }

        /// <summary>
        /// Copy specified file to temp directory
        /// </summary>
        public static string CopyFileToTemp(string path)
        {
            string tempPath = GetTempPath(path);

            // Copy to temp path if not existing
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            File.Copy(path, tempPath);

            Console.WriteLine("Temp file cleared !");

            return tempPath;
        }

        /// <summary>
        /// Move the specified directory in a new folder created at this place
        /// </summary>
        public static void CopyToOutput(string path, string outputPath)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            
            Directory.Move(path, outputPath);
        }

        /// <summary>
        /// Get the temp path
        /// </summary>
        public static string GetTempPath(string path)
        {
            return Path.Combine(TempPath, Path.GetFileName(path));
        }
        
        /// <summary>
        /// Get the output path
        /// </summary>
        public static string GetOutputPath(ScriptConfig scriptConfig, string workPath, int index)
        {
            string outputPath = string.Empty;
            // If folder not existing, abort
            if (!File.Exists(workPath))
            {
                return string.Empty;
            }

            //If specific output path is set
            if (scriptConfig.OutputFiles.Length > index)
            {
                string outputTemp = scriptConfig.OutputFiles.ElementAt(index);
                if (!string.IsNullOrEmpty(outputTemp))
                {
                    outputPath = scriptConfig.OutputFiles.ElementAt(index);
                }
            }

            // Otherwise, set the default one (output path + base filename)
            if (outputPath == string.Empty)
            {
                outputPath = Path.Combine(scriptConfig.OutputPath, Path.GetFileName(workPath));
            }
            return outputPath;
        }

        /// <summary>
        /// Replace text from the config
        /// </summary>
        public static string ReplaceText(string data, ScriptConfig scriptConfig)
        {
            // For each content file
            foreach (string contentfile in scriptConfig.ContentFiles)
            {
                // Load variables
                Dictionary<string, Variable> loadedVars = null;
                try
                {
                    loadedVars = VariableSettingsManager.Load(contentfile);
                }
                catch (Exception ex)
                {
                    // Skip this content file
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


                    switch (scriptConfig.FilterVariableMode)
                    {
                        // No filter
                        case ScriptConfig.FilterMode.None:
                            // Search in data if found
                            if (data.Contains(varName))
                            {
                                // If found replace
                                Console.WriteLine("found variable " + varName + " in " + contentfile);
                                data = data.Replace(varName, varData);
                            }
                            break;

                        // Whitelist
                        case ScriptConfig.FilterMode.Whitelist:
                            // check that this variable is whitelisted
                            if (scriptConfig.Variables.Contains(varName))
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
                        case ScriptConfig.FilterMode.Blacklist:
                            // check that this variable is not in the blacklist
                            if (!scriptConfig.Variables.Contains(varName))
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
            }
            return data;
        }

        /// <summary>
        /// Ranem files in the specified directory
        /// </summary>
        public static void RenameFiles(string root, ScriptConfig scriptConfig)
        {
            // Rename files
            foreach (string file in Directory.GetFiles(root, "*.*",
                    (scriptConfig.IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)))
            {
                Rename(file, scriptConfig, false);
            }
        }

        /// <summary>
        /// Rename folders in root folder
        /// </summary>
        public static void RenameFolders(string root, ScriptConfig scriptConfig)
        {
            DirectoryInfo di = new DirectoryInfo(root);
            // Get all directories
            foreach (DirectoryInfo dir in di.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
            {
                // Rename in ascending order
                if (scriptConfig.IncludeSubDirectories)
                {
                    RenameFolders(dir.FullName, scriptConfig);
                }
                Rename(dir, scriptConfig);
            }
        }

        /// <summary>
        /// Rename directory
        /// </summary>
        public static void Rename(DirectoryInfo directoryInfo, ScriptConfig scriptConfig)
        {
            Rename(directoryInfo.FullName, scriptConfig, true);
        }

        /// <summary>
        /// Rename file or directory
        /// </summary>
        public static void Rename(string name, ScriptConfig scriptConfig, bool isFolder)
        {
            string sourceName = name;
            name = ReplaceText(Path.GetFileName(name), scriptConfig);
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

        /// <summary>
        /// Import config from specified file
        /// </summary>
        /// <param name="Path"></param>
        public static Dictionary<string, ScriptConfig> ImportConfig(string Path)
        {
            try
            {
                return ScriptConfigSettingsManager.Load(Path);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load config", ex);
            }
        }

        public static void ExportConfig(string Path, ScriptConfig[] scriptConfigs)
        {
            try
            {
                ScriptConfigSettingsManager.ClearAll();
                ScriptConfigSettingsManager.AddSettingRange(scriptConfigs);
                ScriptConfigSettingsManager.Save(Path);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save config", ex);
            }
        }

        public static Dictionary<string, Variable> ImportVariables(string Path)
        {
            try
            {
                return VariableSettingsManager.Load(Path);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load variables", ex);
            }
        }

        public static void ExportVariables(string Path, Dictionary<string, Variable> variables)
        {
            try
            {
                VariableSettingsManager.AddSettingRange(variables.Values.ToArray());
                VariableSettingsManager.Save(Path);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save variables", ex);
            }
        }

        public static void BackupFiles(string path)
        {
            // If file existing, backup
            if (File.Exists(path))
            {
                string fileBackupPath = Path.Combine(BackupPath, Path.GetFileName(path));
                if (File.Exists(fileBackupPath))
                {
                    File.Delete(fileBackupPath);
                }

                File.Move(path, fileBackupPath);
            }
        }

        public static void BackupFolder(string path)
        {
            string backupDirectory = Path.Combine(BackupPath, Path.GetFileName(path) + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
            // If directory existing, backup
            if (Directory.Exists(path))
            {
                if (Directory.Exists(backupDirectory))
                {
                    Directory.Delete(backupDirectory, true);
                }
                // Move folder
                Directory.Move(path, backupDirectory);
            }
        }
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

    public class ScriptConfig
    {
        /// <summary>
        /// Priority to run this config, the higher this value, the higher the priority
        /// </summary>
        public int RunPriority { get; set; }
        /// <summary>
        /// Determine the mode
        /// </summary>
        public ReplacementMode Mode { get; set; }
        /// <summary>
        /// Replace all the files and directory names variables present in the specified directory (sub included)
        /// </summary>
        public string DataPath { get; set; }
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
        /// Wheter to include sub directories and files or not
        /// </summary>
        /// <param>Unused for : FileContent</param>
        public bool IncludeSubDirectories { get; set; }
        /// <summary>
        /// Indicate if a config use the output of this one (in temp directory)
        /// </summary>
        public bool ConfigAfter { get; set; }
        /// <summary>
        /// Indicate if this config uses the output of another config (in temp directory)
        /// </summary>
        public bool ConfigBefore { get; set; }

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
