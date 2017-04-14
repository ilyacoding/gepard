using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Core;
using Gepard.Models;

namespace Gepard.Controllers
{
    public class GetController : IController
    {
        public ServerConfig ServerConfig { get; set; }
        public FileHandler FileHandler { get; set; }

        public GetController(ServerConfig serverConfig, FileHandler fileHandler)
        {
            ServerConfig = serverConfig;
            FileHandler = fileHandler;
        }

        public Response Execute(Request request)
        {
            var model = new GetModel(ServerConfig, FileHandler);
            return model.Execute(request);
        }
    }
}
