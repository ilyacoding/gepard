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
            var request = Request.Parse(str);
            request.VirtualHost = VirtualHostList.GetVirtualHost(request.Host);
            var controller = ControllerRegistry.Get(request.Method);

            //Console.WriteLine(controller.GetType().ToString());
            var response = controller.Execute(request);

            // View Response -> byte[]

            var data = $"{Gepard.HttpServer.HttpVersion} {response.Status}\r\nContent-type: {response.Mime}\r\nAccept-Ranges: bytes\r\nContent-Length: {response.Data.Length}\r\n\r\n";
            var dataBytes = Encoding.UTF8.GetBytes(data);

            return Combine(dataBytes, response.Data);
            
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }
}
