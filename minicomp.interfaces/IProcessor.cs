using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.interfaces
{
    public interface IProcessor
    {
        void Initialize(IMemory memory, Dictionary<string,IRegister> registers, ILanguage instructionSet);
        void UpdateRegister(string registerName, long newValue);
        long GetRegisterValue(string registerName);
        void ExecuteNextInstruction();
        byte GetMemoryValue(long memoryLocation);
        void UpdateMemoryValue(byte newValue, long memoryLocation);
    }

    public interface IRegister
    {
        long Value { get; set; }
        event RegisterUpdatedArgs OnRegisterUpdated;
        long NumBits();
    }

    public delegate void RegisterUpdatedArgs(IRegister register, long oldValue, long newValue);
}
