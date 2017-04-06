using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using Gepard.Configuration;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Application(args, "config", new ServerConfigSerializer(), new VirtualHostConfigSerializer());
        }
    }
}
