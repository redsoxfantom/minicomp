using minicomp.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp.processor
{
    public class RegisterFactory
    {
        public static IRegister CreateRegister(string type)
        {
            switch (type)
            {
                case "8bit":
                    return new ByteRegister();
                case "16bit":
                    return new ShortRegister();
                case "32bit":
                    return new IntRegister();
                default:
                    Type registerType = Type.GetType((string)type);
                    return (IRegister)Activator.CreateInstance(registerType);
            }
        }
    }
}
