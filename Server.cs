using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace Gepard
{
    internal class Server
    {
        private HttpServer serv { get; set; }

        public Server(int port)
        {
            serv = new HttpServer(port);
        }

        private static void Main(string[] args)
        {
            //ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            //configMap.ExeConfigFilename = @"d:\test\justAConfigFile.config.whateverYouLikeExtension";
            //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

//            var HttpConfig = new VirtualHostHttpConfig(@"D:\Crypto\GitHub\gepard\bin\Debug\http.config");

//            Console.WriteLine(HttpConfig.VirtualHosts[0].Fields.ToString());

            new Server(8000);
        }
    }
}
