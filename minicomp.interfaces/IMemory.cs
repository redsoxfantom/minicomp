using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.interfaces
{
    public interface IMemory
    {
        void Initialize(long size);
        void UpdateMemoryLocation(long location, byte newValue);
        byte GetMemoryValue(long location);
        void SubscribeForMemoryCell(long location, MemoryCellUpdatedEventArgs handler);
        void UnsubscribeForMemoryCell(long location, MemoryCellUpdatedEventArgs handler);
        void SubscribeForMemoryCells(long startLocation, long endLocation, MemoryCellUpdatedEventArgs handler);
        void UnsubscribeForMemoryCells(long startLocation, long endLocation, MemoryCellUpdatedEventArgs handler);
    }

    public interface IMemoryCell
    {
        byte Value { get; set; }
        long Location { get; }
        event MemoryCellUpdatedEventArgs OnMemoryCellUpdated;
    }

    public delegate void MemoryCellUpdatedEventArgs(IMemoryCell cell, byte oldValue, byte newValue);
}
