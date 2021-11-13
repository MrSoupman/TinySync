using System.Text.Json;
using TinySync.Model;
using System.Collections.Generic;
using System.IO;
using System;

namespace TinySync.ViewModel
{
    public class JsonSvc
    {
        public static string Serialize(IList<Metadata> data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static void SaveJson(IList<Metadata> data)
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

        public static IList<Metadata> LoadJson()
        {
            if (File.Exists("data.json"))
            {
                string json = File.ReadAllText("data.json");
                IList<Metadata> data = JsonSerializer.Deserialize<List<Metadata>>(json);
                return data;
            }
            else 
            {
                File.Create("data.json");
                return new List<Metadata>();
            
            }
        }
    }
}
