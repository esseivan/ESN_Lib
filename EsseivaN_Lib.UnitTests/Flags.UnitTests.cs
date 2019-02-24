using EsseivaN.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EsseivaN.UnitTests
{
    [TestClass]
    public class Flags_UnitTests
    {
        [TestMethod]
        public void Flags_Infinite_SetBits_OneSlot()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(0, 32, writeData);
            // Retrieve data
            int readData = flags_Infinite.Flags[0];
            // Check 
            Assert.AreEqual(readData, (int)writeData);
        }

        [TestMethod]
        public void Flags_Infinite_SetBits_TwoSlots_Symetrical()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(16, 32, writeData);
            // Retrieve data
            int mask = 0x0000ffff; // 0xFFFF0000
            int t0 = ((flags_Infinite.Flags[1] & mask) * (int)Math.Pow(2,16)) & ~mask;
            int t1 = ((flags_Infinite.Flags[0] & ~mask) / (int)Math.Pow(2, 16)) & mask;
            int readData = (int)(t0 + t1);
            // Check 
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void Flags_Infinite_SetBits_TwoSlots_Asymetrical()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(28, 32, writeData);
            // Retrieve data
            int mask = 0x0fffffff; // 0xFFFF0000
            int t0 = ((flags_Infinite.Flags[1] & mask) * (int)Math.Pow(2, 4));
            int t1 = ((flags_Infinite.Flags[0] & ~mask) / (int)Math.Pow(2, 28)) & mask;
            int readData = (int)(t0 + t1);
            // Check 
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void Flags_Infinite_SetBits_Slot_Not1_Asymetrical()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(68, 32, writeData);
            // Retrieve data
            int readData = flags_Infinite.getBits(68, 32);
            // Check 
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void Flags_Infinite_GetBits_OneSlot()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(0, 32, writeData);
            // Retrieve data
            int readData = flags_Infinite.getBits(0, 32);
            // Check 
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void Flags_Infinite_GetBits_TwoSlots_Symetrical()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(16, 32, writeData);
            // Retrieve data
            int readData = flags_Infinite.getBits(16, 32);
            // Check 
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void Flags_Infinite_GetBits_TwoSlots_Asymetrical()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int writeData = 0x55A5A5A5;
            flags_Infinite.setBits(28, 32, writeData);
            // Retrieve data
            int readData = flags_Infinite.getBits(28, 32);
            // Check 
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void Flags_Infinite_GetBits_640bits()
        {
            // Create object
            Flags_infinite flags_Infinite = new Tools.Flags_infinite();
            // Initialize content
            int[] Pattern_index = { 0, 40, 80, 120, 160, 200, 235, 270, 305, 340 };
            Random rnd = new Random();
            int[] writeData = new int[10];
            for (int i = 0; i < 10; i++)
            {
                writeData[i] = (int)(rnd.NextDouble() * int.MaxValue);
                flags_Infinite.setBits(Pattern_index[i], 32, writeData[i]);
            }
            // Retrieve data
            int[] readData = new int[10];
            for (int i = 0; i < 10; i++)
            {
                readData[i] = flags_Infinite.getBits(Pattern_index[i], 32);
            }
            // Check 
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(readData[i], writeData[i]);
            }
        }
    }
}
