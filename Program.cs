using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.ServiceProcess;
using Gepard.Configuration;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;
using Gepard.Controllers;

namespace Gepard
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new GepardService()
            };
            ServiceBase.Run(ServicesToRun);
            //var application = new Application(args, "config", new ServerConfigSerializer(), new VirtualHostConfigSerializer());
            //application.Start();
            //Console.ReadKey();
            //application.Stop();
        }
    }
}
