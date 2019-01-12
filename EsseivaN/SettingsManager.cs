using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EsseivaN.Controls
{
    public class SettingsManager
    {
        private string SettingsPath;
        private Dictionary<string, Setting> SettingsList;

        private SettingsManager()
        {
        }

        public SettingsManager(string FilePath)
        {
            SettingsPath = FilePath;
            SettingsList = new Dictionary<string, Setting>();
        }

        public void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(SettingsList, Formatting.Indented);
            File.WriteAllText(SettingsPath, json);
        }

        public void RetrieveSettings()
        {
            SettingsList = JsonConvert.DeserializeObject<Dictionary<string, Setting>>(File.ReadAllText(SettingsPath));
        }

        public void AddSetting(string Key, object Value)
        {
            Setting setting = new Setting();
            setting.SetData(Value);
            if (SettingsList.ContainsKey(Key))
            {
                SettingsList[Key] = setting;
            }
            else
            {
                SettingsList.Add(Key, setting);
            }
        }

        public void AddSettingRange(Dictionary<string, object> data)
        {
            foreach (var item in data)
            {
                AddSetting(item.Key, item.Value);
            }
        }

        public void AddSettingRange(string data)
        {
            AddSettingRange(Deserialize(data));
        }

        public object GetSetting(string Key)
        {
            if (SettingsList.ContainsKey(Key))
            {
                return SettingsList[Key].GetData();
            }
            else
            {
                return null;
            }
        }

        public T GetSetting<T>(string Key)
        {
            if (SettingsList.ContainsKey(Key))
            {
                return SettingsList[Key].GetData<T>();
            }
            else
            {
                return (T)(object)null;
            }
        }

        public Dictionary<string, object> GetAllSettings()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var item in SettingsList)
            {
                result.Add(item.Key, item.Value.GetData());
            }
            return result;
        }

        public void RemoveSetting(string Key)
        {
            if (SettingsList.ContainsKey(Key))
            {
                SettingsList.Remove(Key);
            }
        }

        public static Dictionary<string, object> Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
        }

        public static string Serialize(Dictionary<string, object> data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        private class Setting
        {
            public object Data { get; set; }

            public T GetData<T>()
            {
                return (T)Data;
            }

            public object GetData()
            {
                return Data;
            }

            public void SetData(object Data)
            {
                this.Data = Data;
            }
        }
    }
}
