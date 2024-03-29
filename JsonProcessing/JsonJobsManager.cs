﻿using AppliedJobsManager.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace AppliedJobsManager.JsonProcessing;

public class JsonJobsManager : JsonDirectory
{
    private readonly string _jsonAppDataPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\JobsManager\jobs.json";

    public JsonJobsManager()
    {
        CreateDirectoryIfDoesntExist(_jsonAppDataPath);
    }

    public ObservableCollection<Row> LoadJobs()
    {
        if (File.Exists(_jsonAppDataPath))
        {
            var json = File.ReadAllText(_jsonAppDataPath);
            var dataItems = JsonConvert.DeserializeObject<ObservableCollection<Row>>(json, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            if (dataItems is not null)
            {
                return dataItems;
            }
        }

        return new ObservableCollection<Row>();
    }

    public void SaveJobs(IList<Row> dataItems)
    {
        var json = JsonConvert.SerializeObject(dataItems);
        File.WriteAllText(_jsonAppDataPath, json);
    }
}