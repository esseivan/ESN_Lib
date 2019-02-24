using System;
using System.Collections.Generic;

namespace EsseivaN.Tools
{
    public class Flags_32
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

    public class Flags_64
    {
        public long Register { get; set; } = 0;
        private const long maxCount = 64;

        public bool getBit(long index)
        {
            return getBits(index, 1) > 0;
        }

        public long getBits(long startIndex)
        {
            return getBits(startIndex, maxCount - startIndex);
        }

        public long getBits(long startIndex, long count)
        {
            long mask = getMask(startIndex, count);
            return (long)((Register & mask) / Math.Pow(2, startIndex));
        }

        public void setBit(long index, bool value)
        {
            setBits(index, 1, value ? 1 : 0);
        }

        public void setBits(long startIndex, long value)
        {
            setBits(startIndex, maxCount - startIndex, value);
        }

        public void setBits(long startIndex, long count, long value)
        {
            long mask = getMask(startIndex, count);
            Register &= ~mask;
            Register |= ((long)(value * Math.Pow(2, startIndex))) & mask;
        }

        public string displayBinary()
        {
            return displayBinary(0, maxCount);
        }

        public string displayBinary(long startIndex)
        {
            return displayBinary(startIndex, maxCount - startIndex);
        }

        public string displayBinary(long startIndex, long count)
        {
            return Convert.ToString(getBits(startIndex, count), 2);
        }

        public long getMask(long startIndex, long count)
        {
            return ((long)Math.Pow(2, count) - 1) * (long)Math.Pow(2, startIndex);
        }
    }

    public class Flags_infinite
    {
        public List<int> Flags { get; set; }
        private const int maxCount = 32;

        public Flags_infinite()
        {
            Flags = new List<int>();
        }

        /// <summary>
        /// Get a single bit
        /// </summary>
        public bool getBit(int index)
        {
            return getBits(index, 1) > 0;
        }

        /// <summary>
        /// Get a range of bits
        /// </summary>
        public long getBits(int startIndex)
        {
            return getBits(startIndex, maxCount - startIndex);
        }

        /// <summary>
        /// Get a range of bits
        /// </summary>
        public int getBits(int startIndex, int count)
        {
            // Invalid count
            if (count > maxCount)
            {
                return 0;
            }

            int t_count = count,
                t_index = startIndex,
                t_count2 = 0;

            int list_index = t_index / maxCount;
            t_index = t_index % maxCount;

            // If overflow, limit count
            if (t_index + t_count > maxCount)
            {
                t_count = maxCount - t_index;
                t_count2 = count - t_count;
            }
            
            if (Flags.Count < list_index + 1)
            {
                return 0;
            }

            long mask = getMask(t_index, t_count);
            long t0 = Flags[list_index] & mask;
            int t1 = (int)Math.Pow(2, t_index);
            long output = (long)(t0 / t1);
            Console.WriteLine(output);
            if (t_count2 > 0)
            {
                if (Flags.Count < list_index + 2)
                {
                    return 0;
                }

                mask = getMask(0, t_count2);
                t0 = Flags[list_index + 1] & mask;
                t1 = (int)Math.Pow(2, t_count);
                output += (long)(t0 * t1);
            }

            return (int)output;
        }

        public void setBit(int index, bool value)
        {
            setBits(index, 1, value ? 1 : 0);
        }

        public void setBits(int startIndex, long value)
        {
            setBits(startIndex, maxCount - startIndex, value);
        }

        public void setBits(int startIndex, int count, long value)
        {
            // Invalid count
            if (count > maxCount)
            {
                return;
            }

            int t_count = count,
                t_index = startIndex,
                t_count2 = 0;

            int list_index = t_index / maxCount;
            t_index = t_index % maxCount;

            // If overflow, limit count
            if (t_index + t_count > maxCount)
            {
                t_count = maxCount - t_index;
                t_count2 = count - t_count;
            }

            while (Flags.Count < list_index + 1)
            {
                Flags.Add(0);
            }

            long mask = getMask(t_index, t_count);
            long t_value = (long)(value * (long)Math.Pow(2, t_index)) & mask;
            long wReg = Flags[list_index];
            wReg &= ~mask;
            wReg |= t_value;
            Flags[list_index] = (int)wReg;

            if (t_count2 > 0)
            {
                while (Flags.Count < list_index + 2)
                {
                    Flags.Add(0);
                }

                mask = getMask(0, t_count2);
                t_value = (long)(value / (long)Math.Pow(2, t_count)) & mask;
                wReg = Flags[list_index + 1];
                wReg &= ~mask;
                wReg |= t_value;
                Flags[list_index + 1] = (int)wReg;
            }
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

        public long getMask(int startIndex, int count)
        {
            long t1 = (long)Math.Pow(2, count) - 1;
            long t2 = (long)Math.Pow(2, startIndex);
            return (long)(t1 * t2);
        }
    }
}
