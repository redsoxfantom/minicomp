using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.processor
{
    public class ProcessorException : Exception
    {
        public ProcessorException()
        {
        }

        public ProcessorException(string message) : base(message)
        {
        }

        public ProcessorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProcessorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
