using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Tools
{
    /// <summary>
    /// Manage a single setting using json. Usefull for creating a setting class and saving it
    /// </summary>
    public class SettingManager<T>
    {
        /// <summary>
        /// The setting to save
        /// </summary>
        private T setting;

        /// <summary>
        /// Indique si le fichier est indenté
        /// </summary>
        public bool Indent { get; set; } = true;

        /// <summary>
        /// Indique si un backup est créée avant la sauvegarde
        /// </summary>
        public bool Backup { get; set; } = true;

        /// <summary>
        /// Create a new settings manager with default getName function (Not recommended)
        /// </summary>
        public SettingManager() { }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public void Save(string path)
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

            File.WriteAllText(path, GenerateFileData());
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public void Save(string path, T setting)
        {
            SetSetting(setting);
            Save(path);
        }

        /// <summary>
        /// Generate file data to be saved in file
        /// </summary>
        public string GenerateFileData()
        {
            return Serialize(setting, Indent);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public bool Load(string path, out T output)
        {
            if (File.Exists(path))
            {
                // Load settings from raw data
                string fileData = File.ReadAllText(path);
                if (string.IsNullOrEmpty(fileData))
                {
                    throw new FileLoadException("Unable to read data from specified file. Aborting");
                }

                setting = Deserialize(fileData);
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
        /// Get setting
        /// </summary>
        public T GetSetting()
        {
            return setting;
        }

        /// <summary>
        /// Set setting
        /// </summary>
        public void SetSetting(T setting)
        {
            this.setting = setting;
        }

        /// <summary>
        /// Clear setting
        /// </summary>
        public void Clear()
        {
            this.setting = default;
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static T Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string Serialize(T data, bool indent)
        {
            return JsonConvert.SerializeObject(data, indent ? Formatting.Indented : Formatting.None);
        }
    }
}
