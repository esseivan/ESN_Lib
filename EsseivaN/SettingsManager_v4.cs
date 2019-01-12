using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Controls
{
    /*  Example of utilsation :
     *  SettingsManager_v2 setma = new SettingsManager_v2();
     *  setma.addSetting
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */

    /// <summary>
    /// Manage settings using json
    /// </summary>
    public class SettingsManager_v4<T>
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
        public Func<T, string> getName { get; set; }

        /// <summary>
        /// Default getName function
        /// </summary>
        /// <returns>value.toString()</returns>
        public string defaultGetNameFunc(T value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Create a new settings manager with default getName function (Not recommended)
        /// </summary>
        public SettingsManager_v3()
        {
            settingsList = new List<T>();
            getName = defaultGetNameFunc;
        }

        /// <summary>
        /// Create a new settings manager with custom getName function
        /// </summary>
        /// <param name="getNameFunc">Function to get the name of the setting</param>
        public SettingsManager_v3(Func<T, string> getNameFunc)
        {
            settingsList = new List<T>();
            getName = getNameFunc;
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public void save(string Path)
        {
            File.WriteAllText(Path, generateFileData());
        }

        /// <summary>
        /// Generate file data to be saved in file
        /// </summary>
        public string generateFileData()
        {
            if (settingsList == null)
                return string.Empty;

            settingsJsonList = new Dictionary<string, T>();

            // Convert list
            foreach (T item in settingsList)
            {
                settingsJsonList.Add(getName(item), item);
            }

            return serialize(settingsJsonList);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public void load(string Path)
        {
            // Load settings from raw data
            settingsJsonList = deserialize(File.ReadAllText(Path));

            if (settingsList == null)
            {
                settingsList = new List<T>();
            }
        }
        
        /// <summary>
        /// Check if the specified setting is already existing
        /// </summary>
        private T checkExisting(T value)
        {
            return checkExisting(getName(value));
        }

        /// <summary>
        /// Check if the specified setting is already existing
        /// </summary>
        private T checkExisting(string name)
        {
            return settingsList.Where((s) => getName(s) == name).FirstOrDefault();
        }

        /// <summary>
        /// Get current setting
        /// </summary>
        public T getSetting(string Key)
        {
            return checkExisting(Key);
        }

        /// <summary>
        /// Get all settings
        /// </summary>
        public List<T> getSettings()
        {
            return settingsList;
        }

        /// <summary>
        /// Add specified setting
        /// </summary>
        public void addSetting(T Value)
        {
            if (Value == null)
            {
                return;
            }

            if (getName(Value) == string.Empty)
            {
                return;
            }

            // Check if entry existing
            T setting = getSetting(getName(Value));
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
        public void addSettingRange(List<T> data)
        {
            foreach (var item in data)
            {
                addSetting(item);
            }
        }

        /// <summary>
        /// Add range of settings
        /// </summary>
        public void addSettingRange(T[] data)
        {
            foreach (var item in data)
            {
                addSetting(item);
            }
        }

        /// <summary>
        /// Add range of settings from json raw text
        /// </summary>
        public void addSettingRange(string data)
        {
            Dictionary<string, T> list = deserialize(data);

            foreach (var item in list)
            {
                addSetting(item.Value);
            }
        }

        /// <summary>
        /// Remove setting from name
        /// </summary>
        public void removeSetting(string name)
        {
            T setting = checkExisting(name);
            if (setting != null)
            {
                settingsList.Remove(setting);
            }
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static Dictionary<string, T> deserialize(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, T>>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string serialize(Dictionary<string, T> data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
