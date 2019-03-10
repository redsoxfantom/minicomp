using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.common.datacontracts
{
    public class ComputerDefinition
    {
        public static readonly int LATEST_VERSION = 1;
        public static readonly int[] SUPPORTED_VERSIONS = new int[] { 1 };

        public string ProcessorType { get; set; }
        public int FileVersion { get; set; }
        public string InstructionSet { get; set; }
        public List<RegisterInfo> RegisterDefinitions { get; set; }
        public MemoryInfo MemoryInfo { get; set; }
        public long Speed { get; set; }

        public static ComputerDefinition ParseFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                dynamic filecontents = JsonConvert.DeserializeObject(file.ReadToEnd());
                return ParseFromFile(filecontents);
            }
        }

        private static ComputerDefinition ParseFromFile(dynamic fileContents)
        {
            int version = fileContents.Version;
            if (!SUPPORTED_VERSIONS.Contains(version))
            {
                throw new ParserException(string.Format("Computer definition file version {0} not supported. Supported versions are {1}", version, String.Join(",", SUPPORTED_VERSIONS)));
            }
            switch (version)
            {
                case 1:
                    return ProcessV1Version(fileContents);
                default:
                    throw new ParserException(string.Format("Computer definition file version {0} not supported. Supported versions are {1}", version, String.Join(",", SUPPORTED_VERSIONS)));
            }
        }

        private static ComputerDefinition ProcessV1Version(dynamic fileContents)
        {
            ComputerDefinition definition = new ComputerDefinition();
            definition.FileVersion = fileContents.Version;

            dynamic processorDef = fileContents.Processor;
            definition.ProcessorType = processorDef.Type;
            definition.InstructionSet = processorDef.InstructionSet;
            definition.Speed = processorDef.Speed;
            dynamic registerDefs = processorDef.Registers;
            List<RegisterInfo> parsedRegisterDefs = new List<RegisterInfo>();
            foreach(var registerDef in registerDefs)
            {
                parsedRegisterDefs.Add(new RegisterInfo()
                {
                    Name = registerDef.Name,
                    Type = registerDef.Type
                });
            }
            definition.RegisterDefinitions = parsedRegisterDefs;
            dynamic memoryInfo = fileContents.Memory;
            MemoryInfo parsedMemoryInfo = new MemoryInfo()
            {
                Type = memoryInfo.Type,
                Size = memoryInfo.Size
            };
            definition.MemoryInfo = parsedMemoryInfo;

            return definition;
        }

        public void WriteToFile(string fileName)
        {

            using (StreamWriter file = new StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Version");
                writer.WriteValue(LATEST_VERSION);
                writer.WritePropertyName("Processor");
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                writer.WriteValue(ProcessorType);
                writer.WritePropertyName("Speed");
                writer.WriteValue(Speed);
                writer.WritePropertyName("InstructionSet");
                writer.WriteValue(InstructionSet);
                writer.WritePropertyName("Registers");
                writer.WriteStartArray();
                foreach(var register in RegisterDefinitions)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("Type");
                    writer.WriteValue(register.Type);
                    writer.WritePropertyName("Name");
                    writer.WriteValue(register.Name);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
                writer.WritePropertyName("Memory");
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                writer.WriteValue(MemoryInfo.Type);
                writer.WritePropertyName("Size");
                writer.WriteValue(MemoryInfo.Size);
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
        }
    }

    public class RegisterInfo
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class MemoryInfo
    {
        public string Type { get; set; }
        public long Size { get; set; }
    }
}
