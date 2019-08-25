using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Tools
{
    /// <summary>
    /// Manage settings using json
    /// </summary>
    public class SettingsManager<T>
    {
        /// <summary>
        /// List of settings
        /// </summary>
        private List<T> settingsList;

        /// <summary>
        /// List of json settings
        /// </summary>
        private Dictionary<string, T> settingsJsonList;

        /// <summary>
        /// Get name function
        /// </summary>
        public Func<T, string> GetName_Function { get; set; }

        /// <summary>
        /// Number of settings in the list
        /// </summary>
        public int Count => settingsList.Count;

        /// <summary>
        /// Default getName function
        /// </summary>
        /// <returns>value.toString()</returns>
        public string DefaultGetNameFunc(T value)
        {
            return $"{value}@{value.GetHashCode()}";
        }

        /// <summary>
        /// Create a new settings manager with default getName function (Not recommended)
        /// </summary>
        public SettingsManager()
        {
            settingsList = new List<T>();
            GetName_Function = DefaultGetNameFunc;
        }

        /// <summary>
        /// Create a new settings manager with custom getName function
        /// </summary>
        /// <param name="getNameFunc">Function to get the name of the setting</param>
        public SettingsManager(Func<T, string> getNameFunc)
        {
            settingsList = new List<T>();
            GetName_Function = getNameFunc;
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public void Save(string Path)
        {
            File.WriteAllText(Path, GenerateFileData());
        }

        /// <summary>
        /// Clear all settings
        /// </summary>
        public void Clear()
        {
            settingsList?.Clear();
            settingsJsonList?.Clear();
        }

        /// <summary>
        /// Clear all settings and set functions to default
        /// </summary>
        public void ClearAll()
        {
            settingsList?.Clear();
            settingsJsonList?.Clear();
            GetName_Function = DefaultGetNameFunc;
        }

        /// <summary>
        /// Generate file data to be saved in file
        /// </summary>
        public string GenerateFileData()
        {
            if (settingsList == null)
            {
                return string.Empty;
            }

            settingsJsonList = new Dictionary<string, T>();

            // Convert list
            foreach (T item in settingsList)
            {
                settingsJsonList.Add(GetName_Function(item), item);
            }

            return Serialize(settingsJsonList);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public Dictionary<string, T> Load(string Path)
        {
            if (File.Exists(Path))
            {
                // Load settings from raw data
                settingsJsonList = Deserialize(File.ReadAllText(Path));
                settingsList = settingsJsonList.Values.ToList();

                if (settingsList == null)
                {
                    settingsList = new List<T>();
                }

                if (settingsJsonList == null)
                {
                    settingsJsonList = new Dictionary<string, T>();
                }
                return settingsJsonList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check if the specified setting is already existing
        /// </summary>
        private T CheckExisting(T value)
        {
            return CheckExisting(GetName_Function(value));
        }

        /// <summary>
        /// Check if the specified setting is already existing
        /// </summary>
        private T CheckExisting(string name)
        {
            return settingsList.Where((s) => GetName_Function(s) == name).FirstOrDefault();
        }

        /// <summary>
        /// Get current setting
        /// </summary>
        public T GetSetting(string Key)
        {
            return CheckExisting(Key);
        }

        /// <summary>
        /// Get all settings
        /// </summary>
        public List<T> GetSettings()
        {
            return settingsList;
        }

        /// <summary>
        /// Get all names from loaded setting
        /// </summary>
        public Dictionary<string, T> GetNames()
        {
            return settingsJsonList;
        }

        /// <summary>
        /// Add specified setting
        /// </summary>
        public void AddSetting(T Value)
        {
            if (Value == null)
            {
                return;
            }

            if (GetName_Function(Value) == string.Empty)
            {
                return;
            }

            // Check if entry existing
            T setting = GetSetting(GetName_Function(Value));
            if (setting == null)
            {
                // Not existing, add new
                settingsList.Add(Value);
            }
            else
            {
                // Existing, replacing
                int index = settingsList.IndexOf(setting);
                settingsList[index] = Value;
            }
        }

        /// <summary>
        /// Add range of settings
        /// </summary>
        public void AddSettingRange(List<T> data)
        {
            foreach (var item in data)
            {
                AddSetting(item);
            }
        }

        /// <summary>
        /// Add range of settings
        /// </summary>
        public void AddSettingRange(T[] data)
        {
            foreach (var item in data)
            {
                AddSetting(item);
            }
        }

        /// <summary>
        /// Add range of settings from json raw text
        /// </summary>
        public void AddSettingRange(string data)
        {
            Dictionary<string, T> list = Deserialize(data);

            if (list == null)
            {
                return;
            }

            foreach (var item in list)
            {
                AddSetting(item.Value);
            }
        }

        /// <summary>
        /// Remove setting from name
        /// </summary>
        public void RemoveSetting(string name)
        {
            T setting = CheckExisting(name);
            if (setting != null)
            {
                settingsList.Remove(setting);
            }
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static Dictionary<string, T> Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, T>>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string Serialize(Dictionary<string, T> data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
