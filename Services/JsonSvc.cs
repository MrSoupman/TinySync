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
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            //byte[] json = JsonSerializer.SerializeToUtf8Bytes<object>(data, options);
            //string json = JsonSerializer.Serialize<object>(data,options);
            using (var stream = File.Create("data.json"))
            {
                string json = "";
                int offset = 0;
                int length;
                for (int i = 0; i < data.Count; i++)
                {
                    
                    json = JsonSerializer.Serialize(data[i], data[i].GetType(), options);
                    if (i == 0)
                        json = '[' + json;
                    if (i != data.Count - 1)
                    {
                        json += ',';
                    }
                    else
                        json += ']';
                    length = json.Length;
                    stream.Write(Encoding.UTF8.GetBytes(json),0,length);
                    offset += length;
                    
                }
                
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
                catch (JsonException)
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
