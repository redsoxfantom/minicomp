using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.processor
{
    public class ProcessorFactory
    {
        public static IProcessor CreateProcessor(string type)
        {
            if(type == "Basic")
            {
                return new BasicProcessor();
            }
            else
            {
                Type proctype = Type.GetType(type);
                return (IProcessor)Activator.CreateInstance(proctype);
            }
        }
    }
}
