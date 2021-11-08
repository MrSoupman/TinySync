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

        public static IList<Metadata> LoadJson()
        {
            if (File.Exists("data.json"))
            {
                using (StreamReader reader = new StreamReader("data.json"))
                {

                    return null;
                }
            }
            else
                return new List<Metadata>();
        }
    }
}
