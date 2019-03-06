using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory
{
    public class BasicMemory : IMemory
    {
        private List<BasicMemoryCell> memoryCells;

        public byte GetMemoryValue(long location)
        {
            ValidateLocation(location);
            return memoryCells[(int)location].Value;
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

        private void ValidateLocation(long location)
        {
            if (location > memoryCells.Count)
            {
                throw new MemoryException(String.Format("Memory location {0} out of range of available memory {1}", location, memoryCells.Count));
            }
        }
    }
}
