using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.memory
{
    public class BasicMemoryCell : IMemoryCell
    {
        public BasicMemoryCell(long location)
        {
            Location = location;
            Value = 0;
        }

        public byte Value
        {
            get
            {
                return Value;
            }
            set
            {
                byte oldValue = Value;
                Value = value;
                OnMemoryCellUpdated?.Invoke(this, oldValue, Value);
            }
        }

        public long Location
        {
            get
            {
                return Location;
            }
            private set
            {
                Location = value;
            }
        }

        public event MemoryCellUpdatedEventArgs OnMemoryCellUpdated;
    }
}
