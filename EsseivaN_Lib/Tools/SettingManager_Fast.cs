using Newtonsoft.Json;
using System.IO;

namespace EsseivaN.Tools
{
    public class SettingManager_Fast
    {
        /// <summary>
        /// Indique si le fichier est indenté
        /// </summary>
        public static bool Indent { get; set; } = true;

        /// <summary>
        /// Indique si un backup est créée avant la sauvegarde
        /// </summary>
        public static bool Backup { get; set; } = true;

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void Save<T>(string path, T setting)
        {
            // Make backup
            if (Backup)
            {
                string bakPath = path + ".bak";
                if (File.Exists(bakPath))
                    File.Delete(bakPath);
                if (File.Exists(path))
                    File.Move(path, bakPath);
            }

            File.WriteAllText(path, Serialize(setting, Indent));
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
