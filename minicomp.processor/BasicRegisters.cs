using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.processor
{
    public class IntRegister : IRegister
    {
        private int internalValue;

        public long Value
        {
            get
            {
                return internalValue;
            }
            set
            {
                if(value > int.MaxValue)
                {
                    throw new RegisterException(String.Format("Value {0} too large to store in int register", value));
                }
                int oldValue = internalValue;
                internalValue = (int)value;
                OnRegisterUpdated?.Invoke(this, oldValue, internalValue);
            }
        }

        public event RegisterUpdatedArgs OnRegisterUpdated;

        public long NumBits()
        {
            return 32;
        }
    }

    public class ShortRegister : IRegister
    {
        private short internalValue;

        public long Value
        {
            get
            {
                return internalValue;
            }
            set
            {
                if (value > short.MaxValue)
                {
                    throw new RegisterException(String.Format("Value {0} too large to store in int register", value));
                }
                short oldValue = internalValue;
                internalValue = (short)value;
                OnRegisterUpdated?.Invoke(this, oldValue, internalValue);
            }
        }

        public event RegisterUpdatedArgs OnRegisterUpdated;

        public long NumBits()
        {
            return 16;
        }
    }

    public class ByteRegister : IRegister
    {
        private byte internalValue;

        public long Value
        {
            get
            {
                return internalValue;
            }
            set
            {
                if (value > byte.MaxValue)
                {
                    throw new RegisterException(String.Format("Value {0} too large to store in int register", value));
                }
                byte oldValue = internalValue;
                internalValue = (byte)value;
                OnRegisterUpdated?.Invoke(this, oldValue, internalValue);
            }
        }

        public event RegisterUpdatedArgs OnRegisterUpdated;

        public long NumBits()
        {
            return 8;
        }
    }
}
