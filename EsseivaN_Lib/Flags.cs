using System;

namespace EsseivaN.Tools
{
    public class Flags
    {
        public ulong Register { get; set; } = 0;
        private const int maxCount = 64;

        public bool getBit(int index)
        {
            return getBits(index, 1) > 0;
        }

        public ulong getBits(int startIndex)
        {
            return getBits(startIndex, maxCount - startIndex);
        }

        public ulong getBits(int startIndex, int count)
        {
            ulong mask = getMask(startIndex, count);
            return (ulong)((Register & mask) / Math.Pow(2, startIndex));
        }

        public void setBit(int index, bool value)
        {
            setBits(index, 1, (ulong)(value ? 1 : 0));
        }

        public void setBits(int startIndex, ulong value)
        {
            setBits(startIndex, maxCount - startIndex, value);
        }

        public void setBits(int startIndex, int count, ulong value)
        {
            ulong mask = getMask(startIndex, count);
            Register &= ~mask;
            Register |= ((ulong)(value * Math.Pow(2, startIndex))) & mask;
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
            string result = Convert.ToString((int)(getBits(startIndex, count) / Math.Pow(2, 32)), 2);
            if (result == "0")
            {
                result = "";
            }

            result += Convert.ToString((int)getBits(startIndex, count), 2);
            return result;
        }

        public ulong getMask(int startIndex, int count)
        {
            return ((ulong)Math.Pow(2, count) - 1) * (ulong)Math.Pow(2, startIndex);
        }
    }
}
