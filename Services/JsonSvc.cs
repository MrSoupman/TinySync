using System.Text.Json;
using TinySync.Model;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.ObjectModel;

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

        public static List<Metadata> LoadJson()
        {
            if (File.Exists("data.json"))
            {
                string json = File.ReadAllText("data.json");
                try
                {
                    List<Metadata> data = JsonSerializer.Deserialize<List<Metadata>>(json);
                    return data;
                }
                catch (JsonException j)
                {
                    //In the event the json file is empty or something
                    return new List<Metadata>();
                }
            }
            else 
            {
                File.Create("data.json");
                return new List<Metadata>();
            
            }
        }
    }
}
