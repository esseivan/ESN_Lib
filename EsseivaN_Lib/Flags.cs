using System;
using System.Collections.Generic;

namespace EsseivaN.Tools
{
    [Obsolete("This class is no longer updated, use Flags class")]
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

    [Obsolete("This class is no longer updated, use Flags class")]
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

    /// <summary>
    /// Easily use flags with a specific length of bits
    /// </summary>
    public class Flags
    {
        /// <summary>
        /// List that contains all flags data
        /// </summary>
        public List<int> FlagList { get; set; }
        /// <summary>
        /// Number of bit per element of the FlagList (Int32)
        /// </summary>
        public const int maxCount = 32;

        public Flags()
        {
            FlagList = new List<int>();
        }

        /// <summary>
        /// Check if FlagList is valid for write/read
        /// </summary>
        public bool CheckValid()
        {
            return FlagList != null;
        }

        #region GetSet

        /// <summary>
        /// Get a single bit at the specified index
        /// </summary>
        public bool getBit(int index)
        {
            return getBits(index, 1) > 0;
        }

        /// <summary>
        /// Get a range of bits from the startIndex to startIndex + maxCount
        /// </summary>
        public int getBits(int startIndex)
        {
            return getBits(startIndex, maxCount);
        }

        /// <summary>
        /// Get a range of bits
        /// </summary>
        public int getBits(int startIndex, int count)
        {
            if (!CheckValid())
            {
                FlagList = new List<int>();
                return -1;
            }

            // Invalid count, set to maxCount
            if (count > maxCount)
            {
                count = maxCount;
            }

            int t_count = count,
                t_index = startIndex,
                t_count2 = 0;

            // Get index for the list
            int list_index = t_index / maxCount;
            t_index = t_index % maxCount;

            // If overflow, do in 2 steps for 2 elements of the list
            if (t_index + t_count > maxCount)
            {
                t_count = maxCount - t_index;
                t_count2 = count - t_count;
            }
            
            // If index not existing, return -1
            if (FlagList.Count < list_index + 1)
            {
                return -1;
            }

            // Get the mask to retrieve only the wanted data
            long mask = getMask(t_index, t_count);
            // Get the value of the first element of the list
            long t0 = FlagList[list_index] & mask;
            int t1 = (int)Math.Pow(2, t_index);
            long output = (t0 / t1);

            // If count 2nd element not 0
            if (t_count2 > 0)
            {
                // If element not existing, abort and return the output
                if (FlagList.Count < list_index + 2)
                {
                    return (int)output;
                }

                // Get the mask to retrieve only the wanted data
                mask = getMask(0, t_count2);

                // Add the value of the first element of the list
                t0 = FlagList[list_index + 1] & mask;
                t1 = (int)Math.Pow(2, t_count);
                output += (t0 * t1);
            }

            return (int)output;
        }

        /// <summary>
        /// Set a specific bit
        /// </summary>
        public void setBit(int index, bool value)
        {
            setBits(index, 1, value ? 1 : 0);
        }

        /// <summary>
        /// Set a specific range from startIndex to startIndex + maxCount
        /// </summary>
        public void setBits(int startIndex, long value)
        {
            setBits(startIndex, maxCount, value);
        }

        /// <summary>
        /// Set a specific range
        /// </summary>
        public void setBits(int startIndex, int count, long value)
        {
            if (!CheckValid())
            {
                FlagList = new List<int>();
            }

            // Invalid count, set to maxCount
            if (count > maxCount)
            {
                count = maxCount;
            }

            int t_count = count,
                t_index = startIndex,
                t_count2 = 0;

            // Get index for the list
            int list_index = t_index / maxCount;
            t_index = t_index % maxCount;

            // If overflow, do in 2 steps for 2 elements of the list
            if (t_index + t_count > maxCount)
            {
                t_count = maxCount - t_index;
                t_count2 = count - t_count;
            }

            // While not enough flags, add new
            while (FlagList.Count < list_index + 1)
            {
                FlagList.Add(0);
            }

            // Get the mask to retrieve only the wanted data
            long mask = getMask(t_index, t_count);
            // Get the value for the first element of the list
            long t_value = (value * (long)Math.Pow(2, t_index)) & mask;
            // Apply the value to the element of the list (without touching others values)
            long wReg = FlagList[list_index];
            wReg &= ~mask;
            wReg |= t_value;
            FlagList[list_index] = (int)wReg;

            // If count 2nd element not 0
            if (t_count2 > 0)
            {
                // While not enough flags, add new
                while (FlagList.Count < list_index + 2)
                {
                    FlagList.Add(0);
                }

                // Get the mask to retrieve only the wanted data
                mask = getMask(0, t_count2);
                // Get the value for the first element of the list
                t_value = (value / (long)Math.Pow(2, t_count)) & mask;
                // Apply the value to the element of the list (without touching others values)
                wReg = FlagList[list_index + 1];
                wReg &= ~mask;
                wReg |= t_value;
                FlagList[list_index + 1] = (int)wReg;
            }
        }

        /// <summary>
        /// Get the mask for the specified rage
        /// </summary>
        public long getMask(int startIndex, int count)
        {
            long t1 = (long)Math.Pow(2, count) - 1;
            long t2 = (long)Math.Pow(2, startIndex);
            return (t1 * t2);
        }

        #endregion GetSet

        /// <summary>
        /// Display the data in binary from startIndex to startIndex + maxCount
        /// </summary>
        public string displayBinary(int startIndex)
        {
            return displayBinary(startIndex, maxCount - startIndex);
        }

        /// <summary>
        /// Display the data in binary
        /// </summary>
        public string displayBinary(int startIndex, int count)
        {
            return Convert.ToString(getBits(startIndex, count), 2);
        }

        /// <summary>
        /// Display the data in binary from startIndex to startIndex + maxCount
        /// </summary>
        public string displayHex(int startIndex)
        {
            return displayHex(startIndex, maxCount - startIndex);
        }

        /// <summary>
        /// Display the data in binary
        /// </summary>
        public string displayHex(int startIndex, int count)
        {
            return Convert.ToString(getBits(startIndex, count), 16);
        }
    }
}
