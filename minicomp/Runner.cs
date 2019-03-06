using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp
{
    public class Runner
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Runner));

        public Runner()
        {

        }

        public void ParseArgs(string[] args)
        {
            logger.InfoFormat("Parsing arguments {0}", String.Join(" ", args));
            bool debug = false;
            string computerDefinitionFile = string.Empty;
            string memoryDefinitionFile = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if(arg == "-d")
                {
                    debug = true;
                }
                if(arg == "-computerdefinition")
                {
                    i++;
                    arg = args[i];
                    computerDefinitionFile = arg;
                }
                if(arg == "-memorydefinition")
                {
                    i++;
                    arg = args[i];
                }
            }
        }

        public void Run()
        {
            logger.Info("Beginning execution");
        }
    }
}
