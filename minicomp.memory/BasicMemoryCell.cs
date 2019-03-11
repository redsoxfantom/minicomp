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
        private byte _value;

        public BasicMemoryCell(long location)
        {
            Location = location;
            Value = 0;
        }

        public byte Value
        {
            get
            {
                return _value;
            }
            set
            {
                byte oldValue = _value;
                _value = value;
                OnMemoryCellUpdated?.Invoke(this, oldValue, _value);
            }
        }

        public long Location { get; private set; }

        public event MemoryCellUpdatedEventArgs OnMemoryCellUpdated;
    }
}
