using Newtonsoft.Json;
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
    public class SettingsManager_v2
    {
        /// <summary>
        /// List of settings
        /// </summary>
        private List<Setting> settingsList;

        /// <summary>
        /// List of json settings
        /// </summary>
        private Dictionary<string, Setting_json> settingsJsonList;

        /// <summary>
        /// Create a new settings manager
        /// </summary>
        public SettingsManager_v2()
        {
            settingsList = new List<Setting>();
        }

        /// <summary>
        /// Load settings from a specified path
        /// </summary>
        public SettingsManager_v2(string Path)
        {
            settingsList = new List<Setting>();
            load(Path);
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
            settingsJsonList = new Dictionary<string, Setting_json>();

            // Convert list
            foreach (Setting item in settingsList)
            {
                settingsJsonList.Add(item.Name, convert(item));
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
            // Convert
            settingsList = convertList(settingsJsonList);

            if (settingsList == null)
            {
                settingsList = new List<Setting>();
            }
        }
        
        /// <summary>
        /// Convert seting_json dictionary to setting_list
        /// </summary>
        private List<Setting> convertList(Dictionary<string, Setting_json> list)
        {
            List<Setting> newList = new List<Setting>();

            foreach (var item in list)
            {
                newList.Add(convert(item.Value, item.Key));
            }

            return newList;
        }

        /// <summary>
        /// Convert Settnig_json to Setting
        /// </summary>
        private Setting convert(Setting_json setting_json, string name)
        {
            return new Setting() { Data = setting_json.Data, ChildSetting = setting_json.ChildSetting, Name = name };
        }

        /// <summary>
        /// Convert Setting_json to Setting
        /// </summary>
        private Setting convert(Setting_json setting_json)
        {
            return new Setting() { Data = setting_json.Data, ChildSetting = setting_json.ChildSetting };
        }

        /// <summary>
        /// Convert Setting to Setting_json
        /// </summary>
        private Setting_json convert(Setting setting)
        {
            return new Setting_json() { ChildSetting = setting.ChildSetting, Data = setting.Data };
        }

        /// <summary>
        /// Check if the specified setting is already existing
        /// </summary>
        private Setting checkExisting(Setting setting)
        {
            return checkExisting(setting.Name);
        }

        /// <summary>
        /// Check if the specified setting is already existing
        /// </summary>
        private Setting checkExisting(string name)
        {
            return settingsList.Where((s) => s.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Get current setting
        /// </summary>
        public Setting getSetting(string Key)
        {
            return checkExisting(Key);
        }

        /// <summary>
        /// Get all settings
        /// </summary>
        public List<Setting> getSettings()
        {
            return settingsList;
        }

        /// <summary>
        /// Add specified setting
        /// </summary>
        public void addSetting(Setting Value)
        {
            if (Value == null)
            {
                return;
            }

            if (Value.Name == string.Empty)
            {
                return;
            }

            // Check if entry existing
            Setting setting = getSetting(Value.Name);
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
        /// set specified setting
        /// </summary>
        public void setChildSetting(string name, Setting childSetting)
        {
            if (childSetting == null)
            {
                return;
            }

            if (childSetting.Name == string.Empty)
            {
                return;
            }

            // Check if entry existing
            Setting setting = getSetting(name);
            if (setting == null)
            {
                // Not existing, abort
                return;
            }

            // Existing, replacing
            int index = settingsList.IndexOf(setting);
            setting.ChildSetting = childSetting;
            settingsList[index] = setting;
        }

        /// <summary>
        /// Add range of settings
        /// </summary>
        /// <param name="data"></param>
        public void addSettingRange(List<Setting> data)
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
            Dictionary<string, Setting_json> list = deserialize(data);

            foreach (var item in list)
            {
                addSetting(convert(item.Value));
            }
        }

        /// <summary>
        /// Remove setting from name
        /// </summary>
        public void removeSetting(string name)
        {
            Setting setting = checkExisting(name);
            if (setting != null)
            {
                settingsList.Remove(setting);
            }
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static Dictionary<string, Setting_json> deserialize(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Setting_json>>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string serialize(Dictionary<string, Setting_json> data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        /// <summary>
        /// Class used to generate the json file
        /// </summary>
        private class Setting_json
        {
            public object Data { get; set; }
            public Setting ChildSetting { get; set; }
        }

        /// <summary>
        /// Class used by the user to add new entries
        /// </summary>
        public class Setting
        {
            public string Name { get; set; }
            public object Data { get; set; }
            public Setting ChildSetting { get; set; }
        }

    }
}
