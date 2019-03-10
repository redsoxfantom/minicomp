using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minicomp.common.datacontracts;

namespace minicomp.common.tests.datacontracts
{
    [TestClass]
    public class MemoryDefinitionsTest
    {
        [TestMethod]
        public void ParsingTest()
        {
            MemoryDefinitions defs = new MemoryDefinitions();
            defs.Add(new MemoryDefinition()
            {
                Location = 0,
                Value = 1
            });
            defs.Add(new MemoryDefinition()
            {
                Location = 2,
                Value = 3
            });
            defs.Add(new MemoryDefinition()
            {
                Location = 4,
                Value = 5
            });
            defs.Add(new MemoryDefinition()
            {
                Location = 6,
                Value = 7
            });

            defs.WriteToFile(Path.Combine(Path.GetTempPath(), "Output.json"));

            MemoryDefinitions parsedDefinitions = MemoryDefinitions.ParseFromFile(Path.Combine(Path.GetTempPath(), "Output.json"));
            Assert.AreEqual(4, parsedDefinitions.Count);
            TestDefinition(parsedDefinitions[0], 0, 1);
            TestDefinition(parsedDefinitions[1], 2, 3);
            TestDefinition(parsedDefinitions[2], 4, 5);
            TestDefinition(parsedDefinitions[3], 6, 7);
        }

        private void TestDefinition(MemoryDefinition def, long expectedLocation, byte expectedValue)
        {
            Assert.AreEqual(expectedLocation, def.Location);
            Assert.AreEqual(expectedValue, def.Value);
        }
    }
}
