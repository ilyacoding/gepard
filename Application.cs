using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;
using Gepard.Controllers;
using Gepard.Core;

namespace Gepard
{
    public class Application
    {
        public string ExecutionPath { get; set; }
        public HttpServer TcpServer { get; set; }

        public ServerConfig ServerConfig { get; set; }
        public VirtualHostList VirtualHostList { get; set; }

        public ServerConfigHandler ServerConfigHandler { get; set; }
        public VirtualHostConfigHandler VirtualHostConfigHandler { get; set; }

        public Application(string[] argStrings, string configDirectory, ServerConfigSerializer serverConfigSerializer, VirtualHostConfigSerializer virtualHostConfigSerializer)
        {
            ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (ExecutionPath == null) throw new ApplicationException("Can' get execution path.");

            try
            {
                ServerConfigHandler = new ServerConfigHandler(serverConfigSerializer);
                ServerConfig = ServerConfigHandler.LoadFromFile(Path.Combine(ExecutionPath, configDirectory, "server.xml"));
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing server.xml configuration file.");
                return;
            }

            try
            {
                VirtualHostConfigHandler = new VirtualHostConfigHandler(virtualHostConfigSerializer);
                VirtualHostList = VirtualHostConfigHandler.LoadFromFile(Path.Combine(ExecutionPath, configDirectory, "vhosts.xml"));
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing vhosts.xml configuration file.");
                return;
            }

            var fileHandler = new FileHandler(ServerConfig);

            var controllerRegistry = new ControllersRegistry(ServerConfig);
            controllerRegistry.RegDefault(new NotImplementedController());

            controllerRegistry.Reg("GET", new GetController(ServerConfig, fileHandler));
//            controllerRegistry.Reg("POST", new PostController());

            var controllerHandler = new ControllerHandler(controllerRegistry, VirtualHostList, ServerConfig);

            TcpServer = new HttpServer(ServerConfig, VirtualHostList, controllerHandler);
            TcpServer.Start();
            Console.ReadKey();
            TcpServer.Stop();
        }
    }
}
