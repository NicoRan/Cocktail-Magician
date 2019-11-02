using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cocktail_Magician_DB.DataSeeder.IOSeed
{
    public class ReadFileDatabase
    {
        public static List<T> ReadJsonsSeeds<T>(string address)
        {
            var json = File.ReadAllText(address);
            var settings = new JsonSerializerSettings();
            var lists = JsonConvert.DeserializeObject<List<T>>(json, settings);
            return lists;
        }
    }
}
