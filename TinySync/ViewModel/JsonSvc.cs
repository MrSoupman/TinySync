using System.Text.Json;
using TinySync.Model;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.ObjectModel;

namespace TinySync.ViewModel
{
    public class JsonSvc
    {
        public static string Serialize(ObservableCollection<Metadata> data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static void SaveJson(ObservableCollection<Metadata> data)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(data,options);
            using (var stream = File.OpenWrite("data.json"))
            {
                stream.Write(json);
            }
        }

        public static ObservableCollection<Metadata> LoadJson()
        {
            if (File.Exists("data.json"))
            {
                string json = File.ReadAllText("data.json");
                ObservableCollection<Metadata> data = JsonSerializer.Deserialize<ObservableCollection<Metadata>>(json);
                return data;
            }
            else 
            {
                File.Create("data.json");
                return new ObservableCollection<Metadata>();
            
            }
        }
    }
}
