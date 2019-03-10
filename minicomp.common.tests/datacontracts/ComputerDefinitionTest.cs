using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minicomp.common.datacontracts;

namespace minicomp.common.tests.datacontracts
{
    [TestClass]
    public class ComputerDefinitionTest
    {
        [TestMethod]
        public void ParseTest()
        {
            ComputerDefinition def = new ComputerDefinition();
            def.InstructionSet = "InstructionSet";
            def.ProcessorType = "Processor";
            def.MemoryInfo = new MemoryInfo()
            {
                Type = "Memory",
                Size = 16
            };
            def.RegisterDefinitions = new List<RegisterInfo>();
            def.RegisterDefinitions.Add(new RegisterInfo() { Type = "Reg1", Name = "Register1" });
            def.RegisterDefinitions.Add(new RegisterInfo() { Type = "Reg2", Name = "Register2" });
            def.WriteToFile(Path.Combine(Path.GetTempPath(), "ComputerDef.json"));

            ComputerDefinition newDef = ComputerDefinition.ParseFromFile(Path.Combine(Path.GetTempPath(), "ComputerDef.json"));
            Assert.AreEqual(def.InstructionSet, newDef.InstructionSet);
            Assert.AreEqual(def.ProcessorType, newDef.ProcessorType);
            Assert.AreEqual(def.MemoryInfo.Type, newDef.MemoryInfo.Type);
            Assert.AreEqual(def.MemoryInfo.Size, newDef.MemoryInfo.Size);
            Assert.AreEqual(def.RegisterDefinitions.Count, newDef.RegisterDefinitions.Count);
            Assert.AreEqual(def.RegisterDefinitions[0].Type, newDef.RegisterDefinitions[0].Type);
            Assert.AreEqual(def.RegisterDefinitions[0].Name, newDef.RegisterDefinitions[0].Name);
            Assert.AreEqual(def.RegisterDefinitions[1].Type, newDef.RegisterDefinitions[1].Type);
            Assert.AreEqual(def.RegisterDefinitions[1].Name, newDef.RegisterDefinitions[1].Name);
        }
    }
}
