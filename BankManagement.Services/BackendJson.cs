using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Bank
{
    public class BackendJson
    {
        public static void JsonStore(List<Bank> studentList, string filePath)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            StreamWriter sw = new StreamWriter(filePath);
            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSerializer.Serialize(jsonWriter, studentList);

            jsonWriter.Close();
            sw.Close();
        }

        public static List<Bank> JsonGet(string filePath)
        {
            var obj = new List<Bank>();

            JsonSerializer jsonSerializer = new JsonSerializer();
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                JsonReader jsonReader = new JsonTextReader(sr);
                obj = jsonSerializer.Deserialize<List<Bank>>(jsonReader);
                jsonReader.Close();
                sr.Close();
            }

            return obj;
        }

        public static void Save()
        {
            BackendJson.JsonStore(BankStore.Banks, "data.save");

        }

        public static void Load()
        {
            BankStore.Banks = BackendJson.JsonGet("data.save");

        }
    }
}
