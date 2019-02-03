using System;

namespace EsseivaN.Tools
{
    public class Flags
    {
        public int Register { get; set; } = 0;
        private const int maxCount = 32;

        public bool getBit(int index)
        {
            return getBits(index, 1) > 0;
        }

        public int getBits(int startIndex)
        {
            return getBits(startIndex, maxCount - startIndex);
        }

        public int getBits(int startIndex, int count)
        {
            int mask = getMask(startIndex, count);
            return (int)((Register & mask) / Math.Pow(2, startIndex));
        }

        public void setBit(int index, bool value)
        {
            setBits(index, 1, value ? 1 : 0);
        }

        public void setBits(int startIndex, int value)
        {
            setBits(startIndex, maxCount - startIndex, value);
        }

        public void setBits(int startIndex, int count, int value)
        {
            int mask = getMask(startIndex, count);
            Register &= ~mask;
            Register |= ((int)(value * Math.Pow(2, startIndex))) & mask;
        }

        public string displayBinary()
        {
            return displayBinary(0, maxCount);
        }

        public string displayBinary(int startIndex)
        {
            return displayBinary(startIndex, maxCount - startIndex);
        }

        public string displayBinary(int startIndex, int count)
        {
            return Convert.ToString(getBits(startIndex, count), 2);
        }

        public int getMask(int startIndex, int count)
        {
            return ((int)Math.Pow(2, count) - 1) * (int)Math.Pow(2, startIndex);
        }
    }
}
