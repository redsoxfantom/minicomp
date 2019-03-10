using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.memory
{
    public class MemoryFactory
    {
        public static IMemory CreateMemory(string type)
        {
            if(type == "Basic")
            {
                return new BasicMemory();
            }
            else
            {
                Type memType = Type.GetType(type);
                return (IMemory)Activator.CreateInstance(memType);
            }
        }
    }
}
