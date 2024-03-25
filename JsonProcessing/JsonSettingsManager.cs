using Newtonsoft.Json;
using System.IO;

namespace AppliedJobsManager.JsonProcessing
{
    public class JsonSettingsManager : JsonDirectory
    {
        private readonly string _jsonAppDataPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\JobsManager\settings.json";

        public JsonSettingsManager()
        {
            CreateDirectoryIfDoesntExist(_jsonAppDataPath);
        }

        public Settings.Settings GetSettings()
        {
            if (File.Exists(_jsonAppDataPath))
            {
                var json = File.ReadAllText(_jsonAppDataPath);
                var settings = JsonConvert.DeserializeObject<Settings.Settings>(json, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                if (settings is not null)
                {
                    return settings;
                }
            }

            return new Settings.Settings();
        }

        public void SaveSettings(Settings.Settings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_jsonAppDataPath, json);
        }      
    }
}
