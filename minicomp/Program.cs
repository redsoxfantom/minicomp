using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicomp
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger("main");

            try
            {
                logger.Info("Starting up...");

                Runner proc = new Runner();
                proc.ParseArgs(args);
                proc.Run();
                logger.Info("Shutting down...");
            }
            catch(Exception ex)
            {
                logger.Fatal("Exception while running minicomp", ex);
            }
        }
    }
}
