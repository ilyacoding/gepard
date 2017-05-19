using System;
using System.IO;
using System.Reflection;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;
using Gepard.Controllers;
using Gepard.Core;
using Gepard.Core.Main;

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
        
        public Application(string configDirectory, ServerConfigSerializer serverConfigSerializer, VirtualHostConfigSerializer virtualHostConfigSerializer)
        {
            ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (ExecutionPath == null) throw new ApplicationException("Can' get execution path.");

            try
            {
                ServerConfigHandler = new ServerConfigHandler(serverConfigSerializer);
                ServerConfig = ServerConfigHandler.LoadFromFile(Path.Combine(ExecutionPath, configDirectory, "server.xml"));
            }
            catch
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
            catch
            {
                ErrorHandler.WriteCriticalError("Error parsing vhosts.xml configuration file.");
            }

            var chainControllerHandler = new ChainControllerHandler();

            chainControllerHandler.Reg(new DigestAuthHandler());
            chainControllerHandler.Reg(new BasicAuthHandler());

            chainControllerHandler.Reg(new OptionsMethodHandler());
            chainControllerHandler.Reg(new PostMethodHandler());
            chainControllerHandler.Reg(new GetHeadMethodHandler(ServerConfig.DirectoryRoot));
            
            var controllerHandler = new ControllerHandler(VirtualHostList, chainControllerHandler, ServerConfig.ServerName, ServerConfig.DirectoryRoot);

            HttpServer = new HttpServer(ServerConfig, controllerHandler);
        }

        public void Start()
        {
            HttpServer.Start();
        }

        public void Stop()
        {
            try
            {
                HttpServer.Stop();
            }
            catch
            {
                // ignored
            }
        }
    }
}
