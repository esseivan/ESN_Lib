using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace EsseivaN.Tools
{
    public class SettingManager_Fast
    {
        public static string GetDefaultPath(string appName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EsseivaN", appName);
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void Save<T>(string path, T setting, bool backup = true, bool indent = true)
        {
            // Make backup
            if (backup)
            {
                string bakPath = path + ".bak";
                if (File.Exists(bakPath))
                    File.Delete(bakPath);
                if (File.Exists(path))
                    File.Move(path, bakPath);
            }

            File.WriteAllText(path, Serialize(setting, indent));
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public static bool Load<T>(string path, out T output)
        {
            if (File.Exists(path))
            {
                // Load settings from raw data
                string fileData = File.ReadAllText(path);
                if (string.IsNullOrEmpty(fileData))
                {
                    throw new FileLoadException("Unable to read data from specified file. Aborting");
                }

                T setting = Deserialize<T>(fileData);
                output = setting;
                return true;
            }
            else
            {
                output = default;
                return false;
            }
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void SaveAppName<T>(string appName, T setting, bool backup = true, bool indent = true)
        {
            Save(GetDefaultPath(appName), setting, backup, indent);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public static bool LoadAppName<T>(string appName, out T output)
        {
            return Load(GetDefaultPath(appName), out output);
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string Serialize<T>(T data, bool indent)
        {
            return JsonConvert.SerializeObject(data, indent ? Formatting.Indented : Formatting.None);
        }
    }
}
