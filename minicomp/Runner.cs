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
        }

        public void Run()
        {
            logger.Info("Beginning execution");
        }
    }
}
