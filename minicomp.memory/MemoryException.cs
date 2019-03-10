using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.memory
{
    public class MemoryException : Exception
    {
        public MemoryException()
        {
        }

        public MemoryException(string message) : base(message)
        {
        }

        public MemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
