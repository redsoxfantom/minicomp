using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace minicomp.memory.tests
{
    [TestClass]
    public class BasicMemoryTest
    {
        private BasicMemory target;

        [TestInitialize]
        public void Init()
        {
            target = new BasicMemory();
            target.Initialize(8);
            target.UpdateMemoryLocation(0, 0x01);
            target.UpdateMemoryLocation(1, 0x02);
            target.UpdateMemoryLocation(2, 0x03);
            target.UpdateMemoryLocation(3, 0x04);
            target.UpdateMemoryLocation(4, 0x05);
            target.UpdateMemoryLocation(5, 0x06);
            target.UpdateMemoryLocation(6, 0x07);
            target.UpdateMemoryLocation(7, 0x08);
        }

        [TestMethod]
        public void TestGetByte()
        {
            Assert.AreEqual(0x01, target.GetByte(0));
            Assert.AreEqual(0x02, target.GetByte(1));
            Assert.AreEqual(0x03, target.GetByte(2));
            Assert.AreEqual(0x04, target.GetByte(3));
            Assert.AreEqual(0x05, target.GetByte(4));
            Assert.AreEqual(0x06, target.GetByte(5));
            Assert.AreEqual(0x07, target.GetByte(6));
            Assert.AreEqual(0x08, target.GetByte(7));
        }

        [TestMethod]
        [ExpectedException(typeof(MemoryException))]
        public void TestGetByteError()
        {
            target.GetByte(8);
        }

        [TestMethod]
        public void TestGetShort()
        {
            Assert.AreEqual(0x0102, target.GetShort(0));
            Assert.AreEqual(0x0203, target.GetShort(1));
            Assert.AreEqual(0x0304, target.GetShort(2));
            Assert.AreEqual(0x0405, target.GetShort(3));
            Assert.AreEqual(0x0506, target.GetShort(4));
            Assert.AreEqual(0x0607, target.GetShort(5));
            Assert.AreEqual(0x0708, target.GetShort(6));
        }

        [TestMethod]
        [ExpectedException(typeof(MemoryException))]
        public void TestGetShortError()
        {
            target.GetShort(7);
        }

        [TestMethod]
        public void TestGetInt()
        {
            Assert.AreEqual(0x01020304, target.GetInt(0));
            Assert.AreEqual(0x02030405, target.GetInt(1));
            Assert.AreEqual(0x03040506, target.GetInt(2));
            Assert.AreEqual(0x04050607, target.GetInt(3));
            Assert.AreEqual(0x05060708, target.GetInt(4));
        }

        [TestMethod]
        [ExpectedException(typeof(MemoryException))]
        public void TestGetIntError()
        {
            target.GetInt(5);
        }

        [TestMethod]
        public void TestGetLong()
        {
            Assert.AreEqual(0x0102030405060708, target.GetLong(0));
        }

        [TestMethod]
        [ExpectedException(typeof(MemoryException))]
        public void TestGetLongError()
        {
            target.GetLong(1);
        }
    }
}
