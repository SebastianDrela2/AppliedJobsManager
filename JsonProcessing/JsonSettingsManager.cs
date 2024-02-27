using Newtonsoft.Json;
using System.IO;

namespace AppliedJobsManager.JsonProcessing
{
    public class JsonSettingsManager : IJsonSettings
    {
        private readonly string _jsonAppDataPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\JobsManager\settings.json";

        public JsonSettingsManager()
        {
            CreateDirectoryIfDoesntExist();
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

        public void CreateDirectoryIfDoesntExist()
        {
            var aboveDir = Path.GetDirectoryName(_jsonAppDataPath);

            if (!Directory.Exists(aboveDir))
            {
                Directory.CreateDirectory(aboveDir!);
            }
        }
    }
}
