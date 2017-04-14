using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard.Controllers
{
    public class ControllerRegistry
    {
        private ServerConfig ServerConfig { get; set; }
        private Dictionary<string, IController> Dictionary { get; set; }

        public ControllerRegistry(ServerConfig serverConfig)
        {
            ServerConfig = serverConfig;
            Dictionary = new Dictionary<string, IController>();
        }

        public void Reg(string str, IController controller)
        {
            controller.ServerConfig = ServerConfig;
            Dictionary.Add(str, controller);
        }

        public IController Get(string str)
        {
            str = str.Trim();
            if (!Dictionary.ContainsKey(str)) throw new Exception("501");
            return Dictionary[str];
        }
    }
}
