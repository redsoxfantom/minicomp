using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.processor
{
    public class BasicProcessor : IProcessor
    {
        private IDictionary<string, IRegister> registers;
        private IMemory memory;
        private ILanguage instructionSet;

        public void ExecuteNextInstruction()
        {
            try
            {
                instructionSet.ExecuteNextInstruction(this, memory);
            }
            catch(Exception ex)
            {
                throw new ProcessorException("Failed to process next instruction", ex);
            }
        }

        public byte GetMemoryValue(long memoryLocation)
        {
            try
            {
                return memory.GetByte(memoryLocation);
            }
            catch(Exception ex)
            {
                throw new ProcessorException(string.Format("Failed to get memory value from location {0}", memoryLocation), ex);
            }
        }

        public long GetRegisterValue(string registerName)
        {
            ValidateRegister(registerName);
            return registers[registerName].Value;
        }

        public void Initialize(IMemory memory, Dictionary<string, IRegister> registers, ILanguage instructionSet)
        {
            this.memory = memory;
            this.registers = registers;
            this.instructionSet = instructionSet;
        }

        public void UpdateMemoryValue(byte newValue, long memoryLocation)
        {
            try
            {
                memory.UpdateMemoryLocation(memoryLocation, newValue);
            }
            catch(Exception ex)
            {
                throw new ProcessorException(String.Format("Error while updating memory location {0} to {1}",memoryLocation,newValue),ex);
            }
        }

        public void UpdateRegister(string registerName, long newValue)
        {
            ValidateRegister(registerName);
            registers[registerName].Value = newValue;
        }

        private void ValidateRegister(string registerName)
        {
            if (!registers.ContainsKey(registerName))
            {
                throw new ProcessorException(string.Format("Register {0} doesn't exist", registerName));
            }
        }
    }
}
