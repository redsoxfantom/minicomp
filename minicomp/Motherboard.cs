using log4net;
using minicomp.common.datacontracts;
using minicomp.interfaces;
using minicomp.memory;
using minicomp.processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace minicomp
{
    public class Motherboard
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Motherboard));
        IProcessor proc;
        IMemory memory;
        ILanguage lang;
        bool debug;
        int speed;

        public Motherboard(bool debug = false)
        {
            this.debug = debug;
        }

        public void Initialize(ComputerDefinition computerDefinition, MemoryDefinitions memoryDefinition)
        {
            memory = MemoryFactory.CreateMemory(computerDefinition.MemoryInfo.Type);
            memory.Initialize(computerDefinition.MemoryInfo.Size);
            foreach(var memDef in memoryDefinition)
            {
                memory.UpdateMemoryLocation(memDef.Location, memDef.Value);
            }

            Dictionary<string, IRegister> registers = new Dictionary<string, IRegister>();
            foreach(var registerDef in computerDefinition.RegisterDefinitions)
            {
                string name = registerDef.Name;
                IRegister reg = RegisterFactory.CreateRegister(registerDef.Type);
                registers.Add(name, reg);
            }
            proc = ProcessorFactory.CreateProcessor(computerDefinition.ProcessorType);
            proc.Initialize(memory, registers, lang);
            speed = computerDefinition.Speed;
        }

        public void Execute()
        {
            while(true)
            {
                proc.ExecuteNextInstruction();
                Thread.Sleep(speed);
                if(debug)
                {
                    logger.Info("Press [enter] to continue");
                    Console.ReadLine();
                }
            }
        }
    }
}
