using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace Bank
{
    class BackendJSON
    {
        public static void JsonStore(List<Bank> BankList, string filePath)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            StreamWriter sw = new StreamWriter(filePath);
            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSerializer.Serialize(jsonWriter, BankList);

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
    }
}
