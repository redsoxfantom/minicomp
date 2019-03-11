using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.memory
{
    public class BasicMemory : IMemory
    {
        private List<BasicMemoryCell> memoryCells;

        public byte GetByte(long location)
        {
            ValidateLocation(location);
            return memoryCells[(int)location].Value;
        }

        public int GetInt(long location)
        {
            ValidateLocation(location, 4);
            byte b1 = memoryCells[(int)location].Value;
            byte b2 = memoryCells[(int)location + 1].Value;
            byte b3 = memoryCells[(int)location + 2].Value;
            byte b4 = memoryCells[(int)location + 3].Value;

            return (int)((b1 << 24) + (b2 << 16) + (b3 << 8) + b4);
        }

        public long GetLong(long location)
        {
            ValidateLocation(location, 8);
            byte b1 = memoryCells[(int)location].Value;
            byte b2 = memoryCells[(int)location + 1].Value;
            byte b3 = memoryCells[(int)location + 2].Value;
            byte b4 = memoryCells[(int)location + 3].Value;
            byte b5 = memoryCells[(int)location + 4].Value;
            byte b6 = memoryCells[(int)location + 5].Value;
            byte b7 = memoryCells[(int)location + 6].Value;
            byte b8 = memoryCells[(int)location + 7].Value;

            return (long)(((long)b1 << 56) + 
                          ((long)b2 << 48) + 
                          ((long)b3 << 40) + 
                          ((long)b4 << 32) + 
                          ((long)b5 << 24) + 
                          ((long)b6 << 16) + 
                          ((long)b7 << 8)  + 
                          b8);
        }

        public short GetShort(long location)
        {
            ValidateLocation(location, 2);
            byte b1 = memoryCells[(int)location].Value;
            byte b2 = memoryCells[(int)location+1].Value;

            return (short)((b1 << 8) + b2);
        }

        public byte[] GetValues(long location, long length)
        {
            ValidateLocation(location, length);
            byte[] newArray = new byte[length];
            for(long i = location; i < location + length; i++)
            {
                newArray[(int)i - (int)location] = memoryCells[(int)i].Value;
            }
            return newArray;
        }

        public void Initialize(long size)
        {
            if(size > int.MaxValue)
            {
                throw new MemoryException(String.Format("Requested size {0} too large for a BasicMemory (max size: {1})", size, int.MaxValue));
            }

            memoryCells = new List<BasicMemoryCell>((int)size);
            for(int i = 0; i < size; i++)
            {
                memoryCells.Add(new BasicMemoryCell(i));
            }
        }

        public void SubscribeForMemoryCell(long location, MemoryCellUpdatedEventArgs handler)
        {
            ValidateLocation(location);
            memoryCells[(int)location].OnMemoryCellUpdated += handler;
        }

        public void SubscribeForMemoryCells(long startLocation, long endLocation, MemoryCellUpdatedEventArgs handler)
        {
            ValidateLocation(startLocation);
            ValidateLocation(endLocation);

            for(int i = (int)startLocation; i < (int)endLocation; i++)
            {
                memoryCells[i].OnMemoryCellUpdated += handler;
            }
        }

        public void UnsubscribeForMemoryCell(long location, MemoryCellUpdatedEventArgs handler)
        {
            ValidateLocation(location);
            memoryCells[(int)location].OnMemoryCellUpdated -= handler;
        }

        public void UnsubscribeForMemoryCells(long startLocation, long endLocation, MemoryCellUpdatedEventArgs handler)
        {
            ValidateLocation(startLocation);
            ValidateLocation(endLocation);

            for (int i = (int)startLocation; i < (int)endLocation; i++)
            {
                memoryCells[i].OnMemoryCellUpdated -= handler;
            }
        }

        public void UpdateMemoryLocation(long location, byte newValue)
        {
            ValidateLocation(location);
            memoryCells[(int)location].Value = newValue;
        }

        private void ValidateLocation(long location, long size = 1)
        {
            if (location >= memoryCells.Count || location + (size - 1) >= memoryCells.Count)
            {
                throw new MemoryException(String.Format("Memory location {0} and length {1} out of range of available memory {2}", location, size, memoryCells.Count));
            }
        }
    }
}
