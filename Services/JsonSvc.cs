using System.Text.Json;
using TinySync.Model;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace TinySync.Services
{
    public class JsonSvc
    {
        public static string Serialize(ObservableCollection<Metadata> data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static void SaveJson(List<Metadata> data)
        {
            using (var stream = File.Create("data.json"))
            {
                var options = new JsonSerializerOptions()
                {
                    Converters =
                    {
                        new MetadataConverter()
                    }
                };
                string json = JsonSerializer.Serialize<List<Metadata>>(data, options);
                stream.Write(Encoding.UTF8.GetBytes(json), 0, json.Length);
            }
        }

        public static List<Metadata> LoadJson()
        {
            if (File.Exists("data.json"))
            {
                string json = File.ReadAllText("data.json");
                try
                {
                    var options = new JsonSerializerOptions()
                    {
                        Converters =
                    {
                        new MetadataConverter()
                    }
                    };
                    List<Metadata> data = JsonSerializer.Deserialize<List<Metadata>>(json,options);
                    return data;
                }
                catch (JsonException)
                {
                    //In the event the json file is empty or something
                    return new List<Metadata>();
                }
            }
            else 
            {
                using (File.Create("data.json"))
                return new List<Metadata>();
            
            }
        }
    }
}
