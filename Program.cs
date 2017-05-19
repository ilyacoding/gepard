using System;
using System.ServiceProcess;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard
{
    internal class Program
    {
        private static void Main()
        {
            //var application = new Application("config", new ServerConfigSerializer(), new VirtualHostConfigSerializer());
            //application.Start();
            //Console.ReadKey();
            //application.Stop();
            var servicesToRun = new ServiceBase[]
            {
                new GepardService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
