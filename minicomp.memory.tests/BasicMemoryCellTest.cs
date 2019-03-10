using System;
using minicomp.memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace minicomp.memory.tests
{
    [TestClass]
    public class BasicMemoryCellTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            BasicMemoryCell cell = new BasicMemoryCell(5);

            Assert.AreEqual(5, cell.Location);
            Assert.AreEqual(0, cell.Value);
        }

        [TestMethod]
        public void TestUpdateValue()
        {
            BasicMemoryCell cell = new BasicMemoryCell(0);
            bool callbackcalled = false;
            cell.OnMemoryCellUpdated += (c, oldVal, newVal) =>
            {
                Assert.AreEqual(c, cell);
                Assert.AreEqual(0, oldVal);
                Assert.AreEqual(1, newVal);
                callbackcalled = true;
            };

            cell.Value = 1;
            Assert.IsTrue(callbackcalled);
        }
    }
}
