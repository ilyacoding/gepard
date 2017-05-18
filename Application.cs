using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration;
using Gepard.Configuration.Auth;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;
using Gepard.Controllers;
using Gepard.Core;

namespace Gepard
{
    public class Application
    {
        private ErrorHandler ErrorHandler { get; set; }

        public string ExecutionPath { get; set; }
        public HttpServer HttpServer { get; set; }

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

            ErrorHandler = new ErrorHandler(Path.Combine(ExecutionPath, "logs", ServerConfig.ErrorLog));

            try
            {
                VirtualHostConfigHandler = new VirtualHostConfigHandler(virtualHostConfigSerializer);
                VirtualHostList = VirtualHostConfigHandler.LoadFromFile(Path.Combine(ExecutionPath, configDirectory, "vhosts.xml"));
            }
            catch (Exception)
            {
                ErrorHandler.WriteCriticalError("Error parsing vhosts.xml configuration file.");
            }

            var chainControllerHandler = new ChainControllerHandler();
            
            chainControllerHandler.Reg(new BasicAuthHandler());
            chainControllerHandler.Reg(new WebSiteOne());
            chainControllerHandler.Reg(new PartGetMethodHandler());
            chainControllerHandler.Reg(new GetMethodHandler(ServerConfig.DirectoryRoot));
            
            var controllerHandler = new ControllerHandler(VirtualHostList, chainControllerHandler, ServerConfig.ServerName, ServerConfig.DirectoryRoot);

            HttpServer = new HttpServer(ServerConfig, VirtualHostList, controllerHandler);

            HttpServer.Start();
            Console.ReadKey();
            HttpServer.Stop();
        }
    }
}
