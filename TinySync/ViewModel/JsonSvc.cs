using System.Text.Json;
using TinySync.Model;
using System.Collections.Generic;
using System;

namespace TinySync.ViewModel
{
    class JsonSvc
    {
        public string Serialize(Dictionary<string, Metadata> data)
        {
            Console.WriteLine(JsonSerializer.Serialize(data));
            return "";
        }
    }
}
