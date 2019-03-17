using System;
using System.IO;

namespace EsseivaN.Tools.VariablesReplacer
{
    public class Replacer
    {
        private class ValidResult
        {
            public bool DirectoryValid { get; set; } = false;
            public bool FilesValid { get; set; } = false;
        }

        private static ValidResult CheckValid(ScriptConfig scriptConfig)
        {
            ValidResult validResult = new ValidResult()
            {
                DirectoryValid = true,
                FilesValid = true,
            };
            

            // Determine if Datadirectory is valid
            if (scriptConfig.DataPath == null)
            {
                validResult.DirectoryValid = false;
            }

            if (scriptConfig.DataPath == string.Empty)
            {
                validResult.DirectoryValid = false;
            }

            if (!Directory.Exists(scriptConfig.DataPath))
            {
                validResult.DirectoryValid = false;
            }

            // Determine if DataFiles is valid
            if (scriptConfig.DataFiles == null)
            {
                validResult.FilesValid = false;
            }

            if (scriptConfig.DataFiles.Length == 0)
            {
                validResult.FilesValid = false;
            }

            return validResult;
        }

        /// <summary>
        /// Replace file content
        /// </summary>
        public static void Replace_FileContent(ScriptConfig scriptConfig)
        {
            ValidResult valid = CheckValid(scriptConfig);

            // Run replacers
            if (valid.DirectoryValid)
            {
                Replace_FileContent_Directory(scriptConfig);
            }
            if (valid.FilesValid)
            {
                Replace_FileContent_Specified(scriptConfig);
            }
        }

        private static void Replace_FileContent_Specified(ScriptConfig scriptConfig)
        {
            // For each data file
            for (short i = 0; i < scriptConfig.DataFiles.Length; i++)
            {
                // Retrieve the file path to the temp folder
                string sourceFile = scriptConfig.DataFiles[i];
                // Copy to temp path if not existing
                string workFile;

                // Get working path
                if (scriptConfig.ConfigBefore)
                {
                    workFile = Tools.GetTempPath(sourceFile);
                }
                else
                {
                    workFile = Tools.CopyFileToTemp(sourceFile);
                }

                // Read data
                string data = File.ReadAllText(workFile);
                string sourceData = data;

                data = Tools.ReplaceText(data, scriptConfig);

                // If any changes made, apply changes
                if (sourceData != data)
                {
                    File.WriteAllText(workFile, data);
                }
            }
        }

        private static void Replace_FileContent_Directory(ScriptConfig scriptConfig)
        {
            // Get source directory
            SearchOption SubIncluded = scriptConfig.IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string sourceDirectory = scriptConfig.DataPath;
            string workDirectory;

            // Get work directory
            if (scriptConfig.ConfigBefore)
            {
                workDirectory = Tools.GetTempPath(sourceDirectory);
            }
            else
            {
                workDirectory = Tools.CopyFolderToTemp(sourceDirectory);
            }

            if (!Directory.Exists(workDirectory))
            {
                return;
            }

            string data;
            string sourceData;

            // Replace content in al files
            foreach (string file in Directory.GetFiles(workDirectory, "*.*", SubIncluded))
            {
                // Get data from temp path
                data = File.ReadAllText(file);
                sourceData = data;

                data = Tools.ReplaceText(data, scriptConfig);

                // If any changes made, apply changes
                if (sourceData != data)
                {
                    File.WriteAllText(file, data);
                }
            }
        }

        /// <summary>
        /// Replace files and folders names
        /// </summary>
        public static void Replace_Names(ScriptConfig scriptConfig)
        {
            ValidResult valid = CheckValid(scriptConfig);

            // Run replacers
            if (valid.DirectoryValid)
            {
                Replace_Names_Directory(scriptConfig);
            }
            if (valid.FilesValid)
            {
                Replace_Names_Specified(scriptConfig);
            }
        }

        private static void Replace_Names_Specified(ScriptConfig scriptConfig)
        {
            // For each data file
            for (short i = 0; i < scriptConfig.DataFiles.Length; i++)
            {
                // Retrieve the file path to the temp folder
                string sourcePath = scriptConfig.DataFiles[i];
                // Copy to temp path if not existing
                string workPath;

                // Get working path
                if (scriptConfig.ConfigBefore)
                {
                    workPath = Tools.GetTempPath(sourcePath);
                }
                else
                {
                    workPath = Tools.CopyFileToTemp(sourcePath);
                }

                bool isFolder = Directory.Exists(workPath);
                // Renames
                Tools.Rename(workPath, scriptConfig, isFolder);
            }
        }

        private static void Replace_Names_Directory(ScriptConfig scriptConfig)
        {
            // Get source directory
            SearchOption SubIncluded = scriptConfig.IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string sourceDirectory = scriptConfig.DataPath;
            string workDirectory;

            // Get work directory
            if (scriptConfig.ConfigBefore)
            {
                // Take the existing temp directory
                workDirectory = Tools.GetTempPath(sourceDirectory);
            }
            else
            {
                // Copy to the temp directory
                workDirectory = Tools.CopyFolderToTemp(sourceDirectory);
            }

            if (!Directory.Exists(workDirectory))
            {
                return;
            }

            // Renames files before folders
            Tools.RenameFiles(workDirectory, scriptConfig);
            // Rename folders
            Tools.RenameFolders(workDirectory, scriptConfig);
        }

        /// <summary>
        /// Copy to output path
        /// </summary>
        public static void CopyToOutput(ScriptConfig scriptConfig)
        {
            // If script after this one, don't copy now
            if (scriptConfig.ConfigAfter)
            {
                return;
            }

            ValidResult valid = CheckValid(scriptConfig);

            // Run replacers
            if (valid.DirectoryValid)
            {
                CopyToOutput_Directory(scriptConfig);
            }
            if (valid.FilesValid)
            {
                CopyToOutput_Specified(scriptConfig);
            }
        }

        private static void CopyToOutput_Specified(ScriptConfig scriptConfig)
        {
            // For each data file
            for (short i = 0; i < scriptConfig.DataFiles.Length; i++)
            {
                string sourceFile = scriptConfig.DataFiles[i];
                string workPath = Tools.GetTempPath(sourceFile);
                string outputPath = Tools.GetOutputPath(scriptConfig, workPath, i);

                if (string.IsNullOrEmpty(outputPath))
                {
                    continue;
                }

                bool isFolder = Directory.Exists(outputPath);
                if (isFolder)
                {
                    Tools.BackupFolder(outputPath);
                    Tools.CopyToOutput(workPath, outputPath);
                }
                else
                {
                    Tools.BackupFiles(outputPath);
                    File.WriteAllText(outputPath, File.ReadAllText(workPath));
                }
            }
        }

        private static void CopyToOutput_Directory(ScriptConfig scriptConfig)
        {
            string workPath = Tools.GetTempPath(scriptConfig.DataPath);
            string outputPath = scriptConfig.OutputPath;

            if (Directory.Exists(outputPath))
                Tools.BackupFolder(outputPath);

            Tools.CopyToOutput(workPath, outputPath);
        }
    }
}
