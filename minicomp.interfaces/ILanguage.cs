using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.interfaces
{
    public interface ILanguage
    {
        void ExecuteNextInstruction(IProcessor proc);
    }
}
