using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.common.datacontracts
{
    public class MemoryDefinitions : List<MemoryDefinition>
    {
        public static readonly int LATEST_VERSION = 1;
        public static readonly int[] SUPPORTED_VERSIONS = new int[] { 1 };

        public static MemoryDefinitions ParseFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                dynamic filecontents = JsonConvert.DeserializeObject(file.ReadToEnd());
                return ParseFromFile(filecontents);
            }
        }

        public void WriteToFile(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            using (JsonWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Version");
                writer.WriteValue(LATEST_VERSION);
                writer.WritePropertyName("Definitions");
                writer.WriteStartArray();
                foreach(var defs in this)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("Location");
                    writer.WriteValue(defs.Location);
                    writer.WritePropertyName("Value");
                    writer.WriteValue(defs.Value);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
        }

        private static MemoryDefinitions ParseFromFile(dynamic fileContents)
        {
            int version = fileContents.Version;
            if(!SUPPORTED_VERSIONS.Contains(version))
            {
                throw new ParserException(string.Format("Memory definition file version {0} not supported. Supported versions are {1}", version, String.Join(",",SUPPORTED_VERSIONS)));
            }
            switch(version)
            {
                case 1:
                    return ProcessV1Version(fileContents.Definitions);
                default:
                    throw new ParserException(string.Format("Memory definition file version {0} not supported. Supported versions are {1}", version, String.Join(",", SUPPORTED_VERSIONS)));
            }
        }

        private static MemoryDefinitions ProcessV1Version(dynamic definitions)
        {
            MemoryDefinitions retVal = new MemoryDefinitions();
            foreach(var item in definitions)
            {
                MemoryDefinition def = new MemoryDefinition()
                {
                    Location = item.Location,
                    Value = item.Value
                };
                retVal.Add(def);
            }
            return retVal;
        }
    }

    public class MemoryDefinition
    {
        public long Location { get; set; }
        public byte Value { get; set; }
    }
}
