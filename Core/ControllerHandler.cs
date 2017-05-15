using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard.Core
{
    public class ControllerHandler
    {
        private ControllersRegistry ControllerRegistry { get; set; }
        private VirtualHostList VirtualHostList { get; set; }

        public ControllerHandler(ControllersRegistry controllersRegistry, VirtualHostList virtualHostList)
        {
            ControllerRegistry = controllersRegistry;
            VirtualHostList = virtualHostList;
        }

        public byte[] Execute(string str)
        {
            var request = new Request(str);
            request.VirtualHost = VirtualHostList.GetVirtualHost(request.Host);
            var controller = ControllerRegistry.Get(request.Method);
            
            var response = controller.Execute(request);

            return response.GetBytes();
        }
    }
}
